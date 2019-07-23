using System;
using System.Collections.Generic;
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
        public double GetPHDTagValue(string tagName, DateTime valueTime)
        {
            DataSet ds = GetDataByTagAndDuration(tagName, valueTime, valueTime, 1);
            if ((ds == null) || (ds.Tables[0].Rows.Count == 0))
            {
                _log.Error("该时间点此位号不存在数据，位号为：" + tagName);
                return double.Parse("0");
            }

            DataRow dr = ds.Tables[0].Rows[0];
            return double.Parse(dr["Value"].ToString());
        }

        /// <summary>
        /// 获取指定位号，特定时间段内的实时数据
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="period"></param>
        /// <returns>结果集，字段分别为"Tag", "timestamp", "Value", "Conf", "HostName"</returns>
        private DataSet GetDataByTagAndDuration(string tagName, DateTime startTime, DateTime endTime, uint period)
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
                _log.Error($"{err.Message} {err.InnerException} {err.Source}");
                return default(DataSet);
            }
        }

        /// <summary>
        /// 获取多个点位指定时间的值
        /// </summary>
        /// <param name="tagNames">点位号</param>
        /// <param name="valueTime">查询时间</param>
        /// <returns>多个点位的tag值的数据结构</returns>
        public DataSet GetDataByTagsAndTime(IList<string> tagNames, DateTime valueTime)
        {
            DataSet ds = GetDataByTagsAndDuration(tagNames, valueTime, valueTime, 1);
            return ds;
        }

        /// <summary>
        /// 同时获取多个指定位号，特定时间段内的实时数据
        /// </summary>
        /// <param name="tagNames">位号列表</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="period">采样间隔</param>
        /// <returns>结果集，字段分别为"Tag", "timestamp", "Value", "Conf", "HostName"</returns>
        public DataSet GetDataByTagsAndDuration(IList<string> tagNames, DateTime startTime, DateTime endTime,
            uint period)
        {
            _log.Debug("开始读取" + tagNames.Count.ToString() + "个数据点，从" + startTime.ToShortTimeString() + "到" +
                       endTime.ToShortTimeString() + "的实时数据库数据。");
            _session.StartTime = _session.ConvertToPHDTime(startTime);
            _session.EndTime = _session.ConvertToPHDTime(endTime);
            _session.SampleFrequency = period;
            _session.MaximumRows = 0;
            Tags tags = new Tags();
            foreach (string tagName in tagNames)
            {
                tags.Add(new Tag(tagName));
            }

            try
            {
                return _session.FetchRowData(tags);
            }
            catch (Exception err)
            {
                _log.Error("实时数据库读取失败，请检查连接到PHD地址、用户名和口令是否正确！或检查查询的数据点是否存在！");
                _log.Error($"{err.Message} {err.InnerException} {err.Source} {err.TargetSite}");
                return default(DataSet);
            }
        }
    }
}