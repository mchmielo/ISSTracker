using ISSTracker.Controller;
using System;
using System.Threading.Tasks;

namespace ISSTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            ISSTrackerApi api = new ISSTrackerApi();
            string userAnswear = "";
            bool firstRun = true;
            while(userAnswear != "q")
            {
                if (!firstRun)
                {
                    Console.Clear();
                    if (api.ErrorLogs != "")
                    {
                        Console.WriteLine(api.ErrorLogs + Environment.NewLine);
                        api.ErrorLogs = "";
                    }
                    Console.WriteLine(api.UserAnswearInterpreter(userAnswear));
                }
                firstRun = false;
                Console.Write(api.ShowMenu());
                userAnswear = Console.ReadLine();
                
            }

        }
    }
}
