using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Client : Human
    {
        private string telephone;
        public string Telephone
        {
            get => telephone;
            set => telephone = value;
        }

    }
}
