using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Dal;
 

namespace BL
{

    public class BLֹֹ_imp : IBL
    {
        public Dal.Idal dal;
        public BLֹֹ_imp()
        {
            dal = Dal.FactoryDal.GetDal();
        }

        #region Child Funcs
        public void AddChild(Child inputChild)
        {
            if (inputChild.ChildAge > DateTime.Now)
                throw new Exception("Error, The date of birth can not be greater than the current date");
            if (inputChild.ChildAge < DateTime.Now.AddYears(-13)) 
                throw new Exception("Error, The child should be younger than 13 years of age");

            if (!BlTools.IsCorrectId(inputChild.ChildID))
                throw new Exception("Error ID, please enter ID with 6 to 9 numbers");


            dal.AddChild(inputChild);
        }
        public bool DeleteChild(int inputChildId)
        {
            Child tempChild = BlTools.GetChild(inputChildId);
            if (tempChild != null && tempChild.IsChildInContract())
                throw new Exception("Child with the same id found in contract. please delete all contracts the child is connected to, and then delete the child");

            return dal.DeleteChild(inputChildId);
        }
        public void UpdateChildDetails(Child inputChild)
        {
            dal.UpdateChildDetails(inputChild);
        }
        public IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null)
        {
            return dal.GetAllChilds(predicate);
        }
        #endregion

        #region Mother Funcs
        public void AddMom(Mother inputMother)
        {
            if (!BlTools.IsCorrectId(inputMother.MomID))
                throw new Exception("Error ID, please enter ID with 6 to 9 numbers");
            dal.AddMom(inputMother);
        }
        public bool DeleteMom(int inputMomId)
        {
            Mother tempMother = DalTools.GetMother(inputMomId);
            if (tempMother.IsMomHaveChilds())
                throw new Exception("Mother with the same id have child in system. please first remove the child and then remove the mother");
            return dal.DeleteMom(inputMomId);
        }
        public void UpdateMomDetails(Mother inputMom)
        {
            dal.UpdateMomDetails(inputMom);
        }
        public IEnumerable<Mother> GetAllMoms(Func<Mother, bool> predicate = null)
        {
            return dal.GetAllMothers(predicate);
        }
        #endregion

        #region Nanny Funcs
        public void AddNanny(Nanny inputNanny)
        {

            DateTime age = inputNanny.NannyDateOfBirth;
            age.AddYears(18);
            if (DateTime.Now < age)
                throw new Exception("Can't add Nanny under 18 age");

//checks if id is correct
            if (!BlTools.IsCorrectId(inputNanny.NannyId))
                throw new Exception("Error ID, please enter ID with 6 to 9 numbers");



            dal.AddNanny(inputNanny);
        }
        public bool DeleteNanny(int inputNannyId)
        {
            Nanny tempNanny = BlTools.GetNanny(inputNannyId);
            if (tempNanny!=null && tempNanny.IsNannyInContract())
                throw new Exception("Nanny with the same id found in contract. please delete all contracts the nanny is connected to, and then delete the child");
            return dal.DeleteNanny(inputNannyId);
        }
        public void UpdateNannyDetails(Nanny inputNanny)
        {
            Nanny oldNanny = BlTools.GetNanny(inputNanny.NannyId);

            DateTime tempDate = inputNanny.NannyDateOfBirth;
            tempDate.AddYears(18);
            if (DateTime.Now < tempDate)
                throw new Exception("Can't add Nanny under 18 age");

            //now will check if nanny have properties in contracts
            //that the new updates not fit to them
            var allNannyContracts = GetAllContracts(s => s.NannyID == inputNanny.NannyId);
            int numOfChildsNannyHave = allNannyContracts.Count();

            if (numOfChildsNannyHave > inputNanny.NannyMaxInfants)
                throw new Exception("Nanny have actual more infants then the new maximum she asking to update to. " +
                    "please remove some contracts until the new maximum will be good.");

            foreach (var item in allNannyContracts)
            {
                Child child = BlTools.GetChild(item.ChildID);
                int childAgeInMonths = child.AgeInMonths();
                if (childAgeInMonths > inputNanny.NannyMaxInfantAge || childAgeInMonths < inputNanny.NannyMinInfantAge)
                    throw new Exception("Nanny have contracts with childs that not match to "
                        + "new max/min age policy. please remove those childs");

                if (item.PaymentMethod == Payment_method.hourly && inputNanny.NannyIsHourlySalary == false)
                    throw new Exception("Nanny have contracts with hourly payments, cant change to monthly salary");

            }

            dal.UpdateNannyDetails(inputNanny);
        }
        public IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null)
        {
            return dal.GetAllNannies(predicate);
        }



        #endregion

        #region Contract Funcs

        public void AddContract(Contract inputContract)
        {
            Nanny nanny = DalTools.GetNanny(inputContract.NannyID);
            Child child = DalTools.GetChild(inputContract.ChildID);

            //cheak if the age of the infant is under 3 month
            DateTime age = (DalTools.GetChild(inputContract.ChildID)).ChildAge;
            age.AddMonths(3);
            if (DateTime.Now < age)
                throw new Exception("Can't add Child to Contract under 3 months");

            //checks if nanny capacity is full
            if (nanny.IsNannyCapacityIsFull())
                throw new Exception("Nanny hace reached the maximum infants capacity...");

            //checks if child age is not over the maximum/minimum nanny accepts
            int childAgeInMonths = child.AgeInMonths();

            if (childAgeInMonths < nanny.NannyMinInfantAge || childAgeInMonths > nanny.NannyMaxInfantAge) 
                throw new Exception("Nanny can't accept this age");

            if (inputContract.IsContractSigned)
            {
                //checks if contract salary calculated by month or by hour and updates the salary nanny
                if (inputContract.PaymentMethod == Payment_method.hourly) 
                    inputContract.HourlySalary = BlTools.CalculateHourlySalary(nanny, child);
                else
                    inputContract.MonthlySalary = BlTools.CalculateMonthlySalary(nanny, child);
            }



            BlTools.GetChild(inputContract.ChildID).IsHaveNanny = true;
            dal.AddContract(inputContract);


        }
        public bool DeleteContract(int inputContractId)
        {
            Child child = BlTools.GetChild(BL.BlTools.GetContract(inputContractId).ChildID);
            if (dal.DeleteContract(inputContractId))
            {
                child.IsHaveNanny = false;
                return true;
            }
            return false;
        }
        public void UpdateContractDetails(Contract inputContract)
        {
            Contract oldContract = BlTools.GetContract(inputContract.ContID);
            if (oldContract.IsIntroductoryMeeting == false && inputContract.IsContractSigned == true) 
                throw new Exception("You can not sign a contract before a meeting");
            // צריך לעשות בדיקה שאם שינינו תאריך או שיטת תשלום אך החוזה לא נחתם אז אי אפשר
           

            dal.UpdateContractDetails(inputContract);
        }

        public IEnumerable<Contract> GetAllContracts(Func<Contract, bool> predicate = null)
        {
            return dal.GetAllContracts(predicate);
        }



        #endregion

    


    }
}
