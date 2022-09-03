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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}");
            }
            Console.WriteLine();    
        }

        public static void DisplayPlaced()
        {
            string sqlCommand = "select s.roll_number, name, package, company_name from Student s right join Placed p on s.roll_number = p.roll_number left join Companies c on c.company_id=p.company_id;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No one placed yet\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name, Current Package, Company Name");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}, {reader[2]}, {reader[3]}");
            }
            Console.WriteLine();
        }

        public static void DisplayUnPlaced()
        {
            string sqlCommand = "select s.roll_number, name from Student s left join Placed p on s.roll_number = p.roll_number  where company_id is NULL;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
