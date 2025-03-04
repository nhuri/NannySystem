using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Contract
    {
        public int ContID { private set; get; }
        public int ChildID { set; get; }
        public int NannyID { set; get; }
        public bool IsIntroductoryMeeting { set; get; }
        public bool IsContractSigned { set; get; }
        public float HourlySalary { set; get; }
        public float MonthlySalary { set; get; }
        public Payment_method PaymentMethod { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }

        public override String ToString()
        {
            string strForReturn;
            strForReturn = "Contract ID: " + ContID + "\n"
                          + "Child ID: " + ChildID + "\n"
                          + "Nanny ID: " + NannyID + "\n"
                          + "Transaction start date: "
                          + StartDate.ToString() + "\n"
                          + "Transaction end date: "
                          + EndDate.ToString() + "\n";

            strForReturn += "Was there an introductory meeting: ";
            if (IsIntroductoryMeeting)
                strForReturn += "YES \n";
            else
                strForReturn += "NO \n";

            strForReturn += "Has a contract of employment been signed: ";
            if (IsContractSigned)
            {
                strForReturn += "YES \n"
                             + "Transaction start date: "
                             + StartDate.Date + "\n"
                             + "Transaction end date: "
                             + EndDate.Date + "\n";
            }
            else
                strForReturn += "NO \n";


            if (PaymentMethod == Payment_method.hourly)
                strForReturn += "Salary: " + HourlySalary + "for hour \n";
            else
                strForReturn += "Salary: " + MonthlySalary + "for month \n";





            return strForReturn;
        }
    }
}
