using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

// C#网络编程 - Part.3

// 此段代码演示客户端输入一段字符串，然后发往服务端
// 服务端接收到以后，将字符串显示在控制台上，
// 然后将字符串改为大写，再回发给客户端，
// 客户端接收以后将回发的字符串输出在控制台上

// 与此对应的代码为ClientConsole项目中的Client2.cs
// 此时为异步操作 并加入了接收的协议
namespace ServerConsole {
	class Server {
		static void Main(string[] args) {
			Console.WriteLine("Server is running ... ");
			IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
			TcpListener listener = new TcpListener(ip, 8500);

			listener.Start();			// 开始侦听
			Console.WriteLine("Start Listening ...");

			while (true) {
				// 获取一个连接，同步方法，在此处中断
				TcpClient client = listener.AcceptTcpClient();				
				RemoteClient wapper = new RemoteClient(client);
			}
		}
	}

	public class RemoteClient {
		private TcpClient client;
		private NetworkStream streamToClient;
		private const int BufferSize = 8192;
		private byte[] buffer;
		private RequestHandler handler;
		
		public RemoteClient(TcpClient client) {
			this.client = client;

			// 打印连接到的客户端信息
			Console.WriteLine("\nClient Connected！{0} <-- {1}",
				client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

			// 获得流
			streamToClient = client.GetStream();
			buffer = new byte[BufferSize];

			// 设置RequestHandler
			handler = new RequestHandler();

			// 在构造函数中就开始准备读取
			AsyncCallback callBack = ReadComplete;
			streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
		}

		// 再读取完成时进行回调
		private void ReadComplete(IAsyncResult ar) {
			int bytesRead = 0;
			try {
				lock (streamToClient) {
					bytesRead = streamToClient.EndRead(ar);
					Console.WriteLine("Reading data, {0} bytes ...", bytesRead);
				}
				if (bytesRead == 0) throw new Exception("读取到0字节");

				string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
				Array.Clear(buffer,0,buffer.Length);		// 清空缓存，避免脏读
			
				string[] msgArray = handler.GetActualString(msg);	// 获取实际的字符串

				// 遍历获得到的字符串
				foreach (string m in msgArray) {
					Console.WriteLine("Received: {0}", m);
					string back = m.ToUpper();

					// 将得到的字符串改为大写并重新发送
					byte[] temp = Encoding.Unicode.GetBytes(back);
					streamToClient.Write(temp, 0, temp.Length);
					streamToClient.Flush();
					Console.WriteLine("Sent: {0}", back);
				}				

				// 再次调用BeginRead()，完成时调用自身，形成无限循环
				lock (streamToClient) {
					AsyncCallback callBack = new AsyncCallback(ReadComplete);
					streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
				}
			} catch(Exception ex) {
				if(streamToClient!=null)
					streamToClient.Dispose();
				client.Close();
				Console.WriteLine(ex.Message);		// 捕获异常时退出程序				
			}
		}
	}
}
