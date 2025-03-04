using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Nanny
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string PrivateName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNum { get; set; }
        public string Adress { get; set; }
        public bool IsElevator { get; set; }
        public int Floor { get; set; }
        public int YearsOfExperience { get; set; }
        public int MaxInfants { get; set; }
        public int MaxInfantAge { get; set; }
        public int MinInfantAge { get; set; }
        public bool IsHourlySalary { get; set; }
        public float HourlySalary { get; set; }
        public float MonthlySalary { get; set; }
        public bool[] WorkingDays { get; set; }
        public string[][] WorkingHours { get; set; }
        public bool IsMOE { get; set; }
        public string Recommendations { get; set; }
        public override string ToString()
        {
            string str = "Nanny details: " + '\n';
            str += "ID: " + Id + '\n' + " Family name: " + FamilyName + " Private name: " + PrivateName ;
            str += "Date of birth: " + DateOfBirth.Date + '\n' + " Phone number: " + PhoneNum + '\n' + " Adress: " + Adress + '\n';
            str += "Nanny has an elavator: " + IsElevator + " Nanny floor: " + Floor + '\n';
            str += "Years of experience: " + YearsOfExperience + '\n' + " maximum infants nanny accept: " + MaxInfants + '\n';
            str += " maximum infants age nanny accept: " + MaxInfantAge + "minimum infants age nanny accept: " + MinInfantAge + '\n';
            str += "Nanny accepts hourly salary: " + IsHourlySalary + '\n';
            if (IsHourlySalary)
            {
                str += "salary per hour: " + HourlySalary + '\n';
            }
            else str += "salary for month: " + MonthlySalary + '\n';
            //סעיפים טז יז
            if (IsMOE)
            {
                str += "nanny days off are calculated by the MOE " + '\n';
            }
            else str += "nanny days off are calculated by the MOIAT: " + '\n';
            str += "details of recommendations: " + Recommendations + '\n';
            return str;
        }
    }
}
