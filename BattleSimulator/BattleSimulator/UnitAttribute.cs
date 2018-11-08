using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator
{
    public class UnitAttribute:Attribute
    {
        public int armor;
        public int health;
        public int size;
        public int damage;
        public int acuracy;
        public int attackradius;
        public int attackspeed;
        public int dexterity;
        public int movementspeed;
        public int attackrange;
        public int x, y;

        public UnitAttribute(int armor , int health , int size , int damage , int acuracy , int attackradius , int attackspeed , int dexterity , int movementspeed , int attackrange , int x , int y )
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
    }
}
