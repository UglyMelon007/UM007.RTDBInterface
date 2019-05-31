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

        [HttpPost("GetDataByTagsAndTime")]
        public ActionResult<string> GetDataByTagsAndTime([FromBody] TagsInfo tagsInfo)
        {
            //yyyy-MM-dd hh:mm:ss
            return RTDBHelper.GetDataByTagAndTime(tagsInfo.TagsName, Convert.ToDateTime(tagsInfo.DateTime)).ToString(CultureInfo.CurrentCulture);
        }

        [HttpPost("GetDataByTagsAndDuration")]
        public ActionResult<string> GetDataByTagsAndDuration([FromBody] TagsInfo tagsInfo)
        {
            return
                $"GetDataByTagsAndDuration:\r\nTagName:{tagsInfo.TagsName}\r\nStartTime:{tagsInfo.StartTime}-EndTime:{tagsInfo.EndTime}";
        }
    }
}