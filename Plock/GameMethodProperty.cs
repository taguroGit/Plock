using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plock
{
    using GameData = DummyGameProject.GameData;



    class GameMethodProperty:baseGameMethodProperty
    {
        public new static Dictionary<string, Method> getDoMethodDictionary()
        {
            Dictionary<string, Method> methodDictionary = baseGameMethodProperty.getDoMethodDictionary();
            //////////TODO:２．ゲームで使うメソッドのクラスを辞書に登録する/////////////
            methodDictionary.Add("xを1増やす", new AddX());
            methodDictionary.Add("xを1減らす", new MinusX());
            methodDictionary.Add("ゲームの初期化", new Constructor());//コンストラクタもここで宣言する
            return methodDictionary;
        }

        public class AddX : Method    //ダミーのゲームのメソッド
        {
            public override GameData execute(GameData game)
            {
                game.addX();
                return game;
            }
        }
        public class MinusX : Method    //ダミーのゲームのメソッド
        {
            public override GameData execute(GameData game)
            {
                game.minusX();
                return game;
            }
        }

        public class Constructor : Method    //ダミーのゲームのメソッド
        {
            public override GameData execute(GameData game)//GUIのプログラムから呼ぶ場合
            {
                return new GameData(3, 3);
            }

            public GameData execute()//最初の初期化
            {
                return new GameData(3, 3);
            }
        }





        //////////bool値を返すメソッドも大体同じ要領で登録する/////////////
        public static Dictionary<string, IsMethod> getIsMethodDictionary()
        {
            Dictionary<string, IsMethod> methodDictionary = new Dictionary<string, IsMethod>();

            methodDictionary.Add("xが2なら", new IsX2());

            return methodDictionary;
        }

        public class IsX2 : IsMethod
        {
            public override bool execute(GameData game)
            {
                return game.isX2();
            }
        }
    }


}
