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
                Int32 port = 4200;
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                Socket client = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ipep);
               
    
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);         

                // Get a client stream for reading and writing.
                //byte[] data = new byte[1024];
                int sent = client.Send(data);
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
            Console.Read();
        }

    }
}