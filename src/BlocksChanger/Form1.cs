using System;
using System.Windows.Forms;
using BlocksChanger.engine;

namespace BlocksChanger
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static Engine en;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            en = Engine.Load(this.mainMap, this);
            en.Init();
        }

        private void mainMap_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            
            if(!pause)
            {
                en.boxes.CLICK(en, me.Location);
            }
        }

        private void mainMap_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(!started)
                {
                    loop.Start();
                    loop.Interval = 200;
                    started = true;

                }
                else
                {
                    pause = !pause;
                }
            }

            if(!pause)
            {
                en.boxes.KEYPRESS(en, e);
            }
        }

        static int at = 0;
        static bool pause = false;
        static bool started = false;

        private void loop_Tick(object sender, EventArgs e)
        {

            if(!pause)
            {
                en.C();
                en.boxes.DRAW(en);

                en.boxes.FALL(en);

                if (at % 9 == 0)
                {
                    en.boxes.generateEnemy();
                }

                at++;
            }

        }
    }
}
