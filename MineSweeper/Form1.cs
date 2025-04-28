using System.CodeDom;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        private string difficulteActuelle;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void debutant_Click(object sender, EventArgs e)
        {
            difficulteActuelle = "Débutant";
            LancerNouvellePartie(10, 10, 10);
        }

        private void intermediaire_Click(object sender, EventArgs e)
        {
            difficulteActuelle = "Intermédiaire";
            LancerNouvellePartie(16, 16, 40);
        }

        private void expert_Click(object sender, EventArgs e)
        {
            difficulteActuelle = "Expert";
            LancerNouvellePartie(30, 16, 99);
        }


        private void nouvellePartie(object sender, EventArgs e)
        {
            switch (difficulteActuelle)
            {
                case "Débutant":
                    LancerNouvellePartie(10, 10, 10);
                    break;
                case "Intermédiaire":
                    LancerNouvellePartie(16, 16, 40);
                    break;
                case "Expert":
                    LancerNouvellePartie(30, 16, 99);
                    break;
                default:
                    LancerNouvellePartie(10, 10, 10);
                    break;
            }
        }



        private void LancerNouvellePartie(int width, int height, int mines)
        {
            Console.WriteLine("test");
            var board = new Board.Board(width, height, mines);
            board.cases = initCases(width, height, mines);


            var panel = userControl11.tableLayoutPanel1;
            panel.Controls.Clear();
            panel.RowCount = height;
            panel.ColumnCount = width;
            panel.RowStyles.Clear();
            panel.ColumnStyles.Clear();

            for (int i = 0; i < height; i++)
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18));

            for (int j = 0; j < width; j++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18));

            var image = new Bitmap(new MemoryStream(Properties.Resources.standard));

            foreach (var cell in board.cases)
            {
                if (cell.posX >= 0 && cell.posX < height && cell.posY >= 0 && cell.posY < width)
                {
                    var pictureBox = new PictureBox
                    {
                        Width = 18,
                        Height = 18,
                        Image = image,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Margin = new Padding(0)
                    };

                    //if (!cell.isFlag && !cell.isRevealed)
                    //{

                    //    pictureBox.MouseEnter += (sender, e) =>
                    //    {
                    //        pictureBox.Image = new Bitmap(new MemoryStream(Properties.Resources.survol));
                    //    };

                    //    pictureBox.MouseLeave += (sender, e) =>
                    //    {
                    //        pictureBox.Image = new Bitmap(new MemoryStream(Properties.Resources.standard));
                    //    };

                    //}

                    pictureBox.MouseDown += (sender, e) =>
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (cell.isFlag || cell.isRevealed)
                                return;

                            if (cell.isMine)
                            {
                                pictureBox.Image = new Bitmap(new MemoryStream(Properties.Resources.mine));
                                MessageBox.Show("💥 BOUM ! Vous avez perdu !", "Défaite", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                ReveleCase(cell, panel, board.cases);
                            }
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            if (!cell.isRevealed)
                            {
                                if (!cell.isFlag)
                                {
                                    pictureBox.Image = new Bitmap(new MemoryStream(Properties.Resources.drapeau));
                                    cell.isFlag = true;
                                }
                                else
                                {
                                    pictureBox.Image = new Bitmap(new MemoryStream(Properties.Resources.standard));
                                    cell.isFlag = false;
                                }
                            }
                        }
                    };





                    panel.Controls.Add(pictureBox, cell.posY, cell.posX);
                }
            }

            

        }

        private Case.Case[,] initCases(int width, int height, int mines)
        {
            Case.Case[,] grid = new Case.Case[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = new Case.Case(x, y);
                }
            }

            Random rand = new Random();
            for (int i = 0; i < mines; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(0, width);
                    y = rand.Next(0, height);
                }
                while (grid[x, y].isMine);

                grid[x, y].isMine = true;
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!grid[x, y].isMine)
                    {
                        int adjacentMines = 0;
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int nx = x + dx;
                                int ny = y + dy;

                                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                                {
                                    if (grid[nx, ny].isMine)
                                    {
                                        adjacentMines++;
                                    }
                                }
                            }
                        }
                        Console.Write(adjacentMines);
                        grid[x, y].nbMine = adjacentMines;
                    }
                }
            }

            return grid;
        }

        private void ReveleCase(Case.Case cell, TableLayoutPanel panel, Case.Case[,] cases)
        {
            if (cell.isRevealed) return;
            cell.isRevealed = true;

            Control control = panel.GetControlFromPosition(cell.posY, cell.posX);
            if (control is PictureBox pictureBox)
            {
                if (cell.nbMine > 0)
                {
                    var resourceName = "cell" + cell.nbMine;
                    var resource = (byte[])Properties.Resources.ResourceManager.GetObject(resourceName);
                    pictureBox.Image = new Bitmap(new MemoryStream(resource));
                }
                else
                {
                    var emptyResource = (byte[])Properties.Resources.ResourceManager.GetObject("cell0");
                    pictureBox.Image = new Bitmap(new MemoryStream(emptyResource));
                }
            }

            if (cell.nbMine == 0)
            {
                int width = cases.GetLength(0);
                int height = cases.GetLength(1);

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = cell.posX + dx;
                        int ny = cell.posY + dy;

                        if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                        {
                            var neighbor = cases[nx, ny];
                            if (!neighbor.isMine)
                            {
                                ReveleCase(neighbor, panel, cases);
                            }
                        }
                    }
                }
            }

            VerifierVictoire(cases);
        }

        private void VerifierVictoire(Case.Case[,] cases)
        {
            foreach (var cell in cases)
            {
                if (!cell.isMine && !cell.isRevealed)
                {
                    return;
                }
            }

            MessageBox.Show("🏆 Félicitations, vous avez gagné !", "Victoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }








    }
}
