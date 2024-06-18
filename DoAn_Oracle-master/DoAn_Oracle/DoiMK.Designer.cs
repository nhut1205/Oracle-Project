namespace DoAn_Net
{
    partial class DoiMK
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
            label3 = new Label();
            txt_NhapLai = new TextBox();
            label1 = new Label();
            btn_Luu = new Button();
            btn_Thoat = new Button();
            txt_MatKhauMoi = new TextBox();
            label4 = new Label();
            txt_MatKhauCU = new Label();
            txtMatKhauCu = new TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 163);
            label3.Name = "label3";
            label3.Size = new Size(124, 15);
            label3.TabIndex = 2;
            label3.Text = "Xác nhận lại mật khẩu";
            // 
            // txt_NhapLai
            // 
            txt_NhapLai.Location = new Point(223, 160);
            txt_NhapLai.Margin = new Padding(3, 2, 3, 2);
            txt_NhapLai.Name = "txt_NhapLai";
            txt_NhapLai.Size = new Size(383, 23);
            txt_NhapLai.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(309, 25);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 5;
            label1.Text = "Đổi mật khẩu";
            // 
            // btn_Luu
            // 
            btn_Luu.BackColor = Color.FromArgb(192, 0, 192);
            btn_Luu.ForeColor = SystemColors.ButtonHighlight;
            btn_Luu.Location = new Point(258, 240);
            btn_Luu.Margin = new Padding(3, 2, 3, 2);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(82, 28);
            btn_Luu.TabIndex = 6;
            btn_Luu.Text = "Lưu";
            btn_Luu.UseVisualStyleBackColor = false;
            btn_Luu.Click += btn_Luu_Click;
            // 
            // btn_Thoat
            // 
            btn_Thoat.BackColor = Color.FromArgb(192, 0, 192);
            btn_Thoat.ForeColor = SystemColors.ButtonHighlight;
            btn_Thoat.Location = new Point(493, 240);
            btn_Thoat.Margin = new Padding(3, 2, 3, 2);
            btn_Thoat.Name = "btn_Thoat";
            btn_Thoat.Size = new Size(82, 28);
            btn_Thoat.TabIndex = 7;
            btn_Thoat.Text = "Thoát";
            btn_Thoat.UseVisualStyleBackColor = false;
            // 
            // txt_MatKhauMoi
            // 
            txt_MatKhauMoi.Location = new Point(223, 112);
            txt_MatKhauMoi.Margin = new Padding(3, 2, 3, 2);
            txt_MatKhauMoi.Name = "txt_MatKhauMoi";
            txt_MatKhauMoi.Size = new Size(383, 23);
            txt_MatKhauMoi.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 112);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 8;
            label4.Text = "Mật khẩu mới";
            // 
            // txt_MatKhauCU
            // 
            txt_MatKhauCU.AutoSize = true;
            txt_MatKhauCU.Location = new Point(50, 72);
            txt_MatKhauCU.Name = "txt_MatKhauCU";
            txt_MatKhauCU.Size = new Size(104, 15);
            txt_MatKhauCU.TabIndex = 10;
            txt_MatKhauCU.Text = "Nhập lại mật khẩu";
            // 
            // txtMatKhauCu
            // 
            txtMatKhauCu.Location = new Point(223, 70);
            txtMatKhauCu.Margin = new Padding(3, 2, 3, 2);
            txtMatKhauCu.Name = "txtMatKhauCu";
            txtMatKhauCu.Size = new Size(383, 23);
            txtMatKhauCu.TabIndex = 11;
            // 
            // DoiMK
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(txtMatKhauCu);
            Controls.Add(txt_MatKhauCU);
            Controls.Add(txt_MatKhauMoi);
            Controls.Add(label4);
            Controls.Add(btn_Thoat);
            Controls.Add(btn_Luu);
            Controls.Add(label1);
            Controls.Add(txt_NhapLai);
            Controls.Add(label3);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DoiMK";
            Text = "DoiMK";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private TextBox txt_NhapLai;
        private Label label1;
        private Button btn_Luu;
        private Button btn_Thoat;
        private TextBox txt_MatKhauMoi;
        private Label label4;
        private Label txt_MatKhauCU;
        private TextBox txtMatKhauCu;
    }
}