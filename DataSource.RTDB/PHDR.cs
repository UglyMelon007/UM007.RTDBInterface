using System;
using System.Data;
using log4net;
using Microsoft.Extensions.Configuration;
using Uniformance.PHD;
using Utils.Attributes;

namespace DataSource.RTDB
{
    public class PHDR
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PHDR));
        private IConfigurationSection ConfigurationSection;

        private PHDHistorian _session;


        public PHDR()
        {
            ConfigurationSection = GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("PHD");
            string tempIp = GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("TempIP").Value;
            IConfigurationSection phdInfo = ConfigurationSection.GetSection(tempIp);
            PHDServer _phdServer = new PHDServer
            {
                HostName = tempIp,
                UserName = phdInfo.GetSection("UserName").Value,
                Password = phdInfo.GetSection("Password").Value,
                APIVersion = SERVERVERSION.RAPI200
            };
            _session = new PHDHistorian {DefaultServer = _phdServer, Sampletype = SAMPLETYPE.Snapshot};
        }

        /// <summary>
        /// 查询指定时间Tag值
        /// </summary>
        /// <param name="tagName">点位号</param>
        /// <param name="valueTime">查询时间</param>
        /// <returns>点位数据结构</returns>
        public double getPHDTagValue(string tagName, DateTime valueTime)
        {
            DataSet ds = getPHDTagValues(tagName, valueTime, valueTime, 1);
            if ((ds == null) || (ds.Tables[0].Rows.Count == 0))
            {
                _log.Error("该时间点此位号不存在数据，位号为：" + tagName);
                return double.Parse("0");
            }
            DataRow dr = ds.Tables[0].Rows[0];
            return double.Parse(dr["Value"].ToString());
        }

        private DataSet getPHDTagValues(string tagName, DateTime startTime, DateTime endTime, uint period)
        {
            _log.Info("开始读取" + tagName + "从" + startTime.ToString() + "到" + endTime.ToString() + "的实时数据库数据。");
            _session.StartTime = _session.ConvertToPHDTime(startTime);
            _session.EndTime = _session.ConvertToPHDTime(endTime);
            _session.SampleFrequency = (uint) period;
            _session.MaximumRows = 0;
            try
            {
                return _session.FetchRowData(tagName);
            }
            catch (Exception err)
            {
                _log.Error("实时数据库读取失败，请检查连接到PHD地址、用户名和口令是否正确！或检查查询的数据点是否存在！");
                _log.Error(err.Message);
                return default(DataSet);
            }
        }
    }
}