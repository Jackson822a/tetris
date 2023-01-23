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

        
        //List<Rectangle> Tpieces = new List<Rectangle>();
      
        
      
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
            //pieces.Add(Tpiece1);
            //pieces.Add(Tpiece2);
            //pieces.Add(Tpiece3);
            //pieces.Add(Tpiece4);
            //pieces.Add(Tpiece5);

           // pieces[0].Add(Tpiece1);
            //pieces[1].Add(Tpiece4);
            //pieces[2].Add(Tpiece2);
            //pieces[3].Add(Tpiece3);
            //pieces[4].Add(Tpiece1);
            //Rectangle test = pieces[0][0];
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //move piece down
            Tpiece1.Y += 20;
            Tpiece4.Y += 20;
            Tpiece3.Y += 20;
            Tpiece2.Y += 20;
            Tpiece5.Y += 20;

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

            }


            //intersection
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



            Refresh();
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
                e.Graphics.FillRectangle(purpleBrush, Tpiece1);
                e.Graphics.FillRectangle(purpleBrush, Tpiece4);
                e.Graphics.FillRectangle(purpleBrush, Tpiece3);
                e.Graphics.FillRectangle(purpleBrush, Tpiece2);
               
                e.Graphics.FillRectangle(whiteBrush, GameBorderL);
                e.Graphics.FillRectangle(whiteBrush, GameBorderR);
                e.Graphics.FillRectangle(whiteBrush, GameBorderT);
                e.Graphics.FillRectangle(whiteBrush, GameBorderB);
            }
        }
    }
}
