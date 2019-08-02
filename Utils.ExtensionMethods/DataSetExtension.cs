using System.Data;
using Newtonsoft.Json;

namespace Utils.ExtensionMethods
{
    public static class DataSetExtension
    {
        public static DataSet DefaultDataSet(this DataSet dataSet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("DefaultTable");
            dt.Columns.Add("tagName");
            dt.Columns.Add("value");
            dt.Columns.Add("confidence");
            dt.Columns.Add("timeStamp");
            DataRow dr = dt.NewRow();
            dr["tagName"] = "CQSH_TEST01_PV";
            dr["value"] = "0.01";
            dr["confidence"] = "100";
            dr["timeStamp"] = "2019-07-26T09:55:27";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["tagName"] = "CQSH_TEST02_PV";
            dr["value"] = "1213";
            dr["confidence"] = "0";
            dr["timeStamp"] = "2019-07-26T09:55:27";
            dt.Rows.Add(dr);

            ds.Tables.Add(dt);
            return ds;
        }

        public static string DataFormat(this DataSet dataSet)
        {
            string result  = JsonConvert.SerializeObject(dataSet);
            return result;
        }
    }
}