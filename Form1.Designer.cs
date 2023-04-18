namespace CorrectEssayV2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            pathBtn = new Button();
            pathTextBox = new TextBox();
            startBtn = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            saveFileDialog1 = new SaveFileDialog();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(19, 100);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "班级文件夹:";
            // 
            // pathBtn
            // 
            pathBtn.Location = new Point(341, 96);
            pathBtn.Name = "pathBtn";
            pathBtn.Size = new Size(37, 28);
            pathBtn.TabIndex = 2;
            pathBtn.Text = "...";
            pathBtn.UseVisualStyleBackColor = true;
            pathBtn.Click += pathBtn_Click;
            // 
            // pathTextBox
            // 
            pathTextBox.Location = new Point(116, 97);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.ReadOnly = true;
            pathTextBox.Size = new Size(219, 23);
            pathTextBox.TabIndex = 1;
            pathTextBox.DoubleClick += pathTextBox_DoubleClick;
            // 
            // startBtn
            // 
            startBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            startBtn.Location = new Point(384, 96);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(90, 28);
            startBtn.TabIndex = 3;
            startBtn.Text = "开始";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(19, 30);
            label2.MaximumSize = new Size(460, 0);
            label2.Name = "label2";
            label2.Size = new Size(457, 38);
            label2.TabIndex = 4;
            label2.Text = "创建一个班级文件夹，然后创建相关学生的文件夹，将作文照片按照顺序放入学生文件夹，最后在下面的选择框中选择班级文件夹.";
            // 
            // Form1
            // 
            AcceptButton = startBtn;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 161);
            Controls.Add(label2);
            Controls.Add(startBtn);
            Controls.Add(pathTextBox);
            Controls.Add(pathBtn);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "给璐璐的作文批改器(o#゜ 曲゜)o";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button pathBtn;
        private TextBox pathTextBox;
        private Button startBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private SaveFileDialog saveFileDialog1;
        private Label label2;
    }
}