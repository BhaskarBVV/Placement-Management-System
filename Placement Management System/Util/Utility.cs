using System;
using MySql.Data.MySqlClient;
using System.Xml;
using Placement_Management_System.Database;
using System.Xml.Linq;
using BetterConsoleTables;

namespace Placement_Management_System.Util
{
    public class Utility
    {
        public static double GetInput(string inputType)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Enter the {inputType} : ");
            Console.ForegroundColor = ConsoleColor.White;
            var userInput = Console.ReadLine();
            double rollNumber;
            try
            {
                rollNumber = Convert.ToDouble(userInput);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n--OOPS! Invlaid Roll Number type, try again---\n");
                return GetInput(inputType);
            }
            return rollNumber;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select form available operations: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Table table = new Table("Enter", "Operation");
            table.AddRow('1', "Display all students");
            table.AddRow('2', "Add new company drive");
            table.AddRow('3', "List all placed students");
            table.AddRow('4', "List all unplaced students");
            table.AddRow('5', "See a student is allowed in which all companies");
            table.AddRow('6', "Add placed student");
            table.AddRow('7', "Delete a placed student");
            table.AddRow('0', "Exit");
            table.Config = TableConfiguration.MySql();
            Console.WriteLine(table.ToString());
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
                Console.Clear();
                ErrorPrinter("OOPS! its an Invlaid input! Try again", "Try from options below");
                return GetChoice();
            }
            if (!(choice >= 0 && choice <= 7))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                ErrorPrinter("OOPS! Its an Invlaid choice!! try again", "Try from options below");
                return GetChoice();
            }
            return choice;
        }

        public static int GetCompanyIndex(int arraySize)
        {
            var input = Console.ReadLine();
            int idx;
            try
            {
                idx = Convert.ToInt32(input);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invlaid Input try again");
                Console.ForegroundColor = ConsoleColor.White;
                return GetCompanyIndex(arraySize);
            }
            while (!(idx >= 0 && idx <arraySize))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invlaid index try again");
                Console.ForegroundColor = ConsoleColor.White;
                return GetCompanyIndex(arraySize);
            }
            return idx;
        }

        public static string GetSqlCommand(string nodeName)
        {
            if(nodeName == "" || nodeName== null)
                return "Invalid Command";
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\codes\\Editors\\C#\\Placement-Management-System\\Placement Management System\\Database\\SqlCommands.xml");
            XmlNode node = doc.DocumentElement!.SelectSingleNode(nodeName)!;
            if (node == null)
                return "No such Sql command found";
            return node.InnerText;
        }

        public static void ErrorPrinter(string errorName, string tryThis)
        {
            Table table = new Table(errorName);
            table.Config = TableConfiguration.MySql();
            table.AddRow(tryThis);
            Console.WriteLine(table.ToString());
            return;
        }
    }
}
