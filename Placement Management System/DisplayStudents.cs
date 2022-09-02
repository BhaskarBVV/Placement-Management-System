using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Placement_Management_System
{
    internal class DisplayStudents
    {
        public static void DisplayAll()
        {
            string sqlCommand = "Select * from Student";
            MySqlDataReader reader=  EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.WriteLine("Roll Number, Name");
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}");
            }
        }

        public static void DisplayPlaced()
        {
            
        }

        public static void DisplayUnPlaced()
        {
            string sqlCommand = "select s.roll_number, name from Student s left join Placed p on s.roll_number = p.roll_number  where current_package is NULL;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name");
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int GetRollNumber()
        {
            var user_input = Console.ReadLine();
            int roll_number;
            try
            {
                roll_number = Convert.ToInt32(user_input);
            }
            catch
            {
                Console.WriteLine("\n---Print Invlaid number, try again---\n");
                return GetRollNumber(); 
            }
            return roll_number;
        }
    }
}
