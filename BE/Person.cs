using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Person
    {
        public Person()
        {
            Name = new FullName();
            DateOfBirth = new DateTime(1980,1,1);
            Gender = new Gender();
            Address = new Address();
            
        }
        public string  ID { get; set; }
        public FullName Name { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } 
        public string Phone { get; set; }
        public Address Address { get; set; }


        public override string ToString()
        {
            return string.Format(  "ID:{0}\nname:{1}\ndate of birth:{2}\ngender:{3}\naddress:{4}\nphone number:{5}\n"
                , ID, Name.ToString(), DateOfBirth.ToString("dd'/'MM'/'yyyy"), Gender, Address.ToString(), Phone);
        }

    }
}
