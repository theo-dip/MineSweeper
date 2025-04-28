namespace Board
{
    public class Board
    {
        public int height { get; set; }
        public int width { get; set; }
        public int mines { get; set; }
        public Case.Case[,] cases { get; set; }

        public Board(int height, int width, int mines)
        {
            this.height = height;
            this.width = width;
            this.mines = mines;
            this.cases = new Case.Case[width,height];
        }
    }
}
