using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator
{
    public static class GeneticEngine
    {
        public  static List<Solution> s = new List<Solution>();
        static int n=10;

        public static void Init()
        {
            for(int i=0;i<n;i++)
            {
                s.Add(new Solution());
            }
        }

        public static void Fights()
        {
            for(int i=0;i<n;i++)
            {
                Engine.Clear();                       //armor, health, size, damage, acuracy, attackradius, attackspeed, dexterity, movementspeed,attackrange
                Engine.AddUnits(Engine.Armyes.LeftGeneral, 0, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 200);
                Engine.AddUnits(Engine.Armyes.LeftGeneral, 1, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 600);
                Engine.AddUnits(Engine.Armyes.LeftGeneral, 2, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 200);
                Engine.AddUnits(Engine.Armyes.RightGeneral, 0, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 2500);
                foreach (Unit u in Engine.LeftGeneral.Armies[0].Men)
                {
                    u.Target = Engine.RightGeneral.Armies[0].Men[1200];
                }
                foreach (Unit u in Engine.LeftGeneral.Armies[1].Men)
                {
                    u.Target = Engine.RightGeneral.Armies[0].Men[50];
                }
                foreach (Unit u in Engine.LeftGeneral.Armies[2].Men)
                {
                    u.Target = Engine.RightGeneral.Armies[0].Men[1300];
                }
                foreach (Unit u in Engine.RightGeneral.Armies[0].Men)
                {
                    u.Target = Engine.LeftGeneral.Armies[1].Men[30];
                }
                Engine.MakeFormation();
               // Engine.ChechCol();
                Engine.LeftGeneral.EffectOnArrmy(s[i].LStartMorale);
                Engine.RightGeneral.EffectOnArrmy(s[i].RStartMorale);
                Engine.LeftGeneral.MenNumUp();
                Engine.RightGeneral.MenNumUp();
                Engine.LeftGeneral.DeadUpdate();
                Engine.RightGeneral.DeadUpdate();
                while (Engine.LeftGeneral.Dead>19||Engine.RightGeneral.Dead>600)//Engine.LeftGeneral.Precentage>30&&Engine.RightGeneral.Precentage>30)
                {
                    Engine.Fight();
                    //Engine.ChechCol();
                    Engine.Update();
                    Engine.LeftGeneral.UpdateMoraleP();
                    Engine.RightGeneral.UpdateMoraleP();
                    Engine.LeftGeneral.MenNumUp();
                    Engine.RightGeneral.MenNumUp();
                    Engine.LeftGeneral.DeadUpdate();
                    Engine.RightGeneral.DeadUpdate();
                }
                Engine.LeftGeneral.DeadUpdate();
                s[i].DeadInLeft = Engine.LeftGeneral.Dead;
                Engine.RightGeneral.DeadUpdate();
                s[i].DeadInRight = Engine.RightGeneral.Dead;
                s[i].CalculateScore(19, 600);
            }

            s.Sort(delegate (Solution a, Solution b) { return a.score.CompareTo(b.score); });
        }

        public static void Mutate()
        {
            int aux = s[0].LGeneralMulty;
            s[0].LGeneralMulty = s[1].LGeneralMulty;
            s[1].LGeneralMulty = aux;
            aux = s[0].RGeneralMulty;
            s[0].RGeneralMulty = s[1].RGeneralMulty;
            s[1].RGeneralMulty = aux;
            s[0].LStartMorale = (s[0].LStartMorale + s[1].LStartMorale) / 2;
            s[1].RStartMorale = Engine.rnd.Next(100);
            List<Solution> p = new List<Solution>();
            p.Add(s[0]);
            p.Add(s[1]);
            s.Clear();
            s.Add(p[0]);
            s.Add(p[1]);
            for(int i=2;i<n;i++)
            {
                s.Add(new Solution());
            }
        }
    }
}
