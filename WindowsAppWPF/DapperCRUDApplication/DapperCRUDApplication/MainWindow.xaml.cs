using Dapper;
using DapperCRUDApplication.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace DapperCRUDApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string sqlConnectionString = @"Data Source = YourDatabaseServerAddress;initial catalog=YourDatabaseName;user id=YourDatabaseLoginId;password=YourDatabaseLoginPassword";

        public MainWindow()
        {
            InitializeComponent();
        }

        //This method gets all record from student table    
        private List<Student> GetAllStudent()
        {
            List<Student> students = new List<Student>();
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                students = connection.Query<Student>("Select Id, Name, Marks from Student").ToList();
                connection.Close();
            }
            return students;
        }

        //This method inserts a student record in database    
        private int InsertStudent(Student student)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute("Insert into Student (Name, Marks) values (@Name, @Marks)", new { Name = student.Name, Marks = student.Marks });
                connection.Close();
                return affectedRows;
            }
        }

        //This method update student record in database    
        private int UpdateStudent(Student student)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute("Update Student set Name = @Name, Marks = @Marks Where Id = @Id", new { Id = student.Id, Name = txtName.Text, Marks = txtMarks.Text });
                connection.Close();
                return affectedRows;
            }
        }

        //This method deletes a student record from database    
        private int DeleteStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute("Delete from Student Where Id = @Id", new { Id = student.Id });
                connection.Close();
                return affectedRows;
            }
        }
    }
}
