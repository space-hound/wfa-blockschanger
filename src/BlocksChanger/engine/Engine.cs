using System.Drawing;
using System.Windows.Forms;
using BlocksChanger.models;

namespace BlocksChanger.engine
{
    public class Engine
    {
        private static Engine instance = null;

        public Form fm;
        public PictureBox PB;
        public Bitmap BMP;
        public Graphics GRP;

        public int W, H, dW, dH;

        private static int MARGIN = 5;
        private static int STEP = 25;

        private void adjust()
        {
            int wid = STEP * dW + MARGIN;

            this.PB.Dock = DockStyle.None;

            this.PB.Width = wid;
            this.fm.Width = wid + (this.fm.Width - W);

            int hei = STEP * dH + MARGIN;

            this.PB.Height = hei;
            this.fm.Height = hei + (this.fm.Height - H);

            this.W = this.PB.Width;
            this.H = this.PB.Height;

            this.fm.MaximumSize = new Size(this.fm.Width, this.fm.Height);
        }

        private Engine(PictureBox pb, Form fm)
        {
            this.PB = pb;
            this.fm = fm;
            this.W = this.PB.Width;
            this.H = this.PB.Height;

            this.dW = (this.W / STEP) - MARGIN;
            this.dH = (this.H / STEP) - MARGIN;

            BMP = new Bitmap(this.W, this.H);
            GRP = Graphics.FromImage(BMP);

            this.adjust();

            this.R();
        }

        public static Engine Load(PictureBox pb, Form fm)
        {
            if (instance is null)
            {
                instance = new Engine(pb, fm);
            }

            return instance;
        }

        public void R()
        {
            this.PB.Image = this.BMP;
        }

        public void C()
        {
            this.GRP.Clear(this.PB.BackColor);
            this.R();
        }

        public BoxCluster boxes;

        public void Init()
        {
            boxes = new BoxCluster(STEP, MARGIN, dW, dH);
            boxes.DRAW(this);
        }
    }
}
