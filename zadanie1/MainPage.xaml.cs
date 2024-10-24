using System.Net;
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
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/usd/2024-10-22/?format=json";
            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/eur/2024-10-22/?format=json";
            string json = "";
            string json2 ;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
                json2 = webClient.DownloadString(url2);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);
            Currency ca = JsonSerializer.Deserialize<Currency>(json2);
            
            string s = $"nazwa waluty: {c.currency}\n";
            s += $"kod waluty: {c.code}\n";
            s += $"Data: {c.rates[0]}\n";
            s += $"Cena skupu: {c.rates[0].bid}]n";
            s += $"Cena sprzedarzy: {c.rates[0].ask}\n";
           
            string sa = $"nazwa waluty: {ca.currency}\n";
            sa += $"kod waluty: {ca.code}\n";
            sa += $"Data: {ca.rates[0]}\n";
            sa += $"Cena skupu: {ca.rates[0].bid}\n";
            sa += $"Cena sprzedarzy: {ca.rates[0].ask}\n";

            cLabel.Text = s + sa; 






        }
    }

}
