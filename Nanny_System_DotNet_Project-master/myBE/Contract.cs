using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Contract
    {
        public int ContID { set; get; }
        public int ChildID { set; get; }
        public int NannyID { set; get; }
        public bool IsIntroductoryMeeting { set; get; }
        public bool IsContractSigned { set; get; }
        public double HourlySalary { set; get; }
        public double MonthlySalary { set; get; }
        public Payment_method PaymentMethod { set; get; }
        public string SpeicalDetailsOfMeeting { get; set; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }

        public Contract GetCopy()
        {
            Contract DuplicatedContract = new Contract();
            DuplicatedContract = (Contract)this.MemberwiseClone();
            return DuplicatedContract;
        }

        public override String ToString() 
        {
            string strForReturn;
            strForReturn = "Contract ID: ".PadRight(50) + ContID + "\n"
                          + "Child ID: ".PadRight(50) + ChildID + "\n"
                          + "Nanny ID: ".PadRight(50) + NannyID + "\n";

            strForReturn += "Was there an introductory meeting: ".PadRight(50);
            if (IsIntroductoryMeeting)
                strForReturn += "YES \n";
            else
                strForReturn += "NO \n";

            if (IsIntroductoryMeeting)
            {
                strForReturn += "Has a contract of employment been signed: ".PadRight(50);
                if (IsContractSigned)
                {
                    strForReturn += "YES \n";
                    if (PaymentMethod == Payment_method.hourly)
                        strForReturn += "Salary: ".PadRight(50) + HourlySalary + " for month  \n";
                    else
                        strForReturn += "Salary: ".PadRight(50) + MonthlySalary + " for month \n";


                    strForReturn += "Transaction start date: ".PadRight(50)
                                 + StartDate.Day + "/" + StartDate.Month + "/" + StartDate.Year + "\n"
                                 + "Transaction end date: ".PadRight(50)
                                 + EndDate.Day + "/" + EndDate.Month + "/" + EndDate.Year + "\n";
                }
                else
                    strForReturn += "NO \n";
            }

            return strForReturn;
        }
    }
}
