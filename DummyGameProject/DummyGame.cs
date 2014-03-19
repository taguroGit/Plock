using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DummyGameProject
{
    //ゲーム作りの作業箇所はこのクラスと、gameMethodPropertyだけで完結する
    public class GameData
    {
        public int x;
        int y;

        public GameData(int _x,int _y){
            x = _x;
            y = _y;
        }
        public void addX(){
            x++;
        }
        public void minusX()
        {
            x--;
        }
        public bool isX2()
        {
            return x == 2;
        }
    }
}
