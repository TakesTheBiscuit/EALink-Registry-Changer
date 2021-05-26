using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EALink_Registry_Changer
{
    public partial class Form1 : Form
    {
        public string registryValue;
        public string hideTheApp;
        public int backOffSeconds;
        public int currentBackOff;
        public Form1()
        {
            InitializeComponent();
            this.registryValue = @Properties.Settings.Default.registryValue;
            this.hideTheApp = @Properties.Settings.Default.hideTheApp;
            this.backOffSeconds = Properties.Settings.Default.backOffSeconds / 2; // account for 2 second form timer


            if (this.hideTheApp == "True")
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.ShowInTaskbar = false;
                this.Load += new EventHandler(Form1_Load);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.hideTheApp == "True")
            {
                this.Size = new Size(0, 0);
            }
        }


        static bool isRunning()
        {

            try
            {
                Process[] localByName = Process.GetProcessesByName("origin");

                if (localByName.Length > 0)
                {
                    // got it
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
                return false;
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (hideTheApp == "True")
            {
                this.Hide();
            }

            bool running = isRunning();

            this.currentBackOff += 1;



            if (running)
            {
                Console.WriteLine("Running");


                if (this.backOffSeconds > 2)
                {
                    if (this.currentBackOff >= this.backOffSeconds)
                    {

                        textBox1.AppendText(DateTime.Now.ToLongTimeString() + ": Running - writing registry now");
                        textBox1.AppendText("\r\n");

                        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"link2ea\shell\open\command", true);
                        // HKEY_CLASSES_ROOT\link2ea\shell\open\command
                        key.SetValue("", this.registryValue);
                        key.Close();

                        this.currentBackOff = 0;
                    }
                }

            }
            else
            {
                Console.WriteLine("Not Running");
            }
        }
    }
}
