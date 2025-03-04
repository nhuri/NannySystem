using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Child
    {
        public int MomID { set; get; }
        public int ChildID { set; get; }
        public int AgeInMonths { set; get; }
        public string ChildName { set; get; }
        public string TypesOfSpecialNeeds { set; get; }
        public bool IsSpecialNeeds { set; get; }


        public override String ToString()
        {
            string strForReturn;
            strForReturn = "Mom ID: " + MomID + "\n"
                         + "Child ID: " + ChildID + "\n"
                         + "Child name: " +ChildName + "\n"
                         + "Age:" + AgeInMonths + "months" + "\n";

            if (IsSpecialNeeds)
                strForReturn += "Special needs:" + TypesOfSpecialNeeds + "\n";
            else
                strForReturn += "Special needs: No";

            return strForReturn;
        }
    }
}
