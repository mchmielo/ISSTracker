using NUnit.Framework;
using ISSTracker.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace ISSTracker.Test.Model
{
    [TestFixture]
    class DataReceiverTests
    {
        [Test]
        public async Task GetDataAsync_GetData()
        {
            ISSPosition position = await DataReceiver.GetISSPositionAsync("http://api.open-notify.org/iss-now.json");
            Assert.AreEqual("success", position.Message);
        }

        [Test]
        public void GetDataAsync_NoConnection_Throws()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await DataReceiver.GetISSPositionAsync("http://api.open-notify.org/iss-now.json"));
        }
        [Test]
        public void GetDataAsync_NullArgument_Throws()
        {
            Assert.ThrowsAsync<InvalidOperationException>( async () => await DataReceiver.GetISSPositionAsync(null));
        }
    }
}
