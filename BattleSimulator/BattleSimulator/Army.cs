using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSimulator
{
    public class Army
    {
        public List<Unit> Men = new List<Unit>();
        public int morale=100;
        public int OriginalManCount=0;
        public int Precentage = 100;

        public void Reset()
        {
            Precentage = 100;
            OriginalManCount = 0;
        }

        public Color c;

        public Army(Color c)
        {
            this.c = c;
        }


        public void AddToMen(int armor, int health, int size, int damage, int acuracy, int attackradius, int attackspeed, int dexterity, int movementspeed, int attackrange, int number, int x = 0, int y = 0)
        {
            for (int i = 0; i < number; i++)
            {
                Men.Add(new Unit(armor, health, size, damage, acuracy, attackradius, attackspeed, dexterity, movementspeed, attackrange, x, y));
            }
            OriginalManCount += number;
        }

        public void MakeRandomFormation(int BackEnd, int FrontEnd, int TerrainHeight)
        {
            for (int i = 0; i < Men.Count; i++)
            {
                Men[i].X = Engine.rnd.Next(BackEnd, FrontEnd);
                Men[i].Y = Engine.rnd.Next(TerrainHeight);
            }
        }

        public void MakeStraightFormation(int BackEndX, int FrontendX, int UpEndY, int MenInARow)
        {
            int i = 0;
            while (Men.Count > i)
            {
                for (int j = 0; j < MenInARow && i < Men.Count; j++)
                {
                    Men[i].X = FrontendX;
                    Men[i].Y = UpEndY + j * 10;
                    i++;
                }
                if (FrontendX > BackEndX)
                {
                    FrontendX -= 5;
                }
                else
                {
                    FrontendX += 5;
                }

            }
        }

        public void TryFormation(int UpperY, int LowerY, int FrontendX,int BackendX)
        {
            int mc = 0;
            int i = 0;
            while(mc<Men.Count)
            {
                Men[mc].X = FrontendX;
                Men[mc].Y = UpperY+i*10;
                i++;
                mc++;
                if (UpperY+i*10>=LowerY||UpperY+i*10>=Engine.p1.Height)
                {
                    i = 0;
                    if(BackendX-FrontendX<0)
                    {
                        FrontendX -= 10;
                    }
                    else
                    {
                        FrontendX += 10;
                    }
                }
            }
        }

        public void DrawArmy(Pen p)
        {
            for (int i = 0; i < Men.Count; i++)
            {
                Engine.grp.DrawEllipse(p, Men[i].X, Men[i].Y, Men[i].Size, Men[i].Size);
            }
        }

        public void Charge(Army Enemy)
        {
            for(int i=0;i<Men.Count;i++)
            {
                Men[i].Charge(Enemy);
            }
        }

        public void MoraleUpdate(int percentage)
        {
            foreach (Unit u in Men)
            {
                if (u.Health > 10||Precentage>100)
                {
                    u.Health = (u.Health * percentage) / 100;
                }
                u.Damage = (u.Damage * percentage) / 100;
                u.Acuracy = (u.Acuracy * percentage) / 100;
                u.AttackSpeed = (u.AttackSpeed * percentage) / 100;
                u.Dexterity = (u.Dexterity * percentage) / 100;
            }
        }

        public void CheckCasulties()
        {
            for (int i = 0; i < Men.Count; i++)
            {
                if (Men[i].Health <= 0)
                {
                    Men.RemoveAt(i);
                }
            }
            if (Men.Count * 100 / OriginalManCount < Precentage)
            {
                MoraleUpdate(100-Precentage+Men.Count*100/OriginalManCount);
                Precentage = Men.Count * 100 / OriginalManCount;
            }
        }

        public int Morale
        {
            get { return morale; }
            set
            {
                if (value > 0 && value <= 120)
                {
                    morale = value;
                }
            }
        }
    }
}
