using System;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Data.SQLite;
using System.IO;
using Microsoft.Win32;
namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Switch = true; // переключатель для кнопки администратора
        public bool CheckClose = true; // проверка закрытия/выхода
        public string FileName = Clipboard.GetText(); // получение пути к файлу БД из буфера обмена
        public MainWindow()
        {
            InitializeComponent();            
        }
        private void Student_MouseEnter(object sender, MouseEventArgs e) // изменение прозрачности кнопок при наведении мыши
        {
            Student.Opacity = 0.9;
        }
        private void Student_MouseLeave(object sender, MouseEventArgs e)
        {
            Student.Opacity = 0.5;
        }
        private void Admin_MouseEnter(object sender, MouseEventArgs e)
        {
            Admin.Opacity = 0.9;
        }
        private void Admin_MouseLeave(object sender, MouseEventArgs e)
        {
            Admin.Opacity = 0.5;
        }
        private void Check_MouseEnter(object sender, MouseEventArgs e)
        {
            Check.Opacity = 0.9;
        }
        private void Check_MouseLeave(object sender, MouseEventArgs e)
        {
            Check.Opacity = 0.5;
        }
        private void NameProg_MouseEnter(object sender, MouseEventArgs e)
        {
            NameProg.Opacity = 0.9;
        }
        private void NameProg_MouseLeave(object sender, MouseEventArgs e)
        {
            NameProg.Opacity = 0.7;
        }
        private void Admin_Click(object sender, RoutedEventArgs e) // при нажатии кнопки режима администратора
        {
            if (Admin.Content.ToString() == "Войти")
            {
                string TruePass = "";
                if (HASH(Pass.Password.ToString()) == TruePassword(TruePass)) // проверка текущего пароля с введённым 
                {
                    DATA.FileDBWayFromMainWindow = FileName; // отправление пути к БД в окно администратора
                    Administration DS = new Administration(); // открытие окна администратора
                    DS.Show();
                    CheckClose = false;                  
                    this.Close(); // закрытие текущего окна
                }
                else // откат к предыдущему состоянию при не верном пароле
                {                   
                    MessageBox.Show("Неверный пароль!", "Ошибка");
                    Pass.Clear();
                    ViewPass.Clear();
                    Pass.Visibility = Visibility.Hidden;
                    Check.Visibility = Visibility.Hidden;
                    ViewPass.Visibility = Visibility.Hidden;
                    Admin.Content = "Администратор";
                    Switch = false;
                }
            }
            if (Admin.Content.ToString() == "Администратор" && Switch) // показ элементов для ввода пароля
            {
                Pass.Visibility = Visibility.Visible;
                Check.Visibility = Visibility.Visible;               
                Check.IsEnabled = true;
                Admin.Content = "Войти";
                Switch = false;
                Pass.Focus();
            }
            Switch = true;
        }
        private void Student_Click(object sender, RoutedEventArgs e) //при нажатии кнопки режима студента
        {
            User US = new User(); // открытие окна студента
            US.Show();
            CheckClose = false;
            this.Close();
        }
        private string HASH(string Input) // шифрование введённого пароля для сверки с текущим в БД
        {
            byte[] Data = Encoding.Default.GetBytes(Input+"I_love_Diana_and_now_you_know_that"); // получение байтового массива введённого пароля
            var Result = new SHA256Managed().ComputeHash(Data); // шифрование байтового массива
            return BitConverter.ToString(Result).Replace("-", "").ToLower(); // реверс байтового массива в строку
        }
        private string TruePassword(string Output)
        {
            SQLiteConnection Conn = new SQLiteConnection(); // создание подключения к БД           
            if (File.Exists("QDB.db") == false && File.Exists(FileName)==false) // проверка на наличие БД или пути к БД
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную!", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" }; // открытие окна выбора файла БД
                if (DialogDB.ShowDialog() == true)
                {
                    FileName = DialogDB.FileName;
                   Conn = new SQLiteConnection("Data Source='" + FileName + "'; Password='230SDa51';"); // создание подключения с выбранным путём к БД
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0); // программный выход из программы
                }
            }
            else if(FileName!="")
            {
                Conn = new SQLiteConnection("Data Source='" + FileName + "'; Password='230SDa51';"); // создание подключения с имеющимся путём
            }
            else
            {
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';"); // создание подключения к БД в папке с исполняемым файлом
            }
            try
            {                               
                SQLiteCommand Query = Conn.CreateCommand(); // создание команды к подключению 
                Conn.Open(); // открытие подключения к БД
                Query.CommandText = "SELECT Hash FROM Admin;"; // команда
                Query.ExecuteNonQuery(); // выполнение команды
                Conn.Close(); // закрытие подключения
                DataTable DT = new DataTable(); // создание таблицы данных
                SQLiteDataAdapter DA = new SQLiteDataAdapter(Query); // привязывание таблицы к команде адаптером
                DA.Fill(DT); // заполнение таблицы
                Output = DT.Rows[0].ItemArray.GetValue(0).ToString(); // взятие значения
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
            }
            return Output;
        }

        private void Check_Click(object sender, RoutedEventArgs e) // кнопка показать пароль
        {
            if (ViewPass.Visibility == Visibility.Visible)
            {
                ViewPass.Visibility = Visibility.Hidden;
            }
            else
            {
                ViewPass.Visibility = Visibility.Visible;
            }
        }
        private void Pass_KeyUp(object sender, KeyEventArgs e) // выполнение кнопки "войти" путём нажатия на Enter
        {
            if (e.Key == Key.Enter)
            {
                Admin_Click(this, new RoutedEventArgs());
            }
        }

        private void NameProg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // открытие окна о программе
        {
            if (MessageBox.Show("Открыть информацию о приложении?", "Требутся подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {                
                var About = new About();               
                About.ShowDialog();
            }           
        }       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // событие при закрытии окна
        {
            if (CheckClose) // если закрытие окна НЕ программное
            {
                if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }            
        }
        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewPass.Text = Pass.Password.ToString(); // заполнение поля с паролем для показа
        }        
    }
}
