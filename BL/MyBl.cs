using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BE;
using DAL;

namespace BL
{
    public class MyBl : IBl
    {
        //key for using map quest
        String API_KEY = @"W1ahdi7Gr0ex7rRwwKtx2inAadymOCKD";
        
        /// <summary>
        /// get 2 addresses and return the distance. in case of mistake returns random
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public int getRange(string origin, string destination)
        {
            Random X = new Random();//for error occurreds
            string KEY = API_KEY;
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
             @"?key=" + KEY +
             @"&from=" + origin +
             @"&to=" + destination +
             @"&outFormat=xml" +
             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
             @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                //Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);
                return (int)(distInMiles * 1.609344);
                //display the returned driving time
                //XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                //string fTime = formattedTime[0].ChildNodes[0].InnerText;
                //Console.WriteLine("Driving Time: " + fTime);
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                return X.Next(0, 150);
                //Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error...
            {
                return X.Next(0, 150);
                //Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
            }
        }


        #region add
        public bool AddDrivingTest(Test test)
        {
            Trainee helpTrainee = GetTrainees().FirstOrDefault(t => t.ID == test.TraineeID);
            Tester helpTester = GetTesters().FirstOrDefault(t => t.ID == test.TesterID);
            
            
            //get all trainee tests. the last test will be first;
            var TraineeTests = ((from t in GetTests()
                                 where t.TraineeID == test.TraineeID
                                 select t).OrderByDescending(t => test.TestDay.Year)
                          .ThenByDescending(t => test.TestDay.Month)
                          .ThenByDescending(t => test.TestDay.Day).ToList());

            //check there is enough days between tests
            if (TraineeTests.Any())
            {
                TimeSpan ts = test.TestDay - TraineeTests.First().TestDay;
                if (ts.Days < Configuration.daysBetweenTests)
                {
                    throw new Exception("There must be at least " + Configuration.daysBetweenTests + " days before a trainee can take another test\n");
                }
            }
            //check trainee took enough lessons
            if (helpTrainee.NumOfLessons < Configuration.minLessons)
            {
                throw new Exception("A trainee cannot take a test if he took less than" + Configuration.minLessons + " lessons\n");
            }
            //find all tests trainee succedded
            var licence = from t in GetTests()
                          where t.TraineeID == test.TraineeID && t.TestResult == true
                          select t;
            
            //check if trainee already have a licence to this type of car
            foreach (var item in licence)
            {
                if (item.Vehicle == test.Vehicle)
                {
                    throw new Exception("Trainee already has licence for this type of car");
                }
            }

            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                if(_dal.AddDrivingTest(test))
                {
                    helpTester.NumOfTests++;
                    helpTrainee.NumOfTests++;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            
       }


        public List<Tester> FindTesterToTest(Test test)
        {
            TimeSpan toAdd = new TimeSpan(1, 0, 0);
            TimeSpan tempTS = new TimeSpan(14, 0, 0);


            //check if tester available in this day and hour
            var testersavailability = TestersAvailableByHour(test.TestDay, test.TestHour);
           
            //if tester not availabla, check the next hour in this day
            while (!testersavailability.Any() && test.TestHour < tempTS)
            {
                testersavailability = TestersAvailableByHour(test.TestDay, test.TestHour.Add(toAdd));
                test.TestHour = test.TestHour.Add(toAdd);


            }
            Trainee trainee = FindTraineeByID(test.TraineeID);

            
            
            var testers = (from t in testersavailability//from testers available in the hour of the test
                           where t.MyVehicle == trainee.MyVehicle && t.MaxTests > t.NumOfTests//get only testers that are match to trainee vehicle and can take another test this week
                           select t).ToList();
            var result = (from t in testers
                      where getRange(t.Address.ToString(), test.ExitPoint.ToString()) < t.MaxDistance//get only testers from close distance
                      select t).ToList();
            return result;
        }

        public bool AddTester(Tester tester)
        {
            //if tester is to young
            if (DateTime.Now.Year - tester.DateOfBirth.Year < Configuration.minTesterAge)
            {
                throw new Exception("A tester cannot be under " + Configuration.minTesterAge + " years old /n");
            }
            //if tester is to old
            if (DateTime.Now.Year - tester.DateOfBirth.Year > Configuration.MaxTesterAge)
            {
                throw new Exception("A tester cannot be over " + Configuration.minTesterAge + " years old /n");
            }
            try
            {
               IDal _dal = FactorySingletonDal.GetDal();
               if( _dal.AddTester(tester))
                {
                    return true;
                }
                return false;
            }
             
            catch (Exception e)
            {

                throw e;
            }
           
           
        }

        public bool AddTrainee(Trainee trainee)
        {
            //if trainee is to young
            if (DateTime.Now.Year - trainee.DateOfBirth.Year < Configuration.minTraineeAge)
            {
                throw new Exception("A trainee cannot be under " + Configuration.minTraineeAge + " years old \n");
            }
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                if(_dal.AddTrainee(trainee))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        #endregion

        #region remove
        public bool RemovedrivingTest(int serialNumber)
        {
            
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                if(_dal.RemovedrivingTest(serialNumber))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public bool RemoveTester(string testerID)
        {
           
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.RemoveTester(testerID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public bool RemoveTrainee(string traineeID)
        {
            IDal _dal = FactorySingletonDal.GetDal();
            try
            {
                if(_dal.RemoveTrainee(traineeID))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        #endregion

        #region update
        public bool UpdateDrivingTest(Test test)
        {
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.UpdateDrivingTest(test);
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
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.UpdateTester(tester);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }

        public bool UpdateTrainee(Trainee trainee)
        {
           
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.UpdateTrainee(trainee);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }

        #endregion

        #region get
        public List<Tester> GetTesters(Predicate<Tester> predicate = null)
        {
            IDal _dal = FactorySingletonDal.GetDal();
            return _dal.GetTesters(predicate);
        }

        public List<Test> GetTests(Predicate<Test> predicate = null)
        {
            IDal _dal = FactorySingletonDal.GetDal();
            return _dal.GetTests(predicate);
        }

        public List<Trainee> GetTrainees(Predicate<Trainee> predicate = null)
        {
            IDal _dal = FactorySingletonDal.GetDal();
            return _dal.GetTrainees(predicate);
        }
        #endregion

        #region search functions
        public Tester FindTesterByID(string id)
        {
            return (from t in GetTesters()
                    where t.ID == id
                    select t).FirstOrDefault();
        }

        public Trainee FindTraineeByID(string id)
        {
            return (from t in GetTrainees()
                    where t.ID == id
                    select t).FirstOrDefault();
        }

        public Test FindTestBySerialNumber(int num)
        {
            return (from t in GetTests()
                    where t.SerialNumber == num
                    select t).FirstOrDefault();
        }
        #endregion
        
        /// <summary>
        /// returns a collection of testers order by a distance from a giving address
        /// </summary>
        /// <returns></returns>
        
        public IEnumerable GetTestersByDistance(Address address)
        {
            Random r = new Random();

            var testersByDistance = from t in GetTesters()
                                    let temp = r.Next(500)
                                    select new { t, temp };

          
            return testersByDistance.OrderByDescending(x => x.temp);
        }
        
        /// <summary>
        /// check if trainee past the test.trainee passes a test if 80% precent of requirments
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsLisense(Test test)
        {
          
            //check how many criterions trainee succeed
            int count = 0;
            foreach (var c in test.Criteria)
            {
                if (c.Value == true)
                {
                    count++;
                }
            }

            //check if above 80%
            if (count > test.Criteria.Count * 0.8)
            {
                return true;
            }
            return false;
            
        }
        
        /// <summary>
        /// returns all testers avaliable in specific hour and day
        /// </summary>
        /// <param name="testTime"></param>
        /// <returns></returns>
        public List<Tester> TestersAvailableByHour(DateTime testTime, TimeSpan timeSpan)
        {
            //get all testers working in this hour and day
            List<Tester> testers = (from t in GetTesters(t => t.WeeklySchedule.weeklySchedule[(int)testTime.DayOfWeek][(int)(timeSpan.Hours - 9)] == true)
                                    select t).ToList();
            //get all testers that already in test in this hour and day
            List<String> testersID = (from t in GetTests(t => (t.TestDay == testTime && t.TestHour == timeSpan))
                                      select t.TesterID).ToList();
            //returns testers that working this day and hour, and dosn't have a test
            return (from t in testers
                    where !(testersID.Contains(t.ID))
                    select t).ToList();


        }
        /// <summary>
        /// returns all tests in a specific day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Test> TestsByDay(DateTime date)
        {
            return GetTests (t => t.TestDay.Day == date.Day);
        }
        /// <summary>
        /// returns all tests in specific month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Test> TestsByMonth(DateTime date)
        {
            return GetTests(t => t.TestDay.Month == date.Month);
                   
        }
        
        #region grouping
        public IEnumerable<IGrouping<Vehicle,Tester> >TestersByVehicle()
        {
            return (from t in GetTesters()
                    group t by t.MyVehicle) ;     
        }

        public IEnumerable<IGrouping<Cities, Tester>> TestersByCity()
        {
            return (from t in GetTesters()
                    group t by t.Address.Town);
        }

        public IEnumerable<IGrouping<string, Trainee>> TraineesBySchool()
        {
            return (from t in GetTrainees()
                    group t by t.School);
        }

        public IEnumerable<IGrouping<FullName, Trainee>> TraineeByTeacher()
        {
            return (from t in GetTrainees()
                    group t by t.TeacherName);
        }

        public IEnumerable<IGrouping<Cities, Trainee>> TraineeByCity()
        {
            return (from t in GetTrainees()
                    group t by t.Address.Town);
        }

        public IEnumerable<IGrouping<int, Trainee>> TraineesByNumOfTests()
        {
            return (from t in GetTrainees()
                    group t by t.NumOfTests);
        }

        


        #endregion

    }
}
