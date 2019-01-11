using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBl
    {
        #region tester function
        bool AddTester(Tester tester);
        bool RemoveTester(string testerID);
        bool UpdateTester(Tester tester);
        #endregion
        
        #region trainee function
        bool AddTrainee(Trainee trainee);
        bool RemoveTrainee(string traineeID);
        bool UpdateTrainee(Trainee trainee);
        #endregion
        
        #region test function
        bool AddDrivingTest(Test test);
        bool RemovedrivingTest(int serialNumber);
        bool UpdateDrivingTest(Test test);
        #endregion
           
        #region getting list function
        List<Tester> GetTesters(Predicate<Tester> predicate = null);
        List<Trainee> GetTrainees(Predicate<Trainee> predicate =null);
        List<Test> GetTests(Predicate<Test> predicate = null);
        #endregion
    }
}
