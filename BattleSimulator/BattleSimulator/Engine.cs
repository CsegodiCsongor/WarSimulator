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
    public static class Engine
    {
        public enum Armyes
        {
            LeftGeneral,
            RightGeneral,
        };

        public static PictureBox p1;
        public static Label l1;
        public static Label l2;
        public static Label l3, l4;
        public static Bitmap bmp;
        public static Graphics grp;
        public static Timer t1;

        public static Army LeftArmy;
        public static Army RightArmy;

        public static General LeftGeneral;
        public static General RightGeneral;

        public static Random rnd;

        public static int TerrainWidthMid;

        public static void Init()
        {
            bmp = new Bitmap(p1.Width, p1.Height);
            grp = Graphics.FromImage(bmp);
            rnd = new Random();
            TerrainWidthMid = p1.Width / 2;
            CreateLeftGeneral(3);
            CreateRightGeneral(1);
        }

        public static void CreateLeftGeneral(int n)
        {
            LeftGeneral = new General(n, Color.Blue);
        }

        public static void CreateRightGeneral(int n)
        {
            RightGeneral = new General(n, Color.Red);
        }

        public static void CreateLeftArmy()
        {
            Color c;
            c = Color.Red;
            LeftArmy = new Army(c);
        }

        public static void CreateRightArmy()
        {
            Color c;
            c = Color.Blue;
            RightArmy = new Army(c);
        }

        public static void AddUnits(Armyes a,int armynum, int armor, int health, int size, int damage, int acuracy, int attackradius, int attackspeed, int dexterity, int movementspeed, int attackrange, int number, int x = 0, int y = 0)
        {
            if (a == Armyes.LeftGeneral)
            {
                LeftGeneral.Armies[armynum].AddToMen(armor, health, size, damage, acuracy, attackradius, attackspeed, dexterity, movementspeed, attackrange, number);
            }
            if (a == Armyes.RightGeneral)
            {
                RightGeneral.Armies[armynum].AddToMen(armor, health, size, damage, acuracy, attackradius, attackspeed, dexterity, movementspeed, attackrange, number);
            }
        }

        public static void MakeFormation()
        {
            LeftGeneral.Armies[0].TryFormation(0, p1.Height / 5, p1.Width/2 - 10, 0);
            LeftGeneral.Armies[1].TryFormation(p1.Height / 5 + 10, p1.Height*4/5, p1.Width / 2 - 10, 0);
            LeftGeneral.Armies[2].TryFormation(p1.Height*4/5+10, p1.Height, p1.Width / 2 - 10, 0);
            RightGeneral.Armies[0].TryFormation(p1.Height/5, p1.Height*4/5, p1.Width / 2 + 10, p1.Width);
        }

        public static void ChechCol()
        {
            for (int i = 0; i < Engine.LeftGeneral.Armies.Count; i++)
            {
                for (int j = 0; j < Engine.LeftGeneral.Armies[i].Men.Count; j++)
                {
                    Engine.LeftGeneral.Armies[i].Men[j].CheckCollision(Engine.LeftGeneral, Engine.RightGeneral);
                }
            }
            for (int i = 0; i < Engine.RightGeneral.Armies.Count; i++)
            {
                for (int j = 0; j < Engine.RightGeneral.Armies[i].Men.Count; j++)
                {
                    Engine.RightGeneral.Armies[i].Men[j].CheckCollision(Engine.LeftGeneral, Engine.RightGeneral);
                }
            }
        }

        public static void DrawArmyes()
        {
            bmp = new Bitmap(p1.Width, p1.Height);
            grp = Graphics.FromImage(bmp);
            for(int i=0;i<LeftGeneral.Armies.Count;i++)
            {
                LeftGeneral.Armies[i].DrawArmy(new Pen(LeftGeneral.Armies[i].c, 1));
            }
            for (int i = 0; i < RightGeneral.Armies.Count; i++)
            {
                RightGeneral.Armies[i].DrawArmy(new Pen(RightGeneral.Armies[i].c, 1));
            }
            p1.Image = bmp;
        }

        public static void Fight()
        {
            LeftGeneral.Strategy(RightGeneral, 0, 0);
            LeftGeneral.Strategy(RightGeneral, 1, 0);
            LeftGeneral.Strategy(RightGeneral, 2, 0);
            RightGeneral.Strategy(LeftGeneral, 0, 1,0,2);
            DrawArmyes();
            for(int i=0;i<LeftGeneral.Armies.Count;i++)
            {
                LeftGeneral.Armies[i].CheckCasulties();
            }
            for (int i = 0; i < RightGeneral.Armies.Count; i++)
            {
                RightGeneral.Armies[i].CheckCasulties();
            }
        }

        public static void Update()
        {
            //int lc=0;
            //for(int i=0;i<LeftGeneral.Armies.Count;i++)
            //{
            //    lc += LeftGeneral.Armies[i].Men.Count;
            //}
            //int rc=0;
            //for(int i=0;i<RightGeneral.Armies.Count;i++)
            //{
            //    rc += RightGeneral.Armies[i].Men.Count;
            //}
            //lc = LeftGeneral.OriginalManCount - lc;
            //rc = RightGeneral.OriginalManCount - rc;
            LeftGeneral.DeadUpdate();
            RightGeneral.DeadUpdate();
            l1.Text = "LeftGeneralManCount= " + LeftGeneral.Dead;
            l2.Text = "RightGeneralMAnCount= " + RightGeneral.Dead;

            LeftGeneral.UpdateMoraleP();
            RightGeneral.UpdateMoraleP();
            l3.Text = "LeftGeneralMorale= " + LeftGeneral.Precentage;
            l4.Text = "RightGeneralMorale= " + RightGeneral.Precentage;
        }

        public static void Clear()
        {
            for(int i=0;i<LeftGeneral.Armies.Count;i++)
            {
                LeftGeneral.Armies[i].Men.Clear();
                LeftGeneral.Armies[i].Reset();
            }
            for (int i = 0; i < RightGeneral.Armies.Count; i++)
            {
                RightGeneral.Armies[i].Men.Clear();
                RightGeneral.Armies[i].Reset();
            }
            LeftGeneral.currentTarget = 0;
            RightGeneral.currentTarget = 0;
        }

        public static UnitAttribute getAtribute(unitType t)
        {
            return (UnitAttribute)Attribute.GetCustomAttribute(
                typeof(unitType).GetField(Enum.GetName(typeof(unitType), t)),
                typeof(UnitAttribute));
        }

        public static Unit getUnit(unitType t)
        {
            UnitAttribute local = getAtribute(t);
            Unit r = new Unit(local.armor, local.health, local.size,local.damage,local.acuracy,local.attackradius,local.attackspeed,local.dexterity,local.movementspeed,local.attackrange,local.x,local.y);
            return r;
        }
    }
}
