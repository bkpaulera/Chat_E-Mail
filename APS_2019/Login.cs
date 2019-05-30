using System;
using System.Windows.Forms;

namespace APS_2019
{
    public partial class Login : Form
    {
        //Instanciando a classe Profile e Menu para utilizar os seus objetos
        Profile profile = new Profile();
        Menu abremenu = new Menu();

        public Login()
        {
            InitializeComponent();
        }

        //Evento de clique do botão entrar
        private void ButtonEntrar_Click(object sender, EventArgs e)
        {       
                //Verificação do usuario e senha, tratamento de campos em branco ou diferentes dos que estão na classe profile
                if (textBoxEmail.Text == String.Empty && textBoxSenha.Text == String.Empty)
                {
                    MessageBox.Show("Para logar no sistema é necessário preencher os campos E-mail e Senha");
                }
                else
                {
                    if (textBoxEmail.Text == String.Empty)
                    {
                        MessageBox.Show("Para logar no sistema é necessário preencher o campo E-mail");
                    }
                    else
                    {
                        if (textBoxSenha.Text == String.Empty)
                        {
                            MessageBox.Show("Para logar no sistema é necessário preencher o campo Senha");
                        }
                        else
                        {
                            if (textBoxEmail.Text != profile.User || textBoxSenha.Text != profile.Password)
                            {
                                MessageBox.Show("Usuario ou senha inseridos incorretamente!");
                            }
                            else
                            {
                                abremenu.Show();
                                Hide();
                            }
                                
                        }
                    }
                }
        }

        //Evento de clique do botão Sair
        private void ButtonSair_Click(object sender, EventArgs e)
        {
            //Caixa de dialogo para a confirmação da ação, caso a resposta seja sim, usuario encerrará a aplicação
            if (MessageBox.Show("Você deseja mesmo sair?", "Sair", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
