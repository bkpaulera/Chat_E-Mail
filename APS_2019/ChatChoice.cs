using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_2019
{
    public partial class ChatChoice : Form
    {
        Chat chatClient = new Chat();
        ChatServer chatServer = new ChatServer();

        public ChatChoice()
        {
            InitializeComponent();


        }


        private void ButtonServer_Click(object sender, EventArgs e)
        {
            //profile.Tokien = true;
            //chat = new Chat(profile.Tokien);
            chatServer.Show();
            Hide();
        }

        private void ButtonClient_Click(object sender, EventArgs e)
        {
            //profile.Tokien = false;
            //chat = new Chat(profile.Tokien);
            chatClient.Show();
            Hide();
        }
    }
}
