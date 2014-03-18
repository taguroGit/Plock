using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DummyGameProject
{
    public partial class Form1 : Form
    {
        public DummyGameProject.DummyGame game;

        public Form1()
        {
            InitializeComponent();
            game= new DummyGameProject.DummyGame(3,3);

            textBox1.Text = game.x.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.addX();

            textBox1.Text = game.x.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.minusX();

            textBox1.Text = game.x.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            game = new DummyGameProject.DummyGame(3, 3);

            textBox1.Text = game.x.ToString();
        }

        public void refreshTextBox1()
        {
            textBox1.Text = game.x.ToString();
        }

        public void refreshWindow()
        {
            refreshTextBox1();
        }
    }
}
