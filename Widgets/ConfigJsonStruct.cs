using System.Windows;
using Widgets.Common;

namespace Widgets
{
    /// <summary>
    /// 
    /// </summary>
    internal class ConfigJsonStruct
    {
        public Dictionary<string, WidgetJsonStruct> Widgets { get; set; }

        public ConfigJsonStruct()
        {
            Widgets = [];
        }

        public static WidgetJsonStruct ConfigToJsonStruct(WidgetDefaultStruct configStruct)
        {
            return new WidgetJsonStruct()
            {
                IsActive = configStruct.IsActive,
                Width = configStruct.Width,
                Height = configStruct.Height,
                MaxWidth = configStruct.MaxWidth,
                MaxHeight = configStruct.MaxHeight,
                MinWidth = configStruct.MinWidth,
                MinHeight = configStruct.MinHeight,
                Top = configStruct.Top,
                Left = configStruct.Left,
                Background = configStruct.Background.ToString(),
                BorderBrush = configStruct.BorderBrush.ToString(),
                BorderThickness = configStruct.BorderThickness,
                Margin = configStruct.Margin,
                Padding = configStruct.Padding,
                AllowsTransparency = configStruct.AllowsTransparency,
                WindowStyle = configStruct.WindowStyle,
                ResizeMode = configStruct.ResizeMode,
                ShowInTaskbar = configStruct.ShowInTaskbar,
                SizeToContent = configStruct.SizeToContent,
                IsHitTestVisible = configStruct.IsHitTestVisible,
                DesktopIntegration = configStruct.DesktopIntegration,
                Dragable = configStruct.Dragable,
            };
        }

        public static WidgetDefaultStruct JsonToConfigStruct(WidgetJsonStruct defaultStruct)
        {
            return new WidgetDefaultStruct()
            {
                IsActive = defaultStruct.IsActive,
                Width = defaultStruct.Width,
                Height = defaultStruct.Height,
                MaxWidth = defaultStruct.MaxWidth,
                MaxHeight = defaultStruct.MaxHeight,
                MinWidth = defaultStruct.MinWidth,
                MinHeight = defaultStruct.MinHeight,
                Top = defaultStruct.Top,
                Left = defaultStruct.Left,
                Background = PropertyParser.ToColorBrush(defaultStruct.Background),
                BorderBrush = PropertyParser.ToColorBrush(defaultStruct.BorderBrush),
                BorderThickness = defaultStruct.BorderThickness,
                Margin = defaultStruct.Margin,
                Padding = defaultStruct.Padding,
                WindowStyle = defaultStruct.WindowStyle,
                ResizeMode = defaultStruct.ResizeMode,
                ShowInTaskbar = defaultStruct.ShowInTaskbar,
                AllowsTransparency= defaultStruct.AllowsTransparency,
                SizeToContent= defaultStruct.SizeToContent,
                IsHitTestVisible = defaultStruct.IsHitTestVisible,
                DesktopIntegration = defaultStruct.DesktopIntegration,
                Dragable = defaultStruct.Dragable,
            };
        }
    }

    public class WidgetJsonStruct
    {
        public bool IsActive { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double MaxWidth { get; set; }
        public double MaxHeight { get; set; }
        public double MinWidth { get; set; }
        public double MinHeight { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public string Background { get; set; } = "";
        public string BorderBrush { get; set; } = "";
        public Thickness BorderThickness { get; set; }
        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public bool AllowsTransparency { get; set; }
        public WindowStyle WindowStyle { get; set; }
        public ResizeMode ResizeMode { get; set; }
        public bool ShowInTaskbar { get; set; }
        public SizeToContent SizeToContent { get; set; }
        public bool IsHitTestVisible { get; set; }
        public bool DesktopIntegration { get; set; }
        public bool Dragable { get; set; }
    };
}
