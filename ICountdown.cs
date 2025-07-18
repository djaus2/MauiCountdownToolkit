
namespace MauiCountdownToolkit
{
    public interface ICountdown
    {
        static abstract Countdown? Create(ContentPage? Page, CountDownMode Mode = CountDownMode.PopupRed, string IconSource = "videogreen.svg", int IconSize = 100, string InitialText = "Starting...");
        void Cancel();
        Task<bool> Wait(int Delay);
    }
}