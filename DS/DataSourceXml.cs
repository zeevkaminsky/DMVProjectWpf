using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS
{
    public static class DataSourceXML
    {
        
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        private static string filePath = Path.Combine(solutionDirectory, "DS", "DataXML");

        private static XElement configRoot = null;
        private static XElement traineeRoot = null;
        private static XElement testerRoot = null;
        private static XElement testRoot = null;

        private static string configPath = Path.Combine(filePath, "ConfigXml.xml");
        private static string traineePath = Path.Combine(filePath, "TraineeXml.xml");
        private static string testerPath = Path.Combine(filePath, "TesterXml.xml");
        private static string testPath = Path.Combine(filePath, "TestXml.xml");

        static DataSourceXML()
        {
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(traineePath))
            {
                CreateFile("Trainees", traineePath);

            }
            traineeRoot = LoadData(traineePath);


            if (!File.Exists(testerPath))
            {
                CreateFile("Testers", testerPath);

            }
            testerRoot = LoadData(testerPath);

            if (!File.Exists(testPath))
            {
                CreateFile("Tests", testPath);

            }
            testRoot = LoadData(testPath);

            if (!File.Exists(configPath))
            {
                CreateFile("Config", configPath);
                configRoot = LoadData(configPath);
                XElement minLessons = new XElement("MIN_LESSONS",  new XElement("value", 28));
                XElement maxTesterAge = new XElement("MAX_AGE_OF_TESTER", new XElement("value", 85));
                XElement minTraineeAge = new XElement("MIN_AGE_OF_TRAINEE",  new XElement("value", 17));
               
                XElement minDaysBetweenTests = new XElement("MIN_DAYS_BETWEEN_TESTS", new XElement("value", 7));
                XElement minTesterAge = new XElement("MIN_AGE_OF_TESTER",  new XElement("value", 30));
                XElement SerialNumber = new XElement("Test_Serial_Number",  new XElement("value", 12345678));
                configRoot.Add(minLessons, maxTesterAge,  minTesterAge, SerialNumber);
                configRoot.Save(configPath);



            }
            configRoot = LoadData(configPath);

        }
        private static void CreateFile(string typename, string path)
        {
            XElement root = new XElement(typename);
            root.Save(path);
        }

        public static void SaveTrainees()
        {
            traineeRoot.Save(traineePath);
        }

        public static void SaveTesters()
        {
            testerRoot.Save(testerPath);
        }

        public static void SaveTests()
        {
            testRoot.Save(testPath);
        }

        public static void SaveConfig()
        {
            configRoot.Save(configPath);
        }

        public static XElement Trainees
        {
            get
            {
                traineeRoot = LoadData(traineePath);
                return traineeRoot;
            }
        }

        public static XElement Testers
        {
            get
            {
                testerRoot = LoadData(testerPath);
                return testerRoot;
            }
        }

        public static XElement Tests
        {
            get
            {
                testRoot = LoadData(testPath);
                return testRoot;
            }
        }

        public static XElement Config
        {
            get
            {
                configRoot = LoadData(configPath);
                return configRoot;
            }
        }

        public static XElement LoadData(string path)
        {
            XElement root;
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            return root;
        }
        private static void setConfig()
        {
            
        }
    }
}
