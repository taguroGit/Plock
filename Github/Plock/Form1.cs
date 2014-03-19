using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plock
{
    using GameForm = DummyGameProject.Form1;//TODO:利用したいゲームのFormを登録

    public partial class Form1 : Form
    {
        GameForm gameForm;

        MethodsAlgorism.MethodsAlgorism gameController;

        public Form1()
        {
            InitializeComponent();
            
            //ゲームのFormのインスタンスを生成
            gameForm = new GameForm();
            gameForm.Show();

            //インタプリタの実体を生成
            gameController = new MethodsAlgorism.MethodsAlgorism();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ゲームのデータクラスの更新
            gameForm.game = gameController.run(textBox1.Text, gameForm.game);
            //ゲームの表示の更新
            gameForm.refreshWindow();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
