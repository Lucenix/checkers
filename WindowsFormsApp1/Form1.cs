using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Piece[,] Board = new Piece[8, 8];
        Piece piece1;
        Piece piece2;
        Button button1;
        Button button2;
        bool IsWhiteTurn = true;
        bool CapturaUltimaJogada = false;
        int scorewhite=0, scoreblack=0;


        private void Restart()
        {
            Application.Restart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            scoreBrancas.BackColor = Color.Beige;

            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.Location.Y == 0 || button.Location.Y == 100 || button.Location.Y == 200)
                {
                    Piece piece =  new Piece("Branca", button);
                    Board[piece.PositionY, piece.PositionX] = piece;
                }
                if (button.Location.Y == 300 || button.Location.Y == 400)
                {
                    Piece piece = new Piece("Vazio", button);
                    Board[piece.PositionY, piece.PositionX] = piece;
                }
                if(button.Location.Y == 500 || button.Location.Y == 600 || button.Location.Y == 700)
                {
                    Piece piece = new Piece("Preta", button);
                    Board[piece.PositionY, piece.PositionX] = piece;
                }
            }
        }
        private void Comer(Piece piece)
        {
            string tipo = piece.Tipo;
            foreach(Button button in Controls.OfType<Button>())
            {
                if (button.Location.Y/100 == piece.PositionY && button.Location.X/100 == piece.PositionX)
                {
                    button.Image = Properties.Resources.Checker_White;
                }
                Board[piece.PositionY, piece.PositionX].IsBlank = true;
                Board[piece.PositionY, piece.PositionX].IsBlack = false;
                Board[piece.PositionY, piece.PositionX].IsWhite = false;
                Board[piece.PositionY, piece.PositionX].Tipo = "Vazio";
            }
            if (tipo == "Preta")
            {
                scorewhite++;
                scoreBrancas.Text = "Brancas: " + scorewhite;
            }
            if (tipo == "Branca")
            {
                scoreblack++;
                scorePretas.Text = "Pretas: " + scoreblack;
            }
        }

        private void TornarRainha()
        {
            if(piece1.IsBlack && piece1.PositionY==0 && piece1.IsQueen == false)
            {
                button2.Image = Properties.Resources.Black_Piece2_Queen;
                piece1.IsQueen = true;
                Board[piece1.PositionY, piece1.PositionX] = piece1;
                CapturaUltimaJogada = false;
            }
            if(piece1.IsWhite && piece1.PositionY==7 && piece1.IsQueen == false)
            {
                button2.Image = Properties.Resources.White_Piece_Queen;
                piece1.IsQueen = true;
                Board[piece1.PositionY, piece1.PositionX] = piece1;
                CapturaUltimaJogada = false;
            }
        }
        private void MoverGrafico(string tipo)
        {
            if(tipo == "Branca" && piece1.IsQueen == false)
            {
                button1.Image = Properties.Resources.Checker_White;
                button2.Image = Properties.Resources.White_Piece;

                int aux = piece1.PositionX;
                int auy = piece1.PositionY;
                piece1.PositionY = piece2.PositionY;
                piece1.PositionX = piece2.PositionX;
                piece2.PositionY = auy;
                piece2.PositionX = aux;
                Board[piece2.PositionY, piece2.PositionX] = piece2;
                Board[piece1.PositionY, piece1.PositionX] = piece1;

                TornarRainha();
            }
            else if (tipo == "Preta" && piece1.IsQueen == false)
            {
                button1.Image = Properties.Resources.Checker_White;
                button2.Image = Properties.Resources.Black_Piece2;

                int aux = piece1.PositionX;
                int auy = piece1.PositionY;
                piece1.PositionY = piece2.PositionY;
                piece1.PositionX = piece2.PositionX;
                piece2.PositionY = auy;
                piece2.PositionX = aux;
                Board[piece2.PositionY, piece2.PositionX] = piece2;
                Board[piece1.PositionY, piece1.PositionX] = piece1;

                TornarRainha();
            }
            else if (tipo == "Branca" && piece1.IsQueen == true)
            {
                button1.Image = Properties.Resources.Checker_White;
                button2.Image = Properties.Resources.White_Piece_Queen;

                int aux = piece1.PositionX;
                int auy = piece1.PositionY;
                piece1.PositionY = piece2.PositionY;
                piece1.PositionX = piece2.PositionX;
                piece2.PositionY = auy;
                piece2.PositionX = aux;
                Board[piece2.PositionY, piece2.PositionX] = piece2;
                Board[piece1.PositionY, piece1.PositionX] = piece1;
            }
            else if (tipo == "Preta" && piece1.IsQueen == true)
            {
                button1.Image = Properties.Resources.Checker_White;
                button2.Image = Properties.Resources.Black_Piece2_Queen;

                int aux = piece1.PositionX;
                int auy = piece1.PositionY;
                piece1.PositionY = piece2.PositionY;
                piece1.PositionX = piece2.PositionX;
                piece2.PositionY = auy;
                piece2.PositionX = aux;
                Board[piece2.PositionY, piece2.PositionX] = piece2;
                Board[piece1.PositionY, piece1.PositionX] = piece1;
            }
        }
        private bool PodeComer()
        {
            bool podecomer = false;
            foreach (Piece piece in Board)
            {
                if (piece == null)
                {

                }
                else if (IsWhiteTurn)
                {
                    if (piece.IsQueen)
                    {
                        if (piece.IsWhite)
                        {
                            //comer na diagonal direita-baixo
                            if (7 - piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= 7 - piece.PositionX; i++)
                                {
                                    if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }
                            else if (7 - piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= 7 - piece.PositionY; i++)
                                {
                                    if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }

                            //comer na diagonal direita-cima
                            if (piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= 7 - piece.PositionX; i++)
                                {
                                    if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }
                            else if (piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionY; i++)
                                {
                                    if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }

                            //comer na diagonal esquerda-baixo
                            if (7 - piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionX; i++)
                                {
                                    if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }
                            else if (7 - piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= 7 - piece.PositionY; i++)
                                {
                                    if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }

                            //diagonal cima esquerda
                            if (piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionX; i++)
                                {
                                    if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }
                            else if (piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionY; i++)
                                {
                                    if (Board[piece.PositionY - +i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer= true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (piece.IsWhite)
                        {
                            if ((piece.PositionX == 0 || piece.PositionX == 1) && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                if (Board[piece.PositionY + 1, piece.PositionX + 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX + 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                                    
                            }
                            else if ((piece.PositionX == 7 || piece.PositionX == 6) && piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                if (Board[piece.PositionY + 1, piece.PositionX - 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX - 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                            }
                            else if (piece.PositionY != 6 && piece.PositionY != 7)
                            {
                                if (Board[piece.PositionY + 1, piece.PositionX - 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX - 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                                else if (Board[piece.PositionY + 1, piece.PositionX + 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX + 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (piece.IsQueen)
                    {
                        if (piece.IsBlack)
                        {
                                //comer na diagonal direita-baixo
                                if (7 - piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= 7 - piece.PositionX; i++)
                                    {
                                        if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer= true;
                                        }
                                    }
                                }
                                else if (7 - piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= 7 - piece.PositionY; i++)
                                    {
                                        if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer = true;
                                        }
                                    }
                                }

                                //comer na diagonal direita-cima
                                if (piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= 7 - piece.PositionX; i++)
                                    {
                                        if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer= true;
                                        }
                                    }
                                }
                                else if (piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= piece.PositionY; i++)
                                    {
                                        if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer= true;
                                        }
                                    }
                                }

                                //comer na diagonal esquerda-baixo
                                if (7 - piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= piece.PositionX; i++)
                                    {
                                        if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer= true;
                                        }
                                    }
                                }
                                else if (7 - piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                                {
                                    bool impossivel = false;
                                    bool possivel = false;
                                    for (int i = 1; i <= 7 - piece.PositionY; i++)
                                    {
                                        if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                        {

                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel == false)
                                        {
                                            possivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel)
                                        {
                                            impossivel = true;
                                        }
                                        else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                        {
                                            podecomer = true;
                                        }
                                    }
                                }

                            //diagonal cima esquerda
                            if (piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionX; i++)
                                {
                                    if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer = true;
                                    }
                                }
                            }
                            else if (piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                            {
                                bool impossivel = false;
                                bool possivel = false;
                                for (int i = 1; i <= piece.PositionY; i++)
                                {
                                    if (Board[piece.PositionY - +i, piece.PositionX - i].IsBlank && possivel == false)
                                    {

                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel == false)
                                    {
                                        possivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel)
                                    {
                                        impossivel = true;
                                    }
                                    else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                    {
                                        podecomer = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (piece.IsBlack)
                        {
                            if ((piece.PositionX == 0 || piece.PositionX == 1) && piece.PositionY != 1 && piece.PositionY != 0)
                            {
                                if (Board[piece.PositionY - 1, piece.PositionX + 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX + 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                            }
                            else if ((piece.PositionX == 7 || piece.PositionX == 6) && piece.PositionY != 1 && piece.PositionY != 0)
                            {
                                if (Board[piece.PositionY - 1, piece.PositionX - 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX - 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                            }
                            else if(piece.PositionY != 1  && piece.PositionY !=0)
                            {
                                if (Board[piece.PositionY - 1, piece.PositionX - 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX - 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                                else if (Board[piece.PositionY - 1, piece.PositionX + 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX + 2].IsBlank)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                    }
                }
            }
            return podecomer;
            
        }

        private bool PodeComer(Piece piece)
        {
            bool podecomer = false;
            if (piece.IsQueen)
            {
                if (piece.IsWhite)
                {

                    {
                        //comer na diagonal direita-baixo
                        if (7 - piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (7 - piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //comer na diagonal direita-cima
                        if (piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //comer na diagonal esquerda-baixo
                        if (7 - piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (7 - piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //diagonal cima esquerda
                        if (piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY - +i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                    }
                }
                if (piece.IsBlack)
                {

                    {
                        //comer na diagonal direita-baixo
                        if (7 - piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (7 - piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //comer na diagonal direita-cima
                        if (piece.PositionY > 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (piece.PositionY < 7 - piece.PositionX && piece.PositionX != 6 && piece.PositionX != 7 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX + i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //comer na diagonal esquerda-baixo
                        if (7 - piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (7 - piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 6 && piece.PositionY != 7)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= 7 - piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY + i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }

                        //diagonal cima esquerda
                        if (piece.PositionY > piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionX; i++)
                            {
                                if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                        else if (piece.PositionY < piece.PositionX && piece.PositionX != 0 && piece.PositionX != 1 && piece.PositionY != 0 && piece.PositionY != 1)
                        {
                            bool impossivel = false;
                            bool possivel = false;
                            for (int i = 1; i <= piece.PositionY; i++)
                            {
                                if (Board[piece.PositionY - +i, piece.PositionX - i].IsBlank && possivel == false)
                                {

                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlack)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel == false)
                                {
                                    possivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsWhite && possivel)
                                {
                                    impossivel = true;
                                }
                                else if (Board[piece.PositionY - i, piece.PositionX - i].IsBlank && possivel && impossivel == false)
                                {
                                    podecomer = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (piece.IsWhite)
                {
                    if ((piece.PositionX == 0 || piece.PositionX == 1) && piece.PositionY != 6 && piece.PositionY != 7)
                    {
                        if (Board[piece.PositionY + 1, piece.PositionX + 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX + 2].IsBlank)
                        {
                            podecomer = true;
                        }

                    }
                    else if ((piece.PositionX == 7 || piece.PositionX == 6) && piece.PositionY != 6 && piece.PositionY != 7)
                    {
                        if (Board[piece.PositionY + 1, piece.PositionX - 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX - 2].IsBlank)
                        {
                            podecomer = true;
                        }
                    }
                    else if (piece.PositionY != 6 && piece.PositionY != 7)
                    {
                        if (Board[piece.PositionY + 1, piece.PositionX - 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX - 2].IsBlank)
                        {
                            podecomer = true;
                        }
                        else if (Board[piece.PositionY + 1, piece.PositionX + 1].IsBlack && Board[piece.PositionY + 2, piece.PositionX + 2].IsBlank)
                        {
                            podecomer = true;
                        }
                    }
                }
                else if (piece.IsBlack)
                {
                    if ((piece.PositionX == 0 || piece.PositionX == 1) && piece.PositionY != 1 && piece.PositionY != 0)
                    {
                        if (Board[piece.PositionY - 1, piece.PositionX + 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX + 2].IsBlank)
                        {
                            podecomer = true;
                        }
                    }
                    else if ((piece.PositionX == 7 || piece.PositionX == 6) && piece.PositionY != 1 && piece.PositionY != 0)
                    {
                        if (Board[piece.PositionY - 1, piece.PositionX - 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX - 2].IsBlank)
                        {
                            podecomer = true;
                        }
                    }
                    else if (piece.PositionY != 1 && piece.PositionY != 0)
                    {
                        if (Board[piece.PositionY - 1, piece.PositionX - 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX - 2].IsBlank)
                        {
                            podecomer = true;
                        }
                        else if (Board[piece.PositionY - 1, piece.PositionX + 1].IsWhite && Board[piece.PositionY - 2, piece.PositionX + 2].IsBlank)
                        {
                            podecomer = true;
                        }
                    }
                }
            }
            return podecomer;
        }
        private bool Mover()
        {
            if (piece2.IsBlank == true)
            {
                bool comer = PodeComer();
                if (piece1.IsQueen == false)
                {
                    if(piece1.IsWhite)
                    {
                        if(piece1.PositionX == 0)
                        {
                            if(piece2.PositionX == piece1.PositionX + 1 && piece2.PositionY == piece1.PositionY + 1 && comer == false)
                            {
                                //Mover
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            else if(Board[piece1.PositionY + 1, piece1.PositionX + 1].IsBlack && piece2.PositionY == piece1.PositionY + 2 
                                && piece2.PositionX == piece1.PositionX + 2 && comer)
                            {
                                //Comer
                                Comer(Board[piece1.PositionY + 1, piece1.PositionX + 1]);
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = true;
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                        else if (piece1.PositionX == 7)
                        {
                            if (piece2.PositionX == piece1.PositionX - 1 && piece2.PositionY == piece1.PositionY + 1 && comer == false)
                            {
                                //Mover
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            else if (Board[piece1.PositionY + 1, piece1.PositionX - 1].IsBlack && piece2.PositionY == piece1.PositionY + 2
                                && piece2.PositionX == piece1.PositionX - 2 && comer)
                            {
                                //Comer
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY + 1, piece1.PositionX - 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if ((piece2.PositionX == piece1.PositionX - 1 || piece2.PositionX == piece1.PositionX + 1) &&
                                (piece2.PositionY == piece1.PositionY + 1) && comer == false)
                            {
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            if(Board[piece1.PositionY + 1, piece1.PositionX + 1].IsBlack && piece2.PositionY == piece1.PositionY + 2 && piece2.PositionX == piece1.PositionX + 2 && comer)
                            {
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY + 1, piece1.PositionX + 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            if(Board[piece1.PositionY + 1, piece1.PositionX - 1].IsBlack && piece2.PositionY == piece1.PositionY + 2 && piece2.PositionX == piece1.PositionX - 2 && comer)
                            {
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY + 1, piece1.PositionX - 1 ]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else 
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                    }
                    else if (piece1.IsBlack)
                    {
                        if (piece1.PositionX == 0)
                        {
                            if (piece2.PositionX == piece1.PositionX + 1 && piece2.PositionY == piece1.PositionY - 1 && comer == false)
                            {
                                //Mover
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            else if (Board[piece1.PositionY - 1, piece1.PositionX + 1].IsWhite && piece2.PositionY == piece1.PositionY - 2
                                && piece2.PositionX == piece1.PositionX + 2 && comer)
                            {
                                //Comer
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY - 1, piece1.PositionX + 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                        else if (piece1.PositionX == 7)
                        {
                            if (piece2.PositionX == piece1.PositionX - 1 && piece2.PositionY == piece1.PositionY - 1 && comer == false)
                            {
                                //Mover
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            else if (Board[piece1.PositionY - 1, piece1.PositionX - 1].IsWhite && piece2.PositionY == piece1.PositionY - 2
                                && piece2.PositionX == piece1.PositionX - 2 && comer)
                            {
                                //Comer
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY - 1, piece1.PositionX - 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if ((piece2.PositionX == piece1.PositionX - 1 || piece2.PositionX == piece1.PositionX + 1) &&
                                (piece2.PositionY == piece1.PositionY - 1) && comer == false)
                            {
                                MoverGrafico(piece1.Tipo);
                                CapturaUltimaJogada = false;
                                return true;
                            }
                            if (Board[piece1.PositionY - 1, piece1.PositionX + 1].IsWhite && piece2.PositionY == piece1.PositionY - 2 && piece2.PositionX == piece1.PositionX + 2 && comer)
                            {
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY - 1, piece1.PositionX + 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            if (Board[piece1.PositionY - 1, piece1.PositionX - 1].IsWhite && piece2.PositionY == piece1.PositionY - 2 && piece2.PositionX == piece1.PositionX - 2 && comer)
                            {
                                CapturaUltimaJogada = true;
                                Comer(Board[piece1.PositionY - 1, piece1.PositionX - 1]);
                                MoverGrafico(piece1.Tipo);
                                return true;
                            }
                            else
                            {
                                if (comer == false)
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("É obrigatório capturar.");
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (piece1.IsWhite)
                    {
                        if ((piece2.PositionY - piece1.PositionY) > 0 && (piece2.PositionX - piece1.PositionX) > 0)
                        {
                            if (piece2.PositionY-piece1.PositionY == piece2.PositionX - piece1.PositionX)
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < (piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY + i, piece1.PositionX + i];
                                    if (piece.IsBlack)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY + i, piece1.PositionX + i];
                                    }
                                    if (piece.IsWhite)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido.");
                                return false;
                            }
                        }
                        if ((piece2.PositionY - piece1.PositionY) > 0 && (piece2.PositionX - piece1.PositionX) < 0)
                        {
                            if(piece2.PositionY - piece1.PositionY == -(piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < (piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY + i, piece1.PositionX - i];
                                    if (piece.IsBlack)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY + i, piece1.PositionX - i];
                                    }
                                    if (piece.IsWhite)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido.");
                                return false;
                            }

                        }
                        if ((piece2.PositionY - piece1.PositionY) < 0 && (piece2.PositionX - piece1.PositionX) < 0)
                        {
                            if (-(piece2.PositionY - piece1.PositionY) == -(piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < -(piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY - i, piece1.PositionX - i];
                                    if (piece.IsBlack)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY - i, piece1.PositionX - i];
                                    }
                                    if (piece.IsWhite)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido.");
                                return false;
                            }


                        }
                        if ((piece2.PositionY - piece1.PositionY) < 0 && (piece2.PositionX - piece1.PositionX) > 0)
                        {
                            if (-(piece2.PositionY - piece1.PositionY) == (piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < -(piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY - i, piece1.PositionX + i];
                                    if (piece.IsBlack)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY - i, piece1.PositionX + i];
                                    }
                                    if (piece.IsWhite)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido.");
                                return false;
                            }

                        }
                    }
                    else if (piece1.IsBlack)
                    {
                        if ((piece2.PositionY - piece1.PositionY) > 0 && (piece2.PositionX - piece1.PositionX) > 0)
                        {
                            if (piece2.PositionY - piece1.PositionY == piece2.PositionX - piece1.PositionX)
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < (piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY + i, piece1.PositionX + i];
                                    if (piece.IsWhite)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY + i, piece1.PositionX + i];
                                    }
                                    if (piece.IsBlack)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido 7.");
                                return false;
                            }
                        }
                        if ((piece2.PositionY - piece1.PositionY) > 0 && (piece2.PositionX - piece1.PositionX) < 0)
                        {
                            if (piece2.PositionY - piece1.PositionY == -(piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < (piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY + i, piece1.PositionX - i];
                                    if (piece.IsWhite)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY + i, piece1.PositionX - i];
                                    }
                                    if (piece.IsBlack)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido gay.");
                                return false;
                            }

                        }
                        if ((piece2.PositionY - piece1.PositionY) < 0 && (piece2.PositionX - piece1.PositionX) < 0)
                        {
                            if (-(piece2.PositionY - piece1.PositionY) == -(piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < -(piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY - i, piece1.PositionX - i];
                                    if (piece.IsWhite)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY - i, piece1.PositionX - i];
                                    }
                                    if (piece.IsBlack)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido 9.");
                                return false;
                            }


                        }
                        if ((piece2.PositionY - piece1.PositionY) < 0 && (piece2.PositionX - piece1.PositionX) > 0)
                        {
                            if (-(piece2.PositionY - piece1.PositionY) == (piece2.PositionX - piece1.PositionX))
                            {
                                int numini = 0;
                                Piece piececaptura = null;
                                for (int i = 1; i < -(piece2.PositionY - piece1.PositionY); i++)
                                {
                                    Piece piece = Board[piece1.PositionY - i, piece1.PositionX + i];
                                    if (piece.IsWhite)
                                    {
                                        numini++;
                                        piececaptura = Board[piece1.PositionY - i, piece1.PositionX + i];
                                    }
                                    if (piece.IsBlack)
                                    {
                                        numini = numini + 2; //Impossibilita o movimento
                                    }
                                }
                                if (numini == 0 && comer==false)
                                {
                                    MoverGrafico(piece1.Tipo);
                                    CapturaUltimaJogada = false;
                                    return true;
                                }
                                else if (numini == 1 && comer)
                                {
                                    CapturaUltimaJogada = true;
                                    Comer(Board[piececaptura.PositionY, piececaptura.PositionX]);
                                    MoverGrafico(piece1.Tipo);
                                    return true;
                                }
                                else if (comer)
                                {
                                    MessageBox.Show("Captura é obrigatória.");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Movimento Inválido.");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Movimento Inválido 10.");
                                return false;
                            }

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Movimento Inválido - O espaço não é vazio.");
                return false;
            }
            MessageBox.Show("Erro 5");
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quer mesmo sair?\nVítor Mendes Nº25 12ºE\nAndré Lucena Nº3 12ºG", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                //shrug
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsWhiteTurn)
            {
                if (MessageBox.Show("Deseja mesmo desistir, Brancas?", "Desistir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show("As Pretas ganham! Recomeçar?", "´Vitória!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Restart();
                        return;
                    }
                    else
                    {
                        //shrug
                    }
                }
            }
            else if (IsWhiteTurn == false)
            {
                if (MessageBox.Show("Deseja mesmo desistir, Pretas?", "Desistir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show("As Brancas ganham! Recomeçar?", "´Vitória!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Restart();
                        return;
                    }
                    else
                    {
                        //shrug
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (piece1 == null)
            {
                button1 = button;
                piece1 = Board[button1.Location.Y / 100, button1.Location.X / 100];
                if (piece1.IsBlank)
                {
                    piece1 = null;
                    button1 = null;
                    return;
                }
                else if (piece1.IsWhite && IsWhiteTurn == false)
                {
                    MessageBox.Show("É o turno das Pretas!");
                    piece1 = null;
                    button1 = null;
                    return;
                }
                else if(piece1.IsBlack && IsWhiteTurn)
                {
                    MessageBox.Show("É o turno das Brancas!");
                    piece1 = null;
                    button1 = null;
                    return;
                }
                button1.FlatAppearance.BorderColor = Color.Green;
                button1.FlatAppearance.BorderSize = 2;

            }
            else
            {
                button2 = button;
                piece2 = Board[button2.Location.Y / 100, button2.Location.X / 100];
                bool move = Mover();
                button1.FlatAppearance.BorderColor = Color.White;
                piece2 = null;
                button1 = null;
                button2 = null;
                //Mover
                if (CapturaUltimaJogada == true && move)
                {
                    if (PodeComer(piece1))
                    {
                        piece1 = null;
                        return;
                    }   
                }
                piece1 = null;
                if (scorewhite == 12)
                {
                    if (MessageBox.Show("As Brancas ganham! Recomeçar?", "´Vitória!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Restart();
                        return;
                    }
                    else
                    {
                        //shrug
                    }
                }
                if (scoreblack == 12)
                {
                    if (MessageBox.Show("As Pretas ganham! Recomeçar?", "´Vitória!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Restart();
                        return;
                    }
                    else
                    {
                        //shrug
                    }
                }

                if (IsWhiteTurn && move)
                {
                    scoreBrancas.BackColor = Color.Transparent;
                    scorePretas.BackColor = Color.Red;
                    IsWhiteTurn = false;
                }
                else if (IsWhiteTurn == false && move)
                {
                    scoreBrancas.BackColor = Color.Beige;
                    scorePretas.BackColor = Color.Transparent;
                    IsWhiteTurn = true;
                }
            }
        }
    }
}
