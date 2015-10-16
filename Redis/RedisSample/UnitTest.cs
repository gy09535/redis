using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Redis;

namespace RedisSample
{

    [TestClass]
    class UnitTest
    {
        [TestMethod]
        public void TestRedis()
        {

            var redisClient = new RedisClient("10.1.200.83", 6379);

            Console.WriteLine(redisClient.Get<string>("tongcheng"));
            Console.WriteLine("Test");


            Assert.Equals(redisClient.Get<string>("city"), "suzhou");
        }
    }
}
