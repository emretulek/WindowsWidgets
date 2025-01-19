using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Widgets.Common;
using static Widgets.Config;

namespace Widgets
{
    public partial class WidgetViewModel : INotifyPropertyChanged
    {
        public WidgetDefaultStruct WidgetSettings;

        public IPlugin Plugin;
        public string WidgetID;
        private TaskCompletionSource<bool>? widgetLoaded;

        public WidgetViewModel(IPlugin Plugin)
        {
            this.Plugin = Plugin;
            WidgetID = Plugin.GetType().Name;
            WidgetSettings = Instance.GetConfig(WidgetID, Plugin.WidgetDefaultStruct());
            IsActive = WidgetSettings.IsActive;
        }

        /// <summary>
        /// Widget Window Instance
        /// </summary>
        /// <returns></returns>
        private bool WidgetInit()
        {
            if (WidgetWindow == null)
            {
                try
                {
                    widgetLoaded = new TaskCompletionSource<bool>();
                    WidgetWindow = Plugin.WidgetWindow();
                    WidgetWindow.SetWidgetStruct(WidgetSettings);
                    WidgetWindow.Window.Loaded += WidgetWindow_Loaded;
                    WidgetWindow.Window.Activated += WidgetWindow_Activated;
                }
                catch (Exception ex)
                {
                    Logger.Error($"Widget Window Instance:{ex.Message}");
                }
            }

            return WidgetWindow != null;
        }

        /// <summary>
        /// Open Widget Window
        /// </summary>
        private void WidgetOpen()
        {
            if (WidgetInit() && WidgetWindow is not null)
            {
                if (_isActive && !WidgetWindow.Window.IsVisible)
                {
                    WidgetWindow.Window.Show();
                    Logger.Info($"Plugin Activated: {Plugin.Name}");
                }
            }
        }

        /// <summary>
        /// Close Widget Window
        /// </summary>
        private void WidgetClose()
        {
            if (WidgetInit() && WidgetWindow is not null)
            {
                if (!_isActive && WidgetWindow.Window.IsVisible)
                {
                    WidgetWindow.Window.Close();
                    WidgetWindow.Window.Loaded -= WidgetWindow_Loaded;
                    WidgetWindow.Window.Activated -= WidgetWindow_Activated;
                    WidgetWindow = null;
                    Logger.Info($"Plugin Deactivated: {Plugin.Name}");
                }
            }
        }

        /// <summary>
        /// Aktif pasif listbox ve checknox binding
        /// </summary>
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;

                    WidgetSettings.IsActive = _isActive;
                    Instance.SetConfig(WidgetID, WidgetSettings);
                    Instance.Save();

                    if(_isActive)
                    {
                        WidgetOpen();
                    }
                    else
                    {
                        WidgetClose();
                    }

                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }


        /// <summary>
        /// Formdaki widget Name binding
        /// </summary>
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Window kapandığında aktif olanları pasife çekme
        /// </summary>
        private WidgetWindow? _widget_window;
        private WidgetWindow? WidgetWindow
        {
            get { return _widget_window; }
            set
            {
                _widget_window = value;

                if (_widget_window != null && _widget_window.Window.IsVisible)
                {
                    _widget_window.Window.Closing += (s, e) =>
                    {
                        _isActive = false;
                        OnPropertyChanged(nameof(IsActive));
                        _widget_window = null;
                    };
                }
            }
        }

