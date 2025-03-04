using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Mother
    {
        //Propertys
        public int MomID { set; get; }
        public string MomFamilyName { get; set; }
        public string MomFirstName { get; set; }
        public int MomPhoneNum { get; set; }
        public string MomAdress { get; set; }
        public string MomSearchAdress { get; set; }
        public bool[] MomDaysNannyNeeds { get; set; } // צריך לממש הדפסה בטו סטרינג 
        public List<WeeklyWorkSchedule> MomHoursNannyNeeds { get; set; }
        public string MomComment { get; set; }

        //Mother Constructor
        public Mother()
        {
            MomHoursNannyNeeds = new List<WeeklyWorkSchedule>(6);
            MomDaysNannyNeeds = new bool[6];
        }

        public Mother GetCopy() // ido
        {
            Mother DuplicatedMother = new Mother();
            DuplicatedMother = (Mother)this.MemberwiseClone();

            DuplicatedMother.MomDaysNannyNeeds = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                DuplicatedMother.MomDaysNannyNeeds[i] = this.MomDaysNannyNeeds[i];
            }

            DuplicatedMother.MomHoursNannyNeeds = new List<WeeklyWorkSchedule>(6);
            for (int i = 0; i < 6; i++)
            {
                DuplicatedMother.MomHoursNannyNeeds.Insert(i, new WeeklyWorkSchedule());
                DuplicatedMother.MomHoursNannyNeeds[i].StartTime = new TimeSpan(this.MomHoursNannyNeeds[i].StartTime.Hours, this.MomHoursNannyNeeds[i].StartTime.Minutes,0);
                DuplicatedMother.MomHoursNannyNeeds[i].EndTime = new TimeSpan(this.MomHoursNannyNeeds[i].EndTime.Hours, this.MomHoursNannyNeeds[i].EndTime.Minutes, 0);
            }

            return DuplicatedMother;
        }

        public override string ToString() 
        {
            string strForReturn;
            strForReturn = "Mom ID: ".PadRight(35) + MomID + "\n"
                         + "Family name: ".PadRight(35) + MomFamilyName + "\n"
                         + "First name: ".PadRight(35) + MomFirstName + "\n"
                         + "Phone number: ".PadRight(35) + "0" + MomPhoneNum + "\n" 
                         + "Adress: ".PadRight(35) + MomAdress + "\n"
                         + "The area of the requested nanny:".PadRight(35) + MomSearchAdress + "\n";

            strForReturn += "Days and hours of work: \n";
            for (int i = 0; i < 6; i++)
            {
                if (MomDaysNannyNeeds[i])
                {
                    strForReturn += "".PadRight(35) + IntToDay(i) + " from " + MomHoursNannyNeeds[i].StartTime.Hours + ":" + MomHoursNannyNeeds[i].StartTime.Minutes
                        + " to " + MomHoursNannyNeeds[i].EndTime.Hours + ":" + MomHoursNannyNeeds[i].EndTime.Minutes + " o'clock\n";
                }
            }

            strForReturn += "Comments: ".PadRight(35) + MomComment + "\n";

            return strForReturn;
        }

        private static string IntToDay(int n)
        {
            switch (n)
            {
                case 0:
                    return "Sunday";
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";

                default:
                    return "null";
            }
        } 











    }
}
