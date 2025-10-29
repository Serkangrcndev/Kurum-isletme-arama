using OfficeOpenXml;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using Font = System.Drawing.Font;
using Microsoft.Extensions.Configuration;

namespace kurum_arama
{
    public partial class Form1 : Form
    {

        private List<Kurum> kurumlar = new List<Kurum>();

        private GooglePlacesService? _googlePlacesService;

        private IConfiguration? _configuration;

        public Form1()
        {
            InitializeComponent();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            _configuration = builder.Build();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.FromArgb(240, 248, 255);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);


            Label baslikLabel = new Label();
            baslikLabel.Text = "İşletme Arama Sistemi";
            baslikLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            baslikLabel.ForeColor = Color.FromArgb(25, 118, 210);
            baslikLabel.Location = new Point(300, 10);
            baslikLabel.Size = new Size(300, 30);
            baslikLabel.TextAlign = ContentAlignment.MiddleCenter;
            baslikLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(baslikLabel);


            Label gelistiriciLabel = new Label();
            gelistiriciLabel.Text = "Serkan Gürcan & Emirhan Karakuş";
            gelistiriciLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            gelistiriciLabel.ForeColor = Color.FromArgb(100, 100, 100);
            gelistiriciLabel.Location = new Point(300, 40);
            gelistiriciLabel.Size = new Size(300, 20);
            gelistiriciLabel.TextAlign = ContentAlignment.MiddleCenter;
            gelistiriciLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(gelistiriciLabel);


            string[] iller = {
                "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balıkesir",
                "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli",
                "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkâri",
                "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
                "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir",
                "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat",
                "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman",
                "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"
            };

            il_secme_box.Items.AddRange(iller);


            il_secme_box.Location = new Point(495, 80);
            il_secme_box.Size = new Size(200, 30);
            il_secme_box.DropDownHeight = 300;
            il_secme_box.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            il_secme_box.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            il_secme_box.ForeColor = Color.FromArgb(33, 33, 33);
            il_secme_box.BackColor = Color.White;
            il_secme_box.FlatStyle = FlatStyle.Flat;


            arama_box.Location = new Point(160, 80);
            arama_box.Size = new Size(300, 30);
            arama_box.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            arama_box.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            arama_box.ForeColor = Color.FromArgb(33, 33, 33);
            arama_box.BackColor = Color.White;
            arama_box.BorderStyle = BorderStyle.FixedSingle;
            arama_box.PlaceholderText = "Örn: Hastane, Restoran, Otel...";

            label1.Location = new Point(161, 61);
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            label2.Location = new Point(495, 61);
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            GroupBox radioGroupBox = new GroupBox();
            radioGroupBox.Text = "📋 İletişim Bilgileri";
            radioGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radioGroupBox.ForeColor = Color.FromArgb(25, 118, 210);
            radioGroupBox.Location = new Point(160, 130);
            radioGroupBox.Size = new Size(400, 60);
            radioGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            radioGroupBox.BackColor = Color.FromArgb(248, 249, 250);
            radioGroupBox.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(radioGroupBox);

            tel_no_combo.Location = new Point(15, 25);
            tel_no_combo.Size = new Size(120, 25);
            tel_no_combo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            tel_no_combo.ForeColor = Color.FromArgb(33, 33, 33);
            tel_no_combo.Text = "📞 Telefon Numarası";
            tel_no_combo.Cursor = Cursors.Hand;
            tel_no_combo.BackColor = Color.Transparent;
            radioGroupBox.Controls.Add(tel_no_combo);

            mail_combo.Location = new Point(145, 25);
            mail_combo.Size = new Size(100, 25);
            mail_combo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            mail_combo.ForeColor = Color.FromArgb(33, 33, 33);
            mail_combo.Text = "📧 E-Mail";
            mail_combo.Cursor = Cursors.Hand;
            mail_combo.BackColor = Color.Transparent;
            radioGroupBox.Controls.Add(mail_combo);

            ikiside_combo.Location = new Point(255, 25);
            ikiside_combo.Size = new Size(100, 25);
            ikiside_combo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            ikiside_combo.ForeColor = Color.FromArgb(33, 33, 33);
            ikiside_combo.Text = "🔄 Her İkisi";
            ikiside_combo.Cursor = Cursors.Hand;
            ikiside_combo.BackColor = Color.Transparent;
            radioGroupBox.Controls.Add(ikiside_combo);

