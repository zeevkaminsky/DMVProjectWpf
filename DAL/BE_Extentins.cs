using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    internal static class BE_Extentions
    {
        /// <summary>
        /// doesn't work with nested classes as proofedint ToolsTests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static T Clone<T>(this T t) where T : class, new()
        //{
        //    T result = new T();
        //    if (t.GetType().Name != "BE.Person")
        //    {

        //        result = new T();
        //        foreach (PropertyInfo item in t.GetType().GetProperties())
        //        {
        //            item.SetValue(result, item.GetValue(t, null));
        //        }
        //    }
        //    return result;
        //}

        public static FullName Clone(this FullName n)
        {
            return new FullName
            {
                FirstName = n.FirstName,
                LastName = n.LastName
            };
        }

        public static XElement ToXML(this FullName n)
        {
            return new XElement("FullName",
                new XElement("FirstName", n.FirstName),
                new XElement("LastName", n.LastName)
            );
        }

        public static FullName ToName(this XElement n)
        {
            return new FullName
            {
                FirstName = n.Element("FirstName").Value,
                LastName = n.Element("LastName").Value
            };
        }

        public static Address Clone(this Address a)
        {
            return new Address
            {
                Town = a.Town,
                Building = a.Building,
                Street = a.Street
            };
        }

        public static XElement ToXML(this Address a)
        {
            return new XElement("Address",
                new XElement("Town", a.Town),
                new XElement("Building", a.Building.ToString()),
                new XElement("Street", a.Street)
                );
        }

        public static Address ToAddress(this XElement a)
        {
            return new Address
            {
                Town = BE.Configuration.ToEnum<Cities>(a.Element("Town").Value),
                Building = Int32.Parse(a.Element("Building").Value),
                Street = a.Element("Street").Value
            };
        }

        public static Test Clone(this Test d)  
        {
            return new Test
            {
                TesterID = d.TesterID,
                TraineeID = d.TraineeID,
                TestDay = d.TestDay,
                
                Criteria = d.Criteria, //need to fix
                ExitPoint = d.ExitPoint.Clone(),
                TestResult = d.TestResult,
                TestHour = d.TestHour
            };
        }

        public static XElement ToXML(this Test d)
        {
            return new XElement("test",
                                 new XElement("TesterID", d.TesterID.ToString()),
                                 new XElement("TraineeID", d.TraineeID.ToString()),
                                 new XElement("TestDay", d.TestDay.ToString()),
                                 //new XElement("Criteria",
                                                    //(from r in d.Criteria
                                                     //select new XElement("Criterion", r)).ToList()),//need to fix
                                 new XElement(d.ExitPoint.ToXML()),
                                 new XElement("TestResult", d.TestResult.ToString()),
                                 new XElement("TestHour", d.TestHour.ToString())
                                );
        }
        public static Test toTest(this XElement d)
        {
            return new Test
            {
                TesterID = d.Element("TesterID").Value,
               // Comment = d.Element("Comment").Value,
                TraineeID = d.Element("TraineeID").Value,
                TestDay = DateTime.Parse(d.Element("TestDay").Value),
                //Criteria = (from s in d.Element("Criteria").Elements("Criterion")
                                //select s.Value).ToList(),
                TestResult = Boolean.Parse(d.Element("TestResult").Value),
                ExitPoint = d.Element("ExitPoint").ToAddress(),
                TestHour = TimeSpan.Parse(d.Element("TestHour").Value)
            };
        }

        
        public static Schedule Clone(this Schedule s)
        {
            Schedule result = new Schedule();
            for (int i = 0; i < s.weeklySchedule.Length; i++)
            {
                if (result.weeklySchedule[i].Count() > 0)
                {
                    result.weeklySchedule[i] = s.weeklySchedule[i].ToArray();
                }
            }
            return result;
        }

               


       

        public static Tester Clone(this Tester t)
        {
            Tester result = null;
            result = new Tester
            {
                Address = t.Address.Clone(),
                DateOfBirth = t.DateOfBirth,
                MyVehicle = t.MyVehicle,
                Gender = t.Gender,
                ID = t.ID,
                MaxDistance = t.MaxDistance,
                Name = t.Name,
                Experience = t.Experience,
                MaxTests = t.MaxTests,
                WeeklySchedule = t.WeeklySchedule.Clone()
            };
            return result;
        }



        public static Trainee Clone(this Trainee t)
        {
            Trainee result = new Trainee
            {
                Address = t.Address.Clone(),
                DateOfBirth = t.DateOfBirth,
                Gender = t.Gender,
                ID = t.ID,
                Name = t.Name,
                MyVehicle = t.MyVehicle,
                School = t.School,
                MyGear = t.MyGear,
                TeacherName = t.TeacherName,
                NumOfLessons = t.NumOfLessons 
            };
            return result;
        }

        public static void SaveToXml<T>(this T source, string fullfilename)
        {
            using (FileStream file = new FileStream(fullfilename, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
                xmlSerializer.Serialize(file, source);
                file.Close();
            }
        }
        public static string ToXMLstring<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        public static T ToObject<T>(this string toDeserialize)
        {
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
