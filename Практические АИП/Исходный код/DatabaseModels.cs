namespace Variant11;

// Эти классы описывают таблицы базы данных, а Models.cs — исходный JSON.
public class SavedWorkout
{
    public int Id { get; set; }
    public string FileName { get; set; } = "";
    public int? ActivityType { get; set; }
    public string StartTime { get; set; } = "";
    public double? Duration { get; set; }
    public double? Distance { get; set; }
    public List<SavedLap> Laps { get; set; } = new List<SavedLap>();

    public override string ToString()
    {
        return Id + ". " + FileName + " — " + StartTime;
    }
}

public class SavedLap
{
    public int Id { get; set; }
    public int SavedWorkoutId { get; set; }
    public int Number { get; set; }
    public int RecordNumber { get; set; }
    // Пульс сохраняется уже в уд/мин. При чтении из базы умножать на 60 не нужно.
    public double? Average { get; set; }
    public double? Minimum { get; set; }
    public double? Maximum { get; set; }
}
