using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using System.Xml.Linq;
using System.IO;

namespace Dal
{
    class Dal_imp : Idal
    {
        #region Child Funcs
        /// <summary>
        /// add a child to database
        /// </summary>
        /// <param name="inputChild">child to add</param>
        public void AddChild(Child inputChild)
        {
            DalTools.CheckifExistsId(inputChild.ChildID); // ido
            Mother tempMom = DalTools.GetMother(inputChild.ChildMomID);
            if (tempMom == null)
                throw new Exception("The child's mother does not exist in the system, please add the mother before");
            DataSource.ChildsList.Add(inputChild);
        }
        /// <summary>
        /// delete a child from database
        /// </summary>
        /// <param name="inputChildId">id of child to delete</param>
        public bool DeleteChild(int inputChildId)
        {
            Child tempChild = DalTools.GetChild(inputChildId);
            if (tempChild == null)
                throw new Exception("Child with the same id not found");
            return DataSource.ChildsList.Remove(tempChild);
        }
        /// <summary>
        /// update details of child
        /// </summary>
        /// <param name="inputChild">new child after update</param>
        public void UpdateChildDetails(Child inputChild)
        {
            int index = DataSource.ChildsList.FindIndex(c => c.ChildID == inputChild.ChildID);
            if (index == -1)
                throw new Exception("Child with the same id not found...");
            DataSource.ChildsList[index] = inputChild;
        }
        /// <summary>
        /// return a collection of all children by some condition
        /// </summary>
        /// <param name="predicate">delegate accept a condition to check</param>
        /// <returns>collection of children</returns>
        public IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.ChildsList.AsEnumerable();

            return DataSource.ChildsList.Where(predicate);
        }
        #endregion

        #region Mother Funcs
        public void AddMom(Mother inputMother)
        {
            DalTools.CheckifExistsId(inputMother.MomID); // ido
            DataSource.MomsList.Add(inputMother);
        }
        public bool DeleteMom(int inputMomId)
        {
            Mother tempMother = DalTools.GetMother(inputMomId);
            if (tempMother == null)
                throw new Exception("Mother with the same id not found...");

            return DataSource.MomsList.Remove(tempMother);
        }
        public void UpdateMomDetails(Mother inputMom)
        {
            int index = DataSource.MomsList.FindIndex(m => m.MomID == inputMom.MomID);
            if (index == -1)
                throw new Exception("Mother with the same id not found...");
            DataSource.MomsList[index] = inputMom;
        }
        public IEnumerable<Mother> GetAllMothers(Func<Mother, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.MomsList.AsEnumerable();

            return DataSource.MomsList.Where(predicate);
        }
        #endregion

        #region Nanny Funcs
        public void AddNanny(Nanny inputNanny) 
        {
            DalTools.CheckifExistsId(inputNanny.NannyId);
            DataSource.NanniesList.Add(inputNanny);
        }
        public bool DeleteNanny(int inputNannyId)
        {
            Nanny tempNanny = DalTools.GetNanny(inputNannyId);
            if (tempNanny == null)
                throw new Exception("Nanny with the same id not found...");


            return DataSource.NanniesList.Remove(tempNanny);

        }
        public void UpdateNannyDetails(Nanny inputNanny)
        {
            int index = DataSource.NanniesList.FindIndex(n => n.NannyId == inputNanny.NannyId);
            if (index == -1)
                throw new Exception("Nanny with the same id not found...");
            DataSource.NanniesList[index] = inputNanny;
        }
        public IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.NanniesList.AsEnumerable();

            return DataSource.NanniesList.Where(predicate);
        }



        #endregion

        #region Contract Funcs
        static int ContractIdCreator = 1000;

        public void AddContract(Contract inputContract)
        {

            Child tempChild = DalTools.GetChild(inputContract.ChildID);
            if (tempChild == null)
                throw new Exception("Child in the contract not found...");

            Nanny tempNanny = DalTools.GetNanny(inputContract.NannyID);
            if (tempNanny == null)
                throw new Exception("Nanny in the contract not found...");

            inputContract.ContID = ContractIdCreator;
            ContractIdCreator++;

            DataSource.ContractList.Add(inputContract);
        }
        public bool DeleteContract(int inputContractId)
        {
            Contract tempContract = DalTools.GetContract(inputContractId);
            if (tempContract == null)
                throw new Exception("Contract with the same id not found...");

            return DataSource.ContractList.Remove(tempContract);
        }
        public void UpdateContractDetails(Contract inputContract)
        {
            int index = DataSource.ContractList.FindIndex(c => c.ContID == inputContract.ContID);
            if (index == -1)
                throw new Exception("Contract with the same id not found...");
            DataSource.ContractList[index] = inputContract;
        }
        public IEnumerable<Contract> GetAllContracts(Func<Contract, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.ContractList.AsEnumerable();

            return DataSource.ContractList.Where(predicate);
        }



        #endregion
    }

}
