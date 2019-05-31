using System;
using System.Collections.Generic;
using System.Data;

namespace Interface.RTDB
{
    public interface RTDBInterface

    {
        /// <summary>
        /// 单个点当前指定时间值
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="dateTime"></param>
        /// <param name="tagIp"></param>
        /// <returns></returns>
        double GetDataByTagAndTime(string tagName, DateTime dateTime, string tagIp);

        /// <summary>
        /// 单个点一段时间段值 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="tagIp"></param>
        /// <returns></returns>
        double[] GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime, string tagIp);

        /// <summary>
        /// 多个点指定时间值
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="dateTime"></param>
        /// <param name="tagIp"></param>
        /// <returns></returns>
        DataSet GetDataByTagsAndTime(List<string> tagList, DateTime dateTime, string tagIp);

        /// <summary>
        /// 多个点指定时间段值
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="tagIp"></param>
        /// <returns></returns>
        DataSet GetDataByTagsAndDuration(List<string> tagList, DateTime startDateTime, DateTime endDateTime,
            string tagIp);
    }
}