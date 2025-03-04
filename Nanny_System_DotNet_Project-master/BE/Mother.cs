using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Mother
    {
        public int MomID { set; get; }
        public string MomFamilyName { get; set; }
        public string MomFirstName { get; set; }
        public int MomPhoneNum { get; set; }
        public string MomAdress { get; set; }
        public string MomSearchArea { get; set; }
        public bool[] MomDaysNannyNeeds { get; set; } // צריך לממש הדפסה בטו סטרינג 
        public string[][] MomHoursNannyNeeds { get; set; }
        public string MomComment { get; set; }
        public override string ToString()
        {
            string strForReturn;
            strForReturn = "Mom ID: " + MomID + "\n"
                         + "Family name: " + MomFamilyName + "\n"
                         + "First name: " + MomFirstName + "\n"
                         + "Phone number: " + MomPhoneNum + "\n"
                         + "Adress: " + MomAdress + "\n"
                         + "The area of the requested nanny: " + MomSearchArea + "\n"
                         + "Comments: " + MomComment + "\n";

            return strForReturn;
        }










    }
}
