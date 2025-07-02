namespace OtpInput
{
    public partial class MainPage : ContentPage
    {
        private string generatedOtp;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                generatedOtp = GenerateRandomOtp();
                await DisplayAlert("OTP Sent", $"Your OTP is: {generatedOtp}", "Copy"); // Use for demonstration
                await Clipboard.Default.SetTextAsync(generatedOtp);

                // Make SfOtpInput visible for OTP entry
                Loginpage.IsVisible = false;
                ValidationPage.IsVisible = true; ;
            }
            else
            {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
            }
        }

        private string GenerateRandomOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999); // Generates a random 6-digit number
            return otp.ToString();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string enteredOtp = OtpInput.Value;
            if (!string.IsNullOrWhiteSpace(enteredOtp) && enteredOtp.Length == 6)
            {
                ValidateOtp(enteredOtp);
            }
            else
            {
                await DisplayAlert("Error", "Invalid OTP. Please ensure it is 6 digits long.", "OK");
            }
        }

        private async void ValidateOtp(string otp)
        {
            if (otp == generatedOtp)
            {
                await DisplayAlert("Success", "OTP verified successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Incorrect OTP. Please try again.", "OK");
            }
        }

    }
}
