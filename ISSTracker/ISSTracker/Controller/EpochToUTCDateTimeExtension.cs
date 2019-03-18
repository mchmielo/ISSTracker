using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSTracker.Controller
{
    public static class EpochToUTCDateTimeExtension
    {
        public static DateTime EpochToDateTime(this int timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
        }
    }
}
