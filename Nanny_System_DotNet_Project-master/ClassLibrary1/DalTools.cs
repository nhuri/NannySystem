using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace Dal
{
   
    static public class DalTools
    {
        static Idal dl = FactoryDal.GetDal();
        //Getters
        public static Child GetChild(int inputId)
        {
            return dl.GetAllChilds().FirstOrDefault(c => c.ChildID == inputId);
        }
        public static Mother GetMother(int inputId)
        {
            return DataSource.MomsList.FirstOrDefault(m => m.MomID == inputId);
        }
        public static Nanny GetNanny(int inputId)
        {
            return DataSource.NanniesList.FirstOrDefault(n => n.NannyId == inputId);
        }
        public static Contract GetContract(int inputId)
        {
            return DataSource.ContractList.FirstOrDefault(c => c.ContID == inputId);
        }

        //"Copy counstructors"
        public static Mother CopyMother(this Mother inputMother)
        {
            Mother newMother = new Mother
            {
                MomID = inputMother.MomID,
                MomFirstName = inputMother.MomFirstName,
                MomFamilyName = inputMother.MomFamilyName,
                MomPhoneNum = inputMother.MomPhoneNum,
                MomAdress = inputMother.MomAdress,
                MomSearchAdress = inputMother.MomSearchAdress,
                MomComment = inputMother.MomComment,
                MomDaysNannyNeeds = CopyDaysArray(inputMother.MomDaysNannyNeeds),
                MomHoursNannyNeeds = CopyHoursArray(inputMother.MomHoursNannyNeeds)
            };
            return newMother;
        }
        public static Child CopyChild(this Child inputChild)
        {
            Child newChild = new Child
            {
                ChildID = inputChild.ChildID,
                ChildAge = inputChild.ChildAge,
                ChildIsSpecialNeeds = inputChild.ChildIsSpecialNeeds,
                ChildMomID = inputChild.ChildMomID,
                ChildName = inputChild.ChildName,
                ChildTypesOfSpecialNeeds = inputChild.ChildTypesOfSpecialNeeds
            };
            return newChild;
        }
        public static Nanny CopyNanny(this Nanny inputNanny)
        {
            Nanny newNanny = new Nanny
            {
                NannyId = inputNanny.NannyId,
                NannyPrivateName = inputNanny.NannyPrivateName,
                NannyFamilyName = inputNanny.NannyFamilyName,
                NannyDateOfBirth = inputNanny.NannyDateOfBirth,
                NannyPhoneNum = inputNanny.NannyPhoneNum,
                NannyAdress = inputNanny.NannyAdress,
                NannyIsElevator = inputNanny.NannyIsElevator,
                NannyFloor = inputNanny.NannyFloor,
                NannyYearsOfExperience = inputNanny.NannyYearsOfExperience,
                NannyMaxInfants = inputNanny.NannyMaxInfants,
                NannyMaxInfantAge = inputNanny.NannyMaxInfantAge,
                NannyMinInfantAge = inputNanny.NannyMinInfantAge,
                NannyIsHourlySalary = inputNanny.NannyIsHourlySalary,
                NannyHourlySalary = inputNanny.NannyHourlySalary,
                NannyMonthlySalary = inputNanny.NannyMonthlySalary,
                NannyIsMOE = inputNanny.NannyIsMOE,
                IsNannyHaveRecommendations = inputNanny.IsNannyHaveRecommendations,
                NannyRecommendations = inputNanny.NannyRecommendations,
                NannyWorkingDays = CopyDaysArray(inputNanny.NannyWorkingDays),
                NannyWorkingHours = CopyHoursArray(inputNanny.NannyWorkingHours)
            };
            return newNanny;
        }
        public static Contract CopyContract(this Contract inputContract)
        {
            Contract newContract = new Contract
            {
                ContID = inputContract.ContID,
                NannyID = inputContract.NannyID,
                ChildID = inputContract.ChildID,
                IsIntroductoryMeeting = inputContract.IsIntroductoryMeeting,
                IsContractSigned = inputContract.IsContractSigned,
                PaymentMethod = inputContract.PaymentMethod,
                HourlySalary = inputContract.HourlySalary,
                MonthlySalary = inputContract.MonthlySalary,
                StartDate = new DateTime(inputContract.StartDate.Year, inputContract.StartDate.Month, inputContract.StartDate.Day),
                EndDate = new DateTime(inputContract.EndDate.Year, inputContract.EndDate.Month, inputContract.EndDate.Day),
            };
            return newContract;
        }
        public static bool[] CopyDaysArray(bool[] inputArry)
        {
            bool[] newArray = new bool[6];
            for (int i = 0; i < 6; i++)
                newArray[i] = inputArry[i];
            return newArray;
        }
        public static List<WeeklyWorkSchedule> CopyHoursArray(List<WeeklyWorkSchedule> inputArry)
        {
            List<WeeklyWorkSchedule> newArray = new List<WeeklyWorkSchedule>(6);
            for (int i = 0; i < 6; i++)
            {  
                newArray.Add(null);
                newArray[i] = new WeeklyWorkSchedule();
                TimeSpan start = new TimeSpan(inputArry[i].StartTime.Hours, inputArry[i].StartTime.Minutes, 0);
                TimeSpan end = new TimeSpan(inputArry[i].EndTime.Hours, inputArry[i].EndTime.Minutes, 0);
                newArray[i].StartTime = start;
                newArray[i].EndTime = end;
            }
            return newArray;
        }

        public static void CheckifExistsId(int idForCheck) // Ido
        {
            Child tempChild = DalTools.GetChild(idForCheck);
            Mother tempMother = DalTools.GetMother(idForCheck);
            Nanny tempNanny = DalTools.GetNanny(idForCheck);

            if (tempNanny != null || tempMother != null || tempChild != null)
                throw new Exception("Error, Same id already exists");
        }
    }
}
