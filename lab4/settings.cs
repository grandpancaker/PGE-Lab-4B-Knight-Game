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
    public partial class settings : Form
    {
        ComboBox myComBox = new ComboBox();
        public settings()
        {
            InitializeComponent();
            addComboBox();
        }

        private void addComboBox()
        {
           
            List<MyClass> list = new List<MyClass>
            {
                new MyClass{ Id =8, Name = "8x8"},
                new MyClass{ Id =10, Name = "10x10"},
                new MyClass{ Id =12, Name = "12x12"},
            };
            
         myComBox.DataSource = list;
         myComBox.ValueMember = "Id";
        //  myComBox.SelectedIndex = 8;
            panel1.Controls.Add(myComBox);
            myComBox.Location = new System.Drawing.Point(317/2-50, 20);
        }

        private void settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.blocs =Convert.ToInt32(myComBox.SelectedValue);
            Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }

    class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
