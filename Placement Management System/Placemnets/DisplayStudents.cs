using System;
using BetterConsoleTables;
using MySql.Data.MySqlClient;
using Placement_Management_System.Company;
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
            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Whooo! All placed\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
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

        public static void FetchParticularCompnayStatus()
        {
            List<Pair> companies = new List<Pair>();
            string sqlCommand = Util.Utility.GetSqlCommand("GetAllCompanies");
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opps! No Company came yet !!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Here is the list of companies that came for hiring");
            Table table = new Table("S.No.","Company Name", "Package");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                companies.Add(new Pair(Convert.ToString(reader[0])!, Convert.ToDouble(reader[1]), Convert.ToInt32(reader[2])-1));
                table.AddRow(Convert.ToInt32(reader[2]) - 1, reader[0], reader[1]);
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Enter the serial number of company, whose record is to be displayed :");
            Console.ForegroundColor = ConsoleColor.White;
            int idx = Util.Utility.GetCompanyIndex(companies.Count());
            sqlCommand = Util.Utility.GetSqlCommand("FetchCompanyRecord");
            sqlCommand = String.Format(sqlCommand,idx+1);
            
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opps! No one placed in this yet !!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            table = new Table("Name of the Placed student");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                table.AddRow(reader[0]);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of students placed in {0}", companies[idx].name);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
