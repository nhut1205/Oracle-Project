namespace DoAn_Net
{
    partial class ThemNV
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
            label6 = new Label();
            button2 = new Button();
            button1 = new Button();
            txt_TenTaiKhoan = new TextBox();
            label7 = new Label();
            label1 = new Label();
            tenNhanVien = new TextBox();
            tenNv = new Label();
            combo_Quyen = new ComboBox();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            cbo_Gender = new ComboBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(88, 130);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 29;
            label6.Text = "Quyền";
            // 
            // button2
            // 
            button2.BackColor = Color.MediumSlateBlue;
            button2.ForeColor = Color.White;
            button2.Location = new Point(460, 247);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(102, 44);
            button2.TabIndex = 28;
            button2.Text = "Hủy";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSlateBlue;
            button1.ForeColor = Color.White;
            button1.Location = new Point(251, 247);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(97, 44);
            button1.TabIndex = 27;
            button1.Text = "Thêm";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // txt_TenTaiKhoan
            // 
            txt_TenTaiKhoan.Location = new Point(226, 53);
            txt_TenTaiKhoan.Margin = new Padding(3, 2, 3, 2);
            txt_TenTaiKhoan.Name = "txt_TenTaiKhoan";
            txt_TenTaiKhoan.Size = new Size(374, 23);
            txt_TenTaiKhoan.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(326, 16);
            label7.Name = "label7";
            label7.Size = new Size(92, 15);
            label7.TabIndex = 22;
            label7.Text = "Thêm nhân viên";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 56);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 17;
            label1.Text = "Tên đăng nhập";
            // 
            // tenNhanVien
            // 
            tenNhanVien.Location = new Point(226, 86);
            tenNhanVien.Margin = new Padding(3, 2, 3, 2);
            tenNhanVien.Name = "tenNhanVien";
            tenNhanVien.Size = new Size(374, 23);
            tenNhanVien.TabIndex = 33;
            // 
            // tenNv
            // 
            tenNv.AutoSize = true;
            tenNv.Location = new Point(88, 88);
            tenNv.Name = "tenNv";
            tenNv.Size = new Size(68, 15);
            tenNv.TabIndex = 32;
            tenNv.Text = "Tên hiển thị";
            // 
            // combo_Quyen
            // 
            combo_Quyen.FormattingEnabled = true;
            combo_Quyen.Items.AddRange(new object[] { "ad", "sl" });
            combo_Quyen.Location = new Point(226, 130);
            combo_Quyen.Margin = new Padding(3, 2, 3, 2);
            combo_Quyen.Name = "combo_Quyen";
            combo_Quyen.Size = new Size(374, 23);
            combo_Quyen.TabIndex = 34;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = SystemColors.Control;
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 30;
            iconPictureBox1.Location = new Point(0, 0);
            iconPictureBox1.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(35, 30);
            iconPictureBox1.TabIndex = 35;
            iconPictureBox1.TabStop = false;
            // 
            // cbo_Gender
            // 
            cbo_Gender.FormattingEnabled = true;
            cbo_Gender.Items.AddRange(new object[] { "Nam", "Nữ " });
            cbo_Gender.Location = new Point(226, 184);
            cbo_Gender.Margin = new Padding(3, 2, 3, 2);
            cbo_Gender.Name = "cbo_Gender";
            cbo_Gender.Size = new Size(374, 23);
            cbo_Gender.TabIndex = 36;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(88, 190);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 37;
            label2.Text = "Giới tính";
            // 
            // ThemNV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(label2);
            Controls.Add(cbo_Gender);
            Controls.Add(iconPictureBox1);
            Controls.Add(combo_Quyen);
            Controls.Add(tenNhanVien);
            Controls.Add(tenNv);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txt_TenTaiKhoan);
            Controls.Add(label7);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ThemNV";
            Text = "ThemNV";
            Load += ThemNV_Load_1;
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label6;
        private Button button2;
        private Button button1;
        private TextBox txt_TenTaiKhoan;
        private Label label7;
        private Label label1;
        private TextBox tenNhanVien;
        private Label tenNv;
        private ComboBox combo_Quyen;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private ComboBox cbo_Gender;
        private Label label2;
    }
}