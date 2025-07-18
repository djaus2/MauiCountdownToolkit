using CommunityToolkit.Maui.Views;

namespace MauiCountdownToolkit.Views;

public partial class CountdownPopup : Popup, ICountdownPopup
{
    private int _secondsRemaining;
    private IDispatcherTimer _timer;
    private TaskCompletionSource<bool> _tcs;
    private double MaxWidth = 0;

    public Task<bool> Result => _tcs.Task;

    /// <summary>
    /// Show count down timer popup.
    /// </summary>
    /// <param name="seconds">Period to count down</param>
    /// <param name="initialText">Initial text to display (Default "Starting...")</param>
    public CountdownPopup(int seconds, SolidColorBrush? color = null, string iconSource = "", int iconWidthHeight = 100, string initialText = "Starting...")
    {
        InitializeComponent();
        OuterBorder.Stroke = color ?? SolidColorBrush.Red; // Use a default value if color is null

        /// <summary>
        /// Width expands with label text but does not contract
        /// </summary>
        CountdownLabel.SizeChanged += (s, e) =>
        {
            // Set Popup width to fit max of label, button and icon widths
            // and height to fit sum of label, icon and button heights
            var sizeLabel = CountdownLabel.Measure(double.PositiveInfinity, double.PositiveInfinity);
            var sizeButton = CancelButton.Measure(double.PositiveInfinity, double.PositiveInfinity);
            double newWidth1 = sizeLabel.Width;
            double newWidth2 = sizeButton.Width;
            double newWidth3 = iconWidthHeight;
            double newWidth = 40 + Math.Max(newWidth1, Math.Max(newWidth2, newWidth3));
            double newHeight = sizeLabel.Height + iconWidthHeight + sizeButton.Height + 40;
            if (newWidth > OuterBorder.WidthRequest)
            {
                OuterBorder.WidthRequest = newWidth;
            }
            if (newHeight > OuterBorder.HeightRequest)
            {
                OuterBorder.HeightRequest = newHeight;
            }
        };

        if (!string.IsNullOrEmpty(iconSource))
        {
            PopupIcon.Source = iconSource;
        }
        else
        {
            // Default icon source if none provided
            PopupIcon.Source = "videogreen.svg"; // Replace with your default icon
        }

            PopupIcon.WidthRequest = iconWidthHeight;
        PopupIcon.HeightRequest = iconWidthHeight;

        CountdownLabel.Text = initialText;

        _secondsRemaining = seconds;
        _tcs = new TaskCompletionSource<bool>();

        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += OnTick;
        _timer.Start();
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (_secondsRemaining > 0)
        {
            CountdownLabel.Text = $"{_secondsRemaining--} seconds";
        }
        else
        {
            _timer.Stop();
            _tcs.TrySetResult(true); // Countdown completed
            Close();
        }
    }

    /// <summary>
    /// Cancel the countdown and close the popup, as an external action
    /// </summary>
    public void Cancel()
    {
        _timer.Stop();
        _tcs.TrySetResult(false); // User cancelled
        Close();
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        _timer?.Stop();
        _tcs.TrySetResult(false); // User cancelled
        Close();
    }
}

