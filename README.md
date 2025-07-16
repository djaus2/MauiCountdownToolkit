# MauiCountdownToolkit

## CountdownPopup/RainbowCountdownPopup
> This is a Maui Countdown Popup that can be used to display a countdown timer in your Maui application. 
> It is designed to be simple and easy to use, allowing you to quickly implement countdown functionality in your app.
> Two border options.
> [Cancel] button and can be programmatically cancelled. Displays an icon.

- Components:  
   `MauiCountdownToolkit.Views.CountdownPopup`   Has one solid color border (default red).  
   `MauiCountdownToolkit.Views.RainbowCountdownPopup` Has rainbow border.
- NuGet Package:  
   [djaus2MauiCountdownToolkit](https://www.nuget.org/packages/djaus2MauiCountdownToolkit/)
- Interface:
   ```cs
    public CountdownPopup(int seconds, SolidColorBrush? color = null, string iconSource = "videogreen.svg", int iconWidthHeight = 100, string initialText = "Starting...")

   and 
    public RainbowCountdownPopup(int seconds, string iconSource = "videogreen.svg",int IconWidthHeight=100, string initialText= "Starting...")
   ```
- Parameters:
   - `seconds`: The number of seconds for the countdown.
   - `iconSource`: The source of the icon to display in the popup (default is "videogreen.svg").
   - `color` **(CountdownPopup only)**: _The color of the border (default is `null`, which uses a default red)._
   - `IconWidthHeight`: The width and height of the icon (default is 100).
   - `initialText`: The initial text to display in the popup (default is "Starting...").

   - **Nb:** gunx.svg, the default icon is in the lib
     - You can use your own SVG icon from an app using this lib.*
     - Make sure its property is set to MauiImage.  
![Countdown Popup](https://raw.githubusercontent.com/djaus2/MauiCountdownToolkit/master/Popup1.png)

     ***The RainbowCountdownPopup in action using gunx.svg icon***
     ---
     ```*``` Icons embedded in the library:  
     Can be used without instantiation (inclusion) in the host app:
     - gunx.svg
     - videogreen.svg _(default)_
     - videored.svg
     - veideoblack.svg

     ## Usage Example
     ```csharp
     using MauiCountdownToolkit.Views;
         ...
         ...
         CountdownPopup? CountdownPopup = null;
         ...
         ...
        int delay = //Get from app properties etc.
        if (delay > 0)
        {
            CountdownPopup = new CountdownPopup(delay,null, "dotnet_athletics.jpg", 64,"Starting...");
            await this.ShowPopupAsync(CountdownPopup);
                      
            bool result = await CountdownPopup.Result;
            CountdownPopup = null;
            if (result)
                //Call process to be triggered after countdown
        }
        ...
        ...
    Elsewhere eg if process manual start button is pressed before countdown fimishes.
        if (CountdownPopup != null)
        {
            // If a countdown popup is active, cancel it
            CountdownPopup.Cancel();
        }
     ```