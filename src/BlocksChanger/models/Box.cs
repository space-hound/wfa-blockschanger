using System.Drawing;

namespace BlocksChanger.models
{
    public class Box
    {

        public class Position
        {
            private int i;
            private int j;

            public int I { get => i; set => i = value; }
            public int J { get => j; set => j = value; }

            public Position(int _i, int _j)
            {
                this.I = _i;
                this.J = _j;
            }
        }
        public enum Kind
        {
            INACTIVE,
            ACTIVE,
            ENEMY
        }

        private static Color[] COLORS =
        {
            Color.Black,
            Color.Goldenrod,
            Color.Red
        };

        private Position pos;
        public Position Pos
        {
            get => pos;
            set => pos = value;
        }

        private Point loc;
        public Point Loc
        {
            get => loc;
            set => loc = value;
        }

        private int w, h;
        public int W
        {
            get => w;
            set => w = value;
        }
        public int H
        {
            get => h;
            set => h = value;
        }


        private static int BORDER_THICK = 1;
        private static Pen BORDER = new Pen(Color.DarkRed, BORDER_THICK);
        private SolidBrush FILL;

        private Kind knd;
        public Kind Knd
        {
            get => knd;
            set
            {
                knd = value;
                this.setFillDependentKind();
            }
        }
        private void setFillDependentKind()
        {
            this.FILL = new SolidBrush(COLORS[(int)this.knd]);
        }

        public Box(Point _loc, int _w, int _h, int kind, Position _pos)
        {
            this.loc = _loc;

            this.w = _w;
            this.h = _h;

            this.Knd = kind == 0 ? Kind.INACTIVE : Kind.ACTIVE;

            this.FILL = new SolidBrush(COLORS[kind]);

            this.Pos = _pos;
        }

        private bool IsPointInside(Point p)
        {
            bool c1 = (p.X >= this.loc.X) && (p.X <= (this.loc.X + this.w));
            bool c2 = (p.Y >= this.loc.Y) && (p.Y <= (this.loc.Y + this.h));

            return c1 && c2;
        }

        public void CLICK(engine.Engine e, Point p, Box inept)
        {
            if (this.IsPointInside(p))
            {
                if ((int)this.Knd == 1)
                {
                    this.Knd = Kind.INACTIVE;
                    inept.Knd = Kind.ACTIVE;
                }
            }
        }

        public void FALL(engine.Engine e)
        {

        }

        public void HOVER(Point p, engine.Engine e)
        {
        }

        public void DRAW(engine.Engine e)
        {
            if ((int)this.Knd != 0)
            {
                e.GRP.FillRectangle(FILL, loc.X, loc.Y, w, h);
                e.GRP.DrawRectangle(BORDER, loc.X, loc.Y, w, h);
                e.R();
            }
            else
            {
                e.GRP.FillRectangle(FILL, loc.X, loc.Y, w, h);
                //e.GRP.DrawRectangle(BORDER, loc.X, loc.Y, w, h);
                e.R();
            }
        }
    }
}
