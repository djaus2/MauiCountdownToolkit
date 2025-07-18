
namespace MauiCountdownToolkit.Views
{
    /// <summary>
    /// Interface for Countdown Popup
    /// Can be (Red)CountdownPopup or RainbowCountdownPopup
    /// </summary>
    public interface ICountdownPopup
    {
        Task<bool> Result { get; }

        void Cancel();
    }
}