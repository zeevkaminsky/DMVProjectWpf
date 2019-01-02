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
        public Vehicle MyVehicle = new Vehicle();
        public Schedule WeeklySchedule { get; set; }
        public double MaxDistance { get; set; }

        

        public override string ToString()
        {
            return base.ToString() + string.Format("years of experience:{0}\n maximum tests for a week:{1}\n type of vehicle:{2}\n",
               Experience, MaxTests, MyVehicle);
        }

        
    }
}
