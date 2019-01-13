using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {
        
        public string Street { get; set; }
        public int Building { get; set; }
        public Cities Town { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", Street, Building, Town);
        }
    }
}
