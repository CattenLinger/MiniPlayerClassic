using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic.Algorithms
{
    class Algorithms
    {
        public enum t_formate //时间转换函数的格式类型枚举
        {
            full_day = 0,
            full_hour = 1,
            full_minute = 2,
            simple_second = 3,

            day = 4,
            hour = 5,
            minute = 6,
            second = 7,
            ms = 8

        }

        //转换毫秒到格式化时间字符串
        public static string trans_Time(Int64 ms) //单变量的重载
        {
            return trans_Time(ms, t_formate.full_day, true);//其实就是填默认值而已
        }

        public static string trans_Time(Int64 ms, t_formate formate)//这个也是
        {
            return trans_Time(ms, formate, true);
        }

        public static string trans_Time(Int64 ms, t_formate formate, bool full)
        {
            const int msPerSec = 1000;//每秒的毫秒数
            const int sPerMinute = 60;//每分钟的秒数
            const int sPerHour = 60 * 60;//每小时的秒数

            string temp1 = "";//存储结果的临时变量
            Int32 second, minute = 0;
            int hour, day = 0;
            byte tmp_second, tmp_minute, tmp_hour, tmp_day = 0;//这个存放最终算出来的数据
            int tmp_ms;//存储最终的毫秒

            second = (Int32)(ms / msPerSec);//把毫秒转换为秒
            minute = second / sPerMinute;//把秒转换为分钟
            hour = second / sPerHour;//把分钟转换为小时
            day = (int)(second / (sPerHour * 24));//把小时转换为日

            tmp_ms = (int)(ms % 1000); if (ms >= 1000) { ms = 0; }//毫秒模1000放进tmp_ms，后面那句防溢出，下面以此类推
            tmp_second = (byte)(second % 60); if (tmp_second >= 60) { tmp_second = 0; }
            tmp_minute = (byte)(minute % 60); if (tmp_minute >= 60) { tmp_minute = 0; }
            tmp_hour = (byte)(hour % 24); if (tmp_hour >= 24) { tmp_hour = 0; }
            tmp_day = (byte)(day);

            if (formate == t_formate.full_day) //根据输入的时间格式整理日、月、小时、分钟、秒以及毫秒的显示
            {
                temp1 = tmp_day.ToString() + ":";
                if ((tmp_hour < 10) && full) { temp1 += "0"; }//不足10补零
                temp1 += tmp_hour.ToString() + ":";
                if ((tmp_minute < 10) && full) { temp1 += "0"; }
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.full_hour)
            {
                temp1 += tmp_hour.ToString() + ":";
                if ((tmp_minute < 10) && full) { temp1 += "0"; }
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.full_minute)
            {
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.simple_second)
            {
                if ((second < 10) && full) { temp1 = "0"; }
                temp1 += minute.ToString();
                if (ms < 1000) { temp1 += "0"; }//位不足补零
                else if (ms < 100) { temp1 += "00"; }
                else if (ms < 10) { temp1 += "000"; }
                temp1 += "." + tmp_ms.ToString();
            }

            switch (formate)//只输出单独的各个数字
            {
                case t_formate.day: return tmp_day.ToString();
                case t_formate.hour: return tmp_hour.ToString();
                case t_formate.minute: return tmp_minute.ToString();
                case t_formate.second: return tmp_second.ToString();
                case t_formate.ms: return (tmp_ms).ToString();
            }
            return temp1;//返回处理好的格式化时间
        }
    }
}
