# Widget Manager

Widget Manager is a Windows application that enables you to create, load, and manage customizable, standalone widgets for your desktop. The project is released under the AGPL license. This repository provides an overview of Widget Manager and the supporting plugins (widgets), including their functionality and features.

![Clock Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/manager.jpg)

## Features
- **Lightweight and User-Friendly:** Widget Manager efficiently utilizes system resources.
- **Flexible Structure:** Users can activate/deactivate existing widgets or easily develop their own.
- **Dynamic Loading:** Widgets are loaded and unloaded as independent plugins.
- **Auto-Start on System Boot:** Automatically starts with Windows.

## Release Files
- **Widget Manager :** Includes only the Widget Manager application.
[Download](https://github.com/emretulek/Widgets/releases/download/v1.0.0/Widget.Manager.zip)
- **Widget Manager + All Widgets:** Includes Widget Manager and 10 different widgets.
[Download](https://github.com/emretulek/Widgets/releases/download/v1.0.0/Widgets.zip)

## Widgets

- [Clock](#clock)
- [CPU Monitor](#cpu-monitor)
- [Memory Monitor](#memory-monitor)
- [Network Monitor](#network-monitor)
- [Disk Monitor](#disk-monitor)
- [Disk Usage](#disk-usage)
- [Weather Widget](#weather-widget)
- [Finance Tracker](#finance-tracker)
- [Crypto Tracker](#crypto-tracker)
- [Radio](#radio)
- [SnapLate](#snaplate)

Common settings for all widgets include:
- Height / Width
- Max Height / Max Width
- Min Height / Min Width
- Top / Left positions (desktop location)
- Margin
- Padding
- Border Width
- Border Color
- Background Color
- Auto Width (WPF Size to Content)
- Resizable
- Draggable
- Interactive

settings.default.json configuration file example

```json
{
    "Widgets": {
        "ClockPlugin": {
            "IsActive": false,
            "Width": 400.0,
            "Height": 200.0,
            "MaxWidth": 99999.0,
            "MaxHeight": 99999.0,
            "MinWidth": 0.0,
            "MinHeight": 0.0,
            "Left": 200.0,
            "Top": 200.0,
            "Background": "#20000000",
            "BorderBrush": "#FF000000",
            "BorderThickness": "1,1,1,1",
            "Margin": "0,0,0,0",
            "Padding": "10,10,10,10",
            "AllowsTransparency": true,
            "WindowStyle": 0,
            "ResizeMode": 3,
            "ShowInTaskbar": false,
            "SizeToContent": 0,
            "IsHitTestVisible": true,
            "Dragable": true
        },
        "NetworkMonitorPlugin": {
            "IsActive": false,
            "Width": 400.0,
            "Height": 200.0,
            "MaxWidth": 99999.0,
            "MaxHeight": 99999.0,
            "MinWidth": 0.0,
            "MinHeight": 0.0,
            "Left": 209.0,
            "Top": 193.0,
            "Background": "#20000000",
            "BorderBrush": "#FF000000",
            "BorderThickness": "1,1,1,1",
            "Margin": "0,0,0,0",
            "Padding": "0,0,0,0",
            "AllowsTransparency": true,
            "WindowStyle": 0,
            "ResizeMode": 3,
            "ShowInTaskbar": false,
            "SizeToContent": 0,
            "IsHitTestVisible": true,
            "Dragable": true
        }
    }
}
```
The widgets are located under the Plugins directory, and each widget has its own folder. For example, the file paths for the Clock Plugin are as follows:

```
Widgets/Plugins/Clock/Clock.dll  
Widgets/Plugins/Clock/clock.settings.json
```

# Internal Settings

Internal settings are unique configuration files for each widget. Beyond general features, the creator of the plugin can use this settings file to make anything customizable, such as font size, background, or any other property.

```json
{
  "clock_font_size": 64.0,
  "clock_foreground": "#FF8B0000",
  "date_font_size": 18.0,
  "date_foreground": "#FFBC8F8F"
}
```

---

## Main Widgets

Below is a brief description of each widget and its corresponding repository link.

### Clock
Displays the current time and date.

[GitHub Repo](https://github.com/emretulek/Clock)

![Clock Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/clock.jpg)

---

### CPU Monitor
Monitors real-time CPU usage.

[GitHub Repo](https://github.com/emretulek/Cpu-Monitor)

![CPU Monitor Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/cpu_monitor.jpg)

---

### Memory Monitor
Tracks RAM usage, showing the current memory status, used, and free amounts.

[GitHub Repo](https://github.com/emretulek/Memory-Monitor)

![Memory Monitor Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/memory_monitor.jpg)

---

### Network Monitor
Tracks real-time network traffic, displaying download and upload speeds.

[GitHub Repo](https://github.com/emretulek/Network-Monitor)

![Network Monitor Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/network_monitor.jpg)

---

### Disk Monitor
Displays disk read/write capacity as a percentage. Can show specific disk usage (e.g., "C") or "Total" shared usage.

[GitHub Repo](https://github.com/emretulek/Disk-Monitor)

![Disk Monitor Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/disk_monitor.jpg)

---

### Disk Usage
Visualizes disk space usage, showing free and used percentages.

[GitHub Repo](https://github.com/emretulek/Disk-Usage)

![Disk Usage Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/disk_usage.jpg)

---

### Weather Widget
Provides 5-day weather forecasts.

[GitHub Repo](https://github.com/emretulek/Weather)

![Weather Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/weather.jpg)

---

### Finance Tracker
Tracks financial information like exchange rates, stocks, and indices in real time. Users can select financial instruments to monitor.

[GitHub Repo](https://github.com/emretulek/Finance-Tracker)

![Finance Tracker Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/finance_tracker.jpg)

---

### Crypto Tracker
Tracks cryptocurrencies on the Binance exchange in real time. Users can choose their favorite pairs.

[GitHub Repo](https://github.com/emretulek/Crypto-Tracker)

![Crypto Tracker Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/crypto_tracker.jpg)

---

### Radio
A widget designed for listening to internet radio stations. Built on VLCLib, it allows users to save their favorite stations.

[GitHub Repo](https://github.com/emretulek/Radio)

![Radio Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/radio.jpg)

---

### SnapLate
With this widget, you can use translate on your desktop, you can extract and translate the text and images from the text and images that cannot be copied.

[GitHub Repo](https://github.com/emretulek/SnapLate)

![SnapLate_Widget](https://raw.githubusercontent.com/emretulek/Widgets/refs/heads/master/srcreenshots/snaplate_1.jpg)

---

## Widgets.Common
**Widgets.Common** serves as the bridge between widgets and provides tools to simplify development for widget creators. It includes the essential base classes and examples for building widgets.

[GitHub Repo](https://github.com/emretulek/Widgets.Common)

---

## License
This project is released under the [AGPL License](https://www.gnu.org/licenses/agpl-3.0.html).

