using System;
using BetterConsoleTables;
using MySql.Data.MySqlClient;
using Placement_Management_System.Database;

namespace Placement_Management_System.Placemnets
{
    internal class DisplayStudents
    {
        public static void DisplayAll()
        {
            string sqlCommand = "Select * from Student";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Table table = new Table("Roll Number", "Name");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                table.AddRow(reader[0], reader[1]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of all students");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayPlaced()
        {
            string sqlCommand = "select s.roll_number, name, package, company_name from Student s right join Placed p on s.roll_number = p.roll_number left join Companies c on c.company_id=p.company_id order by p.roll_number;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No one placed yet\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Table table = new Table("Roll Number", "Name", "Current Package", "Company Name");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                table.AddRow(reader[0], reader[1], reader[2], reader[3]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of all Placed students");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayUnPlaced()
        {
            string sqlCommand = "select s.roll_number, name from Student s left join Placed p on s.roll_number = p.roll_number  where company_id is NULL order by p.roll_number;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Table table = new Table("Roll Number", "Name");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                table.AddRow(reader[0], reader[1]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of all Unplaced students");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
