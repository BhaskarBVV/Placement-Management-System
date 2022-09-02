using System;
using MySql.Data.MySqlClient;


namespace Placement_Management_System
{
    internal class CompanyDrive
    {
        public static void AddCompnay()
        {
            //string companyName;
            //int companyPackage;
        }

        public static void AddSelectedStudent()
        {

        }

        public static void ViewStudentCompanies()
        {
            // this function is used to display the list of all the companies in which the student was allowed to sit
            int rollNumber=Utility.GetRollNumber();
            Console.WriteLine();
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if(!doesExist)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No student with such roll number exists !! Try with a valid roll number\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            string sqlCommand = $"select company_name from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={rollNumber};";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not allowed in any till yet..!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return ;
            }
            Console.WriteLine($"Displaying the list of all comapnies\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Company Name");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}");
            }
            Console.WriteLine();
        }
    }
}
