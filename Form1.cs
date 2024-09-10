namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // Use este objeto Random para escolher ícones aleatórios para os quadrados
        Random random = new Random();

        // Cada uma dessas letras é um ícone interessante
        // na fonte Webdings,
        // e cada ícone aparece duas vezes nesta lista
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // firstClicked aponta para o primeiro controle Label 
        // que o jogador clica, mas será nulo 
        // se o jogador ainda não clicou em um rótulo
        Label firstClicked = null;

        //secondClicked aponta para o segundo controle Label 
        // que o jogador clica
        Label secondClicked = null;

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();

        }

        /// Atribua cada ícone da lista de ícones a um quadrado aleatório
        public void AssignIconsToSquares()
        {
            // O TableLayoutPanel possui 16 rótulos,
            // e a lista de ícones possui 16 ícones,
            // então um ícone é retirado aleatoriamente da lista
            // e adicionado a cada rótulo
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);

                }
            }
        }

        /// O evento Click de cada rótulo é tratado por este manipulador de eventos
        /// <param name="sender">O rótulo que foi clicado</param>
        /// <param name="e"></param>

        private void label1_Click(object sender, EventArgs e)
        {
            // O timer só é ativado após dois não correspondentes 
            // os ícones foram mostrados ao jogador, 
            // então ignore qualquer clique se o cronômetro estiver funcionando
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se o rótulo clicado for preto, o jogador clicou
                // um ícone que já foi revelado --
                //ignora o clique
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Se firstClicked for nulo, este é o primeiro ícone 
                // no par que o jogador clicou,
                // então defina firstClicked para o rótulo que o player 
                // clicado, muda sua cor para preto e retorna
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Se o jogador chegar até aqui, o cronômetro não funciona
                // em execução e firstClicked não é nulo,
                // então este deve ser o segundo ícone em que o jogador clicou
                //Define sua cor para preto
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Verifica se o jogador ganhou
                CheckForWinner();

                // Se o jogador clicou em dois ícones correspondentes, mantenha-os 
                //preto e redefine firstClicked e secondClicked 
                // para que o jogador possa clicar em outro ícone
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //Se o jogador chegar até aqui, o jogador 
                // clicou em dois ícones diferentes, então inicie o 
                // timer (que irá esperar três quartos do 
                // um segundo e depois oculta os ícones)
                timer1.Start();

            }
        }
            /// Este cronômetro é iniciado quando o jogador clica 
            /// dois ícones que não correspondem,
            /// então conta três quartos de segundo 
            /// e então desliga e oculta ambos os ícones
            private void timer1_Tick(object sender, EventArgs e)
            {
            //Para o cronômetro
            timer1.Stop();

            //Oculta ambos os ícones
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Redefinir firstClicked e secondClicked 
            // então da próxima vez que um rótulo for
            // clicado, o programa sabe que é o primeiro clique
            firstClicked = null;
            secondClicked = null;
            }

            /// Verifica cada ícone para ver se ele corresponde, por 
            /// comparando sua cor de primeiro plano com sua cor de fundo. 
            /// Se todos os ícones corresponderem, o jogador vence
            public void CheckForWinner()
            {
                // Percorre todos os rótulos no TableLayoutPanel, 
                // verificando cada um para ver se seu ícone corresponde
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Label iconLabel = control as Label;

                    if (iconLabel != null)
                    {
                        if (iconLabel.ForeColor == iconLabel.BackColor)
                            return;
                    }
                }

                // Se o loop não retornou, não foi encontrado
                // quaisquer ícones incomparáveis
                // Isso significa que o usuário venceu. Mostrar uma mensagem e fechar o formulário

                MessageBox.Show("Você combinou todos os ícones!", "Parabéns");
                Close();

            }
        }
    }
        
