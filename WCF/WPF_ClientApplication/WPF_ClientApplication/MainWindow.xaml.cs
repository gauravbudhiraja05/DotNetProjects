using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;


namespace WPF_ClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_AddnewEmployee(object sender, RoutedEventArgs e)
        {
            EmployeeDataContract employee = new EmployeeDataContract
            {
                EmployeeID = txtShowEmpId.Text,
                Name = txtShowEmpName.Text,
                JoiningDate = txtShowEmpJoiningDate.Text,
                CompanyName = txtShowCompany.Text,
                Address = txtShowAddress.Text
            };

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmployeeDataContract));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, employee);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:23610/EmployeeService.svc/AddNewEmployee", "POST", data);
            MessageBox.Show("Employee Saved Successfully...");

            txtShowEmpId.Text = "";
            txtShowEmpName.Text = "";
            txtShowEmpJoiningDate.Text = "";
            txtShowCompany.Text = "";
            txtShowAddress.Text = "";
        }

        private void btn_ViewEmployeeDetail(object sender, RoutedEventArgs e)
        {
            WebClient proxy = new WebClient();
            string serviceURL = string.Format("http://localhost:62131/EmployeeService.svc/GetEmployeeDetails/{0}", txtEmpId.Text);
            byte[] data = proxy.DownloadData(serviceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(EmployeeDataContract));
            EmployeeDataContract employee = obj.ReadObject(stream) as EmployeeDataContract;
            txtShowEmpId.Text = employee.EmployeeID;
            txtShowEmpName.Text = employee.Name;
            txtShowEmpJoiningDate.Text = employee.JoiningDate;
            txtShowCompany.Text = employee.CompanyName;
            txtShowAddress.Text = employee.Address;
        }

        private void btn_UpdateEmployee(object sender, RoutedEventArgs e)
        {
            EmployeeDataContract employeeContact = new EmployeeDataContract
            {
                EmployeeID = txtShowEmpId.Text,
                Name = txtShowEmpName.Text,
                JoiningDate = txtShowEmpJoiningDate.Text,
                CompanyName = txtShowCompany.Text,
                Address = txtShowAddress.Text
            };

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmployeeDataContract));
            ser.WriteObject(ms, employeeContact);

            // invoke the REST method    
            client.UploadData("http://localhost:23610/EmployeeService.svc/UpdateEmployee", "PUT", ms.ToArray());

            MessageBox.Show("Employee updated Successfully...");

            txtShowEmpId.Text = "";
            txtShowEmpName.Text = "";
            txtShowEmpJoiningDate.Text = "";
            txtShowCompany.Text = "";
            txtShowAddress.Text = "";

        }

        private void btn_DeleteEmployee(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
            ser.WriteObject(ms, txtEmpId.Text);

            // invoke the REST method    
            byte[] data = client.UploadData("http://localhost:23610/EmployeeService.svc/DeleteEmployee", "DELETE", ms.ToArray());

            MessageBox.Show("Employee deleted Successfully...");

            txtShowEmpId.Text = "";
            txtShowEmpName.Text = "";
            txtShowEmpJoiningDate.Text = "";
            txtShowCompany.Text = "";
            txtShowAddress.Text = "";
        }
    }
}
