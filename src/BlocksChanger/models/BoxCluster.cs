using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BlocksChanger.models
{
    public class BoxCluster
    {
        private MatrixGenerator matrix;
        private int NO;

        private int FLOOR;
        private int CEIL;

        public List<Box> Cluster;
        public List<Box> MiniCluster;

        public List<Box> Enemy;

        public List<Box> Hero;

        public int SCORE = 1;

        public void getHero()
        {
            Hero = new List<Box>();
            foreach (Box b in Cluster)
            {
                if ((int)b.Knd == 0)
                {
                    Hero.Add(b);
                }
            }
        }

        public void addHero()
        {
            int x = Hero[Hero.Count - 1].Pos.I;
            int y = Hero[Hero.Count - 1].Pos.J;

            if (x + 1 < NO)
            {
                ClusterBox[x + 1, y].Knd = Box.Kind.INACTIVE;
                Hero.Add(ClusterBox[x + 1, y]);
            }
            else if (x - 1 >= 0)
            {
                ClusterBox[x - 1, y].Knd = Box.Kind.INACTIVE;
                Hero.Add(ClusterBox[x - 1, y]);
            }


        }

        public Box[,] ClusterBox;

        public Box INEPTS;

        public void generateEnemy()
        {
            int c = MatrixGenerator.rnd.Next(0, NO - 1);

            this.ClusterBox[c, 1].Knd = Box.Kind.ENEMY;
            Enemy.Add(this.ClusterBox[c, 1]);
        }

        private void REPLACE(Box a, Box b)
        {
            Box temp = a;
            a = b;
            b = temp;
        }

        public void FALL(engine.Engine e)
        {

            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].Pos.J + 1 <= NO - 1)
                {

                    if ((int)ClusterBox[Enemy[i].Pos.I, Enemy[i].Pos.J + 1].Knd == 0)
                    {
                        Enemy[i].Knd = Box.Kind.ACTIVE;
                        Enemy.RemoveAt(i);
                        SCORE++;

                        if (SCORE % 2 == 0)
                        {
                            addHero();
                        }
                    }
                    else
                    {
                        Box enemy = Enemy[i];
                        Box newEnemy = ClusterBox[enemy.Pos.I, enemy.Pos.J + 1];

                        enemy.Knd = Box.Kind.ACTIVE;
                        newEnemy.Knd = Box.Kind.ENEMY;

                        Enemy.RemoveAt(i);
                        Enemy.Insert(0, newEnemy);

                    }
                }
            }


        }



        public BoxCluster(int no, int margin, int w, int h)
        {
            this.load(no, margin, w, h);
            this.NO = no;

            this.CEIL = 0;

            Enemy = new List<Box>();
        }

        private void load(int no, int margin, int w, int h)
        {
            Cluster = new List<Box>();

            ClusterBox = new Box[no, no];

            this.matrix = new MatrixGenerator(no);
            int max = no * no;
            int k = 0;

            for (int i = 0; i < no; i++)
            {
                for (int j = 0; j < no; j++)
                {
                    int kind = this.matrix.Matrix[i, j] == 0 ? 0 : 1;
                    int x = (i * w) + margin;
                    int y = (j * h) + margin;

                    Cluster.Add(new Box(new Point(x, y), w - margin, h - margin, kind, new Box.Position(i, j)));
                    ClusterBox[i, j] = Cluster[k];
                    k++;
                }
            }

            Hero = new List<Box>();
            getHero();
        }

        private Box.Position getInact()
        {
            foreach (Box b in Cluster)
            {
                if ((int)b.Knd == 0)
                {
                    INEPTS = b;
                    return b.Pos;
                }
            }

            return new Box.Position(-1, -1);
        }

        public void miniCluster()
        {
            MiniCluster = new List<Box>();
            Box.Position ps = this.getInact();

            if (ps.I - 1 >= 0 && ps.J - 1 >= CEIL)
            {
                MiniCluster.Add(ClusterBox[ps.I - 1, ps.J - 1]);
            }
            if (ps.I + 1 <= (NO - 1) && ps.J + 1 <= (NO - 1))
            {
                MiniCluster.Add(ClusterBox[ps.I + 1, ps.J + 1]);

            }

            if (ps.I - 1 >= 0 && ps.J + 1 <= (NO - 1))
            {
                MiniCluster.Add(ClusterBox[ps.I - 1, ps.J + 1]);
            }
            if (ps.I + 1 <= (NO - 1) && ps.J - 1 >= CEIL)
            {
                MiniCluster.Add(ClusterBox[ps.I + 1, ps.J - 1]);
            }

            if (ps.I - 1 >= 0)
            {
                MiniCluster.Add(ClusterBox[ps.I - 1, ps.J]);
            }
            if (ps.J - 1 >= CEIL)
            {
                MiniCluster.Add(ClusterBox[ps.I, ps.J - 1]);
            }
            if (ps.I + 1 <= (NO - 1))
            {
                MiniCluster.Add(ClusterBox[ps.I + 1, ps.J]);
            }
            if (ps.J + 1 <= (NO - 1))
            {
                MiniCluster.Add(ClusterBox[ps.I, ps.J + 1]);
            }

        }

        public void DRAW(engine.Engine e)
        {
            foreach (Box b in Cluster)
            {
                b.DRAW(e);
            }
        }

        public void CLICK(engine.Engine e, Point p)
        {
            miniCluster();

            foreach (Box b in MiniCluster)
            {
                b.CLICK(e, p, INEPTS);
            }

            e.C();
            this.DRAW(e);
        }

        public void remEnemyAt(Box.Position pos)
        {
            for (int i = 0; i < Enemy.Count; i++)
            {
                if (Enemy[i].Pos.I == pos.I && Enemy[i].Pos.J == pos.J)
                {
                    Enemy.RemoveAt(i);
                    SCORE++;
                    return;
                }
            }
        }

        public void remHeroAt(Box.Position pos)
        {
            for (int i = 0; i < Hero.Count; i++)
            {
                if (Hero[i].Pos.I == pos.I && Hero[i].Pos.J == pos.J)
                {
                    Hero.RemoveAt(i);
                    return;
                }
            }
        }

        public void KEYPRESS(engine.Engine e, KeyEventArgs key)
        {
            //Box.Position ps = this.getInact();

            //getHero();


            if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up || key.KeyCode == Keys.NumPad8)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k];
                    Box INEPT = b;
                    Box.Position ps = b.Pos;

                    if (ps.J - 1 >= CEIL)
                    {
                        if ((int)ClusterBox[ps.I, ps.J - 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I, ps.J - 1));
                            ClusterBox[ps.I, ps.J - 1].Knd = Box.Kind.ACTIVE;
                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I, ps.J - 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I, ps.J - 1]);

                    }

                }

            }
            if (key.KeyCode == Keys.S || key.KeyCode == Keys.Down || key.KeyCode == Keys.NumPad2)
            {

                for (int k = 0; k < Hero.Count; k++)
                {

                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;
                    if (ps.J + 1 <= NO - 1)
                    {
                        if ((int)ClusterBox[ps.I, ps.J + 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I, ps.J + 1));
                            ClusterBox[ps.I, ps.J + 1].Knd = Box.Kind.ACTIVE;
                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I, ps.J + 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I, ps.J + 1]);
                    }
                }

            }
            if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left || key.KeyCode == Keys.NumPad4)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;

                    if (ps.I - 1 >= 0 && (ClusterBox[ps.I - 1, ps.J].Knd != Box.Kind.INACTIVE))
                    {
                        if ((int)ClusterBox[ps.I - 1, ps.J].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I - 1, ps.J));
                            ClusterBox[ps.I - 1, ps.J].Knd = Box.Kind.ACTIVE;
                            SCORE++;
                        }


                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I - 1, ps.J].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I - 1, ps.J]);

                    }
                }
            }
            if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right || key.KeyCode == Keys.NumPad6)
            {
                for (int k = Hero.Count - 1; k >= 0; k--)
                {
                    Box b = Hero[k];
                    Box INEPT = b;
                    Box.Position ps = b.Pos;

                    if (ps.I + 1 < NO && (ClusterBox[ps.I + 1, ps.J].Knd != Box.Kind.INACTIVE))
                    {
                        if ((int)ClusterBox[ps.I + 1, ps.J].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I + 1, ps.J));
                            ClusterBox[ps.I + 1, ps.J].Knd = Box.Kind.ACTIVE;
                            SCORE++;
                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I + 1, ps.J].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I + 1, ps.J]);
                    }
                }
            }

            if (key.KeyCode == Keys.NumPad7)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;
                    if (ps.I - 1 >= 0 && ps.J - 1 >= CEIL)
                    {
                        if ((int)ClusterBox[ps.I - 1, ps.J - 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I - 1, ps.J - 1));
                            ClusterBox[ps.I - 1, ps.J - 1].Knd = Box.Kind.ACTIVE;

                        }
                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I - 1, ps.J - 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I - 1, ps.J - 1]);
                    }
                }
            }

            if (key.KeyCode == Keys.NumPad3)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;
                    if (ps.I + 1 <= NO - 1 && ps.J + 1 <= NO - 1)
                    {
                        if ((int)ClusterBox[ps.I + 1, ps.J + 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I + 1, ps.J + 1));
                            ClusterBox[ps.I + 1, ps.J + 1].Knd = Box.Kind.ACTIVE;

                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I + 1, ps.J + 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I + 1, ps.J + 1]);
                    }
                }
            }

            if (key.KeyCode == Keys.NumPad9)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;
                    if (ps.I + 1 <= NO - 1 && ps.J - 1 >= CEIL)
                    {
                        if ((int)ClusterBox[ps.I + 1, ps.J - 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I + 1, ps.J - 1));
                            ClusterBox[ps.I + 1, ps.J - 1].Knd = Box.Kind.ACTIVE;

                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I + 1, ps.J - 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I + 1, ps.J - 1]);
                    }
                }
            }

            if (key.KeyCode == Keys.NumPad1)
            {
                for (int k = 0; k < Hero.Count; k++)
                {
                    Box b = Hero[k]; Box INEPT = b;
                    Box.Position ps = b.Pos;
                    if (ps.I - 1 >= 0 && ps.J + 1 <= NO - 1)
                    {
                        if ((int)ClusterBox[ps.I - 1, ps.J + 1].Knd == 2)
                        {
                            remEnemyAt(new Box.Position(ps.I - 1, ps.J + 1));
                            ClusterBox[ps.I - 1, ps.J + 1].Knd = Box.Kind.ACTIVE;

                        }

                        INEPT.Knd = Box.Kind.ACTIVE;
                        //remHeroAt(INEPT.Pos);
                        ClusterBox[ps.I - 1, ps.J + 1].Knd = Box.Kind.INACTIVE;
                        //Hero.Add(ClusterBox[ps.I - 1, ps.J + 1]);
                    }
                }
            }

            getHero();
            e.C();
            this.DRAW(e);

        }

        public void HOVER(engine.Engine e, Point p)
        {
            foreach (Box b in Cluster)
            {
                b.HOVER(p, e);
            }
        }
    }
}
