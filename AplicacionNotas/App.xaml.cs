using AplicacionNotas.Data;

namespace AplicacionNotas
{
    public partial class App : Application
    {
        public static BaseDatosNotas NotasRepo { get; private set; }

        public App(BaseDatosNotas repo)
        {
            InitializeComponent();
            NotasRepo = repo;
            MainPage = new AppShell();
        }
    }
}