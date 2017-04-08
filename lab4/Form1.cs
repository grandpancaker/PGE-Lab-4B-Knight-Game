using lab4.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        int x_player = -1, 
            y_player = -1,
            x_door =-1,
            y_door = -1,
            x_key = -1,
            y_key =-1;

        Random _random = new Random();
        public static int blocs = 8;
        int[,] table_color = new int[blocs, blocs];
        Bitmap knightPng = Properties.Resources.knight;
        Bitmap knight2Png = Properties.Resources.knight2;
        Bitmap closed_doorPng = Properties.Resources.closed_door;
        Bitmap opened_doorPng = Properties.Resources.opened_door;
        Bitmap keyPng = Properties.Resources.key;
        bool gotKey = false;

        PictureBox k = new PictureBox();
        PictureBox key = new PictureBox();
        PictureBox door =new PictureBox();
        


        void InitPic()
        {
            opened_doorPng.MakeTransparent(Color.White);
            closed_doorPng.MakeTransparent(Color.White);
            knightPng.MakeTransparent(Color.White);
            knight2Png.MakeTransparent(Color.White);
            keyPng.MakeTransparent(Color.White);


            

            key.Dock = System.Windows.Forms.DockStyle.Fill;
            key.BackColor = Color.Transparent;
            key.SizeMode = PictureBoxSizeMode.StretchImage;

            door.Dock = System.Windows.Forms.DockStyle.Fill;
            door.BackColor = Color.Transparent;
            door.SizeMode = PictureBoxSizeMode.StretchImage;

            
            

            
            key.Image = Resources.key;
           

            
            door.Image = Resources.closed_door;
           


        }


        public Form1()
        {
            InitializeComponent();
            NewGame();
            
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        
     
       
       
        private void setingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings a1 = new settings();
            a1.ShowDialog();
            board.ColumnCount = blocs;
            board.RowCount = blocs;
            this.Text ="game"+ " " + blocs.ToString();
            NewGame();
            board.RowStyles.Clear();
            for (int i = 1; i <= blocs; i++)
            {
                board.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            }
            board.ColumnStyles.Clear();
            for (int i = 1; i <= blocs; i++)
            {
                board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }
            board.Size = new System.Drawing.Size(284, 238);
            board.Dock = System.Windows.Forms.DockStyle.Fill;
           
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
        void NewGame()
        {
            x_player = -1;
            y_player = -1;
            x_door = -1;
            y_door = -1;
            x_key = -1;
            y_key = -1;
            gotKey = false;

            ColorTable();
            PutKnight();
            PutKey();
            PutDoor();
            board.Refresh();
            
        }
        void ColorTable()
        {

          //  int[,] new_table_color = new int[blocs, blocs];
            table_color  = new int[blocs, blocs];

            for (int i = 0; i < blocs; i++)
            {
                for (int j = 0; j < blocs; j++)
                {
                    table_color[i, j] = _random.Next(0, 2);
                    
                }
            }
        }
        #region put
        void PutKnight()
        {
            if(x_player!=-1)
            {
                board.Controls.RemoveAt(0);
            }

            for (int i = 0; i < blocs; i++)
            {
                for (int j = 0; j < blocs; j++)
                {
                    if (table_color[i, j] == 1)
                    {
                        x_player = i;
                        y_player = j;
                        break;
                    }
                }
            }

            k.Dock = System.Windows.Forms.DockStyle.Fill;
            k.BackColor = Color.Transparent;
            k.SizeMode = PictureBoxSizeMode.StretchImage;
            k.Image =knightPng;
            board.Controls.Add(k, x_player, y_player);
        }

        void PutKey()
        {
            if (x_key != -1)
            {
                board.Controls.Remove(key);
            }

            for (int i = 0; i < blocs; i++)
            {
                for (int j = 0; j < blocs; j++)
                {
                    if (table_color[i, j] == 1)
                    {
                        if (i != x_player && j != x_player)
                        { 
                        x_key = i;
                        y_key = j;
                        break;
                    }
                    }
                }
            }

            key.Dock = System.Windows.Forms.DockStyle.Fill;
            key.BackColor = Color.Transparent;
            key.SizeMode = PictureBoxSizeMode.StretchImage;
            key.Image = keyPng;
            board.Controls.Add(key, x_key, y_key);
        }
    
        void PutDoor()
        {
        if (x_door != -1)
        {
            board.Controls.Remove(door);
        }

        for (int i = 0; i < blocs; i++)
        {
            for (int j = 0; j < blocs; j++)
            {
                if (table_color[i, j] == 1)
                        if (i != x_player && j != x_player)
                            if (i != x_key && j != y_key)
                            {
                        x_door = i;
                        y_door = j;
                    break;
                }
            }
        }

            door.Dock = System.Windows.Forms.DockStyle.Fill;
            door.BackColor = Color.Transparent;
            door.SizeMode = PictureBoxSizeMode.StretchImage;
            door.Image = closed_doorPng;
        board.Controls.Add(door, x_door, y_door);
      }

        #endregion
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                board.Controls.Remove(k);
            }
                
            if (e.KeyCode == Keys.Right)
            {
                if (x_player+1 < blocs  )
                {
                    if(table_color[x_player + 1, y_player] == 1 )

                    x_player++;
                   
                }
                if (!OnDoors())
                    x_player--;
                k.Image = knightPng;
                k.Dock = System.Windows.Forms.DockStyle.Fill;
                k.BackColor = Color.Transparent;
                k.SizeMode = PictureBoxSizeMode.StretchImage;
                board.Controls.Add(k, x_player, y_player);

            }
            else if (e.KeyCode == Keys.Left)
            {
                if (x_player-1 >= 0  )
                {
                    if(table_color[x_player - 1, y_player] == 1)
                    x_player--;
                  
                }
                if (!OnDoors())
                    x_player++;
                k.Image = knight2Png;
                k.Dock = System.Windows.Forms.DockStyle.Fill;
                k.BackColor = Color.Transparent;
                k.SizeMode = PictureBoxSizeMode.StretchImage;
                board.Controls.Add(k, x_player, y_player);
            }

            else if (e.KeyCode == Keys.Up)
            {
                if (y_player-1 >= 0 )
                {
                    if( table_color[x_player, y_player - 1] == 1)
                    y_player--;
                    
                    
                }
                if (!OnDoors())
                    y_player++;
                k.Dock = System.Windows.Forms.DockStyle.Fill;
                k.BackColor = Color.Transparent;
                k.SizeMode = PictureBoxSizeMode.StretchImage;
                board.Controls.Add(k, x_player, y_player);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (y_player+1 < blocs  )
                {
                    if(table_color[x_player, y_player + 1] == 1)
                        
                    y_player++;
                    
                   
                }
                if (!OnDoors()) 
                    y_player--;
                k.Dock = System.Windows.Forms.DockStyle.Fill;
                k.BackColor = Color.Transparent;
                k.SizeMode = PictureBoxSizeMode.StretchImage;
                board.Controls.Add(k, x_player, y_player);
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (x_player + 1 < blocs)
                    table_color[x_player+1, y_player] = 1;
                if (x_player - 1 >= 0)
                    table_color[x_player-1, y_player] = 1;
                if (y_player - 1 >= 0)
                    table_color[x_player, y_player - 1] = 1;
                if (y_player + 1 < blocs)
                    table_color[x_player, y_player + 1] = 1;
                board.Refresh();
            }

            

            if (x_key==x_player && y_key ==y_player)
            {
                gotKey = true;
                board.Controls.Remove(key);
                door.Image = opened_doorPng;
            }
           

        }
        bool OnDoors()
        {
            if (x_door == x_player && y_door == y_player && gotKey)
            {
                NewGame();
                return true;
            }
            else
                return false;
        }
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Dispose();
            if (table_color[e.Column,e.Row] == 1)

                e.Graphics.FillRectangle(Brushes.ForestGreen, e.CellBounds);

            else

                e.Graphics.FillRectangle(Brushes.Maroon, e.CellBounds);



        }
    }
}
