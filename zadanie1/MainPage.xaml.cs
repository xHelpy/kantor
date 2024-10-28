using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace zadanie1
{
    public partial class MainPage : ContentPage
    {

        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates {  get; set; }


        }

        
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }
        }
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            string date = dpData.Date.ToString("yyyy-MM-dd");
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/"+picker.SelectedItem+"/"+date+"/?format=json";
            string json = "";
            
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);
            
            string s = $"nazwa waluty: {c.currency}\n";
            s += $"kod waluty: {c.code}\n";
            s += $"Data: {c.rates[0].effectiveDate}\n";
            s += $"Cena skupu: {c.rates[0].bid}\n";
            s += $"Cena sprzedarzy: {c.rates[0].ask}\n";



            cLabel.Text = s; 






        }
    }

}
