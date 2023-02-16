using SweatySenpaiBot.Content;

namespace SweatySenpaiBot.Test
{
    [TestClass]
    public class HttpContentProviderTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var timeout = TimeSpan.FromSeconds(10);
            var url = @"http://www.google.com/aha";
            var instance = new HttpContentProvider(timeout);
            var result = await instance.GetContentAsync(url);
            Console.WriteLine(result.Length);
        }
    }
}