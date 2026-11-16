using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using OmenCore.Services;
using OmenCore.Localization;
using OmenCore.Utils;

namespace OmenCore.Views
{
    public partial class AboutWindow : Window
    {
        private readonly AutoUpdateService _updateService;
        private bool _isCheckingUpdates;

        public AboutWindow()
        {
            InitializeComponent();
            _updateService = new AutoUpdateService(App.Logging, "https://api.github.com/repos/theantipopau/omencore/releases/latest");
            VersionText.Text = string.Format(Strings.AboutVersion, AppVersionProvider.GetVersionString());
        }

        private async void CheckUpdatesClicked(object sender, RoutedEventArgs e)
        {
            if (_isCheckingUpdates) return;
            
            _isCheckingUpdates = true;
            CheckUpdatesButton.IsEnabled = false;
            SetStatus(Strings.AboutChecking, "TextSecondaryBrush");
            
            try
            {
                var result = await _updateService.CheckForUpdatesAsync();
                
                if (result.UpdateAvailable && result.LatestVersion != null)
                {
                    var latest = result.LatestVersion;
                    SetStatus(string.Format(Strings.AboutUpdateAvailableStatus, latest.VersionString), "SuccessBrush");

                    if (string.IsNullOrWhiteSpace(latest.DownloadUrl))
                    {
                        var openRelease = MessageBox.Show(
                            Strings.AboutOpenRelease,
                            Strings.AboutUpdateAvailable,
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Information);

                        if (openRelease == MessageBoxResult.Yes)
                        {
                            OpenUrl(latest.ChangelogUrl);
                        }

                        return;
                    }

                    var msgResult = MessageBox.Show(
                        string.Format(Strings.AboutInstallPrompt, result.CurrentVersion.VersionString, latest.VersionString),
                        Strings.AboutUpdateAvailable,
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);

                    if (msgResult == MessageBoxResult.Yes)
                    {
                        SetStatus(Strings.AboutDownloading, "TextSecondaryBrush");
                        var installerPath = await _updateService.DownloadUpdateAsync(latest);

                        if (installerPath != null)
                        {
                            SetStatus(Strings.AboutInstalling, "TextSecondaryBrush");
                            var installResult = await _updateService.InstallUpdateAsync(installerPath);
                            if (!installResult.Success)
                            {
                                SetStatus(UpdateDisplayText.Translate(installResult.Message), "AccentBrush");
                            }
                        }
                        else
                        {
                                SetStatus(Strings.AboutDownloadFailedStatus, "AccentBrush");
                                if (!string.IsNullOrWhiteSpace(latest.ChangelogUrl))
                                {
                                    var openGitHub = MessageBox.Show(
                                        Strings.AboutDownloadFailedPrompt,
                                        Strings.AboutDownloadFailed,
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning);
                                    if (openGitHub == MessageBoxResult.Yes)
                                        OpenUrl(latest.ChangelogUrl);
                                }
                        }
                    }
                }
                else
                {
                    SetStatus(UpdateDisplayText.Translate(result.Message), "TextSecondaryBrush");
                }
            }
            catch (Exception ex)
            {
                SetStatus(string.Format(Strings.AboutCheckFailed, ex.Message), "AccentBrush");
            }
            finally
            {
                _isCheckingUpdates = false;
                CheckUpdatesButton.IsEnabled = true;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }

        private void CloseClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void SetStatus(string message, string brushResourceKey)
        {
            UpdateStatusText.Text = message;
            if (FindResource(brushResourceKey) is Brush brush)
            {
                UpdateStatusText.Foreground = brush;
            }
        }

        private void OpenUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
