using System.Text.Json;

namespace Variant11;

public static class WorkoutImporter
{
    public static SavedWorkout Read(string path)
    {
        string json = File.ReadAllText(path);
        WorkoutFile? file = JsonSerializer.Deserialize<WorkoutFile>(json);
        if (file == null || file.DeviceLog == null || file.DeviceLog.Header == null)
            throw new InvalidDataException("В файле нет DeviceLog.Header.");

        Header header = file.DeviceLog.Header;
        SavedWorkout result = new SavedWorkout();
        result.FileName = Path.GetFileName(path);
        result.ActivityType = header.ActivityType;
        result.StartTime = header.DateTime?.ToString("dd.MM.yyyy HH:mm:ss") ?? "Нет данных";
        result.Duration = header.Duration;
        result.Distance = header.Distance;

        int number = 0;
        if (file.DeviceLog.Windows == null) return result;
        foreach (WindowEntry? entry in file.DeviceLog.Windows)
        {
            if (entry == null || entry.Window == null) continue;
            WorkoutWindow window = entry.Window;
            // По просьбе пользователя оставлены только автоматические круги.
            if (window.Type != "Autolap") continue;
            number++;

            if (window.HR == null || window.HR.Count == 0)
            {
                result.Laps.Add(new SavedLap { Number = number, RecordNumber = 1 });
                continue;
            }
            int record = 0;
            foreach (HeartRate? hr in window.HR)
            {
                record++;
                SavedLap lap = new SavedLap();
                lap.Number = number;
                lap.RecordNumber = record;
                lap.Average = ToBpm(hr?.Avg);
                lap.Minimum = ToBpm(hr?.Min);
                lap.Maximum = ToBpm(hr?.Max);
                result.Laps.Add(lap);
            }
        }
        return result;
    }

    private static double? ToBpm(double? value)
    {
        if (value == null || value <= 0) return null;
        return value * 60;
    }
}
