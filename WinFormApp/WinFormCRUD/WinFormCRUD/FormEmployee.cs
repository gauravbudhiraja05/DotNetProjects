using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormCRUD
{
    public partial class FormEmployee : Form
    {
        string conString = @"Data Source=DESKTOP-V32N4KG\SQLEXPRESS; Initial Catalog=EmployeeDb; User Id=sa; Password=1234;";
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        string EmployeeId = "";
        public FormEmployee()
        {
            InitializeComponent();
            sqlCon = new SqlConnection(conString);
            sqlCon.Open();

            cmbGender.Items.Insert(0, "Select Gender");
            cmbGender.Items.Insert(1, "Male");
            cmbGender.Items.Insert(2, "Female");

            cmbGender.SelectedIndex = 0;
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            dGVEmployee.AutoGenerateColumns = false; // dgvEmp is DataGridView name  
            dGVEmployee.DataSource = FetchEmpDetails();

        }

        private DataTable FetchEmpDetails()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spEmployee", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchData");
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            return dtData;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeName.Text))
            {
                MessageBox.Show("Enter Employee Name !!!");
                txtEmployeeName.Select();
            }
            else if (string.IsNullOrWhiteSpace(txtCurrentCity.Text))
            {
                MessageBox.Show("Enter Current City !!!");
                txtCurrentCity.Select();
            }
            else if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Enter Department !!!");
                txtDepartment.Select();
            }
            else if (cmbGender.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Gender !!!");
                cmbGender.Select();
            }
            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    sqlCmd = new SqlCommand("spEmployee", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ActionType", "SaveData");
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    sqlCmd.Parameters.AddWithValue("@Name", txtEmployeeName.Text);
                    sqlCmd.Parameters.AddWithValue("@City", txtCurrentCity.Text);
                    sqlCmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                    sqlCmd.Parameters.AddWithValue("@Gender", cmbGender.Text);
                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Record Saved Successfully !!!");
                        ClearAllData();
                    }
                    else
                        MessageBox.Show("Please Try Again !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            }
        }

        private void ClearAllData()
        {
            btnSave.Text = "Save";
            txtEmployeeName.Text = "";
            txtCurrentCity.Text = "";
            txtDepartment.Text = "";
            cmbGender.SelectedIndex = -1;
            EmployeeId = "";
            dGVEmployee.AutoGenerateColumns = false;
            dGVEmployee.DataSource = FetchEmpDetails();
        }

        private void DGVEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSave.Text = "Update";
                EmployeeId = dGVEmployee.Rows[e.RowIndex].Cells[0].Value.ToString();
                DataTable dtData = FetchEmpRecords(EmployeeId);
                if (dtData.Rows.Count > 0)
                {
                    EmployeeId = dtData.Rows[0][0].ToString();
                    txtEmployeeName.Text = dtData.Rows[0][1].ToString();
                    txtCurrentCity.Text = dtData.Rows[0][2].ToString();
                    txtDepartment.Text = dtData.Rows[0][3].ToString();
                    cmbGender.Text = dtData.Rows[0][4].ToString();
                }
                else
                {
                    ClearAllData(); // For clear all control and refresh DataGridView data.  
                }
            }
        }

        private DataTable FetchEmpRecords(string empId)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spEmployee", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchRecord");
            sqlCmd.Parameters.AddWithValue("@EmployeeId", empId);
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            return dtData;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmployeeId))
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }
                    DataTable dtData = new DataTable();
                    sqlCmd = new SqlCommand("spEmployee", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully !!!");
                        ClearAllData();
                    }
                    else
                    {
                        MessageBox.Show("Please Try Again !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Record !!!");
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }
    }
}
