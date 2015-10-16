using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


// 此段演示客户端输入一段字符串，然后发往服务端
// 服务端接收到以后，将字符串显示在控制台上
// 与此对应的代码为ServerConsole项目中的Server1.cs

// 注意：演示字符串的异常 -- 服务端将客户端的多次请求合并为了一个请求
namespace ClientConsole {
	class Client {
		static void Main(string[] args) {
			Console.WriteLine("Client is running ...");
			TcpClient client;
			ConsoleKey key;

			try {
				client = new TcpClient();
				client.Connect("localhost", 8500);		// 与服务器连接
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				return;
			}
			// 打印连接到的服务端信息
			Console.WriteLine("Server Connected！{0} --> {1}",
				client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

			NetworkStream streamToServer = client.GetStream();
			//Console.WriteLine("Menu: S - Send, X - Exit");

			string msg = "Welcome to TraceFact.Net!";
			msg = String.Format("[length={0}]{1}", msg.Length, msg);

			for (int i = 0; i <= 2; i++) {
				byte[] buffer = Encoding.Unicode.GetBytes(msg);		// 获得缓存
				try {
					streamToServer.Write(buffer, 0, buffer.Length);		// 发往服务器
					Console.WriteLine("Sent: {0}", msg);
				} catch (Exception ex) {
					Console.WriteLine(ex.Message);
					break;
				}
			}

			Console.WriteLine("\n\n输入\"Q\"键退出。");
			do {
				key = Console.ReadKey(true).Key;
			} while (key != ConsoleKey.Q);

			streamToServer.Dispose();
			client.Close();
		}
	}
}
