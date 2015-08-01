using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SleepControll_Lib.Sockets
{

    public class SynchronousSocketClient
    {


        public static Socket createClientSocket()
        {
            // Establish the remote endpoint for the socket.
            // This example uses port 11000 on the local computer.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);
            //Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
            return sender;
        }

        public static void sendData(Socket sender, String data)
        {
            

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(data);

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
        }

        public static string revieveResponde(Socket sender)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];


            // Receive the response from the remote device.
            int bytesRec = sender.Receive(bytes);

            string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

           

            return response;

        }

        public static void closeCOnnection(Socket sender)
        {
            // Release the socket.
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        public static void StartClient()
        {
            

            // Connect to a remote device.
            try
            {
                for (int i = 0; i < 80000; i++)
                {
                Socket sender = createClientSocket();
                string data = "This is a test<EOF>";

                sendData(sender, data);
                revieveResponde(sender);

                closeCOnnection(sender);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

}