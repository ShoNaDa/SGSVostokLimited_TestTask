using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSVostokLimited
{
    public class Employee
    {
        private string name;

        public string Name { get => name; private set => name = value; }

        public Employee(string _name)
        {
            Name = _name;
        }
    }
}
