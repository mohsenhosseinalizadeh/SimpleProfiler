using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleProfiler
{
    public static class SimpleProfiler
    {
        public const string __SimpleProfilerKey = "SimpleProfiler";
        public static Dictionary<string, Stopwatch> _profilerDic
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(__SimpleProfilerKey))
                {
                    var newprofiledic = new Dictionary<string, Stopwatch>() { };
                    HttpContext.Current.Items.Add(__SimpleProfilerKey, newprofiledic);
                }

                return (Dictionary<string, Stopwatch>)HttpContext.Current.Items[__SimpleProfilerKey];
            }
        }
        public static void Start(string key)
        {
            var watch = getWatch(key, _profilerDic);
            watch.Start();
        }

        public static void Stop(string key)
        {
            var watch = getWatch(key, _profilerDic);
            watch.Stop();
        }

        public static long ElaplsedMiliSecond(string key)
        {
            var watch = getWatch(key, _profilerDic);
            return watch.ElapsedMilliseconds;
        }

        private static Stopwatch getWatch(string key, Dictionary<string, Stopwatch> profilerDic)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Please provide a key.");

            if (!profilerDic.ContainsKey(key))
                profilerDic.Add(key, new Stopwatch());

            return (Stopwatch)profilerDic[key];
        }
    }
}
