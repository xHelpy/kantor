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



        /*
         nazwa funkcji: <OnCounterClicked>
         parametry wejściowe: <sender, EventArgs sender -> obiekt wysyłający, EventArgs -> argumenty wysyłane z przycisku>
         wartość zwracana: <brak>
         informacje: <Funkcja pobiera dane odnośnie walut z linku w formie JSON, Pobiera 2 daty z dpData, dpData2, sprawdzająć czy nie są zaznaczone dni weekendu,
                     pobiera 6 informacji potrzebne do porównania 3 walut 1 daty i 2 daty, wyświetla ona dane takie jak: kod, cene sprzedaży,
                     cene skupu, porównuje cenę skupu 1 daty i 2 daty wyświetlając zdjęcie strzałki do góry, jeżeli cena jest wyższa niż od daty drugiej lub do dołu jeżeli jest niższa
         autor: Wojciech Todys
        */
        private void OnCounterClicked(object sender, EventArgs e)
        {
            string date = dpData.Date.ToString("yyyy-MM-dd");
            string date2 = dpData2.Date.ToString("yyyy-MM-dd");
            if (dpData.Date.DayOfWeek == DayOfWeek.Sunday || dpData.Date.DayOfWeek == DayOfWeek.Saturday || dpData2.Date.DayOfWeek == DayOfWeek.Saturday || dpData2.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                return; 
            }
            string usd = "https://api.nbp.pl/api/exchangerates/rates/c/usd/" + date + "/?format=json";
            string usd2 = "https://api.nbp.pl/api/exchangerates/rates/c/usd/" + date2 + "/?format=json";
            string eur = "https://api.nbp.pl/api/exchangerates/rates/c/eur/" + date + "/?format=json";
            string eur2 = "https://api.nbp.pl/api/exchangerates/rates/c/eur/" + date2 + "/?format=json";
            string gpd = "https://api.nbp.pl/api/exchangerates/rates/c/gbp/" + date + "/?format=json";
            string gpd2 = "https://api.nbp.pl/api/exchangerates/rates/c/gbp/" + date2 + "/?format=json";
            string usdj;
            string usdj2;
            string eurj;
            string eurj2;
            string gpdj;
            string gpdj2;

            using (var webClient = new WebClient())
            {
                usdj = webClient.DownloadString(usd);
                usdj2 = webClient.DownloadString(usd2);
                eurj = webClient.DownloadString(eur);
                eurj2 = webClient.DownloadString(eur2);
                gpdj = webClient.DownloadString(gpd);
                gpdj2 = webClient.DownloadString(gpd2);
            }

            Currency usdc = JsonSerializer.Deserialize<Currency>(usdj);
            Currency usdc2 = JsonSerializer.Deserialize<Currency>(usdj2);
            Currency eurc = JsonSerializer.Deserialize<Currency>(eurj);
            Currency eurc2 = JsonSerializer.Deserialize<Currency>(eurj2);
            Currency gpdc = JsonSerializer.Deserialize<Currency>(gpdj);
            Currency gpdc2 = JsonSerializer.Deserialize<Currency>(gpdj2);

            //    string s = $"nazwa waluty: {usdc.currency}\n";
            //    s += $"kod waluty: {usdc.code}\n";
            //    s += $"Data: {usdc.rates[0].effectiveDate}\n";
            //   s += $"Cena skupu: {usdc.rates[0].bid}\n";
            //   s += $"Cena sprzedarzy: {usdc.rates[0].ask}\n";

            dLabel.Text = usdc.code;
            dPrice.Text = "Skup: " + usdc.rates[0].bid + " | Sprzedaż: " + usdc.rates[0].ask + " | " + usdc2.rates[0].ask;
        
            
                if (usdc.rates[0].ask > usdc2.rates[0].ask)
                {
                    Console.WriteLine(usdc.rates[0].ask);
                    Console.WriteLine(usdc2.rates[0].ask);
                    dState.Source = "up.png";

                }
                else
                {
                    dState.Source = "down.png";
                }
                eLabel.Text = eurc.code;
                ePrice.Text = "Skup: " + eurc.rates[0].bid + " | Sprzedaż: " + eurc.rates[0].ask + " | " + eurc2.rates[0].ask;
                if (eurc.rates[0].ask > eurc2.rates[0].ask)
                {
                    Console.WriteLine(eurc.rates[0].ask);
                    Console.WriteLine(eurc2.rates[0].ask);
                    eState.Source = "up.png";

                }
                else
                {
                    eState.Source = "down.png";
                }
                pLabel.Text = gpdc.code;
                pPrice.Text = "Skup: " + gpdc.rates[0].bid + " | Sprzedaż: " + gpdc.rates[0].ask + " | " + gpdc2.rates[0].ask;
                if (gpdc.rates[0].ask > gpdc2.rates[0].ask)
                {
                    Console.WriteLine(gpdc.rates[0].ask);
                    Console.WriteLine(gpdc2.rates[0].ask);
                    pState.Source = "up.png";

                }
                else
                {
                    pState.Source = "down.png";
                }
           
        }
    }

}
