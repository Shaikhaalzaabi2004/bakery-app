
using BakeryMobileApp.Helper;
using BakeryMobileApp.Models;
using System.Diagnostics.CodeAnalysis;

namespace Session6
{
    public partial class MainPage : ContentPage
    {
        ApiHelper api = new ApiHelper();
        private bool _keepRunning;
        public MainPage()
        {
            InitializeComponent();
            Preferences.Set("@Cart", string.Empty);
        }

        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passEntry.Text))
            {
                DisplayAlert("Error", "Please Input All Fields", "Return");
            }
            else 
            {
                LoginRequests loginRequests = new LoginRequests() 
                {
                    Email = emailEntry.Text,
                    Password = passEntry.Text,
                };

                var result = await LoginResult(loginRequests);
                if (result.FirstName != null)
                {
                    Preferences.Set($"@user", result.Id.ToString());
                    await Shell.Current.GoToAsync("//home");
                }
                else 
                {
                    DisplayAlert("Error", "Invalid Credentials", "Return");

                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _keepRunning = true;

            TimeLabel.Text = DateTime.Now.ToString("HH:mm");
            DayLabel.Text = DateTime.Now.ToString("dddd");
            DateLabel.Text = DateTime.Now.ToString("dd/MM");

            Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                DayLabel.Text = DateTime.Now.ToString("dddd");
                DateLabel.Text = DateTime.Now.ToString("dd/MM");
                return _keepRunning;
            });
        }
        protected override void OnDisappearing()
        {
            _keepRunning = false;
            base.OnDisappearing();
        }

        private async Task<Customer> LoginResult(LoginRequests loginRequests) 
        {

            return await api.LoginResult(loginRequests);
        }
    }
}
