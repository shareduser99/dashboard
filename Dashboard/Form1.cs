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
using RestSharp;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Dashboard

{

    public partial class CryptoDashboard : Form
    {
        bool isTopPanelDragged = false;
        bool isLeftPanelDragged = false;
        bool isRightPanelDragged = false;
        bool isBottomPanelDragged = false;
        bool isTopBorderPanelDragged = false;
        bool isWindowMaximized = false;
        Point offset;
        Size _normalWindowSize;
        Point _normalWindowLocation = Point.Empty;
        private int ElectrumBalance = 0;
        private int BinanceBalance = 0;
        private BinanceAuthenticator BinanceAPI;
        public string ElectrumRPC(string method)
        {
            var json = "{\"method\":\"" + method + "\",\"id\":\"1\"}";

            var jsonrpc_response_raw = "";

            #region http web request
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(new Uri("http://127.0.0.1" + ":" + "7777"));


            var encoded_authorization = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("user" + ":" + "myrpcpass"));

            http.Headers.Add("Authorization", "Basic " + encoded_authorization);

            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(json);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            try
            {
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                var CurrentHttpStatusCode = response.StatusCode.ToString();
                var CurrentStatusDescription = response.StatusDescription;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    jsonrpc_response_raw = "Error! " + response.StatusCode.ToString() + "; Info: " + response.StatusDescription;
                }
                else
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);

                    jsonrpc_response_raw = sr.ReadToEnd().Replace(@"\n", "\n").Replace(@"\t", "\t").Replace(@"\r", "\r").Replace(@"\""", "\"").Replace(@"""{", "{").Replace(@"}""", "}");

                }
            }
            catch (Exception e)
            {
                jsonrpc_response_raw = "Error: " + e.Message;
            }
            #endregion

            return jsonrpc_response_raw;
        }

        public CryptoDashboard()
        {
            InitializeComponent();
            /* BinanceAPI = new BinanceAuthenticator("DyQjRW8xyaAwwx7uu5EB3KUVXGnY5HPfW8Pkkd6djMvN9YnylHT4AOeKxZTDPrkV", "WCEA0bKYd1tw5BLjYW9o3HGyODWQCYiKlljjVn7RVOqV6vSRGBAZIKxGkc3dAF9Q");
            var client = BinanceAPI.NewAPIClient();
            var request = BinanceAPI.NewGetRequest("/api/v3/account", DataFormat.Json);
            var response = client.Get(request);
            var jsonObject = JObject.Parse(response.Content);
            if (jsonObject.ContainsKey("balances"))
            {
                foreach (var item in jsonObject["balances"])
                {
                    if ((item["asset"].ToString()) == "BTC")
                    {
                        DashboardBinanceBalance.Text = item["free"].ToString();
                    }
                }
            }

            else
            {
                DashboardBinanceBalance.Text = "0 mBTC";
            }
            */

             var startProcess = new CMDProcess("C:\\Users\\Paul\\source\\repos\\Dashboard\\Dashboard\\electrum-daemon", "start_daemon-rpc.bat");
             startProcess.Run();
             DashboardBinanceBalance.Text = (JObject.Parse(ElectrumRPC("getbalance"))["result"]["confirmed"]).ToString() + " mBTC";
}

        private void TopBorderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopBorderPanelDragged = true;
            }
            else
            {
                isTopBorderPanelDragged = false;
            }
        }

        private void TopBorderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < this.Location.Y)
            {
                if (isTopBorderPanelDragged)
                {
                    if (this.Height < 50)
                    {
                        this.Height = 50;
                        isTopBorderPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y + e.Y);
                        this.Height = this.Height - e.Y;
                    }
                }
            }
        }

        private void TopBorderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopBorderPanelDragged = false;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopPanelDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point();
                offset.X = this.Location.X - (pointStartPosition.X + LeftTopPanel.Size.Width);
                offset.Y = this.Location.Y - pointStartPosition.Y;
            }
            else
            {
                isTopPanelDragged = false;
            }
            if (e.Clicks == 2)
            {
                isTopPanelDragged = false;
                MaxButton_Click(sender, e);
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTopPanelDragged)
            {
                Point newPoint = TopPanel.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;

                if (this.Location.X > 2 || this.Location.Y > 2)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.Location = _normalWindowLocation;
                        this.Size = _normalWindowSize;
                        toolTip1.SetToolTip(MaxButton, "Maximize");
                        MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                        isWindowMaximized = false;
                    }
                }
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {
                if (!isWindowMaximized)
                {
                    _normalWindowSize = this.Size;
                    _normalWindowLocation = this.Location;

                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(rect.Width, rect.Height);
                    toolTip1.SetToolTip(MaxButton, "Restore Down");
                    MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                    isWindowMaximized = true;
                }
            }
        }

        private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Location.X <= 0 || e.X < 0)
            {
                isLeftPanelDragged = false;
                this.Location = new Point(10, this.Location.Y);
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    isLeftPanelDragged = true;
                }
                else
                {
                    isLeftPanelDragged = false;
                }
            }
        }

        private void LeftPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < this.Location.X)
            {
                if (isLeftPanelDragged)
                {
                    if (this.Width < 100)
                    {
                        this.Width = 100;
                        isLeftPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X + e.X, this.Location.Y);
                        this.Width = this.Width - e.X;
                    }
                }
            }
        }

        private void LeftPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftPanelDragged = false;
        }

        private void RightPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRightPanelDragged = true;
            }
            else
            {
                isRightPanelDragged = false;
            }
        }

        private void RightPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRightPanelDragged)
            {
                if (this.Width < 100)
                {
                    this.Width = 100;
                    isRightPanelDragged = false;
                }
                else
                {
                    this.Width = this.Width + e.X;
                }
            }
        }

        private void RightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isRightPanelDragged = false;
        }

        private void BottomPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isBottomPanelDragged = true;
            }
            else
            {
                isBottomPanelDragged = false;
            }
        }

        private void BottomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isBottomPanelDragged)
            {
                if (this.Height < 50)
                {
                    this.Height = 50;
                    isBottomPanelDragged = false;
                }
                else
                {
                    this.Height = this.Height + e.Y;
                }
            }
        }

        private void BottomPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isBottomPanelDragged = false;
        }

        private void MinButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaxButton_Click(object sender, EventArgs e)
        {
            if (isWindowMaximized)
            {
                this.Location = _normalWindowLocation;
                this.Size = _normalWindowSize;
                toolTip1.SetToolTip(MaxButton, "Maximize");
                MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                isWindowMaximized = false;
            }
            else
            {
                _normalWindowSize = this.Size;
                _normalWindowLocation = this.Location;

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new System.Drawing.Size(rect.Width, rect.Height);
                toolTip1.SetToolTip(MaxButton, "Restore Down");
                MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                isWindowMaximized = true;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            var endProcess = new CMDProcess("C:\\Users\\Paul\\source\\repos\\Dashboard\\Dashboard\\electrum-daemon", "kill_daemon-rpc.bat");
            endProcess.Run();
            this.Close();
        }

        private void WindowTextLabel_MouseDown(object sender, MouseEventArgs e)
        {
            TopPanel_MouseDown(sender, e);
        }

        private void WindowTextLabel_MouseMove(object sender, MouseEventArgs e)
        {
            TopPanel_MouseMove(sender, e);
        }

        private void WindowTextLabel_MouseUp(object sender, MouseEventArgs e)
        {
            TopPanel_MouseUp(sender, e);
        }

        private void ElectrumDocumentationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://electrum.readthedocs.io/en/latest/");
        }

        private void ElectrumDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://electrum.org/#download");
        }

        private void BinanceLoginLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.binance.us/en/login");
        }
    }



    public class BinanceAuthenticator
    {
        private readonly string APIsecret;
        private readonly string APIkey;
        private readonly string rootUrl = "https://api.binance.us";
        public BinanceAuthenticator(string key, string secret)
        {
            this.APIkey = key;
            this.APIsecret = secret;
        }

        public RestClient NewAPIClient()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var client = new RestClient(this.rootUrl);
            return client;
        }
        public RestRequest NewGetRequest(string requestPath, RestSharp.DataFormat dataformat)
        {

            var request = new RestRequest(requestPath, dataformat);
            var totalParams = "";
            string timestamp = this.Timestamp();
            request.AddHeader("X-MBX-APIKEY", this.APIkey);
            request.AddQueryParameter("timestamp", timestamp);
            totalParams = totalParams + "timestamp=" + timestamp;
            request.AddQueryParameter("signature", this.Signature(totalParams));
            return request;

        }


        public string Timestamp()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long secondsSinceEpoch = (long)(t.TotalSeconds * 1000);
            return secondsSinceEpoch.ToString();
        }

        private string Signature(string totalParams)
        {
            byte[] key = Encoding.ASCII.GetBytes(this.APIsecret);
            byte[] message = Encoding.ASCII.GetBytes(totalParams);
            var hmac = new HMACSHA256(key);
            byte[] hash = hmac.ComputeHash(message);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            hmac.Dispose();
            return builder.ToString();
        }
    }

    public class CMDProcess
    {
        private Process task;
        public CMDProcess(string dir, string file)
        {
            this.task = new Process();
            this.task.StartInfo.WorkingDirectory = dir;
            this.task.StartInfo.FileName = file;
            this.task.StartInfo.CreateNoWindow = false;
            this.task.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

        }

        public void Run()
        {
            this.task.Start();
            this.task.WaitForExit();

        }

        public void ForceKill()
        {
            this.task.Kill();
        }
    }


}

  



        