            arama_button.Location = new Point(271, 200);
            arama_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            verileri_indir_button.BackColor = Color.FromArgb(76, 175, 80);
            verileri_indir_button.ForeColor = Color.White;
            verileri_indir_button.FlatStyle = FlatStyle.Flat;
            verileri_indir_button.FlatAppearance.BorderSize = 0;
            verileri_indir_button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            verileri_indir_button.Size = new Size(140, 40);
            verileri_indir_button.Cursor = Cursors.Hand;
            verileri_indir_button.Location = new Point(647, 200);
            verileri_indir_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            verileri_indir_button.Text = "📊 Verileri İndir";
            verileri_indir_button.Click += VerileriIndirButton_Click;

            pdf_button.BackColor = Color.FromArgb(220, 53, 69);
            pdf_button.ForeColor = Color.White;
            pdf_button.FlatStyle = FlatStyle.Flat;
            pdf_button.FlatAppearance.BorderSize = 0;
            pdf_button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pdf_button.Size = new Size(140, 40);
            pdf_button.Cursor = Cursors.Hand;
            pdf_button.Location = new Point(519, 200);
            pdf_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pdf_button.Text = "📄 PDF İndir";

            Button whatsapp_button = new Button();
            whatsapp_button.BackColor = Color.FromArgb(37, 211, 102);
            whatsapp_button.ForeColor = Color.White;
            whatsapp_button.FlatStyle = FlatStyle.Flat;
            whatsapp_button.FlatAppearance.BorderSize = 0;
            whatsapp_button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            whatsapp_button.Size = new Size(120, 40);
            whatsapp_button.Cursor = Cursors.Hand;
            whatsapp_button.Location = new Point(395, 200);
            whatsapp_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            whatsapp_button.Text = "💬 WhatsApp";
            whatsapp_button.Click += WhatsAppButton_Click;
            this.Controls.Add(whatsapp_button);

            arama_sonuclari_box.Location = new Point(12, 250);
            arama_sonuclari_box.Size = new Size(876, 330);
            arama_sonuclari_box.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            arama_sonuclari_box.Text = "📊 Arama Sonuçları";
            arama_sonuclari_box.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            arama_sonuclari_box.ForeColor = Color.FromArgb(33, 150, 243);

            DataGridView sonucDataGrid = new DataGridView();
            sonucDataGrid.Location = new Point(10, 20);
            sonucDataGrid.Size = new Size(856, 300);
            sonucDataGrid.Name = "sonucDataGrid";
            sonucDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sonucDataGrid.AllowUserToAddRows = false;
            sonucDataGrid.ReadOnly = true;
            sonucDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            sonucDataGrid.MultiSelect = false;
            sonucDataGrid.BackgroundColor = Color.White;
            sonucDataGrid.GridColor = Color.FromArgb(220, 220, 220);
            sonucDataGrid.BorderStyle = BorderStyle.None;
            sonucDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            sonucDataGrid.RowHeadersVisible = false;
            sonucDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            sonucDataGrid.EnableHeadersVisualStyles = false;

            sonucDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            sonucDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            sonucDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sonucDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sonucDataGrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            sonucDataGrid.ColumnHeadersHeight = 40;
            sonucDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            sonucDataGrid.RowTemplate.Height = 35;
            sonucDataGrid.DefaultCellStyle.BackColor = Color.White;
            sonucDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            sonucDataGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            sonucDataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(197, 202, 233);
            sonucDataGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 33, 33);
            sonucDataGrid.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            sonucDataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            sonucDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            sonucDataGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(197, 202, 233);


