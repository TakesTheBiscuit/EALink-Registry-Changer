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

        public Form1()
        {
            InitializeComponent();
            this.registryValue = @Properties.Settings.Default.registryValue;
            this.hideTheApp = @Properties.Settings.Default.hideTheApp;
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


        static bool isRunning(int id)
        {

            try
            {
                Process[] localByName = Process.GetProcessesByName("origin");
                Console.WriteLine(localByName.Length);

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
            Console.WriteLine(hideTheApp);
            if (hideTheApp == "True")
            {
                this.Hide();
            }

            bool running = isRunning(15);
            if (running)
            {
                Console.WriteLine("Running");

                textBox1.AppendText(DateTime.Now.ToLongTimeString() + ": Running");
                textBox1.AppendText("\r\n");

                //                                                                                           HKEY_CLASSES_ROOT\link2ea\shell\open\command
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"link2ea\shell\open\command", true);

                // HKEY_CLASSES_ROOT\link2ea\shell\open\command
                key.SetValue("", this.registryValue);

                //and finally, you close the key
                key.Close();
            }
            else
            {
                Console.WriteLine("Not Running");
            }
        }
    }
}
