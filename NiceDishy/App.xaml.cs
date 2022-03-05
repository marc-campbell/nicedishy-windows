using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;

namespace NiceDishy
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

        private FastSpeedTest downloadTester;
        private FastSpeedTest uploadTester;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Register or Handle Uri Scheme
            HandleArguments(e);

            // Create the notifyicon (it's a resource declared in NotifyIconResources.xaml)
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            // Register Uri Scheme
            ApiManager.Shared.Initialize();
            ApiManager.Shared.RegisterUriScheme();

            // First push on start
            if (ApiManager.Shared.IsLoggedIn())
            {
                DishyService.Shared.GetDataAsync();
                DishyService.Shared.GetFastSpeed();
            }

            // Timer
            CreateTimers();

            // Fast Speed Test
            TestFastSpeed();
        }
        private void TestFastSpeed()
        {
            downloadTester = new FastSpeedTest();
            downloadTester.completedHandler += OnDownloadSpeedCompleted;
            downloadTester.Download();
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
        public void OnTokenUpdated()
        {
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

        #region Timer
        public void CreateTimers()
        {
            if (ApiManager.Shared.IsLoggedIn())
            {
                CreateDataTimer();
                CreateSpeedTestTimer();
            }
        }

        public void CreateDataTimer()
        {
            dataTimer = new DispatcherTimer();
            dataTimer.Interval = new TimeSpan(0, Preferences.FreqSendingData, 0);
            dataTimer.Tick += new EventHandler((sender, e) => DishyService.Shared.GetDataAsync());
            dataTimer.Start();
        }

        public void CreateSpeedTestTimer()
        {
            speedTestTimer = new DispatcherTimer();
            speedTestTimer.Interval = new TimeSpan(0, Preferences.FreqSendingData, 0);
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
