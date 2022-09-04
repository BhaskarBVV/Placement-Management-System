using System;
using MySql.Data.MySqlClient;
using Placement_Management_System.Database;

namespace Placement_Management_System.Util
{
    internal class Utility
    {
        public static int GetRollNumber()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the roll number : ");
            Console.ForegroundColor = ConsoleColor.White;
            var user_input = Console.ReadLine();
            int roll_number;
            try
            {
                roll_number = Convert.ToInt32(user_input);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n---Print Invlaid number, try again---\n");
                return GetRollNumber();
            }
            return roll_number;
        }

        public static bool ValidateRollNumber(int roll)
        {
            string sqlCommand = $"Select * from Student where roll_number={roll}";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No student with such roll number exists !! Try with a valid roll number\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }

        public static int GetChoice()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter 1 to display all students");
            Console.WriteLine("Enter 2 to add new company drive");
            Console.WriteLine("Enter 3 to list all placed students");
            Console.WriteLine("Enter 4 to list all unplaced students");
            Console.WriteLine("Enter 5 to see a student is allowed in which all companies");
            Console.WriteLine("Enter 6 to add placed student");
            Console.WriteLine("Enter 0 to exit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEnter your choice : ");
            Console.ForegroundColor = ConsoleColor.White;
            var key = Console.ReadLine();
            Console.WriteLine();
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(key);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n---Print Invlaid choice, try again---\n");
                return GetChoice();
            }
            if (!(choice >= 0 && choice <= 6))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n---Print Invlaid choice, try again---\n");
                return GetChoice();
            }
            return choice;
        }

        public static int GetCompanyIndex(int arraySize)
        {
            int idx = -1;
            try
            {
                idx = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invlaid Input try again");
                Console.ForegroundColor = ConsoleColor.White;
                return GetCompanyIndex(arraySize);
            }
            while (!(idx >= 0 && idx <= arraySize))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invlaid index try again");
                Console.ForegroundColor = ConsoleColor.White;
                return GetCompanyIndex(arraySize);
            }
            return idx;
        }
    }
}
