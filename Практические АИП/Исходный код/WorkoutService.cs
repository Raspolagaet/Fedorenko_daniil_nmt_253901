using Microsoft.EntityFrameworkCore;

namespace Variant11;

public class WorkoutService
{
    private readonly WorkoutContext context;

    public WorkoutService(WorkoutContext context)
    {
        this.context = context;
    }

    public void CreateDatabase()
    {
        context.Database.EnsureCreated();
    }

    public List<SavedWorkout> GetAll()
    {
        return context.Workouts.AsNoTracking().OrderByDescending(w => w.Id).ToList();
    }

    public SavedWorkout? GetById(int id)
    {
        // Include загружает связанные строки с показателями кругов.
        return context.Workouts.AsNoTracking().Include(w => w.Laps)
            .FirstOrDefault(w => w.Id == id);
    }

    public void Add(SavedWorkout workout)
    {
        if (workout.Id != 0) return; // Эта запись уже была сохранена.
        try
        {
            context.Workouts.Add(workout);
            context.SaveChanges(); // Тренировка и её круги сохраняются вместе.
        }
        finally
        {
            // Не держим старые объекты в памяти контекста после операции.
            context.ChangeTracker.Clear();
        }
    }
}
