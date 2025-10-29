namespace kurum_arama
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
            il_secme_box = new ComboBox();
            arama_box = new TextBox();
            arama_sonuclari_box = new GroupBox();
            arama_button = new Button();
            tel_no_combo = new RadioButton();
            ikiside_combo = new RadioButton();
            mail_combo = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            verileri_indir_button = new Button();
            pdf_button = new Button();
            SuspendLayout();
            // 
            // il_secme_box
            // 
            il_secme_box.FormattingEnabled = true;
            il_secme_box.Location = new Point(495, 39);
            il_secme_box.Name = "il_secme_box";
            il_secme_box.Size = new Size(170, 23);
            il_secme_box.TabIndex = 0;
            il_secme_box.SelectedIndexChanged += il_secme_box_SelectedIndexChanged;
            // 
            // arama_box
            // 
            arama_box.Location = new Point(160, 39);
            arama_box.Name = "arama_box";
            arama_box.Size = new Size(269, 23);
            arama_box.TabIndex = 1;
            arama_box.TextChanged += arama_box_TextChanged;
            // 
            // arama_sonuclari_box
            // 
            arama_sonuclari_box.Location = new Point(12, 210);
            arama_sonuclari_box.Name = "arama_sonuclari_box";
            arama_sonuclari_box.Size = new Size(757, 228);
            arama_sonuclari_box.TabIndex = 2;
            arama_sonuclari_box.TabStop = false;
            arama_sonuclari_box.Text = "Arama Sonuçları";
            arama_sonuclari_box.Enter += arama_sonuclari_box_Enter_1;
            // 
            // arama_button
            // 
            arama_button.Location = new Point(373, 166);
            arama_button.Name = "arama_button";
            arama_button.Size = new Size(146, 33);
            arama_button.TabIndex = 3;
            arama_button.Text = "Ara";
            arama_button.UseVisualStyleBackColor = true;
            arama_button.Click += arama_button_Click;
            // 
            // tel_no_combo
            // 
            tel_no_combo.AutoSize = true;
            tel_no_combo.Location = new Point(202, 108);
            tel_no_combo.Name = "tel_no_combo";
            tel_no_combo.Size = new Size(118, 19);
            tel_no_combo.TabIndex = 4;
            tel_no_combo.TabStop = true;
            tel_no_combo.Text = "Telefon Numarası";
            tel_no_combo.UseVisualStyleBackColor = true;
            tel_no_combo.CheckedChanged += tel_no_combo_CheckedChanged;
            // 
            // ikiside_combo
            // 
            ikiside_combo.AutoSize = true;
            ikiside_combo.Location = new Point(397, 108);
            ikiside_combo.Name = "ikiside_combo";
            ikiside_combo.Size = new Size(80, 19);
            ikiside_combo.TabIndex = 5;
            ikiside_combo.TabStop = true;
            ikiside_combo.Text = "Her ikiside";
            ikiside_combo.UseVisualStyleBackColor = true;
            ikiside_combo.CheckedChanged += ikiside_combo_CheckedChanged;
            // 
            // mail_combo
            // 
            mail_combo.AutoSize = true;
            mail_combo.Location = new Point(326, 108);
            mail_combo.Name = "mail_combo";
            mail_combo.Size = new Size(65, 19);
            mail_combo.TabIndex = 6;
            mail_combo.TabStop = true;
            mail_combo.Text = "E - Mail";
            mail_combo.UseVisualStyleBackColor = true;
            mail_combo.CheckedChanged += mail_combo_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(161, 20);
            label1.Name = "label1";
            label1.Size = new Size(139, 15);
            label1.TabIndex = 7;
            label1.Text = "Aranacak İşletme Giriniz :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(495, 20);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 8;
            label2.Text = "İl Giriniz :";
            // 
            // verileri_indir_button
            // 
            verileri_indir_button.Location = new Point(525, 166);
            verileri_indir_button.Name = "verileri_indir_button";
            verileri_indir_button.Size = new Size(122, 33);
            verileri_indir_button.TabIndex = 9;
            verileri_indir_button.Text = "Verileri İndir";
            verileri_indir_button.UseVisualStyleBackColor = true;
            verileri_indir_button.Click += VerileriIndirButton_Click;
            // 
            // pdf_button
            // 
            pdf_button.Location = new Point(653, 166);
            pdf_button.Name = "pdf_button";
            pdf_button.Size = new Size(116, 33);
            pdf_button.TabIndex = 10;
            pdf_button.Text = "Pdf Olarak İndir";
            pdf_button.UseVisualStyleBackColor = true;
            pdf_button.Click += pdf_button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(900, 600);
            Controls.Add(pdf_button);
            Controls.Add(verileri_indir_button);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(arama_button);
            Controls.Add(arama_sonuclari_box);
            Controls.Add(arama_box);
            Controls.Add(il_secme_box);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(800, 500);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "İşletme Arama Sistemi";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox il_secme_box;
        private TextBox arama_box;
        private GroupBox arama_sonuclari_box;
        private Button arama_button;
        private RadioButton tel_no_combo;
        private RadioButton ikiside_combo;
        private RadioButton mail_combo;
        private Label label1;
        private Label label2;
        private Button verileri_indir_button;
        private Button pdf_button;
    }
}
