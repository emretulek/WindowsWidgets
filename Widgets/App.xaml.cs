using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Widgets.Common;
using Form = System.Windows.Forms;


namespace Widgets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static string AppName = "Widgets";
        private readonly static string? AppPath = Process.GetCurrentProcess().MainModule?.FileName;
        private readonly static RegistryKey? RegisterKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        public readonly static string pluginsPath = Widgets.Common.Config.PluginPath;
        public readonly static string SettingsDefaultFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.default.json");
        public readonly static string IconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Widget.ico");

        private static Mutex? _mutex;
        readonly Form.NotifyIcon nIcon = new();

        public App()
        {
            // Unhandled Excepitons
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        // Register for startup
        public static void RegisterOnStartup(bool state)
        {  
            if (RegisterKey == null) return;

            if (state)
            {
                RegisterKey.SetValue(AppName, $"\"{AppPath}\" -Startup");
            }
            else
            {
                RegisterKey.DeleteValue(AppName, false);
            }
        }

        public static bool IsRegisteredOnStartup()
        {
            return RegisterKey?.GetValue(AppName) != null;
        }

        // hide if launcher is startup
        public static void HideAfterStartup()
        {  
            string[] args = Environment.GetCommandLineArgs();
            bool isStartup = args.Contains("-Startup");

            if (isStartup)
            {
                Current.MainWindow.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Single instance
            _mutex = new Mutex(true, @"Global\Widgets_SingleInstance", out bool isSingleInstance);
            GC.KeepAlive(_mutex);

            if (!isSingleInstance)
            {
                Current.Shutdown();
                return;
            }

            StartLogger();
            SystemTrayIcon();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            nIcon.Dispose();
            Logger.CloseWithFlush();
            base.OnExit(e);
        }


        /// <summary>
        /// 
        /// </summary>
        private void SystemTrayIcon()
        {
            nIcon.Icon = new Icon(IconPath);
            nIcon.Text = "Widgets";
            nIcon.Visible = true;

            Form.ContextMenuStrip _contextMenu = new ();
            var exitMenuItem = new Form.ToolStripMenuItem("Exit", new Icon(IconPath).ToBitmap(), OnExitClicked);
            //var titleLabel = new Form.ToolStripLabel("Widgets", new Icon("Widget.ico").ToBitmap());
            //_contextMenu.Items.Add(titleLabel);
            _contextMenu.Items.Add(exitMenuItem);
            nIcon.ContextMenuStrip = _contextMenu;
            //nIcon.ShowBalloonTip(5000, "Title", "Text", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.MouseClick += NIcon_Click;
        }

        private void NIcon_Click(object? sender, Form.MouseEventArgs e)
        {
            if (e.Button == Form.MouseButtons.Left)
            {
                if (MainWindow.IsVisible)
                {
                    MainWindow.Visibility = Visibility.Hidden;
                }
                else
                {
                    MainWindow.Visibility = Visibility.Visible;
                    MainWindow.WindowState = WindowState.Normal;
                    MainWindow.Activate();
                }
            }
            else if (e.Button == Form.MouseButtons.Right)
            {
                nIcon?.ContextMenuStrip?.Show();
            }
        }

        private void OnExitClicked(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

       /// <summary>
       /// 
       /// </summary>
        private void StartLogger()
        {
            Logger.WriteLogSchedule(5);
            Logger.LogEvent += HandleLogEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="logMessage"></param>
        private void HandleLogEvent(object? sender, LogMessage logMessage)
        {
            Debug.WriteLine(logMessage.ToString());

            var logFormat = $"[{logMessage.Timestamp}] [{logMessage.Level}] [{logMessage.PluginName}] {logMessage.Message}";

            Logger.BufferLog(logFormat);
        }

        // Unhandled excepitons
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Warning(e.Exception.Message);
            MessageBox.Show($"Error: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        // Unhandled excepitons
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                Logger.Error(exception.Message);
                //MessageBox.Show($"Error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Unhandled excepitons
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Error(e.Exception.Message);
            //MessageBox.Show($"Error: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved();
        }
    }
}
