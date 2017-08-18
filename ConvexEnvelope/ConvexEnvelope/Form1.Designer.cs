namespace ConvexEnvelope
{
    partial class Form1
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
            this.InsertPoints = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InsertPoints
            // 
            this.InsertPoints.Location = new System.Drawing.Point(12, 389);
            this.InsertPoints.Name = "InsertPoints";
            this.InsertPoints.Size = new System.Drawing.Size(121, 79);
            this.InsertPoints.TabIndex = 1;
            this.InsertPoints.Text = "button";
            this.InsertPoints.UseVisualStyleBackColor = true;
            this.InsertPoints.Click += new System.EventHandler(this.InsertPoints_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox.Location = new System.Drawing.Point(139, 13);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(613, 455);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 480);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.InsertPoints);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InsertPoints;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

