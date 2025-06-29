using Microsoft.Extensions.Logging;
using AplicacionNotas.Data;

namespace AplicacionNotas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var constructor = MauiApp.CreateBuilder();
            constructor
                .UseMauiApp<App>()
                .ConfigureFonts(fuentes =>
                {
                    fuentes.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

#if DEBUG
            constructor.Services.AddLogging(logging =>
            {
                logging.AddDebug();
            });
#endif

            // Registrar las páginas
            constructor.Services.AddSingleton<PaginaPrincipal>();

            // Registrar la base de datos
            string dbPath = FileAccessHelper.GetLocalFilePath("notas.db3");
            constructor.Services.AddSingleton<BaseDatosNotas>(s => ActivatorUtilities.CreateInstance<BaseDatosNotas>(s, dbPath));

            return constructor.Build();
        }
    }
}