using System;
using System.Collections.Generic;
using System.Data;
using DataSource.RTDB;

namespace Utils.Helper
{
    public static class RTDBHelper
    {
        public static double GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            return new PHD().GetDataByTagAndTime(tagName, dateTime, GetIP(tagName));
        }

        public static double[] GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            throw new NotImplementedException();
        }

        public static DataSet GetDataByTagsAndTime(List<string> tagList, DateTime dateTime, string tagIp)
        {
            throw new NotImplementedException();
        }

        public static DataSet GetDataByTagsAndDuration(List<string> tagList, DateTime startDateTime,
            DateTime endDateTime, string tagIp)
        {
            throw new NotImplementedException();
        }

        private static string GetIP(string tagName)
        {
            return "10.189.100.52";
        }
    }
}