using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Schedule
    {
        public bool[][] weeklySchedule { get; set; }
        public Schedule()
        {
            weeklySchedule = new bool[5][];
            for (int i = 0; i < 5; i++)
            {
                weeklySchedule[i] = new bool[6];
            }
        }
    }
}
