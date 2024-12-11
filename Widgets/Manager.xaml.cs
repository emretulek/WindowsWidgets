using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;
using Widgets.Common;
using static Widgets.Config;
using System.IO;
using System.Windows.Controls;

namespace Widgets
{
    public partial class Manager : Window, INotifyPropertyChanged
    {

        /// <summary>
        /// Auto Import Plugins
        /// </summary>
        [ImportMany(typeof(IPlugin))]
        public IEnumerable<IPlugin>? Plugins { get; set; }

        /// <summary>
        /// Selected Widget
        /// </summary>
        public ObservableCollection<WidgetViewModel> Widgets { get; set; } = [];
        public required WidgetViewModel _selectedWidget;
        public WidgetViewModel SelectedWidget
        {
            get { return _selectedWidget; }
            set
            {
                if (_selectedWidget != value)
                {
                    _selectedWidget = value;
                    if (SelectedWidget?.WidgetID != null)
                    {
                        SelectedWidget.InitSettings();
                    }
                }
                OnPropertyChanged(nameof(SelectedWidget));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Manager Constructer
        /// </summary>
        public Manager()
        {
            InitializeComponent(); 
            LoadPlugins();
            DataContext = this;
            SourceInitialized += WindowSourceInitialized;   
        }

        private void WindowSourceInitialized(object? sender, EventArgs e)
        {
            App.HideAfterStartup();
            CreateWidgetList();
            StartupCheckBox.IsChecked = App.IsRegisteredOnStartup();
        }

        // widget list to listbox
        public void CreateWidgetList()
        {
            WidgetListBox.ItemsSource = Widgets;

            if (Plugins is null)
            {
                return;
            }

            // Add plugins to widget list
            foreach (var plugin in Plugins)
            {
                try
                {
                    var widgetViewModel = new WidgetViewModel(plugin)
                    {
                        Name = plugin.Name,
                    };

                    Widgets.Add(widgetViewModel);

                }
                catch (Exception ex)
                {
                    Logger.Error("Manager 56: " + ex.Message + " Trace: " + ex.StackTrace);
                }
            }

            if (Widgets.Any())
            {
                SelectedWidget = Widgets.First();
            }
        }


        /// <summary>
        /// Load Plugins
        /// </summary>
        public void LoadPlugins()
        {
            try
            {
                var catalog = new AggregateCatalog();
                var subdirectories = Directory.GetDirectories(App.pluginsPath);

                foreach (var subdirectory in subdirectories)
                {
                    try
                    {
                        var pluginFiles = Directory.GetFiles(subdirectory, "*.dll", SearchOption.TopDirectoryOnly);

                        foreach (var pluginFile in pluginFiles)
                        {
                            try
                            {
                                catalog.Catalogs.Add(new AssemblyCatalog(pluginFile));
                            }
                            catch (Exception ex)
                            {
                                Logger.Warning($"Failed to load plugin: {pluginFile}. Error: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning($"Failed to process directory: {subdirectory}. Error: {ex.Message}");
                    }
                }

                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                var exports = container.GetExports<IPlugin>();

                foreach (var export in exports)
                {
                    Logger.Info($"Plugin loaded: {export.Value.Name} - {export.Value.GetType().Assembly.Location}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Critical error during plugin loading: {ex.Message}");
            }
        }

        /// <summary>
        /// Window drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_DragMove(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Window Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Close(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.Close();
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Window Minimize to (Task bar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Min(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// Refresh Widget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedWidget?.Plugin?.ConfigFile != null)
            {
                var pluginPath = Path.GetDirectoryName(SelectedWidget.Plugin.GetType().Assembly.Location) ?? "";
                var configFilePath = Path.Combine(pluginPath, SelectedWidget.Plugin.ConfigFile);

                if (File.Exists(configFilePath))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(configFilePath) { UseShellExecute = true });
                        return;
                    }
                    catch (Exception)
                    {
                        Process.Start(new ProcessStartInfo("notepad.exe", configFilePath) { UseShellExecute = true });
                        return;
                    }
                }

                MessageBox.Show("There is no setting file");
            }
        }

        /// <summary>
        /// Anapencere kapanırsa hepsini kapat
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            foreach (Window window in Application.Current.Windows)
            {
                if (window != this) // Ana pencereyi atlayın
                {
                    window.Close();
                }
            }
        }

        /// <summary>
        /// Ayarlarıın Kaydedilmesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WidgetListBox.SelectedItem is WidgetViewModel selectedItem)
            {
                Instance.SetConfig(selectedItem.WidgetID, selectedItem.ExportSettings());
                Instance.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Startup_Click(object? sender, RoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is CheckBox checkBox)
            {
                App.RegisterOnStartup(checkBox.IsChecked ?? false);
            }
        }
    }
}