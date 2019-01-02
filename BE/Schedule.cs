using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Schedule
    {
        public bool?[,] weeklySchedule = new bool?[5,6];

        public Schedule() { }

        public Schedule(bool? [,] newSchedule)
        {
            weeklySchedule = newSchedule;
        }


        
    }
}
