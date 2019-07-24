using System;
using System.Collections.Generic;
using System.Data;
using DataSource.RTDB;
using Utils.Attributes;

namespace Utils.Helper
{
    public static class RTDBHelper
    {
        public static DataSet GetCurrentDataByTag(string tagName)
        {
            return new PHDR().GetCurrentDataByTag(tagName);
        }

        public static DataSet GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            return new PHDR().GetDataByTagAndTime(tagName, dateTime);
        }

        public static DataSet GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime)
        {
            return new PHDR().GetDataByTagAndDuration(tagName, startDateTime, endDateTime);
        }

        public static DataSet GetCurrentDataByTags(IList<string> tagList)
        {
            return new PHDR().GetCurrentDataByTags(tagList);
        }

        public static DataSet GetDataByTagsAndTime(IList<string> tagList, DateTime dateTime)
        {
            return new PHDR().GetDataByTagsAndTime(tagList, dateTime);
        }

        public static DataSet GetDataByTagsAndDuration(IList<string> tagList, DateTime startDateTime,
            DateTime endDateTime)
        {
            return new PHDR().GetDataByTagsAndDuration(tagList, startDateTime, endDateTime);
        }

        private static string GetIP(string tagName)
        {
            return GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("TempIP").Value;
        }
    }
}