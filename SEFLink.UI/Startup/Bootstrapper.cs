using Autofac;
using Prism.Events;
using SEFLink.UI.Data;
using SEFLink.UI.ViewModels;
using SEFLink.UI.ViewModels.Dashboard;

namespace SEFLink.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            // Main Window
            builder.RegisterType<MainWindow>().AsSelf();



            #region HCI ViewModels
            builder.RegisterType<HCI.ViewModels.AppViewModel>().AsSelf();

            // App base level views models
            builder.RegisterType<HCI.ViewModels.DialogOverlayViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.LanguageSettingsViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.MenuViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.NavigationBarViewModel>().AsSelf();

            // Menu base level views models
            builder.RegisterType<HCI.ViewModels.SidebarViewModel>().AsSelf();

            // Menu payment views models
            builder.RegisterType<HCI.ViewModels.Payment.PaymentOptionsViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.Payment.PaymentCashViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.Payment.PaymentCreditCardViewModel>().AsSelf();

            // Menu navigation views models
            builder.RegisterType<HCI.ViewModels.Menu.FoodDrinkViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.Menu.DrinksViewModel>().AsSelf();
            builder.RegisterType<HCI.ViewModels.Menu.FoodViewModel>().AsSelf();

            // Other
            builder.RegisterType<HCI.ViewModels.OrderItemViewModel>().AsSelf();

            #endregion



            #region Base ViewModels
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<BaseTitleBarViewModel>().AsSelf().SingleInstance();
            #endregion

            #region Other ViewModels
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<DashboardViewModel>().AsSelf();
            builder.RegisterType<SettingsViewModel>().AsSelf();

            builder.RegisterType<FirstSettingsViewModel>().AsSelf();
            builder.RegisterType<SecondSettingsViewModel>().AsSelf();

            builder.RegisterType<IncomingDocumentsViewModel>().AsSelf();
            builder.RegisterType<OutgoingDocumentsViewModel>().AsSelf();
            #endregion            

            #region Other
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<DocumentInfoDataService>().As<IDocumentInfoDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>().SingleInstance();
            builder.RegisterType<UserDocumentsDataService>().As<IUserDocumentsDataService>().SingleInstance();
            #endregion

            return builder.Build();
        }
    }
}
