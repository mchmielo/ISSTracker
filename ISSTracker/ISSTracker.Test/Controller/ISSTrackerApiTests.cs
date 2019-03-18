using System;
using NUnit.Framework;
using ISSTracker.Model;
using ISSTracker.Controller;
using System.Threading.Tasks;
using System.Net.Http;

namespace ISSTracker.Test.Controller
{
    [TestFixture]
    class ISSTrackerApiTests
    {
        [Test]
        public async Task AddRecord_CorrectRecordAdded()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            await api.AddRecord();
            Assert.AreEqual(1, api.RecordCount());
        }

        [Test]
        public void AddRecord_NoConnection_Throws()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            Assert.ThrowsAsync<HttpRequestException>(async ()=>await api.AddRecord());
        }

        [Test]
        public void CalculateSpeed_LessThan2Records_Throws()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            Assert.Throws<InvalidOperationException>(() => api.CalculateISSSpeed());
        }

        [Test]
        public void CalculateSpeed_ReturnValue()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            
            Assert.Throws<InvalidOperationException>(() => api.CalculateISSSpeed());
        }

        [Test]
        public void CalculateDistance_LessThan2Records_Throws()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            Assert.Throws<InvalidOperationException>(() => api.CalculateISSSpeed());
        }

        [Test]
        public async Task CalculateDistance_ReturnValue()
        {
            ISSTrackerApi api = new ISSTrackerApi();
            await api.AddRecord();
            await api.AddRecord();
            Assert.AreNotEqual(0, api.CalculateDistanceFromAllRecords());
        }
    }
}
