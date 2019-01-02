using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Person
    {
        public string ID { get; set; }
        public FullName Name = new FullName();
        public DateTime DateOfBirth = new DateTime();
        public Gender Gender = new Gender();
        public string Phone { get; set; }
        public Address Address = new Address();


        public override string ToString()
        {
            return string.Format("ID:{0}/n name:{1}\n date of birth:{2}\n gender:{3}\n address:{4} phone number:{5}\n"
                , ID, Name, DateOfBirth, Gender, Address, Phone);
        }

    }
}
