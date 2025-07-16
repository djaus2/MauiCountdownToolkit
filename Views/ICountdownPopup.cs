
namespace MauiCountdownToolkit.Views
{
    public interface ICountdownPopup
    {
        Task<bool> Result { get; }

        void Cancel();
    }
}