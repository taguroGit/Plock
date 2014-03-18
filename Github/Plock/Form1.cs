using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MethodsAlgorism;
using DummyGameProject;

namespace Plock
{
    public partial class Form1 : Form
    {
        DummyGameProject.Form1 gameForm;
        MethodsAlgorism.MethodsAlgorism gameController;

        public Form1()
        {
            InitializeComponent();
            
            //ゲームのウィンドウを登録
            gameForm = new DummyGameProject.Form1();
            gameForm.Show();

            //インタプリタの実態
            gameController = new MethodsAlgorism.MethodsAlgorism(gameForm);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string[] dummy=new string[1];
            //MethodsAlgorism.MethodsAlgorism.Main(dummy);

           
            gameForm.game=gameController.run(textBox1.Text);
            gameForm.refreshWindow();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
