using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace RedisSample
{
    class Program
    {
        public class User
        {
            public string Name { get; set; }

            public string Password { get; set; }
        }
        static void Main(string[] args)
        {
            try
            {
                var redisClient = new RedisClient("10.1.200.83", 6379);
                Console.WriteLine(redisClient.Get<string>("city"));
                Console.WriteLine(redisClient.Get<string>("tongcheng"));
                Console.WriteLine("Test");
                var redisUser = redisClient.As<User>();

                redisUser.Store(new User()
                {
                    Name = "gu yang",
                    Password = "redis use"

                });

                var user = redisUser.GetAll();
                Console.WriteLine(user.First().Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
