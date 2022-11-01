using System;
using System.Collections.Generic;
using System.Windows;

namespace SGSVostokLimited
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<City> listCities = new List<City>();
        private readonly Dictionary<City, List<Workshop>> listWorkshops = new Dictionary<City, List<Workshop>>();
        private readonly Dictionary<Workshop, List<Employee>> listEmployee = new Dictionary<Workshop, List<Employee>>();
        private readonly Brigade[] listBrigadeNumbers = new Brigade[2];

        private Shift shift1;

        private readonly int countOfListBrigade;

        public MainWindow()
        {
            InitializeComponent();

            //тестовые данные: (подгружать с бд было бы куда проще)
            listCities.Add(new City("Город_1"));
            listCities.Add(new City("Город_2"));

            listWorkshops[listCities[0]] = new List<Workshop>() { new Workshop("Цех_1"), new Workshop("Цех_2") };
            listWorkshops[listCities[1]] = new List<Workshop>() { new Workshop("Цех_3"), new Workshop("Цех_4") };

            listEmployee[listWorkshops[listCities[0]][0]] = new List<Employee>() { new Employee("Сотрудник_1"), new Employee("Сотрудник_2") };
            listEmployee[listWorkshops[listCities[0]][1]] = new List<Employee>() { new Employee("Сотрудник_3"), new Employee("Сотрудник_4") };
            listEmployee[listWorkshops[listCities[1]][0]] = new List<Employee>() { new Employee("Сотрудник_5"), new Employee("Сотрудник_6") };
            listEmployee[listWorkshops[listCities[1]][1]] = new List<Employee>() { new Employee("Сотрудник_7"), new Employee("Сотрудник_8") };

            listBrigadeNumbers[0] = new Brigade(1);
            listBrigadeNumbers[1] = new Brigade(2);

            //заполним ComboBox города 
            foreach (var item in listCities)
            {
                cityChoiceComboBox.Items.Add(item.Name);
            }

            //бригада
            if (DateTime.Now.Hour > 7 && DateTime.Now.Hour < 21)
            {
                brigadeLabel.Content += $"{listBrigadeNumbers[0].BrigadeNumber} (8:00 - 20:00)";

                countOfListBrigade = 0;
            }
            else
            {
                brigadeLabel.Content += $"{listBrigadeNumbers[1].BrigadeNumber} (20:00 - 8:00)";

                countOfListBrigade = 1;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cityChoiceComboBox.Text != "" && workshopChoiceComboBox.Text != "" && employeeChoiceComboBox.Text != "")
            {
                if (ushort.TryParse(shiftNumberTextBox.Text, out ushort numShift))
                {
                    if (numShift != 0)
                    {
                        shift1 = new Shift(numShift);

                        WorkingWithJSON fileIO = new WorkingWithJSON($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\result.json");

                        fileIO.SaveData(listCities[cityChoiceComboBox.SelectedIndex]);
                        fileIO.SaveData(listWorkshops[listCities[cityChoiceComboBox.SelectedIndex]][workshopChoiceComboBox.SelectedIndex]);
                        fileIO.SaveData(listEmployee[listWorkshops[listCities[cityChoiceComboBox.SelectedIndex]][workshopChoiceComboBox.SelectedIndex]][employeeChoiceComboBox.SelectedIndex]); //so hard
                        fileIO.SaveData(listBrigadeNumbers[countOfListBrigade]);
                        fileIO.SaveData(shift1);

                        MessageBox.Show("Проверьте рабочий стол!");
                    }
                }
                else
                {
                    MessageBox.Show("Впишите номер смены положительным числом");
                }
            }
            else
            {
                MessageBox.Show("Не оставляйте пустыми поля 'Город', 'Цех', 'Сотрудник'");
            }
        }

        private void WorkshopChoiceComboBox_DropDownOpened(object sender, System.EventArgs e)
        {
            workshopChoiceComboBox.Items.Clear();

            var listOfWorkshops = listWorkshops[listCities[cityChoiceComboBox.SelectedIndex]];

            //заполним ComboBox цехов
            foreach (var item in listOfWorkshops)
            {
                workshopChoiceComboBox.Items.Add(item.Name);
            }
        }

        private void EmployeeChoiceComboBox_DropDownOpened(object sender, System.EventArgs e)
        {
            employeeChoiceComboBox.Items.Clear();

            var listOfEmployee = listEmployee[listWorkshops[listCities[cityChoiceComboBox.SelectedIndex]][workshopChoiceComboBox.SelectedIndex]];

            //заполним ComboBox сотрудников 
            foreach (var item in listOfEmployee)
            {
                employeeChoiceComboBox.Items.Add(item.Name);
            }
        }
    }
}
