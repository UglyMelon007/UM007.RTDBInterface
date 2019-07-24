using System;
using System.Collections.Generic;
using System.Data;
using Interface.RTDB;
using log4net;
using Uniformance.PHD;
using Microsoft.Extensions.Configuration;
using Utils.Attributes;
using Utils.ExtensionMethods;

namespace DataSource.RTDB
{
    public class PHD : RTDBInterface
    {
        private readonly ILog _log = LogManager.GetLogger(GlobalAttributes.RepositoryName, typeof(PHD));
        private readonly PHDHistorian _session;


        public PHD()
        {
            var configurationSection = GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("PHD");
            string tempIp = GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("TempIP").Value;
            IConfigurationSection phdInfo = configurationSection.GetSection(tempIp);
            var _phdServer = new PHDServer(tempIp, SERVERVERSION.API200)
            {
                UserName = phdInfo.GetSection("UserName").Value,
                Password = phdInfo.GetSection("Password").Value,
                Port = Convert.ToInt32(phdInfo.GetSection("Port").Value),
            };
            _log.Info($"UserName:{_phdServer.UserName}\r\nPassword:{_phdServer.Password}\r\nPort:{_phdServer.Port}");
            _session = new PHDHistorian {DefaultServer = _phdServer};
        }

        public DataSet GetCurrentDataByTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagAndTime(string tagName, DateTime dateTime)
        {
            DataSet ds = GetDataByTagAndTime(tagName, dateTime, dateTime);
            return ds;
        }

        public DataSet GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime,
            uint period = 1)
        {
            return GetDataByTagAndTime(tagName, startDateTime, endDateTime, period);
        }

        public DataSet GetCurrentDataByTags(IList<string> tagList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataByTagsAndTime(IList<string> tagList, DateTime dateTime)
        {
            DataSet result = GetDataByTagsAndTime(tagList, dateTime, dateTime);
            return result;
        }

        public DataSet GetDataByTagsAndDuration(IList<string> tagList, DateTime startDateTime, DateTime endDateTime,
            uint period = 1)
        {
            DataSet result = GetDataByTagsAndTime(tagList, startDateTime, startDateTime, period);
            return result;
        }

        private DataSet GetDataByTagsAndTime(IList<string> tagList, DateTime startDateTime, DateTime endDateTime,
            uint period = 1)
        {
            Tags tags = new Tags();
            foreach (var t in tagList)
            {
                tags.Add(new Tag(t));
            }

            DataSet ds;
            try
            {
                _session.StartTime = _session.ConvertToPHDTime(startDateTime);
                _session.EndTime = _session.ConvertToPHDTime(endDateTime);
                _session.SampleFrequency = period;
                ds = _session.FetchRowData(tags);
            }
            catch (Exception err)
            {
                _log.Error($"取数失败 {err.Message} {err.InnerException} {err.TargetSite}");
                ds = new DataSet().DefaultDataSet();
            }
            finally
            {
                _session.Dispose();
            }

            return ds;
        }

        private DataSet GetDataByTagAndTime(string tagName, DateTime startDateTime, DateTime endDateTime,
            uint period = 1)
        {
            _log.Info($"开始取数");
            DataSet ds;
            _session.StartTime = _session.ConvertToPHDTime(startDateTime);
            _session.EndTime = _session.ConvertToPHDTime(endDateTime);
            _session.SampleFrequency = period;
            _session.Sampletype = SAMPLETYPE.Raw;

            try
            {
                ds = _session.FetchRowData(tagName);
                _log.Info($"取数成功");
            }
            catch (Exception err)
            {
                _log.Info($"取数失败{err.Message}\r\n{err.InnerException}\r\n{err.Source}\r\n{err.TargetSite}");
                _session.Dispose();
                ds = new DataSet().DefaultDataSet();
            }
            finally
            {
                _session.Dispose();
            }

            return ds;
        }
    }
}