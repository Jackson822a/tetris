using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace tetris
{
    public partial class Form1 : Form
    {
        Rectangle Tpiece1 = new Rectangle(230, 30, 20, 20); //start left
        Rectangle Tpiece4 = new Rectangle(250, 30, 20, 20); //start middle
        Rectangle Tpiece3 = new Rectangle(270, 30, 20, 20); //start right
        Rectangle Tpiece2 = new Rectangle(250, 10, 20, 20); //start top
        Rectangle Tpiece5 = new Rectangle(250, 50, 20, 20); //invis, rotate. start bottom
        Rectangle GameBorderL = new Rectangle(150, 30, 20, 420);
        Rectangle GameBorderR = new Rectangle(370, 30, 20, 420);
        Rectangle GameBorderT = new Rectangle(150, 30, 220, 20);
        Rectangle GameBorderB = new Rectangle(150, 430, 220, 20);


        List<Rectangle> Tpieces = new List<Rectangle>();

        List<List<Rectangle>> pieces = new List<List<Rectangle>>();


        int playerScore = 0;

        string gameState = "waiting";

        bool aDown = false;
        bool dDown = false;
        bool jDown = false;


        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush purpleBrush = new SolidBrush(Color.MediumPurple);
        public Form1()
        {
            InitializeComponent();

            Tpieces.Add(Tpiece1);
            Tpieces.Add(Tpiece2);
            Tpieces.Add(Tpiece3);
            Tpieces.Add(Tpiece4);
            Tpieces.Add(Tpiece5);

            pieces.Add(Tpieces);


        }

        int counter = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            for (int j = 0; j < 5; j++)
            {
                int y = pieces[pieces.Count - 1][j].Y;
                y += 20;
                Rectangle tempRec = new Rectangle(pieces[pieces.Count - 1][j].X, y, 20, 20);
                pieces[pieces.Count - 1][j] = tempRec;

            }

            for (int j = 0; j < 4; j++)
            {
                int y = pieces[pieces.Count - 1][j].Y;
                y += 20;
                if (pieces[0][j].IntersectsWith(GameBorderB))
                {
                    Rectangle Tpiece1 = new Rectangle(230, 30, 20, 20); //start left
                    Rectangle Tpiece4 = new Rectangle(250, 30, 20, 20); //start middle
                    Rectangle Tpiece3 = new Rectangle(270, 30, 20, 20); //start right
                    Rectangle Tpiece2 = new Rectangle(250, 10, 20, 20); //start top
                    Rectangle Tpiece5 = new Rectangle(250, 50, 20, 20); //invis, rotate. start bottom
                    List<Rectangle> list = new List<Rectangle>();

                    list.Add(Tpiece1);
                    list.Add(Tpiece2);
                    list.Add(Tpiece3);
                    list.Add(Tpiece4);
                    list.Add(Tpiece5);
                    pieces.Add(list);

                    counter = 0;

                }
                //left right

                if (aDown == true)
                {
                    Tpiece1.X -= 20;
                    Tpiece4.X -= 20;
                    Tpiece3.X -= 20;
                    Tpiece2.X -= 20;
                    Tpiece5.X -= 20;
                }
                if (dDown == true)
                {
                    Tpiece1.X += 20;
                    Tpiece4.X += 20;
                    Tpiece3.X += 20;
                    Tpiece2.X += 20;
                    Tpiece5.X += 20;
                }
                //rotate
                if (jDown == true)
                {
                    Rectangle temp = Tpiece1;

                    Tpiece1 = Tpiece2;
                    Tpiece2 = Tpiece3;
                    Tpiece3 = Tpiece5;
                    Tpiece5 = temp;

                    //  intersection
                    if (Tpiece1.IntersectsWith(GameBorderB) || Tpiece3.IntersectsWith(GameBorderB) || Tpiece2.IntersectsWith(GameBorderB))
                    {
                        Tpiece1.Y -= 20;
                        Tpiece4.Y -= 20;
                        Tpiece3.Y -= 20;
                        Tpiece2.Y -= 20;
                    }
                    if (Tpiece1.IntersectsWith(GameBorderL))
                    {
                        Tpiece1.X += 20;
                        Tpiece4.X += 20;
                        Tpiece3.X += 20;
                        Tpiece2.X += 20;
                    }
                    if (Tpiece3.IntersectsWith(GameBorderR))
                    {
                        Tpiece1.X -= 20;
                        Tpiece4.X -= 20;
                        Tpiece3.X -= 20;
                        Tpiece2.X -= 20;
                    }
                }
                Refresh();
            }
        }



        private void GameInitalize()
        {
            gameState = "running";

            tetrisLabel.Text = "";
            subtitleLabel.Text = "";

            timer.Enabled = true;



        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.J:
                    jDown = false;
                    break;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.J:
                    jDown = true;
                    break;
                case Keys.Enter:
                    if (gameState == "waiting" || gameState == "over")
                    {
                        GameInitalize();
                    }
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (gameState == "waiting")
            {
                tetrisLabel.Text = "TETRIS";
                subtitleLabel.Text = "Press Enter to start";

            }
            else if (gameState == "running")
            {


                e.Graphics.FillRectangle(whiteBrush, GameBorderL);
                e.Graphics.FillRectangle(whiteBrush, GameBorderR);
                e.Graphics.FillRectangle(whiteBrush, GameBorderT);
                e.Graphics.FillRectangle(whiteBrush, GameBorderB);

                for (int i = 0; i < pieces.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        e.Graphics.FillRectangle(purpleBrush, pieces[i][j]);
                    }
                }

            }
        }
    }
}

