using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Piece
    {
        public string Tipo { get; set; }
        public bool IsBlank { get; set; }
        public bool IsWhite { get; set; }
        public bool IsBlack { get; set; }
        public int PositionY { get; set; }
        public int PositionX { get; set; }
        public bool IsQueen { get; set; }

        public Piece(string tipo, System.Windows.Forms.Button button)
        {
            Tipo = tipo;
            if (Tipo == "Branca")
            {
                IsBlack = false;
                IsWhite = true;
                IsBlank = false;
            }
            else if (Tipo == "Preta")
            {
                IsBlack = true;
                IsWhite = false;
                IsBlank = false;
            }
            else if (Tipo == "Vazio")
            {
                IsBlack = false;
                IsWhite = false;
                IsBlank = true;
            }
            //Posições no gráfica (A Matriz dá-se por (Y,X))
            PositionY = button.Location.Y / 100;
            PositionX = button.Location.X / 100;
            IsQueen = false;
        }
    }    
}
