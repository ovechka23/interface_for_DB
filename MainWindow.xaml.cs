using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Npgsql;
using NpgsqlTypes;

namespace Program
{
    public class ProductIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                int productId = System.Convert.ToInt32(value);
                return MainWindow.ProductIdName[productId];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string productName = (string)value;
                return MainWindow.ProductIdName.FirstOrDefault(x => x.Value == productName).Key;
            }
            return null;
        }
    }
    public class OwnerIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string productId = value.ToString();
                return MainWindow.OwnerIdName[productId];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string productName = (string)value;
                return MainWindow.OwnerIdName.FirstOrDefault(x => x.Value == productName).Key;
            }
            return null;
        }
    }
    public class ShopIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                int shopId = System.Convert.ToInt32(value);
                return MainWindow.ShopIdName[shopId];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string shopName = (string)value;
                return MainWindow.ShopIdName.FirstOrDefault(x => x.Value == shopName).Key;
            }
            return null;
        }
    }
    public class SupplierIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                int supplierId = System.Convert.ToInt32(value);
                return MainWindow.SupplierIdName[supplierId];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string supplierName = (string)value;
                return MainWindow.SupplierIdName.FirstOrDefault(x => x.Value == supplierName).Key;
            }
            return null;
        }
    }
    public class BuyerIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string ownerId = value.ToString();
                if (MainWindow.OwnerIdName.ContainsKey(ownerId))
                {
                    return MainWindow.OwnerIdName[ownerId];
                }
                return "Нет значения для ID: " + ownerId;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!System.Convert.IsDBNull(value))
            {
                string ownerName = (string)value;
                return MainWindow.OwnerIdName.FirstOrDefault(x => x.Value == ownerName).Key;
            }
            return null;
        }
    }

    public partial class MainWindow : Window
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlCommand _firstCommand;
        private NpgsqlCommand _secondCommand;
        private NpgsqlCommand _thirdCommand;
        private NpgsqlCommand _fourthCommand;
        private NpgsqlCommand _fifthCommand;
        private NpgsqlCommand _sixthCommand;
        private NpgsqlDataAdapter _firstAdapter;
        private NpgsqlDataAdapter _secondAdapter;
        private NpgsqlDataAdapter _thirdAdapter;
        private NpgsqlDataAdapter _fourthAdapter;
        private NpgsqlDataAdapter _fifthAdapter;
        private NpgsqlDataAdapter _sixthAdapter;
        private DataTable _firstDataTable;
        private DataTable _secondDataTable;
        private DataTable _thirdDataTable;
        private DataTable _fourthDataTable;
        private DataTable _fifthDataTable;
        private DataTable _sixthDataTable;
        private readonly string _firstQuery = "select * from \"product\"";
        private readonly string _secondQuery = "select * from \"owner\"";
        private readonly string _thirdQuery = "select * from \"ownership share\"";
        private readonly string _fourthQuery = "select * from \"product in the shop\"";
        private readonly string _fifthQuery = "select * from \"shop\"";
        private readonly string _sixthQuery = "select * from \"supplier\"";
        public static Dictionary<int, string> ProductIdName { get; set; }
        public static Dictionary<string, string> OwnerIdName { get; set; }
        public static Dictionary<int, string> ShopIdName { get; set; }
        public static Dictionary<int, string> SupplierIdName { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            _connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=lab1; User Id = postgres; Password=********");
            _firstCommand = new NpgsqlCommand(_firstQuery, _connection);
            _secondCommand = new NpgsqlCommand(_secondQuery, _connection);
            _thirdCommand = new NpgsqlCommand(_thirdQuery, _connection);
            _fourthCommand = new NpgsqlCommand(_fourthQuery, _connection);
            _fifthCommand = new NpgsqlCommand(_fifthQuery, _connection);
            _sixthCommand = new NpgsqlCommand(_sixthQuery, _connection);
            _firstAdapter = new NpgsqlDataAdapter(_firstCommand);
            _secondAdapter = new NpgsqlDataAdapter(_secondCommand);
            _thirdAdapter = new NpgsqlDataAdapter(_thirdCommand);
            _fourthAdapter = new NpgsqlDataAdapter(_fourthCommand);
            _fifthAdapter = new NpgsqlDataAdapter(_fifthCommand);
            _sixthAdapter = new NpgsqlDataAdapter(_sixthCommand);
            _firstDataTable = new DataTable();
            _secondDataTable = new DataTable();
            _thirdDataTable = new DataTable();
            _fourthDataTable = new DataTable();
            _fifthDataTable = new DataTable();
            _sixthDataTable = new DataTable();
            _firstAdapter.Fill(_firstDataTable);
            _secondAdapter.Fill(_secondDataTable);
            _thirdAdapter.Fill(_thirdDataTable);
            _fourthAdapter.Fill(_fourthDataTable);
            _fifthAdapter.Fill(_fifthDataTable);
            _sixthAdapter.Fill(_sixthDataTable);
            firstDataGrid.ItemsSource = _firstDataTable.DefaultView;
            secondDataGrid.ItemsSource = _secondDataTable.DefaultView;
            thirdDataGrid.ItemsSource = _thirdDataTable.DefaultView;
            fourthDataGrid.ItemsSource = _fourthDataTable.DefaultView;
            fifthDataGrid.ItemsSource = _fifthDataTable.DefaultView;
            sixthDataGrid.ItemsSource = _sixthDataTable.DefaultView;
            ProductIdName = new Dictionary<int, string>();
            ShopIdName = new Dictionary<int, string>();
            SupplierIdName = new Dictionary<int, string>();
            OwnerIdName = new Dictionary<string, string>();
            ProductIdName.Clear();
            foreach (DataRow row in _firstDataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string name = row["Name"].ToString();
                ProductIdName[id] = name;
            }
            ShopIdName.Clear();
            foreach (DataRow row in _fifthDataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string name = (string)row["Name"];
                ShopIdName[id] = name;
            }
            SupplierIdName.Clear();
            foreach (DataRow row in _sixthDataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string name = (string)row["Name"];
                SupplierIdName[id] = name;
            }
            OwnerIdName.Clear();
            foreach (DataRow row in _secondDataTable.Rows)
            {
                string id = row["id"].ToString();
                string surname = (string)row["Surname"];
                string name = (string)row["Name"];
                string patronymic = (string)row["Patronymic"];
                string fio = surname + " " + name + " " + patronymic;
                OwnerIdName.Add(id, fio);
            }
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_firstAdapter);
                _firstAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _firstAdapter.Update(_firstDataTable);
                _firstDataTable.Clear();
                _firstAdapter.Fill(_firstDataTable);
                firstDataGrid.ItemsSource = _firstDataTable.DefaultView;
                ProductIdName.Clear();
                foreach (DataRow row in _firstDataTable.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string name = row["Name"].ToString();
                    ProductIdName[id] = name;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _firstDataTable.NewRow();
            _firstDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = firstDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _firstAdapter.DeleteCommand = FirstCreateDeleteCommand();
                _firstAdapter.Update(_firstDataTable);
            }
            catch
            {
                return;
            }
        }

        private NpgsqlCommand FirstCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.product where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void firstDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = firstDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_secondAdapter);
                _secondAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _secondAdapter.Update(_secondDataTable);
                _secondDataTable.Clear();
                _secondAdapter.Fill(_secondDataTable);
                
                OwnerIdName.Clear();
                foreach (DataRow row in _secondDataTable.Rows)
                {
                    string id = row["id"].ToString();
                    string surname = (string)row["Surname"];
                    string name = (string)row["Name"];
                    string patronymic = (string)row["Patronymic"];
                    string fio = surname + " " + name + " " + patronymic;
                    OwnerIdName.Add(id, fio);
                }
                secondDataGrid.ItemsSource = _secondDataTable.DefaultView;
            }
            catch
            {
                return;
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _secondDataTable.NewRow();
            _secondDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = secondDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _secondAdapter.DeleteCommand = SecondCreateDeleteCommand();
                _secondAdapter.Update(_secondDataTable);
            }
            catch
            {
                return;
            }
        }

        private NpgsqlCommand SecondCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.owner where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void secondDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = secondDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_thirdAdapter);
                _thirdAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _thirdAdapter.Update(_thirdDataTable);
                _thirdDataTable.Clear();
                _thirdAdapter.Fill(_thirdDataTable);
                thirdDataGrid.ItemsSource = _thirdDataTable.DefaultView;
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _thirdDataTable.NewRow();
            _thirdDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = thirdDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _thirdAdapter.DeleteCommand = ThirdCreateDeleteCommand();
                _thirdAdapter.Update(_thirdDataTable);
            }
            catch
            {
                return;
            }
        }
        private NpgsqlCommand ThirdCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.'ownership share' where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void thirdDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = thirdDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_fourthAdapter);
                _fourthAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _fourthAdapter.Update(_firstDataTable);
                _fourthDataTable.Clear();
                _fourthAdapter.Fill(_fourthDataTable);
                fourthDataGrid.ItemsSource = _fourthDataTable.DefaultView;
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _fourthDataTable.NewRow();
            _fourthDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = fourthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _fourthAdapter.DeleteCommand = FourthCreateDeleteCommand();
                _fourthAdapter.Update(_fourthDataTable);
            }
            catch
            {
                return;
            }
        }

        private NpgsqlCommand FourthCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.'product in the shop' where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void fourthDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = fourthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_fifthAdapter);
                _fifthAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _fifthAdapter.Update(_fifthDataTable);
                _firstDataTable.Clear();
                _fifthAdapter.Fill(_fifthDataTable);
                fifthDataGrid.ItemsSource = _fifthDataTable.DefaultView;
                ShopIdName.Clear();
                foreach (DataRow row in _fifthDataTable.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string name = (string)row["Name"];
                    ShopIdName[id] = name;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _fifthDataTable.NewRow();
            _fifthDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = fifthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _fifthAdapter.DeleteCommand = FifthCreateDeleteCommand();
                _fifthAdapter.Update(_fifthDataTable);
            }
            catch
            {
                return;
            }
        }

        private NpgsqlCommand FifthCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.shop where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void fifthDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = fifthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(_sixthAdapter);
                _sixthAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _sixthAdapter.Update(_sixthDataTable);
                _sixthDataTable.Clear();
                _sixthAdapter.Fill(_sixthDataTable);
                sixthDataGrid.ItemsSource = _sixthDataTable.DefaultView;
                SupplierIdName.Clear();
                foreach (DataRow row in _sixthDataTable.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string name = (string)row["Name"];
                    SupplierIdName[id] = name;
                }
            }
            catch
            {
                return;
            }
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            DataRow dataRow = _sixthDataTable.NewRow();
            _sixthDataTable.Rows.Add(dataRow);
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = sixthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
                _sixthAdapter.DeleteCommand = SixthCreateDeleteCommand();
                _sixthAdapter.Update(_sixthDataTable);
            }
            catch
            {
                return;
            }
        }

        private NpgsqlCommand SixthCreateDeleteCommand()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = _connection;
            command.CommandText = "delete from lab1.supplier where id = @id";
            command.Parameters.Add("@id", NpgsqlDbType.Integer);
            command.Parameters["@id"].SourceColumn = "id";
            return command;
        }

        private void sixthDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = sixthDataGrid.SelectedItems.Cast<DataRowView>().ToList();
                foreach (DataRowView dataRowView in selectedItems)
                {
                    DataRow dataRow = dataRowView.Row;
                }
            }
            catch
            {
                return;
            }
        }
    }
}
