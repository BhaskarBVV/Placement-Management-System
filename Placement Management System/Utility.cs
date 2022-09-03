using System;
using MySql.Data.MySqlClient;
namespace Placement_Management_System
{
    internal class Utility
    {
        public static int GetRollNumber()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No student with such roll number exists !! Try with a valid roll number\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            return true;
        }
    }
}
