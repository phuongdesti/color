using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Color.Models
{
    public class Current
    {
        public const string TimeStamp = "yyyy.MM.dd HH-mm-ss";
        public static string DataFolder()
        {

            return HttpRuntime.AppDomainAppPath  +"data\\";
        }
    }
}