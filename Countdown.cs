using MauiCountdownToolkit.Views;
using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace MauiCountdownToolkit;

public enum CountDownMode
{
    None,
    Soft,
    PopupRed,
    PopupRainbow
}


public class Countdown : Popup, ICountdown
{
    private ICountdownPopup? CountdownPopup = null;
    private CancellationTokenSource? _cts = null;

    private ContentPage page;
    private int delay;
    private string iconSource = "";
    private int iconSize = 100;
    private string initialText = "Starting...";
    private CountDownMode mode = CountDownMode.PopupRed;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Page">The Xaml ContentPage</param>
    /// <param name="Mode">Set the countdown mode</param>
    /// <param name="IconSource">Name of SVG file in host or app or one of 4 in package</param>
    /// <param name="IconSize">Width and Height of incon in popup</param>
    /// <param name="InitialText">Initial text display in popup before countdown</param>
    /// <returns></returns>
    public static Countdown? Create(ContentPage? Page, CountDownMode Mode = CountDownMode.PopupRed, string IconSource = "", int IconSize = 100, string InitialText = "Starting...")
    {
        try
        {
            return new Countdown(Page, Mode, IconSource, IconSize, InitialText);
        }
        catch (Exception ex)
        {
            // Optionally log or handle the error
            return null;
        }
    }

    /// Constructor for Countdown class
    public Countdown(ContentPage? _page, CountDownMode _mode = CountDownMode.PopupRed, string _iconSource = "", int _iconSize = 100, string _initialText = "Starting...")
    {
        if (_page == null) // Check if _page is null
        {
            throw new ArgumentNullException(nameof(_page), "ContentPage cannot be null.");
        }
        else
        {
            page = _page; // Assign _page to page after null check
            mode = _mode;
            iconSource = _iconSource;
            iconSize = _iconSize;
            initialText = _initialText;
        }
    }

    /// <summary>
    /// Action the countdown.
    /// </summary>
    /// <param name="Delay">Countdown time in seconds. Must be GT 0</param>
    /// <returns>True iff countdown isn't aborted</returns>
    public async Task<bool> Wait(int Delay)
    {
        if (delay > 0)
        {
            // If doing delay here then signal to VideoKapture to not use soft auto start
            if (mode != CountDownMode.None)
            {
                return false;
            }
            else if (mode != CountDownMode.Soft)
            {
                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                try
                {
                    for (int i = 0; i < delay; i++)
                    {
                        // Optional: update UI or status
                        await Task.Delay(1000, token); // Pass the token here
                    }
                }
                catch (TaskCanceledException)
                {
                    _cts?.Dispose();
                    _cts = null;
                    return false;
                }

                return true;
            }
            // Nb ShowPopupAsync()m can't take an interface type.
            else
            {
                if (mode == CountDownMode.PopupRed)
                {
                    //2Do set color from red
                    ICountdownPopup? cp = new CountdownPopup(delay, null, iconSource, iconSize, initialText);
                    await page.ShowPopupAsync((Popup)cp);
                    CountdownPopup = cp;
                }
                else if (mode == CountDownMode.PopupRainbow)
                {
                    //Default to Rainbow
                    ICountdownPopup cp = new RainbowCountdownPopup(delay, iconSource, iconSize, initialText);
                    await page.ShowPopupAsync((Popup)cp);
                    CountdownPopup = cp;
                }
                if (CountdownPopup == null)
                {
                    return false;
                }
                bool result = await CountdownPopup.Result;
                CountdownPopup = null;
                return result;
            }
        }
        return false;
    }

    /// <summary>
    /// Cancel the countdown if started and not complete.
    /// Cause false return from countdown
    /// </summary>
    public void Cancel()
    {
        // Cancel any countdown popup
        if (CountdownPopup != null)
        {
            CountdownPopup.Cancel();
            CountdownPopup = null;
        }
        _cts?.Cancel(); // This will interrupt the Task.Delay loop
    }

}

