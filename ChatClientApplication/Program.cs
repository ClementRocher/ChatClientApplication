using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace ChatClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            String message;
            do
            {
                message = Console.ReadLine();
                Connect(message);
            } while (message != "/exit");
        }
        
        static void Connect(String message) 
        {
            try 
            {

                // Initialization (Create and connect)
                TcpClient client = new TcpClient("127.0.0.1", 4200);
                NetworkStream stream = client.GetStream();
                // Send operation
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                // Receive operation
                data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received : " + responseData);
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
    
            Console.WriteLine("\n Press Enter to continue...");
            //Console.Read();
        }

    }
}