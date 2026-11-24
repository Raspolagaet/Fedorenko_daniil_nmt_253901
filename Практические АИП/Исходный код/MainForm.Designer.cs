namespace Variant11;

partial class MainForm
{
    private Button openButton = null!;
    private Label fileLabel = null!;
    private Label infoLabel = null!;
    private Label messageLabel = null!;
    private Button saveButton = null!;
    private Button openSavedButton = null!;
    private ComboBox savedList = null!;
    private Label savedLabel = null!;
    private DataGridView table = null!;


    private DataGridViewTextBoxColumn columnLap = null!;
    private DataGridViewTextBoxColumn columnRecord = null!;
    private DataGridViewTextBoxColumn columnAverage = null!;
    private DataGridViewTextBoxColumn columnMinimum = null!;
    private DataGridViewTextBoxColumn columnMaximum = null!;

    private void InitializeComponent()
    {
        DataGridViewCellStyle pulseStyle = new DataGridViewCellStyle();
        pulseStyle.Format = "N1";
        pulseStyle.NullValue = "—";
        openButton = new Button();
        openButton.Name = "openButton";
        fileLabel = new Label();
        fileLabel.Name = "fileLabel";
        infoLabel = new Label();
        infoLabel.Name = "infoLabel";
        messageLabel = new Label();
        messageLabel.Name = "messageLabel";
        saveButton = new Button();
        openSavedButton = new Button();
        savedList = new ComboBox();
        savedLabel = new Label();
        table = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)table).BeginInit();
        SuspendLayout();
        table.Name = "table";

        Text = "Вариант 11 — Анализ пульса по кругам";
        ClientSize = new Size(960, 650);
        MinimumSize = new Size(980, 690);
        Font = new Font("Segoe UI", 10);
        StartPosition = FormStartPosition.CenterScreen;

        openButton.Text = "Открыть JSON";
        openButton.Location = new Point(15, 15);
        openButton.Size = new Size(150, 35);
        openButton.Click += OpenButton_Click;

        fileLabel.Text = "Файл не выбран";
        fileLabel.Location = new Point(365, 20);
        fileLabel.Size = new Size(570, 30);
        infoLabel.Text = "Выберите файл тренировки.";
        infoLabel.Location = new Point(15, 65);
        infoLabel.Size = new Size(855, 80);



        table.Location = new Point(15, 230);
        table.Size = new Size(930, 320);
        table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        table.ReadOnly = true;
        table.AllowUserToAddRows = false;
        table.RowHeadersVisible = false;
        table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        columnLap = new DataGridViewTextBoxColumn();
        columnLap.Name = "columnLap";
        columnLap.HeaderText = "Круг";
        columnLap.SortMode = DataGridViewColumnSortMode.NotSortable;
        columnRecord = new DataGridViewTextBoxColumn();
        columnRecord.Name = "columnRecord";
        columnRecord.HeaderText = "Запись HR";
        columnRecord.SortMode = DataGridViewColumnSortMode.NotSortable;
        columnAverage = new DataGridViewTextBoxColumn();
        columnAverage.Name = "columnAverage";
        columnAverage.HeaderText = "Средний, уд/мин";
        columnAverage.SortMode = DataGridViewColumnSortMode.NotSortable;
        columnMinimum = new DataGridViewTextBoxColumn();
        columnMinimum.Name = "columnMinimum";
        columnMinimum.HeaderText = "Минимальный, уд/мин";
        columnMinimum.SortMode = DataGridViewColumnSortMode.NotSortable;
        columnMaximum = new DataGridViewTextBoxColumn();
        columnMaximum.Name = "columnMaximum";
        columnMaximum.HeaderText = "Максимальный, уд/мин";
        columnMaximum.SortMode = DataGridViewColumnSortMode.NotSortable;
        table.Columns.AddRange(new DataGridViewColumn[] { columnLap, columnRecord, columnAverage, columnMinimum, columnMaximum });
        columnAverage.DefaultCellStyle = pulseStyle;
        columnMinimum.DefaultCellStyle = pulseStyle;
        columnMaximum.DefaultCellStyle = pulseStyle;
        // Сохраняем исходную последовательность кругов.


        messageLabel.Location = new Point(15, 565);
        messageLabel.Size = new Size(930, 70);
        messageLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        saveButton.Name = "saveButton";
        saveButton.Text = "Сохранить в базу";
        saveButton.Location = new Point(180, 15);
        saveButton.Size = new Size(170, 35);
        saveButton.Enabled = false;
        saveButton.Click += SaveButton_Click;
        savedLabel.Name = "savedLabel";
        savedLabel.Text = "Сохранённые тренировки";
        savedLabel.Location = new Point(15, 150);
        savedLabel.Size = new Size(300, 25);
        savedList.Name = "savedList";
        savedList.Location = new Point(15, 180);
        savedList.Size = new Size(720, 30);
        savedList.DropDownStyle = ComboBoxStyle.DropDownList;
        openSavedButton.Name = "openSavedButton";
        openSavedButton.Text = "Открыть из базы";
        openSavedButton.Location = new Point(750, 178);
        openSavedButton.Size = new Size(195, 35);
        openSavedButton.Click += OpenSavedButton_Click;
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Name = "MainForm";
        Load += MainForm_Load;
        Controls.Add(saveButton);
        Controls.Add(savedLabel);
        Controls.Add(savedList);
        Controls.Add(openSavedButton);
        Controls.Add(openButton);
        Controls.Add(fileLabel);
        Controls.Add(infoLabel);
        Controls.Add(table);
        Controls.Add(messageLabel);
        ((System.ComponentModel.ISupportInitialize)table).EndInit();
        ResumeLayout(false);
    }

}



