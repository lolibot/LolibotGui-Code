using Ini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoliBot.Utils;

namespace VoliBot
{
    public partial class WelcomeWindow : Form
    {
        public VoliBot _parent;
        string specificFolder;
        bool _forConfig ;
        public WelcomeWindow(VoliBot parent, bool isOnlyForConfig = false)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            specificFolder = Path.Combine(folder, "VoliBot");
            _parent = parent;
            _forConfig = isOnlyForConfig;
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                if (!_forConfig)
                {
                    parms.ClassStyle |= 0x200;
                }
                return parms;
            }
        }

        private void WelcomeWindow_Load(object sender, EventArgs e)
        {

            
            // Regions
            

            if (_forConfig)
            {
                button6.Text = "Close";
                tabControl1.SelectTab(1);
                label4.Text = "Global Settings";
                button6.Click += button6_alternate;
                textBox1.Text = Config.defaultPath;
                Text = "Global / Default Configuration";
                richTextBox2.Text = "Need to change settings, mh? Configurate here the default settings for new bots.";
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                specificFolder = Path.Combine(folder, "LoliBot");
                string path = specificFolder;

                string test = System.IO.Path.Combine(path, "version.ini");

                IniFile ini = new IniFile(path + "\\version.ini");
                string ver = "";
                ver = ini.IniReadValue("General", "version");
                if (ver == "")
                {
                    ver = Config.clientSeason + "." + Config.clientSubVersion;
                    ini.IniWriteValue("General", "version", Config.clientSeason + "." + Config.clientSubVersion);
                }
                this.textBox1.Text = ver; 
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/lolibotofficial/");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(specificFolder + "\\config.ini"))
            {
                MessageBox.Show(specificFolder + "\\config.ini");
                tabControl1.SelectTab(1);
            }
            else
            {
                _parent.accpetedAgreement();
                this.Close();
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wow! Thanks!\nBut please Note: If you only want to donate less than 0,50€ please keep it, I'll get 0,00€ because of the PayPal fees, still thank you!");
            Process.Start("http://qlaffont.com/lolibot.html");
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button6_alternate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            specificFolder = Path.Combine(folder, "LoliBot");
            string path = specificFolder;

            string test = System.IO.Path.Combine(path, "version.ini");

            IniFile ini = new IniFile(path + "\\version.ini");
            if (!System.IO.File.Exists(test))
            {
                ini.IniWriteValue("General", "version", this.textBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "lol.launcher.exe|lol.launcher.exe";
            String strfilename = "";
            if (opd.ShowDialog() == DialogResult.OK) // Test result.
            {
                strfilename = opd.FileName;
            }
            else
            {
                return;
            }
            textBox1.Text = Path.GetDirectoryName(strfilename) + "\\"; // <-- For debugging use.
        }
    }
}
