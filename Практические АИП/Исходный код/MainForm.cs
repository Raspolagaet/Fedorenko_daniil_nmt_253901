namespace Variant11;

public partial class MainForm : Form
{
    private WorkoutService? service;
    private SavedWorkout? workout;

    // Пустой конструктор нужен конструктору форм Visual Studio.
    // Он не открывает базу и не читает файлы.
    public MainForm()
    {
        InitializeComponent();
    }

    // При запуске DI выбирает этот конструктор и передаёт готовый сервис.
    public MainForm(WorkoutService service) : this()
    {
        this.service = service;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        if (service == null) return;
        try { RefreshSavedList(); }
        catch (Exception ex) { messageLabel.Text = "Не удалось прочитать список: " + ex.Message; }
    }

    private void OpenButton_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Файлы JSON (*.json)|*.json";
        if (dialog.ShowDialog() != DialogResult.OK) return;
        ClearWorkout();
        try
        {
            workout = WorkoutImporter.Read(dialog.FileName);
            ShowWorkout();
        }
        catch (System.Text.Json.JsonException)
        { messageLabel.Text = "Неправильный формат JSON или тип данных."; }
        catch (IOException ex)
        { messageLabel.Text = "Не удалось прочитать тренировку: " + ex.Message; }
        catch (UnauthorizedAccessException)
        { messageLabel.Text = "Нет доступа к файлу."; }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        if (service == null || workout == null) return;
        try
        {
            service.Add(workout);
            saveButton.Enabled = false;
            messageLabel.Text = "Тренировка сохранена. Теперь её можно открыть из базы.";
        }
        catch (Exception ex)
        {
            messageLabel.Text = "Ошибка сохранения: " + ex.Message;
            return;
        }
        try { RefreshSavedList(); }
        catch (Exception ex) { messageLabel.Text = "Сохранено, но список не обновился: " + ex.Message; }
    }

    private void OpenSavedButton_Click(object? sender, EventArgs e)
    {
        if (service == null) return;
        SavedWorkout? selected = savedList.SelectedItem as SavedWorkout;
        if (selected == null)
        {
            messageLabel.Text = "Сначала выберите тренировку в списке сохранённых.";
            return;
        }
        ClearWorkout();
        try
        {
            // Загружаем только из SQLite. Исходный JSON больше не нужен.
            workout = service.GetById(selected.Id);
            if (workout == null) messageLabel.Text = "Запись не найдена в базе.";
            else ShowWorkout();
        }
        catch (Exception ex) { messageLabel.Text = "Ошибка чтения базы: " + ex.Message; }
    }

    private void RefreshSavedList()
    {
        if (service == null) return;
        savedList.DataSource = service.GetAll();
    }

    private void ClearWorkout()
    {
        workout = null;
        table.Rows.Clear();
        fileLabel.Text = "Файл не выбран";
        infoLabel.Text = "";
        messageLabel.Text = "";
        saveButton.Enabled = false;
    }

    private void ShowWorkout()
    {
        if (workout == null) return;
        fileLabel.Text = workout.FileName;
        string activity = "Код " + workout.ActivityType;
        if (workout.ActivityType == 3) activity = "Бег";
        if (workout.ActivityType == 12) activity = "Ходьба";
        if (workout.ActivityType == 34) activity = "Бадминтон";
        infoLabel.Text = "Активность: " + activity + "\nНачало: " + workout.StartTime;
        infoLabel.Text += "\nДлительность: " + (workout.Duration?.ToString("N1") ?? "—") + " с; ";
        infoLabel.Text += "Дистанция: " + (workout.Distance?.ToString("N1") ?? "—") + " м";
        saveButton.Enabled = workout.Id == 0;

        table.Rows.Clear();
        bool hasPulse = false;
        bool hasMissing = false;
        int count = 0;
        foreach (SavedLap lap in workout.Laps.OrderBy(l => l.Number).ThenBy(l => l.RecordNumber))
        {
            int row = table.Rows.Add();
            table.Rows[row].Cells[0].Value = lap.Number;
            table.Rows[row].Cells[1].Value = lap.RecordNumber;
            table.Rows[row].Cells[2].Value = lap.Average;
            table.Rows[row].Cells[3].Value = lap.Minimum;
            table.Rows[row].Cells[4].Value = lap.Maximum;
            count = Math.Max(count, lap.Number);
            if (lap.Average != null || lap.Minimum != null || lap.Maximum != null) hasPulse = true;
            if (lap.Average == null || lap.Minimum == null || lap.Maximum == null) hasMissing = true;
        }
        string source = workout.Id == 0 ? "Загружено из JSON. " : "Загружено из базы. ";
        messageLabel.Text = source + "Кругов: " + count + ".";
        if (hasMissing) messageLabel.Text += " Пропуски обозначены знаком —.";
        if (!hasPulse) messageLabel.Text += " Нет данных пульса по кругам.";
        if (count == 0) messageLabel.Text = source + "Автоматических кругов нет. Общую информацию можно сохранить.";
    }
}
