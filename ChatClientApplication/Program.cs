using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;

namespace ChatClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            String message;
            new Program().Connect();
        }


        private void Connect() 
        {
            try 
            {

                // Initialization (Create and connect)
                TcpClient client = new TcpClient("127.0.0.1", 4200);
                NetworkStream stream = client.GetStream();
                new Thread((o) => { Reading(stream,client); });
                String message = "";
                
                while(client.Available>0 || message.Equals("/exit"))
                {
                    // Send operation
                    
                    message = Console.ReadLine();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                   
                }
                
               
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

        private void Reading(NetworkStream stream,TcpClient client)
        {
            while(client.Available>0)
            {
                Byte[] data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received : " + responseData);
            }
        }

    }
}