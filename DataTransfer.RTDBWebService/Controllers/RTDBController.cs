using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using DataModel.Web;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Utils.Helper;

namespace DataTransfer.RTDBWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RTDBController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(RTDBController));

        [HttpGet]
        public ActionResult<string> Get()
        {
            return
                $"Get: GetDataByTagAndTime、GetDataByTagAndDuration;\r\nPost:GetDataByTagsAndTime、GetDataByTagsAndDuration";
        }

        [HttpGet("GetDataByTagAndTime")]
        public ActionResult<string> GetDataByTagAndTime(string tagName, string dateTime)
        {
            //yyyy-MM-dd hh:mm:ss
            return RTDBHelper.GetDataByTagAndTime(tagName, Convert.ToDateTime(dateTime))
                .ToString(CultureInfo.CurrentCulture);
        }

        [HttpGet("GetDataByTagAndDuration")]
        public ActionResult<string> GetDataByTagAndDuration(string tagName, string startTime, string endTime)
        {
            return string.Join(",",
                RTDBHelper.GetDataByTagAndDuration(tagName, Convert.ToDateTime(startTime),
                    Convert.ToDateTime(endTime)));
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
        public ActionResult<string> GetDataByTagsAndDuration([FromBody] TagsInfo tagsInfo)
        {
            return string.Join(",",
                RTDBHelper.GetDataByTagsAndDuration(new List<string>(tagsInfo.TagsName.Split(',')),
                    Convert.ToDateTime(tagsInfo.StartTime), Convert.ToDateTime(tagsInfo.EndTime)));
        }
    }
}