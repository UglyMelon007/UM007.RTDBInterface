using System;
using System.Collections.Generic;
using System.Data;
using Interface.RTDB;

namespace DataSource.RTDB
{
    public class OpenPlant : RTDBInterface
    {
        public OpenPlant()
        {
        }

        public double GetDataByTagAndTime(string tagName, DateTime dateTime, string tagIp)
        {
            throw new NotImplementedException();
        }

        public double[] GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagsAndTime(List<string> tagName, DateTime dateTime, string tagIp)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagsAndDuration(List<string> tagList, DateTime startDateTime, DateTime endDateTime, string tagIp)
        {
            throw new NotImplementedException();
        }
    }
}