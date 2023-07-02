using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omron.Cnc.Device;
using Omron.Cnc.Logging;
using Omron.Cnc.Messages;
using Omron.Cnc.Parsing;


namespace TestForm
{
    public partial class Form1 : Form
    {

        private readonly IController controller;
        private Hashtable myVar;

        public Form1()
        {
            InitializeComponent();
            var log = new NullLog();
            this.controller = ControllerFactory.CreateController(true, log, new Omron.Cnc.Range(17000,17005));            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.controller.Connect("127.0.0.1");
            if (this.controller.IsConnected)
            {
                textBox2.Text = "Simulatöre bağlantı kuruldu.";
                return;
            }

            if (!this.controller.IsConnected)
            {
                DialogResult result = MessageBox.Show("Baglanti kurulamadi.");
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int test_int = Int32.Parse(textBox5.Text);
            this.controller.Write("test", test_int);
            int test2_int = Int32.Parse(textBox4.Text);
            this.controller.Write("test2", test2_int);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string[] readvar = new string[] { "test", "test2" };
            this.controller.Read(readvar, out myVar);
            textBox5.Text = myVar["test"].ToString();
            textBox4.Text = myVar["test2"].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.controller.Disconnect();
            textBox2.Text = "Bağlantı kesildi.";
        }

        
    }
}
