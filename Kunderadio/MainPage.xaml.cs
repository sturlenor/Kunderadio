using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kunderadio
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();

        String day;
        int hh;
        int mm;
        int ss;
        int opens;
        int close;
        Boolean regularDay;
        Boolean nattOpen;

        public MainPage()
        {
            this.InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            opens = getOpenTime(DateTime.Now);
            close = getCloseTime(DateTime.Now);
            nattOpen = false;
            regularDay = true;
            nattopen.Content = nattText();
            open.Content = openText();

        }

        private async void Timer_Tick(object sender, object e)
        {
            DateTime dt = DateTime.Now;

            day = getWeekday(dt);
            hh = dt.Hour;
            mm = dt.Minute;
            ss = dt.Second;

            kalender.Text = day + " " + dt.Day + "." + dt.Month + "." + dt.Year + " " + hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
            if (hh == 0 && mm == 0 && ss == 0)
            {
                regularDay = true;
                nattOpen = false;
                nattopen.Content = nattText();
                open.Content = openText();
                opens = getOpenTime(dt);
                close = getCloseTime(dt);
            }

            if (hh == close-1 && mm == 58 && ss == 0 && regularDay)
            {
                if (nattOpen)
                {
                    webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/stenging_natt.mp3"));

                }

                else if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek <= DayOfWeek.Sunday)
                {
                    webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/stenging_helg.mp3"));

                }
                else
                {
                    webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/stenging_hverdag.mp3"));
                }
                await Task.Delay(30000);

                webView.GoBack();

            }

            if (hh == opens-1 && mm == 45 && ss == 0 && regularDay)
            {
               if (dt.DayOfWeek <= DayOfWeek.Sunday)
                {
                    webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/apning_sondag.mp3"));

                }
                else
                {
                    webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/apning_hverdag.mp3"));
                }
                await Task.Delay(20000);

                webView.GoBack();


            }

            if (hh == 19 && mm == 58 && ss == 0 && regularDay && nattOpen)
            {
                
                webView.Navigate(new Uri("http://gneist2dagers.no/wp-content/uploads/2017/12/utvidet_natt.mp3"));


                await Task.Delay(15000);
                webView.GoBack();

            }
        }

        private string getWeekday(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Monday)
                return "Mandag";
            if (dt.DayOfWeek == DayOfWeek.Tuesday)
                return "Tirsdag";
            if (dt.DayOfWeek == DayOfWeek.Wednesday)
                return "Onsdag";
            if (dt.DayOfWeek == DayOfWeek.Thursday)
                return "Torsdag";
            if (dt.DayOfWeek == DayOfWeek.Friday)
                return "Fredag";
            if (dt.DayOfWeek == DayOfWeek.Saturday)
                return "Lørdag";
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                return "Søndag";
            return "";
        }

        private void Spotify_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("https://open.spotify.com/"));

        }

        private void P4_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("http://www.p4.no/player"));

        }
        private void P5_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("http://bergen.p5.no/player/"));

        }
        private void P7_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("https://www.p7klem.no/player/"));

        }

        private int getOpenTime(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                return 12;
            return 10;
        }
        private int getCloseTime(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                return 18;
            return 20;
        }

        private void Nrk_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("https://radio.nrk.no/"));
        }

        private void Radionorge_Click(object sender, RoutedEventArgs e)
        {
            webView.Navigate(new Uri("https://radioplay.no/radio-norge/spiller/?clientid=77986970.1512859760"));

        }

        private void Uvanlig_Click(object sender, RoutedEventArgs e)
        {
            if (regularDay)
                regularDay = false;
            else
                regularDay = true;
            open.Content = openText();
        }
        private void Natt_Click(object sender, RoutedEventArgs e)
        {
            if (!nattOpen)
            {
                close = 22;
                nattOpen = true;
            }
            else
            {
                nattOpen = false;
                close = getCloseTime(DateTime.Now);
            }
            open.Content = openText();
            nattopen.Content = nattText();
        }

        private String openText()
        {
            if (regularDay)
                return "Åpent i dag: " + opens + " - " + close;
            else
                return "Ingen opprop i dag";


        }

        private String nattText()
        {
            if (!nattOpen)
                return "Ikke nattåpent";
            else
                return "I dag er det nattåpent.";
        }
    }
}
