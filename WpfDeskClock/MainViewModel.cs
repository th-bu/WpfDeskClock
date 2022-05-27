namespace WpfDeskClock;

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Threading;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer timer = new();
    private readonly TimeSpan checkTime;
    private DateTime displayDateTime = DateTime.Now;
    private Brush fontColor = Brushes.White;

    public MainViewModel()
    {
        this.FontFamily = new FontFamily(ConfigurationManager.AppSettings["Font"] ?? "Segoe UI");
        this.checkTime = TimeSpan.Parse(ConfigurationManager.AppSettings["CheckTime"] ?? "09:16:00");

        this.timer.Tick += this.TimerTick;
        this.timer.Interval = new TimeSpan(0, 0, 1);
        this.timer.Start();
    }

    public DateTime DisplayDateTime
    {
        get => this.displayDateTime;
        private set
        {
            this.displayDateTime = value;
            this.OnPropertyChanged();
        }
    }

    public Brush FontColor
    {
        get => this.fontColor;
        private set
        {
            this.fontColor = value;
            this.OnPropertyChanged();
        }
    }

    public FontFamily FontFamily { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void TimerTick(object? sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        this.DisplayDateTime = now;

        if (now.Hour >= this.checkTime.Hours && now.Minute >= this.checkTime.Minutes && now.Second >= this.checkTime.Seconds)
        {
            this.FontColor = Brushes.DeepSkyBlue;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}