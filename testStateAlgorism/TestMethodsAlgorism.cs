using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DummyGameProject;

namespace MethodsAlgorism
{
    using GameData = DummyGameProject.GameData;//TODO:利用したいゲームのデータクラスを登録

    public class MethodsAlgorism
    {


        public static void Main(string[] args)
        {
            //////////////////ゲームの初期設定///////////////////////////////////////////
            GameData game = new GameMethodProperty.Constructor().execute();

            //////////////////GUIによるプログラムの入力///////////////////////////////////////////
            List<string> dummy =new List<string>();
            
            //ダミーの入力テキスト
            dummy.Add("ゲームの初期化　3,3");//引数は「,」の両隣にスペースを挟まずに書く。0～9までの値のみ可。今のところ。

            dummy.Add("xを1減らす 0");
            dummy.Add("xを1増やす 1");
            dummy.Add("xを1減らす 2");
            
            //ifのテスト
            //dummy.Add("もしxが2なら{ 3");
            //    dummy.Add("xを1減らす 3-0");
            //    dummy.Add("xを1減らす 3-1");
            //    dummy.Add("} 3-2");

            //dummy.Add("もしxが2なら{ 4");
            //    dummy.Add("xを1減らす 4-0");
            //    dummy.Add("} 4-1");

            //ifのテスト
            dummy.Add("もしxが2なら{ 5");
                dummy.Add("xを1減らす 5-0");
                dummy.Add("xを1増やす 5-1");
                dummy.Add("もしxが2なら{ 5-2");
                    dummy.Add("xを1減らす 5-2-0");
                    dummy.Add("xを1減らす 5-2-1");
                    dummy.Add("} 5-2-2");
                dummy.Add("xを1減らす 5-3");
                dummy.Add("} 5-4");

            //////////////////実行可能なプログラムの生成////////////////////////////////////////

            MakeMethods statements = new MakeMethods();
            statements.makeMethods(dummy);          //実行用のメソッドのリストを作る
            game=statements.mainMethod.execute(game);//メソッドのリストを実行する
        }

        public GameData run(String code,GameData game)
        {
            MakeMethods statements = new MakeMethods();
            statements.makeMethods(code);          //実行用のメソッドのリストを作る
            game = statements.mainMethod.execute(game);//メソッドのリストを実行する

            return game;
        }

    }
}
