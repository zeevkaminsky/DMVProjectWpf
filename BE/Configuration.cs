﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static double minTraineeAge = 17.0;
        public static double minTesterAge = 25.0;
        public static int minLessons = 28;
        public static int lessonsBetweenTests = 2;
        public static int daysBetweenTests = 7;
        public static int InitialSerialNumber = 12345678;
        public static double MaxTesterAge = 85.0;

        public static T ToEnum<T>(string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

    }
}