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
            build(code);

            while (true)
            {
                game = currentCode.execute(game);
                currentCode = currentCode.getMoveTo(game);
                if (currentCode.isEnd()) break;
            }
            return game;
        }

        internal GameData runOneLine(string code, GameData game)
        {
            if (currentCode.isEnd()) return game;

            game = currentCode.execute(game);
            currentCode = currentCode.getMoveTo(game);
            return game;
        }

        internal void build(string code)
        {
            currentCode = new CodeList();
            currentCode.setValue(code);
        }
    }

    /// <summary>
    /// コードの双方向リンク
    /// </summary>
    class CodeList
    {
        String _value;
        MethodWrapper value;
        CodeList nextCode;//後に実行されるコード
        CodeList previousCode;//前に実行されたコード
        CodeList collerCode;//このコードの呼び出し元
        CodeList colleeCode;//このコードの呼び出し先

        public CodeList()
        {
            value = new MethodWrapper();
            nextCode = null;
            previousCode = null;
            collerCode = null;
            colleeCode = null;
        }
        public bool isEnd()
        {
            return _value == null;
        }

        void setValue(Queue<String> codeQueue){
            
            if (codeQueue.Count == 0) return;
            
            _value = codeQueue.Dequeue();
            value.set(_value);

            if (_value.Contains("{"))
            {
                //次は、呼び出し先に値をセットする
                colleeCode = new CodeList();
                colleeCode.collerCode = this;

                colleeCode.setValue(codeQueue);
            }
            else if (_value.Contains("}"))
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

        internal GameData execute(GameData game)
        {
            return value.execute(game);
        }

        internal CodeList getMoveTo(GameData game)
        {
            if (value.isControlMethod()) return getCodeList(value.getMoveTo(game));//制御文の時は要検討（制御文による）
            if(nextCode!=null)return nextCode;//次のコードがあるときは次のコード
            return collerCode.nextCode;//次のコードがない時は要検討（コードによる）
        }

        internal CodeList getCodeList(MoveTo moveTo)
        {
            switch (moveTo)
            {
                case MoveTo.NEXT:
                    return nextCode;
                case MoveTo.COLLERNEXT:
                    return collerCode;
                case MoveTo.COLLEE:
                    return colleeCode;
            }
            return null;
        }
    }

}
