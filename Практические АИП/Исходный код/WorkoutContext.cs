using Microsoft.EntityFrameworkCore;

namespace Variant11;

public class WorkoutContext : DbContext
{
    public DbSet<SavedWorkout> Workouts { get; set; }
    public DbSet<SavedLap> Laps { get; set; }

    public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
    {
    }
}
