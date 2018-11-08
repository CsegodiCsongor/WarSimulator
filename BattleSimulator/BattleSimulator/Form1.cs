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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void ChechCol()
        {
            for(int i=0;i<Engine.LeftGeneral.Armies.Count;i++)
            {
                for(int j=0;j<Engine.LeftGeneral.Armies[i].Men.Count;j++)
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
  
        public void Clasic()
        {
            Engine.Clear();                       //armor, health, size, damage, acuracy, attackradius, attackspeed, dexterity, movementspeed,attackrange
            Engine.AddUnits(Engine.Armyes.LeftGeneral,0, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 200);
            Engine.AddUnits(Engine.Armyes.LeftGeneral, 1, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 600);
            Engine.AddUnits(Engine.Armyes.LeftGeneral, 2, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 200);
            Engine.AddUnits(Engine.Armyes.RightGeneral,0, 10, 10, 10, 5, 10, 10, 1, 10, 1, 10, 2500);
            foreach(Unit u in Engine.LeftGeneral.Armies[0].Men)
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
            ChechCol();
            Engine.DrawArmyes();
        }

        public void Scalc()
        {
            GeneticEngine.Init();
            for(int i=0;i<10;i++)
            {
                GeneticEngine.Fights();
                GeneticEngine.Mutate();
            }
            GeneticEngine.Fights();
            MessageBox.Show(GeneticEngine.s[0].LStartMorale+GeneticEngine.s[0].LGeneralMulty+"  "+GeneticEngine.s[0].RStartMorale + GeneticEngine.s[0].RGeneralMulty);
            MessageBox.Show(GeneticEngine.s[0].score + " " + GeneticEngine.s[1].score);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.p1 = pictureBox1;
            Engine.l1 = label2;
            Engine.l2 = label3;
            Engine.l3 = label4;
            Engine.l4 = label5;
            Engine.t1 = timer1;
            Engine.Init();
            Clasic();
            Engine.LeftGeneral.GetBegeningManCount();
            Engine.RightGeneral.GetBegeningManCount();
            Scalc();
            Engine.LeftGeneral.EffectOnArrmy(GeneticEngine.s[0].LStartMorale + GeneticEngine.s[0].LGeneralMulty);
            Engine.RightGeneral.EffectOnArrmy(GeneticEngine.s[0].RStartMorale + GeneticEngine.s[0].RGeneralMulty);
            label1.Text = "Interval: " + timer1.Interval;
            Engine.Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.Fight();
            Engine.ChechCol();
            Engine.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.Fight();
            ChechCol();
            Engine.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(richTextBox1.Text);
            label1.Text = "Interval: " + timer1.Interval;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clasic();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChechCol();
            Engine.DrawArmyes();
        }
    }
}
