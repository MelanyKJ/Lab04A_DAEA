using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Lab04A_DAEA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Data Source=MACK26;Initial Catalog=Registro;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("BuscarPersonaNombre", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            parameter1.Value = "";
            parameter1.ParameterName = "@LastName";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter2.Value = txtNombre.Text;
            parameter2.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader["Id"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),
                    FullName = dataReader["FullName"].ToString(),
                    Age = (int)dataReader["Age"]
                });
            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}

