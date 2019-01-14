using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        public Vehicle Vehicle { get; set; } 
        public Gear Gear { get; set; }
        public int SerialNumber { get; set; }
        public string TesterID { get; set; }
        public string TraineeID { get; set; }
        public DateTime TestDay { get; set; }
        public TimeSpan testHour { get; set; }
        public Address ExitPoint { get; set; }
        public bool? TestResult  { get; set; }
        public Dictionary<string, bool?> Criteria { get; set; } 
           



        public override string ToString()
        {
            return string.Format("serial number:{0}\n tester's ID:{1}\n trainee's ID:{2}\n test time:{3}\n exit point:{4}\n test result:{5}",
          SerialNumber, TesterID, TraineeID, TestDay, ExitPoint, TestResult);
        } 

        
    
    }
}
