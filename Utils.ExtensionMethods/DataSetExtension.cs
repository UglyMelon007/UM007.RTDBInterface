using System;
using System.Data;

namespace Utils.ExtensionMethods
{
    public static class DataSetExtension
    {
        public static DataSet DefaultDataSet(this DataSet dataSet)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("DefaultTable");
            dt.Columns.Add("TagName");
            dt.Columns.Add("Value");
            dt.Columns.Add("Confidence");
            DataRow dr = dt.NewRow();
            dr["TagName"] = "DefaultTagName";
            dr["Value"] = "DefaultValue";
            dr["Confidence"] = "DefaultConfidence";
            dt.Rows.Add(dr);

            ds.Tables.Add(dt);
            return ds;
        }
    }
}
