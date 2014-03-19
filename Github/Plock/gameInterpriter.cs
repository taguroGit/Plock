using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plock
{
    using GameData = DummyGameProject.GameData;//TODO:利用したいゲームのデータクラスを登録

    class GameInterpriter
    {
        CodeList currentCode;

        public GameData run(String code, GameData game)
        {
            currentCode = new CodeList();
            //statements.makeMethods(code);          //実行用のメソッドのリストを作る
            //game = statements.mainMethod.execute(game);//メソッドのリストを実行する
            currentCode.setValue(code);
            return game;
        }
    }

    /// <summary>
    /// コードの双方向リンク
    /// </summary>
    class CodeList
    {
        String value;
        DoMethod method;
        CodeList nextCode;//後に実行されるコード
        CodeList previousCode;//前に実行されたコード
        CodeList collerCode;//このコードの呼び出し元
        CodeList colleeCode;//このコードの呼び出し先

        public CodeList()
        {
            nextCode = null;
            previousCode = null;
            collerCode = null;
            colleeCode = null;
        }

        void setValue(Queue<String> codeQueue){
            
            if (codeQueue.Count == 0) return;
            
            value = codeQueue.Dequeue();

            if (value.Contains("{"))
            {
                //次は、呼び出し先に値をセットする
                colleeCode = new CodeList();
                colleeCode.collerCode = this;

                colleeCode.setValue(codeQueue);
            }
            else if (value.Contains("}"))
            {
                //次は、呼び出し元の後のコードに値をセットする
                collerCode.nextCode = new CodeList();
                collerCode.nextCode.previousCode = collerCode;
                collerCode.nextCode.collerCode = collerCode.collerCode;

                collerCode.nextCode.setValue(codeQueue);
            }
            else
            {
                //次は、後のコードに値をセットする
                nextCode = new CodeList();
                nextCode.previousCode = this;
                nextCode.collerCode = this.collerCode;

                nextCode.setValue(codeQueue);
            }
        }

        public void setValue(String code)
        {
            var codeQueue = splitToQueue(code);
            setValue(codeQueue);
        }

        /// <summary>
        /// コードを/r/nごとに区切って、順次キューに格納して返す
        /// /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Queue<string> splitToQueue(String str)
        {
            string[] separator = { "\r\n" };
            string[] texts = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Queue<String> codeQueue = new Queue<string>();
            foreach (String text in texts)
            {
                codeQueue.Enqueue(text);
            }
            return codeQueue;
        }
    }

}
