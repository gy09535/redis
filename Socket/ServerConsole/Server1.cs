using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

// C#网络编程 - Part.3

// 此段演示客户端输入一段字符串，然后发往服务端
// 服务端接收到以后，将字符串显示在控制台上
// 与此对应的代码为ClientConsole项目中的Client1.cs

// 注意：演示字符串的异常 -- 服务端将客户端的多次请求合并为了一个请求
// 加入了RequestHandler对字符串进行处理
namespace ServerConsole {
	class Server {
		static void Main(string[] args) {
			const int BufferSize = 8192;	// 缓存大小，8192Bytes
			ConsoleKey key;

			Console.WriteLine("Server is running ... ");
			IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
			TcpListener listener = new TcpListener(ip, 8500);

			listener.Start();			// 开始侦听
			Console.WriteLine("Start Listening ...");


			// 获取一个连接，中断方法
			TcpClient remoteClient = listener.AcceptTcpClient();

			// 打印连接到的客户端信息
			Console.WriteLine("Client Connected！{0} <-- {1}",
				remoteClient.Client.LocalEndPoint, remoteClient.Client.RemoteEndPoint);

			// 获得流
			NetworkStream streamToClient = remoteClient.GetStream();
			RequestHandler handler = new RequestHandler();

			do {
				// 写入buffer中
				byte[] buffer = new byte[BufferSize];
				int bytesRead;
				try {
					bytesRead = streamToClient.Read(buffer, 0, BufferSize);
					if (bytesRead == 0) throw new Exception("读取到0字节");
				} catch (Exception ex) {					
					Console.WriteLine(ex.Message);
					break;
				}
				
				Console.WriteLine("Reading data, {0} bytes ...", bytesRead);
				// 获得请求的字符串
				string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);

				string[] msgArray = handler.GetActualString(msg);
				foreach (string m in msgArray) {
					Console.WriteLine("Received: {0}", m);
				}
			} while (true);

			streamToClient.Dispose();
			remoteClient.Close();
			
			Console.WriteLine("\n\n输入\"Q\"键退出。");
			do {
				key = Console.ReadKey(true).Key;
			} while (key != ConsoleKey.Q);
		}
	}
}
