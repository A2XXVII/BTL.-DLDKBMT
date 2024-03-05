using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Xml;

namespace Contact_Uart
{
    public partial class Form1 : Form
    {
        SerialPort P = new SerialPort();
        string InputData = "";
        delegate void SetTextCallback(string text);  
        public Form1()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports) { cbCom.Items.Add(port); }
            //P.ReadTimeout = 1000;
            P.DataReceived += new SerialDataReceivedEventHandler(DataReceived); // Kết nối dataPort vào phần mềm

            // cai dat BauRate
            string[] BaudRate = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            foreach(string baudRate in BaudRate) { cbRate.Items.Add(baudRate); }
           
            // cai dat DataBits
            string[] Databits = { "6", "7", "8" };
            foreach(string databits in Databits) { cbBits.Items.Add(databits); }
           
            // cai dat Parity
            string[] Parity = { "None", "Odd", " Even" };
            foreach(string parity in Parity) {  cbParity.Items.Add(parity); }
           
            // Cai dat cho stopbit
            string[] Stopbit = { "1", "1.5", "2" };
            foreach(string stopbit in Stopbit) {  cbBit.Items.Add(stopbit); }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // chọn giá trị mặc định 
            cbRate.SelectedIndex = 3;
            cbBits.SelectedIndex = 2;
            cbParity.SelectedIndex = 0;
            cbBit.SelectedIndex = 0;
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn chắc muốn thoát không?",
                "Contact Uart",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = P.ReadExisting();
            if (InputData != String.Empty)
            {
                SetText (InputData); // them vao du lieu tu input
            }
        }
        private void SetText (string text)
        {
            if (this.txtkq.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else this.txtkq.Text += text;
        }

        private void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(P.IsOpen)
            {
                P.Close();
            }
            P.PortName = cbCom.SelectedItem.ToString();
        }

        private void cbRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.BaudRate = Convert.ToInt32(cbRate.Text);
        }

        private void cbBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.DataBits = Convert.ToInt32(cbBits.Text);
        }

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            switch (cbParity.SelectedItem.ToString())
            {
                case "odd":
                    P.Parity = Parity.Odd;
                    break;
                case "None":
                    P.Parity = Parity.None;
                    break;
                case "Even":
                    P.Parity = Parity.Even;
                    break;
            }
        }

        private void cbBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            switch (cbBit.SelectedItem.ToString())
            {
                case "1":
                    P.StopBits = StopBits.One;
                    break;
                case "1.5":
                    P.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    P.StopBits = StopBits.Two;
                    break;
            }
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                if (txtSend.Text == "") MessageBox.Show("chưa có dữ liệu!", "Thông Báo");
                else P.Write(txtkq.Text);
            }
            else MessageBox.Show("COM chưa mở.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtSend.Clear();
        }

        private void btKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                P.Open();
                btNgat.Enabled = true;
                btKetNoi.Enabled = false;
                statusStrip1.Text = "Đang kết nối với cổng " + cbCom.SelectedItem.ToString();
            }
            catch (Exception ex )
            {
                MessageBox.Show("Không kết nối được.", "Thử lại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btNgat_Click(object sender, EventArgs e)
        {
            P.Close();
            btKetNoi.Enabled = true;
            btNgat.Enabled = false;
            statusStrip1.Text = "Đã Ngắt Kết Nối ";
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            txtkq.Text = "";
            txtSend.Text = "";
        }

        private void status(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
