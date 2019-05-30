using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_2019
{
    public partial class Chat : Form
    {
        Menu menu = new Menu();
        Profile userProfile = new Profile();

        public Chat()
        {
            InitializeComponent();
            //Verifica de o ip faz parte da familia TcpIp 

            foreach (IPAddress address in userProfile.LocalIp)
            {
                //Adiciona  o ip e uma porta aos campos de IP e Porta
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox_IP.Text = address.ToString();
                    textBox_Port.Text = userProfile.Port.ToString();
                }


            }

        }

        private void ButtonVoltar_Click(object sender, EventArgs e)
        {      
            menu.Show();
            Hide();
        }

        private void ButtonConectar_Click(object sender, EventArgs e)
        {

            //Seta o ip e a porta com quem o cliente irá inicar uma conexão
            userProfile.Client = new TcpClient();
            userProfile.EndPoint = new IPEndPoint(IPAddress.Parse(textBox_IP.Text), int.Parse(textBox_Port.Text));

            try
            {
                //Estabelece uma coneção
                userProfile.Client.Connect(userProfile.EndPoint);

                if (userProfile.Client.Connected)
                {
                    //Adiciona um texto de sucesso ao textbox 
                    //ChatScreentextBox.AppendText("\n Coneção Estabelecida \n");
                    userProfile.STR = new StreamReader(userProfile.Client.GetStream());
                    userProfile.STW = new StreamWriter(userProfile.Client.GetStream());
                    userProfile.STW.AutoFlush = true;
                    // Inicia o uma execução em segundo plano 
                    backgroundWorkerRecebe.RunWorkerAsync();
                    backgroundWorkerEnvia.WorkerSupportsCancellation = true;
                }


            }
            catch (Exception ex)
            {
                //ChatScreentextBox.AppendText("\n Falha em estabelecer a Conexão : \t\n" + ex);
                MessageBox.Show("Falha em estabelecer a conexão : \n" + ex);
            }

        }
        //Solicita o envio da menssagem para o  Servidor ou o Cliente
        private void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (MessagetextBox.Text != "")
            {
                //TextToSend = MessagetextBox.Text;
                userProfile.TextToSend = MessagetextBox.Text;
                backgroundWorkerEnvia.RunWorkerAsync();

            }
            MessagetextBox.Text = "";
        }
        //Recebe menssagem e Exibe 
        private void BackgroundWorkerRecebe_DoWork(object sender, DoWorkEventArgs e)
        {

            while (userProfile.Client.Connected)
            {
                try
                {
                    userProfile.Recieve = userProfile.STR.ReadLine();
                    this.ChatScreentextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        ChatScreentextBox.AppendText("Recebido " + userProfile.Recieve + "\n");
                    }));
                    userProfile.Recieve = "";
                }
                catch (Exception ex)
                {
                    //ChatScreentextBox.AppendText("Messagem não enviada");
                    MessageBox.Show("Erro : \n " + ex);
                }
            }
        }
        //Envia Menssagem e exibe
        private void BackgroundWorkerEnvia_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (userProfile.Client.Connected)
                {
                    userProfile.STW.WriteLine(userProfile.TextToSend);
                    this.ChatScreentextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        ChatScreentextBox.AppendText("Enviado : " + userProfile.TextToSend + "\n");
                    }
                    ));
                }
                else
                {
                    ChatScreentextBox.AppendText("\n  Erro de envio \n");
                }
                backgroundWorkerEnvia.CancelAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:  \n" + ex);
            }
        }
        

        
    }
}
