using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plock
{
    using GameData = DummyGameProject.GameData;

    public class MethodWrapper
    {
        Method method;//execute(ゲームの更新用)
        IsMethod isMethod;//getMoveTo(次のcurrentCodeの場所の取得用)
        public MethodWrapper()
        {
            method = null;
            isMethod = null;
        }

        public void set(String code)
        {
            method=GameMethodProperty.getDoMethodDictionary().Single(_method => code.Contains(_method.Key)).Value;
            method.set(code);
        }
        public GameData execute(GameData game)
        {
            return method.execute(game);
        }

        /// <summary>
        /// 次のcurrentCodeの場所を返す
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public MoveTo getMoveTo(GameData game)
        {
            return    method.getMoveTo(game);
        }

        public bool isControlMethod()
        {
            return method is ControlMethod;
        }
    }

    abstract internal class Method
    {
        public virtual GameData execute(GameData game)//受け取ったゲームのデータを更新して返すメソッドを実装する必要がある
        {
            throw new NotImplementedException();
        }

        public virtual MoveTo getMoveTo(GameData game)
        {
            return MoveTo.NEXT;//通常はNEXT
        }

        internal virtual void set(string code)
        {

        }
    }

    abstract internal class ControlMethod : Method//制御文のメソッド
    {
        internal IsMethod isMethod;
        public override GameData execute(GameData game)
        {
            return game;
        }

        /// <summary>
        /// フィールドを設定する
        /// </summary>
        /// <param name="code"></param>
        internal override void set(string code)
        {
            isMethod = GameMethodProperty.getIsMethodDictionary().Single(_method => code.Contains(_method.Key)).Value;
        }
    }

    abstract internal class IsMethod
    {
        public virtual bool execute(GameData game)//ゲームのデータを受け取ってboolを返すメソッド
        {
            throw new NotImplementedException();
        }
    }



    class GameMethodProperty
    {
        public static Dictionary<string, Method> getDoMethodDictionary()
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

        public class IfMethod : ControlMethod
        {
            public override MoveTo getMoveTo(GameData game)
            {
                if (isMethod.execute(game)) return MoveTo.COLLEE;
                return MoveTo.NEXT;
            }
        }

        //何もしないメソッド。　例えば、中かっこの終わり｝とか。表示上は残しておいた方が見やすいが、何もしない行用。
        public class EmptyMethod : Method
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

    public enum MoveTo
    {
        NEXT,COLLERNEXT,COLLEE
    }
}
