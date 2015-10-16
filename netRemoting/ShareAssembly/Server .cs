using System;
using System.Runtime.CompilerServices;
using ShareAssembly;

public delegate void NumberChangedEventHandler(string name, int count);

public class Server : MarshalByRefObject
{
    private int count = 0;
    private const string serverName = "SimpleServer";

    public event NumberChangedEventHandler NumberChanged;

    // 触发事件，调用客户端方法
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DoSomething()
    {
        // 做某些额外方法
        count++;
        if (NumberChanged != null)
        {
            Delegate[] delArray = NumberChanged.GetInvocationList();
            foreach (Delegate del in delArray)
            {
                NumberChangedEventHandler method = (NumberChangedEventHandler)del;
                try
                {
                    method(serverName, count);
                }
                catch
                {
                    Delegate.Remove(NumberChanged, del);//取消某一客户端的订阅
                }
            }
        }
    }

    // 直接调用客户端方法
    public void InvokeClient(Client remoteClient, int x, int y)
    {
        int total = remoteClient.Add(x, y); //方法回调
        Console.WriteLine(
            "Invoke client method: x={0}, y={1}, total={2}", x, y, total);
    }

    // 调用客户端属性
    public void GetCount(Client remoteClient)
    {
        Console.WriteLine("Count value from client: {0}", remoteClient.Count);
    }
}