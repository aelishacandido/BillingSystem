using Microsoft.Maui.Controls;
using System.Net.Http.Json;

namespace BillingSystemMobile
{
    // Matches the JSON returned by GET /api/customer 
    public class CustomerItem
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public partial class MainPage : ContentPage
    {
        // Change this URL to match your API port 
        private const string ApiBase = "http://localhost:5000";

        private readonly HttpClient _http = new HttpClient();
        private List<CustomerItem> _customers = new();

        public MainPage()
        {
            InitializeComponent();
            // Load customers when the page is created 
            Loaded += async (s, e) => await LoadCustomersAsync();
        }

        // ── Load customers from GET /api/customer ──────────────────── 
        private async Task LoadCustomersAsync()
        {
            try
            {
                _customers = await _http.GetFromJsonAsync<List<CustomerItem>>($"{ApiBase}/api/customer") ?? new();

                pickerCustomer.ItemsSource = null;
                pickerCustomer.ItemsSource = _customers
                    .Select(c => c.FullName).ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Connection Error",
                    $"Could not load customers:\n{ex.Message}",
                    "OK");
            }
        }

        // ── Recompute Total Amount on any reading/rate change ───────── 
        private void OnReadingChanged(object sender, TextChangedEventArgs e)
        {
            ComputeTotal();
        }

        private void ComputeTotal()
        {
            if (!int.TryParse(entryPreviousReading.Text, out int previous))
            {
                entryTotalAmount.Text = string.Empty; return;
            }
            if (!int.TryParse(entryPresentReading.Text, out int present))
            {
                entryTotalAmount.Text = string.Empty; return;
            }
            if (!decimal.TryParse(entryRate.Text, out decimal rate))
            {
                entryTotalAmount.Text = string.Empty; return;
            }

            int consumption = present - previous;
            if (consumption < 0)
            {
                entryTotalAmount.Text = "Invalid readings";
                return;
            }

            entryTotalAmount.Text = (consumption * rate).ToString("N2");
        }

        // ── Save Bill → POST /api/billing ───────────────────────────── 
        private async void OnSaveBillClicked(object sender, EventArgs e)
        {
            // Validate: customer must be selected 
            if (pickerCustomer.SelectedIndex < 0)
            {
                await DisplayAlertAsync("Validation", "Please select a customer.",
"OK");
                return;
            }

            // Validate: billing month must not be empty 
            if (string.IsNullOrWhiteSpace(entryBillingMonth.Text))
            {
                await DisplayAlertAsync("Validation", "Please enter the billing month.", "OK"); 
                return;
            }

            // Validate: readings and rate must be valid numbers 
            if (!int.TryParse(entryPreviousReading.Text, out int previous) ||
                !int.TryParse(entryPresentReading.Text, out int present) ||
                !decimal.TryParse(entryRate.Text, out decimal rate))
            {
                await DisplayAlertAsync("Validation", "Please enter valid numeric values for readings and rate.", "OK"); 
                          return;
            }

            int consumption = present - previous;
            if (consumption < 0)
            {
                await DisplayAlertAsync("Validation", "Present Reading must be greater than Previous Reading.", "OK"); 
                return;
            }

            // Get the CustomerID of the selected customer 
            var selectedCustomer = _customers[pickerCustomer.SelectedIndex];

            // Build the billing record to send 
            var record = new
            {
                customerID = selectedCustomer.CustomerID,
                billingMonth = entryBillingMonth.Text.Trim(),
                previousReading = previous,
                presentReading = present,
                consumption = consumption,
                ratePerCubic = rate,
                totalAmount = consumption * rate,
                status = "Unpaid"
            };

            try
            {
                var response = await
                    _http.PostAsJsonAsync($"{ApiBase}/api/billing", record);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlertAsync("Success", "Billing record saved successfully!", "OK"); 
                    ClearForm();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    await DisplayAlertAsync("Error", $"Save failed:\n{error}",
"OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Connection Error",
                    $"Could not reach the API:\n{ex.Message}",
                    "OK");
            }
        }

        // ── Clear all form fields after a successful save ───────────── 
        private void ClearForm()
        {
            pickerCustomer.SelectedIndex = -1;
            entryBillingMonth.Text = string.Empty;
            entryPreviousReading.Text = string.Empty;
            entryPresentReading.Text = string.Empty;
            entryRate.Text = "18.50";
            entryTotalAmount.Text = string.Empty;
        }
    }
}