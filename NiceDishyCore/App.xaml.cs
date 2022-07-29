using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using System.Threading;

namespace NiceDishyCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TaskbarIcon notifyIcon;
        public Preferences preferences;

        private DispatcherTimer dataTimer;
        private DispatcherTimer speedTestTimer;
        private DispatcherTimer registryWatchTimer;

        private FastSpeedTest downloadTester;
        private FastSpeedTest uploadTester;

        private static Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            // ensure there's a single instance
            const string appName = "NiceDishy";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);
            if (!createdNew)
            {
                // write the token, if there is one in the args
                HandleArguments(e);

                // and shutdown
                Application.Current.Shutdown();
            }

            base.OnStartup(e);

            HandleArguments(e);

            // Create the notifyicon (it's a resource declared in NotifyIconResources.xaml)
            App app = (App)Application.Current;

            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            notifyIcon = (TaskbarIcon)app.FindResource("NotifyIcon");

            // Register Uri Scheme
            ApiManager.Shared.Initialize();
            ApiManager.Shared.RegisterUriScheme();

            refreshToken(true);
            createRegistryWatchTimer();

        }
        private void OnDownloadSpeedCompleted(double speed)
        {
            Console.WriteLine("Final Download Speed is {0} Mbps", (long)speed / (1024 * 1024));
            uploadTester = new FastSpeedTest();
            uploadTester.completedHandler += OnUploadSpeedCompleted;
            uploadTester.Upload();
        }
        private void OnUploadSpeedCompleted(double speed)
        {
            Console.WriteLine("Final Upload Speed is {0} Mbps", (long)speed / (1024 * 1024));

            DishyService.Shared.PushSpeed();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

        private void refreshToken(bool force)
        {
            var currentToken = ApiManager.Shared.Token;
            var registryToken = ApiManager.Shared.readToken();

            if (force || currentToken != registryToken)
            {
                ApiManager.Shared.Token = registryToken;

                if (string.IsNullOrEmpty(ApiManager.Shared.Token)) 
                {
                    ContextMenu menu = (ContextMenu)FindResource("SysTrayMenu");
                    notifyIcon.ContextMenu = menu;
                } 
                else
                {
                    ContextMenu menu = (ContextMenu)FindResource("SysTrayMenuLoggedIn");
                    notifyIcon.ContextMenu = menu;
                }

                CreateTimers();
            }
        }
        public void OnTokenUpdated()
        {
            if (string.IsNullOrEmpty(ApiManager.Shared.Token))
            {
                ContextMenu menu = (ContextMenu)FindResource("SysTrayMenu");
                notifyIcon.ContextMenu = menu;
            }
    

            CreateTimers();
        }

        public void OnDataIntervalUpdated()
        {
            CreateDataTimer();
        }

        public void OnSpeedIntervalUpdated()
        {
            CreateSpeedTestTimer();
        }

        private void createRegistryWatchTimer()
        {
            registryWatchTimer = new DispatcherTimer();
            registryWatchTimer.Interval = new TimeSpan(0, 0, 3);
            registryWatchTimer.Tick += new EventHandler((sender, e) => this.refreshToken(false));
            registryWatchTimer.Start();
        }
        #region Timer
        public void CreateTimers()
        {
            Console.WriteLine("CreateTimers");
            if (ApiManager.Shared.IsLoggedIn())
            {
                CreateDataTimer();
                CreateSpeedTestTimer();
            }
        }

        public void CreateDataTimer()
        {
            if (dataTimer != null)
            {
                dataTimer.Stop();
            }

            // run immediately
            DishyService.Shared.GetDataAsync();

            dataTimer = new DispatcherTimer();
            dataTimer.Interval = new TimeSpan(0, Preferences.FreqSendingData, 0);
            dataTimer.Tick += new EventHandler((sender, e) => DishyService.Shared.GetDataAsync());
            dataTimer.Start();
        }

        public void CreateSpeedTestTimer()
        {
            if (speedTestTimer != null)
            {
                speedTestTimer.Stop();
            }

            // run immediately
            DishyService.Shared.GetFastSpeed();

            speedTestTimer = new DispatcherTimer();
            speedTestTimer.Interval = new TimeSpan(0, Preferences.FreqSpeedTests, 0);
            speedTestTimer.Tick += new EventHandler((sender, e) => DishyService.Shared.GetFastSpeed());
            speedTestTimer.Start();
        }

        #endregion

        private void HandleArguments(StartupEventArgs e)
        {
            Uri uri = null;
            if (e.Args.Length > 0)
            {
                // a URI was passed and needs to be handled
                try
                {
                    uri = new Uri(e.Args[0].Trim());
                }
                catch (UriFormatException)
                {
                    Console.WriteLine("Invalid URI.");
                }
            }

            IUriHandler handler = UriHandler.GetHandler();
            if (handler != null)
            {
                // the singular instance of the application is already running
                if (uri != null) handler.HandleUri(uri);

                // the process will now exit without displaying the main form
                Shutdown();
            }
            else
            {
                // this must become the singular instance of the application
                UriHandler.Register();

                if (uri != null) new UriHandler().HandleUri(uri);
            }
        }

    }
}
