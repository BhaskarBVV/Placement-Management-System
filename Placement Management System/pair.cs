using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Placement_Management_System
{
    internal class pair
    {
        public string name;
        public double package;
        public int id;
        public pair(string name, double package, int id)
        {
            this.name = name;
            this.package = package;
            this.id = id;   
        }
    }
}
