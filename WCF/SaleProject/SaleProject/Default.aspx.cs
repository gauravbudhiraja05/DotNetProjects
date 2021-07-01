using SaleProject.SaleService;
using System;
using System.Web.UI.WebControls;

namespace SaleProject
{
    public partial class Default : System.Web.UI.Page
    {
        SaleServiceClient saleServiceClient;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                saleServiceClient = new SaleServiceClient();
                GridView1.DataSource = saleServiceClient.GetAllCustomer();
                GridView1.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saleServiceClient = new SaleServiceClient();
            Customer customer = new Customer()
            {
                CustomerName = TextBox1.Text,
                Address = TextBox2.Text,
                EmailId = TextBox3.Text
            };

            bool check = saleServiceClient.InsertCustomer(customer);

            GridView1.DataSource = saleServiceClient.GetAllCustomer();
            GridView1.DataBind();
            if (check)
                Label1.Text = "Record Saved Successfully";
            else
                Label1.Text = "There is some issue while saving the record";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userid = Convert.ToInt32(GridView1.DataKeys
            [e.RowIndex].Values["CustomerID"].ToString());
            saleServiceClient = new SaleServiceClient();

            bool check = saleServiceClient.DeleteCustomer(userid);
            if (check)
                Label1.Text = "Record Deleted Successfully";
            else
                Label1.Text = "There is some issue while deleting the record";
            GridView1.DataSource = saleServiceClient.GetAllCustomer();
            GridView1.DataBind();
        }
    }
}