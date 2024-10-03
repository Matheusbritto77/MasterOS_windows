using System;
using System.Diagnostics; // Necess�rio para Process
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq; // Certifique-se de ter instalado o Newtonsoft.Json via NuGet

namespace MasterOS
{
    public partial class Master_os : Form
    {
        private WebView2 webView;
        private const string apiUrl = "https://api.os-master.online/version.php"; // URL da sua API
        private string currentAppVersion = "1.0.1"; // Vers�o atual do seu aplicativo

        public Master_os()
        {
            InitializeComponent();
            InitializeWebView();
            CheckForUpdates(); // Verifica por atualiza��es ao inicializar

            // Configura��es para tela cheia
            this.WindowState = FormWindowState.Maximized; // Maximiza a janela
            this.FormBorderStyle = FormBorderStyle.None; // Remove bordas da janela
        }

        private void InitializeWebView()
        {
            // Instancia o WebView2 e define as propriedades b�sicas
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            // Inicia o WebView2 e carrega uma URL
            webView.Source = new Uri("https://os-master.online/");
            webView.EnsureCoreWebView2Async();

            // Adiciona o controle WebView2 ao formul�rio
            this.Controls.Add(webView);
        }

        private async void CheckForUpdates()
        {
            try
            {
                string latestVersion = await GetLatestVersionAsync();
                if (latestVersion != currentAppVersion)
                {
                    ShowUpdateButton(); // Exibe o bot�o de atualiza��o se a vers�o for diferente
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar atualiza��es: " + ex.Message);
            }
        }

        private async Task<string> GetLatestVersionAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response); // Parseia a resposta JSON
                return json["version"]?.ToString(); // Retorna a vers�o
            }
        }

        private void ShowUpdateButton()
        {
            // Cria uma nova janela pop-up para mostrar a mensagem
            Form updateForm = new Form
            {
                Text = "Atualiza��o Necess�ria",
                Size = new System.Drawing.Size(400, 200), // Aumentando a largura e altura
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog, // Impede redimensionamento
                MaximizeBox = false, // Impede maximizar
                MinimizeBox = false, // Impede minimizar
                BackColor = System.Drawing.Color.FromArgb(30, 30, 30) // Fundo escuro
            };

            // Fecha a aplica��o se o usu�rio clicar no X
            updateForm.FormClosing += (sender, e) =>
            {
                Application.Exit(); // Fecha a aplica��o principal
            };

            Label messageLabel = new Label
            {
                Text = "Uma nova vers�o do aplicativo est� dispon�vel. Por favor, atualize para continuar.",
                Dock = DockStyle.Top,
                AutoSize = false, // Desabilita AutoSize para permitir o ajuste manual
                MaximumSize = new System.Drawing.Size(380, 0), // Define a largura m�xima e permite altura vari�vel
                ForeColor = System.Drawing.Color.White, // Cor do texto
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular), // Estilo da fonte
                TextAlign = ContentAlignment.MiddleCenter // Centraliza o texto
            };

            Button updateButton = new Button
            {
                Text = "Atualizar Agora",
                Dock = DockStyle.Bottom,
                BackColor = System.Drawing.Color.DodgerBlue, // Cor de fundo do bot�o
                ForeColor = System.Drawing.Color.White, // Cor do texto do bot�o
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold), // Estilo da fonte
                Height = 40, // Aumentando altura do bot�o
                Width = 380, // Definindo a largura do bot�o (um pouco menor que a largura da caixa)
                FlatStyle = FlatStyle.Flat, // Estilo plano
                Margin = new Padding(10), // Margens
                Cursor = Cursors.Hand // Cursor de m�o ao passar sobre o bot�o
            };

            // Efeito de hover
            updateButton.MouseEnter += (s, e) => updateButton.BackColor = System.Drawing.Color.CornflowerBlue;
            updateButton.MouseLeave += (s, e) => updateButton.BackColor = System.Drawing.Color.DodgerBlue;

            updateButton.Click += (sender, e) => UpdateButton_Click(sender, e, updateForm);

            updateForm.Controls.Add(messageLabel);
            updateForm.Controls.Add(updateButton);
            updateForm.ShowDialog(this); // Mostra o pop-up como uma janela modal
        }

        private void UpdateButton_Click(object sender, EventArgs e, Form updateForm)
        {
            // Abre a p�gina de download no navegador padr�o
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://os-master.online/dawnload/", // Corrigido o link para download
                UseShellExecute = true // Usado para abrir a URL no navegador padr�o
            });

            // Fecha o formul�rio de atualiza��o
            updateForm.Close();
            // Fecha o aplicativo
            Application.Exit();
        }
    }
}
