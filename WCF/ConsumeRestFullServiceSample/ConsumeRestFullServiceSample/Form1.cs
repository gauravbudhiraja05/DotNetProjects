using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace ConsumeRestFullServiceSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBind_Click(object sender, EventArgs e)
        {
            WebClient proxy = new WebClient();
            proxy.DownloadStringAsync(new Uri("http://localhost:52321/Service1.svc/Students"));
            proxy.DownloadStringCompleted += proxy_DownloadStringCompleted;
        }

        void proxy_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Stream stream = new MemoryStream(Encoding.Unicode.GetBytes(e.Result));
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(List<Student>));
            List<Student> result = obj.ReadObject(stream) as List<Student>;


            lblStudentIdValue.Text = result[0].ID.ToString();
            lblStudentNameValue.Text = result[0].Name.ToString();

        }
    }
}
