using System;
using System.Collections.Generic;
using System.Data;
using DataSource.RTDB;
using Utils.Attributes;
using Utils.ExtensionMethods;

namespace Utils.Helper
{
    public static class RTDBHelper
    {
        public static string GetCurrentDataByTag(string tagName)
        {
            return new PHDR().GetCurrentDataByTag(tagName).DataFormat();
        }

        public static string GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            return new PHDR().GetDataByTagAndTime(tagName, dateTime).DataFormat();
        }

        public static string GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime)
        {
            return new PHDR().GetDataByTagAndDuration(tagName, startDateTime, endDateTime).DataFormat();
        }

        public static string GetCurrentDataByTags(IList<string> tagList)
        {
            return new PHDR().GetCurrentDataByTags(tagList).DataFormat();
        }

        public static string GetDataByTagsAndTime(IList<string> tagList, DateTime dateTime)
        {
            return new PHDR().GetDataByTagsAndTime(tagList, dateTime).DataFormat();
        }

        public static string GetDataByTagsAndDuration(IList<string> tagList, DateTime startDateTime,
            DateTime endDateTime, uint period = 0)
        {
            return new PHDR().GetDataByTagsAndDuration(tagList, startDateTime, endDateTime, period.Init(startDateTime, endDateTime)).DataFormat();
        }

        private static string GetIP(string tagName)
        {
            return GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("TempIP").Value;
        }
    }
}