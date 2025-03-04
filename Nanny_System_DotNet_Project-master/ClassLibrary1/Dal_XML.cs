using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.IO;
using DS;
using System.Xml.Serialization;

namespace Dal
{
    class Dal_XML: Idal
    {
        XElement ChildrenXmlRoot;
        XElement MothersXmlRoot;
        XElement NanniesXmlRoot;
        XElement ContractsXmlRoot;

        string ChildrenPath = @"ChildrenXml.xml";
        string MothersPath = @"MothersXml.xml";
        string NanniesPath = @"NannysXml.xml";
        string ContractsPath = @"ContractsXml.xml";

        public Dal_XML()
        {
            try
            {
                if (!File.Exists(ChildrenPath))
                {
                    ChildrenXmlRoot = new XElement("Children");
                    ChildrenXmlRoot.Save(ChildrenPath);
                }
                else
                    ChildrenXmlRoot = XElement.Load(ChildrenPath);

                if (!File.Exists(MothersPath))
                {
                    MothersXmlRoot = new XElement("Mothers");
                    MothersXmlRoot.Save(MothersPath);
                }
                else
                    DS.DataSource.MomsList = LoadFromXML<List<Mother>>(MothersPath);


                if (!File.Exists(NanniesPath))
                {
                    NanniesXmlRoot = new XElement("Nannys");
                    NanniesXmlRoot.Save(NanniesPath);
                }
                else
                    DS.DataSource.NanniesList = LoadFromXML<List<Nanny>>(NanniesPath);

                if (!File.Exists(ContractsPath))
                {
                    ContractsXmlRoot = new XElement("Contracts");
                    ContractsXmlRoot.Save(ContractsPath);
                }
                else
                    DS.DataSource.ContractList = LoadFromXML<List<Contract>>(ContractsPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source); file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }


        #region Child Funcs
            public void AddChild(Child inputChild)
        {

            DalTools.CheckifExistsId(inputChild.ChildID); // ido
            Mother tempMom = DalTools.GetMother(inputChild.ChildMomID);
            if (tempMom == null)
                throw new Exception("The child's mother does not exist in the system, please add the mother before");

            XElement ChildID = new XElement("ChildID", inputChild.ChildID);
            XElement ChildMomID = new XElement("ChildMomID", inputChild.ChildMomID);
            XElement Name = new XElement("Name", inputChild.ChildName);

            XElement YearDate = new XElement("YearDate", inputChild.ChildAge.Year);
            XElement MonthDate = new XElement("MonthDate", inputChild.ChildAge.Month);
            XElement DayDate = new XElement("DayDate", inputChild.ChildAge.Day);
            XElement Age = new XElement("Age", DayDate, MonthDate, YearDate);

            XElement ChildIsSpecialNeeds = new XElement("IsSpecialNeeds", inputChild.ChildIsSpecialNeeds);
            XElement ChildTypesOfSpecialNeeds = new XElement("TypesOfSpecialNeeds", inputChild.ChildTypesOfSpecialNeeds);
            XElement IsHaveNanny = new XElement("IsHaveNanny", inputChild.IsHaveNanny);

            XElement Child = new XElement("Child", ChildID, ChildMomID, Name, Age, ChildIsSpecialNeeds, ChildTypesOfSpecialNeeds, IsHaveNanny);

            ChildrenXmlRoot.Add(Child);
            ChildrenXmlRoot.Save(ChildrenPath);






        }

        public bool DeleteChild(int inputChildId)
        {
            try
            {
                XElement ChildElement = (from item in ChildrenXmlRoot.Elements()
                                         where int.Parse(item.Element("ChildID").Value) == inputChildId
                                         select item).FirstOrDefault();
                ChildElement.Remove();
                ChildrenXmlRoot.Save(ChildrenPath);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null)
        {
            List<Child> ChildList = new List<Child>();

            if (predicate == null)
            {
                ChildList = (from item in ChildrenXmlRoot.Elements()
                             select new Child()
                             {
                                 ChildID = int.Parse(item.Element("ChildID").Value),
                                 ChildMomID = int.Parse(item.Element("ChildMomID").Value),
                                 ChildName = item.Element("Name").Value,
                                 ChildAge = new DateTime(int.Parse(item.Element("Age").Element("YearDate").Value), int.Parse(item.Element("Age").Element("MonthDate").Value), int.Parse(item.Element("Age").Element("DayDate").Value)),
                                 ChildIsSpecialNeeds = bool.Parse(item.Element("IsSpecialNeeds").Value),
                                 ChildTypesOfSpecialNeeds = item.Element("TypesOfSpecialNeeds").Value,
                                 IsHaveNanny = bool.Parse(item.Element("IsHaveNanny").Value)
                             }).ToList();
            }
            else
            {
                ChildList = (from item in ChildrenXmlRoot.Elements()
                             let x = new Child()
                             {
                                 ChildID = int.Parse(item.Element("ChildID").Value),
                                 ChildMomID = int.Parse(item.Element("ChildMomID").Value),
                                 ChildName = item.Element("Name").Value,
                                 ChildAge = new DateTime(int.Parse(item.Element("Age").Element("YearDate").Value), int.Parse(item.Element("Age").Element("MonthDate").Value), int.Parse(item.Element("Age").Element("DayDate").Value)),
                                 ChildIsSpecialNeeds = bool.Parse(item.Element("IsSpecialNeeds").Value),
                                 ChildTypesOfSpecialNeeds = item.Element("TypesOfSpecialNeeds").Value,
                                 IsHaveNanny = bool.Parse(item.Element("IsHaveNanny").Value)
                             }
                             where predicate(x) == true
                             select x).ToList();
            }

            return ChildList.AsEnumerable();

        }

        public void UpdateChildDetails(Child inputChild)
        {
            XElement ChildElement = (from item in ChildrenXmlRoot.Elements()
                                     where int.Parse(item.Element("ChildID").Value) == inputChild.ChildID
                                     select item).FirstOrDefault();
            if (ChildElement == null)
                throw new Exception("Child with the same id not found...");


            ChildElement.Element("Name").Value = inputChild.ChildName;
            ChildElement.Element("Age").Element("YearDate").Value = inputChild.ChildAge.Year.ToString();
            ChildElement.Element("Age").Element("MonthDate").Value = inputChild.ChildAge.Month.ToString();
            ChildElement.Element("Age").Element("DayDate").Value = inputChild.ChildAge.Day.ToString();



            ChildElement.Element("IsSpecialNeeds").Value = inputChild.ChildIsSpecialNeeds.ToString().ToLower();
            ChildElement.Element("TypesOfSpecialNeeds").Value = inputChild.ChildTypesOfSpecialNeeds;
            ChildElement.Element("IsHaveNanny").Value = inputChild.IsHaveNanny.ToString().ToLower();

            ChildrenXmlRoot.Save(ChildrenPath);
        }
        #endregion

        #region Mother Funcs
        public void AddMom(Mother inputMother)
        {
            DalTools.CheckifExistsId(inputMother.MomID); // ido
            DataSource.MomsList.Add(inputMother);
            SaveToXML<List<Mother>>(DataSource.MomsList, MothersPath);
        }
        public bool DeleteMom(int inputMomId)
        {
            Mother tempMother = DalTools.GetMother(inputMomId);
            if (tempMother == null)
                throw new Exception("Mother with the same id not found...");

            bool DeleteIsSuccessful = DataSource.MomsList.Remove(tempMother);
            SaveToXML<List<Mother>>(DataSource.MomsList, MothersPath);
            return DeleteIsSuccessful;


        }
        public void UpdateMomDetails(Mother inputMom)
        {
            int index = DataSource.MomsList.FindIndex(m => m.MomID == inputMom.MomID);
            if (index == -1)
                throw new Exception("Mother with the same id not found...");
            DataSource.MomsList[index] = inputMom;
            SaveToXML<List<Mother>>(DataSource.MomsList, MothersPath);
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
            SaveToXML<List<Nanny>>(DataSource.NanniesList, NanniesPath);
        }

        public bool DeleteNanny(int inputNannyId)
        {
            Nanny tempNanny = DalTools.GetNanny(inputNannyId);
            if (tempNanny == null)
                throw new Exception("Nanny with the same id not found...");


            bool DeleteIsSuccessful = DataSource.NanniesList.Remove(tempNanny);
            SaveToXML<List<Nanny>>(DataSource.NanniesList, NanniesPath);
            return DeleteIsSuccessful;

        }
        public void UpdateNannyDetails(Nanny inputNanny)
        {
            int index = DataSource.NanniesList.FindIndex(n => n.NannyId == inputNanny.NannyId);
            if (index == -1)
                throw new Exception("Nanny with the same id not found...");
            DataSource.NanniesList[index] = inputNanny;
            SaveToXML<List<Nanny>>(DataSource.NanniesList, NanniesPath);

        }
        public IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.NanniesList.AsEnumerable();

            return DataSource.NanniesList.Where(predicate);
        }



        #endregion

        #region Contract Funcs

        public void AddContract(Contract inputContract)
        {
            Child tempChild = DalTools.GetChild(inputContract.ChildID);
            if (tempChild == null)
                throw new Exception("Child in the contract not found...");

            Nanny tempNanny = DalTools.GetNanny(inputContract.NannyID);
            if (tempNanny == null)
                throw new Exception("Nanny in the contract not found...");

            DataSource.ContractList.Add(inputContract);         

            int ContractIdCreator = 0;
            if (DataSource.ContractList.Count == 1)
                ContractIdCreator = 999;
            else
                ContractIdCreator = DataSource.ContractList[DataSource.ContractList.Count - 2].ContID;
            inputContract.ContID = ++ContractIdCreator;
            SaveToXML < List < Contract>>(DataSource.ContractList, ContractsPath);
        }
        public bool DeleteContract(int inputContractId)
        {
            Contract tempContract = DalTools.GetContract(inputContractId);
            if (tempContract == null)
                throw new Exception("Contract with the same id not found...");

            bool DeleteIsSuccessful = DataSource.ContractList.Remove(tempContract);
            SaveToXML<List<Contract>>(DataSource.ContractList, ContractsPath);
            return DeleteIsSuccessful;

        }
        public void UpdateContractDetails(Contract inputContract)
        {
            int index = DataSource.ContractList.FindIndex(c => c.ContID == inputContract.ContID);
            if (index == -1)
                throw new Exception("Contract with the same id not found...");
            DataSource.ContractList[index] = inputContract;
            SaveToXML<List<Contract>>(DataSource.ContractList, ContractsPath);

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
