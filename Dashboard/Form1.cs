using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Dashboard
{
    public partial class Form1 : Form
    {


        private void GetBitcoinData(string startDate, string endDate, bool init)
        {

            WebRequest request;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            if (init)
            {
               request = WebRequest.Create("https://api.coindesk.com/v1/bpi/historical/close.json");
            }
            else
            {
                String url = "https://api.coindesk.com/v1/bpi/historical/close.json?start=" + startDate + "&end=" + endDate;
               request = WebRequest.Create(url);
            }
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var obj = JObject.Parse(responseFromServer);
            reader.Dispose();
            //Clear all points in chart to refresh
            chartPrice.Series["Price"].Points.Clear();

            foreach (JProperty item in obj["bpi"])
            {
                chartPrice.Series["Price"].Points.AddXY((string)item.Name, (decimal)item.Value);
            }
        }
        public Form1()
        {
            InitializeComponent();
            GetBitcoinData("", "", true);
        }

 
        private void Button1_Click(object sender, EventArgs e)
        {


            
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                WebRequest request = WebRequest.Create("https://api.coindesk.com/v1/bpi/historical/close.json");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                var obj = JObject.Parse(responseFromServer);
                reader.Dispose();
                //Clear all points in chart to refresh
                chartPrice.Series["Price"].Points.Clear();

                foreach (JProperty item in obj["bpi"])
                {
                    chartPrice.Series["Price"].Points.AddXY((string)item.Name, (decimal)item.Value);
                }


        
        }

        private void BitcoinRangeBtn_Click(object sender, EventArgs e)
        {
            string start = startDate.Text;
            string end = endDate.Text;
            GetBitcoinData(start, end, false);
        }

       
    }
}
