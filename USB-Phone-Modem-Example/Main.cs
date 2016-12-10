using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        SerialPort phonePort = new SerialPort();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var serialPortList = SerialPort.GetPortNames();
            foreach(var serialPort in serialPortList)
            {
                serialDevices.Items.Add(serialPort);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialDevices.Items.Clear();
            var serialPortList = SerialPort.GetPortNames();
            foreach (var serialPort in serialPortList)
            {
                serialDevices.Items.Add(serialPort);
            }
        }

        private void serialDevices_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var device = serialDevices.SelectedItem;
            if (device != null)
            {
                try
                {
                    phonePort.PortName = device.ToString();
                    phonePort.BaudRate = 9600;
                    phonePort.DataBits = 8;
                    phonePort.StopBits = StopBits.One;
                    phonePort.Parity = Parity.None;
                    phonePort.ReadTimeout = 1000;
                    phonePort.WriteTimeout = 1000;
                    phonePort.Encoding = Encoding.GetEncoding("iso-8859-1");
                    phonePort.DataReceived += new SerialDataReceivedEventHandler(serialReplyRecieved);
                    phonePort.Open();
                    phonePort.DtrEnable = true;
                    phonePort.RtsEnable = true;
                    MessageBox.Show("Connected");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error opening port: " + ex.Message);
                }
            }
            else
                MessageBox.Show("Select a device");
        }

        private void serialReplyRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            MessageBox.Show(phonePort.ReadLine());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (phonePort.IsOpen)
                phonePort.WriteLine("AT+CREG=2");
            else
                MessageBox.Show("Select a device first");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
