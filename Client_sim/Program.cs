using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_sim
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            // 1. connect
            client.Connect("192.168.1.136", 5000);
            Stream stream = client.GetStream();

            Console.WriteLine("Connected to Server_sim");
            while (true)
            {
                Console.Write("'Exit to exit' ");
                Console.Write("Enter your name: ");

                string str = Console.ReadLine();
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                // 2. send
                writer.WriteLine(str);

                // 3. receive
                str = reader.ReadLine();
                Console.WriteLine(str);
                if (str.ToUpper() == "BYE")// nhận bye sẽ ngắt vòng lập
                    break;
            }
            // 4. close
            stream.Close();
            client.Close();
        }
    }
}
