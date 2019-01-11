using BE;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class MyDal : IDal
    {
        #region add functions
        public bool AddDrivingTest(Test test)
        {
            Tester tester = GetTesters().FirstOrDefault(t => t.ID == test.TesterID);
            tester.WeeklySchedule.weeklySchedule[(int)test.TestTime.DayOfWeek][(int)test.TestTime.Hour] = "in test";
            Trainee trainee = GetTrainees().FirstOrDefault(t => t.ID == test.TraineeID);
            trainee.NUmOfTests++;

            test.SerialNumber = Configuration.InitialSerialNumber++;
            DataSource.Test.Add(test);
            return true;
        }

        public bool AddTester(Tester tester)
        {
            foreach (Tester t in GetTesters())
            {
                if (tester.ID == t.ID)
                {
                    throw new Exception("tester with the same id allready exists");
                }
            }
            tester.NumOfTests++;
            DataSource.Testers.Add(tester);
            return true;
        }

        public bool AddTrainee(Trainee trainee)
        {
            foreach (Trainee T in GetTrainees())
            {
                if (trainee.ID == T.ID)
                {
                    throw new Exception("Trainee with the same id allready exists");
                }
            }
            DataSource.trainees.Add(trainee);
            return true;
        }
        #endregion

        #region remove functions
        public bool RemovedrivingTest(int serialNumber)
        {
            Test drivTestToDel = GetTests().FirstOrDefault(t => t.SerialNumber == serialNumber);
            if (drivTestToDel != null)
            {
                DataSource.Test.Remove(drivTestToDel);
                return true;
            }
            throw new Exception("test didn't found. make sure you're writing the serial number correct");
               

        }

        public bool RemoveTester(string id)
        {
            Tester testerToDel = DataSource.Testers.FirstOrDefault(t => t.ID == id);
            if (testerToDel != null)
            {
                DataSource.Testers.Remove(testerToDel);
                return true;
            }
            throw new Exception(" tester didn't found. make sure you're writing the id correctly");
        }

        public bool RemoveTrainee(string id)
        {
            Trainee traineeToDel = GetTrainees().FirstOrDefault(t => t.ID == id);
            if (traineeToDel != null)
            {
                DataSource.trainees.Remove(traineeToDel);
                return true;
            }
            throw new Exception(" trainee didn't found. make sure you're writing the id correctly");
        }
        #endregion

        #region update functions
        public bool UpdateDrivingTest(Test test)
        {
            Test testToUp = GetTests().FirstOrDefault(t => t.SerialNumber == test.SerialNumber);
            if (testToUp == null)
            {
                throw new Exception("test didn't found. make sure you enter the right serial number.");
            }
            testToUp = test;
            return true;
        }

        public bool UpdateTester(Tester tester)
        {
            Tester testerToUp = GetTesters().FirstOrDefault(t => t.ID == tester.ID);
            if (testerToUp == null)
            {
                throw new Exception("tester didn't found. make sure you enter the right id.");
            }
            testerToUp = tester;
            return true;
        }

        public bool UpdateTrainee(Trainee trainee)
        {
            Trainee traineeToUp =GetTrainees().FirstOrDefault(t => t.ID == trainee.ID);
            if (traineeToUp == null)
            {
                throw new Exception("traine didn't found. make sure you enter the right id.");
            }
            traineeToUp = trainee;
            return true;
        }
        #endregion

        #region get lists
        public List<Tester> GetTesters(Predicate<Tester>predicate = null )
        {
            if (predicate != null)
            {
                var res = from t in DataSource.Testers
                          where predicate(t)
                          select t;
                return res.ToList();
            }
            return DataSource.Testers.ToList();
        }

        public List<Test> GetTests(Predicate<Test> predicate = null)
        {
            if (predicate != null)
            {
                var res = from t in DataSource.Test
                          where predicate(t)
                          select t;
                return res.ToList();
            }
            return DataSource.Test.ToList();
        }

        public List<Trainee> GetTrainees(Predicate<Trainee> predicate = null)
        {
            if (predicate != null)
            {
                var res = from t in DataSource.trainees
                          where predicate(t)
                          select t;
                return res.ToList();
            }
            return DataSource.trainees.ToList();
        }
        #endregion
    }
}
