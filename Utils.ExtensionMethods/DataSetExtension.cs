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
            var result = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(dataSet.Tables[0].Rows[0]["timeStamp"].ToString().Substring(0,16));
            DataRow dr = dt.NewRow();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                dt.Columns.Add(dataRow["tagName"].ToString());
                dr[dataRow["tagName"].ToString()] = dataRow["value"].ToString();
            }

            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            result  = JsonConvert.SerializeObject(ds).Replace("[","").Replace("]","");
            return result;
        }
    }
}