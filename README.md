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

## Widgets.Common
**Widgets.Common** serves as the bridge between widgets and provides tools to simplify development for widget creators. It includes the essential base classes and examples for building widgets.

[GitHub Repo](#)

---

## License
This project is released under the [AGPL License](https://www.gnu.org/licenses/agpl-3.0.html).

