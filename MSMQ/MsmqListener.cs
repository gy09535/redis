using System;
using System.Diagnostics;
using System.Messaging;

namespace Msmq.PerfermanceTest
{
    public class MsmqListener
    {
        private bool _listen;
        private MessageQueue _queue;

        public event MessageReceivedEventHandler MessageReceived;

        public MsmqListener(string queuePath, XmlMessageFormatter xmlMessageFormatter)
        {
            _queue = new MessageQueue(queuePath) {Formatter = xmlMessageFormatter};
        }

        public void Start()
        {
            _listen = true;

            _queue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);

            StartListening();
        }

        public void Stop()
        {
            _listen = false;
            _queue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);
        }

        private void StartListening()
        {
            if (!_listen)
            {
                return;
            }

            // 异步接收BeginReceive()方法无MessageQueueTransaction重载(微软类库的Bug?)
            // 这里变通一下：先异步BeginPeek()，然后带事务异步接收Receive(MessageQueueTransaction)
            if (_queue.Transactional)
            {
                _queue.BeginPeek();
            }
            else
            {
                _queue.BeginReceive();
            }
        }

        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            _queue.EndPeek(e.AsyncResult);
            var trans = new MessageQueueTransaction();
            Message msg = null;
            try
            {
                trans.Begin();
                msg = _queue.Receive(trans);
                trans.Commit();

                StartListening();

                FireRecieveEvent(msg.Body);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                trans.Abort();
            }
        }

        private void FireRecieveEvent(object body)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, new MessageEventArgs(body));
            }
        }

        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = _queue.EndReceive(e.AsyncResult);

            StartListening();

            FireRecieveEvent(msg.Body);
        }
    }

    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);
    
    public class MessageEventArgs : EventArgs
    {
        private object _messageBody;

        public object MessageBody
        {
            get { return _messageBody; }
        }

        public MessageEventArgs(object body)
        {
            _messageBody = body;
        }
    }
}
