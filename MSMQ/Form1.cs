using System;
using System.Configuration;
using System.Diagnostics;
using System.Messaging;
using System.Threading;
using System.Windows.Forms;
using Message = System.Messaging.Message;

namespace Msmq.PerfermanceTest
{
    public partial class Form1 : Form
    {
        private MessageQueue _queue;
        private MessageQueue _transactionalQueue;
        private MsmqListener _listener;
        private readonly string _queuePath = ConfigurationManager.AppSettings["MqPath"];
        private readonly string _transactionalQueuePath = ConfigurationManager.AppSettings["TransactionalQueuePath"];
        private long _messageCount = long.Parse(ConfigurationManager.AppSettings["MessageCount"]);

        public Form1()
        {
            InitializeComponent();
        }

        private MessageQueue Queue
        {
            get
            {
                if (_queue == null)
                {
                    _queue = new MessageQueue(_queuePath)
                    {
                        Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageObj) })
                    };

                    Debug.WriteLine("Connect To " + _queuePath + " Done!");
                }

                return _queue;
            }
        }

        private MessageQueue TransactionalQueue
        {
            get
            {
                if (_transactionalQueue == null)
                {
                    _transactionalQueue = new MessageQueue(_transactionalQueuePath)
                    {
                        Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageObj) })
                    };

                    Debug.WriteLine("Connect To " + _transactionalQueuePath + " Done!");
                }

                return _transactionalQueue;
            }
        }

        /// <summary>
        /// 如果要多线程监听，请自己加double check锁
        /// </summary>
        private MsmqListener Listener
        {
            get
            {
                if (_listener == null)
                {
                    _listener = new MsmqListener(_transactionalQueuePath, new XmlMessageFormatter(new Type[] { typeof(MessageObj) }));
                }

                return _listener;
            }
        }

        private void btnSend_Click(object sender, System.EventArgs e)
        {
            var threadStart = new ThreadStart(Send);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        private void btnReceive_Click(object sender, System.EventArgs e)
        {
            var threadStart = new ThreadStart(Receive);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// Send
        /// </summary>
        private void Send()
        {

            var mediaTaskId = 100000000000000;
            for (var i = 1; i <= _messageCount; i++)
            {
                mediaTaskId++;
                var message = new Message(Common.GetMessageObj(mediaTaskId.ToString()));
                try
                {
                    Queue.Send(message);
                    Debug.WriteLine("Send Message" + i + " Done!(" + mediaTaskId + ")");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception:SendMessage Exception:" + ex);
                }
            }

            MessageBox.Show("Send Done!");
        }

        /// <summary>
        /// Receive
        /// </summary>
        private void Receive()
        {

            var queue = new MessageQueue(@".\private$\MSMQDemo")
            {
                Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageObj) })
            };
            using (var messageEnumerator = queue.GetMessageEnumerator2())
            {
                var index = 1;
                while (messageEnumerator.MoveNext())
                {
                    Message message = null;
                    try
                    {
                        message = queue.Receive();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
                    }

                    if (message != null)
                    {
                        var messageObj = (MessageObj)message.Body;
                        Debug.WriteLine("Receive Message" + index + " Done!(" + messageObj.MediaTaskId + ")");
                    }

                    index++;
                }
            }

            MessageBox.Show("Receive Done!");
        }

        private void btnSendWithTracnsactation_Click(object sender, EventArgs e)
        {
            var threadStart = new ThreadStart(SendWithTransactional);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        private void btnReceiveWithTracnsactation_Click(object sender, EventArgs e)
        {
            var threadStart = new ThreadStart(ReceiveWithTransactional);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// SendWithTransactional
        /// </summary>
        private void SendWithTransactional()
        {
            if (!TransactionalQueue.Transactional)
            {
                Debug.WriteLine("TransactionalQueue.Transactional Is False!");
                return;
            }

            //lblBeginTime.Text = DateTime.Now.ToString();
            var mediaTaskId = 100000000000000;
            for (var i = 1; i <= _messageCount; i++)
            {
                mediaTaskId++;
                var messageQueueTransaction = new MessageQueueTransaction();
                var message = new Message(Common.GetMessageObj(mediaTaskId.ToString()));

                try
                {
                    messageQueueTransaction.Begin();
                    TransactionalQueue.Send(message, messageQueueTransaction);
                    messageQueueTransaction.Commit();
                    Debug.WriteLine("Send Message" + i + " Done!(" + mediaTaskId + ")");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception:SendMessage Exception:" + ex);
                }
            }

            // lblEndTime.Text = DateTime.Now.ToString();
            MessageBox.Show("Send Done!");
        }

        /// <summary>
        /// ReceiveWithTransactional
        /// </summary>
        private void ReceiveWithTransactional()
        {
            if (!TransactionalQueue.Transactional)
            {
                Debug.WriteLine("TransactionalQueue.Transactional Is False!");
                return;
            }

            lblBeginTime.Text = DateTime.Now.ToString();
            using (var messageEnumerator = TransactionalQueue.GetMessageEnumerator2())
            {
                var index = 1;
                Message message = null;
                while (messageEnumerator.MoveNext())
                {
                    var messageQueueTransaction = new MessageQueueTransaction();
                    try
                    {
                        messageQueueTransaction.Begin();
                        message = TransactionalQueue.Receive(messageQueueTransaction);
                        messageQueueTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        messageQueueTransaction.Abort();
                        Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
                    }

                    if (message != null)
                    {
                        var messageObj = (MessageObj)message.Body;
                        Debug.WriteLine("Receive Message" + index + " Done!(" + messageObj.MediaTaskId + ")");
                    }

                    index++;
                }
            }

            // lblEndTime.Text = DateTime.Now.ToString();
            MessageBox.Show("Receive Done!");
        }

        private void btnAsyncReceive_Click(object sender, EventArgs e)
        {
            var threadStart = new ThreadStart(AsyncReceive);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// AsyncReceive
        /// </summary>
        private void AsyncReceive()
        {
            // lblBeginTime.Text = DateTime.Now.ToString();
            var messageQueue = new MessageQueue(_queuePath)
            {
                Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageObj) })
            };
            messageQueue.ReceiveCompleted += ReceiveCompleted;
            using (var messageEnumerator = messageQueue.GetMessageEnumerator2())
            {
                while (messageEnumerator.MoveNext())
                {
                    try
                    {
                        messageQueue.BeginReceive();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
                    }
                }
            }

            // lblEndTime.Text = DateTime.Now.ToString();
            MessageBox.Show("Receive Done!");
        }

        /// <summary>
        /// ReceiveCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var messageQueue = (MessageQueue)sender;
                var message = messageQueue.EndReceive(e.AsyncResult);
                var messageObj = (MessageObj)message.Body;

                Debug.WriteLine("Receive Message Done!(" + messageObj.MediaTaskId + ")");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
            }
        }

        private void btnAsyncReceiveWithTracnsactation_Click(object sender, EventArgs e)
        {
            var threadStart = new ThreadStart(AsyncReceiveWithTransactional);
            var thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// 异步接收事务消息队列消息
        /// 异步接收BeginReceive()方法无MessageQueueTransaction重载(微软类库的Bug?)
        /// 这里变通一下：先异步BeginPeek()，然后带事务异步接收Receive(MessageQueueTransaction)
        /// </summary>
        private void AsyncReceiveWithTransactional()
        {
            var messageQueue = new MessageQueue(_transactionalQueuePath)
            {
                Formatter = new XmlMessageFormatter(new Type[] { typeof(MessageObj) })
            };
            messageQueue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);

            //lblBeginTime.Text = DateTime.Now.ToString();
            using (var messageEnumerator = messageQueue.GetMessageEnumerator2())
            {
                while (messageEnumerator.MoveNext())
                {
                    try
                    {
                        messageQueue.BeginPeek();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
                    }
                }
            }

            //lblEndTime.Text = DateTime.Now.ToString();
            MessageBox.Show("Receive Done!");
        }

        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            Message message = null;
            var messageQueueTransaction = new MessageQueueTransaction();
            try
            {
                var messageQueue = (MessageQueue)sender;
                messageQueue.EndPeek(e.AsyncResult);

                messageQueueTransaction.Begin();
                message = messageQueue.Receive(messageQueueTransaction);
                messageQueueTransaction.Commit();
            }
            catch (Exception ex)
            {
                messageQueueTransaction.Abort();
                Debug.WriteLine("Exception:ReceiveMessage Exception:" + ex);
            }

            if (message != null)
            {
                var messageObj = (MessageObj)message.Body;
                Debug.WriteLine("Async Receive Message Done!(" + messageObj.MediaTaskId + ")");
            }
        }

        private void btnQueueListener_Click(object sender, EventArgs e)
        {
            Listener.MessageReceived += new MessageReceivedEventHandler(MsmqListener_MessageReceived);
            Listener.Start();
        }

        private void MsmqListener_MessageReceived(object sender, MessageEventArgs args)
        {
            var message = args.MessageBody;
            if (message == null)
            {
                return;
            }

            var messageObj = (MessageObj)message;
            Debug.WriteLine("MsmqListener_MessageReceived Done!(" + messageObj.MediaTaskId + ")");
        }

        private void btnStopQueueListener_Click(object sender, EventArgs e)
        {
            Listener.Stop();
        }
    }
}
