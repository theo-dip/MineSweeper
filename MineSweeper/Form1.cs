using System.CodeDom;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        private string difficulteActuelle;
        private int caseRevealed;

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
            expertToolStripMenuItem.CheckState = CheckState.Checked;
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
            var board = new Board.Board(height, width, mines);
            board.cases = initCases(width, height, mines);

            var panel = userControl11;
            panel.Controls.Clear();

            var image = new Bitmap(new MemoryStream(Properties.Resources.standard));
            int cellSize = 18; // taille d'une cellule

            foreach (var cell in board.cases)
            {
                if (cell.posX >= 0 && cell.posX < height && cell.posY >= 0 && cell.posY < width)
                {
                    var pictureBox = new PictureBox
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Image = image,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Margin = new Padding(0),
                        Location = new Point(cell.posY * cellSize, cell.posX * cellSize) // Positionnement !
                    };

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

                        VerifierVictoire(width * height, mines);
                    };

                    panel.Controls.Add(pictureBox);
                }
            }

            panel.Width = width * cellSize;
            panel.Height = height * cellSize;
        }


        private Case.Case[,] initCases(int width, int height, int mines)
        {
            Case.Case[,] grid = new Case.Case[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[y, x] = new Case.Case(x, y);
                }
            }

            Random rand = new Random();
            HashSet<Tuple<int, int>> minePositions = new HashSet<Tuple<int, int>>();

            while (minePositions.Count < mines)
            {
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);

                minePositions.Add(new Tuple<int, int>(x, y));
            }

            foreach (var pos in minePositions)
            {
                grid[pos.Item2, pos.Item1].isMine = true;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (!grid[y, x].isMine)
                    {
                        int adjacentMines = CountAdjacentMines(grid, x, y, width, height);
                        grid[y, x].nbMine = adjacentMines;
                    }
                }
            }

            return grid;
        }

        private int CountAdjacentMines(Case.Case[,] grid, int x, int y, int width, int height)
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
                        if (grid[ny, nx].isMine)
                        {
                            adjacentMines++;
                        }
                    }
                }
            }

            return adjacentMines;
        }



        private void ReveleCase(Case.Case cell, UserControl panel, Case.Case[,] cases)
        {
            if (cell.isRevealed) return;
            cell.isRevealed = true;

            int cellSize = 18;

            foreach (Control control in panel.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    if (pictureBox.Location.X == cell.posY * cellSize && pictureBox.Location.Y == cell.posX * cellSize)
                    {
                        if (cell.nbMine > 0)
                        {
                            var resourceName = "cell" + cell.nbMine;
                            var resource = (byte[])Properties.Resources.ResourceManager.GetObject(resourceName);
                            pictureBox.Image = new Bitmap(new MemoryStream(resource));
                            caseRevealed++;
                        }
                        else
                        {
                            var emptyResource = (byte[])Properties.Resources.ResourceManager.GetObject("cell0");
                            pictureBox.Image = new Bitmap(new MemoryStream(emptyResource));
                            caseRevealed++;
                        }

                        break;
                    }
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
        }


        private void VerifierVictoire(int nbCase, int nbMine)
        {
            if ((nbCase - nbMine) == caseRevealed)
            {
                MessageBox.Show("🏆 Félicitations, vous avez gagné !", "Victoire", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            
        }


    }
}
