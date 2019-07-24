using System;
using System.Collections.Generic;
using System.Data;

namespace Interface.RTDB
{
    public interface RTDBInterface

    {
        /// <summary>
        /// 单个点当前值
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        DataSet GetCurrentDataByTag(string tagName);

        /// <summary>
        /// 单个点指定时间值
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DataSet GetDataByTagAndTime(string tagName, DateTime dateTime);

        /// <summary>
        /// 单个点一段时间段值 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        DataSet GetDataByTagAndDuration(string tagName, DateTime startDateTime, DateTime endDateTime, uint period = 1);


        /// <summary>
        /// 多个点当前值
        /// </summary>
        /// <param name="tagList"></param>
        /// <returns></returns>
        DataSet GetCurrentDataByTags(IList<string> tagList);

        /// <summary>
        /// 多个点指定时间值
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DataSet GetDataByTagsAndTime(IList<string> tagList, DateTime dateTime);

        /// <summary>
        /// 多个点指定时间段值
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        DataSet GetDataByTagsAndDuration(IList<string> tagList, DateTime startDateTime, DateTime endDateTime,
            uint period = 1);
    }
}