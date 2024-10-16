using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace TestToolKit2
{
    public partial class MainPage : ContentPage
    {
        public enum MessageType
        {
            Info,
            Exclamation,
            Warning,
            Failure,
        }

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            ShowSnackBar("Pressing snackbar");

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);


        }

        private void OnOpenWndClicked(object sender, EventArgs e)
        {
            TestWindow.OpenNewWindow(new TestWindow());
        }

        public static async Task ShowSnackBar(string aText,
                                     TimeSpan? aDuration = null,    /* Default to 3 seconds */
                                     Action? aAction = null,
                                     string aActionButtonText = "OK",
                                     SnackbarOptions? aSnackbarOptions = null,
                                     MessageType? aMessageType = null,
                                     CancellationToken? aCancellationTokenSource = null)
        {
            var snackbarOptions = aSnackbarOptions ?? new SnackbarOptions
            {
                BackgroundColor = Colors.Olive,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Black,
                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                CharacterSpacing = 0.5
            };

            if (aMessageType != null)
            {
                switch (aMessageType)
                {
                    case MessageType.Info:
                        snackbarOptions.BackgroundColor = Colors.Olive;
                        break;

                    case MessageType.Exclamation:
                        snackbarOptions.BackgroundColor = Colors.Orange;
                        break;

                    case MessageType.Warning:
                        snackbarOptions.BackgroundColor = Colors.Yellow;
                        break;

                    case MessageType.Failure:
                        snackbarOptions.BackgroundColor = Colors.Red;
                        break;
                }
            }

            string text = aText;
            TimeSpan duration = aDuration ?? TimeSpan.FromSeconds(3);

            _ = duration;
            _ = aActionButtonText;
            _ = aAction;
            _ = aCancellationTokenSource;


            //async () => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");

            //BUG:  Disable due to bug in community addition  when going from 7.0.1 to 8.0.1
            //      Error on closing two windows (TradingRun)
            //           ABI.Microsoft.Windows.AppNotifications.IAppNotificationManagerMethods.Unregister(IObjectReference _obj)

            var snackbar = Snackbar.Make(text, aAction, aActionButtonText, duration, snackbarOptions);

            if (aCancellationTokenSource == null)
                await snackbar.Show();
            else
            {
                var token = aCancellationTokenSource.Value;

                var ctr = aCancellationTokenSource?.Register(() =>
                {
                    snackbar.Dismiss();
                });

                await snackbar.Show(token);
            }
        }


    }

}
