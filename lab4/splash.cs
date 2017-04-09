using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class splash : Form
    {
        Timer t = new Timer();
        Timer close = new Timer();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public splash()
        {
            InitializeComponent();
            Rectangle myEllipse = new Rectangle(0, 0, 333, 333);
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(myEllipse);

            

            this.Region = new Region(myPath);

            t.Interval = 100;
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
            
            close.Interval = 3000;
            close.Tick += new EventHandler (HandleTimer);
            close.Start();
        }

        void HandleTimer(object sender, EventArgs e)
        {
           this.Hide();


        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
                this.Opacity = 1;
        }

        private void splash_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void splash_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void splash_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
