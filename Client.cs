using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WpfApp1
{
    class Client
    {
        /// <summary>
        /// The IPAddress associated with the remote machine we are connecting to.
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// The port number associated with the remote machine we are connecting to.
        /// </summary>
        public int Port { get; set; }

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        public Client(IPAddress ipAddress, int port)
        {
            this.IPAddress = ipAddress;
            this.Port = port;
        }
        public void StartClient()
        {
            // Establish the remote endpoint for the socket.
            var remoteEndPoint = new IPEndPoint(this.IPAddress, this.Port);

            // Create a TCP/IP socket.  
            var client = new Socket(this.IPAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.  
            client.BeginConnect(remoteEndPoint,
                ConnectCallback, client);
            connectDone.WaitOne();

            if (!client.Connected)
            {
                // Wait 10 seconds
                Thread.Sleep(10000);
                // Retry to connect to server
                StartClient();
                return;
            }

            // Receive the response from the remote device.  
            Receive(client);
            receiveDone.WaitOne();

            // Release the socket.  
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            var client = (Socket)ar.AsyncState;

            if (!client.Connected)
            {
                return;
            }

            // Complete the connection.  
            client.EndConnect(ar);

            // Signal that the connection has been made.  
            connectDone.Set();
        }

        private void Receive(Socket client)
        {
            // Create the state object.  
            var state = new StateObject();
            state.workSocket = client;

            // Begin receiving the data from the remote device.  
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                ReceiveCallback, state);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the client socket
            // from the asynchronous state object.  
            var state = (StateObject)ar.AsyncState;
            var client = state.workSocket;

            try
            {

                if (!client.Connected)
                {
                    return;
                }

                // Waits for and reads data from the remote device.  
                var bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    var data = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
                    state.sb.Append(data);
                    OnMessageReceived(new MessageRecievedEventArgs()
                    {
                        Message = data,
                        Sender = client
                    });
                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        ReceiveCallback, state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        var response = state.sb.ToString();
                        Console.WriteLine(response);
                    }

                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch
            {
                // Lost connection to the server so we attempt to re-establish connection.
                StartClient();
            }
        }

        public void Send(Socket client, string data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            var byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallback, client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            var client = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            client.EndSend(ar);

            // Signal that all bytes have been sent.  
            sendDone.Set();
        }

        public delegate void MessageRecievedEventHandler(object sender, MessageRecievedEventArgs e);

        public event MessageRecievedEventHandler MessageRecieved;

        public class MessageRecievedEventArgs : EventArgs
        {
            public string Message { get; set; }
            public Socket Sender { get; set; }
        }

        protected virtual void OnMessageReceived(MessageRecievedEventArgs e)
        {
            MessageRecievedEventHandler handler = MessageRecieved;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
