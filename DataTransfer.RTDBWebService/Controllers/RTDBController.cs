using System;
using System.Globalization;
using DataModel.Web;
using Microsoft.AspNetCore.Mvc;
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
            return RTDBHelper.GetDataByTagAndTime(tagName, Convert.ToDateTime(dateTime)).ToString(CultureInfo.CurrentCulture);
        }

        [HttpGet("GetDataByTagAndDuration")]
        public ActionResult<string> GetDataByTagAndDuration(string tagName, string startTime, string endTime)
        {
            return $"GetDataByTagAndTime:\r\nTagName:{tagName}\r\nStartTime:{startTime}-EndTime:{endTime}";
        }

        [HttpPost("GetDataByTagsAndTime")]
        public ActionResult<string> GetDataByTagsAndTime([FromBody] TagsInfo tagsInfo)
        {
            return $"GetDataByTagAndTime:\r\nTagName:{tagsInfo.TagsName}\r\nDateTime:{tagsInfo.DateTime}";
        }

        [HttpPost("GetDataByTagsAndDuration")]
        public ActionResult<string> GetDataByTagsAndDuration([FromBody] TagsInfo tagsInfo)
        {
            return
                $"GetDataByTagsAndDuration:\r\nTagName:{tagsInfo.TagsName}\r\nStartTime:{tagsInfo.StartTime}-EndTime:{tagsInfo.EndTime}";
        }
    }
}