            sonucDataGrid.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    sonucDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
                }
            };

            sonucDataGrid.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    if (e.RowIndex % 2 == 0)
                        sonucDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    else
                        sonucDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                }
            };

            sonucDataGrid.Columns.Add("KurumAdi", "🏢 Kurum Adı");
            sonucDataGrid.Columns.Add("Il", "📍 İl");
            sonucDataGrid.Columns.Add("Adres", "🏠 Adres");
            sonucDataGrid.Columns.Add("Telefon", "📞 Telefon");
            sonucDataGrid.Columns.Add("Website", "🌐 Website");
            sonucDataGrid.Columns.Add("Rating", "⭐ Puan");

  
            sonucDataGrid.Columns["KurumAdi"].Width = 180;
            sonucDataGrid.Columns["Il"].Width = 100;
            sonucDataGrid.Columns["Adres"].Width = 250;
            sonucDataGrid.Columns["Telefon"].Width = 140;
            sonucDataGrid.Columns["Website"].Width = 180;
            sonucDataGrid.Columns["Rating"].Width = 100;

    
            sonucDataGrid.Columns["Il"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sonucDataGrid.Columns["Rating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sonucDataGrid.Columns["Telefon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            arama_sonuclari_box.Controls.Add(sonucDataGrid);

            arama_button.BackColor = Color.FromArgb(25, 118, 210);
            arama_button.ForeColor = Color.White;
            arama_button.FlatStyle = FlatStyle.Flat;
            arama_button.FlatAppearance.BorderSize = 0;
            arama_button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            arama_button.Size = new Size(120, 40);
            arama_button.Cursor = Cursors.Hand;

            string? apiKey = _configuration?["GooglePlacesAPI:ApiKey"];
            
            if (!string.IsNullOrEmpty(apiKey) && apiKey != "YOUR_GOOGLE_PLACES_API_KEY_HERE")
            {
                _googlePlacesService = new GooglePlacesService(apiKey);
            }
            else
            {
                MessageBox.Show("Google Places API anahtarı ayarlanmamış!\n\n" +
                    "Lütfen şu adımları takip edin:\n" +
                    "1. appsettings.example.json dosyasını kopyalayın\n" +
                    "2. appsettings.json olarak yeniden adlandırın\n" +
                    "3. GooglePlacesAPI:ApiKey değerini kendi API anahtarınızla değiştirin\n\n" +
                    "API anahtarı almak için: https://console.cloud.google.com/",
                    "API Anahtarı Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void arama_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void il_secme_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tel_no_combo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mail_combo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ikiside_combo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void arama_button_Click(object sender, EventArgs e)
        {
           
            string arananKurum = arama_box.Text.Trim();
            string secilenIl = il_secme_box.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrEmpty(arananKurum) || string.IsNullOrEmpty(secilenIl))
            {
                MessageBox.Show("Lütfen kurum adı ve il seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridView? sonucDataGrid = arama_sonuclari_box.Controls["sonucDataGrid"] as DataGridView;
            if (sonucDataGrid == null) return;

            sonucDataGrid.Rows.Clear();
            sonucDataGrid.Rows.Add("Arama yapılıyor...", "", "", "", "", "");

            try
            {
                if (_googlePlacesService == null)
                {
                    MessageBox.Show("Google Places API servisi başlatılamadı!\nLütfen API anahtarını kontrol edin.",
                        "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var googleSonuclar = await _googlePlacesService.AramaYap(arananKurum, secilenIl);

                sonucDataGrid.Rows.Clear();

                if (googleSonuclar.Count == 0)
                {
                    sonucDataGrid.Rows.Add("Sonuç bulunamadı", "", "", "", "", "");
                }
                else
                {
                    foreach (var place in googleSonuclar)
                    {
                        string telefon = (tel_no_combo.Checked || ikiside_combo.Checked) ? place.Telefon : "";
                        string website = (mail_combo.Checked || ikiside_combo.Checked) ? place.Website : "";
                        string rating = place.Rating > 0 ? place.Rating.ToString("F1") : "";

                        sonucDataGrid.Rows.Add(place.Ad, secilenIl, place.Adres, telefon, website, rating);
                    }
                }
            }
            catch (Exception ex)
            {
                sonucDataGrid.Rows.Clear();
                sonucDataGrid.Rows.Add($"Hata: {ex.Message}", "", "", "", "", "");
                MessageBox.Show($"Arama sırasında hata oluştu: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void arama_sonuclari_box_Enter_1(object sender, EventArgs e)
        {

        }

        private void VerileriIndirButton_Click(object? sender, EventArgs e)
        {
            DataGridView? sonucDataGrid = arama_sonuclari_box.Controls["sonucDataGrid"] as DataGridView;
            if (sonucDataGrid == null)
            {
                MessageBox.Show("Veri tablosu bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sonucDataGrid.Rows.Count == 0 || (sonucDataGrid.Rows.Count == 1 && sonucDataGrid.Rows[0].Cells[0].Value?.ToString() == "Arama yapılıyor..."))
            {
                MessageBox.Show("İndirilecek veri bulunamadı!\nLütfen önce arama yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar (*.*)|*.*";
                saveFileDialog.FileName = $"Kurum_Arama_Sonuclari_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                saveFileDialog.Title = "Excel Dosyasını Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Arama Sonuçları");

                        for (int i = 0; i < sonucDataGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = sonucDataGrid.Columns[i].HeaderText;
                        }

         
                        using (var range = worksheet.Cells[1, 1, 1, sonucDataGrid.Columns.Count])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(33, 150, 243));
                            range.Style.Font.Color.SetColor(Color.White);
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }

                     
                        for (int i = 0; i < sonucDataGrid.Rows.Count; i++)
                        {
                            for (int j = 0; j < sonucDataGrid.Columns.Count; j++)
                            {
                                var cellValue = sonucDataGrid.Rows[i].Cells[j].Value?.ToString() ?? "";
                                
                   
                                if (cellValue.Contains("Arama yapılıyor") || cellValue.Contains("Sonuç bulunamadı") || cellValue.Contains("Hata:"))
                                {
                                    continue;
                                }

                                worksheet.Cells[i + 2, j + 1].Value = cellValue;
                            }
                        }

        
                        worksheet.Cells.AutoFitColumns();

                
                        using (var range = worksheet.Cells[1, 1, sonucDataGrid.Rows.Count + 1, sonucDataGrid.Columns.Count])
                        {
                            range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }

                
                        package.SaveAs(new FileInfo(saveFileDialog.FileName));
                    }

                    MessageBox.Show($"Veriler başarıyla Excel dosyasına kaydedildi!\n\nDosya Konumu: {saveFileDialog.FileName}", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excel dosyası oluşturulurken hata oluştu:\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pdf_button_Click(object? sender, EventArgs e)
        {

            DataGridView? sonucDataGrid = arama_sonuclari_box.Controls["sonucDataGrid"] as DataGridView;
            if (sonucDataGrid == null)
            {
                MessageBox.Show("Veri tablosu bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            if (sonucDataGrid.Rows.Count == 0 || (sonucDataGrid.Rows.Count == 1 && sonucDataGrid.Rows[0].Cells[0].Value?.ToString() == "Arama yapılıyor..."))
            {
                MessageBox.Show("İndirilecek veri bulunamadı!\nLütfen önce arama yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar (*.*)|*.*";
                saveFileDialog.FileName = $"Kurum_Arama_Sonuclari_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                saveFileDialog.Title = "PDF Dosyasını Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                 
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    
                    document.Open();

                  
                    iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.DARK_GRAY);
                    Paragraph title = new Paragraph("Kurum Arama Sonuçları", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20;
                    document.Add(title);

               
                    iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
                    Paragraph dateInfo = new Paragraph($"Rapor Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm}", dateFont);
                    dateInfo.Alignment = Element.ALIGN_RIGHT;
                    dateInfo.SpacingAfter = 15;
                    document.Add(dateInfo);

                
                    PdfPTable table = new PdfPTable(sonucDataGrid.Columns.Count);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 2f, 1f, 3f, 1.5f, 2f, 1f });

                  
                    iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                    foreach (DataGridViewColumn column in sonucDataGrid.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, headerFont));
                        cell.BackgroundColor = new BaseColor(33, 150, 243);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Padding = 8;
                        table.AddCell(cell);
                    }

                   
                    iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
                    for (int i = 0; i < sonucDataGrid.Rows.Count; i++)
                    {
                        var cellValue = sonucDataGrid.Rows[i].Cells[0].Value?.ToString() ?? "";
                        
                      
                        if (cellValue.Contains("Arama yapılıyor") || cellValue.Contains("Sonuç bulunamadı") || cellValue.Contains("Hata:"))
                        {
                            continue;
                        }

                        for (int j = 0; j < sonucDataGrid.Columns.Count; j++)
                        {
                            string cellText = sonucDataGrid.Rows[i].Cells[j].Value?.ToString() ?? "";
                            PdfPCell cell = new PdfPCell(new Phrase(cellText, dataFont));
                            cell.Padding = 5;
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            
                       
                            if (i % 2 == 0)
                                cell.BackgroundColor = new BaseColor(248, 249, 250);
                            
                            table.AddCell(cell);
                        }
                    }

                    document.Add(table);

                
                    Paragraph footer = new Paragraph($"\n\nToplam {sonucDataGrid.Rows.Count} kayıt bulundu.", dateFont);
                    footer.Alignment = Element.ALIGN_CENTER;
                    document.Add(footer);

                    document.Close();
                    writer.Close();

                    MessageBox.Show($"Veriler başarıyla PDF dosyasına kaydedildi!\n\nDosya Konumu: {saveFileDialog.FileName}", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PDF dosyası oluşturulurken hata oluştu:\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WhatsAppButton_Click(object? sender, EventArgs e)
        {

            DataGridView? sonucDataGrid = arama_sonuclari_box.Controls["sonucDataGrid"] as DataGridView;
            if (sonucDataGrid == null)
            {
                MessageBox.Show("Veri tablosu bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sonucDataGrid.Rows.Count == 0 || (sonucDataGrid.Rows.Count == 1 && sonucDataGrid.Rows[0].Cells[0].Value?.ToString() == "Arama yapılıyor..."))
            {
                MessageBox.Show("Paylaşılacak veri bulunamadı!\nLütfen önce arama yapın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
            
                string mesaj = "🏢 *Kurum Arama Sonuçları*\n\n";
                mesaj += $"📅 Tarih: {DateTime.Now:dd.MM.yyyy HH:mm}\n";
                mesaj += $"🔍 Arama Terimi: {arama_box.Text}\n";
                mesaj += $"📍 Şehir: {il_secme_box.SelectedItem}\n\n";
                mesaj += "📋 *Bulunan Kurumlar:*\n\n";

                int sayac = 1;
                for (int i = 0; i < sonucDataGrid.Rows.Count; i++)
                {
                    var cellValue = sonucDataGrid.Rows[i].Cells[0].Value?.ToString() ?? "";
                    
                
                    if (cellValue.Contains("Arama yapılıyor") || cellValue.Contains("Sonuç bulunamadı") || cellValue.Contains("Hata:"))
                    {
                        continue;
                    }

                    string kurumAdi = sonucDataGrid.Rows[i].Cells[0].Value?.ToString() ?? "";
                    string adres = sonucDataGrid.Rows[i].Cells[2].Value?.ToString() ?? "";
                    string telefon = sonucDataGrid.Rows[i].Cells[3].Value?.ToString() ?? "";
                    string website = sonucDataGrid.Rows[i].Cells[4].Value?.ToString() ?? "";
                    string rating = sonucDataGrid.Rows[i].Cells[5].Value?.ToString() ?? "";

                    mesaj += $"{sayac}. *{kurumAdi}*\n";
                    if (!string.IsNullOrEmpty(adres))
                        mesaj += $"   📍 {adres}\n";
                    if (!string.IsNullOrEmpty(telefon))
                        mesaj += $"   📞 {telefon}\n";
                    if (!string.IsNullOrEmpty(website))
                        mesaj += $"   🌐 {website}\n";
                    if (!string.IsNullOrEmpty(rating))
                        mesaj += $"   ⭐ {rating}\n";
                    mesaj += "\n";
                    sayac++;
                }

                mesaj += "\n📱 *Kurum Arama Sistemi* tarafından oluşturuldu.";

              
                string encodedMessage = Uri.EscapeDataString(mesaj);
                string whatsappUrl = $"https://wa.me/?text={encodedMessage}";

             
                Process.Start(new ProcessStartInfo
                {
                    FileName = whatsappUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WhatsApp paylaşımında hata oluştu:\n{ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

  
    public class Kurum
    {
        public string Ad { get; set; }
        public string Il { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        
        public Kurum(string ad, string il, string adres, string telefon, string email)
        {
            Ad = ad;
            Il = il;
            Adres = adres;
            Telefon = telefon;
            Email = email;
        }
    }
}
