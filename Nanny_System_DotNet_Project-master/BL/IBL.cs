using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using BE;

namespace BL
{
    public interface IBL
    {
        void AddNanny(Nanny inputNanny);
        bool DeleteNanny(int inputNannyId);
        void UpdateNannyDetails(Nanny inputNanny);

        void AddMom(Mother inputMother);
        bool DeleteMom(int inputMomId);
        void UpdateMomDetails(Mother inputMother);

        void AddChild(Child inputChild);
        bool DeleteChild(int inputChildId);
        void UpdateChildDetails(Child inputChild);

        void AddContract(Contract inputContract);
        bool DeleteContract(int inputContractId);
        void UpdateContractDetails(Contract inputContract);

        IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null);
        IEnumerable<Mother> GetAllMoms(Func<Mother, bool> predicate = null);
        IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null);
        IEnumerable<Contract> GetAllContracts(Func<Contract, bool> predicate = null);
    }
}
