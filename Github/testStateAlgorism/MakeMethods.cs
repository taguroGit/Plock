using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MethodsAlgorism
{
    class MakeMethods
    {
        public BaseGameMethodProperty.MainMethod mainMethod;//自作したプログラムのエントリーポイントを持っているメソッド

        internal void makeMethods(List<string> stringList)
        {
            //前処理
            TextList sortedText = TextList.sortText(stringList);//ただの文字列のリストを、構文規則にのっとって並び替える

            mainMethod = new BaseGameMethodProperty.MainMethod();//初期化
            mainMethod.addMethodList(sortedText);//テキストのリストをメソッドのリストに置き換える
        }

        /// <summary>
        /// 文字列を渡した場合には、/r/nごとにList<string>に切り分けてから委譲する
        /// </summary>
        /// <param name="str"></param>
        internal void makeMethods(string str)
        {
            makeMethods(TextList.splitToList(str));
        }


        /// <summary>
        /// 文字列のリストとそれを操作するメソッド群
        /// </summary>
        public class TextList
        {
            public List<TextList> textList;
            public string text;


            public TextList()
            {
                textList = new List<TextList>();
            }

            public TextList(string _string)
                : this()
            {
                set(_string);
            }

            public void set(string _string)
            {
                text = _string;
            }

            public void add(TextList _textList)
            {
                textList.Add(_textList);
            }
            public void add(List<TextList> _textList)
            {
                foreach (TextList _text in _textList)
                {
                    textList.Add(_text);
                }
            }


            /// <summary>
            /// textListの要素数を返す。
            /// </summary>
            /// <returns></returns>
            internal int count()
            {
                return textList.Count;
            }

            /// <summary>
            /// 階層内部を含めたtestListの要素数を返す。
            /// </summary>
            /// <returns></returns>
            internal int countAll()
            {
                int _count = 0;
                _count += count();

                //階層内部があるなら再帰的に呼び出して探索する
                foreach (TextList _textList in textList)
                {
                    _count += _textList.countAll();
                }

                return _count;
            }

            /// <summary>
            /// 指定したindexからcount+1個後までのtextListを抜き出して返す
            /// </summary>
            /// <param name="_index"></param>
            /// <param name="_count"></param>
            /// <returns></returns>
            public List<TextList> getRange(int _index, int _count)
            {
                return textList.GetRange(_index, _count);
            }

            public TextList Last()
            {
                return textList.Last();
            }

            /// <summary>
            /// 階層のないstringのListを、制御文のスコープが明確になるような階層のあるリストの形に変換して返す
            /// </summary>
            /// <param name="stringList"></param>
            /// <returns></returns>
            public static TextList sortText(List<string> stringList)
            {
                //扱いやすい形式（リストを格納するリスト）に変換
                TextList _normalTextList = new TextList();
                foreach (var _string in stringList)
                {
                    _normalTextList.add(new TextList(_string));
                }

                //委譲する
                return divideText(_normalTextList);
            }


            /// <summary>
            /// 階層のないtextListを、制御文のスコープが明確になるような階層のあるリストの形に変換して返す
            /// </summary>
            /// <param name="_normalTextList"></param>
            /// <returns></returns>
            public static TextList divideText(TextList _normalTextList)
            {

                TextList resultList = new TextList();//返り値の宣言。ここに登録していく。
                resultList.text = _normalTextList.text;

                //全ての文を登録していく
                int textListSize = _normalTextList.count();
                for (int listIndex = 0; listIndex < textListSize; listIndex++)
                {
                    TextList _text = _normalTextList.textList[listIndex];

                    //中かっこから中かっこまで制御文として登録する。
                    if (_text.text.Contains("{"))
                    {
                        //制御文のスコープ内を階層のあるリストの形に変換して制御文として登録する。
                        _text.add(_normalTextList.getRange(listIndex + 1, textListSize - listIndex - 1));//前準備として制御文より後を取り出す
                        resultList.add(divideText(_text));//制御文のスコープ内を階層のあるリストの形に変換してから、登録する。

                        //制御文内で登録した分のインデックスを進める。
                        listIndex += resultList.Last().countAll();
                        continue;
                    }
                    else
                    {
                        //ゲームのデータを操作する文を登録する
                        resultList.add(_text);
                    }

                    if (_text.text.Contains("}"))//自分自身のスコープが終わったら、登録を終える。
                    {
                        break;
                    }
                }
                return resultList;
            }

            /// <summary>
            /// Stringを/r/nごとに区切ってList<String>に格納して返す
            /// /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static List<string> splitToList(String str){
                string[] separator = {"\r\n"};
                string[] texts = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                List<String> stringList = new List<string>();
                foreach (String text in texts)
                {
                    stringList.Add(text);
                }
                return stringList;
            }
        }
    }
}
