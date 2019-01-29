using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee:Person
    {
        public Trainee()
        {
            MyVehicle = new Vehicle();
            MyGear = new Gear();
            TeacherName = new FullName();
        }
        public Vehicle MyVehicle { get; set; }
        public Gear MyGear { get; set; }
        public string School { get; set; }
        public FullName TeacherName { get; set; }
        public int NumOfLessons { get; set; }
        public int NumOfTests { get; set; }

        public override string ToString()
        {
            return base.ToString() + string.Format("vehicle:{0}\n gear:{1}\n school:{2}\n teacher name:{3}\n number of lessons:{4}\n",
               MyVehicle, MyGear, School, TeacherName, NumOfLessons);
        }

        public  Trainee Clone()
        {
            
            return new Trainee
            {
                Address = this.Address.Clone(),
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                ID = this.ID,
                Name = this.Name,
                Phone = this.Phone,
                MyVehicle = this.MyVehicle,
                School = this.School,
                TeacherName = new FullName
                {
                    FirstName = this.TeacherName.FirstName,
                    LastName = this.TeacherName.LastName
                },
                NumOfLessons = this.NumOfLessons,
                NumOfTests = this.NumOfTests

            };
            
        }
    }
}
