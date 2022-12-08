namespace Needle
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.init = new System.Windows.Forms.Button();
            this.Launch = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.timerRotate = new System.Windows.Forms.Timer(this.components);
            this.timerLaunch = new System.Windows.Forms.Timer(this.components);
            this.roomhost = new System.Windows.Forms.Button();
            this.gamer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(598, 593);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // init
            // 
            this.init.Location = new System.Drawing.Point(24, 643);
            this.init.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.init.Name = "init";
            this.init.Size = new System.Drawing.Size(84, 28);
            this.init.TabIndex = 2;
            this.init.Text = "init";
            this.init.UseVisualStyleBackColor = true;
            this.init.Click += new System.EventHandler(this.init_Click);
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(290, 643);
            this.Launch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(84, 28);
            this.Launch.TabIndex = 3;
            this.Launch.Text = "Launch";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.Launch_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(425, 643);
            this.Stop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(84, 28);
            this.Stop.TabIndex = 4;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(156, 643);
            this.Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(84, 28);
            this.Start.TabIndex = 5;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // timerRotate
            // 
            this.timerRotate.Tick += new System.EventHandler(this.timerRotate_Tick);
            // 
            // timerLaunch
            // 
            this.timerLaunch.Tick += new System.EventHandler(this.timerLaunch_Tick);
            // 
            // roomhost
            // 
            this.roomhost.Location = new System.Drawing.Point(537, 614);
            this.roomhost.Name = "roomhost";
            this.roomhost.Size = new System.Drawing.Size(75, 30);
            this.roomhost.TabIndex = 6;
            this.roomhost.Text = "host";
            this.roomhost.UseVisualStyleBackColor = true;
            this.roomhost.Click += new System.EventHandler(this.roomhost_Click);
            // 
            // gamer
            // 
            this.gamer.Location = new System.Drawing.Point(537, 651);
            this.gamer.Name = "gamer";
            this.gamer.Size = new System.Drawing.Size(75, 31);
            this.gamer.TabIndex = 7;
            this.gamer.Text = "gamer";
            this.gamer.UseVisualStyleBackColor = true;
            this.gamer.Click += new System.EventHandler(this.gamer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 685);
            this.Controls.Add(this.gamer);
            this.Controls.Add(this.roomhost);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Launch);
            this.Controls.Add(this.init);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "主体";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button init;
        private System.Windows.Forms.Button Launch;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Timer timerRotate;
        private System.Windows.Forms.Timer timerLaunch;
        private System.Windows.Forms.Button roomhost;
        private System.Windows.Forms.Button gamer;
    }
}

