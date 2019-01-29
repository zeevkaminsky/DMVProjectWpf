using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BE
{
    public class Address
    {
        public Address()
        {
            Town = new Cities();
        }
        public string Street { get; set; }
        public int Building { get; set; }
        public Cities Town { get; set; }

        //to string match to map request
        public override string ToString()
        {
            return Street + " " + Building + " " + "st." + Town;
            
        }

        public Address Clone()
        {

            return new Address
            {
                Street = this.Street,
                Building = this.Building,
                Town = this.Town
            };
            
        }
    }
}
