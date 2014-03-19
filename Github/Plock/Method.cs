using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plock
{
    using GameData = DummyGameProject.GameData;

    abstract internal class Method
    {
        delegate GameData execute(GameData gameData);

        public void set(String code)
        {

        }
    }

    abstract internal class DoMethod:Method
    {
        public virtual GameData execute(GameData game)//受け取ったゲームのデータを更新して返すメソッドを実装する必要がある
        {
            throw new NotImplementedException();
        }
    }

    abstract internal class ControlMethod : Method//制御文のメソッド、currentCodeをずらすだけ
    {

    }

    abstract internal class IsMethod : Method
    {
        public virtual bool execute(GameData game)//ゲームのデータを受け取ってboolを返すメソッド
        {
            throw new NotImplementedException();
        }
    }



    class GameMethodProperty
    {
        public static Dictionary<string, Method> getMethodDictionary()
        {
            Dictionary<string, Method> methodDictionary = new Dictionary<string, Method>();
            methodDictionary.Add("もし", new IfMethod());
            methodDictionary.Add("}", new EmptyMethod());
            //////////TODO:２．ゲームで使うメソッドのクラスを辞書に登録する/////////////
            methodDictionary.Add("xを1増やす", new AddX());
            methodDictionary.Add("xを1減らす", new MinusX());
            methodDictionary.Add("ゲームの初期化", new Constructor());//コンストラクタもここで宣言する
            return methodDictionary;
        }

        public class AddX : DoMethod    //ダミーのゲームのメソッド
        {
            public override GameData execute(GameData game)
            {
                game.addX();
                return game;
            }
        }
        public class MinusX : DoMethod    //ダミーのゲームのメソッド
        {
            public override GameData execute(GameData game)
            {
                game.minusX();
                return game;
            }
        }

        public class Constructor : DoMethod    //ダミーのゲームのメソッド
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

        public class IfMethod : ControlMethod
        {
            
        }

        //何もしないメソッド。　例えば、中かっこの終わり｝とか。表示上は残しておいた方が見やすいが、何もしない行用。
        public class EmptyMethod : DoMethod
        {
            public override GameData execute(GameData game)
            {
                return game;
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
