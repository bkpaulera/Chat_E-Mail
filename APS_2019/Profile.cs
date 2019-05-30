using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace APS_2019
{
    class Profile
    {
        //Criando os objetos que serão utilizados na parte de e-mail e login da aplicação
        private String user = "rjurema407@gmail.com";
      
        private String username = "Jurema Ribeiro";

        private String password = "12345";
        private String Email_HOST = "smtp.gmail.com";
        private int Email_PORT = 587;

        //Criando os objetos que serão ultilizados na parde de chat da aplicação
        private int port = 7777;
        private IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());
        private TcpClient client;
        private StreamReader sTR;
        private StreamWriter sTW;
        private string recieve;
        private String textToSend;
        private IPEndPoint endPoint;
        private Boolean tokien;
        private TcpListener listener;
        
        //Aqui também devem ser definidas as informações de sockets e tcp ip para utilização no chat

        //Encapsulamento dos objetos para acessibilidade nas outras classes
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email_HOST1 { get => Email_HOST; set => Email_HOST = value; }
        public int Email_PORT1 { get => Email_PORT; set => Email_PORT = value; }
        public string User { get => user; set => user = value; }
        public int Port { get => port; set => port = value; }
        public IPAddress[] LocalIp { get => localIp; set => localIp = value; }
        public TcpClient Client { get => client; set => client = value; }
        public StreamReader STR { get => sTR; set => sTR = value; }
        public StreamWriter STW { get => sTW; set => sTW = value; }
        public string Recieve { get => recieve; set => recieve = value; }
        public string TextToSend { get => textToSend; set => textToSend = value; }
        public IPEndPoint EndPoint { get => endPoint; set => endPoint = value; }
        public bool Tokien { get => tokien; set => tokien = value; }
        public TcpListener Listener { get => listener; set => listener = value; }
    }
}
