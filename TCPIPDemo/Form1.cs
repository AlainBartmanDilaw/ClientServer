using SimpleTCP;
using System;
using System.Text;
using System.Windows.Forms;

namespace TCPIPDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update mesage to txtStatus
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.AppendText(e.MessageString);
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(server.IsStarted)
            {
                txtStatus.AppendText("Server has already have been started..." + Environment.NewLine);
                return;
            }
            //Start server host
            txtStatus.AppendText("Server starting..." + Environment.NewLine);
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
            txtStatus.AppendText("Server started..." + Environment.NewLine);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                txtStatus.AppendText("Server stopping..." + Environment.NewLine);
                server.Stop();
                txtStatus.AppendText("Server stopped..." + Environment.NewLine);

            }
        }

    }
}