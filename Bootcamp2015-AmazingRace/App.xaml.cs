using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Base.Services;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.ViewModels;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Helpers;

namespace Bootcamp2015.AmazingRace
{
    public sealed partial class App 
    {
        private WinRTContainer container;
        private SettingsService settings;
        private DataService dataService;
        private MobileService mobileService;

        public App()
        {
            this.InitializeComponent();
            settings = new SettingsService();
            mobileService = new MobileService();
            dataService = new DataService(mobileService);
        }

        #region Bootstrapper

        #region IOC

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();
            container.RegisterSharingService();
            
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<JoinTheTeamPageViewModel>();
            container.PerRequest<LeaderboardPageViewModel>();
            container.PerRequest<CluePageViewModel>();
            container.PerRequest<MapPageViewModel>();

            container.RegisterSingleton(typeof(IEventAggregator), "ea", typeof(EventAggregator));
            container.RegisterSingleton(typeof(IMessageDialogService), null, typeof(MessageDialogService));
            container.RegisterInstance(typeof(IMobileService), null, this.mobileService);
            container.RegisterInstance(typeof(ISettingsService), null, this.settings);
            container.RegisterInstance(typeof(IDataService), null, this.dataService);

            FakeIoC.GetInstance().DataService = this.dataService;
            FakeIoC.GetInstance().SettingsService = this.settings;

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

        public INavigationService NavigationService
        {
            get { return (INavigationService)container.GetInstance(typeof(INavigationService), null); }
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
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
            BackgroundTaskHelpers.BackgroundTaskRemove();
        }


        #endregion
    }
}