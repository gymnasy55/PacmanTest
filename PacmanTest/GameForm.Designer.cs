namespace PacmanTest
{
    sealed partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.pcb = new System.Windows.Forms.PictureBox();
            this.tmrRestart = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcb)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrGame
            // 
            this.tmrGame.Enabled = true;
            this.tmrGame.Interval = 66;
            this.tmrGame.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // pcb
            // 
            this.pcb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcb.Location = new System.Drawing.Point(0, 0);
            this.pcb.Name = "pcb";
            this.pcb.Size = new System.Drawing.Size(800, 450);
            this.pcb.TabIndex = 0;
            this.pcb.TabStop = false;
            this.pcb.Paint += new System.Windows.Forms.PaintEventHandler(this.pcb_Paint);
            // 
            // tmrRestart
            // 
            this.tmrRestart.Interval = 1000;
            this.tmrRestart.Tick += new System.EventHandler(this.tmrRestart_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pcb);
            this.Name = "GameForm";
            this.Text = "Pacman";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pcb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.PictureBox pcb;
        private System.Windows.Forms.Timer tmrRestart;
    }
}

