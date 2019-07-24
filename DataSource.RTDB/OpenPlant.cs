using System;
using System.Collections.Generic;
using System.Data;
using Interface.RTDB;

namespace DataSource.RTDB
{
    public class OpenPlant : RTDBInterface
    {
        public DataSet GetCurrentDataByTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime, uint period = 1)
        {
            throw new NotImplementedException();
        }

        public DataSet GetCurrentDataByTags(IList<string> tagList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagsAndTime(IList<string> tagList, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagsAndDuration(IList<string> tagList, DateTime startDateTime, DateTime endDateTime, uint period = 1)
        {
            throw new NotImplementedException();
        }
    }
}