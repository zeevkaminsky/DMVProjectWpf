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
        //private static string currentDirectory = Directory.GetCurrentDirectory();
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        private static string filePath = Path.Combine(solutionDirectory, "DS", "DataXML");


        private static XElement traineeRoot = null;
        private static XElement testerRoot = null;
        private static XElement drivingtestRoot = null;

        private static string traineePath = Path.Combine(filePath, "TraineeXml.xml");
        private static string testerPath = Path.Combine(filePath, "TesterXml.xml");
        private static string drivingtestPath = Path.Combine(filePath, "DrivingtestXml.xml");

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

            if (!File.Exists(drivingtestPath))
            {
                CreateFile("DrivingTests", drivingtestPath);

            }
            drivingtestRoot = LoadData(drivingtestPath);

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

        public static void SaveDrivingtests()
        {
            drivingtestRoot.Save(drivingtestPath);
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

        public static XElement DrivingTests
        {
            get
            {
                drivingtestRoot = LoadData(drivingtestPath);
                return drivingtestRoot;
            }
        }

        private static XElement LoadData(string path)
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

    }
}
