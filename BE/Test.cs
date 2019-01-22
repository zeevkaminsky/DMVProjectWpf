using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Test
    {
        public Test()
        {
            TestDay = DateTime.Now;
            TestHour = new TimeSpan();
            ExitPoint = new Address();
            Criteria = new Dictionary<string, bool>();
            Vehicle = new Vehicle();
            Gear = new Gear();
           
            foreach (var item in BE.Configuration.requirments)
            {
                Criteria.Add(item, false);
            }
        }
        public Vehicle Vehicle { get; set; } 
        public Gear Gear { get; set; }
        public int SerialNumber { get; set; }
        public string TesterID { get; set; }
        public string TraineeID { get; set; }
        
        public DateTime TestDay { get; set; }
        
        public TimeSpan TestHour { get; set; }
        public Address ExitPoint { get; set; }
        public bool TestResult  { get; set; }
        [XmlIgnore]
        public Dictionary<string, bool> Criteria { get; set; } 
        public string tempDict
        {
            get
            {
                if (Criteria == null)
                    return null;
                string result = "";
                if (Criteria != null)
                {

                    foreach (var item in Criteria)
                    {
                        result += item.Key + "," + item.Value.ToString().ToLower() + ",";
                    }
                }
                result = result.Substring(0, result.Length - 1);
                return result;
            }
            set
            {
                if (value != null && value.Count() > 0)
                {
                    string[] values = value.Split(',');
                    Criteria = new Dictionary<string, bool>();
                    for (int i = 0; i < values.Length - 1; i+=2)
                    {
                        Criteria.Add(values[i], bool.Parse(values[i + 1]));
                    }
                    
                }
            }
        }
           



        public override string ToString()
        {
            return string.Format("serial number:{0}\n tester's ID:{1}\n trainee's ID:{2}\n test time:{3}\n test hour\n{4}exit point:{5}\n test result:{6}",
          SerialNumber, TesterID, TraineeID, TestDay.ToString("dd'/'MM'/'yyyy"), TestHour, ExitPoint, TestResult);
        } 

        
    
    }
}
