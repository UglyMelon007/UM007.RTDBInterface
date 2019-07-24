using System;
using System.Collections.Generic;
using System.Data;
using DataModel.Web;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Utils.Attributes;
using Utils.Helper;

namespace DataTransfer.RTDBWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RTDBController : ControllerBase
    {
        private static readonly ILog _log =
            LogManager.GetLogger(GlobalAttributes.RepositoryName, typeof(RTDBController));

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Get: GetDataByTagAndTime(获取指定位号指定时间的值)、GetDataByTagAndDuration(获取指定位号指定时间段的值);\r\n" +
                   "Post:GetDataByTagsAndTime(获取多个位号指定时间的值)、GetDataByTagsAndDuration(获取多个位号指定时间段的值)\r\n" +
                   "时间类型：yyyy-MM-dd hh:mm:ss";
        }

        [HttpGet("GetCurrentDataByTag")]
        public ActionResult<DataSet> GetCurrentDataByTag(string tagName)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetCurrentDataByTag(tagName);
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }

        [HttpGet("GetDataByTagAndTime")]
        public ActionResult<DataSet> GetDataByTagAndTime(string tagName, string dateTime)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetDataByTagAndTime(tagName, Convert.ToDateTime(dateTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }

        [HttpGet("GetDataByTagAndDuration")]
        public ActionResult<DataSet> GetDataByTagAndDuration(string tagName, string startTime, string endTime)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetDataByTagAndDuration(tagName, Convert.ToDateTime(startTime),
                Convert.ToDateTime(endTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }

        [HttpGet("GetCurrentDataByTags")]
        public ActionResult<DataSet> GetCurrentDataByTags([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetCurrentDataByTags(tagsName);
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }

        [HttpPost("GetDataByTagsAndTime")]
        public ActionResult<DataSet> GetDataByTagsAndTime([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetDataByTagsAndTime(tagsName, Convert.ToDateTime(tagsInfo.DateTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }

        [HttpPost("GetDataByTagsAndDuration")]
        public ActionResult<DataSet> GetDataByTagsAndDuration([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            DataSet ds = RTDBHelper.GetDataByTagsAndDuration(tagsName, Convert.ToDateTime(tagsInfo.StartTime),
                Convert.ToDateTime(tagsInfo.EndTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return ds;
        }
    }
}