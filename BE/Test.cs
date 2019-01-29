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
            {//initialize the dictionary with the requirments ,and set them to false.
                Criteria.Add(item, false);
            }
        }
        public Vehicle Vehicle { get; set; } 
        public Gear Gear { get; set; }
        public int SerialNumber { get; set; }
        public string TesterID { get; set; }
        public string TraineeID { get; set; }
        
        public DateTime TestDay { get; set; }
        [XmlIgnore]
        public TimeSpan TestHour { get; set; }
        //create for the serializer
        public string TempTimeSpan
        {
            get
            {
                return TestHour.ToString();
            }
            set
            {
                TestHour = TimeSpan.Parse(value);
            }
        }
        public Address ExitPoint { get; set; }
        public bool TestResult  { get; set; }
        [XmlIgnore]
        public Dictionary<string, bool> Criteria { get; set; } 
        //create for the serializer
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
                //result = result.Substring(0, result.Length - 1);
                return result;
            }
            set
            {
                if (value != null && value.Any())
                {
                    string[] values = value.Split(',');
                    Criteria = new Dictionary<string, bool>();
                    for (int i = 0; i < values.Length - 1; i+=2)
                    {
                        if (values[i + 1] == "")
                        {
                            Criteria.Add(values[i], false);
                        }
                        else
                        {
                            Criteria.Add(values[i], bool.Parse(values[i + 1]));
                        }
                        
                    }
                    
                }
            }
        }


        public Test Clone()  
        {
            return new Test
            {
                SerialNumber = this.SerialNumber,
                TesterID = this.TesterID,
                TraineeID = this.TraineeID,
                TestDay = this.TestDay,
                ExitPoint = this.ExitPoint.Clone(),
                TestResult = this.TestResult,
                Vehicle = this.Vehicle,
                Gear = this.Gear,
                tempDict = this.tempDict
                
            };
        }

        public override string ToString()
        {
            return string.Format("serial number:{0}\n tester's ID:{1}\ntrainee's ID:{2}\ntest time:{3}\ntest hour{4}\nexit point:{5}\n",
          SerialNumber, TesterID, TraineeID, TestDay.ToString("dd'/'MM'/'yyyy"), TestHour, ExitPoint);
        } 

        
    
    }
}
