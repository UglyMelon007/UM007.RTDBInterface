using System;
using System.Collections.Generic;
using System.Data;
using DataSource.RTDB;
using Utils.Attributes;

namespace Utils.Helper
{
    public static class RTDBHelper
    {
        public static double GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            return new PHD().GetDataByTagAndTime(tagName, dateTime, GetIP(tagName));
        }

        public static double[] GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime)
        {
            return new PHD().GetDataByTagAndDuration(tagName, startDateTime, endDateTime,GetIP(tagName));
        }

        public static DataSet GetDataByTagsAndTime(List<string> tagList, DateTime dateTime)
        {
            return new PHD().GetDataByTagsAndTime(tagList,dateTime,GetIP(tagList[0]));
        }

        public static DataSet GetDataByTagsAndDuration(List<string> tagList, DateTime startDateTime, DateTime endDateTime)
        {
            return new PHD().GetDataByTagsAndDuration(tagList, startDateTime, endDateTime, GetIP(tagList[0]));
        }

        private static string GetIP(string tagName)
        {
            return GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("TempIP").Value;
        }
    }
}