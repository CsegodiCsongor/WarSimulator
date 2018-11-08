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
    public class General
    {
        public List<Army> Armies = new List<Army>();
        public int currentTarget=0;
        public int OriginalManCount;
        public int currentMen;
        public int Precentage=0;
        public int Dead = 0;
        public General(int n, Color c)
        {
            for (int i = 0; i < n; i++)
            {
                Armies.Add(new Army(c));
            }
        }

        public void Strategy(General enemy, int n, params int[] a)
        {
            if (currentTarget < enemy.Armies.Count && enemy.Armies[a[currentTarget]].Men.Count == 0)
            {
                currentTarget++;
            }
            else if (currentTarget < enemy.Armies.Count)
            {
                Armies[n].Charge(enemy.Armies[a[currentTarget]]);
            }
        }

        public void MenNumUp()
        {
            currentMen = 0;
            for(int i=0;i<Armies.Count;i++)
            {
                currentMen += Armies[i].Men.Count;
            }
        }

        public void DeadUpdate()
        {
            Dead = 0;
            for (int i = 0; i <Armies.Count; i++)
            {
                Dead += Armies[i].Men.Count;
            }
            Dead = OriginalManCount - Dead;
        }

        public void GetBegeningManCount()
        {
            for(int i=0;i<Armies.Count;i++)
            {
                OriginalManCount += Armies[i].OriginalManCount;
            }
        }

        public void UpdateMoraleP()
        {
            Precentage = 0;
            for(int i=0;i<Armies.Count;i++)
            {
                Precentage += Armies[i].Precentage;
            }
            Precentage /= Armies.Count;
        }

        public void EffectOnArrmy(int precentage)
        {
            for(int i=0;i<Armies.Count;i++)
            { 
                Armies[i].MoraleUpdate(100 + precentage);
            }
        }
    }
}
