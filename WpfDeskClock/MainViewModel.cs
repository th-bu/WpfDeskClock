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
    private DateTime displayDateTime;
    private Brush fontColor = Brushes.White;

    public MainViewModel()
    {
        this.FontFamily = new FontFamily(ConfigurationManager.AppSettings["Font"] ?? "Segoe UI");
        this.checkTime = TimeSpan.Parse(ConfigurationManager.AppSettings["CheckTime"] ?? "09:16:00");
        this.SetTimeAndFontColor();

        this.timer.Tick += this.TimerTick;
        this.timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
        this.timer.Start();
    }

    public DateTime DisplayDateTime
    {
        get => this.displayDateTime;
        private set
        {
            if (this.displayDateTime.CompareTo(value) != 0)
            {
                this.displayDateTime = value;
                this.OnPropertyChanged();
            }
        }
    }

    public Brush FontColor
    {
        get => this.fontColor;
        private set
        {
            if (this.fontColor != value)
            {
                this.fontColor = value;
                this.OnPropertyChanged();
            }
        }
    }

    public FontFamily FontFamily { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void TimerTick(object? sender, EventArgs e)
    {
        this.SetTimeAndFontColor();
    }

    private void SetTimeAndFontColor()
    {
        DateTime now = DateTime.Now;
        this.DisplayDateTime = now;

        if (now.TimeOfDay.CompareTo(this.checkTime) >= 0)
        {
            this.FontColor = Brushes.DeepSkyBlue;
        }
        else
        {
            this.FontColor = Brushes.White;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}