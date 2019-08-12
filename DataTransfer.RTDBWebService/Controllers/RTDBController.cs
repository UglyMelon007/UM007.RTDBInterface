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
            return "Get: GetCurrentDataByTag(获取指定位号当前值)、GetDataByTagAndTime(获取指定位号指定时间的值)、GetDataByTagAndDuration(获取指定位号指定时间段的值);\r\n" +
                   "Post: GetCurrentDataByTags(获取多个位号当前值)、GetDataByTagsAndTime(获取多个位号指定时间的值)、GetDataByTagsAndDuration(获取多个位号指定时间段的值)\r\n" +
                   "时间格式：yyyy-MM-dd hh:mm:ss";
        }

        [HttpGet("GetCurrentDataByTag")]
        public ActionResult<string> GetCurrentDataByTag(string tagName)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetCurrentDataByTag(tagName);
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }

        [HttpGet("GetDataByTagAndTime")]
        public ActionResult<string> GetDataByTagAndTime(string tagName, string dateTime)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetDataByTagAndTime(tagName, Convert.ToDateTime(dateTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }

        [HttpGet("GetDataByTagAndDuration")]
        public ActionResult<string> GetDataByTagAndDuration(string tagName, string startTime, string endTime, string period)
        {
            _log.Info($"开始取数据，位号:{tagName}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetDataByTagAndDuration(tagName, Convert.ToDateTime(startTime),
                Convert.ToDateTime(endTime), Convert.ToUInt32(period));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }

        [HttpPost("GetCurrentDataByTags")]
        public ActionResult<string> GetCurrentDataByTags([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetCurrentDataByTags(tagsName);
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }

        [HttpPost("GetDataByTagsAndTime")]
        public ActionResult<string> GetDataByTagsAndTime([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetDataByTagsAndTime(tagsName, Convert.ToDateTime(tagsInfo.DateTime));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }

        [HttpPost("GetDataByTagsAndDuration")]
        public ActionResult<string> GetDataByTagsAndDuration([FromBody] TagsInfo tagsInfo)
        {
            IList<string> tagsName = new List<string>(tagsInfo.TagsName.Split(','));
            _log.Info($"开始取数据，位号个数{tagsName.Count}, 时间：{DateTime.Now.ToLongTimeString()}");
            var result = RTDBHelper.GetDataByTagsAndDuration(tagsName, Convert.ToDateTime(tagsInfo.StartTime),
                Convert.ToDateTime(tagsInfo.EndTime),Convert.ToUInt32(tagsInfo.Period));
            _log.Info($"取数完成，时间：{DateTime.Now.ToLongTimeString()}");
            return result;
        }
    }
}