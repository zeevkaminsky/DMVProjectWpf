
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Xml;

namespace PL_Wpf
{
    /// <summary>
    /// Interaction logic for Distance.xaml
    /// </summary>
    public partial class Distance : Window
    {
        String API_KEY = @"W1ahdi7Gr0ex7rRwwKtx2inAadymOCKD";
        BackgroundWorker backgroundworker;

        BE.Address addressA = new BE.Address();
        BE.Address addressB = new BE.Address();
        public Distance()
        {
            addressA.Town = BE.Cities.Jerusalem;
            addressA.Building = 45;
            addressA.Street = "pisga";

            addressB.Town = BE.Cities.Holon;
            addressB.Building = 2;
            addressB.Street = "rambam";

            InitializeComponent();
            this.address1.DataContext = addressA;
            this.address2.DataContext = addressB;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource addressViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("addressViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // addressViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            backgroundworker = new BackgroundWorker();
            backgroundworker.DoWork += Backgroundworker_DoWork;
            backgroundworker.RunWorkerCompleted += Backgroundworker_RunWorkerCompleted;
            backgroundworker.RunWorkerAsync(new List<BE.Address> { addressA, addressB });

        }

        private void Backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double result = (double)e.Result;
            this.EizeYofi.Text = result.ToString();
        }

        private void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            //string origin = "pisga 45 st. jerusalem"; //or " אחד העם 100 פתח תקווה " etc.
            //string destination = "gilgal 78 st. ramat-gan";//or " ז'בוטינסקי 10 רמת גן " etc. string KEY = @"<PUT_YOUR_KEY_HERE>";

            double result;

            List<BE.Address> addr = e.Argument as List<BE.Address>;
            //MessageBox.Show(addr[0].ToString());

            string origin = addr[0].Street + " " + addr[0].Building + " st. " + addr[0].Town;
            string destination = addr[1].Street + " " + addr[1].Building + " st. " + addr[1].Town;
            string KEY = API_KEY;

            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + KEY +
            @"&from=" + origin +
            @"&to=" + destination +
            @"&outFormat=xml" +
            @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
            @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                // Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);

                result = (distInMiles * 1.609344);

                e.Result = result;

                //display the returned driving time
                // XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                // string fTime = formattedTime[0].ChildNodes[0].InnerText;
                //  Console.WriteLine("Driving Time: " + fTime);
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                // Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
                MessageBox.Show("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error...
            {
                // Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
                MessageBox.Show("We have'nt got an answer, maybe the net is busy...");
            }
        }
    }
}
