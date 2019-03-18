using ISSTracker.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace ISSTracker.Controller
{
    public class ISSTrackerApi
    {
        const string url = "http://api.open-notify.org/iss-now.json";
        ISSHistory history = new ISSHistory();
        LatLongCalculator calculator = new LatLongCalculator();
        Timer dataTimer = new Timer();

        public string ErrorLogs { get; set; }

        public ISSTrackerApi()
        {
            dataTimer.AutoReset = true;
            dataTimer.Interval = 5000;
            dataTimer.Elapsed += DataTimer_Elapsed;
        }

        internal string UserAnswearInterpreter(string userAnswear)
        {
            string result = "";
            switch (userAnswear)
            {
                case "q":
                    break;
                case "t":
                    StartDataPolling();
                    result = "Rozpoczęto odczyt danych.";
                    break;
                case "s":
                    StopDataPolling();
                    result = "Zakończono odczyt danych.";
                    break;
                case "p":
                    result = LastTwoRecordsToString();
                    break;
                case "c":
                    result = history.Count().ToString();
                    break;
                case "v":
                    try
                    {
                        result = Math.Round(CalculateISSSpeed(), 2).ToString() + " km/s";
                    }
                    catch(InvalidOperationException)
                    {
                        result = "Za mało odczytów do wyznaczenia prędkości.";
                    }
                    break;
                case "d":
                    try
                    {
                        result = Math.Round(CalculateDistanceFromAllRecords(), 2).ToString() + " km";
                    }
                    catch(InvalidOperationException)
                    {
                        result = "Za mało odczytów do wyznaczenia odległości.";
                    }
                    
                    break;
                default:
                    result = "Nierozpoznana komenda.";
                    break;
            }
            return result;
        }

        private string LastTwoRecordsToString()
        {
            string result = "";
            if (history.Count() < 2)
                return "Za mało danych.";
            for(int i = history.Count() - 1; i > history.Count() - 3; i--)
            {
                result += "Czas: " + history[i].Timestamp.EpochToDateTime() + " UTC" + Environment.NewLine;
                result += "Szerokość: " + history[i].ISS_Position.Latitude.ToString() + Environment.NewLine;
                result += "Długość: " + history[i].ISS_Position.Longitude.ToString() + Environment.NewLine;
                result += Environment.NewLine;
            }
            return result;
        }

        internal string ShowMenu()
        {
            string menu = Environment.NewLine;
            menu += "######################################################################" + Environment.NewLine;
            menu += Environment.NewLine;
            menu += "\tProgram ISSTracker by mchmielo." + Environment.NewLine;
            menu += "\tProgram służy do przedstawienia funkcjonalności API" + Environment.NewLine;
            menu += "\tśledzenia położenia Międzynarodowej Stacji Kosmicznej ISS." + Environment.NewLine;
            menu += "\tDostępne opcje:" + Environment.NewLine;
            menu += "\tq - wyjdź," + Environment.NewLine;
            menu += "\tt - rozpocznij automatyczny odczyt położenia ISS," + Environment.NewLine;
            menu += "\ts - zakończ automatyczny odczyt położenia ISS," + Environment.NewLine;
            menu += "\tp - pokaż 2 ostatnie rekordy," + Environment.NewLine;
            menu += "\tc - pokaż liczbę wszystkich rekordów" + Environment.NewLine;
            menu += "\tv - pokaż najbardziej aktualną prędkość ISS," + Environment.NewLine;
            menu += "\td - przebytą odległość od początku odczytów." + Environment.NewLine;
            menu += Environment.NewLine;
            menu += "######################################################################" + Environment.NewLine;
            menu += Environment.NewLine;
            return menu;
        }

        private async void DataTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await AddRecord();
            }
            catch(HttpRequestException hre)
            {   
                ErrorLogs = hre.Message;
            }
        }

        public int RecordCount()
        {
            return history.Count();
        }

        public double CalculateISSSpeed()
        {
            if (!history.IsMoreThanOneRecord())
            {
                throw new InvalidOperationException();
            }
            return calculator.CalculateSpeed(history[history.Count() - 2], history[history.Count() - 1]);
        }

        public double CalculateDistanceFromAllRecords()
        {
            if (!history.IsMoreThanOneRecord())
            {
                throw new InvalidOperationException();
            }
            double distance = 0;
            for(int i = 0; i < history.Count() - 1; i++)
            {
                distance += calculator.CalculatePath(history[i].ISS_Position, history[i + 1].ISS_Position);
            }
            return distance;
        }

        public async Task AddRecord()
        {
            ISSPosition position = await DataReceiver.GetISSPositionAsync(url);
            history.AddRecord(position);
        }

        public ISSPosition GetRecord(int index)
        {
            return history[index];
        }

        public void StartDataPolling()
        {
            dataTimer.Enabled = true;
        }

        public void StopDataPolling()
        {
            dataTimer.Enabled = false;
        }
    }
}
