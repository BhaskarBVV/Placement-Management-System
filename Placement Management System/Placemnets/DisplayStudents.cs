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
            string sqlCommand = Util.Utility.GetSqlCommand("SelectAll");
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
            string sqlCommand = Util.Utility.GetSqlCommand("Placed");
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
            string sqlCommand = Util.Utility.GetSqlCommand("UnPlaced");
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
