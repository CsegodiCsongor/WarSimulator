using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator
{
    public class Solution
    {
        public int LStartMorale;
        public int RStartMorale;
        public int LGeneralMulty=0;
        public int RGeneralMulty=0;
        public int DeadInLeft;
        public int DeadInRight;
        public float score;
        
        public void CalculateScore(int LGD,int RGD)
        {
            float p = RGD / LGD;
            if(DeadInLeft==0)
            {
                DeadInLeft = 1;
            }
            float p1 = DeadInRight / DeadInLeft;
            score= Math.Abs(p - p1);
        }

        public Solution()
        {
            LGeneralMulty = Engine.rnd.Next(20);
            RGeneralMulty = Engine.rnd.Next(20);
            LStartMorale=Engine.rnd.Next(100+LGeneralMulty);
            RStartMorale = Engine.rnd.Next(100+RGeneralMulty);
        }
    }
}
