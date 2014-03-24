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
            method = GameMethodProperty.getDoMethodDictionary().Single(_method => code.Contains(_method.Key)).Value;
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
            return method.getMoveTo(game);
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


    class baseGameMethodProperty
    {
        public static Dictionary<string, Method> getDoMethodDictionary()
        {
            Dictionary<string, Method> methodDictionary = new Dictionary<string, Method>();
            methodDictionary.Add("もし", new IfMethod());
            methodDictionary.Add("}", new EmptyMethod());
            return methodDictionary;
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
    }
    public enum MoveTo
    {
        NEXT, COLLERNEXT, COLLEE
    }
}
