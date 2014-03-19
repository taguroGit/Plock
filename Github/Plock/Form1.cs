﻿using System;
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
            var gameData = gameForm.game;//TODO:利用したいゲームのデータクラスを登録
            gameForm.Show();

            //インタプリタの実体を生成
            gameController = new MethodsAlgorism.MethodsAlgorism(gameForm);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {  
            gameForm.game=gameController.run(textBox1.Text);
            gameForm.refreshWindow();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
