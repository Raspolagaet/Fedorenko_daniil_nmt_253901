namespace Variant11;

// Названия свойств совпадают с названиями полей в JSON.
public class WorkoutFile
{
    public DeviceLog? DeviceLog { get; set; }
}

public class DeviceLog
{
    public Header? Header { get; set; }
    public List<WindowEntry?>? Windows { get; set; }
}

public class Header
{
    public int? ActivityType { get; set; }
    public DateTimeOffset? DateTime { get; set; }
    public double? Duration { get; set; }
    public double? Distance { get; set; }
}

public class WindowEntry
{
    public WorkoutWindow? Window { get; set; }
}

public class WorkoutWindow
{
    public string? Type { get; set; }
    public List<HeartRate?>? HR { get; set; }
}

public class HeartRate
{
    // Знак ? разрешает хранить null, если показателя нет.
    public double? Avg { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
}
