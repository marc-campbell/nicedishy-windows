using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;

namespace NiceDishy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Register or Handle Uri Scheme
            HandleArguments(e);

            // Register Uri Scheme
            ApiManager.Shared.RegisterUriScheme();

            // Create the notifyicon (it's a resource declared in NotifyIconResources.xaml)
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
        public void UpdateNotifyIcon()
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
        }

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
