using System;
using System.Collections.Generic;
using System.Globalization;
using DataModel.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utils.Helper;

namespace DataTransfer.RTDBWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RTDBController : ControllerBase
    {
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
        public ActionResult<string> GetDataByTagsAndTime([FromBody] TagsInfo tagsInfo)
        {
            return string.Join(",",
                RTDBHelper.GetDataByTagsAndTime(new List<string>(tagsInfo.TagsName.Split(',')),
                    Convert.ToDateTime(tagsInfo.DateTime)));
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