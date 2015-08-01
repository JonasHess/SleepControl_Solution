using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SleepControll_Lib.Sockets
{
    public class SynchronousSocketListener
    {

        // Incoming data from the client.
        public static string data = null;

        public static Socket setupListener()
        {
            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            return listener;
        }

        public static void handleConnection(Socket listener)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            
            // Program is suspended while waiting for an incoming connection.
            Socket handler = listener.Accept();
            data = null;

            // An incoming connection needs to be processed.
            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }

            // Show the data on the console.
            Console.WriteLine("Server: Text received : {0}", data);

            string response = getRespondeMessage(data);


            // Echo the data back to the client.
            byte[] msg = Encoding.ASCII.GetBytes(response);
          
            handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    
        public static string getRespondeMessage(string revievedData)
        {
            return revievedData;
        }

        public static void StartListening()
        {
            

            Socket listener = setupListener();
            try
            {


                // Start listening for connections.
                while (true)
                {
                    handleConnection(listener);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
    }
    }
