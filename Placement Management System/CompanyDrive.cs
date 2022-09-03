using System;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;


namespace Placement_Management_System
{
    internal class CompanyDrive
    {
        public static void AddCompnay()
        {
            Console.WriteLine("Enter tha name of the company : ");
            string companyName = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter tha package of the company : ");
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
            sqlCommand= $"select company_id from Companies where company_name='{companyName}'";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int companyId = -1;
            while(reader.Read())
            {
                companyId = Convert.ToInt32(reader[0]);
            }

            // creating a list of allowed students
            sqlCommand = "select s.roll_number, c.company_id, package, s.name from Student s left join Placed p on s.roll_number = p.roll_number left join Companies c on p.company_id=c.company_id;";
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nList of allowed students:");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name");
            Console.ForegroundColor = ConsoleColor.White;
            int currentPackage = -1;
            while(reader.Read())
            {
                if (reader[2] != DBNull.Value)
                    currentPackage = Convert.ToInt32(reader[2]);
                if (reader[2]==DBNull.Value ||  currentPackage+4.0<companyPackage)
                {
                    Console.WriteLine($"{reader[0]}, {reader[3]}");
                    sqlCommand = $"insert into AllowedStudents values({companyId},'{reader[3]}',{reader[0]});";
                    EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
                }
            }
            Console.WriteLine();
        }

        public static void AddSelectedStudent()
        {
            int roll_number = Utility.GetRollNumber();
            bool doesExist = Utility.ValidateRollNumber(roll_number);
            if(!doesExist)
                return;
            string sqlCommand = $"select company_name, package from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={roll_number};";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);

            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not allowed in any till yet..!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return ;
            }
            Console.WriteLine("\nSelected in which company : ");
            
            List<pair> al = new List<pair>();

            int idx = 0;
            while(reader.Read())
            {
                al.Add(new pair(Convert.ToString(reader[0]), Convert.ToDouble(reader[1])));
                Console.WriteLine($"{idx} {reader[0]} {reader[1]}");
                idx++;
            }
            idx=Convert.ToInt32(Console.ReadLine());
            sqlCommand = $"select company_id from Companies where company_name='{al[idx].name}'";
            reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            int companyId = -1;
            while(reader.Read())
            {
                companyId = Convert.ToInt32(reader[0]);
            }
            sqlCommand = $"insert into Placed values({roll_number},{companyId})";
            EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            al.Clear();
        }

        public static void StudentAllowedCompanies()
        {
            // this function is used to display the list of all the companies in which the student was allowed to sit
            int rollNumber=Utility.GetRollNumber();
            Console.WriteLine();
            bool doesExist = Utility.ValidateRollNumber(rollNumber);
            if(!doesExist)
                return;
            
            Console.ForegroundColor = ConsoleColor.Green;
            string sqlCommand = $"select company_name, package from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={rollNumber};";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            if(!reader.HasRows)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not allowed in any till yet..!\n");
                Console.ForegroundColor = ConsoleColor.White;
                return ;
            }
            Console.WriteLine($"Displaying the list of all comapnies\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Company Name");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}Lpa");
            }
            Console.WriteLine();
        }
    }
}
