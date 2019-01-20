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
            
            //check trainee if exist
            if (helpTrainee == null)
            {
                throw new Exception("ERROR: Trainee wasn't found in the system\n");
            }

            ////check tester if exist
            //if (helpTester == null)
            //{
            //    throw new Exception("ERROR: tester wasn't found in the system\n");
            //}

            //check the test as the same type of vheicle that trainee took lessons of
            //if (test.Vehicle != helpTrainee.MyVehicle)
            //{
            //    throw new Exception("trainee can't take a test of this type of vehicle");
            //}

            //check there is enough days between tests
            TimeSpan ts = DateTime.Now -test.TestDay;
            if ( ts.Days < Configuration.daysBetweenTests)
            {
                throw new Exception("There must be at least " + Configuration.daysBetweenTests + " days before a trainee can take another test\n");
            }

            //check there is enough lessons
            if (helpTrainee.NumOfLessons < Configuration.minLessons)
            {
                throw new Exception("A trainee cannot take a test if he took less than" + Configuration.minLessons + " lessons\n");
            }

            //if tester is full
            //if (helpTester.NumOfTests >= helpTester.MaxTests)
            //{
            //    throw new Exception("tester is full");
            //}

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
                _dal.AddDrivingTest(test);
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
       }


        public List<Tester> FindTesterToTest(Test test)
        {
            
           var testers = (from t in TestersAvailableByHour(test.TestDay, test.TestHour)//find all testers available in the hour of the test
                                 where t.MyVehicle == FindTraineeByID(test.TraineeID).MyVehicle && t.MaxTests > t.NumOfTests//get only testers that are match to trainee vehicle andcan take another test this week
                                 select t).ToList();

            return testers;
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
        #endregion
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
        public bool IsLisense(string traineeID)
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


        /// <summary>
        /// returns all testers avaliable in specific hour
        /// </summary>
        /// <param name="testTime"></param>
        /// <returns></returns>
        public List<Tester> TestersAvailableByHour(DateTime testTime, TimeSpan timeSpan)
        {
            return GetTesters(t => t.WeeklySchedule.weeklySchedule[(int)testTime.DayOfWeek][(int)(timeSpan.Hours-9)] == WorkAvailability.work);
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

        public Test FindTestBySerialNumber(int serial_number)
        {
            return GetTests(t => t.SerialNumber == serial_number).FirstOrDefault();
        }

        #region grouping
        public IEnumerable<IGrouping<Vehicle,Tester> >TestersByVehicle()
        {
            return (from t in GetTesters()
                    group t by t.MyVehicle) ;     
        }

        public IEnumerable<IGrouping<string, Trainee>> TrauneesBySchool()
        {
            return (from t in GetTrainees()
                    group t by t.School);
        }

        public IEnumerable<IGrouping<FullName, Trainee>> TraineeByTeacher()
        {
            return (from t in GetTrainees()
                    group t by t.TeacherName);
        }

        public IEnumerable<IGrouping<int, Trainee>> TraineesByNumOfTests()
        {
            return (from t in GetTrainees()
                    group t by t.NumOfTests);
        }

        


        #endregion

    }
}
