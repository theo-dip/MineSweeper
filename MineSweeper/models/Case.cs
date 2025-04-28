using System.ComponentModel;
using System.Numerics;

namespace Case
{
    public class Case   
    {

        public int nbMine { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public bool isMine { get; set; }
        public bool isFlag { get; set; }
        public bool isRevealed { get; set; }

        public Case() { }

        public Case(int posX, int posY)
        {
            this.nbMine = 0;
            this.posX = posX;
            this.posY = posY;
            this.isMine = false;
            this.isFlag = false;
            this.isRevealed = false;
        }
    }
}
