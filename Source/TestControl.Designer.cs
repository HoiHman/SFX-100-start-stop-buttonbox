namespace ButtonBoxExtension
{
    partial class TestControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestControl));
            this.changeStartStopButtonButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxJoysticks = new System.Windows.Forms.ComboBox();
            this.testBtn = new System.Windows.Forms.Button();
            this.changeDecreaseIntensity1ButtonButton = new System.Windows.Forms.Button();
            this.changeIncreaseIntensity1ButtonButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startStopDelayNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.changeIncreaseIntensity5ButtonButton = new System.Windows.Forms.Button();
            this.changeDecreaseIntensity5ButtonButton = new System.Windows.Forms.Button();
            this.changeIncreaseIntensity10ButtonButton = new System.Windows.Forms.Button();
            this.changeDecreaseIntensity10ButtonButton = new System.Windows.Forms.Button();
            this.changeIncreaseIntensity20ButtonButton = new System.Windows.Forms.Button();
            this.changeDecreaseIntensity20ButtonButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.startStopDelayNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // changeStartStopButtonButton
            // 
            this.changeStartStopButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changeStartStopButtonButton.Location = new System.Drawing.Point(3, 3);
            this.changeStartStopButtonButton.Name = "changeStartStopButtonButton";
            this.changeStartStopButtonButton.Size = new System.Drawing.Size(488, 338);
            this.changeStartStopButtonButton.TabIndex = 0;
            this.changeStartStopButtonButton.Text = "Change Start-Stop Button";
            this.changeStartStopButtonButton.UseVisualStyleBackColor = true;
            this.changeStartStopButtonButton.Click += new System.EventHandler(this.changeStartStopButtonButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 563);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "SimFeedback is turned OFF";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxJoysticks
            // 
            this.comboBoxJoysticks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxJoysticks.FormattingEnabled = true;
            this.comboBoxJoysticks.Location = new System.Drawing.Point(3, 466);
            this.comboBoxJoysticks.Name = "comboBoxJoysticks";
            this.comboBoxJoysticks.Size = new System.Drawing.Size(926, 24);
            this.comboBoxJoysticks.TabIndex = 2;
            this.comboBoxJoysticks.SelectedIndexChanged += new System.EventHandler(this.comboBoxJoysticks_SelectedIndexChanged);
            this.comboBoxJoysticks.SelectionChangeCommitted += new System.EventHandler(this.comboBoxJoysticks_SelectionChangeCommitted);
            // 
            // testBtn
            // 
            this.testBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.testBtn.Location = new System.Drawing.Point(6, 496);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(177, 60);
            this.testBtn.TabIndex = 3;
            this.testBtn.Text = "Refresh Joysticks";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // changeDecreaseIntensity1ButtonButton
            // 
            this.changeDecreaseIntensity1ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeDecreaseIntensity1ButtonButton.Location = new System.Drawing.Point(497, 3);
            this.changeDecreaseIntensity1ButtonButton.Name = "changeDecreaseIntensity1ButtonButton";
            this.changeDecreaseIntensity1ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeDecreaseIntensity1ButtonButton.TabIndex = 5;
            this.changeDecreaseIntensity1ButtonButton.Text = "Change Decrease-Intensity Button (1% intensity per click)";
            this.changeDecreaseIntensity1ButtonButton.UseVisualStyleBackColor = true;
            this.changeDecreaseIntensity1ButtonButton.Click += new System.EventHandler(this.changeDecreaseIntensity1ButtonButton_Click);
            // 
            // changeIncreaseIntensity1ButtonButton
            // 
            this.changeIncreaseIntensity1ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeIncreaseIntensity1ButtonButton.Location = new System.Drawing.Point(716, 3);
            this.changeIncreaseIntensity1ButtonButton.Name = "changeIncreaseIntensity1ButtonButton";
            this.changeIncreaseIntensity1ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeIncreaseIntensity1ButtonButton.TabIndex = 6;
            this.changeIncreaseIntensity1ButtonButton.Text = "Change Increase-Intensity Button (1% intensity per click)";
            this.changeIncreaseIntensity1ButtonButton.UseVisualStyleBackColor = true;
            this.changeIncreaseIntensity1ButtonButton.Click += new System.EventHandler(this.changeIncreaseIntensity1ButtonButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Selected Joystick:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 358);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 72);
            this.label3.TabIndex = 8;
            this.label3.Text = "For extra security fill in the minimum time between two start-stop button-clicks:" +
    "        Disable by typing in 0";
            // 
            // startStopDelayNumUpDown
            // 
            this.startStopDelayNumUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopDelayNumUpDown.Location = new System.Drawing.Point(196, 391);
            this.startStopDelayNumUpDown.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.startStopDelayNumUpDown.Name = "startStopDelayNumUpDown";
            this.startStopDelayNumUpDown.Size = new System.Drawing.Size(733, 22);
            this.startStopDelayNumUpDown.TabIndex = 9;
            this.startStopDelayNumUpDown.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.startStopDelayNumUpDown.ValueChanged += new System.EventHandler(this.startStopDelayNumUpDown_ValueChanged);
            // 
            // changeIncreaseIntensity5ButtonButton
            // 
            this.changeIncreaseIntensity5ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeIncreaseIntensity5ButtonButton.Location = new System.Drawing.Point(716, 89);
            this.changeIncreaseIntensity5ButtonButton.Name = "changeIncreaseIntensity5ButtonButton";
            this.changeIncreaseIntensity5ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeIncreaseIntensity5ButtonButton.TabIndex = 11;
            this.changeIncreaseIntensity5ButtonButton.Text = "Change Increase-Intensity Button (5% intensity per click)";
            this.changeIncreaseIntensity5ButtonButton.UseVisualStyleBackColor = true;
            this.changeIncreaseIntensity5ButtonButton.Click += new System.EventHandler(this.ChangeIncreaseIntensity5ButtonButton_Click);
            // 
            // changeDecreaseIntensity5ButtonButton
            // 
            this.changeDecreaseIntensity5ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeDecreaseIntensity5ButtonButton.Location = new System.Drawing.Point(497, 89);
            this.changeDecreaseIntensity5ButtonButton.Name = "changeDecreaseIntensity5ButtonButton";
            this.changeDecreaseIntensity5ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeDecreaseIntensity5ButtonButton.TabIndex = 10;
            this.changeDecreaseIntensity5ButtonButton.Text = "Change Decrease-Intensity Button (5% intensity per click)";
            this.changeDecreaseIntensity5ButtonButton.UseVisualStyleBackColor = true;
            this.changeDecreaseIntensity5ButtonButton.Click += new System.EventHandler(this.ChangeDecreaseIntensity5ButtonButton_Click);
            // 
            // changeIncreaseIntensity10ButtonButton
            // 
            this.changeIncreaseIntensity10ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeIncreaseIntensity10ButtonButton.Location = new System.Drawing.Point(716, 175);
            this.changeIncreaseIntensity10ButtonButton.Name = "changeIncreaseIntensity10ButtonButton";
            this.changeIncreaseIntensity10ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeIncreaseIntensity10ButtonButton.TabIndex = 13;
            this.changeIncreaseIntensity10ButtonButton.Text = "Change Increase-Intensity Button (10% intensity per click)";
            this.changeIncreaseIntensity10ButtonButton.UseVisualStyleBackColor = true;
            this.changeIncreaseIntensity10ButtonButton.Click += new System.EventHandler(this.ChangeIncreaseIntensity10ButtonButton_Click);
            // 
            // changeDecreaseIntensity10ButtonButton
            // 
            this.changeDecreaseIntensity10ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeDecreaseIntensity10ButtonButton.Location = new System.Drawing.Point(497, 175);
            this.changeDecreaseIntensity10ButtonButton.Name = "changeDecreaseIntensity10ButtonButton";
            this.changeDecreaseIntensity10ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeDecreaseIntensity10ButtonButton.TabIndex = 12;
            this.changeDecreaseIntensity10ButtonButton.Text = "Change Decrease-Intensity Button (10% intensity per click)";
            this.changeDecreaseIntensity10ButtonButton.UseVisualStyleBackColor = true;
            this.changeDecreaseIntensity10ButtonButton.Click += new System.EventHandler(this.ChangeDecreaseIntensity10ButtonButton_Click);
            // 
            // changeIncreaseIntensity20ButtonButton
            // 
            this.changeIncreaseIntensity20ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeIncreaseIntensity20ButtonButton.Location = new System.Drawing.Point(716, 261);
            this.changeIncreaseIntensity20ButtonButton.Name = "changeIncreaseIntensity20ButtonButton";
            this.changeIncreaseIntensity20ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeIncreaseIntensity20ButtonButton.TabIndex = 15;
            this.changeIncreaseIntensity20ButtonButton.Text = "Change Increase-Intensity Button (20% intensity per click)";
            this.changeIncreaseIntensity20ButtonButton.UseVisualStyleBackColor = true;
            this.changeIncreaseIntensity20ButtonButton.Click += new System.EventHandler(this.ChangeIncreaseIntensity20ButtonButton_Click);
            // 
            // changeDecreaseIntensity20ButtonButton
            // 
            this.changeDecreaseIntensity20ButtonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeDecreaseIntensity20ButtonButton.Location = new System.Drawing.Point(497, 261);
            this.changeDecreaseIntensity20ButtonButton.Name = "changeDecreaseIntensity20ButtonButton";
            this.changeDecreaseIntensity20ButtonButton.Size = new System.Drawing.Size(213, 80);
            this.changeDecreaseIntensity20ButtonButton.TabIndex = 14;
            this.changeDecreaseIntensity20ButtonButton.Text = "Change Decrease-Intensity Button (20% intensity per click)";
            this.changeDecreaseIntensity20ButtonButton.UseVisualStyleBackColor = true;
            this.changeDecreaseIntensity20ButtonButton.Click += new System.EventHandler(this.ChangeDecreaseIntensity20ButtonButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(352, 496);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(241, 518);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "By S4nder";
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.changeIncreaseIntensity20ButtonButton);
            this.Controls.Add(this.changeDecreaseIntensity20ButtonButton);
            this.Controls.Add(this.changeIncreaseIntensity10ButtonButton);
            this.Controls.Add(this.changeDecreaseIntensity10ButtonButton);
            this.Controls.Add(this.changeIncreaseIntensity5ButtonButton);
            this.Controls.Add(this.changeDecreaseIntensity5ButtonButton);
            this.Controls.Add(this.startStopDelayNumUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.changeIncreaseIntensity1ButtonButton);
            this.Controls.Add(this.changeDecreaseIntensity1ButtonButton);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.comboBoxJoysticks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeStartStopButtonButton);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(932, 585);
            this.Load += new System.EventHandler(this.TestControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TestControl_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.startStopDelayNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changeStartStopButtonButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxJoysticks;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button changeDecreaseIntensity1ButtonButton;
        private System.Windows.Forms.Button changeIncreaseIntensity1ButtonButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown startStopDelayNumUpDown;
        private System.Windows.Forms.Button changeIncreaseIntensity5ButtonButton;
        private System.Windows.Forms.Button changeDecreaseIntensity5ButtonButton;
        private System.Windows.Forms.Button changeIncreaseIntensity10ButtonButton;
        private System.Windows.Forms.Button changeDecreaseIntensity10ButtonButton;
        private System.Windows.Forms.Button changeIncreaseIntensity20ButtonButton;
        private System.Windows.Forms.Button changeDecreaseIntensity20ButtonButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
    }
}
