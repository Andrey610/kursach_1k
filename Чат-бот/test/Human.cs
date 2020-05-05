using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    abstract public class Human
    {
        private string fullName;
        public string FullName
        {
            get => fullName;
            set => fullName = value;
        }
    }
}
