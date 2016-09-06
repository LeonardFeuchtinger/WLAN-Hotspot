using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Wlan_GUI
{
    public partial class Form1 : Form
    {
        public bool status = false;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Action(object sender, EventArgs e)
        {

            if (checkText())
            {
                if (status == false)
                {
                    start();
                    status = true;
                }
                else if (status == true)
                {
                    stop();
                    status = false;
                }
            }
        }

        bool checkText()
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("SSID must not be empty!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            
                return false;
            }
            if(textBox2.Text.Length < 8)
            {
                MessageBox.Show("The key must be between 8 to 63 ASCII characters!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return false;
            }
            else { return true; }           
        }

        void start()
        {
                label3.Visible = true;
                button1.BackColor = Color.IndianRed;
                button1.Text = "Stop hotspot";
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine("netsh wlan set hostednetwork mode=allow");
                        sw.WriteLine("netsh wlan set hostednetwork ssid=" + textBox1.Text + " key="+ textBox2.Text + " keyUsage=persistent");
                        sw.WriteLine("netsh wlan start hostednetwork");
                    }
                }
        }

        void stop()
        {
                label3.Visible = false;
                button1.BackColor = Color.SeaGreen;
                button1.Text = "Start hotspot";

                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {                  
                        sw.WriteLine("netsh wlan stop hostednetwork");
                    }
                }
        }
    }
}
