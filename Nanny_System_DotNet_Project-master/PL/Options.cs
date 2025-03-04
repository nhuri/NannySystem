using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    public static class MotherOptions
    {
        static IBL bl = FactoryBL.GetBL();

        public static void MomMain()
        {
            bool ExitMom = false;
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("\nEnter your choice:");
                    Console.WriteLine("1. Add new mother");
                    Console.WriteLine("2. Delete existing mother");
                    Console.WriteLine("3. Update existing mother");
                    Console.WriteLine("4. Print details of an existing mother");
                    Console.WriteLine("5. Print all mothers");
                    Console.WriteLine("6. Print all mothers by");
                    Console.WriteLine("7. Return to menu");

                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n");

                    switch (choice)
                    {
                        case 1:
                            bl.AddMom(CreateNewMom());
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*           The mother was successfully added          *");
                            Console.WriteLine("********************************************************");
                            break;
                        case 2:
                            Console.Write("Enter the ID of the mother to be removed:    ");
                            bl.DeleteMom(int.Parse(Console.ReadLine()));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The mother was successfully deleted         *");
                            Console.WriteLine("********************************************************");
                            break;
                        case 3:
                            Console.Write("Enter the ID of the mother to be update:    ");
                            bl.UpdateMomDetails(UpdateMother(int.Parse(Console.ReadLine())));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The mother was successfully updated         *");
                            Console.WriteLine("********************************************************");
                            break;
                        case 4:
                            Console.WriteLine("Enter the ID of the mother for whom the details will be printed");
                            Mother tempMother = BlTools.GetMother(int.Parse(Console.ReadLine()));
                            if (tempMother == null)
                                throw new Exception("Mother with the same id not found");
                            Console.WriteLine(tempMother);
                            break;
                        case 5:
                            int MomIndex = 1;
                            foreach (Mother item in bl.GetAllMoms())
                            {
                                Console.WriteLine("{0}. {1}     {2} {3}", MomIndex, item.MomID, item.MomFirstName, item.MomFamilyName);
                                MomIndex++;
                            }
                            break;


                        case 7:
                            ExitMom = true;
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 7");

                    }
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                }

            } while (!ExitMom);





        }

        public static Mother CreateNewMom()
        {
            Mother MotherToAdd = new Mother();
            Console.Write("\nEnter ID:".PadRight(43));
            MotherToAdd.MomID = int.Parse(Console.ReadLine());

            Console.Write("\nEnter first name:".PadRight(43));
            MotherToAdd.MomFirstName = Console.ReadLine();

            Console.Write("\nEnter last name:".PadRight(43));
            MotherToAdd.MomFamilyName = Console.ReadLine();


            Console.Write("\nEnter number phone:".PadRight(43));
            MotherToAdd.MomPhoneNum = int.Parse(Console.ReadLine()); // לעשות בדיקה על הספרות

            Console.Write("\nEnter mother's street:".PadRight(43));
            string adress = Console.ReadLine() + ", ";

            Console.Write("\nEnter mother's city:".PadRight(43));
            adress += Console.ReadLine() + ", Israel";
            MotherToAdd.MomAdress = adress;

            Console.Write("\nEnter the street of the requested nanny:".PadRight(43));
            adress = Console.ReadLine() + ", ";
            Console.Write("\nEnter the city of the requested nanny:".PadRight(43));
            adress += Console.ReadLine() + ", Israel";
            MotherToAdd.MomSearchAdress = adress;

            Console.WriteLine("\nThe days and hours a nanny needs:");
            DaysAndHoursSchedule ScheduleMom = FuncsTools.ScheduleDays(43);
            MotherToAdd.MomDaysNannyNeeds = ScheduleMom.ScheduleDays;
            MotherToAdd.MomHoursNannyNeeds = ScheduleMom.ScheduleHours;

            Console.Write("\nEnter additional comments:".PadRight(43));
            MotherToAdd.MomComment = Console.ReadLine();

            return MotherToAdd;

        }

        public static Mother UpdateMother(int idForUpdate)
        {
            Mother MotherToUpdate = BlTools.GetMother(idForUpdate);
            if (MotherToUpdate == null)
                throw new Exception("Mother with the same id not found");


            Console.WriteLine("Select the field you want to change:");

            Console.WriteLine("1.".PadRight(4) + "First name");
            Console.WriteLine("2.".PadRight(4) + "Last name");
            Console.WriteLine("3.".PadRight(4) + "Phone number");
            Console.WriteLine("4.".PadRight(4) + "Mother's Adress");
            Console.WriteLine("5.".PadRight(4) + "Adress of the requested nanny");
            Console.WriteLine("6.".PadRight(4) + "The Mother's working days");
            Console.WriteLine("7.".PadRight(4) + "Additional comments");

            int choice;
            bool flag;
            do
            {
                choice = int.Parse(Console.ReadLine());
                flag = false;
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nEnter first name:".PadRight(45));
                            MotherToUpdate.MomFirstName = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nEnter last name:".PadRight(45));
                            MotherToUpdate.MomFamilyName = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("\nEnter number phone:".PadRight(58));
                            MotherToUpdate.MomPhoneNum = int.Parse(Console.ReadLine());
                            break;
                        case 4:
                            Console.Write("\nEnter Mother's street:".PadRight(58));
                            string adress = Console.ReadLine() + ", ";
                            Console.Write("\nEnter Mother's city:".PadRight(58));
                            adress += Console.ReadLine() + ", Israel";
                            MotherToUpdate.MomAdress = adress;
                            break;
                        case 5:
                            Console.Write("\nEnter the street of the requested nanny:".PadRight(58));
                            string adressNanny = Console.ReadLine() + ", ";
                            Console.Write("\nEnter the city of the requested nanny::".PadRight(58));
                            adressNanny += Console.ReadLine() + ", Israel";
                            MotherToUpdate.MomAdress = adressNanny;
                            break;

                        case 6:
                            Console.Write("\nThe days and hours a nanny needs:".PadRight(58));
                            DaysAndHoursSchedule tempSchedule = FuncsTools.ScheduleDays(58);
                            MotherToUpdate.MomDaysNannyNeeds = tempSchedule.ScheduleDays;
                            MotherToUpdate.MomHoursNannyNeeds = tempSchedule.ScheduleHours;
                            break;
                        case 7:
                            Console.Write("\nEnter additional comments:".PadRight(58));
                            MotherToUpdate.MomComment = Console.ReadLine();
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 7");
                    }

                }
                catch (Exception c)
                {

                    Console.WriteLine(c.Message);
                    flag = true;
                }
            } while (flag);

            return MotherToUpdate;

        }

        private static void ScheduleDays(Mother inputMom)
        {
            bool flag;
            string YorN;
            Console.Write("\n\nDo you need a nanny for a day (Enter Y for yes or N for no only)");
            for (int i = 0; i < 6; i++)
            {
                inputMom.MomHoursNannyNeeds.Add(null);
                Console.Write("\n{0}?".PadRight(12), IntToDay(i));
                do
                {
                    try
                    {
                        flag = false;
                        YorN = Console.ReadLine().ToUpper();
                        Console.WriteLine();
                        switch (YorN)
                        {
                            case "Y":
                                inputMom.MomDaysNannyNeeds[i] = true;
                                ScheduleHours(inputMom, i);
                                break;
                            case "N":
                                inputMom.MomDaysNannyNeeds[i] = false;
                                break;

                            default:
                                throw new Exception("Error input, Enter Y for yes or N for no only");
                        }
                    }
                    catch (Exception c)
                    {
                        Console.WriteLine(c.Message);
                        flag = true;
                    }
                } while (flag);
            }
        }

        private static void ScheduleHours(Mother inputMom, int i)
        {
            inputMom.MomHoursNannyNeeds[i] = new WeeklyWorkSchedule();
            bool flag;
            int tempHour, tempMinute;

            do
            {
                flag = false;
                try
                {
                    Console.Write("\n       Enter start hour(Number between 0 and 23)".PadRight(53));
                    tempHour = int.Parse(Console.ReadLine());
                    if (tempHour < 0 || tempHour > 23)
                        throw new Exception("Error input, Enter number between 0 and 23 in start hour");

                    Console.Write("\n       Enter start minute(Number between 0 and 59)".PadRight(53));
                    tempMinute = int.Parse(Console.ReadLine());
                    if (tempMinute < 0 || tempHour > 59)
                        throw new Exception("Error input, Enter number between 0 and 59 in start minute");
                    inputMom.MomHoursNannyNeeds[i].StartTime = new TimeSpan(tempHour, tempMinute, 0);

                    Console.Write("\n       Enter end hour(Number between 0 and 23)".PadRight(53));
                    tempHour = int.Parse(Console.ReadLine());
                    if (tempHour < 0 || tempHour > 23)
                        throw new Exception("Error input, Enter number between 0 and 23 in end hour");

                    Console.Write("\n       Enter end minute(Number between 0 and 59)".PadRight(53));
                    tempMinute = int.Parse(Console.ReadLine());
                    if (tempMinute < 0 || tempHour > 59)
                        throw new Exception("Error input, Enter number between 0 and 59 in end minute");
                    inputMom.MomHoursNannyNeeds[i].EndTime = new TimeSpan(tempHour, tempMinute, 0);

                    if (inputMom.MomHoursNannyNeeds[i].EndTime <= inputMom.MomHoursNannyNeeds[i].StartTime)
                    {
                        throw new Exception("Error input, Start time can not be greater than end time");
                    }
                }
                catch (Exception c)
                {

                    Console.WriteLine(c.Message + "\n");
                    flag = true;
                }

            } while (flag);

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

    public static class ChildOptions
    {
        static IBL bl = FactoryBL.GetBL();

        public static void ChildMain()
        {
            bool ExitChild = false;
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("\nEnter your choice:");
                    Console.WriteLine("1. Add new Child");
                    Console.WriteLine("2. Delete existing Child");
                    Console.WriteLine("3. Update existing Child");
                    Console.WriteLine("4. Print details of an existing Child");
                    Console.WriteLine("5. Print all Childs");
                    Console.WriteLine("6. Print all children with special needs");
                    Console.WriteLine("7. Print all children sorted by age");
                    Console.WriteLine("8. Return to menu");

                    int childIndex = 1;
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n");

                    switch (choice)
                    {

                        case 1:
                            bl.AddChild(CreateNewChild());
                            Console.WriteLine();
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*           The child was successfully added           *");
                            Console.WriteLine("********************************************************");

                            break;
                        case 2:
                            Console.Write("Enter the ID of the child to be removed:    ");
                            bl.DeleteChild(int.Parse(Console.ReadLine()));
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.Write("Enter the ID of the child to be update:    ");
                            bl.UpdateChildDetails(UpdateChild(int.Parse(Console.ReadLine())));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The child was successfully updated          *");
                            Console.WriteLine("********************************************************");
                            break;

                        case 4:
                            Console.Write("Enter the ID of the child for whom the details will be printed: ");
                            Child tempChild = BlTools.GetChild(int.Parse(Console.ReadLine()));
                            if (tempChild == null)
                                throw new Exception("Mother with the same id not found...");
                            Console.WriteLine(tempChild);
                            break;
                        case 5:
                            childIndex = 1;
                            foreach (Child item in bl.GetAllChilds())
                            {
                                Console.WriteLine("{0, -3} {1}     {2}", childIndex, item.ChildID, item.ChildName);
                                childIndex++;
                            }
                            break;

                        case 6:
                            childIndex = 1;
                            foreach (Child item in BlTools.AllchildsHaveSecialNeeds())
                            {
                                Console.WriteLine("{0, -3} {1}     {2}", childIndex, item.ChildID, item.ChildName);
                                childIndex++;
                            }
                            break;

                        case 7:
                            childIndex = 1;
                            foreach (Child item in BlTools.SortChildrenByAge())
                            {
                                Console.WriteLine("{0, -3} {1}     {2}  ({3} months)", childIndex, item.ChildID, item.ChildName, item.AgeInMonths());
                                childIndex++;
                            }
                            break;

                        case 8:
                            ExitChild = true;
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 8");

                    }
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                }

            } while (!ExitChild);




        }

        public static Child CreateNewChild()
        {
            bool ExitCreator = true;
            Child childToAdd = new Child();
            do
            {
                try
                {
                    Console.Write("\nEnter ID:".PadRight(45));
                    childToAdd.ChildID = int.Parse(Console.ReadLine());
                    Console.Write("\nEnter first name:".PadRight(45));
                    childToAdd.ChildName = Console.ReadLine();
                    Console.Write("\nEnter His mother's ID:".PadRight(45));
                    childToAdd.ChildMomID = int.Parse(Console.ReadLine());
                    Console.Write("\nEnter Date of birth:".PadRight(45));
                    childToAdd.ChildAge = FuncsTools.DateInput(45);
                    Console.Write("\nDoes have special needs? (Enter Y/N only):".PadRight(45));
                    childToAdd.ChildIsSpecialNeeds = FuncsTools.YesOrNoToBool();

                    if (childToAdd.ChildIsSpecialNeeds)
                    {
                        Console.Write("\nEnter details of the child's special needs:".PadRight(45));
                        childToAdd.ChildTypesOfSpecialNeeds = Console.ReadLine();
                    }
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                    ExitCreator = false;
                }
            } while (!ExitCreator);


            return childToAdd;

        }

        public static Child UpdateChild(int idForUpdate)
        {
            Child childToUpdate = BlTools.GetChild(idForUpdate);
            if (childToUpdate == null)
                throw new Exception("Child with the same id not found");


            Console.WriteLine("Select the field you want to change:");

            Console.WriteLine("1.".PadRight(4) + "First name");
            Console.WriteLine("3.".PadRight(4) + "Date of birth");
            Console.WriteLine("4.".PadRight(4) + "Special needs");

            if (childToUpdate.ChildIsSpecialNeeds)
            {
                Console.WriteLine("5.".PadRight(4) + "details of the child's special needs");
            }

            int choice;
            bool flag;
            do
            {
                flag = false;
                choice = int.Parse(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nEnter first name:".PadRight(45));
                            childToUpdate.ChildName = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nEnter Date of birth:".PadRight(45));
                            childToUpdate.ChildAge = FuncsTools.DateInput(45);
                            break;
                        case 3:
                            Console.Write("\nDoes have special needs? (Enter Y/N only):".PadRight(45));
                            childToUpdate.ChildIsSpecialNeeds = FuncsTools.YesOrNoToBool();


                            if (childToUpdate.ChildIsSpecialNeeds)
                            {
                                Console.Write("\nEnter details of the child's special needs:".PadRight(45));
                                childToUpdate.ChildTypesOfSpecialNeeds = Console.ReadLine();
                            }
                            else
                                childToUpdate.ChildTypesOfSpecialNeeds = "";
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 7");
                    }

                }
                catch (Exception c)
                {

                    Console.WriteLine(c.Message);
                    flag = true;
                }
            } while (flag);

            return childToUpdate;

        }

    }

    public static class NannyOptions
    {
        static IBL bl = FactoryBL.GetBL();

        public static void NannyMain()
        {
            bool exitNanny = false;
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("\nEnter your choice:");
                    Console.WriteLine("1. Add new nanny");
                    Console.WriteLine("2. Delete existing nanny");
                    Console.WriteLine("3. Update existing nanny");
                    Console.WriteLine("4. Print details of an existing nanny");
                    Console.WriteLine("5. Print all nannys");
                    Console.WriteLine("6. Print nannies by treatment age");
                    Console.WriteLine("7. Print nannies by years of experience");
                    Console.WriteLine("8. Return to menu");

                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n");
                    int nannyIndex = 1;
                    switch (choice)
                    {

                        case 1:
                            bl.AddNanny(CreateNewNanny());
                            Console.WriteLine();
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*           The nanny was successfully added           *");
                            Console.WriteLine("********************************************************");
                            break;

                        case 2:
                            Console.Write("Enter the ID of the nanny to be removed:    ");
                            bl.DeleteNanny(int.Parse(Console.ReadLine()));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The nanny was successfully deleted          *");
                            Console.WriteLine("********************************************************");
                            break;
                        case 3:
                            Console.Write("Enter the ID of the nanny to be update:    ");
                            bl.UpdateNannyDetails(UpdateNanny(int.Parse(Console.ReadLine())));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The nanny was successfully updated          *");
                            Console.WriteLine("********************************************************");
                            break;

                        case 4:
                            Console.Write("Enter the ID of the nanny for whom the details will be printed: ");
                            Nanny tempNanny = BlTools.GetNanny(int.Parse(Console.ReadLine()));
                            if (tempNanny == null)
                                throw new Exception("Nanny with the same id not found");
                            Console.WriteLine(tempNanny);
                            break;
                        case 5:
                            nannyIndex = 1;
                            foreach (Nanny item in bl.GetAllNannies())
                            {
                                Console.WriteLine("{0, -3} {1}     {2} {3}", nannyIndex, item.NannyId, item.NannyPrivateName, item.NannyFamilyName);
                                nannyIndex++;
                            }
                            break;
                        case 6:
                            PrintNanniesByAge();
                            break;

                        case 7:
                            foreach (var item in BlTools.SortNanniesByYearOfExperience())
                            {
                                nannyIndex = 1;
                                Console.WriteLine("{0, -3} {1}     {2} {3} ({4} years of experience)", nannyIndex, item.NannyId, item.NannyPrivateName, item.NannyFamilyName, item.NannyYearsOfExperience);
                            }
                            break;
                        case 8:
                            exitNanny = true;
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 8");

                    }
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                }

            } while (!exitNanny);




        }

        public static Nanny CreateNewNanny()
        {
            bool exitCreator = true;
            Nanny nannyToAdd = new Nanny();
            do
            {
                try
                {
                    Console.Write("\nEnter ID:".PadRight(58));
                    nannyToAdd.NannyId = int.Parse(Console.ReadLine());
                    Console.Write("\nEnter first name:".PadRight(58));
                    nannyToAdd.NannyPrivateName = Console.ReadLine();
                    Console.Write("\nEnter last name:".PadRight(58));
                    nannyToAdd.NannyFamilyName = Console.ReadLine();

                    Console.Write("\nEnter Date of birth:".PadRight(58));
                    nannyToAdd.NannyDateOfBirth = FuncsTools.DateInput(58);
                    Console.Write("\nEnter number phone:".PadRight(58));
                    nannyToAdd.NannyPhoneNum = int.Parse(Console.ReadLine());

                    Console.Write("\nEnter nanny's street:".PadRight(58));
                    string adress = Console.ReadLine() + ", ";
                    Console.Write("\nEnter nanny's city:".PadRight(58));
                    adress += Console.ReadLine() + ", Israel";
                    nannyToAdd.NannyAdress = adress;

                    Console.Write("\nIs there an elevator? (Enter Y/N only):".PadRight(58));
                    nannyToAdd.NannyIsElevator = FuncsTools.YesOrNoToBool();

                    Console.Write("\nFloor in building:".PadRight(58));
                    nannyToAdd.NannyFloor = int.Parse(Console.ReadLine());

                    Console.Write("\nYears of experience:".PadRight(58));
                    nannyToAdd.NannyYearsOfExperience = int.Parse(Console.ReadLine());

                    Console.Write("\nMaximum number of children for treatment:".PadRight(58));
                    nannyToAdd.NannyMaxInfants = int.Parse(Console.ReadLine());

                    Console.Write("\nMinimum age of children for treatment(months):".PadRight(58)); //remmber2 בדיקה שמינמלי קטן מהמקסימלי
                    nannyToAdd.NannyMinInfantAge = int.Parse(Console.ReadLine());

                    Console.Write("\nMaximum age of children for treatment(months):".PadRight(58));
                    nannyToAdd.NannyMaxInfantAge = int.Parse(Console.ReadLine());

                    Console.Write("\nMonthly rate:".PadRight(58));
                    nannyToAdd.NannyMonthlySalary = int.Parse(Console.ReadLine());

                    Console.Write("\nIs it possible to pay in hourly rates? (Enter Y/N only):".PadRight(58));
                    nannyToAdd.NannyIsHourlySalary = FuncsTools.YesOrNoToBool();

                    if (nannyToAdd.NannyIsHourlySalary)
                    {
                        Console.Write("\nHourly rate:".PadRight(58));
                        nannyToAdd.NannyHourlySalary = int.Parse(Console.ReadLine());
                    }

                    Console.Write("\nEnter the nanny's working days".PadRight(58));
                    DaysAndHoursSchedule tempSchedule = FuncsTools.ScheduleDays(58);
                    nannyToAdd.NannyWorkingDays = tempSchedule.ScheduleDays;
                    nannyToAdd.NannyWorkingHours = tempSchedule.ScheduleHours;

                    Console.Write("\nThe vacations are according to the Ministry of M.O.E ?".PadRight(58));
                    nannyToAdd.NannyIsMOE = FuncsTools.YesOrNoToBool();

                    Console.Write("\nEnter recommendations on the nanny".PadRight(58));
                    nannyToAdd.NannyRecommendations = Console.ReadLine();



                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                    exitCreator = false;
                }
            } while (!exitCreator);


            return nannyToAdd;

        }

        public static Nanny UpdateNanny(int idForUpdate)
        {
            Nanny nannyToUpdate = BlTools.GetNanny(idForUpdate);
            if (nannyToUpdate == null)
                throw new Exception("Nanny with the same id not found");


            Console.WriteLine("Select the field you want to change:");

            Console.WriteLine("1.".PadRight(4) + "First name");
            Console.WriteLine("2.".PadRight(4) + "Last name");
            Console.WriteLine("3.".PadRight(4) + "Date of birth");
            Console.WriteLine("4.".PadRight(4) + "Phone number");
            Console.WriteLine("5.".PadRight(4) + "nanny's Adress");
            Console.WriteLine("6.".PadRight(4) + "Is there an elevator");
            Console.WriteLine("7.".PadRight(4) + "Floor in building");
            Console.WriteLine("8.".PadRight(4) + "Years of experience:");
            Console.WriteLine("9.".PadRight(4) + "Maximum number of children for treatment");
            Console.WriteLine("10.".PadRight(4) + "Minimum age of children for treatment(months)");
            Console.WriteLine("11.".PadRight(4) + "Maximum age of children for treatment(months)");
            Console.WriteLine("12.".PadRight(4) + "Is it possible to pay in hourly rates");
            Console.WriteLine("13.".PadRight(4) + "Monthly rate");
            Console.WriteLine("14.".PadRight(4) + "Hourly rate");
            Console.WriteLine("15.".PadRight(4) + "The nanny's working days");
            Console.WriteLine("16.".PadRight(4) + "The vacations are according to the Ministry of M.O.E");
            Console.WriteLine("17.".PadRight(4) + "recommendations on the nanny");

            int choice;
            bool flag;
            do
            {
                choice = int.Parse(Console.ReadLine());
                flag = false;
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nEnter first name:".PadRight(45));
                            nannyToUpdate.NannyPrivateName = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nEnter last name:".PadRight(45));
                            nannyToUpdate.NannyFamilyName = Console.ReadLine();
                            break;

                        case 3:
                            Console.Write("\nEnter Date of birth:".PadRight(45));
                            nannyToUpdate.NannyDateOfBirth = FuncsTools.DateInput(45);
                            break;
                        case 4:
                            Console.Write("\nEnter number phone:".PadRight(58));
                            nannyToUpdate.NannyPhoneNum = int.Parse(Console.ReadLine());
                            break;
                        case 5:
                            Console.Write("\nEnter nanny's street:".PadRight(58));
                            string adress = Console.ReadLine() + ", ";
                            Console.Write("\nEnter nanny's city:".PadRight(58));
                            adress += Console.ReadLine() + ", Israel";
                            nannyToUpdate.NannyAdress = adress;
                            break;
                        case 6:
                            Console.Write("\nIs there an elevator? (Enter Y/N only):".PadRight(58));
                            nannyToUpdate.NannyIsElevator = FuncsTools.YesOrNoToBool();
                            break;
                        case 7:
                            Console.Write("\nFloor in building:".PadRight(58));
                            nannyToUpdate.NannyFloor = int.Parse(Console.ReadLine());
                            break;
                        case 8:
                            Console.Write("\nYears of experience:".PadRight(58));
                            nannyToUpdate.NannyYearsOfExperience = int.Parse(Console.ReadLine());
                            break;
                        case 9:
                            Console.Write("\nMaximum number of children for treatment:".PadRight(58));
                            nannyToUpdate.NannyMaxInfants = int.Parse(Console.ReadLine());
                            break;
                        case 10:
                            Console.Write("\nMinimum age of children for treatment(months):".PadRight(58)); //remmber2 בדיקה שמינמלי קטן מהמקסימלי
                            nannyToUpdate.NannyMinInfantAge = int.Parse(Console.ReadLine());
                            break;
                        case 11:
                            Console.Write("\nMaximum age of children for treatment(months):".PadRight(58));
                            nannyToUpdate.NannyMaxInfantAge = int.Parse(Console.ReadLine());
                            break;
                        case 12:
                            Console.Write("\nIs it possible to pay in hourly rates? (Enter Y/N only):".PadRight(58));
                            nannyToUpdate.NannyIsHourlySalary = FuncsTools.YesOrNoToBool();
                            break;

                        case 13:
                            Console.Write("Monthly rate:".PadRight(58));
                            nannyToUpdate.NannyMonthlySalary = int.Parse(Console.ReadLine());
                            break;
                        case 14:
                            if (nannyToUpdate.NannyIsHourlySalary)
                            {
                                Console.Write("\nHourly rate:".PadRight(58));
                                nannyToUpdate.NannyHourlySalary = int.Parse(Console.ReadLine());
                            }
                            else
                                throw new Exception("The nanny does not receive an hourly wage");
                            break;
                        case 15:
                            Console.Write("\nEnter the nanny's working days".PadRight(58));
                            DaysAndHoursSchedule tempSchedule = FuncsTools.ScheduleDays(58);
                            nannyToUpdate.NannyWorkingDays = tempSchedule.ScheduleDays;
                            nannyToUpdate.NannyWorkingHours = tempSchedule.ScheduleHours;
                            break;
                        case 16:
                            Console.Write("\nThe vacations are according to the Ministry of M.O.E ?".PadRight(58));
                            nannyToUpdate.NannyIsMOE = FuncsTools.YesOrNoToBool();
                            break;
                        case 17:
                            Console.Write("\nEnter recommendations on the nanny".PadRight(58));
                            nannyToUpdate.NannyRecommendations = Console.ReadLine();
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 17");
                    }

                }
                catch (Exception c)
                {

                    Console.WriteLine(c.Message);
                    flag = true;
                }
            } while (flag);

            return nannyToUpdate;

        }

        public static void PrintNanniesByAge() // ido
        {
            Console.Write("\nDo you want to group the caregivers according to the maximum or minimum age? (Enter MAX/MIN only)      ");
            bool max = FuncsTools.StringsToBool("MAX", "MIN");

            Console.Write("\nDo you want the nannies to be sorted by name? (Enter Y/N only)     ");
            bool sorted = FuncsTools.YesOrNoToBool();

            foreach (var item1 in BlTools.GroupNanniesByChildMonths(max, sorted))
            {
                Console.WriteLine("All nannies the " + (max ? " maximum" : "minimum") + " age they receive is between " + (item1.Key - 3) + " months and " + item1.Key + " months: ");
                foreach (var item2 in item1)
                {
                    Console.WriteLine("".PadRight(20) + item2.NannyId + "       " + item2.NannyPrivateName + " " + item2.NannyFamilyName);

                }
            }
        }

    }

    public static class ContractOptions
    {
        static IBL bl = FactoryBL.GetBL();

        public static void ContractMain()
        {
            bool exitContrat = false;
            int choice;
            do
            {
                try
                {
                    Console.WriteLine("\nEnter your choice:");
                    Console.WriteLine("1. Add new contract");
                    Console.WriteLine("2. Delete existing contract");
                    Console.WriteLine("3. Update existing contract");
                    Console.WriteLine("4. Print details of an existing contract");
                    Console.WriteLine("5. Print all contract");
                    Console.WriteLine("6. Print all contracts according to the distance between the nanny and the mother's desired address");
                    Console.WriteLine("7. Return to menu");

                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n");

                    switch (choice)
                    {
                        case 1:
                            bl.AddContract(CreateNewContract());
                            Console.WriteLine();
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*          The contract was successfully added         *");
                            Console.WriteLine("********************************************************");
                            break;

                        case 2:
                            Console.Write("Enter the ID of the contract to be removed:    ");
                            bl.DeleteContract(int.Parse(Console.ReadLine()));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*         The contract was successfully deleted        *");
                            Console.WriteLine("********************************************************");
                            break;
                        case 3:
                            Console.Write("Enter the ID of the contract to be update:    ");
                            bl.UpdateContractDetails(UpdateContract(int.Parse(Console.ReadLine())));
                            Console.WriteLine("********************************************************");
                            Console.WriteLine("*        The contract was successfully updated         *");
                            Console.WriteLine("********************************************************");

                            break;
                        case 4:
                            Console.Write("Enter the ID of the contract for whom the details will be printed: ");
                            Contract tempContract = BlTools.GetContract(int.Parse(Console.ReadLine()));
                            if (tempContract == null)
                                throw new Exception("Contract with the same id not found");
                            Console.WriteLine(tempContract);
                            break;
                        case 5:
                            int contractIndex = 1;
                            foreach (Contract item in bl.GetAllContracts())
                            {
                                Console.WriteLine("{0, -3} {1}     Child name: {2,-10} Nanny name: {3} {4}", contractIndex, item.ContID, BlTools.GetChild(item.ChildID).ChildName
                                    , BlTools.GetNanny(item.NannyID).NannyPrivateName, BlTools.GetNanny(item.NannyID).NannyFamilyName);
                                contractIndex++;
                            }
                            break;

                        case 6:
                            PrintContractByDistance();
                            break;


                        case 7:
                            exitContrat = true;
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 7");

                    }
                }
                catch (Exception c)
                {
                    Console.WriteLine(c.Message);
                }

            } while (!exitContrat);




        }

        public static Contract CreateNewContract()
        {

            Contract ContractToAdd = new Contract();
            Console.Write("\nEnter Child ID:".PadRight(55));
            ContractToAdd.ChildID = int.Parse(Console.ReadLine());

            Console.WriteLine("Please select the desired nanny selection method: ");
            Console.WriteLine("1.By distance");
            Console.WriteLine("2.By years of experience");
            Console.WriteLine("3.By Monthly price");
            Console.WriteLine("4.By hourly price");
            int selectionMethod = int.Parse(Console.ReadLine());

            Console.Write("\nPlease insert the maximum distance from the attendant(in meters):   ");
            int limitDistance = int.Parse(Console.ReadLine());


            bool flag;
            IEnumerable<Nanny> v = null;

            do
            {
                try
                {
                    flag = false;
                    switch (selectionMethod)
                    {
                        case 1:
                            v = BlTools.MostAppropriateNanniesByDistance(BlTools.GetChild(ContractToAdd.ChildID), limitDistance);
                            break;
                        case 2:
                            v = BlTools.MostAppropriateNanniesByYearsOfExperience(BlTools.GetChild(ContractToAdd.ChildID), limitDistance);
                            break;
                        case 3:
                            v = BlTools.MostAppropriateNanniesByMonthlyPrice(BlTools.GetChild(ContractToAdd.ChildID), limitDistance);
                            break;
                        case 4:
                            v = BlTools.MostAppropriateNanniesByHourlyPrice(BlTools.GetChild(ContractToAdd.ChildID), limitDistance);
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 4");

                    }
                }
                catch (Exception c)
                {
                    flag = true;
                    Console.WriteLine(c.Message);
                }
            } while (flag);



            if (v.Count() == 0)
            {
                Console.WriteLine("There is no nanny exactly matching requirements, five nannies closest to the requirements are:(sorted from near to far):");
                //v = BlTools.FiveAppropriateNannies(BlTools.GetMother((BlTools.GetChild(ContractToAdd.ChildID)).ChildMomID));
            }
            else
            {
                Console.Write("The list of caregivers is appropriate ");
                switch (selectionMethod)
                {
                    case 1:
                        Console.WriteLine("(sorted from near to far)");
                        break;
                    case 2:
                        Console.WriteLine("(Sorted by years of experience, from many to less)");
                        break;
                    case 3:
                        Console.WriteLine("(Sorted according the cheapest to dear by month)");
                        break;
                    case 4:
                        Console.WriteLine("(Sorted according the cheapest to dear by hour)");
                        break;
                    default:
                        break;
                }
            }

            int counter = 0;



            foreach (var item in v)
            {
                Console.WriteLine(++counter + ".".PadRight(4) + "Name:" + item.NannyPrivateName + " " + item.NannyFamilyName + "".PadRight(25)
                                 + "Adress: " + item.NannyAdress);
            }


            Console.Write("Please select the desired nanny number:      ");

            int choice = int.Parse(Console.ReadLine());
            if (choice < 1 || choice > counter)
                throw new Exception("Error input");
            ContractToAdd.NannyID = (v.ElementAt(choice - 1)).NannyId;


            Console.Write("\nWas there an acquaintance meeting? (Enter Y/N only):".PadRight(55));
            ContractToAdd.IsIntroductoryMeeting = FuncsTools.YesOrNoToBool();

            if (ContractToAdd.IsIntroductoryMeeting)
            {
                Console.Write("\nHas a contract been signed? (Enter Y/N only)".PadRight(55));
                ContractToAdd.IsContractSigned = FuncsTools.YesOrNoToBool();

                if (ContractToAdd.IsContractSigned)
                {
                    if (BlTools.GetNanny(ContractToAdd.NannyID).NannyIsHourlySalary)
                    {
                        Console.Write("\nDo you pay per hour or month ? (Enter H/M only)".PadRight(55));
                        ContractToAdd.PaymentMethod = FuncsTools.HourlyOrMonthly(55);
                    }
                    else
                        ContractToAdd.PaymentMethod = Payment_method.monthly;



                    Console.Write("\nStart date of employing a nanny".PadRight(55));
                    ContractToAdd.StartDate = FuncsTools.DateInput(55);

                    Console.Write("\nEnd date of employing a nanny".PadRight(55));
                    ContractToAdd.EndDate = FuncsTools.DateInput(55);

                }
            }


            return ContractToAdd;

        }

        public static Contract UpdateContract(int idForUpdate)
        {
            Contract ContractToUpdate = BlTools.GetContract(idForUpdate);
            if (ContractToUpdate == null)
                throw new Exception("Contract with the same id not found");


            Console.WriteLine("Select the field you want to change:");

            Console.WriteLine("1.".PadRight(4) + "Was there an acquaintance meeting");
            Console.WriteLine("2.".PadRight(4) + "Has a contract been signed");
            Console.WriteLine("3.".PadRight(4) + "pay per hour or month ");
            Console.WriteLine("4.".PadRight(4) + "Start date of employing a nanny");
            Console.WriteLine("5.".PadRight(4) + "End date of employing a nanny");

            int choice;
            bool flag;
            do
            {
                choice = int.Parse(Console.ReadLine());
                flag = false;
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nWas there an acquaintance meeting? (Enter Y/N only):".PadRight(55));
                            ContractToUpdate.IsIntroductoryMeeting = FuncsTools.YesOrNoToBool();
                            break;
                        case 2:
                            Console.Write("\nHas a contract been signed? (Enter Y/N only)".PadRight(55));
                            ContractToUpdate.IsContractSigned = FuncsTools.YesOrNoToBool();
                            if (ContractToUpdate.IsContractSigned)
                            {
                                if (BlTools.GetNanny(ContractToUpdate.NannyID).NannyIsHourlySalary)
                                {
                                    Console.Write("\nDo you pay per hour or month ? (Enter H/M only)".PadRight(55));
                                    ContractToUpdate.PaymentMethod = FuncsTools.HourlyOrMonthly(55);
                                }
                                else
                                    ContractToUpdate.PaymentMethod = Payment_method.monthly;



                                Console.Write("\nStart date of employing a nanny".PadRight(55));
                                ContractToUpdate.StartDate = FuncsTools.DateInput(55);

                                Console.Write("\nEnd date of employing a nanny".PadRight(55));
                                ContractToUpdate.EndDate = FuncsTools.DateInput(55);

                            }

                            break;
                        case 3:

                            if (BlTools.GetNanny(ContractToUpdate.NannyID).NannyIsHourlySalary == false)
                                throw new Exception("The nanny receives a monthly salary only"); // אולי לשים ב BL
                            Console.Write("\nDo you pay per hour or month ? (Enter H/M only)".PadRight(55));
                            ContractToUpdate.PaymentMethod = FuncsTools.HourlyOrMonthly(55);
                            break;

                        case 4:
                            Console.Write("\nStart date of employing a nanny".PadRight(55));
                            ContractToUpdate.StartDate = FuncsTools.DateInput(55);
                            break;
                        case 5:
                            Console.Write("\nEnd date of employing a nanny".PadRight(55));
                            ContractToUpdate.EndDate = FuncsTools.DateInput(55);
                            break;
                        default:
                            throw new Exception("Error, Your choice does not exist, please enter a number between 1 and 5");
                    }

                }
                catch (Exception c)
                {

                    Console.WriteLine(c.Message);
                    flag = true;
                }
            } while (flag);

            return ContractToUpdate;

        }

        public static void PrintContractByDistance() // ido
        {

            Console.Write("\nEnter the distance in meters by which you want to group the contracts      ");
            int distance = int.Parse(Console.ReadLine());
            Console.Write("\nDo you want the contracts to be sorted by Nanny id? (Enter Y/N only)     ");
            bool sorted = FuncsTools.YesOrNoToBool();

            Console.WriteLine("\nAll contracts that the distance between the caregiver and the requested address of the mother is between: ");
            //foreach (var item1 in BlTools.GroupContractByDistance(distance, sorted))
            //{
            //    Console.WriteLine("\n".PadRight(20) + (item1.Key - distance) + " meters to " + item1.Key + " meters: ");
            //    foreach (var item2 in item1)
            //    {
            //        Console.WriteLine("".PadRight(20) + "Contract ID: " + item2.ContID + "       Nanny ID: " + item2.NannyID + "    Nanny name: " + BlTools.GetNanny(item2.NannyID).NannyPrivateName + " " + BlTools.GetNanny(item2.NannyID).NannyFamilyName);

            //    }
            //}



        }

    }

    public static class FuncsTools
    {
        internal static DateTime DateInput(int margin)
        {
            bool flag = false;
            int tempYear = 0, tempMonth = 0, tempDay = 0;
            do
            {
                try
                {
                    flag = false;
                    Console.Write("\n      Year:".PadRight(margin));
                    tempYear = int.Parse(Console.ReadLine());
                    if (tempYear < 0 || tempYear > 3000)
                        throw new Exception("Error input, Enter correct year");

                    Console.Write("\n      Month:".PadRight(margin));
                    tempMonth = int.Parse(Console.ReadLine());
                    if (tempMonth < 1 || tempMonth > 12)
                        throw new Exception("Error input, Enter correct month");

                    Console.Write("\n      Day:".PadRight(margin));
                    tempDay = int.Parse(Console.ReadLine());
                    if (tempMonth < 1 || tempMonth > 31)
                        throw new Exception("Error input, Enter correct day");
                }
                catch (Exception c)
                {
                    Console.Write("\n" + "".PadRight(margin) + c.Message);
                    flag = true;
                }
            } while (flag);


            return new DateTime(tempYear, tempMonth, tempDay);

        }

        internal static bool YesOrNoToBool()
        {
            bool flag;
            string YorN = null;

            do
            {
                flag = false;
                try
                {
                    YorN = Console.ReadLine().ToUpper();
                    if (YorN != "Y" && YorN != "N")
                        throw new Exception("\nError input, Enter Y for yes or N for no only: ");
                }
                catch (Exception c)
                {
                    Console.Write(c.Message);
                    flag = true;
                }
            } while (flag);



            if (YorN == "Y")
                return true;
            else
                return false;
        } //remmber 4

        internal static bool StringsToBool(string a, string b)
        {
            a.ToUpper();
            b.ToUpper();
            bool flag;
            string Choice = null;

            do
            {
                flag = false;
                try
                {
                    Choice = Console.ReadLine().ToUpper();
                    if (Choice != a && Choice != b)
                        throw new Exception($"\nError input, Enter {a} or {b} only: ");
                }
                catch (Exception c)
                {
                    Console.Write(c.Message);
                    flag = true;
                }
            } while (flag);



            if (Choice == a)
                return true;
            else
                return false;
        } 

        internal static Payment_method HourlyOrMonthly(int margin)
        {
            bool flag;
            string HorM = null;

            do
            {
                flag = false;
                try
                {
                    HorM = Console.ReadLine().ToUpper();
                    if (HorM != "H" && HorM != "M")
                        throw new Exception("Error input, Enter 'H' for hourly or 'M' for monthly only: ");
                }
                catch (Exception c)
                {
                    Console.Write("\n" + "".PadRight(margin) + c.Message);
                    flag = true;
                }
            } while (flag);



            if (HorM == "H")
                return Payment_method.hourly;
            else
                return Payment_method.monthly;
        }

        public static DaysAndHoursSchedule ScheduleDays(int margin)
        {
            bool[] daysArray = new bool[6];
            List<WeeklyWorkSchedule> hoursArray = new List<WeeklyWorkSchedule>(6);

            bool flag = false;
            for (int i = 0; i < 6; i++)
            {
                hoursArray.Add(null);
                Console.Write("\n" + "".PadRight(margin) + IntToDay(i) + "?");

                do
                {
                    try
                    {
                        flag = false;
                        daysArray[i] = YesOrNoToBool();
                        if (daysArray[i])
                            ScheduleHours(hoursArray, i, margin);
                    }
                    catch (Exception c)
                    {

                        Console.WriteLine(c.Message);
                        flag = true;
                    }
                } while (flag);
            }

            return new DaysAndHoursSchedule { ScheduleDays = daysArray, ScheduleHours = hoursArray };
        }

        public static void ScheduleHours(List<WeeklyWorkSchedule> workHourList, int i, int margin)
        {
            workHourList[i] = new WeeklyWorkSchedule();
            bool flag;
            int tempHour, tempMinute;

            do
            {
                flag = false;
                try
                {
                    Console.Write("\n" + "".PadRight(margin) + "Enter start hour(Number between 0 and 23): ");
                    tempHour = int.Parse(Console.ReadLine());
                    if (tempHour < 0 || tempHour > 23)
                        throw new Exception("Error input, Enter number between 0 and 23 in start hour");

                    Console.Write("\n" + "".PadRight(margin) + "Enter start minute(Number between 0 and 59): ");
                    tempMinute = int.Parse(Console.ReadLine());
                    if (tempMinute < 0 || tempHour > 59)
                        throw new Exception("Error input, Enter number between 0 and 59 in start minute");
                    workHourList[i].StartTime = new TimeSpan(tempHour, tempMinute, 0);

                    Console.Write("\n" + "".PadRight(margin) + "Enter end hour(Number between 0 and 23): ");
                    tempHour = int.Parse(Console.ReadLine());
                    if (tempHour < 0 || tempHour > 23)
                        throw new Exception("Error input, Enter number between 0 and 23 in end hour");

                    Console.Write("\n" + "".PadRight(margin) + "Enter end minute(Number between 0 and 59): ");
                    tempMinute = int.Parse(Console.ReadLine());
                    if (tempMinute < 0 || tempHour > 59)
                        throw new Exception("Error input, Enter number between 0 and 59 in end minute");
                    workHourList[i].EndTime = new TimeSpan(tempHour, tempMinute, 0);

                    if (workHourList[i].EndTime <= workHourList[i].StartTime)
                    {
                        throw new Exception("Error input, Start time can not be greater than end time");
                    }
                }
                catch (Exception c)
                {

                    Console.WriteLine("".PadRight(margin) + c.Message + "\n");
                    flag = true;
                }

            } while (flag);

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

    public class DaysAndHoursSchedule
    {
        public bool[] ScheduleDays;
        public List<WeeklyWorkSchedule> ScheduleHours;
    }
}

