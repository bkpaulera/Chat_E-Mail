using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace APS_2019
{
    public partial class Email : Form
    {
        //Instanciando a classe Profile e Menu para utilizar os seus objetos
        Profile profile = new Profile();
        Menu menu = new Menu();

        public Email()
        {
            InitializeComponent();
        }

        //Evento de clique do botão anexar
        private void ButtonAnexar_Click(object sender, EventArgs e)
        {
            //Abre a caixa de dialogo para selecionar um arquivo da maquina local
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                //habilita a seleção multipla
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Adiciona o nome/caminho do arquivo na listBox
                    foreach (var file in dialog.FileNames)
                    {
                        listBoxAttachments.Items.Add(file);
                    }
                }
            }
        }

        //Evento de clique do botão Enviar
        private void ButtonEnviar_Click(object sender, EventArgs e)
        {
            //Utilizando o protocolo SMTP, variável cliente receberá o HOST e a Porta do dominio do email
            using (var client = new System.Net.Mail.SmtpClient(profile.Email_HOST1, profile.Email_PORT1))
            {
                //definindo o corpo do email a partir do Mail Message
                MailMessage message = new MailMessage();
                message.From = new MailAddress(profile.User, profile.Username);
                message.To.Add(new MailAddress(textBoxDestinatario.Text));

                if (!string.IsNullOrWhiteSpace(textBoxCopia.Text))
                {
                    message.CC.Add(new System.Net.Mail.MailAddress(textBoxCopia.Text));
                }
                    
                if (!string.IsNullOrWhiteSpace(textBoxCopiaOculta.Text))
                {
                    message.Bcc.Add(new System.Net.Mail.MailAddress(textBoxCopiaOculta.Text));
                }
                
                message.Subject = textBoxAssunto.Text;
                message.Body = richTextBoxMessage.Text;

                //Definindo criptografia e credenciais para a conexao
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(profile.User, profile.Password);

                
                try
                {
                    //Caso exista algum conteudo na listbox, fazer o envio
                    foreach (string file in listBoxAttachments.Items)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(file));
                    }
                    //Enviando a mensagem
                    client.Send(message);
                    MessageBox.Show("E-mail Enviado com Sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O E-mail não foi enviado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }
        }

        //Evento de clique do botão Voltar, ele retorna ao Menu da aplicação
        private void ButtonVoltar_Click(object sender, EventArgs e)
        {
            menu.Show();
            Hide();
        }
    }
}
