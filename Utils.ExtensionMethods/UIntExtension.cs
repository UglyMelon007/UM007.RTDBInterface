using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.ExtensionMethods
{
    public static class UIntExtension
    {
        public static uint Init(this uint period, DateTime startDate, DateTime endTime)
        {
            return period == 0 ? GetPeriod(startDate, endTime) : period;
        }

        private static uint GetPeriod(in DateTime startDate, in DateTime endTime)
        {
            uint period = 0;
            var days = (endTime - startDate).Days;
            if (days >= 15)
            {
                period = 60;
            }
            else if (days >= 7)
            {
                period = 30;
            }
            else if (days >= 1)
            {
                period = 15;
            }
            else
            {
                period = 1;
            }

            return period * 60;
        }
    }
}