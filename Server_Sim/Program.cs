using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server_Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("192.168.1.136");

            TcpListener listener = new TcpListener(address, 5000);

            listener.Start();

            Console.WriteLine("Server started on " + listener.LocalEndpoint);
            Console.WriteLine("Waiting for a connection...");

            Socket socket = listener.AcceptSocket();
            Console.WriteLine("Connection received from " + socket.RemoteEndPoint);

            var stream = new NetworkStream(socket);
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);
            writer.AutoFlush = true;//tự động gửi dữ liệu mà không cần đợi bộ đệm đầy hoặc bạn phải gọi thủ công phương thức Flush()
            while (true)
            {
                // 2. receive
                string str = reader.ReadLine();
                if (str.ToUpper() == "EXIT")// khi nhận exit sẽ trả về bye
                {
                    writer.WriteLine("bye");
                    break;
                }
                // 3. send
                writer.WriteLine("Hello " + str);
            }
        }
    }
}
