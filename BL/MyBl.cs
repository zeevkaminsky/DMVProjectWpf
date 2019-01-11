using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class MyBl : IBl
    {

        #region add
        public bool AddDrivingTest(Test test)
        {
            Trainee helpTrainee = GetTrainees().FirstOrDefault(t => t.ID == test.TraineeID);
            Tester helpTester = GetTesters().FirstOrDefault(t => t.ID == test.TesterID);
            Test helpTest = GetTests().FirstOrDefault(t => t == test);
            //check trainee if exist
            if (helpTrainee == null)
            {
                throw new Exception("ERROR: Trainee wasn't found in the system\n");
            }

            //check tester if exist
            if (helpTester == null)
            {
                throw new Exception("ERROR: tester wasn't found in the system\n");
            }

            //check there is enough days between tests
            if (helpTest != null && (DateTime.Now.Day - helpTest.TestTime.Day < Configuration.daysBetweenTests))
            {
                throw new Exception("There must be at least " + Configuration.daysBetweenTests + " days before a trainee can take another test\n");
            }

            //check there is enough lessons
            if (helpTrainee.NumOfLessons < Configuration.minLessons)
            {
                throw new Exception("A trainee cannot take a test if he took less than" + Configuration.minLessons + " lessons\n");
            }
            
            //if tester is full
            if (helpTester.NumOfTests >= helpTester.MaxTests)
            {
                throw new Exception("tester is full");
            }

            //find all tests trainee succedded
            var licence = from t in GetTests()
                          where t.TraineeID == test.TraineeID && t.TestResult == true
                          select t;
            
            //check if trainee already have a licence to this type of car
            foreach (var item in licence)
            {
                if (item.vehicle == test.vehicle)
                {
                    throw new Exception("Trainee already has licence for this type of car");
                }
            }

            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.AddDrivingTest(test);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
       }

        public bool AddTester(Tester tester)
        {
            if (DateTime.Now.Year - tester.DateOfBirth.Year < Configuration.minTesterAge)
            {
                throw new Exception("A tester cannot be under " + Configuration.minTesterAge + " years old /n");
            }
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
               if( _dal.AddTester(tester))
                {
                    return true;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
           
            return true;
        }

        public bool AddTrainee(Trainee trainee)
        {
            if (DateTime.Now.Year - trainee.DateOfBirth.Year < Configuration.minTraineeAge)
            {
                throw new Exception("A trainee cannot be under " + Configuration.minTraineeAge + " years old \n");
            }
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.AddTrainee(trainee);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }
        #endregion

        #region remove
        public bool RemovedrivingTest(int serialNumber)
        {
            
            try
            {
                IDal _dal = FactorySingletonDal.GetDal();
                _dal.RemovedrivingTest(serialNumber);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
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
                _dal.RemoveTrainee(traineeID);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
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

        public Test FindTestWithSerialNumber(int num)
        {
            return (from t in GetTests()
                    where t.SerialNumber == num
                    select t).FirstOrDefault();
        }

        /// <summary>
        /// return the number of tests trainee did
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        public int NumOfTests (string traineeID)
        {
            int count = 0;
            foreach (Test t in GetTests())
            {
                if (t.TraineeID == traineeID)
                {
                    count++;
                }
            }
            return count;
            
        }

        
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
        public bool IsLisence(string traineeID)
        {
          //get all the tests of the trainee. the last test will be first in the list
            var tests = from t in GetTests()
                        where t.TraineeID == traineeID
                        orderby t.SerialNumber
                        select t;

            //if trainee didn't found
            if (!tests.Any() )
            {
                throw new Exception("this trainee didn't took a test yet.\n");
            }
            

            //check how many criterions trainee succeed
            int count = 0;
            foreach (var c in tests.First().Criteria)
            {
                if (c.Value == true)
                {
                    count++;
                }
            }

            //check if above 80%
            if (count > tests.First().Criteria.Count * 0.8)
            {
                return true;
            }
            return false;
            
        }

        

        public Trainee GetTraineeByName(FullName name)
        {
            return GetTrainees().FirstOrDefault(t => t.Name == name);
        }



        //public List<Tester> availableTesters(DateTime dayAndHour)
        //{
        //    var availableTesters = from tester in GetTesters()
        //                           where tester.WeeklySchedule.weeklySchedule[dayAndHour.Day, dayAndHour.Hour]
        //                           select tester;

        //    var test = from t in GetTests()
        //                  where t.TestTime == dayAndHour
        //                  select t;

        //    var notAvailableTesters = from tester in GetTesters()
        //                  from t in GetTests()
        //                  where t.TesterID == tester.ID
        //                  select tester;

        //    List<Tester> result = availableTesters.ToList();

        //    /*foreach (Tester tester in availableTesters)
        //    {
        //        foreach (var t  in notAvailableTesters)
        //        {
        //            if (tester == t)
        //            {
        //                result.Remove(t);
        //            }
        //        }
        //    }*/

        //    return result;
           
             
        //}





        
    }
}
