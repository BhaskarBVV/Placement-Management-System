using System;
using BetterConsoleTables;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Placement_Management_System.Database;
using Placement_Management_System.Util;

namespace Placement_Management_System.Company
{
    internal class CompanyDrive
    {
        public static void AddCompnay()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the name of the company : ");
            Console.ForegroundColor = ConsoleColor.White;
            string companyName = Console.ReadLine()!.ToUpper();
            double companyPackage =Utility.GetInput("Package"); ;
            string sqlCommand = Util.Utility.GetSqlCommand("AddCompany");
            sqlCommand=String.Format(sqlCommand, companyName, companyPackage);
            try
            {
                EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCompany already exsits\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            sqlCommand = Util.Utility.GetSqlCommand("GetCompanyId");
            sqlCommand = String.Format(sqlCommand, companyName);
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int companyId = -1;
            while (reader.Read())
            {
                companyId = Convert.ToInt32(reader[0]);
            }

            // creating a list of allowed students
            sqlCommand = Util.Utility.GetSqlCommand("AllowedStudents");
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nList of allowed students:");
            Table table = new Table("Roll Number", "Name");
            table.Config = TableConfiguration.MySql();
            double currentPackage = -1;
            while (reader.Read())
            {
                if (reader[2] != DBNull.Value)
                    currentPackage = Convert.ToDouble(reader[2]);
                if (reader[2] == DBNull.Value || currentPackage + Constants.Const.freezingDifference < companyPackage)
                {
                    table.AddRow(reader[0], reader[3]);
                    sqlCommand = Util.Utility.GetSqlCommand("AddAllowedStudent");
                    sqlCommand = String.Format(sqlCommand, companyId, reader[3], reader[0]);
                    EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void AddSelectedStudent()
        {
            int rollNumber = Convert.ToInt32(Utility.GetInput("Roll Number"));
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if (!doesExist)
                return;
            //finding the current package.
            string sqlCommand = Util.Utility.GetSqlCommand("GetCurrentPackage");
            sqlCommand = String.Format(sqlCommand, rollNumber);
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int currentPackage = -1, curCompanyId = -1;
            if (reader.Read())
            {
                currentPackage = Convert.ToInt32(reader[0]);
                curCompanyId = Convert.ToInt32(reader[1]);
            }

            // getting the list of companies in which student is allowed to sit.
            sqlCommand = Util.Utility.GetSqlCommand("ListAllowedCompanies");
            sqlCommand = String.Format(sqlCommand, rollNumber);
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);

            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not allowed in any till yet..!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSelected in which company : ");
            Table table = new Table("S.No.", "Company Name", "CTC (Lpa)");
            table.Config = TableConfiguration.MySql();
            List<Pair> al = new List<Pair>();
            int idx = 0;
            while (reader.Read())
            {
                table.AddRow(idx, reader[0], reader[1]);
                al.Add(new Pair(Convert.ToString(reader[0]), Convert.ToDouble(reader[1]), Convert.ToInt32(reader[2])));
                idx++;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(table.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the company number : ");
            Console.ForegroundColor = ConsoleColor.White;

            idx = Utility.GetCompanyIndex(al.Count);
            if (curCompanyId == al[idx].id || al[idx].package < currentPackage)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAlert!!!\nNew package cannot be less than the current Package And,\nNew company cannot be same as current Compnay !\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (currentPackage == -1)
            {
                sqlCommand = Util.Utility.GetSqlCommand("AddPlaced");
                sqlCommand = String.Format(sqlCommand, rollNumber, al[idx].id);
                EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            }
            else
            {
                sqlCommand = Util.Utility.GetSqlCommand("UpdatePlaced");
                sqlCommand = String.Format(sqlCommand, al[idx].id, rollNumber);
                EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Successfully placed in {al[idx].name}\n");
            Console.ForegroundColor = ConsoleColor.White;
            al.Clear();
        }

        public static void StudentAllowedCompanies()
        {
            // this function is used to display the list of all the companies in which the student was allowed to sit
            int rollNumber = Convert.ToInt32(Utility.GetInput("Roll Number"));
            Console.WriteLine();
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if (!doesExist)
                return;

            string sqlCommand = Util.Utility.GetSqlCommand("ListAllowedCompanies");
            sqlCommand = String.Format(sqlCommand, rollNumber);
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if (!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not allowed in any till yet..!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Displaying the list of all comapnies");
            Table table = new Table("Company Name", "CTC (Lpa)");
            table.Config = TableConfiguration.MySql();
            while (reader.Read())
            {
                table.AddRow(reader[0], reader[1]);
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(table.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void DeletePlacedStudent()
        {
            int rollNumber = Convert.ToInt32(Utility.GetInput("Roll Number"));
            Console.WriteLine();
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if (!doesExist)
                return;
            
            string sqlCommand = Util.Utility.GetSqlCommand("CheckPlaced");
            sqlCommand = String.Format(sqlCommand, rollNumber);
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if(!reader.HasRows)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("Opps! Selected student is not placed yet !!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            sqlCommand = Util.Utility.GetSqlCommand("DeletePlaced");
            sqlCommand = String.Format(sqlCommand, rollNumber);
            EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student successfully removed\n");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }
    }
}
