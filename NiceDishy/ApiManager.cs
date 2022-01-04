﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;

namespace NiceDishy
{
    public class ApiManager : DependencyObject
    {
        public static ApiManager Shared = new ApiManager();

        const string ConnectDishyUrl = "https://nicedishy-marccampbell.cloud.okteto.net/connect_device";
        const string PushDataUrl = "https://nicedishy-api-marccampbell.cloud.okteto.net/api/v1/stats";

        const string UriScheme = "nicedishy";
        const string UriFriendlyName = "NiceDishy Protocol";

        #region Token
        public static readonly DependencyProperty TokenProperty =
           DependencyProperty.Register("Token", typeof(string), typeof(ApiManager), new
              PropertyMetadata("", new PropertyChangedCallback(OnTokenChanged)));
        private static void OnTokenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            App app = (App)Application.Current;
            app.UpdateNotifyIcon();
        }
        public string Token
        {
            get { return (string)GetValue(TokenProperty); }
            set { SetValue(TokenProperty, value); }
        }
        #endregion
        public void ConnectDishy()
        {
            System.Diagnostics.Process.Start(ConnectDishyUrl);
        }
        public void DisconnectDishy()
        {
            Token = "";
        }

        #region Uri
        public void RegisterUriScheme()
        {
            string subkey = "SOFTWARE\\Classes\\" + UriScheme;

            try
            {
                // Delete existing Key
                Registry.CurrentUser.DeleteSubKeyTree(subkey, false);
            }
            catch
            {

            }

            using (var key = Registry.CurrentUser.CreateSubKey(subkey))
            {
                // Replace typeof(App) by the class that contains the Main method or any class located in the project that produces the exe.
                // or replace typeof(App).Assembly.Location by anything that gives the full path to the exe
                string applicationLocation = typeof(App).Assembly.Location;

                key.SetValue("", "URL:" + UriFriendlyName);
                key.SetValue("URL Protocol", "");

                using (var defaultIcon = key.CreateSubKey("DefaultIcon"))
                {
                    defaultIcon.SetValue("", applicationLocation + ",1");
                }

                using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                }
            }
        }
        public void HandleUri(Uri uri)
        {
            if (string.Equals(uri.Scheme, UriScheme, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(uri.Host, "connected", StringComparison.OrdinalIgnoreCase))
            {
                // TODO do something with the uri
                var query = uri.Query.Replace("?", "");
                var queryValues = query.Split('&').Select(q => q.Split('=')).ToDictionary(k => k[0], v => v[1]);
                Token = queryValues["token"];
            }
        }
        #endregion

        #region
        public async void Push(object payload)
        {
            if (string.IsNullOrEmpty(Token))
                return;

            try
            {
                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                var client = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(PushDataUrl);
                request.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", string.Format("Token ${0}", Token));

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
                    Console.Write(res);
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Console.Write(res);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}
