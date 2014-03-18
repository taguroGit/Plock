using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DummyGameProject;

//このファイルにはメソッドの登録以外のことは書かない
namespace MethodsAlgorism
{

    class GameMethodProperty : BaseGameMethodProperty
    {
        Dictionary<string, DoMethod> doMethodDictionary;
        Dictionary<string, IsMethod> isMethodDictionary;

        public GameMethodProperty()
        {
            this.doMethodDictionary = getDoMethodDictionary();
            this.isMethodDictionary = getIsMethodDictionary();
        }

        public static Dictionary<string, DoMethod> getDoMethodDictionary()
        {
            Dictionary<string, DoMethod> methodDictionary = BaseGameMethodProperty.getDoMethodDictionary();

            //////////TODO:２．ゲームで使うメソッドのクラスを辞書に登録する/////////////
            methodDictionary.Add("xを1増やす", new AddX());
            methodDictionary.Add("xを1減らす", new MinusX());
            methodDictionary.Add("ゲームの初期化", new Constructor());//コンストラクタもここで宣言する
            return methodDictionary;
        }

        //////////TODO:１．ゲームで使うメソッドのクラスを書く。///////////
        //(持っているリストとかの値がいろいろあるからメソッドをDeligateにはできない)
        public class AddX : DoMethod    //ダミーのゲームのメソッド
        {
            public override DummyGame execute(DummyGame game)
            {
                game.addX();
                return game;
            }
        }
        public class MinusX : DoMethod    //ダミーのゲームのメソッド
        {
            public override DummyGame execute(DummyGame game)
            {
                game.minusX();
                return game;
            }
        }

        public class Constructor : DoMethod    //ダミーのゲームのメソッド
        {
            public override DummyGame execute(DummyGame game)//GUIのプログラムから呼ぶ場合
            {
                return new DummyGame(3, 3);
            }

            public DummyGame execute()//最初の初期化
            {
                return new DummyGame(3, 3);
            }
        }


        //////////bool値を返すメソッドも大体同じ要領で登録する/////////////
        public static Dictionary<string, IsMethod> getIsMethodDictionary()
        {
            Dictionary<string, IsMethod> methodDictionary = BaseGameMethodProperty.getIsMethodDictionary();
            
            methodDictionary.Add("xが2なら", new IsX2());

            return methodDictionary;
        }

        public class IsX2 : IsMethod
        {
            public override bool execute(DummyGame game)
            {
                return game.isX2();
            }
        }
    }

}
