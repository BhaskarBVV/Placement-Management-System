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
            string companyName = Console.ReadLine().ToUpper();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter tha package offered : ");
            Console.ForegroundColor = ConsoleColor.White;
            double companyPackage = Convert.ToDouble(Console.ReadLine());
            string sqlCommand = $"insert into Companies (company_name, package) values ('{companyName}',{companyPackage})";
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
            sqlCommand = $"select company_id from Companies where company_name='{companyName}'";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int companyId = -1;
            while (reader.Read())
            {
                companyId = Convert.ToInt32(reader[0]);
            }

            // creating a list of allowed students
            sqlCommand = "select s.roll_number, c.company_id, package, s.name from Student s left join Placed p on s.roll_number = p.roll_number left join Companies c on p.company_id=c.company_id;";
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nList of allowed students:");
            Table table = new Table("Roll Number", "Name");
            table.Config = TableConfiguration.MySql();
            int currentPackage = -1;
            while (reader.Read())
            {
                if (reader[2] != DBNull.Value)
                    currentPackage = Convert.ToInt32(reader[2]);
                if (reader[2] == DBNull.Value || currentPackage + 4.0 < companyPackage)
                {
                    table.AddRow(reader[0], reader[3]);
                    sqlCommand = $"insert into AllowedStudents values({companyId},'{reader[3]}',{reader[0]});";
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
            int roll_number = Utility.GetRollNumber();
            bool doesExist = Utility.ValidateRollNumber(roll_number);
            if (!doesExist)
                return;
            //finding the current package.
            string sqlCommand = $"select package,c.company_id from Placed p left join Companies c on p.company_id=c.company_id where p.roll_number = {roll_number};";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int currentPackage = -1, curCompanyId = -1;
            if (reader.Read())
            {
                currentPackage = Convert.ToInt32(reader[0]);
                curCompanyId = Convert.ToInt32(reader[1]);
            }

            // getting the list of companies in which student is allowed to sit.
            sqlCommand = $"select company_name, package, c.company_id from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={roll_number};";
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
            Table table = new Table("S.No.", "Company Name", "Package");
            table.Config = TableConfiguration.MySql();
            List<pair> al = new List<pair>();
            int idx = 0;
            while (reader.Read())
            {
                table.AddRow(idx, reader[0], reader[1]);
                al.Add(new pair(Convert.ToString(reader[0]), Convert.ToDouble(reader[1]), Convert.ToInt32(reader[2])));
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
                sqlCommand = $"insert into Placed values({roll_number},{al[idx].id})";
                EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            }
            else
            {
                sqlCommand = $"update Placed set company_id={al[idx].id} where roll_number={roll_number};";
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
            int rollNumber = Utility.GetRollNumber();
            Console.WriteLine();
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if (!doesExist)
                return;

            string sqlCommand = $"select company_name, package from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={rollNumber};";
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
            Table table = new Table("Company Name", "Package");
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
    }
}
