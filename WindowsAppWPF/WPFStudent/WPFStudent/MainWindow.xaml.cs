using System;
using System.Diagnostics;
using System.Windows;

namespace WPFStudent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentRepository studentRepository;

        private int Id
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            studentRepository = new StudentRepository();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Id = -1;
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            DataGridStudents.ItemsSource = studentRepository.GetAll();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var student = ((FrameworkElement)sender).DataContext as StudentsData;
            if (student != null)
            {
                TextBoxName.Text = student.Name;
                TextBoxAge.Text = student.Age.ToString();
                TextBoxAddress.Text = student.Address;
                TextBoxContact.Text = student.Contact;
                this.Id = student.ID;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void ResetControls()
        {
            this.Id = -1;
            TextBoxName.Text = string.Empty;
            TextBoxAge.Text = string.Empty;
            TextBoxAddress.Text = string.Empty;
            TextBoxContact.Text = string.Empty;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxAddress.Text) && !string.IsNullOrEmpty(TextBoxAge.Text)
                && !string.IsNullOrEmpty(TextBoxContact.Text) && !string.IsNullOrEmpty(TextBoxName.Text))
            {
                try
                {
                    StudentsData student = new StudentsData()
                    {
                        Address = TextBoxAddress.Text,
                        Age = Convert.ToInt32(TextBoxAge.Text),
                        Contact = TextBoxContact.Text,
                        Name = TextBoxName.Text
                    };

                    if (this.Id <= 0) //save
                    {
                        studentRepository.AddStudent(student);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else //save
                    {
                        student.ID = this.Id;
                        studentRepository.UpdateStudent(student);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured unable to save record!", "", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Debug.Print(ex.Message);
                }
                finally
                {
                    ResetControls();
                    PopulateGrid();
                }
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Student", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                var student = ((FrameworkElement)sender).DataContext as StudentsData;
                if (student != null)
                {
                    try
                    {
                        studentRepository.RemoveStudent(student.ID);
                        MessageBox.Show("Record successfully deleted");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error occured. Unable to delete record!", "", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    finally
                    {
                        ResetControls();
                        PopulateGrid();
                    }
                }
            }
        }
    }
}
