namespace BlocksChanger
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainMap = new System.Windows.Forms.PictureBox();
            this.loop = new System.Windows.Forms.Timer(this.components);
            this.mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMap)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Controls.Add(this.mainMap, 0, 0);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(11, 11);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 1;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(1330, 1058);
            this.mainLayoutPanel.TabIndex = 1;
            // 
            // mainMap
            // 
            this.mainMap.BackColor = System.Drawing.Color.Black;
            this.mainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMap.Location = new System.Drawing.Point(3, 3);
            this.mainMap.Name = "mainMap";
            this.mainMap.Size = new System.Drawing.Size(1324, 1052);
            this.mainMap.TabIndex = 1;
            this.mainMap.TabStop = false;
            this.mainMap.Click += new System.EventHandler(this.mainMap_Click);
            // 
            // loop
            // 
            this.loop.Interval = 500;
            this.loop.Tick += new System.EventHandler(this.loop_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(110F, 110F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1352, 1080);
            this.Controls.Add(this.mainLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(11);
            this.ShowIcon = false;
            this.Text = "Blocks Changer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.mainLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.PictureBox mainMap;
        private System.Windows.Forms.Timer loop;
    }
}

