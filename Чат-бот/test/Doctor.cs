using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Doctor : Human
    {
        private string specialty;
        private string cabinet;
        public string Specialty
        {
            get => specialty;
            set => specialty = value;
        }
        public string Cabinet
        {
            get => cabinet;
            set => cabinet = value;
        }

    }
}
