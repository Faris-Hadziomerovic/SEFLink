using Autofac;
using SEFLink.UI.Startup;
using System.Windows;

namespace SEFLink.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var Window = container.Resolve<MainWindow>();

            Window.Show();
        }
    }
}
