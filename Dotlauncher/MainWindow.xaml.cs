using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;



namespace Dotlauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            loadlist();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddURL dlgAdd = new AddURL();

                if (dlgAdd.ShowDialog() == true)
                {
                    if (ValidateURL(dlgAdd.txtURL.Text) == true)
                    {
                        this.lstUrls.Items.Add(dlgAdd.txtURL.Text);
                        savelist();
                    
                    }
                    else
                    {
                        MessageBox.Show("invalidurl");
                    }
                }
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }

        private void cmdLaunch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (string url in this.lstUrls.Items)
                {
                    launchUrl(url);
                }
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }

        private void lstUrls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Delete)
                {
                    Int16 iselected = Convert.ToInt16(this.lstUrls.SelectedIndex);
                    if (iselected > -1)
                    {
                        this.lstUrls.Items.RemoveAt(iselected);
                        savelist();
                    }


                }
            }
            catch (Exception ex)
            {

                logException(ex);
            }
        }

        private void savelist()
        {
            try
            {
                string urls = "";
                foreach (string lbiurls in this.lstUrls.Items)
                {
                    urls += lbiurls + ",";
                }
                urls = urls.Substring(0, urls.Length - 1);
                settingSave("UrlList", urls);
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }

        private void loadlist()
        {
            try
            {
                string urllist = settingGet("UrlList");
                string[] urls = urllist.Split(',');

                foreach (string url in urls)
                {
                    this.lstUrls.Items.Add(url);
                }
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }

        private bool ValidateURL(string URL)
        {
            bool bReturn = false;

            try
            {
                if (URL.ToLower().Substring(0,4) == "http")
                {
                    bReturn = true;
                }
                else if (URL.ToLower().Substring(0, 4) == "www.")
                {
                    bReturn = true;
                }
                               
            }
            catch (Exception ex)
            {
                logException(ex);
            }

            return bReturn;
        }

        private string settingGet(string settingName)
        {
            string sReturn = "";
            try
            {
                sReturn = (string)Properties.Settings.Default[settingName];
            }
            catch (Exception ex)
            {  
                logException (ex);
            }
            return sReturn;
        }

        private void settingSave (string settingname, string value)
        {
            try
            {
                Properties.Settings.Default[settingname] = value;

                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                
                logException(ex);
            }

        }

        private void launchUrl(string url)
        {
            try
            {
                ProcessStartInfo pi = new ProcessStartInfo(url);
                Process.Start(pi);
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }
            
        private void logException(Exception ex)
        {
            try
            {
                Console.WriteLine(ex.Message);
            }
            catch { }
        }

    }
}
