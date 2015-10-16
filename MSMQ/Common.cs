using System;

namespace Msmq.PerfermanceTest
{
    public class Common
    {
        public static MessageObj GetMessageObj(string mediaTaskId)
        {
            var messageObj = new MessageObj
                                 {
                                     MonthDay = DateTime.Now.ToString("MMdd"),
                                     MediaTaskId = mediaTaskId,
                                     MediaType = "1014002",
                                     Priority = "High",
                                     SendNo = "125829901",
                                     Subject = "彩信主题",
                                     MobileNo = "13810494631",
                                     Content =
                                         "先说一下公共队列和专用队列，公共队列只能建立在域中，或者说建立在活动目录中，而工作组的计算机只能创建专用队列。如上的路径方式，所以在工作组模式下，不支持远程计算机执行此方法，而专有队列应该在远程计算机上是无法创建的。",
                                     ContentType = "1028001",
                                     AccessoryType = "1011004",
                                     AccessoryInfo = "ftp://127.0.0.1/slave/aaaaaaa.zip",
                                     CreateTime = DateTime.Now.ToString(),
                                     StatusFlag = "1013004",
                                     ServiceId = "wwww.baidu.com",
                                     FeeAddr = "AAAAAAAAAAAA",
                                     Reserve3 = "100000000000",
                                     BatchId = Guid.NewGuid().ToString()
                                 };
            return messageObj;
        }
    }
}
