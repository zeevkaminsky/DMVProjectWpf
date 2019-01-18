using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    internal class DalXML : IDal
    {
        public bool AddDrivingTest(Test test)
        {
            DS.DataSourceXML.DrivingTests.Add(test.ToXML());
            DS.DataSourceXML.SaveDrivingtests();
            return true;
        }

        public bool AddTester(Tester tester)
        {
            string str = tester.ToXMLstring();
            XElement xml = XElement.Parse(str);
            DS.DataSourceXML.Testers.Add(xml);
            DS.DataSourceXML.SaveTesters();
            return true;
        }

        public bool AddTrainee(Trainee trainee)
        {
            string str = trainee.ToXMLstring();
            XElement xml = XElement.Parse(str);
            DS.DataSourceXML.Trainees.Add(xml);
            DS.DataSourceXML.SaveTrainees();
            return true;
        }

        public List<Tester> GetTesters(Predicate<Tester> predicate = null)
        {
             
        
            var serializer = new XmlSerializer(typeof(Tester));

            var elements = DS.DataSourceXML.Testers.Elements("Tester");
            return elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader())).ToList();
        
        }

        public List<Test> GetTests(Predicate<Test> predicate = null)
        {
            throw new NotImplementedException();
        }

        public List<Trainee> GetTrainees(Predicate<Trainee> predicate = null)
        {
           
                var result = from t in DS.DataSourceXML.Trainees.Elements()
                             select new Trainee
                             {
                                 ID = t.Element("ID").Value,
                                 Name = new FullName
                                 {
                                     FirstName = t.Element("FullName").Element("FirstName").Value,
                                     LastName = t.Element("FullName").Element("LastName").Value
                                 },
                                 Address = new Address
                                 {
                                     Town = BE.Configuration.ToEnum<Cities>(t.Element("Address").Element("Town").Value),
                                     Building = Int32.Parse(t.Element("Address").Element("Building").Value),
                                     Street = t.Element("Address").Element("Street").Value
                                 },
                                 TeacherName = new FullName
                                 {
                                     FirstName = t.Element("TeacherName").Element("FirstName").Value,
                                     LastName = t.Element("TeacherName").Element("LastName").Value
                                 },
                                 MyVehicle = BE.Configuration.ToEnum<Vehicle> (t.Element("MyVehicle").Value),
                                 DateOfBirth = BE.Configuration.ToEnum<DateTime>(t.Element("DateOfBirth").Value),
                                 School = t.Element("School").Value,
                                 NumOfLessons = Int32.Parse(t.Element("NumOfLessons").Value),
                                 MyGear = BE.Configuration.ToEnum < Gear > (t.Element("GearType").Value),
                                 Gender = BE.Configuration.ToEnum < Gender >( t.Element("Gender").Value)
                             };
                if (predicate != null)
                {
                    return (from tr in result
                            where predicate(tr)
                            select tr).ToList();
                }
                return result.ToList();
            
        }

        public bool RemovedrivingTest(int serialNumber)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTester(string testerID)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTrainee(string traineeID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDrivingTest(Test test)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTester(Tester tester)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }
    }
}
