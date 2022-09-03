using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Placement_Management_System
{
    public class GetUserChoice
    {
        public int a;
        public static int GetChoice()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter 1 to display all students");
            Console.WriteLine("Enter 2 to add new company drive");
            Console.WriteLine("Enter 3 to list all placed students");
            Console.WriteLine("Enter 4 to list all unplaced students");
            Console.WriteLine("Enter 5 to see a student is allowed in which all companies");
            Console.WriteLine("Enter 6 to add placed student");
            Console.WriteLine("Enter 0 to exit");
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
                Console.WriteLine("\n---Print Invlaid choice, try again---\n");
                return GetChoice();
            }
            if (!(choice >= 0 && choice <= 6))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n---Print Invlaid choice, try again---\n");
                return GetChoice();
            }
            return choice;   
        }
    }
}
