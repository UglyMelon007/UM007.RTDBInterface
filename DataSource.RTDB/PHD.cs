using System;
using System.Collections.Generic;
using System.Data;
using Interface.RTDB;
using log4net;
using Uniformance.PHD;
using Microsoft.Extensions.Configuration;
using Utils.Attributes;

namespace DataSource.RTDB
{
    public class PHD : RTDBInterface
    {
        private readonly ILog _log = LogManager.GetLogger("RTDBTest", typeof(PHD));
        private IConfigurationSection ConfigurationSection;

        public PHD()
        {
            ConfigurationSection = GlobalAttributes.Configuration.GetSection("RTDBInfo").GetSection("PHD");
        }

        public PHDServer GetPHDserver(string tagIp)
        {
            IConfigurationSection phdInfo = ConfigurationSection.GetSection(tagIp);
            var pHDServer = new PHDServer(tagIp, SERVERVERSION.API200)
            {
                UserName = phdInfo.GetSection("UserName").Value,
                Password = phdInfo.GetSection("Password").Value,
                Port = Convert.ToInt32(phdInfo.GetSection("Port").Value),
            };
            _log.Info($"UserName:{pHDServer.UserName}\r\nPassword:{pHDServer.Password}\r\nPort:{pHDServer.Port}");
            return pHDServer;
        }

        public double GetDataByTagAndTime(string tagName, DateTime dateTime, string tagIp)
        {
            double[] result = GetDataByTagAndTime(tagName, dateTime, dateTime, tagIp);
            return result != null ? double.Parse(result[0].ToString()) : 0.0;
        }

        public double[] GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            double[] result = GetDataByTagAndTime(tagName, startDateTime, endDateTime, tagIp);
            return result;
        }

        public DataSet GetDataByTagsAndTime(List<string> tagList, DateTime dateTime, string tagIp)
        {
            DataSet result = GetDataByTagsAndTime(tagList, dateTime, dateTime, tagIp);
            return result;
        }

        public DataSet GetDataByTagsAndDuration(List<string> tagList, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            DataSet result = GetDataByTagsAndTime(tagList, startDateTime, startDateTime, tagIp);
            return result;
        }

        private DataSet GetDataByTagsAndTime(List<string> tagList, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            PHDHistorian pHDHistorian = new PHDHistorian();
            Tags tags = new Tags();
            for (int i = 0; i < tagList.Count; i++)
            {
                tags.Add(new Tag(tagList[i]));
            }

            using (PHDServer pHDServer = GetPHDserver(tagIp))
            {
                DataSet result = new DataSet();
                try
                {
                    pHDHistorian.DefaultServer = pHDServer;
                    pHDHistorian.FetchRowData(tags);
                    pHDHistorian.StartTime = pHDHistorian.ConvertToPHDTime(startDateTime);
                    pHDHistorian.EndTime = pHDHistorian.ConvertToPHDTime(endDateTime);
                    result = pHDHistorian.FetchRowData(tags);
                }
                catch
                {
                }
                finally
                {
                    pHDServer.Dispose();
                }

                return result;
            }
        }

        private double[] GetDataByTagAndTime(string tagName, DateTime startDateTime, DateTime endDateTime,
            string tagIp)
        {
            _log.Info($"开始取数");
            PHDHistorian pHDHistorian = new PHDHistorian();
            using (PHDServer pHDServer = GetPHDserver(tagIp))
            {
                pHDHistorian.DefaultServer = pHDServer;
                pHDHistorian.StartTime = pHDHistorian.ConvertToPHDTime(startDateTime);
                pHDHistorian.EndTime = pHDHistorian.ConvertToPHDTime(endDateTime);
                pHDHistorian.Sampletype = SAMPLETYPE.Raw;
                double[] array = null;
                short[] array2 = null;
                double[] result = null;
                try
                {
                    pHDHistorian.FetchData(new Tag(tagName), ref array, ref result, ref array2);
                    _log.Info($"取数成功");
                }
                catch (Exception err)
                {
                    _log.Info($"取数失败{err.Message}\r\n{err.InnerException}\r\n{err.Source}\r\n{err.TargetSite}");
                    pHDServer.Dispose();
                }
                finally
                {
                    pHDServer.Dispose();
                }

                return result;
            }
        }
    }
}