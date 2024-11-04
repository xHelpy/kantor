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
            public double ask { get; set; }
        }
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            string usd = "https://api.nbp.pl/api/exchangerates/rates/c/"+ fe.Text + "/" + Date + "/?format=json";
            string usd2 = "https://api.nbp.pl/api/exchangerates/rates/c/"+ se.Text + "/" + Date + "/?format=json";

            string usdj;
            string usdj2;
            using (var webClient = new WebClient())
            {
                usdj = webClient.DownloadString(usd);
                usdj2 = webClient.DownloadString(usd2);
              
            }

            Currency usdc = JsonSerializer.Deserialize<Currency>(usdj);
            Currency usdc2 = JsonSerializer.Deserialize<Currency>(usdj2);
            double number1 = double.Parse(pr.Text);

            double InitialNumber = number1 * usdc.rates[0].ask;
            double convertedNumber = InitialNumber / usdc2.rates[0].ask;
            wy.Text = convertedNumber.ToString();
            //    string s = $"nazwa waluty: {usdc.currency}\n";
            //    s += $"kod waluty: {usdc.code}\n";
            //    s += $"Data: {usdc.rates[0].effectiveDate}\n";
            //   s += $"Cena skupu: {usdc.rates[0].bid}\n";
            //   s += $"Cena sprzedarzy: {usdc.rates[0].ask}\n";

          

           
           










        }
    }

}
