using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Redis;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {

        public class User
        {
            public string Name { get; set; }

            public string Password { get; set; }
        }

        [TestMethod]


        public void TestMethod1()
        {
            var redisClient = new RedisClient("10.1.200.83", 6379);

             var redisUser= redisClient.As<User>();

            redisUser.Store(new User()
            {
                Name = "gu yang",
                Password = "redis use"

            });

            Console.WriteLine(redisClient.Get<string>("tongcheng"));
            Console.WriteLine("Test");
            var orderRedis = redisClient.As<string>();

            orderRedis.Store("yang");
            orderRedis.GetAll();
            Assert.Equals(redisClient.Get<string>("city"), "suzhou");
        }
    }
}
