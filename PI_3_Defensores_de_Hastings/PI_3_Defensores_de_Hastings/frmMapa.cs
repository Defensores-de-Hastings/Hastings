using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Configuration;
using System.Windows.Forms;
using KingMeServer;

namespace PI_3_Defensores_de_Hastings
{
    public partial class Mapa : Form
    {

        private string[] arEstadoDoJogo;


        public Mapa(string estadoDoJogo)
        {
            InitializeComponent();
            arEstadoDoJogo = estadoDoJogo.Split('$');
            CarregarImagem();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CarregarImagem();
        }

        private void ColocarPesonagem()
        {
            // Contadores por nível (como no original)
            int controleNivel1 = 0;
            int controleNivel2 = 0;
            int controleNivel3 = 0;
            int controleNivel4 = 0;

            foreach (string LugarPerso in arEstadoDoJogo)
            {
                if (LugarPerso == null || LugarPerso.Length < 2)
                {
                    MessageBox.Show("Dados do jogo incompletos!");
                    return;
                }

                string[] persoInfo = LugarPerso.Split(',');
                string nivel = persoInfo[0];
                string letraPersonagem = persoInfo[1].Replace("'", "").Trim().ToUpper();

                Dictionary<string, string> personagensDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "A", "Adilson Konrad.png" }, { "B", "Beatriz Paiva.png" }, { "C", "Claro.png" },
                    { "D", "Douglas Baquiao.png" }, { "E", "Eduardo Takeo.png" }, { "G", "Guilherme Rey.png" },
                    { "H", "Heredia.png" }, { "K", "Kelly Kiyumi.png" }, { "L", "Leonardo Takuno.png" },
                    { "M", "Mario Toledo.png" }, { "Q", "Quintas.png" }, { "R", "Ranulfo.png" },
                    { "T", "Toshio.png" }
                };

                if (!personagensDict.TryGetValue(letraPersonagem, out string nomeArquivo))
                {
                    MessageBox.Show($"Personagem '{letraPersonagem}' não encontrado. Opções válidas: " +
                                  string.Join(", ", personagensDict.Keys));
                    continue;
                }
     
                string caminhoImagem = Path.Combine(@".\imagens\Personagens", nomeArquivo);

                if (!File.Exists(caminhoImagem))
                {
                    MessageBox.Show($"Arquivo não encontrado: {caminhoImagem}");
                    continue;
                }

                int posicaoY = 0;
                int posicaoX = 0; // Variável única para posição X

                // Determina posição Y e incrementa o contador do nível
                switch (nivel)
                {
                    case "1":
                        posicaoY = 535;
                        controleNivel1++;
                        // Determina posição X baseada no contador
                        posicaoX = 350 - (60 * (controleNivel1 - 1));
                        if (controleNivel1 > 4)
                        {
                            MessageBox.Show("Máximo de personagens no nível 1 atingido.");
                            return;
                        }
                        break;

                    case "2":
                        posicaoY = 435;
                        controleNivel2++;
                        posicaoX = 350 - (60 * (controleNivel2 - 1));
                        if (controleNivel2 > 4)
                        {
                            MessageBox.Show("Máximo de personagens no nível 2 atingido.");
                            return;
                        }
                        break;

                    case "3":
                        posicaoY = 355;
                        controleNivel3++;
                        posicaoX = 350 - (60 * (controleNivel3 - 1));
                        if (controleNivel3 > 4)
                        {
                            MessageBox.Show("Máximo de personagens no nível 3 atingido.");
                            return;
                        }
                        break;

                    case "4":
                        posicaoY = 265;
                        controleNivel4++;
                        posicaoX = 350 - (60 * (controleNivel4 - 1));
                        if (controleNivel4 > 4)
                        {
                            MessageBox.Show("Máximo de personagens no nível 4 atingido.");
                            return;
                        }
                        break;

                    case "0":
                        posicaoY = 600;
                        controleNivel4++;
                        posicaoX = 200;
                        break;

                    case "10":
                        posicaoY = 165;
                        controleNivel4++;
                        posicaoX = 150;
                        break;

                    default:
                        MessageBox.Show("Nível inválido: " + nivel);
                        return;
                }

                // Cria o painel do personagem
                Panel pnlPersonagem = new Panel();
                pnlPersonagem.Location = new Point(posicaoX, posicaoY);
                pnlPersonagem.Size = new Size(50, 50);
                pnlPersonagem.BackgroundImage = Image.FromFile(caminhoImagem);
                pnlPersonagem.BackgroundImageLayout = ImageLayout.Stretch;

                // ADIÇÃO AO FORMULÁRIO 
                this.Controls.Add(pnlPersonagem); // Adiciona ao formulário atual
                pnlPersonagem.BringToFront(); // Garante que fique visível
            }
        }



        private void CarregarImagem()
        {
            string imagePath = @".\imagens\tabuleiro.jpg";

            if (File.Exists(imagePath))
            {
                if (pictureBox1.Image == null)
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                }
            }
            else
            {
                MessageBox.Show("Imagem não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void btnColocarPerso_Click(object sender, EventArgs e)
        {
            ColocarPesonagem();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário atual
        }
    }
}