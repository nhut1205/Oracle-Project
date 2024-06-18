namespace DoAn_Net
{
    partial class suaTT_NV
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
            label1 = new Label();
            label3 = new Label();
            label7 = new Label();
            txt_TenDangNhap = new TextBox();
            txt_TenHienThi = new TextBox();
            button2 = new Button();
            textBtxt_Email = new TextBox();
            label2 = new Label();
            label4 = new Label();
            cbo_Quyen = new ComboBox();
            btn_Luu = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 56);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 0;
            label1.Text = "Tên đăng nhập";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(51, 106);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 2;
            label3.Text = "Tên hiển thị mới";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(298, 7);
            label7.Name = "label7";
            label7.Size = new Size(133, 15);
            label7.TabIndex = 6;
            label7.Text = "Sửa thông tin nhân viên";
            // 
            // txt_TenDangNhap
            // 
            txt_TenDangNhap.Enabled = false;
            txt_TenDangNhap.Location = new Point(189, 54);
            txt_TenDangNhap.Margin = new Padding(3, 2, 3, 2);
            txt_TenDangNhap.Name = "txt_TenDangNhap";
            txt_TenDangNhap.Size = new Size(374, 23);
            txt_TenDangNhap.TabIndex = 7;
            // 
            // txt_TenHienThi
            // 
            txt_TenHienThi.Location = new Point(189, 100);
            txt_TenHienThi.Margin = new Padding(3, 2, 3, 2);
            txt_TenHienThi.Name = "txt_TenHienThi";
            txt_TenHienThi.Size = new Size(374, 23);
            txt_TenHienThi.TabIndex = 10;
            // 
            // button2
            // 
            button2.BackColor = Color.MediumSlateBlue;
            button2.ForeColor = Color.White;
            button2.Location = new Point(403, 258);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(83, 42);
            button2.TabIndex = 13;
            button2.Text = "Hủy";
            button2.UseVisualStyleBackColor = false;
            // 
            // textBtxt_Email
            // 
            textBtxt_Email.Location = new Point(189, 150);
            textBtxt_Email.Margin = new Padding(3, 2, 3, 2);
            textBtxt_Email.Name = "textBtxt_Email";
            textBtxt_Email.Size = new Size(374, 23);
            textBtxt_Email.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(51, 155);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 15;
            label2.Text = "Email mới";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 211);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 17;
            label4.Text = "Quyền mới";
            // 
            // cbo_Quyen
            // 
            cbo_Quyen.FormattingEnabled = true;
            cbo_Quyen.Location = new Point(189, 211);
            cbo_Quyen.Margin = new Padding(3, 2, 3, 2);
            cbo_Quyen.Name = "cbo_Quyen";
            cbo_Quyen.Size = new Size(374, 23);
            cbo_Quyen.TabIndex = 18;
            // 
            // btn_Luu
            // 
            btn_Luu.BackColor = Color.MediumSlateBlue;
            btn_Luu.ForeColor = Color.White;
            btn_Luu.Location = new Point(266, 258);
            btn_Luu.Margin = new Padding(3, 2, 3, 2);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(95, 42);
            btn_Luu.TabIndex = 19;
            btn_Luu.Text = "Lưu";
            btn_Luu.UseVisualStyleBackColor = false;
            btn_Luu.Click += btn_Luu_Click;
            // 
            // suaTT_NV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(btn_Luu);
            Controls.Add(cbo_Quyen);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBtxt_Email);
            Controls.Add(button2);
            Controls.Add(txt_TenHienThi);
            Controls.Add(txt_TenDangNhap);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "suaTT_NV";
            Text = "suaTT_NV";
            Load += suaTT_NV_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label7;
        private TextBox txt_TenDangNhap;
        private TextBox txt_TenHienThi;
        private Button button2;
        private TextBox textBtxt_Email;
        private Label label2;
        private Label label4;
        private ComboBox cbo_Quyen;
        private Button btn_Luu;
    }
}