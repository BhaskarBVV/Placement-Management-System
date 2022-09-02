﻿using System;
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
            string sqlCommand = "select s.roll_number, name, current_package from Student s right join Placed p on s.roll_number = p.roll_number;";
            MySqlDataReader reader = EditAndSaveDatabase.ReadAndUpdateDatabase(sqlCommand);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Roll Number, Name, current_package");
            Console.ForegroundColor = ConsoleColor.White;
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}, {reader[1]}, {reader[2]}");
            }
            Console.WriteLine();
        }

        public static void DisplayUnPlaced()
        {
            string sqlCommand = "select s.roll_number, name from Student s left join Placed p on s.roll_number = p.roll_number  where current_package is NULL;";
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
