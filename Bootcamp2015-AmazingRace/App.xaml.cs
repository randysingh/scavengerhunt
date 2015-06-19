using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Base.Services;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.ViewModels;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bootcamp2015.AmazingRace
{
    public sealed partial class App
    {
        private WinRTContainer container;
        private SettingsService settings;

        public App()
        {
            this.InitializeComponent();
            settings = new SettingsService();
        }

        public static MobileServiceClient MobileService = new MobileServiceClient(
            Connections.MobileServicesUri,
            Connections.MobileServicesAppKey
        );

        #region Bootstrapper

        #region IOC

        protected override void Configure()
        {
            ViewModelLocator.AddNamespaceMapping("Bootcamp2015.AmazingRace.Views", "Bootcamp2015.AmazingRace.ViewModels");
            ViewLocator.AddNamespaceMapping("Bootcamp2015.AmazingRace.ViewModels", "Bootcamp2015.AmazingRace.Views");

            container = new WinRTContainer();
            container.RegisterWinRTServices();
            container.RegisterSharingService();

            container.PerRequest<CluePageViewModel>();
            container.PerRequest<JoinTeamPageViewModel>();
            container.PerRequest<LeaderboardPageViewModel>();
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<MapPageViewModel>();

            PrepareViewFirst();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        #endregion

        #region Navigation Service

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);

            container.RegisterSingleton(typeof(IEventAggregator), "ea", typeof(EventAggregator));
            container.RegisterSingleton(typeof(IDataService), null, typeof(DataService));
            container.RegisterSingleton(typeof(IMessageDialogService), null, typeof(MessageDialogService));
            container.RegisterInstance(typeof(ISettingsService), null, settings);
        }

        public INavigationService NavigationService
        {
            get { return (INavigationService)container.GetInstance(typeof(INavigationService), null); }
        }

        #endregion

        #endregion

        #region Application Lifecylcle

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            DisplayRootView<MainPage>();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            var continuationManager = new ContinuationManager();
            var rootFrame = Window.Current.Content as Frame;
            var continuationEventArgs = args as IContinuationActivatedEventArgs;

            if (continuationEventArgs != null && rootFrame != null)
            {
                continuationManager.Continue(continuationEventArgs, rootFrame);
            }

            Window.Current.Activate();
            base.OnActivated(args);
        }

        protected override void OnResuming(object sender, object e)
        {
            base.OnResuming(sender, e);
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            base.OnSuspending(sender, e);
        }

        #endregion
    }
}