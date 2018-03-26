using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClientApplication
{
    class Program
    {

        private Boolean isConnected = true;
            
        static void Main(string[] args)
        {
            new Program().Connect();
        }


        private void Connect() 
        {
            try 
            {
                // Initialization (Create and connect)
                TcpClient client = new TcpClient("192.168.43.197", 4200);
                
                // New thread for printing new messages
                NetworkStream stream = client.GetStream();
                Thread t = new Thread((o) =>
                {
                    Reading(client);
                });
                t.Start();
                
                String message = "";

                do
                {
                    // Send operation

                    message = Console.ReadLine();
                    Byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    data = new Byte[256];

                } while (client.Connected && !message.Equals("/exit"));

                isConnected = false;
                // It’s done !
                stream.Close();
                client.Close();
               
               
            } 
            catch (ArgumentNullException e) 
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            } 
            catch (SocketException e) 
            {
                Console.WriteLine("SocketException: {0}", e);
            }
    
        }

        private void Reading(TcpClient client)
        {
            while(client.Connected && isConnected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    Byte[] data = new Byte[256];
                    String responseData = String.Empty;
                    stream.Read(data, 0, data.Length);
                
                    responseData = Encoding.UTF8.GetString(data);

                    Console.WriteLine("Received : " + responseData);

                }
                catch (Exception e)
                {
                    Console.WriteLine("bye");
                }
                

            }
        }
    }
}