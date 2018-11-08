using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator
{
    public class Unit
    {
        int armor;
        int health;
        int size;
        int damage;
        int acuracy;
        int attackradius;
        int attackspeed;
        int dexterity;
        int movementspeed;
        int attackrange;
        int x, y;

        public Unit Target;

        public void Attack(Unit Target)
        {
            int ThisScore = acuracy + 3 * attackradius;//0-160
            int EnemyScore = 2 * Target.Dexterity - 2 * Target.Size;//0-160
            for (int i = 0; i < this.attackspeed; i++)
            {
                if (Engine.rnd.Next(ThisScore) > Engine.rnd.Next(EnemyScore))
                {
                    Target.TakeDamage((this.damage * (101 - Target.Armor)) / 100);
                }
            }
        }

        public void GoTo(Unit Target)
        {
            float x = Target.X - this.x;
            float y = Target.Y - this.y;
            Normalize(ref x, ref y);
            //CheckCollision(Engine.LeftGeneral, Engine.RightGeneral);
            this.X += (int)(x * 10 * movementspeed);
            this.Y += (int)(y * 10 * movementspeed);

        }

        public void ChooseTarget(Army Enemy)
        {
            if (Enemy.Men.Count > 0)
            {
                List<Unit> NearEnemy = new List<Unit>();
                for (int n = 0; n < Enemy.Men.Count; n++)
                {
                    if (Enemy.Men[n].health > 0 && Dist(this, Enemy.Men[n]) < 50)
                    {
                        NearEnemy.Add(Enemy.Men[n]);
                    }
                    if (NearEnemy.Count > 0)
                    {
                        Target = NearEnemy[Engine.rnd.Next(0, NearEnemy.Count)];
                    }
                }
                if (Target == null || !Enemy.Men.Contains(Target))
                {
                    Target = Enemy.Men[Engine.rnd.Next(0, Enemy.Men.Count)];
                }
            }
        }

        public void Charge(Army Enemy)
        {
            ChooseTarget(Enemy);
            if (Enemy.Men.Contains(Target) && Target.Health > 0)
            {
                if (Dist(Target) > attackrange)
                {
                    GoTo(Target);
                }
                else
                {
                    Attack(Target);
                    for (int i = 0; i < Enemy.Men.Count; i++)
                    {
                        if (Dist(Target, Enemy.Men[i]) <= this.attackradius)
                        {
                            Attack(Enemy.Men[i]);
                            if (Enemy.Men[i].Health <= 0 && Enemy.Men.Contains(Enemy.Men[i]))
                            {
                                Enemy.Men.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public int Dist(Unit Target)
        {
            return (int)Math.Sqrt((Target.X - x) * (Target.X - x) + (Target.Y - y) * (Target.Y - y));
        }

        public int Dist(Unit Target1, Unit Target2)
        {
            return (int)Math.Sqrt((Target1.X - Target2.X) * (Target1.X - Target2.X) + (Target1.Y - Target2.Y) * (Target1.Y - Target2.Y));
        }

        void Normalize(ref float x, ref float y)
        {
            if (Math.Abs(x) >= Math.Abs(y))
            {
                y = y / Math.Abs(x);
                x = Math.Abs(x) / x;
            }
            else
            {
                x = x / Math.Abs(y);
                y = Math.Abs(y) / y;
            }
        }

        public void CheckCollision(General a,General b)
        {
            for(int i=0;i<a.Armies.Count;i++)
            {
                for(int j=0;j<a.Armies[i].Men.Count;j++)
                {
                    if(Dist(this,a.Armies[i].Men[j])<10)
                    {
                        int d = Dist(this, a.Armies[i].Men[j]);
                        int dx = a.Armies[i].Men[j].x - this.x;
                        int dy = a.Armies[i].Men[j].y - this.y;
                        a.Armies[i].Men[j].X = a.Armies[i].Men[j].X + dx*(int)((10-d)*Math.Cos(dx));
                        a.Armies[i].Men[j].Y = a.Armies[i].Men[j].Y + dy*(int)((10-d)*Math.Sin(dy));
                        //a.Armies[i].Men[j].CheckCollision(a, b);
                    }
                }
            }

            for (int i = 0; i < b.Armies.Count; i++)
            {
                for (int j = 0; j < b.Armies[i].Men.Count; j++)
                {
                    if (Dist(this, b.Armies[i].Men[j]) < 10)
                    {
                        int d = Dist(this, b.Armies[i].Men[j]);
                        int dx = b.Armies[i].Men[j].x - this.X;
                        int dy = b.Armies[i].Men[j].y - this.Y;
                        b.Armies[i].Men[j].X = b.Armies[i].Men[j].X + dx*(int)((10-d)*Math.Cos(dx));
                        b.Armies[i].Men[j].Y = b.Armies[i].Men[j].Y + dy*(int)((10-d)*Math.Sin(dy));
                        //b.Armies[i].Men[j].CheckCollision(a, b);
                    }
                }
            }
        }

        public Unit(int armor = 0, int health = 10, int size = 1, int damage = 1, int acuracy = 10, int attackradius = 1, int attackspeed = 1, int dexterity = 5, int movementspeed = 1, int attackrange = 10, int x = 0, int y = 0)
        {
            this.armor = armor;
            this.health = health;
            this.size = size;
            this.damage = damage;
            this.acuracy = acuracy;
            this.attackradius = attackradius;
            this.attackspeed = attackspeed;
            this.dexterity = dexterity;
            this.movementspeed = movementspeed;
            this.attackrange = attackrange;
            this.x = x;
            this.y = y;
        }

        public void TakeDamage(int damage)
        {
            health = health - damage;
        }

        public int Armor
        {
            get { return armor; }
            set
            {
                if (value <= 100 || value >= 0)
                {
                    armor = value;
                }
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                if (value <= 0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
        }

        public int Size
        {
            get { return size; }
            set
            {
                if (value >= 0 && value <= 30)
                {
                    size = value;
                }
            }
        }

        public int Damage
        {
            get { return damage; }
            set
            {
                if (value > 0)
                {
                    damage = value;
                }
            }

        }

        public int Acuracy
        {
            get { return acuracy; }
            set
            {
                if (value > 0 && value <= 100)
                {
                    acuracy = value;
                }
            }
        }

        public int AttackRadius
        {
            get { return attackradius; }
            set
            {
                if (value > 0)
                {
                    attackradius = value;
                }
            }
        }

        public int AttackSpeed
        {
            get { return attackspeed; }
            set
            {
                if (value <= 10 && value > 0)
                {
                    attackspeed = value;
                }
            }
        }

        public int Dexterity
        {
            get { return dexterity; }
            set
            {
                if (value < 50 && value >= 20)
                {
                    dexterity = value;
                }
            }
        }

        public int MovementSpeed
        {
            get { return movementspeed; }
            set
            {
                if (value > 0)
                {
                    movementspeed = value;
                }
            }
        }

        public int AttackRange
        {
            get { return attackrange; }
            set
            {
                if (value > 0)
                {
                    attackrange = value;
                }
            }
        }


        public int X
        {
            get { return x; }
            set
            {
                if (value < 0)
                {
                    x = 0;
                }
                else if (value > Engine.p1.Width)
                {
                    x = Engine.p1.Width;
                }
                else { x = value; }
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                if (value < 0)
                {
                    y = 0;
                }
                else if (value > Engine.p1.Height)
                {
                    y = Engine.p1.Height;
                }
                else { y = value; }
            }
        }


    }
}
