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
    public partial class ChatServer : Form
    {
        Menu menu = new Menu();
        Profile serverProfile = new Profile();
        public ChatServer()
        {
            InitializeComponent();

            //Verifica de o ip faz parte da falimia TcpIp 

            foreach (IPAddress address in serverProfile.LocalIp)
            {
                //Adiciona  o ip e uma porta aos campos de IP e Porta
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox_IP.Text = address.ToString();
                    textBox_Port.Text = serverProfile.Port.ToString();
                }


            }
        }

        //Solicita o envio da menssagem para o  Servidor ou o Cliente
        private void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (MessagetextBox.Text != "")
            {
                //TextToSend = MessagetextBox.Text;
                serverProfile.TextToSend = MessagetextBox.Text;
                backgroundWorker2.RunWorkerAsync();

            }
            MessagetextBox.Text = "";
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            backgroundWorker3.RunWorkerAsync();
        }

        private void ButtonVoltar_Click(object sender, EventArgs e)
        {
            menu.Show();
            Hide();
            Close();

        }
        //Recebe menssagem e Exibe 
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            while (serverProfile.Client.Connected)
            {
                try
                {
                    serverProfile.Recieve = serverProfile.STR.ReadLine();
                    this.ChatScreentextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        //Adiciona a menssagem na tela
                        ChatScreentextBox.AppendText("Recebido " + serverProfile.Recieve + "\n");
                    }));
                    serverProfile.Recieve = "";
                }
                catch (Exception ex)
                {
                    //ChatScreentextBox.AppendText("Messagem não enviada");
                    MessageBox.Show("Erro : \n " + ex);
                }
            }
        }
        //Envia Menssagem e exibe
        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (serverProfile.Client.Connected)
                {
                    serverProfile.STW.WriteLine(serverProfile.TextToSend);
                    this.ChatScreentextBox.Invoke(new MethodInvoker(delegate ()
                    {
                        //Adiciona Menssagem na tela
                        ChatScreentextBox.AppendText("Enviado : " + serverProfile.TextToSend + "\n");
                    }
                    ));
                }
                else
                {
                    ChatScreentextBox.AppendText("\n  Erro de envio \n");
                }
                backgroundWorker2.CancelAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:  \n" + ex);
            }
        }
        //Inicia o Servidor
        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            if (IPAddress.TryParse(textBox_IP.Text, out IPAddress address))
            {
                if (int.Parse(textBox_Port.Text) > 999 &
                                int.Parse(textBox_Port.Text) < 8081)
                {
                    try
                    {
                        serverProfile.Listener = new TcpListener(IPAddress.Any, int.Parse(textBox_Port.Text));
                        serverProfile.Listener.Start();
                        serverProfile.Client = serverProfile.Listener.AcceptTcpClient();

                        serverProfile.STR = new StreamReader(serverProfile.Client.GetStream());
                        serverProfile.STW = new StreamWriter(serverProfile.Client.GetStream());
                        serverProfile.STW.AutoFlush = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : \n" + ex);
                    }
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker2.WorkerSupportsCancellation = true;
                }
                else
                {
                    MessageBox.Show("\n\t Error: Porta Invalida \n Tente uma porta entre 1000 e 8080");
                }
            }
            else
            {
                MessageBox.Show(" \n Ip invalido \n");
            }
        }

        private void ChatServer_Load(object sender, EventArgs e)
        {

        }
    }
}
