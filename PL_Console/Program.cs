using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            init();
            Test test = new Test
            {
                vehicle = Vehicle.car,
                gear = Gear.auto
            };

            Console.WriteLine("trainee id");
            test.TraineeID = Console.ReadLine();
            DateTime datetime = new DateTime(2020, 5, 5, 10, 0, 0);
            test.TestTime = datetime;
            Console.WriteLine("exit point");

            BL.IBl mofa = FactorySingletonBl.GetBl();

            
            List<Tester> testers = mofa.FindTesterToTest(test);
            if (testers.Count == 0)
            {
                Console.WriteLine("damn");
                Console.ReadKey();
                return;
            }
            test.TesterID = testers.First().ID;
            Console.WriteLine(test.ToString());

            Console.ReadKey();



        }

        public static void init()
        {
            try
            {
                BL.IBl mofa = FactorySingletonBl.GetBl();


                mofa.AddTester(new Tester
                {
                    ID = "1111",
                    Name = new FullName { FirstName = "jojo", LastName = "chalass" },
                    Address = new Address
                    {
                        Town = "Jerusalem",
                        Building = 21,
                        Street = "havvad haleumi",
                        //                  ZipCode = 91160
                    },
                    DateOfBirth = DateTime.Now.AddYears(-45),
                    Gender = Gender.MALE,
                    Experience = 10,
                    MyVehicle = Vehicle.trailer,
                    MaxDistance = 2,
                    MaxTests = 1,
                    WeeklySchedule = new Schedule
                    {
                        weeklySchedule = new string[5][]
                        {
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                         }
                    }


                });

                mofa.AddTrainee(new Trainee
                {
                    ID = "9999",
                    Name = new FullName { FirstName = "eran", LastName = "zehuze" },
                    Address = new Address
                    {
                        Town = "TLv",
                        Building = 21,
                        Street = "Jerusalem Bld",
                        //                  ZipCode = 91160
                    },
                    DateOfBirth = DateTime.Now.AddYears(-21),
                    Gender = Gender.MALE,
                    MyVehicle = Vehicle.motorcycle,
                    School = "Machon Bli Lev",
                    MyGear = Gear.manual,
                    TeacherName = new FullName { FirstName = "Martze", LastName = "bemivne netunim" },
                    NumOfLessons = 134
                }
                );
                mofa.AddTrainee(new Trainee
                {
                    ID = "99910",
                    Name = new FullName { FirstName = "Emanuel", LastName = "Macron" },
                    Address = new Address
                    {
                        Town = "Haifa",
                        Building = 100,
                        Street = "Hell Av.",
                        //                  ZipCode = 91160
                    },
                    DateOfBirth = DateTime.Now.AddYears(-24),
                    Gender = Gender.MALE,
                    MyVehicle = Vehicle.car,
                    School = "Machon Bli Kishkes",
                    MyGear = Gear.manual,
                    TeacherName = new FullName { FirstName = "Super", LastName = "lo Kayam" },
                    NumOfLessons = 12
                }
                );
                mofa.AddTester(new Tester
                {
                    ID = "12345",
                    Name = new FullName { FirstName = "jojo", LastName = "chalass" },
                    Address = new Address
                    {
                        Town = "Jerusalem",
                        Building = 21,
                        Street = "havvad haleumi",
                        //                  ZipCode = 91160
                    },
                    DateOfBirth = DateTime.Now.AddYears(-50),
                    Gender = Gender.MALE,
                    Experience = 10,
                    MyVehicle = Vehicle.tractor,
                    MaxDistance = 2,
                    MaxTests = 1,
                    WeeklySchedule = new Schedule
                    {
                        weeklySchedule = new string[5][]
                        {
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                        new string[] { "work", "dosn't work", "dosn't work", "dosn't work",  "work",  "work" },
                         }
                    }
                });





                foreach (var item in mofa.GetTesters())
                {
                    Console.WriteLine(item.ToString());
                }

                foreach (var item in mofa.GetTrainees())
                {
                    Console.WriteLine(item.ToString());
                }



            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}

