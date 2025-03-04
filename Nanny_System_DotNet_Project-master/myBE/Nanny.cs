using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Nanny
    {
        //Propertys
        public int NannyId { get; set; }
        public string NannyFamilyName { get; set; }
        public string NannyPrivateName { get; set; }
        public DateTime NannyDateOfBirth { get; set; }
        public int NannyPhoneNum { get; set; }
        public string NannyAdress { get; set; }
        public bool NannyIsElevator { get; set; }
        public int NannyFloor { get; set; }
        public int NannyYearsOfExperience { get; set; }
        public int NannyMaxInfants { get; set; }
        public int NannyMaxInfantAge { get; set; }
        public int NannyMinInfantAge { get; set; }
        public bool NannyIsHourlySalary { get; set; }
        public double NannyHourlySalary { get; set; }
        public double NannyMonthlySalary { get; set; }
        public bool[] NannyWorkingDays { get; set; }
        public List<WeeklyWorkSchedule> NannyWorkingHours { get; set; }
        public bool NannyIsMOE { get; set; }
        public bool IsNannyHaveRecommendations { get; set; }
        public string NannyRecommendations { get; set; }

        //Nanny Constructor
        public Nanny()
        {
            NannyWorkingDays = new bool[6];
            NannyWorkingHours = new List<WeeklyWorkSchedule>(6);
        }

        public Nanny GetCopy() // ido
        {
            Nanny DuplicatedNanny = new Nanny();
            DuplicatedNanny = (Nanny)this.MemberwiseClone();

            DuplicatedNanny.NannyDateOfBirth = new DateTime(this.NannyDateOfBirth.Year, this.NannyDateOfBirth.Month, this.NannyDateOfBirth.Day);

            DuplicatedNanny.NannyWorkingDays = new bool[6];

            for (int i = 0; i < 6; i++)
            {
                DuplicatedNanny.NannyWorkingDays[i] = this.NannyWorkingDays[i];
            }

            DuplicatedNanny.NannyWorkingHours = new List<WeeklyWorkSchedule>(6);
            for (int i = 0; i < 6; i++)
            {
                DuplicatedNanny.NannyWorkingHours.Insert(i, new WeeklyWorkSchedule());
                DuplicatedNanny.NannyWorkingHours[i].StartTime = new TimeSpan(this.NannyWorkingHours[i].StartTime.Hours, this.NannyWorkingHours[i].StartTime.Minutes, 0);
                DuplicatedNanny.NannyWorkingHours[i].EndTime = new TimeSpan(this.NannyWorkingHours[i].EndTime.Hours, this.NannyWorkingHours[i].EndTime.Minutes, 0);
            }

            return DuplicatedNanny;
        }

        public override string ToString() 
        {
            string str = "ID: ".PadRight(40) + NannyId + '\n'
                       + "First name: ".PadRight(40) + NannyPrivateName + "\n"
                       + "Family name: ".PadRight(40) + NannyFamilyName + "\n"
                       + "Date of birth: ".PadRight(40) + NannyDateOfBirth.Day + "/" + NannyDateOfBirth.Month + "/" + NannyDateOfBirth.Year + '\n'
                       + "Phone number: ".PadRight(40) + "0" + NannyPhoneNum + '\n'
                       + "Adress: ".PadRight(40) + NannyAdress + '\n'
                       + "Nanny floor: ".PadRight(40) + NannyFloor + '\n'
                       + "Nanny has an elavator: ".PadRight(40) + (NannyIsElevator ? "Yes" : "No") + "\n"
                       + "Years of experience: ".PadRight(40) + NannyYearsOfExperience + '\n'
                       + "maximum infants nanny accept: ".PadRight(40) + NannyMaxInfants + '\n'
                       + "maximum infants age nanny accept: ".PadRight(40) + NannyMaxInfantAge + "\n"
                       + "minimum infants age nanny accept: ".PadRight(40) + NannyMinInfantAge + '\n'
                       + "Nanny accepts hourly salary: ".PadRight(40) + NannyIsHourlySalary + '\n'
                       + "Salary for month: ".PadRight(40) + NannyMonthlySalary + '\n';

            if (NannyIsHourlySalary)
                str += "Salary per hour: ".PadRight(40) + NannyHourlySalary + '\n';


            str += "Nanny days off are calculated by the: ".PadRight(40) + (NannyIsMOE ? "MOE" : "MOIAT") + "\n";

            str += "Days and hours of work: \n";
            for (int i = 0; i < 6; i++)
            {
                if (NannyWorkingDays[i])
                {
                    str += "".PadRight(40) + IntToDay(i) + " from " + NannyWorkingHours[i].StartTime.Hours + ":" + NannyWorkingHours[i].StartTime.Minutes
                        + " to " + NannyWorkingHours[i].EndTime.Hours + ":" + NannyWorkingHours[i].EndTime.Minutes + " o'clock\n";
                }
            }


            str += "Recommendations: ".PadRight(40) + NannyRecommendations + '\n';
            return str;
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

    public class NannyWithDis // ido
    {
        public int NannyId { get; set; }
        public string NannyFamilyName { get; set; }
        public string NannyPrivateName { get; set; }
        public int distance2 { get; set; }
    }
}
