using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    internal class DalXML : IDal
    {


        


        #region add
        public bool AddDrivingTest(Test test)
        {

            try
            {
                test.SerialNumber = getSerialNum();
                raiseSerialNumber();
                string str = test.ToXMLstring();
                XElement xml = XElement.Parse(str);
                DS.DataSourceXML.Tests.Add(xml);
                DS.DataSourceXML.SaveTests();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        private int getSerialNum()
        {
            return int.Parse(DataSourceXML.Config.Element("Test_Serial_Number").Element("value").Value);
        }

        //serialNumber++; xml version
        private void raiseSerialNumber()
        {
            int x = getSerialNum() + 1;
            DataSourceXML.Config.Element("Test_Serial_Number").Element("value").Value = x.ToString();
            DataSourceXML.SaveConfig();
        }

        public bool AddTester(Tester tester)
        {
            try
            {
                //check for duplicate
                var testers = from t in GetTesters()
                          where t.ID == tester.ID
                          select t;
                if (testers.Any())
                {
                throw new Exception("tester with same id already exist");
                }

            
            
                string str = tester.ToXMLstring();
                XElement xml = XElement.Parse(str);
                DS.DataSourceXML.Testers.Add(xml);
                DS.DataSourceXML.SaveTesters();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }


        public bool AddTrainee(Trainee trainee)
        {
            try
            {
                var trainees = (from t in DataSourceXML.Trainees.Elements()
                            where t.Element("ID").Value == trainee.ID
                            select t);
            if (trainees.Any())
            {
                throw new Exception("Trainee with the same ID already exist");
            }
            
                string str = trainee.ToXMLstring();
                XElement xml = XElement.Parse(str);
                DS.DataSourceXML.Trainees.Add(xml);
                DS.DataSourceXML.SaveTrainees();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        #endregion

        #region get
        public List<Tester> GetTesters(Predicate<Tester> predicate = null)
        {


            var serializer = new XmlSerializer(typeof(Tester));

            var elements = DS.DataSourceXML.Testers.Elements("Tester");
            var result = elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader()));
            if (predicate != null)
            {
                result = from t in result
                         where predicate(t)
                         select t.Clone();
            }
            return result.ToList();

        }

        public List<Test> GetTests(Predicate<Test> predicate = null)
        {
            var serializer = new XmlSerializer(typeof(Test));

            var elements = DS.DataSourceXML.Tests.Elements("Test");
            var result = elements.Select(element => (Test)serializer.Deserialize(element.CreateReader()));
            if (predicate != null)
            {
                result = from t in result
                         where predicate(t)
                         select t.Clone();
            }
            return result.ToList();

            // try
            // {


            //    var tests = from test in DataSourceXML.Tests.Elements()
            //                select new Test()
            //                {
            //                    SerialNumber = int.Parse(test.Element("SerialNumber").Value),
            //                    TesterID = test.Element("TesterID").Value,
            //                    TraineeID = test.Element("TraineeID").Value,
            //                    Vehicle = BE.Configuration.ToEnum<Vehicle>(test.Element("Vehicle").Value),

            //                    TestDay = DateTime.Parse(test.Element("TestDay").Value),
            //                    TestHour = TimeSpan.Parse(test.Element("TestHour").Value),
            //                    // ExitPoint = BE_Extentions.ToAddress(test.Element("ExitPoint")),
            //                    TestResult = bool.Parse(test.Element("TestResult").Value),
            //                    Gear = BE.Configuration.ToEnum<Gear>(test.Element("Gear").Value),
            //                    Criteria = test.Element("Criteria").Elements().Select(t => new { key = t.Name.LocalName, val = bool.Parse(t.Value) })
            //                    .ToDictionary(t => t.key, t => t.val)
            //                };

            //    return (from t in tests
            //            where predicate(t)
            //            select t).ToList();
            //}
            //catch (Exception ex)
            //{

            //    throw ex;


            //}




        }

        public List<Trainee> GetTrainees(Predicate<Trainee> predicate = null)
        {
            var serializer = new XmlSerializer(typeof(Trainee));

            var elements = DS.DataSourceXML.Trainees.Elements("Trainee");
            var res = elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader()));

            if (predicate != null)
            {
                res = from t in res
                      where predicate(t)
                      select t.Clone();
            }
            return res.ToList();
            
        }
        #endregion


        #region remove
        public bool RemovedrivingTest(int serialNumber)
        {
            {
                XElement testElement;
                try
                {
                    testElement = (from tes in DS.DataSourceXML.Tests.Elements()
                                      where int.Parse( (tes.Element("SerialNumber").Value)) == serialNumber
                                      select tes).FirstOrDefault();
                    testElement.Remove();
                    DS.DataSourceXML.SaveTests();
                    return true;
                }
                catch (Exception p)
                {
                    throw p;
                }
            }
        }

        public bool RemoveTester(string testerID)
        {
            {
                XElement studentElement;
                try
                {
                    studentElement = (from tes in DS.DataSourceXML.Testers.Elements()
                                      where (tes.Element("ID").Value) == testerID
                                      select tes).FirstOrDefault();
                    studentElement.Remove();
                    DS.DataSourceXML.SaveTesters();
                    return true;
                }
                catch (Exception m)
                {
                    throw m;
                }
            }



        }

        public bool RemoveTrainee(string traineeID)
        {
            {
                XElement traineeElement;
                try
                {
                    traineeElement = (from tra in DS.DataSourceXML.Trainees.Elements()
                                      where (tra.Element("ID").Value) == traineeID
                                      select tra).FirstOrDefault();
                    traineeElement.Remove();
                    DS.DataSourceXML.SaveTrainees();
                    return true;
                }
                catch (Exception me)
                {
                    throw me;
                }
            }
        }
        #endregion


        #region update
        public bool UpdateDrivingTest(Test test)
        {
            try
            {
                XElement oldtestElement = (from tes in DataSourceXML.Tests.Elements()
                                         where int.Parse(tes.Element("SerialNumber").Value) == test.SerialNumber
                                         select tes).FirstOrDefault();
            if (oldtestElement == null)
                throw new Exception("test didnt found");
           
           
                //easy way to update, without to update every filed
                oldtestElement.Remove();
                DataSourceXML.SaveTests();
                AddDrivingTest(test);
                DataSourceXML.SaveTests();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public bool UpdateTester(Tester tester)
        {
            try
            {
                
                XElement oldtesterElement = (from tes in DataSourceXML.Testers.Elements()
                                             where (tes.Element("ID").Value) == tester.ID
                                             select tes).FirstOrDefault();
                if (oldtesterElement == null)
                    throw new Exception("tester didnt found");

                //easy way to update, without to update every filed
                oldtesterElement.Remove();
                DataSourceXML.SaveTesters();
                AddTester(tester);
                DataSourceXML.SaveTesters();
                return true;
            }

            catch (Exception e)
            {

                throw e;
            }
        }

           
       
        public bool UpdateTrainee(Trainee trainee)
        {
            try
            {

                XElement oldTraineeElement = (from t in DataSourceXML.Trainees.Elements()
                                             where t.Element("ID").Value == trainee.ID
                                             select t).FirstOrDefault();

                if (oldTraineeElement == null)
                    throw new Exception("tester didnt found");

                //easy way to update, without to update every filed
                oldTraineeElement.Remove();
                DataSourceXML.SaveTrainees();
                AddTrainee(trainee);
                DataSourceXML.SaveTrainees();
                return true;
            }

            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion
    }
}
