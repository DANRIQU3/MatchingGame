namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // Use este objeto Random para escolher �cones aleat�rios para os quadrados
        Random random = new Random();

        // Cada uma dessas letras � um �cone interessante
        // na fonte Webdings,
        // e cada �cone aparece duas vezes nesta lista
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // firstClicked aponta para o primeiro controle Label 
        // que o jogador clica, mas ser� nulo 
        // se o jogador ainda n�o clicou em um r�tulo
        Label firstClicked = null;

        //secondClicked aponta para o segundo controle Label 
        // que o jogador clica
        Label secondClicked = null;

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();

        }

        /// Atribua cada �cone da lista de �cones a um quadrado aleat�rio
        public void AssignIconsToSquares()
        {
            // O TableLayoutPanel possui 16 r�tulos,
            // e a lista de �cones possui 16 �cones,
            // ent�o um �cone � retirado aleatoriamente da lista
            // e adicionado a cada r�tulo
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

        /// O evento Click de cada r�tulo � tratado por este manipulador de eventos
        /// <param name="sender">O r�tulo que foi clicado</param>
        /// <param name="e"></param>

        private void label1_Click(object sender, EventArgs e)
        {
            // O timer s� � ativado ap�s dois n�o correspondentes 
            // os �cones foram mostrados ao jogador, 
            // ent�o ignore qualquer clique se o cron�metro estiver funcionando
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se o r�tulo clicado for preto, o jogador clicou
                // um �cone que j� foi revelado --
                //ignora o clique
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Se firstClicked for nulo, este � o primeiro �cone 
                // no par que o jogador clicou,
                // ent�o defina firstClicked para o r�tulo que o player 
                // clicado, muda sua cor para preto e retorna
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Se o jogador chegar at� aqui, o cron�metro n�o funciona
                // em execu��o e firstClicked n�o � nulo,
                // ent�o este deve ser o segundo �cone em que o jogador clicou
                //Define sua cor para preto
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Verifica se o jogador ganhou
                CheckForWinner();

                // Se o jogador clicou em dois �cones correspondentes, mantenha-os 
                //preto e redefine firstClicked e secondClicked 
                // para que o jogador possa clicar em outro �cone
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //Se o jogador chegar at� aqui, o jogador 
                // clicou em dois �cones diferentes, ent�o inicie o 
                // timer (que ir� esperar tr�s quartos do 
                // um segundo e depois oculta os �cones)
                timer1.Start();

            }
        }
            /// Este cron�metro � iniciado quando o jogador clica 
            /// dois �cones que n�o correspondem,
            /// ent�o conta tr�s quartos de segundo 
            /// e ent�o desliga e oculta ambos os �cones
            private void timer1_Tick(object sender, EventArgs e)
            {
            //Para o cron�metro
            timer1.Stop();

            //Oculta ambos os �cones
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Redefinir firstClicked e secondClicked 
            // ent�o da pr�xima vez que um r�tulo for
            // clicado, o programa sabe que � o primeiro clique
            firstClicked = null;
            secondClicked = null;
            }

            /// Verifica cada �cone para ver se ele corresponde, por 
            /// comparando sua cor de primeiro plano com sua cor de fundo. 
            /// Se todos os �cones corresponderem, o jogador vence
            public void CheckForWinner()
            {
                // Percorre todos os r�tulos no TableLayoutPanel, 
                // verificando cada um para ver se seu �cone corresponde
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Label iconLabel = control as Label;

                    if (iconLabel != null)
                    {
                        if (iconLabel.ForeColor == iconLabel.BackColor)
                            return;
                    }
                }

                // Se o loop n�o retornou, n�o foi encontrado
                // quaisquer �cones incompar�veis
                // Isso significa que o usu�rio venceu. Mostrar uma mensagem e fechar o formul�rio

                MessageBox.Show("Voc� combinou todos os �cones!", "Parab�ns");
                Close();

            }
        }
    }
        
