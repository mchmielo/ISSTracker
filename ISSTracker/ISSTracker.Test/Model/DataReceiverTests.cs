using NUnit.Framework;
using ISSTracker.Model;
using System.Threading.Tasks;

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
    }
}
