using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace Widgets
{
    internal partial class PropertyParser
    {

        [GeneratedRegex(@"argb\((\d+),\s*(\d+),\s*(\d+),\s*(\d+)\)")]
        private static partial Regex ArgbRegex();
        [GeneratedRegex(@"rgb\((\d+),\s*(\d+),\s*(\d+)\)")]
        private static partial Regex RgbRegex();
        [GeneratedRegex(@"(#[0-9a-fA-F]{3,8})")]
        private static partial Regex HexRegex();
        [GeneratedRegex(@"^(\d+),(\d+),(\d+),(\d+)$")]
        private static partial Regex PropertiesRegex();
        [GeneratedRegex(@"\""(\w+)\"":\s*([\d.]+)")]
        private static partial Regex PropertiesRegex2();
        [GeneratedRegex(@"^\d+$")]
        private static partial Regex PropertyRegex();

        /// <summary>
        /// Object to Bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is not bool)
            {
                if (bool.TryParse(value.ToString(), out bool result))
                {
                    return result;
                }
                else
                {
                    return false;
                }
            }

            return (bool)value;
        }

        /// <summary>
        /// Object to float
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static float ToFloat(object? value, float _default = 0)
        {
            value ??= _default;
            if (value is float f)
            {
                return f;
            }
            else if (value is double d)
            {
                return (float)d;
            }
            else if (value is int i)
            {
                return (float)i;
            }
            else if (value.ToString() is not null && float.TryParse(value.ToString(), out float result))
            {
                return result;
            }
            else
            {
                return (float)_default;
            }
        }

        /// <summary>
        /// Object to ColorBrush
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static SolidColorBrush ToColorBrush(object? value, string _default = "#FFFFFF")
        {
            value ??= _default;
            string stringValue = value.ToString() ?? "";

            Color color;

            var match = ArgbRegex().Match(stringValue);
            var match2 = RgbRegex().Match(stringValue);
            var match3 = HexRegex().Match(stringValue);

            if (match.Success)
            {
                byte a = byte.Parse(match.Groups[1].Value);
                byte r = byte.Parse(match.Groups[2].Value);
                byte g = byte.Parse(match.Groups[3].Value);
                byte b = byte.Parse(match.Groups[4].Value);
                color = Color.FromArgb(a, r, g, b);
            }
            else if (match.Success)
            {

                byte r = byte.Parse(match2.Groups[1].Value);
                byte g = byte.Parse(match2.Groups[2].Value);
                byte b = byte.Parse(match2.Groups[3].Value);
                color = Color.FromRgb(r, g, b);
            }
            else if (match3.Success)
            {
                color = (Color)ColorConverter.ConvertFromString(match3.Groups[1].Value);
            }
            else
            {
                color = (Color)ColorConverter.ConvertFromString(_default);
            }


            SolidColorBrush brush = new(color);
            return brush;
        }


        /// <summary>
        /// Border
        /// </summary>
        /// <param name="property"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static Thickness ToThickness(object? property, int _default = 1)
        {
            property ??= _default;

            string _property = property.ToString() ?? "";
            int[] properties; ;

            var matches = PropertiesRegex().Match(_property);
            var matches2 = PropertiesRegex2().Matches(_property);

            if (PropertyRegex().IsMatch(_property))
            {
                properties = [int.Parse(_property)];
                return new Thickness(properties[0]);
            }
            else if (matches.Success)
            {
                properties = [
                    int.Parse(matches.Groups[1].Value),
                    int.Parse(matches.Groups[2].Value),
                    int.Parse(matches.Groups[3].Value),
                    int.Parse(matches.Groups[4].Value)
                ];

                return new Thickness(properties[0], properties[1], properties[2], properties[3]);
            }
            else if (matches2.Count > 0)
            {
                properties = new int[matches2.Count];

                var i = 0;
                foreach (Match match in matches2)
                {
                    properties[i] = int.Parse(match.Groups[2].Value);
                    i++;
                }

                if (properties.Length == 2)
                {
                    return new Thickness(properties[0], properties[1], properties[0], properties[1]);
                }

                if (properties.Length == 4)
                {
                    return new Thickness(properties[0], properties[1], properties[2], properties[3]);
                }
            }

            return new Thickness(_default);
        }
    }
}
