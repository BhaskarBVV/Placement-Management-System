using System;
using Placement_Management_System.Company;
using Placement_Management_System.Placemnets;
using Placement_Management_System.Util;

namespace Placement_Management_System
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome to Placement Management System\n");
            int remainInLoop = 1;
            while (remainInLoop == 1)
            {
                int choice = Utility.GetChoice();

                switch (choice)
                {
                    case 0:
                        remainInLoop=0;
                        break;
                    case 1:
                        DisplayStudents.DisplayAll();
                        break;
                    case 2:
                        CompanyDrive.AddCompnay();
                        break;
                    case 3:
                        DisplayStudents.DisplayPlaced();
                        break;
                    case 4:
                        DisplayStudents.DisplayUnPlaced();
                        break;
                    case 5:
                        CompanyDrive.StudentAllowedCompanies();
                        break;
                    case 6:
                        CompanyDrive.AddSelectedStudent();
                        break;
                }
            }
        }
    }
}