        /// <summary>
        /// Widget Width
        /// </summary>
        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Width = _width;
                }
                OnPropertyChanged(nameof(Width));
            }
        }

        /// <summary>
        /// Widget Height
        /// </summary>
        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Height = _height;
                }
                OnPropertyChanged(nameof(Height));
            }
        }

        /// <summary>
        /// Widget MaxWidth
        /// </summary>
        private double _maxWidth;
        public double MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                _maxWidth = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.MaxWidth = _maxWidth;
                }
                OnPropertyChanged(nameof(MaxWidth));
            }
        }

        /// <summary>
        /// Widget MaxHeight
        /// </summary>
        private double _maxHeight;
        public double MaxHeight
        {
            get { return _maxHeight; }
            set
            {
                _maxHeight = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.MaxHeight = _maxHeight;
                }
                OnPropertyChanged(nameof(MaxHeight));
            }
        }

        /// <summary>
        /// Widget MinWidth
        /// </summary>
        private double _minWidth;
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.MinWidth = _minWidth;   
                }
                OnPropertyChanged(nameof(MinWidth));
            }
        }

        /// <summary>
        /// Widget MinHeight
        /// </summary>
        private double _minHeight;
        public double MinHeight
        {
            get { return _minHeight; }
            set
            {
                _minHeight = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.MinHeight = _minHeight;
                }
                OnPropertyChanged(nameof(MinHeight));
            }
        }

        /// <summary>
        /// Widget Left Position
        /// </summary>
        private double _left;
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Left = _left;
                }
                OnPropertyChanged(nameof(Left));
            }
        }

        /// <summary>
        /// Widget Top Position
        /// </summary>
        private double _top;
        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Top = _top;
                }
                OnPropertyChanged(nameof(Top));
            }
        }

        /// <summary>
        /// Widget Border Width
        /// </summary>
        private Thickness _border;
        public Thickness Border
        {
            get { return _border; }
            set
            {
                _border = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.BorderThickness = PropertyParser.ToThickness(value);
                }
                OnPropertyChanged(nameof(Border));
            }
        }

        /// <summary>
        /// Widget Margin
        /// </summary>
        private Thickness _margin;
        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Margin = PropertyParser.ToThickness(value);
                }
                OnPropertyChanged(nameof(Margin));
            }
        }

        /// <summary>
        /// Widget Padding
        /// </summary>
        private Thickness _padding;
        public Thickness Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Padding = PropertyParser.ToThickness(value);
                }
                OnPropertyChanged(nameof(Padding));
            }
        }

        /// <summary>
        /// Widget Border Color
        /// </summary>
        private Color _borderColor;
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.BorderBrush = PropertyParser.ToColorBrush(value);
                }
                OnPropertyChanged(nameof(BorderColor));
            }
        }

        /// <summary>
        /// Widget Background Color
        /// </summary>
        private Color _background;
        public Color Background
        {
            get { return _background; }
            set
            {
                _background = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.Background = PropertyParser.ToColorBrush(value);
                }
                OnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Widget SizeToContent
        /// </summary>
        private int _sizeToContent;
        public int SizeToContent
        {
            get { return _sizeToContent; }
            set
            {
                _sizeToContent = value;

                if (WidgetWindow != null)
                {
                    WidgetWindow.SizeToContent = (SizeToContent)_sizeToContent;

                    if (_sizeToContent == 0)
                    {
                        MaxWidth = 99999;
                        MaxHeight = 99999;
                        MinWidth = 0;
                        MinHeight = 0;
                    }else if(_sizeToContent == 1)
                    {
                        MaxHeight = 99999;
                        MinHeight = 0;
                    }else if(_sizeToContent == 2)
                    {
                        MaxWidth = 99999;
                        MinWidth = 0;
                    }
                    else
                    {}

                    OnPropertyChanged(nameof(SizeToContent));
                }
            }
        }

        /// <summary>
        /// Widget Dragable
        /// </summary>
        private bool _dragable;
        public bool Dragable
        {
            get { return _dragable; }
            set
            {
                _dragable = value;
                WidgetDragableUpdate();
                OnPropertyChanged(nameof(Dragable));
            }
        }

        /// <summary>
        /// Widget Resizable
        /// </summary>
        private int _resizable;
        public int Resizable
        {
            get { return _resizable; }
            set
            { 
                _resizable = value;
                WidgetResizableUpdate();
                OnPropertyChanged(nameof(Resizable)); 
            }
        }

        /// <summary>
        /// Widget Interactive
        /// </summary>
        private bool _interactive;
        public bool Interactive
        {
            get { return _interactive; }
            set
            {
                _interactive = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.IsHitTestVisible = _interactive;
                }
                OnPropertyChanged(nameof(Interactive));
            }
        }

        /// <summary>
        /// Widget Dragable
        /// </summary>
        private bool _desktopIntegration;
        public bool DesktopIntegration
        {
            get { return _desktopIntegration; }
            set
            {
                _desktopIntegration = value;
                if (WidgetWindow != null)
                {
                    WidgetWindow.DesktopIntegration = _desktopIntegration;
                }
                OnPropertyChanged(nameof(DesktopIntegration));
            }
        }

        //widget is Dragable
        private void WidgetDragableUpdate()
        {
            if (WidgetWindow != null && WidgetWindow.Window.IsVisible)
            {
                if (_dragable == true)
                {
                    WidgetWindow.Window.MouseDown -= Widget_MouseDown_Dragable;
                    WidgetWindow.Window.MouseDown += Widget_MouseDown_Dragable;
                }
                else
                {
                    WidgetWindow.Window.MouseDown -= Widget_MouseDown_Dragable;
                }
            }
        }

        // Widget window dragmove
        private void Widget_MouseDown_Dragable(object sender, MouseButtonEventArgs e)
        {
            if (WidgetWindow != null && e.LeftButton == MouseButtonState.Pressed)
            {
                WidgetWindow.Window.DragMove();

                if (e.LeftButton == MouseButtonState.Released)
                {
                    WidgetSettings.Left = Left = WidgetWindow.Left;
                    WidgetSettings.Top = Top = WidgetWindow.Top;
                    Instance.SetConfig(WidgetID, WidgetSettings);
                }
            }
        }

        //widget is Resizable
        private void WidgetResizableUpdate()
        {
            if (WidgetWindow != null && WidgetWindow.Window.IsVisible)
            {
                if (_resizable == (int) ResizeMode.NoResize)
                {
                    WidgetWindow.ResizeMode = ResizeMode.NoResize;
                    WidgetWindow.Window.SizeChanged -= Widget_Resize;
                }
                else
                {
                    WidgetWindow.ResizeMode = ResizeMode.CanResizeWithGrip;
                    WidgetWindow.Window.SizeChanged += Widget_Resize;
                }
            }
        }

        // Widget window resize
        private void Widget_Resize(object sender, SizeChangedEventArgs e)
        {
            if (WidgetWindow != null && WidgetWindow.Window.IsActive)
            {
                Width = WidgetWindow.Window.ActualWidth;
                Height = WidgetWindow.Window.ActualHeight;
            }
        }

        // Widget window Loaded
        private void WidgetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Resizable = (int)WidgetSettings.ResizeMode;
                Dragable = WidgetSettings.Dragable;
                DesktopIntegration = WidgetSettings.DesktopIntegration;
            }
            finally
            {
                widgetLoaded?.SetResult(true);
            }
        }

        // Widget window Activated
        public async Task WidgetLoaded()
        {
            if (widgetLoaded != null)
            {
                await widgetLoaded.Task;
            }
        }

        // Widget window Activated
        private void WidgetWindow_Activated(object? sender, EventArgs e)
        {
            if (Application.Current.MainWindow is Manager manager)
                manager.WidgetListBox.SelectedItem = this;
        }

        public void InitSettings()
        {
            _width = WidgetSettings.Width;
            _height = WidgetSettings.Height;
            _maxWidth = WidgetSettings.MaxWidth;
            _maxHeight = WidgetSettings.MaxHeight;
            _minWidth = WidgetSettings.MinWidth;
            _minHeight = WidgetSettings.MinHeight;
            _left = WidgetSettings.Left;
            _top = WidgetSettings.Top;
            _border = WidgetSettings.BorderThickness;
            _margin = WidgetSettings.Margin;
            _padding = WidgetSettings.Padding;
            _borderColor = WidgetSettings.BorderBrush.Color;
            _background = WidgetSettings.Background.Color;
            _sizeToContent = (int)WidgetSettings.SizeToContent;
            _interactive = WidgetSettings.IsHitTestVisible;
            _resizable = (int)WidgetSettings.ResizeMode;
            _dragable = WidgetSettings.Dragable;
            _desktopIntegration = WidgetSettings.DesktopIntegration;
        }

        public WidgetDefaultStruct ExportSettings()
        {
            WidgetSettings.Width = Width;
            WidgetSettings.Height = Height;
            WidgetSettings.MaxWidth = MaxWidth;
            WidgetSettings.MaxHeight = MaxHeight;
            WidgetSettings.MinWidth = MinWidth;
            WidgetSettings.MinHeight = MinHeight;
            WidgetSettings.Left = Left;
            WidgetSettings.Top = Top;
            WidgetSettings.BorderThickness = Border;
            WidgetSettings.Margin = Margin;
            WidgetSettings.Padding = Padding;
            WidgetSettings.SizeToContent = (SizeToContent)SizeToContent;
            WidgetSettings.ResizeMode = (ResizeMode)Resizable;
            WidgetSettings.Dragable = Dragable;
            WidgetSettings.IsHitTestVisible = Interactive;
            WidgetSettings.DesktopIntegration = DesktopIntegration;
            WidgetSettings.BorderBrush = new SolidColorBrush(BorderColor);
            WidgetSettings.Background = new SolidColorBrush(Background);
            
            return WidgetSettings;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
