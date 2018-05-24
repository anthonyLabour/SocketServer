using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SocketServer
{
    class Program
    {
        public static String message = null;

        static void Main(string[] args)
        {
            Byte[] bytes = new Byte[1024];


            IPHostEntry myHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress myAddress = myHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(myAddress, 11000);

            Socket listener = new Socket(myAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(5);

                while(true)
                {

                    Console.WriteLine("Esperando Conexion de cliente");

                    Socket manejadorRequest = listener.Accept();

                    message = null;

                    while(true)
                    {
                        int bytesRecibidos = manejadorRequest.Receive(bytes);
                       message += Encoding.ASCII.GetString(bytes, 0, bytesRecibidos);

                       

                        if(message == "" )
                        {

                            break;

                        }
                        else
                        {
                            Console.WriteLine("Usuario dijo: {0}", message);

                            message += "Gracias por el mensaje";

                            Byte[] mensajeDeRetornos = Encoding.ASCII.GetBytes(message);

                            manejadorRequest.Send(mensajeDeRetornos);
                            //manejadorRequest.Shutdown(SocketShutdown.Both);
                            //manejadorRequest.Close();


                        }

                    }

    //                Console.WriteLine("Usuario dijo: {0}", message);

  //                  message += "Gracias por el mensaje";

//                    Byte[] mensajeDeRetornos = Encoding.ASCII.GetBytes(message);

                    //manejadorRequest.Send(mensajeDeRetornos);
                    //manejadorRequest.Shutdown(SocketShutdown.Both);
                    //manejadorRequest.Close();

                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }
    }
}
