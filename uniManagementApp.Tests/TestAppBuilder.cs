using Avalonia;
using Avalonia.Headless;
using Avalonia.Controls.ApplicationLifetimes;
using uniManagementApp;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => 
        AppBuilder.Configure<App>()
                  .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
