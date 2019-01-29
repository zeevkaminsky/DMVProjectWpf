using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
    {
        public int Experience { get; set; }
        public int NumOfTests { get; set; }
        public int MaxTests { get; set; }
        public Vehicle MyVehicle { get; set; }
        public Schedule WeeklySchedule { get; set; }
        public int MaxDistance { get; set; }

        
        public Tester()
        {
            MyVehicle = new Vehicle();
            WeeklySchedule = new Schedule();
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("years of experience:{0}\n maximum tests for a week:{1}\n type of vehicle:{2}\nmax distance: {3}\n",
               Experience, MaxTests, MyVehicle, MaxDistance);
        }

        public  Tester Clone()
        {
            
            return new Tester
            {
                Address = this.Address.Clone(),
                DateOfBirth = this.DateOfBirth,
                MyVehicle = this.MyVehicle,
                Gender = this.Gender,
                ID = this.ID,
                MaxDistance = this.MaxDistance,
                Name = this.Name,
                Phone = this.Phone,
                Experience = this.Experience,
                MaxTests = this.MaxTests,
                WeeklySchedule = this.WeeklySchedule.Clone()
            };
            
        }
    }
}
