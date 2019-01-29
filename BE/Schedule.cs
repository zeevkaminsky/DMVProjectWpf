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

        public Schedule(bool[][] sched)
        {
            this.weeklySchedule = sched;
        }

        public Schedule Clone()
        {
            return new Schedule((bool[][])this.weeklySchedule.Clone());
            
        }
        public override string ToString()
        {
            int starttime = 9;
            bool oved = false;
            string result = null;
            string hayom = null;
            for (int i = 0; i < 5; i++)
            {
                oved = false;
                hayom = null;
                //result += ((Day)i).ToString() + "\n";
                for (int j = 0; j < 6; j++)
                {
                    if (weeklySchedule[i][j] == true)
                    {
                        oved = true;
                        hayom += "\t" + (starttime + j) + ":00-";
                        hayom += (starttime + j + 1).ToString() + ":00\n";
                    }
                }
                if (oved == true)
                {
                    result += ((Days)i).ToString() + "\n";
                    result += hayom;
                }
            }
            if (result != null && result.Length > 1)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
