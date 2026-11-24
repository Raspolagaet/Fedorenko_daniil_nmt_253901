using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Variant11;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        try
        {
            // База всегда находится в одной папке, независимо от способа запуска.
            string folder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "Variant11Practice2");
            Directory.CreateDirectory(folder);
            string databasePath = Path.Combine(folder, "workouts.db");

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<WorkoutContext>(options =>
                options.UseSqlite("Data Source=" + databasePath));
            services.AddTransient<WorkoutService>();
            services.AddTransient<MainForm>();

            using ServiceProvider provider = services.BuildServiceProvider();
            using IServiceScope scope = provider.CreateScope();
            WorkoutService service = scope.ServiceProvider.GetRequiredService<WorkoutService>();
            service.CreateDatabase();
            MainForm form = scope.ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(form);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не удалось запустить приложение: " + ex.Message,
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
