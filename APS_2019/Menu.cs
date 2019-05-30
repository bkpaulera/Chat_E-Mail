using System;
using System.Windows.Forms;

namespace APS_2019
{
    public partial class Menu : Form
    { 
        public Menu()
        {
            InitializeComponent();   
        }

        //Evento de clique do botão entrar
        private void ButtonSair_Click(object sender, EventArgs e)
        {
            //Caixa de dialogo para a confirmação da ação, caso a resposta seja sim, usuario retornara ao login
            if (MessageBox.Show("Você deseja mesmo sair?", "Sair", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                Hide();
            }
        }

        //Evento de clique da imagem do chat, ao clicar o usuario entrara na parte do chat da aplicação
        private void PictureBoxChat_Click(object sender, EventArgs e)
        {
            //Chat chat = new Chat();
            ChatChoice chat = new ChatChoice();
            chat.Show();
            

            Hide();
        }

        //Evento de clique da imagem do e-mail, ao clicar o usuario entrara na parte de e-mail da aplicação
        private void PictureBoxEmail_Click(object sender, EventArgs e)
        {
            Email email = new Email();
            email.Show();
            Hide();
        }
    }
}
