using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Child
    {
        public int ChildMomID { set; get; }
        public int ChildID { set; get; }
        public DateTime ChildAge { set; get; }
        public string ChildName { set; get; }
        public string ChildTypesOfSpecialNeeds { set; get; }
        public bool ChildIsSpecialNeeds { set; get; }
        public bool IsHaveNanny { set; get; }//remmber 

        public Child GetCopy()
        {
            Child DuplicatedChild = new Child();
            DuplicatedChild = (Child)this.MemberwiseClone();
            DuplicatedChild.ChildAge = new DateTime(ChildAge.Year, ChildAge.Month, ChildAge.Day);
            return DuplicatedChild;
        }

        public override String ToString()
        {
            string strForReturn;
            strForReturn = "Child ID:".PadRight(25) + ChildID + "\n"
                         + "Child name:".PadRight(25) + ChildName + "\n"
                         + "Date of birth:".PadRight(25) + ChildAge.Day+"/"+ChildAge.Month + "/" +ChildAge.Year + "\n"
                         + "Mom ID:".PadRight(25) + ChildMomID + "\n";


            if (ChildIsSpecialNeeds)
                strForReturn += "Special needs:".PadRight(25) + ChildTypesOfSpecialNeeds + "\n";
            else
                strForReturn += "Special needs:".PadRight(25) + "No";

            return strForReturn;
        }
    }
}
