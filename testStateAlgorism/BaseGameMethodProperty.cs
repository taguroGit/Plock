using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DummyGameProject;

namespace MethodsAlgorism
{
    //制御文の定義
    abstract class BaseGameMethodProperty
    {
        public static Dictionary<string, DoMethod> getDoMethodDictionary()
        {
            Dictionary<string, DoMethod> methodDictionary = new Dictionary<string, DoMethod>();
            methodDictionary.Add("もし", new IfMethod());
            methodDictionary.Add("}", new EmptyMethod());
            return methodDictionary;
        }

        protected static Dictionary<string, IsMethod> getIsMethodDictionary()
        {
            Dictionary<string, IsMethod> methodDictionary = new Dictionary<string, IsMethod>();


            return methodDictionary;
        }

        ////////////////////制御文のクラスを書く/////////////////////////
        public class MainMethod : DoMethod//一番多機能なメソッドを指定しないと、登録が上手くいかない（親クラスから子クラスのメソッドを呼び出せないので）Whileとか実装の際には、登録用のMethodを新たに作る必要がある
        {
            public override GameData execute(GameData game)
            {
                foreach (var _doMethod in methodList)
                {
                    game = _doMethod.execute(game);
                }
                return game;
            }
        }

        public class IfMethod : DoMethod
        {
            public override GameData execute(GameData game)
            {
                if (isMethod.execute(game))
                {
                    foreach (DoMethod _doMethod in methodList)
                    {
                        game = _doMethod.execute(game);
                    }
                }
                return game;
            }
        }

        //何もしないメソッド。　例えば、中かっこの終わり｝とか。表示上は残しておいた方が見やすいが、何もしない行用。
        public class EmptyMethod : DoMethod
        {
            public override GameData execute(GameData game)
            {
                return game;
            }
        }

        abstract internal class DoMethod
        {
            public List<DoMethod> methodList;//中かっこの中で実行するメソッドのリストがあれば格納する
            public IsMethod isMethod;//addMethodListによるメソッドの登録の際に、こっちで宣言しておく必要がある。
            public virtual GameData execute(GameData game)//受け取ったゲームのデータを更新して返すメソッドを実装する必要がある
            {
                return game;
            }
            public DoMethod()
            {
                methodList = new List<DoMethod>();
            }


            //メソッドの生成に関するところ
            internal void addMethodList(MakeMethods.TextList dividedText)
            {
                //List持ってたらそっちを登録…と再帰的にやっていく
                foreach (MakeMethods.TextList _text in dividedText.textList)
                {
                    methodList.Add(GameMethodProperty.getDoMethodDictionary().Single(_method => _text.text.Contains(_method.Key)).Value);

                    if (_text.count() > 0)//今追加したメソッドmethodList.Last()がリストを持っていたら、
                    {
                        if (_text.text != null)
                        {
                            //boolを返す条件文を登録
                            methodList.Last().isMethod = GameMethodProperty.getIsMethodDictionary().Single(_method => _text.text.Contains(_method.Key)).Value;
                        }
                        methodList.Last().addMethodList(_text);//そいつのメソッドも再帰的に登録
                    }
                }
            }
        }


        abstract internal class IsMethod
        {
            public virtual bool execute(GameData game)//ゲームのデータを受け取ってbool値を返すメソッドを実装する必要がある
            {
                return false;
            }
        }
    }
}
