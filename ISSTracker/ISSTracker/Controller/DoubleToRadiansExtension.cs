using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSTracker.Controller
{
    public static class DoubleToRadiansExtension
    {
        public static double ToRadians(this double value)
        {
            return (Math.PI / 180) * value;
        }
    }
}
