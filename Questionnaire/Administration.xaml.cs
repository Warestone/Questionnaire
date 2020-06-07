using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Ionic.Zip;
namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        public List<String> INFO = new List<String>();
        public List<String> DataMassive = new List<String>();
        public List<String> DataMassiveStat = new List<String>();
        public SQLiteDataReader Reader;
        public bool GetSomeData = false;
        public bool CheckClosing = true;
        public bool GetNewID = false;
        public bool CheckBlockNumVar = false;
        public string FileDBWay = DATA.FileDBWayFromMainWindow;
        public int CounterAnswear, CounterQuestion, NewID, NewIDQuest, NewIDForm, IDSelectedForm, Mode, Course, NumVarEditSel;        
        public Administration()
        {
            InitializeComponent();
            UpdateAll(); // функция обновления всех объектов, где берутся данные из БД
        }        
        private void ToWord_MouseEnter(object sender, MouseEventArgs e) // изменение прозрачности кнопок при наведении мыши
        {
            ToTXT.Opacity = 0.9;
        }
        private void ToWord_MouseLeave(object sender, MouseEventArgs e)
        {
            ToTXT.Opacity = 0.5;
        }
        private void AddQ_MouseEnter(object sender, MouseEventArgs e)
        {
            AddQ.Opacity = 0.9;
        }
        private void AddQ_MouseLeave(object sender, MouseEventArgs e)
        {
            AddQ.Opacity = 0.5;
        }
        private void AddA_MouseEnter(object sender, MouseEventArgs e)
        {
            AddA.Opacity = 0.9;
        }
        private void AddA_MouseLeave(object sender, MouseEventArgs e)
        {
            AddA.Opacity = 0.5;
        }
        private void EndA_MouseEnter(object sender, MouseEventArgs e)
        {
            EndA.Opacity = 0.9;
        }
        private void EndA_MouseLeave(object sender, MouseEventArgs e)
        {
            EndA.Opacity = 0.5;
        }
        private void DeleteQuest_MouseEnter(object sender, MouseEventArgs e)
        {
            DeleteQuest.Opacity = 0.9;
        }
        private void DeleteQuest_MouseLeave(object sender, MouseEventArgs e)
        {
            DeleteQuest.Opacity = 0.5;
        }
        private void AddNewQuest_MouseEnter(object sender, MouseEventArgs e)
        {
            AddNewQuest.Opacity = 0.9;
        }
        private void AddNewQuest_MouseLeave(object sender, MouseEventArgs e)
        {
            AddNewQuest.Opacity = 0.5;
        }
        private void EndQuest_MouseEnter(object sender, MouseEventArgs e)
        {
            EndQuest.Opacity = 0.9;
        }
        private void EndQuest_MouseLeave(object sender, MouseEventArgs e)
        {
            EndQuest.Opacity = 0.5;
        }
        private void DeleteSelected_MouseEnter(object sender, MouseEventArgs e)
        {
            DeleteSelected.Opacity = 0.9;
        }
        private void DeleteSelected_MouseLeave(object sender, MouseEventArgs e)
        {
            DeleteSelected.Opacity = 0.5;
        }
        private void Back_MouseEnter(object sender, MouseEventArgs e)
        {
            Back.Opacity = 0.9;
        }
        private void Back_MouseLeave(object sender, MouseEventArgs e)
        {
            Back.Opacity = 0.5;
        }
        private void Check_MouseEnter(object sender, MouseEventArgs e)
        {
            Check.Opacity = 0.9;
        }
        private void Check_MouseLeave(object sender, MouseEventArgs e)
        {
            Check.Opacity = 0.5;
        }
        private void AcceptEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            AcceptEdit.Opacity = 0.9;
        }
        private void AcceptEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            AcceptEdit.Opacity = 0.5;
        }
        private void CheckNewPass_MouseEnter(object sender, MouseEventArgs e)
        {
            CheckNewPass.Opacity = 0.9;
        }
        private void CheckNewPass_MouseLeave(object sender, MouseEventArgs e)
        {
            CheckNewPass.Opacity = 0.5;
        }
        private void AddNew_MouseEnter(object sender, MouseEventArgs e)
        {
            AddNewAnsw.Opacity = 0.9;
        }
        private void AddNew_MouseLeave(object sender, MouseEventArgs e)
        {
            AddNewAnsw.Opacity = 0.5;
        }
        private void CheckNewPassConfirm_MouseEnter(object sender, MouseEventArgs e)
        {
            CheckNewPassConfirm.Opacity = 0.9;
        }
        private void AddNewGroup_MouseEnter(object sender, MouseEventArgs e)
        {
            AddNewGroup.Opacity = 0.9;
        }
        private void AddNewGroup_MouseLeave(object sender, MouseEventArgs e)
        {
            AddNewGroup.Opacity = 0.5;
        }
        private void CheckNewPassConfirm_MouseLeave(object sender, MouseEventArgs e)
        {
            CheckNewPassConfirm.Opacity = 0.5;
        }
        private void AcceptPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            AcceptPassword.Opacity = 0.9;
        }
        private void AcceptPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            AcceptPassword.Opacity = 0.5;
        }
        private void DeleteSelectedGroup_MouseEnter(object sender, MouseEventArgs e)
        {
            DeleteSelectedGroup.Opacity = 0.9;
        }
        private void DeleteSelectedGroup_MouseLeave(object sender, MouseEventArgs e)
        {
            DeleteSelectedGroup.Opacity = 0.5;
        }
        private void AcceptEditGroup_MouseEnter(object sender, MouseEventArgs e)
        {
            AcceptEditGroup.Opacity = 0.9;
        }
        private void AcceptEditGroup_MouseLeave(object sender, MouseEventArgs e)
        {
            AcceptEditGroup.Opacity = 0.5;
        }
        private void StatToFile_MouseEnter(object sender, MouseEventArgs e)
        {
            StatToFile.Opacity = 0.9;
        }
        private void StatToFile_MouseLeave(object sender, MouseEventArgs e)
        {
            StatToFile.Opacity = 0.5;
        }
        private void Backup_MouseEnter(object sender, MouseEventArgs e)
        {
            Backup.Opacity = 0.9;
        }
        private void Backup_MouseLeave(object sender, MouseEventArgs e)
        {
            Backup.Opacity = 0.5;
        }
        private void Help_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Opacity = 0.9;
        }
        private void Help_MouseLeave(object sender, MouseEventArgs e)
        {
            Help.Opacity = 0.5;
        }
        private void HelpEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            HelpEdit.Opacity = 0.9;
        }
        private void HelpEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            HelpEdit.Opacity = 0.5;
        }
        private void AnswTextToTXT_MouseEnter(object sender, MouseEventArgs e)
        {
            AnswTextToTXT.Opacity = 0.9;
        }
        private void AnswTextToTXT_MouseLeave(object sender, MouseEventArgs e)
        {
            AnswTextToTXT.Opacity = 0.5;
        }
        private void NullRes_MouseEnter(object sender, MouseEventArgs e)
        {
            NullRes.Opacity = 0.9;
        }
        private void NullRes_MouseLeave(object sender, MouseEventArgs e)
        {
            NullRes.Opacity = 0.5;
        }
        private void UpdateAll()  // функция обновления всех объектов, где берутся данные из БД
        {
            string CommandText = "SELECT Date FROM Admin; ";
            GetSomeData = true;
            ConnectDB(CommandText); // вызов функции подключения к БД
            try { LastChangesPass.Content = "Последнее изменение пароля: " + INFO[0].ToString(); } // вывод информации о дате последнего изменения пароля
            catch (Exception) { }
            INFO.Clear();
            CommandText = "SELECT FormN FROM Form; ";
            GetSomeData = true;
            ConnectDB(CommandText); // получение списка всех анкет
            IDSelectedForm = ListOfQuestEdit.SelectedIndex; // выбранная анкета
            ListOfQuest.Items.Clear(); // очистка списков
            ListOfQuestForDelete.Items.Clear();
            ListOfQuestEdit.Items.Clear();
            ListOfQuestStat.Items.Clear();
            for (int vvI = 0; vvI < INFO.Count(); vvI++) // заполение списков
            {
                ListOfQuest.Items.Add(INFO[vvI]); // анкеты
                ListOfQuestForDelete.Items.Add(INFO[vvI]); // анкеты для удаления
                ListOfQuestEdit.Items.Add(INFO[vvI]); // анкеты для изменения
                ListOfQuestStat.Items.Add(INFO[vvI]); // анкеты для статистики
            }
            INFO.Clear();
            if (IDSelectedForm >= 0)
            {
                ListOfQuestEdit.SelectedIndex = IDSelectedForm;
            }
            CommandText = "SELECT GroupN, Specialization FROM [Group]; ";
            SQLiteConnection Conn = new SQLiteConnection(); // создание подключения к БД 
            if (File.Exists("QDB.db") == false && File.Exists(FileDBWay) == false) // проверка на наличие БД или пути к БД
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную!", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" }; // открытие окна выбора файла БД
                if (DialogDB.ShowDialog() == true)
                {
                    FileDBWay = DialogDB.FileName;
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';"); // создание подключения с выбранным путём к БД
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0); // программный выход из программы
                }
            }
            else if (File.Exists("QDB.db") == true)
            {
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';");  // создание подключения к БД в папке с исполняемым файлом
                FileDBWay = "QDB.db";
            }
            else if (File.Exists(FileDBWay) == true)
            {
                Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';"); // создание подключения с имеющимся путём
            }
            SQLiteCommand Command = Conn.CreateCommand();  // создание команды к подключению 
            try
            {
                Conn.Open(); // открытие подключения к БД
                Command.CommandText = CommandText; // команда
                Command.ExecuteNonQuery(); // выполнение команды
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
                return;
            }
            DataTable DT = new DataTable(); // создание таблицы данных
            SQLiteDataAdapter DA = new SQLiteDataAdapter(Command); // привязывание таблицы к команде адаптером
            DA.Fill(DT); // заполнение таблицы
            ListOfGroupsDG.ItemsSource = DT.DefaultView; // заполение таблицы с данными в интерфейсе 
        }
        private void Back_Click(object sender, RoutedEventArgs e) // кнопка возвраат на Главное окно
        {
            if (MessageBox.Show("Вы действительно хотите вернуться на экран выбора режима работы?\nНесохранённые данные могут быть утеряны!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Clipboard.Clear(); // очистка буфер обмена
                Clipboard.SetText(FileDBWay); // занесение пути к БД в буфер обмена
                MainWindow DS = new MainWindow();
                DS.Show();  // открытие Главного окна
                CheckClosing = false;
                this.Close(); // закрытие текущего
            }           
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // событие при закрытии программы
        {
            if (CheckClosing) // если закрытие программы НЕ программное
            {
                if (MessageBox.Show("Вы действительно хотите закрыть приложение?\nНесохранённые данные могут быть утеряны!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }            
        }
        private void ConnectDB(string CommandText) // функция подключения к БД
        {
            SQLiteConnection Conn = new SQLiteConnection(); // создание подключения к БД 
            if (File.Exists("QDB.db") == false && File.Exists(FileDBWay)==false) // проверка на наличие БД или пути к БД
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" }; // открытие окна выбора файла БД
                if (DialogDB.ShowDialog() == true)
                {
                    FileDBWay = DialogDB.FileName;
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';"); // создание подключения с выбранным путём к БД
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0);  // программный выход из программы
                }
            }           
            else if(File.Exists("QDB.db") == true)
            { 
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';"); // создание подключения к БД в папке с исполняемым файлом
                FileDBWay = "QDB.db";
            }
            else if(File.Exists(FileDBWay) == true)
            {
                Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';"); // создание подключения с имеющимся путём
            }
            SQLiteCommand Command = Conn.CreateCommand(); // создание команды к подключению 
            try
            {
                Conn.Open(); // открытие подключения к БД
                Command.CommandText = CommandText; // команда
                Command.ExecuteNonQuery(); // выполнение команды
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
            }
            if  (GetSomeData) // получение данных из БД
            {
                 try
                 {
                    Reader = Command.ExecuteReader(); // создание "читающего" оператора                     
                    while (Reader.Read())
                    {
                        INFO.Add(Reader.GetValue(0).ToString()); // занесение данных в массив
                    }
                 }
                 catch(Exception){}
                GetSomeData = false;
             }
            if (GetNewID) // получение нового ID
            {
                try
                {
                    Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        INFO.Add(Reader.GetValue(0).ToString());
                    }
                    if (INFO[0]=="") // если записей нет
                    {
                        INFO.Clear();
                        NewID = 1;
                        GetNewID = false;
                    }
                    else
                    {
                        NewID = Convert.ToInt32(INFO[0]) + 1;
                        INFO.Clear();
                        GetNewID = false;
                    }                    
                }
                catch (Exception){ }
                GetNewID = false;
            }
                Conn.Close();  // закрытие подключения                      
        }
        private string HASH(string Input) // шифрование введённого пароля для сверки с текущим в БД
        {
            byte[] Data = Encoding.Default.GetBytes(Input+"I_love_Diana_and_now_you_know_that"); // получение байтового массива введённого пароля
            var Result = new SHA256Managed().ComputeHash(Data); // шифрование байтового массива
            return BitConverter.ToString(Result).Replace("-", "").ToLower(); // реверс байтового массива в строку
        }
        private void Check_Click(object sender, RoutedEventArgs e) // событие при нажатии кнопки показа введённого пароля
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
        private void CheckNewPass_Click(object sender, RoutedEventArgs e) // событие при нажатии кнопки показа введённого пароля
        {                        
            if (ViewPass2.Visibility == Visibility.Visible)
            {
                ViewPass2.Visibility = Visibility.Hidden;
            }
            else
            {
                ViewPass2.Visibility = Visibility.Visible;
            }
        }
        private void CheckNewPassConfirm_Click(object sender, RoutedEventArgs e) // событие при нажатии кнопки показа введённого пароля
        {
            if (ViewPass3.Visibility == Visibility.Visible)
            {
                ViewPass3.Visibility = Visibility.Hidden;
            }
            else
            {
                ViewPass3.Visibility = Visibility.Visible;
            }           
        }
        private void AcceptPassword_Click(object sender, RoutedEventArgs e) // кнопка приняти изменения пароля
        {
            if (MessageBox.Show("Вы действительно хотите изменить текущий пароль?","Требуется подтверждение",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                if (NewPassword.Password.ToString()!=PasswordNowConfirm.Password.ToString()) // проверка новых введённых паролей
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка");
                    return;
                }
                else
                {
                    string CommandText = "SELECT Hash FROM Admin; ";
                    GetSomeData = true;
                    ConnectDB(CommandText); // получение текущего пароля
                    string PasswordNowDB = INFO[0].ToString();
                    INFO.Clear();
                    if (PasswordNowDB==HASH(NewPassword.Password.ToString())) // проверка введённого пароля с текущим
                    {
                        MessageBox.Show("Новый пароль совпадает с текущим!", "Ошибка");
                        return;
                    }
                    if (PasswordNowDB == HASH(PasswordNow.Password.ToString()))
                    {                       
                        CommandText = "UPDATE Admin SET Hash = '" + HASH(NewPassword.Password.ToString()) + "', Date ='"+ DateTime.Now.ToUniversalTime() + "' WHERE ID = 1; ";
                        ConnectDB(CommandText); // обновление пароля в БД
                        UpdateAll(); // обновление списков с данными
                        MessageBox.Show("Пароль успешно изменён!", "Сообщение");
                        PasswordNow.Clear(); // возврат к исходному состоянию объектов/очистка
                        NewPassword.Clear();
                        PasswordNowConfirm.Clear();
                        ViewPass.Visibility = Visibility.Hidden;
                        ViewPass2.Visibility = Visibility.Hidden;
                        ViewPass3.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Текущие пароли не совпадают!", "Ошибка");
                    }
                }
            }
        }
        private void PasswordNow_PasswordChanged(object sender, RoutedEventArgs e) // заполение поля для просмотра введённого пароля
        {
            ViewPass.Text = PasswordNow.Password.ToString();
            if (PasswordNow.Password.ToString()!=""&& NewPassword.Password.ToString() != "" && PasswordNowConfirm.Password.ToString() != "")
            {
                AcceptPassword.IsEnabled = true;
            }
            else
            {
                AcceptPassword.IsEnabled = false;
            }
        }
        private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e) // заполение поля для просмотра введённого пароля
        {
            ViewPass2.Text = NewPassword.Password.ToString();
            if (PasswordNow.Password.ToString() != "" && NewPassword.Password.ToString() != "" && PasswordNowConfirm.Password.ToString() != "")
            {
                AcceptPassword.IsEnabled = true;
            }
            else
            {
                AcceptPassword.IsEnabled = false;
            }
        }
        private void PasswordNowConfirm_PasswordChanged(object sender, RoutedEventArgs e) // заполение поля для просмотра введённого пароля
        {
            ViewPass3.Text = PasswordNowConfirm.Password.ToString();
            if (PasswordNow.Password.ToString() != "" && NewPassword.Password.ToString() != "" && PasswordNowConfirm.Password.ToString() != "")
            {
                AcceptPassword.IsEnabled = true;
            }
            else
            {
                AcceptPassword.IsEnabled = false;
            }
        }
        private void PasswordNowConfirm_KeyUp(object sender, KeyEventArgs e) // выполенение функции принять изменения пароля нажатием Enter
        {
            if (PasswordNow.Password.ToString() != "" && NewPassword.Password.ToString() != "" && PasswordNowConfirm.Password.ToString() != "")
            {
                if (e.Key == Key.Enter)
                {
                    AcceptPassword_Click(this, new RoutedEventArgs());
                }
            }           
            
        }
        private void NameQuest_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameQuest.Text!="")
            {
                NameQLabel.Opacity = 0.9;
                NameQ.IsEnabled = true;
            }
            else
            {
                NameQLabel.Opacity = 0.5;
                NameQ.IsEnabled = false;
            }
        }
        private void NameQ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameQ.Text!="")
            {
                AddQ.IsEnabled = true;
                AddQ.Opacity = 0.5;
            }
            else
            {
                AddQ.IsEnabled = false;
                AddQ.Opacity = 0.3;
            }
        }
        private void AddQ_Click(object sender, RoutedEventArgs e) // выполнение функции добавить вопрос в создании анкеты
        {
            if (NameQ.Text== "//Quest//" || NameQ.Text == "//ENDQ\\") // этот ввод запрещён, т.к. это "контрольные точки" в обработке массива данных
            {
                MessageBox.Show("Данный ввод вопроса недопустим", "Ошибка");
                return;
            }
            if (NameQ.Text=="")
            {
                AddQ.IsEnabled = false;
                NameQ.Focus();
                return;
            }
            DataMassive.Add( "//Quest//");
            DataMassive.Add(NameQ.Text); // занесение вопроса в массив
            AddQuestGD.Items.Add(new Item() {QorA="Вопрос", Text=NameQ.Text });
            AddQuestGD.Opacity = 0.8;
            AddQuestGD.IsEnabled = true;
            NameQuestLabel.Opacity = 0.5;
            NameQuest.IsEnabled = false;
            NameQLabel.Opacity = 0.5;
            NameQ.IsEnabled = false;
            NameALabel.Opacity = 0.9;
            NameA.IsEnabled = true;
            AddQ.Opacity = 0.3;
            AddQ.IsEnabled = false;            
            NameA.Focus();
        }
        private void NameA_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameA.Text!="")
            {
                AddA.Opacity = 0.5;
                AddA.IsEnabled = true;
            }
            else
            {
                AddA.Opacity = 0.3;
                AddA.IsEnabled = false;
            }
        }       
        private void EndA_Click(object sender, RoutedEventArgs e) // выполнение функции завершения вопроса
        {
            DataMassive.Add(NumVar.SelectedItem.ToString()); // количество вариантов ответа для выбора
            DataMassive.Add("//ENDQ\\"); // занесение "контрольной точки"
            AddQuestGD.Items.Add(new Item() { QorA = "---------", Text = "------------------------------------------------------------------------------------------------------------" });
            NameALabel.Opacity = 0.5;
            NameA.Clear();
            NameA.IsEnabled = false;
            NameQLabel.Opacity = 0.9;
            NameQ.Clear();
            NameQ.IsEnabled = true;
            AddA.Opacity = 0.3;
            AddA.IsEnabled = false;
            AddQ.Opacity = 0.5;
            AddQ.IsEnabled = true;
            EndA.Opacity = 0.3;
            EndA.IsEnabled = false;
            CounterAnswear = 0;
            NumVar.Items.Clear();
            NumVar.Opacity = 0.5;
            NumVar.IsEnabled = false;
            NumVarLabel.Opacity = 0.5;
            NameQ.Focus();
            CounterQuestion++;
            CheckBlockNumVar = false;
            if (CounterQuestion>=3) // контроль количества введённых вопросов
            {
                EndQuest.Opacity = 0.5;
                EndQuest.IsEnabled = true;
            }
        }
        private void ListOfQuest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfQuest.SelectedIndex==-1||ListOfQuest.SelectedItem.ToString()=="")
            {                
                ListOfAnswersDG.ItemsSource = null;
                ListOfQuestionsDG.ItemsSource = null;
                ListOfAnswersDG.Opacity = 0.5;
                ListOfQuestionsDG.Opacity = 0.5;
                ListOfAnswersDG.IsEnabled = false;
                ListOfQuestionsDG.IsEnabled = false;
                ToTXT.IsEnabled = false;
                ToTXT.Opacity = 0.3;
                return;
            }
            ListOfAnswersDG.ItemsSource = null;
            ListOfAnswersDG.Opacity = 0.5;
            string CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN = '"+ListOfQuest.SelectedItem.ToString()+"' ); ";
            SQLiteConnection Conn = new SQLiteConnection();
            if (File.Exists("QDB.db") == false && File.Exists(FileDBWay) == false)
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" };
                if (DialogDB.ShowDialog() == true)
                {
                    FileDBWay = DialogDB.FileName;
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0);
                }
            }
            else if (File.Exists("QDB.db") == true)
            {
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';");
                FileDBWay = "QDB.db";
            }
            else if (File.Exists(FileDBWay) == true)
            {
                Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
            }
            SQLiteCommand Command = new SQLiteCommand();
            Command = Conn.CreateCommand();
            try
            {
                Conn.Open();
                Command.CommandText = CommandText;
                Command.ExecuteNonQuery();
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
            }
            DataTable DT = new DataTable();
            SQLiteDataAdapter DA = new SQLiteDataAdapter(Command);
            DA.Fill(DT);
            ListOfQuestionsDG.ItemsSource = DT.DefaultView;
            ListOfQuestionsDG.Opacity = 0.8;
            ListOfQuestionsDG.IsEnabled = true;
            ToTXT.IsEnabled = true;
            ToTXT.Opacity = 0.5;
        }
        private void ListOfQuestionsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string CommandText;
            try
            {
                CommandText = "SELECT AnswN FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsDG.SelectedItems[0]).Row["QuestN"] + "'); ";
            }
            catch (Exception)
            {
                return;
            }
            SQLiteConnection Conn = new SQLiteConnection();
            if (File.Exists("QDB.db") == false && File.Exists(FileDBWay) == false)
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" };
                if (DialogDB.ShowDialog() == true)
                {
                    FileDBWay = DialogDB.FileName;
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0);
                }
            }
            else if (File.Exists("QDB.db") == true)
            {
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';");
                FileDBWay = "QDB.db";
            }
            else if (File.Exists(FileDBWay) == true)
            {
                Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
            }
            SQLiteCommand Command = new SQLiteCommand();
            Command = Conn.CreateCommand();
            try
            {
                Conn.Open();
                Command.CommandText = CommandText;
                Command.ExecuteNonQuery();
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
            }
            Conn.Close();
            DataTable DT = new DataTable();
            SQLiteDataAdapter DA = new SQLiteDataAdapter(Command);
            DA.Fill(DT);
            ListOfAnswersDG.ItemsSource = DT.DefaultView;
            ListOfAnswersDG.Opacity = 0.8;
            ListOfAnswersDG.IsEnabled = true;
        }
        private void DeleteQuest_Click(object sender, RoutedEventArgs e) // выполнение функции удаления анкеты
        {
            if (MessageBox.Show("Вы действительно хотите удалить анкету '"+ ListOfQuestForDelete.SelectedItem.ToString()+"'?\nТак же будут удалены все вопросы и ответы на эти вопросы в анкете и результаты прохождения этой анкеты!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string CommandText = "SELECT ID_Q FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='"+ ListOfQuestForDelete.SelectedItem.ToString()+"'); ";
                GetSomeData = true;
                ConnectDB(CommandText); // получение вопросов в анкете
                for (int i = 0; i < INFO.Count; i++)
                {
                    CommandText = "DELETE FROM Answear WHERE ID_Q = '" + INFO[i] + "';";
                    ConnectDB(CommandText); // удаление ответов
                    CommandText = "DELETE FROM TextAnswear WHERE ID_Q = '" + INFO[i] + "';";
                    ConnectDB(CommandText); // удаление текстовых ответов
                    CommandText = "DELETE FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='"+ ListOfQuestForDelete.SelectedItem.ToString()+"');";
                    ConnectDB(CommandText); // удаление анкет
                }
                INFO.Clear();              
                CommandText = "DELETE FROM MainTable WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuestForDelete.SelectedItem.ToString() + "');";
                ConnectDB(CommandText); // удаление результатов прохождения анкеты
                CommandText = "DELETE FROM Form WHERE FormN ='" + ListOfQuestForDelete.SelectedItem.ToString() + "';";
                ConnectDB(CommandText); // удаление анкеты
                MessageBox.Show("Анкета '"+ ListOfQuestForDelete.SelectedItem.ToString()+"' успешно удалена!", "Сообщение");
                UpdateAll();                             
            }
        }
        private void ListOfQuestForDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfQuestForDelete.SelectedIndex==-1|| ListOfQuestForDelete.SelectedItem.ToString()=="")
            {
                DeleteQuest.IsEnabled = false;
            }
            else
            {
                DeleteQuest.IsEnabled = true;
            }
        }
        private void ListOfQuestEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfQuestEdit.SelectedIndex == -1 || ListOfQuestEdit.SelectedItem.ToString() == "")
            {
                NameQuestEdit.Clear();
                NameQuestEdit.Opacity = 0.5;
                NameQuestEditLabel.Opacity = 0.5;
                NameQuestEdit.IsEnabled = false;
                ListOfAnswersEditDG.ItemsSource = null;
                ListOfQuestionsEditDG.ItemsSource = null;
                ListOfAnswersEditDG.Opacity = 0.5;
                ListOfQuestionsEditDG.Opacity = 0.5;
                ListOfAnswersEditDG.IsEnabled = false;
                ListOfQuestionsEditDG.IsEnabled = false;
                EditFieldLabel.Opacity = 0.5;
                EditField.IsEnabled = false;
                EditField.Clear();
                AddAnswEditLabel.Opacity = 0.5;
                AddAnswEdit.IsEnabled = false;
                AddAnswEdit.Clear();
                AddQuestEditLabel.Opacity = 0.5;
                AddQuestEdit.IsEnabled = false;
                AddQuestEdit.Clear();
                AcceptEdit.Opacity = 0.3;
                AcceptEdit.IsEnabled = false;
                DeleteSelected.Content = "Удалить";
                DeleteSelected.Opacity = 0.3;
                DeleteSelected.IsEnabled = false;
                AddNewAnsw.Opacity = 0.3;
                AddNewAnsw.IsEnabled = false;
                AddNewQuest.Opacity = 0.3;
                AddNewQuest.IsEnabled = false;
                NumVarEditLabel.Opacity = 0.5;
                NumVarEdit.Opacity = 0.5;
                NumVarEdit.IsEnabled = false;
                NumVarEdit.Items.Clear();
                return;
            }
            NumVarEdit.Items.Clear();
            NumVarEditLabel.Opacity = 0.5;
            NumVarEdit.Opacity = 0.5;
            NumVarEdit.IsEnabled = false;
            DeleteSelected.Content = "Удалить";
            DeleteSelected.Opacity = 0.3;
            DeleteSelected.IsEnabled = false;
            EditFieldLabel.Opacity = 0.5;
            EditField.IsEnabled = false;
            EditField.Clear();
            AddQuestEditLabel.Opacity = 0.5;
            AddQuestEdit.IsEnabled = false;
            AddQuestEdit.Clear();
            AddAnswEditLabel.Opacity = 0.5;
            AddAnswEdit.IsEnabled = false;
            AddAnswEdit.Clear();
            ListOfAnswersEditDG.ItemsSource = null;
            ListOfAnswersEditDG.Opacity = 0.5;
            NameQuestEdit.Text = ListOfQuestEdit.SelectedItem.ToString();
            NameQuestEdit.IsEnabled = true;
            NameQuestEdit.Opacity = 0.8;
            NameQuestEditLabel.Opacity = 0.9;
            string CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN = '" + ListOfQuestEdit.SelectedItem.ToString() + "' ); ";
            SQLiteConnection Conn = new SQLiteConnection();
            if (File.Exists("QDB.db") == false && File.Exists(FileDBWay) == false)
            {
                MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную", "Требуется действие");
                OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" };
                if (DialogDB.ShowDialog() == true)
                {
                    FileDBWay = DialogDB.FileName;
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
                }
                else
                {
                    MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                    Environment.Exit(0);
                }
            }
            else if (File.Exists("QDB.db") == true)
            {
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';");
                FileDBWay = "QDB.db";
            }
            else if (File.Exists(FileDBWay) == true)
            {
                Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
            }
            SQLiteCommand Command = new SQLiteCommand();
            Command = Conn.CreateCommand();
            try
            {
                Conn.Open();
                Command.CommandText = CommandText;
                Command.ExecuteNonQuery();
            }
            catch (Exception s)
            {
                MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
            }
            DataTable DT = new DataTable();
            SQLiteDataAdapter DA = new SQLiteDataAdapter(Command);
            DA.Fill(DT);
            ListOfQuestionsEditDG.ItemsSource = DT.DefaultView;
            ListOfQuestionsEditDG.IsEnabled = true;
            ListOfQuestionsEditDG.Opacity = 0.8;
            AddQuestEditLabel.Opacity = 0.9;
            AddQuestEdit.IsEnabled = true;
        }
        private void ListOfQuestionsEditDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddQuestEdit.IsEnabled)
            {
                string CommandText;
                try
                {
                    CommandText = "SELECT AnswN FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "' ); ";
                }
                catch (Exception){return;}               
                SQLiteConnection Conn = new SQLiteConnection();
                if (File.Exists("QDB.db") == false && File.Exists(FileDBWay) == false)
                {
                    MessageBox.Show("Файл базы данных 'QDB.db' не найден в папке с исполняемым файлом, укажите его вручную", "Требуется действие");
                    OpenFileDialog DialogDB = new OpenFileDialog() { Filter = "Data Base Files(*.db)|*.db|All(*.*)|*" };
                    if (DialogDB.ShowDialog() == true)
                    {
                        FileDBWay = DialogDB.FileName;
                        Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
                    }
                    else
                    {
                        MessageBox.Show("Программа не может корректно работать без базы данных 'QDB.db'!", "Ошибка");
                        Environment.Exit(0);
                    }
                }
                else if (File.Exists("QDB.db") == true)
                {
                    Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';");
                    FileDBWay = "QDB.db";
                }
                else if (File.Exists(FileDBWay) == true)
                {
                    Conn = new SQLiteConnection("Data Source='" + FileDBWay + "'; Password='230SDa51';");
                }
                SQLiteCommand Command = new SQLiteCommand();
                Command = Conn.CreateCommand();
                try
                {
                    Conn.Open();
                    Command.CommandText = CommandText;
                    Command.ExecuteNonQuery();
                }
                catch (Exception s)
                {
                    MessageBox.Show("База данных 'QDB.db' не найдена или используется другим процессом (приложением) в данный момент.\nСообщение ошибки:\n" + s, "Ошибка");
                }
                DataTable DT = new DataTable();
                SQLiteDataAdapter DA = new SQLiteDataAdapter(Command);
                DA.Fill(DT);
                Conn.Close();
                ListOfAnswersEditDG.ItemsSource = DT.DefaultView;
                ListOfAnswersEditDG.IsEnabled = true;
                ListOfAnswersEditDG.Opacity = 0.8;
                EditFieldLabel.Content = "Вопрос:";
                EditFieldLabel.Opacity = 0.9;
                EditField.Text = (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"];
                EditField.IsEnabled = true;
                DeleteSelected.Content = "Удалить вопрос";
                DeleteSelected.Opacity = 0.5;
                DeleteSelected.IsEnabled = true;
                AddAnswEditLabel.Opacity = 0.9; ;
                AddAnswEdit.IsEnabled = true;
                try
                {
                    CommandText = "SELECT NumVar FROM Question WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "') AND ID_F = (SELECT ID_F FROM Form WHERE FormN ='"+ ListOfQuestEdit.SelectedItem.ToString()+"'); ";
                }
                catch (Exception) { return; }
                GetSomeData = true;
                ConnectDB(CommandText);
                NumVarEditSel = Convert.ToInt32(INFO[0]);
                INFO.Clear();
                NumVarEdit.Items.Clear();
                for (int i = 1; i <= ListOfAnswersEditDG.Items.Count; i++)
                {
                    NumVarEdit.Items.Add(i);
                }
                NumVarEdit.SelectedItem = NumVarEditSel;               
                NumVarEditLabel.Opacity = 0.9;
                NumVarEdit.Opacity = 0.9;
                NumVarEdit.IsEnabled = true;
            }          
        }
        private void ListOfAnswersEditDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddQuestEdit.IsEnabled)
            {
                try
                {
                    EditField.Text = (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"];
                }
                catch (Exception)
                {return;}
                EditFieldLabel.Content = "Ответ:";
                AcceptEdit.Opacity = 0.3;
                AcceptEdit.IsEnabled = false;
                if (ListOfQuestEdit.SelectedItem.ToString() != NameQuestEdit.Text && NameQuestEdit.Text != "")
                {
                    AcceptEdit.Opacity = 0.5;
                    AcceptEdit.IsEnabled = true;
                }
                DeleteSelected.Content = "Удалить ответ";
                DeleteSelected.Opacity = 0.5;
                DeleteSelected.IsEnabled = true;
            }            
        }
        private void EditField_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                switch (EditFieldLabel.Content)
                {
                    case "Вопрос:":
                        if (EditField.Text != (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] && EditField.Text != "" && NameQuestEdit.Text != "")
                        {
                            AcceptEdit.Opacity = 0.5;
                            AcceptEdit.IsEnabled = true;
                            return;
                        }
                        break;
                    case "Ответ:":
                        if (EditField.Text != (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] && EditField.Text != "" && NameQuestEdit.Text != "")
                        {
                            AcceptEdit.Opacity = 0.5;
                            AcceptEdit.IsEnabled = true;
                            return;
                        }
                        break;
                }
                if (ListOfQuestEdit.SelectedItem.ToString() != NameQuestEdit.Text && NameQuestEdit.Text != "")
                {
                    AcceptEdit.Opacity = 0.5;
                    AcceptEdit.IsEnabled = true;
                }
                else
                {
                    AcceptEdit.Opacity = 0.3;
                    AcceptEdit.IsEnabled = false;
                }
            }
            catch (Exception)
            {}            
        }
        private void NameQuestEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ListOfQuestEdit.SelectedItem.ToString() != NameQuestEdit.Text && NameQuestEdit.Text != "")
                {
                    AcceptEdit.Opacity = 0.5;
                    AcceptEdit.IsEnabled = true;
                }
                else
                {
                    AcceptEdit.Opacity = 0.3;
                    AcceptEdit.IsEnabled = false;
                }
            }
            catch (Exception)
            {}
        }
        private void DeleteSelected_Click(object sender, RoutedEventArgs e) // выполнение функции удаления вопросов и ответов из анкеты
        {
            switch (DeleteSelected.Content.ToString())
            {
                case "Удалить вопрос": // удаление выделенного вопроса
                    if (MessageBox.Show("Вы действительно хотите удалить вопрос '"+ (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"]+"'?\nВместе с вопросом будут так же удалены ответы на этот вопрос и результаты прохождения этого вопроса в анкете!","Требуется подтверждение", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                    {
                        int TotalQuestions = ListOfQuestionsEditDG.Items.Count;
                        if (TotalQuestions < 4)
                        {
                            MessageBox.Show("Невозможно оставить анкету без минимум 3-ёх вопросов!", "Ошибка");
                            return;
                        }
                        string CommandText = "DELETE FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '"+ (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "') ;";
                        ConnectDB(CommandText); // удаление ответов на вопрос
                        CommandText = "DELETE FROM MainTable WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN ='"+ (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"]+ "') AND ID_F = (SELECT ID_F FROM Form WHERE FormN = '" + ListOfQuestEdit.SelectedItem.ToString() + "') ;"; 
                        ConnectDB(CommandText); // удаление результатов прохождения на вопрос
                        CommandText = "DELETE FROM TextAnswear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN ='"  + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"]+ "');";
                        ConnectDB(CommandText); // удаление текстовых ответов на вопрос
                        CommandText = "DELETE FROM Question WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "' AND ID_F = (SELECT ID_F FROM Form WHERE FormN = '" + ListOfQuestEdit.SelectedItem.ToString() + "') ;";
                        ConnectDB(CommandText); // удаление вопроса
                        MessageBox.Show("Вопрос '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "' успешно удалён!", "Сообщение");
                        UpdateAll();
                    }
                    break;
                case "Удалить ответ": // удаление выделенного ответа
                    if (MessageBox.Show("Вы действительно хотите удалить ответ '" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "'?\nВместе с ответом будут так же удалены результаты прохождения вопроса в анкете, где выбран этот ответ!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        int TotalAnswers = ListOfAnswersEditDG.Items.Count;
                        if (TotalAnswers<2)
                        {
                            MessageBox.Show("Невозможно оставить вопрос без вариантов ответа!","Ошибка");
                            return;
                        }                        
                        string CommandText="";
                        try
                        {
                            CommandText = "DELETE FROM MainTable WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "') AND ID_F = (SELECT ID_F FROM Form WHERE FormN = '" + ListOfQuestEdit.SelectedItem.ToString() + "') ;";
                        }
                        catch (Exception)
                        { return; }                        
                        ConnectDB(CommandText); // удаление результатов прохождения на ответ
                        CommandText = "DELETE FROM TextAnswear WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "');";
                        ConnectDB(CommandText); // удаление текстового ответа
                        CommandText = "DELETE FROM Answear WHERE AnswN = '" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "';";
                        ConnectDB(CommandText); // удаление ответа
                        MessageBox.Show("Ответ '" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "' успешно удалён!", "Сообщение");
                        UpdateAll();
                    }
                    break;
                default:
                    break;
            }
        }
        private void AcceptEdit_Click(object sender, RoutedEventArgs e) // выполнение функции принятия изменений
        {
            if (MessageBox.Show("Вы действительно хотите принять изменения?","Требуется подтверждение",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                try
                {
                    if (EditFieldLabel.Content.ToString() == "Вопрос:" && EditField.Text != (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"])
                    {
                        string CommandText = "UPDATE Question SET QuestN = '" + EditField.Text + "' WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "';";
                        ConnectDB(CommandText); // обновление наименования вопроса
                    }
                }catch (Exception){}
                try
                {
                    if (EditFieldLabel.Content.ToString() == "Ответ:" && EditField.Text != (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"])
                    {
                        string CommandText = "UPDATE Answear SET AnswN = '" + EditField.Text + "' WHERE AnswN = '" + (string)((DataRowView)ListOfAnswersEditDG.SelectedItems[0]).Row["AnswN"] + "';";
                        ConnectDB(CommandText); // обновление наименования ответа
                    }
                }catch (Exception){}
                try
                {
                    if (NameQuestEdit.Text != ListOfQuestEdit.SelectedItem.ToString())
                    {
                        string CommandText = "UPDATE Form SET FormN = '" + NameQuestEdit.Text + "' WHERE FormN = '" + ListOfQuestEdit.SelectedItem.ToString() + "';";
                        ConnectDB(CommandText); // обновление наименования анкеты
                    }
                }
                catch (Exception) { }
                try
                {
                    if (NumVarEdit.SelectedItem.ToString() != Convert.ToString(NumVarEditSel))
                    {
                        string CommandText = "UPDATE Question SET NumVar = '" + NumVarEdit.SelectedItem.ToString() + "' WHERE QuestN = '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "';";
                        ConnectDB(CommandText);
                    }
                }catch (Exception){}
                MessageBox.Show("Изменения успешно сохранены!", "Сообщение");
                UpdateAll();
            }
        }
        private void ToTXT_Click(object sender, RoutedEventArgs e) // выполнение функции вывода анкеты в TXT файл
        {
            if (MessageBox.Show("Вы действительно хотите вывести анкету '"+ ListOfQuest.SelectedItem.ToString()+"' в txt файл?", "Требуется подтверждение", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                try
                {
                    SaveFileDialog DialogTxT = new SaveFileDialog() { Filter = "Text Files(*.txt)|*.txt|All(*.*)|*" }; // получение пути путём выбора пользователем
                    if (DialogTxT.ShowDialog() == true)
                    {
                        if (File.Exists(DialogTxT.FileName)) // проверка наличия файла с таким же именем и его удаление
                        {
                            File.Delete(DialogTxT.FileName);
                        }
                        string CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN = '" + ListOfQuest.SelectedItem.ToString() + "');";
                        GetSomeData = true;
                        ConnectDB(CommandText); // получение вопросов
                        string[] Questions = new string[INFO.Count()];
                        int Count = 0;
                        foreach (var Quest in INFO)
                        {
                            Questions[Count] = Quest;
                            Count++;
                        }
                        INFO.Clear();
                        FileStream FS = new FileStream(DialogTxT.FileName, FileMode.OpenOrCreate); // создание файла
                        StreamWriter OUT = new StreamWriter(FS);
                        OUT.WriteLine(); // запись
                        OUT.WriteLine();
                        OUT.WriteLine("\t\t\t Анкета: " + ListOfQuest.SelectedItem.ToString()); // наименование анкеты
                        for (int i = 0; i < Questions.Length; i++)
                        {
                            OUT.WriteLine();
                            OUT.WriteLine();
                            OUT.WriteLine("\t\t" + Questions[i]); // вопрос
                            OUT.WriteLine();
                            CommandText = "SELECT AnswN FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + Questions[i] + "');";
                            GetSomeData = true;
                            ConnectDB(CommandText); // получение ответов
                            for (int s = 0; s < INFO.Count(); s++)
                            {
                                OUT.WriteLine("\t* " + INFO[s]); // ответ
                            }
                            INFO.Clear();
                        }
                        OUT.Close(); // закрытие файла, завершение записи
                        try { Process.Start(DialogTxT.FileName); } // открытие файла
                        catch (Exception) { }
                    }                                       
                }
                catch(Exception s)
                {
                    MessageBox.Show("Произола ошибка при записи в файл. Путь может содержать недопустимые символы!\nСообщение ошибки:\n"+s, "Ошибка");
                }
            }
        }
        private void AddAnswEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddAnswEdit.Text!="")
            {
                AddNewAnsw.Opacity = 0.5;
                AddNewAnsw.IsEnabled = true;
            }
            else
            {
                AddNewAnsw.Opacity = 0.3;
                AddNewAnsw.IsEnabled = false;
            }
        }
        private void AddQuestEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddQuestEdit.Text!="")
            {
                AddNewQuest.Opacity = 0.5;
                AddNewQuest.IsEnabled = true;
            }
            else
            {
                AddNewQuest.Opacity = 0.3;
                AddNewQuest.IsEnabled = false;
            }
        }
        private void AddNewAnsw_Click(object sender, RoutedEventArgs e) // добавление нового ответа на вопрос в анкету в редактировании
        {
            if (AddNewQuest.Content.ToString()== "Завершить формирование вопроса")
            {
                
                ListOfAnswersEditDG.IsEnabled = true;
                ListOfAnswersEditDG.Opacity = 0.8;
                ListOfAnswersEditDG.Items.Add(new Item3() { AnswN = AddAnswEdit.Text }); // добавление ответа в массив
                CounterAnswear++;
                DataMassive.Add( AddAnswEdit.Text);
                NumVarEdit.Items.Clear();
                for (int i = 1; i <= CounterAnswear; i++)
                {
                    NumVarEdit.Items.Add(i);
                }
                if (AddAnswEdit.Text == "Напишите свой ответ")
                {
                    NumVarEdit.SelectedIndex = 0;
                    NumVarEdit.Opacity = 0.5;
                    NumVarEdit.IsEnabled = false;
                    NumVarEditLabel.Opacity = 0.5;
                    CheckBlockNumVar = true;
                }
                else if (CheckBlockNumVar == false)
                {
                    NumVarEdit.Opacity = 0.9;
                    NumVarEdit.IsEnabled = true;
                    NumVarEditLabel.Opacity = 0.9;
                }
                if (CheckBlockNumVar)
                {
                    NumVarEdit.SelectedIndex = 0;
                    NumVarEdit.Opacity = 0.5;
                    NumVarEdit.IsEnabled = false;
                    NumVarEditLabel.Opacity = 0.5;
                }
                if (CounterAnswear>0 && NumVarEdit.SelectedIndex!=-1)
                {
                    AddNewQuest.Opacity = 0.5;
                    AddNewQuest.IsEnabled = true;
                }
                else
                {
                    AddNewQuest.Opacity = 0.3;
                    AddNewQuest.IsEnabled = false;
                }
                AddAnswEdit.Clear();
            }
            else
            {
                if (MessageBox.Show("Вы действительно хотите добавить ответ '" + AddAnswEdit.Text + "' в вопрос '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "'?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string CommandText = "SELECT max(ID_A) FROM Answear; ";
                    GetNewID = true;
                    ConnectDB(CommandText);          
                    CommandText = "INSERT INTO Answear (ID_A, ID_Q, AnswN) VALUES ('" + NewID + "', (SELECT ID_Q FROM Question WHERE QuestN= '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "'), '" + AddAnswEdit.Text + "');";
                    ConnectDB(CommandText); // добавление ответа на выбранный вопрос         
                    MessageBox.Show("Ответ '" + AddAnswEdit.Text + "' успешно добавлен в вопрос '" + (string)((DataRowView)ListOfQuestionsEditDG.SelectedItems[0]).Row["QuestN"] + "'!", "Сообщение");
                    UpdateAll();
                }
            }            
        }
        private void AddQuestEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (AddQuestEdit.Text!="" && e.Key == Key.Enter )
            {
                AddNewQuest_Click(this, new RoutedEventArgs());
            }
        }
        private void AddNewQuest_Click(object sender, RoutedEventArgs e) // выполнение функции добавления нового вопроса в анкету в редактировании
        {
            if (AddNewQuest.Content.ToString()== "Завершить формирование вопроса")
            {
                int i = 0;
                int NewIDQuest=0;
                while (DataMassive.Count>i)
                {
                    if (i==0)
                    {
                        string CommandText = "SELECT max(ID_Q) FROM Question; ";
                        GetNewID = true;
                        ConnectDB(CommandText);
                        NewIDQuest = NewID;
                        CommandText = "INSERT INTO Question (ID_Q, ID_F, QuestN, NumVar) VALUES ('"+ NewIDQuest + "', (SELECT ID_F FROM Form WHERE FormN ='"+ ListOfQuestEdit.SelectedItem.ToString()+"'), '"+DataMassive[0]+"', '" + NumVarEdit.SelectedItem.ToString() + "');";
                        ConnectDB(CommandText); // занесение вопроса в базу
                        i++;
                    }
                    else if(i!= DataMassive.Count)
                    {
                        string CommandText = "SELECT max(ID_A) FROM Answear; ";
                        GetNewID = true;
                        ConnectDB(CommandText);
                        CommandText = "INSERT INTO Answear (ID_A, ID_Q, AnswN) VALUES ('" + NewID + "', '"+ NewIDQuest+"', '" + DataMassive[i] + "');";
                        ConnectDB(CommandText); // занесение ответов на вопрос в базу
                        i++;
                    }
                }
                DataMassive.Clear();
                AddNewQuest.Content = "Добавить вопрос";
                CounterAnswear = 0;
                ListOfQuestionsEditDG.Items.Clear();
                ListOfAnswersEditDG.Items.Clear();
                NameQuest3.Opacity = 0.9;
                ListOfQuestEdit.IsEnabled = true;
                NumVarEdit.Items.Clear();
                CheckBlockNumVar = false;
                MessageBox.Show("Вопрос '" + EditField.Text + "' успешно добавлен в анкету '" + ListOfQuestEdit.SelectedItem.ToString() + "'!", "Сообщение");
                UpdateAll();                
            }
            else
            {
                if (MessageBox.Show("Вы действительно хотите добавить вопрос '" + AddQuestEdit.Text + "' в анкету '" + ListOfQuestEdit.SelectedItem.ToString() + "'?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    NumVarEditLabel.Opacity = 0.5;
                    NumVarEdit.Opacity = 0.5;
                    NumVarEdit.Items.Clear();
                    NumVarEdit.IsEnabled = false;
                    ListOfAnswersEditDG.ItemsSource = null;
                    ListOfQuestionsEditDG.ItemsSource = null;
                    EditField.Text = AddQuestEdit.Text;
                    EditField.IsEnabled = false;
                    EditFieldLabel.Content = "Вопрос:";
                    AcceptEdit.IsEnabled = false;
                    DeleteSelected.Content = "Удалить";
                    DeleteSelected.Opacity = 0.3;
                    DeleteSelected.IsEnabled = false;
                    ListOfQuestEdit.IsEnabled = false;
                    NameQuest3.Opacity = 0.5;
                    ListOfQuestionsEditDG.Items.Add(new Item2() { QuestN = AddQuestEdit.Text });
                    AddQuestEditLabel.Opacity = 0.5;
                    AddQuestEdit.IsEnabled = false;
                    AddAnswEditLabel.Opacity = 0.9;
                    AddAnswEdit.IsEnabled = true;
                    AddAnswEdit.Clear();
                    NameQuestEditLabel.Opacity = 0.5;
                    NameQuestEdit.IsEnabled = false;
                    DataMassive.Add(AddQuestEdit.Text);
                    AddQuestEdit.Clear();
                    AddNewQuest.Content = "Завершить формирование вопроса";
                    AddNewQuest.IsEnabled = false;                   
                    AddAnswEdit.Focus();
                }
            }                        
        }
        private void SpecializationName_KeyUp(object sender, KeyEventArgs e)
        {
            if (SpecializationName.Text!="" && GroupName.Text!="" && e.Key == Key.Enter)
            {
                AddNewGroup_Click(this, new RoutedEventArgs());
            }
            if (SpecializationName.Text != "" && e.Key == Key.Enter)
            {
                GroupName.Focus();
            }
        }
        private void AcceptEditGroup_Click(object sender, RoutedEventArgs e) // выполнение функции обновления данных группы в БД
        {
            if (MessageBox.Show("Вы действительно хотите принять изменения?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                if (SpecializationNameUpd.Text == (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["Specialization"] && EditGroup.Text != "" && EditGroup.Text != (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"])
                {
                    string CommandText = "UPDATE [Group] SET GroupN ='"+EditGroup.Text+ "' WHERE GroupN = '"+ (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"]+"'; ";
                    ConnectDB(CommandText); // обновление наименования группы
                }
                if (SpecializationNameUpd.Text != (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["Specialization"] && SpecializationNameUpd.Text !="" && EditGroup.Text == (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"])
                {
                    string CommandText = "UPDATE [Group] SET Specialization ='" + SpecializationNameUpd.Text + "' WHERE GroupN = '" + (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"] + "'; ";
                    ConnectDB(CommandText); // обновление наименования специальности
                }
                if (SpecializationNameUpd.Text!="" && SpecializationNameUpd.Text!= (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["Specialization"] && EditGroup.Text!="" && EditGroup.Text != (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"])
                {
                    string CommandText = "UPDATE [Group] SET GroupN ='" + EditGroup.Text + "', Specialization ='"+ SpecializationNameUpd .Text+ "'  WHERE GroupN = '" + (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"] + "'; ";
                    ConnectDB(CommandText); // обновление наименования группы и специальности
                }
                UpdateAll();
                SpecializationNameUpd.Clear();
                EditGroup.Clear();
                MessageBox.Show("Изменения успешно сохранены!", "Сообщение");
            }
        }
        private void GroupName_KeyUp(object sender, KeyEventArgs e)
        {
            if (SpecializationName.Text != "" && GroupName.Text!="" && e.Key == Key.Enter)
            {
                AddNewGroup_Click(this, new RoutedEventArgs());
            }
            if (GroupName.Text != "" && e.Key == Key.Enter)
            {
                SpecializationName.Focus();
            }
        }
        private void SpecializationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpecializationName.Text != "" && GroupName.Text != "")
            {
                AddNewGroup.IsEnabled = true;
                AddNewGroup.Opacity = 0.5;
            }
            else
            {
                AddNewGroup.IsEnabled = false;
                AddNewGroup.Opacity = 0.3;
            }
        }
        private void GroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpecializationName.Text != "" && GroupName.Text != "")
            {
                AddNewGroup.IsEnabled = true;
                AddNewGroup.Opacity = 0.5;
            }
            else
            {
                AddNewGroup.IsEnabled = false;
                AddNewGroup.Opacity = 0.3;
            }
        }
        private void ListOfGroupsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DeleteSelectedGroup.Opacity = 0.5;
                DeleteSelectedGroup.IsEnabled = true;
                SpecializationLabelUpd.Opacity = 0.9;
                SpecializationNameUpd.IsEnabled = true;
                EditGroupLabel.Opacity = 0.9;
                EditGroup.IsEnabled = true;
                SpecializationNameUpd.Text = (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["Specialization"];
                EditGroup.Text = (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"];
            }
            catch (Exception) { }
        }
        private void SpecializationNameUpd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SpecializationNameUpd.Text != (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["Specialization"] && SpecializationNameUpd.Text != "" && EditGroup.Text != "")
                {
                    AcceptEditGroup.Opacity = 0.5;
                    AcceptEditGroup.IsEnabled = true;
                }
                else
                {
                    AcceptEditGroup.Opacity = 0.3;
                    AcceptEditGroup.IsEnabled = false;
                }
            }
            catch (Exception){}            
        }
        private void EditGroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EditGroup.Text != (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"] && EditGroup.Text != "" && SpecializationNameUpd.Text != "")
                {
                    AcceptEditGroup.Opacity = 0.5;
                    AcceptEditGroup.IsEnabled = true;
                }
                else
                {
                    AcceptEditGroup.Opacity = 0.3;
                    AcceptEditGroup.IsEnabled = false;
                }
            }
            catch (Exception){}            
        }
        private void AddNewGroup_Click(object sender, RoutedEventArgs e) // выполнение функции добавления новой группы
        {
            if (MessageBox.Show("Вы действительно хотите добавить группу '" + GroupName.Text + "' и специальность '" + SpecializationName.Text + "'?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string CommandText = "SELECT max(ID_G) FROM [Group]; ";
                GetNewID = true;
                ConnectDB(CommandText);
                INFO.Clear();
                CommandText = "INSERT INTO [Group] (ID_G, GroupN, Specialization) VALUES ('"+NewID+"' ,'"+ GroupName.Text+"' ,'"+ SpecializationName.Text+"'); ";
                ConnectDB(CommandText); // добавление новой группы
                UpdateAll();
                MessageBox.Show("Группа '" + GroupName.Text + "' и специальность '" + SpecializationName.Text + "' успешно добавлены!", "Сообщение");
                SpecializationName.Clear();
                GroupName.Clear();                
            }
        }
        private void DeleteSelectedGroup_Click(object sender, RoutedEventArgs e) // выполнение функции удаления выбранной группы
        {
            if (MessageBox.Show("Вы действительно хотите удалить группу '" + (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"] + "'?\nТак же будут удалены результаты пройденных анкет этой группы!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string CommandText = "DELETE FROM MainTable WHERE ID_G = (SELECT ID_G FROM [Group] WHERE GroupN='" + (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"] + "'); ";
                ConnectDB(CommandText); // удаление результатов прохождения анкет выбранной группы
                CommandText = "DELETE FROM [Group] WHERE GroupN ='"+ (string)((DataRowView)ListOfGroupsDG.SelectedItems[0]).Row["GroupN"]+"'; ";
                ConnectDB(CommandText); // удаление группы
                UpdateAll();
                MessageBox.Show("Группа '" + EditGroup.Text + "' успешно удалена!", "Сообщение");
                EditGroup.Clear();
                SpecializationNameUpd.Clear();
            }
        }
        private void ListOfQuestStat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfQuestStat.SelectedIndex!=-1)
            {                
                Param1StatLabel.Opacity = 0.9;
                ListOfParam1.Opacity = 0.9;
                ListOfParam1.IsEnabled = true;
                ListOfParam1.SelectedIndex = -1;
                Param2StatLabel.Content = "Параметр:";
                Param2StatLabel.Opacity = 0.5;
                ListOfParam2.Items.Clear();
                ListOfParam2.IsEnabled = false;
                StatToFile.Opacity = 0.3;
                StatToFile.IsEnabled = false;
                AnswTextToTXT.Opacity = 0.3;
                AnswTextToTXT.IsEnabled = false;
                ListOfTotalAnswers.Items.Clear();
                ListOfTotalAnswers.Opacity = 0.5;
                ListOfTotalAnswers.IsEnabled = false;
            }
            else
            {
                Param2StatLabel.Content = "Параметр:";
                Param2StatLabel.Opacity = 0.5;
                ListOfParam2.Items.Clear();
                ListOfParam2.IsEnabled = false;
                Param1StatLabel.Opacity = 0.5;
                ListOfParam1.Opacity = 0.5;
                ListOfParam1.IsEnabled = false;
                ListOfParam1.SelectedIndex = -1;
                ListOfParam2.Items.Clear();
                ListOfTotalAnswers.Items.Clear();
                ListOfTotalAnswers.Opacity = 0.5;
                ListOfTotalAnswers.IsEnabled = false;
            }
        }
        private void Statistics() // выполнение функции подсчёта статистики
        {
            ListOfTotalAnswers.Opacity = 0.8;
            ListOfTotalAnswers.IsEnabled = true;
            ListOfTotalAnswers.Items.Clear();
            DataMassiveStat.Clear();
            string CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuestStat.SelectedItem.ToString() + "');";
            GetSomeData = true;
            ConnectDB(CommandText); // получение вопросов
            foreach (var Question in INFO)
            {
                DataMassive.Add(Question);
            }
            INFO.Clear();
            int i = 0;
            while (DataMassive.Count() > i)
            {
                CommandText = "SELECT AnswN FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN ='" + DataMassive[i] + "');";
                GetSomeData = true;
                ConnectDB(CommandText); // получение ответов на вопрос
                ListOfTotalAnswers.Items.Add(new Item() { QorA = "---------", Text = "     " + DataMassive[i] });
                DataMassiveStat.Add(DataMassive[i]); // занесение в массив для вывода в Excel
                List<String> Answers = new List<String>();
                foreach (var AnswersI in INFO)
                {
                    Answers.Add(AnswersI);
                }
                INFO.Clear();
                for (int s = 0; s < Answers.Count(); s++)
                {
                    try
                    {
                        switch (Mode) // в зависимости от выбранных параметров
                        {
                            case 1:
                                CommandText = "SELECT COUNT (ID_A) FROM MainTable WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + Answers[s] + "') AND ID_TA = '0' AND ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '"+DataMassive[i]+"');";
                                break;
                            case 2:
                                CommandText = "SELECT COUNT (ID_A) FROM MainTable WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + Answers[s] + "') AND Specialization = '" + ListOfParam2.SelectedItem.ToString() + "' AND ID_TA = '0' AND ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + DataMassive[i] + "');";
                                break;
                            case 3:
                                CommandText = "SELECT COUNT (ID_A) FROM MainTable WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + Answers[s] + "') AND ID_G = (SELECT ID_G FROM [Group] WHERE GroupN = '" + ListOfParam2.SelectedItem.ToString() + "') AND ID_TA = '0' AND ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + DataMassive[i] + "');";
                                break;
                            case 4:
                                CommandText = "SELECT COUNT (ID_A) FROM MainTable WHERE ID_A = (SELECT ID_A FROM Answear WHERE AnswN ='" + Answers[s] + "') AND Course = '" + Course + "' AND ID_TA = '0' AND ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + DataMassive[i] + "');";
                                break;
                        }                       
                    }
                    catch (Exception) { };
                    GetSomeData = true;
                    ConnectDB(CommandText); // подсчёт ответов
                    ListOfTotalAnswers.Items.Add(new Item() { QorA = INFO[0], Text = Answers[s] });
                    DataMassiveStat.Add(Answers[s]);
                    DataMassiveStat.Add(INFO[0]);
                    INFO.Clear();
                }
                DataMassiveStat.Add("//ENDQ\\");
                ListOfTotalAnswers.Items.Add(new Item() { QorA = "---------", Text = "----------------------------------------------------------------------------------------------------------------" });
                Answers.Clear();
                i++;
            }
            DataMassive.Clear();
            INFO.Clear();
            StatToFile.Opacity = 0.5;
            StatToFile.IsEnabled = true;           
        }
        private void ListOfParam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfParam1.IsEnabled==true && ListOfParam1.SelectedIndex!=-1)
            {
                ListOfParam2.Items.Clear();
                StatToFile.Opacity = 0.3;
                StatToFile.IsEnabled = false;
                AnswTextToTXT.Opacity = 0.3;
                AnswTextToTXT.IsEnabled = false;
                switch (ListOfParam1.SelectedIndex)
                {
                    case 2:
                        ListOfTotalAnswers.Items.Clear();
                        ListOfTotalAnswers.Opacity = 0.5;
                        ListOfTotalAnswers.IsEnabled = false;
                        DataMassiveStat.Clear();
                        string CommandText = "SELECT DISTINCT Specialization FROM [Group]";
                        GetSomeData = true;
                        ConnectDB(CommandText);
                        int i = 0;
                        while (i<INFO.Count)
                        {
                            ListOfParam2.Items.Add(INFO[i]);
                            i++;
                        }
                        Param2StatLabel.Opacity = 0.9;
                        Param2StatLabel.Content = "Специальность:";
                        ListOfParam2.Opacity = 0.9;
                        ListOfParam2.IsEnabled = true;
                        INFO.Clear();
                        break;
                    case 3:
                        Param2StatLabel.Content = "Параметр:";
                        Param2StatLabel.Opacity = 0.5;
                        ListOfParam2.Opacity = 0.5;
                        ListOfParam2.IsEnabled = false;
                        Mode = 1;
                        Statistics();
                        AnswTextToTXT.Opacity = 0.5;
                        AnswTextToTXT.IsEnabled = true;
                        break;
                    case 0:
                        ListOfTotalAnswers.Items.Clear();
                        ListOfTotalAnswers.Opacity = 0.5;
                        ListOfTotalAnswers.IsEnabled = false;
                        DataMassiveStat.Clear();
                        CommandText = "SELECT DISTINCT GroupN FROM [Group]";
                        GetSomeData = true;
                        ConnectDB(CommandText);
                        i = 0;
                        while (i < INFO.Count)
                        {
                            ListOfParam2.Items.Add(INFO[i]);
                            i++;
                        }
                        Param2StatLabel.Opacity = 0.9;
                        Param2StatLabel.Content = "Группа:";
                        ListOfParam2.Opacity = 0.9;
                        ListOfParam2.IsEnabled = true;
                        INFO.Clear();
                        break;
                    case 1:
                        ListOfTotalAnswers.Items.Clear();
                        ListOfTotalAnswers.Opacity = 0.5;
                        ListOfTotalAnswers.IsEnabled = false;
                        DataMassiveStat.Clear();
                        ListOfParam2.Items.Add("Первый");
                        ListOfParam2.Items.Add("Второй");
                        ListOfParam2.Items.Add("Третий");
                        ListOfParam2.Items.Add("Четвёртый");
                        Param2StatLabel.Opacity = 0.9;
                        Param2StatLabel.Content = "Курс:";
                        ListOfParam2.Opacity = 0.9;
                        ListOfParam2.IsEnabled = true;
                        break;                   
                }
            }
            else
            {
                ListOfParam2.Items.Clear();
                Param2StatLabel.Opacity = 0.5;
                Param2StatLabel.Content = "Параметр";
                ListOfParam2.Opacity = 0.5;
                ListOfParam2.IsEnabled = false;
            }
        }
        private void StatToFile_Click(object sender, RoutedEventArgs e) // выполнение функции записи статистики в Excel файл
        {
            if (MessageBox.Show("Вы действительно хотите вывести статистику в файл Microsoft Excel?","Требуется подтверждение", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                try
                {                                     
                    SaveFileDialog DialogExcel = new SaveFileDialog(){Filter = "Microsoft Excel Files(*.xls)|*.xls|All(*.*)|*"};
                    if (DialogExcel.ShowDialog() == true)
                    {
                        if (File.Exists(DialogExcel.FileName))
                        {
                            File.Delete(DialogExcel.FileName);
                        }
                        Excel.Application ObjExcel = new Excel.Application(); // создание нового подключения
                        Excel.Workbook ObjWorkBook;
                        Excel.Worksheet ObjWorkSheet;
                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value); // добавление рабочей книги
                        ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; // добавление страницы
                        int i = 1;
                        ObjWorkSheet.Cells[i, 1] = "                  " + ListOfQuestStat.SelectedItem.ToString(); // добавление статистики
                        ObjWorkSheet.Cells[i, 1].Font.Bold = true;
                        i++;
                        ObjWorkSheet.Cells[i, 2] = "Количество раз выбрано";
                        i++;
                        for (int s = 0; s < DataMassiveStat.Count; s++)
                        {
                            ObjWorkSheet.Cells[i, 1] = "       "+ DataMassiveStat[s];
                            ObjWorkSheet.Cells[i, 1].Font.Underline = true;
                            int FinalAddr = 0;
                            for (int d = s + 1; d < DataMassiveStat.Count; d++)
                            {
                                if (DataMassiveStat[d] == "//ENDQ\\")
                                {
                                    FinalAddr = d;
                                    break;
                                }
                            }
                            i++;
                            for (int a = s + 1; a < FinalAddr; a++)
                            {
                                ObjWorkSheet.Cells[i, 1] = "  " + DataMassiveStat[a];
                                ObjWorkSheet.Cells[i, 1].Font.Italic = true;
                                ObjWorkSheet.Cells[i, 2] = DataMassiveStat[a + 1];
                                i++;
                                a++;
                            }
                            s = FinalAddr;
                        }                        
                        ObjWorkSheet.Cells.EntireColumn.Font.Name = "Times New Roman"; // шрифт 
                        ObjWorkSheet.Cells.EntireColumn.Font.Size = 20; // размер
                        ObjWorkSheet.Cells.EntireColumn.AutoFit(); // корректировка ширины столбцов
                        ObjWorkBook.SaveAs(DialogExcel.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        ObjExcel.Quit(); // закрытие подключения
                    }                                      
                    try { Process.Start(DialogExcel.FileName); } // открытие файла
                    catch (Exception) { }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка при записи статистики в файл Microsoft Excel\n\nСообщение ошибки:\n\n" + exc.Message);
                }
            }
        }
        private void Backup_Click(object sender, RoutedEventArgs e) // выполнение функции резервного копирования
        {
            try
            {
                if (MessageBox.Show("Создать резервную копию базы данных?", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SaveFileDialog DialogBackup = new SaveFileDialog() { Filter = "Zip files (*.zip)|*.zip" };
                    if (DialogBackup.ShowDialog() == true)
                    {
                        if (File.Exists(DialogBackup.FileName))
                        {
                            File.Delete(DialogBackup.FileName);
                        }
                        ZipFile BackupZip = new ZipFile(DialogBackup.FileName); // создание нового zip архива
                        BackupZip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed; // уровень компрессии
                        BackupZip.AddFile(FileDBWay); // добавление файла БД
                        BackupZip.Save(DialogBackup.FileName);
                        try { Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + DialogBackup.FileName)); }
                        catch (Exception) { }
                    }
                }
            }
            catch (Exception s) { MessageBox.Show("Произошла ошибка при создании резервной копии базы данных!\nСообщение ошибки:\n"+s,"Ошибка"); }
        }
        private void Help_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Помощь в создании анкеты, последовательность действий:\n1 - введите наименование анкеты\n2 - введите вопрос\n3 - нажмите кнопку 'Добавить вопрос' или Enter\n4 - введите ответ\n5 - нажмите кнопку 'Добавить ответ' или Enter\n6 - введя необходимое количество ответов, выберите количество доступных вариантов для выбора и нажмите кнопку 'Завершить формирование вопроса'\n7 - введите ещё 2 вопроса\n8 - нажмите кнопку 'Завершить формирование анкеты'\n\nЕсли ввести ответ 'Напишите свой ответ', в режиме Студента будет возможность написать свой ответ!","Справка");
        }       
        private void HelpEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Помощь в редактировании анкеты, последовательность действий:\n1 - выберите анкету\n2 - вы можете изменить наименование анкеты в поле 'Наименование анкеты'\n3 - выберите вопрос\n4 - вы можете изменить выбранный вопрос в поле 'Вопрос'\n5 - вы можете изменить количество доступных вариантов ответов для выбора\n6 - после любых изменений, нажмите кнопку 'Принять изменения', поля не могут оставаться пустыми!\n7 - выберите ответ\n8 - вы можете изменить выбранный ответ в поле 'Ответ'\n9 - вы можете добавить ответ, на выбранный вопрос, написав ответ в поле 'Добавить ответ' и нажав кнопку 'Добавить ответ'\n10 - если вы добавите ответ 'Напишите свой ответ', в режиме прохождения анкет будет разблокированно поле ввода для произвольного ответа\n11 - вы можете добавить вопрос, написав вопрос в поле 'Добавить вопрос'и нажав кнопку 'Добавить вопрос'\n12 - вы можете удалить выбранный вопрос или ответ, нажав кнопку 'Удалить вопрос' или 'Удалить ответ'","Справка");
        }
        private void AnswTextToTXT_Click(object sender, RoutedEventArgs e) // выполнение функции вывода в TXT файл текстовых ответов
        {
            string CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuestStat.SelectedItem.ToString() + "');";
            GetSomeData = true;
            ConnectDB(CommandText); // получение всех вопросов
            List<String> Questions = new List<String>();
            foreach (var Quest in INFO)
            {
                Questions.Add(Quest);
            }
            INFO.Clear();
            List<String> AnswersTA = new List<String>();
            int i = 0;
            while (Questions.Count>i)
            {
                CommandText = "SELECT TextTA FROM TextAnswear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN ='"+ Questions[i]+"')";
                GetSomeData = true;
                ConnectDB(CommandText); // получение текстовых ответов на вопрос
                if (INFO.Count!=0)
                {
                    AnswersTA.Add(Questions[i]);
                    foreach (var AnswTA in INFO)
                    {
                        AnswersTA.Add(AnswTA); // занесение в массив
                    }
                    AnswersTA.Add("//ENDQ\\");
                }
                INFO.Clear();
                i++;
            }
            if (AnswersTA.Count!=0)
            {
                try
                {
                    SaveFileDialog DialogTxT = new SaveFileDialog() { Filter = "Text Files(*.txt)|*.txt|All(*.*)|*" };
                    if (DialogTxT.ShowDialog() == true)
                    {
                        if (File.Exists(DialogTxT.FileName))
                        {
                            File.Delete(DialogTxT.FileName);
                        }
                        int FinalAddr = 0;
                        FileStream FS = new FileStream(DialogTxT.FileName, FileMode.OpenOrCreate);
                        StreamWriter OUT = new StreamWriter(FS);
                        OUT.WriteLine();
                        OUT.WriteLine();
                        OUT.WriteLine("\t\t\t Анкета: " + ListOfQuestStat.SelectedItem.ToString());
                        OUT.WriteLine();
                        OUT.WriteLine();
                        for (int s = 0; s < AnswersTA.Count; s++) // запись текстовых ответов из массива в файл
                        {                            
                            OUT.WriteLine("\t\t" + AnswersTA[s]);                            
                            for (int d = s; d < AnswersTA.Count; d++)
                            {
                                if (AnswersTA[d]== "//ENDQ\\")
                                {
                                    FinalAddr = d;
                                }
                            }
                            for (int r = s+1; r < FinalAddr; r++)
                            {
                                OUT.WriteLine("\t\t\t"+AnswersTA[r]);
                                OUT.WriteLine();
                            }
                            OUT.WriteLine();
                            OUT.WriteLine();
                            s = FinalAddr + 1;
                        }
                        OUT.Close();
                    }
                    try { Process.Start(DialogTxT.FileName); }
                    catch (Exception) { }
                }
                catch (Exception s)
                {
                    MessageBox.Show("Произола ошибка при записи в файл. Путь может содержать недопустимые символы!\nСообщение ошибки:\n" + s, "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("На эту анкету нет ответов, введённых вручную!", "Сообщение");
                AnswTextToTXT.Opacity = 0.3;
                AnswTextToTXT.IsEnabled = false;
            }
        }
        private void NullRes_Click(object sender, RoutedEventArgs e) // выполнение функции обнуления результатов прохождения анкет
        {
            if (MessageBox.Show("Вы действительно хотите обнулить все результаты прохождения анкет?\nВНИМАНИЕ, рекомендуется сделать резервную копию базы данных!","Требуется подтверждение",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                string CommandText = "DELETE FROM MainTable;";
                ConnectDB(CommandText); // очистка таблицы с результатами прохождения анкет
                CommandText = "DELETE FROM TextAnswear;";
                ConnectDB(CommandText); // очистка таблицы с текстовыми ответами
                UpdateAll();
                MessageBox.Show("Результаты прохождения анкет успешно обнулены!","Сообщение");
            }
        }
        private void NumVarEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (NumVarEdit.SelectedItem.ToString() != Convert.ToString(NumVarEditSel) && AddNewQuest.Content.ToString() != "Завершить формирование вопроса")
                {
                    AcceptEdit.Opacity = 0.5;
                    AcceptEdit.IsEnabled = true;
                }
                else if (AddNewQuest.Content.ToString() != "Завершить формирование вопроса")
                {
                    AcceptEdit.Opacity = 0.3;
                    AcceptEdit.IsEnabled = false;
                }
                if (NumVarEdit.SelectedIndex!=-1 && AddNewQuest.Content.ToString() == "Завершить формирование вопроса" && CheckBlockNumVar == false)
                {
                    AddNewQuest.Opacity = 0.5;
                    AddNewQuest.IsEnabled = true;
                }
                else if(NumVarEdit.SelectedIndex == -1 && AddNewQuest.Content.ToString() == "Завершить формирование вопроса")
                {
                    AddNewQuest.Opacity = 0.3;
                    AddNewQuest.IsEnabled = false;
                }
            }
            catch (Exception) { }            
        }       
        private void NumVar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CounterAnswear > 0 && NumVar.SelectedIndex != -1)
            {
                EndA.Opacity = 0.5;
                EndA.IsEnabled = true;
            }
        }
        private void AddAnswEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (AddAnswEdit.Text != "" && e.Key==Key.Enter)
            {
                AddNewAnsw_Click(this, new RoutedEventArgs());
            }
        }
        private void ListOfParam2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Param2StatLabel.Content.ToString())
            {
                case "Специальность:":
                    Mode = 2;
                    Statistics();
                    break;
                case "Группа:":
                    Mode = 3;
                    Statistics();
                    break;
                case "Курс:":                    
                    try
                    {
                        switch (ListOfParam2.SelectedItem.ToString())
                        {
                            case "Первый":
                                Course = 1;
                                break;
                            case "Второй":
                                Course = 2;
                                break;
                            case "Третий":
                                Course = 3;
                                break;
                            case "Четвёртый":
                                Course = 4;
                                break;
                        }
                    }
                    catch (Exception) { }
                    Mode = 4;
                    Statistics();
                    break;
            }
        }
        private void EndQuest_Click(object sender, RoutedEventArgs e) // выполение функции завершения создания анкеты 
        {
            string CommandText = "SELECT max(ID_F) FROM Form; ";
            GetNewID = true;
            ConnectDB(CommandText);
            NewIDForm = NewID;
            INFO.Clear();
            CommandText = "INSERT INTO Form (ID_F, FormN) VALUES('"+NewIDForm+"', '"+ NameQuest.Text+"');";
            ConnectDB(CommandText); // занесение новой анкеты
            for (int i = 0; i < DataMassive.Count; i++) // занесение вопросов и ответов
            {
                if (DataMassive[i]== "//Quest//")// ОТ
                {
                    int FinalQuestAdrr=0;
                    bool SwitchQuestAdd = true;
                    for (int s = i+1; s < DataMassive.Count; s++)
                    {
                        if (DataMassive[s] =="//ENDQ\\") // ДО
                        {
                            FinalQuestAdrr = s-1;
                            break;
                        }
                    }
                    for (int d = i+1; d < FinalQuestAdrr; d++) // МЕЖДУ
                    {
                        if (SwitchQuestAdd)
                        {
                            CommandText = "SELECT max(ID_Q) FROM Question; ";
                            GetNewID = true;
                            ConnectDB(CommandText);
                            NewIDQuest = NewID;
                            INFO.Clear();
                            CommandText = "INSERT INTO Question (ID_F, ID_Q, QuestN, NumVar) VALUES('" + NewIDForm + "', '" + NewIDQuest + "', '"+ DataMassive[d]+ "', '" + DataMassive[FinalQuestAdrr] + "');";
                            ConnectDB(CommandText);
                            SwitchQuestAdd = false;
                            d++;
                        }
                        CommandText = "SELECT max(ID_A) FROM Answear; ";
                        GetNewID = true;
                        ConnectDB(CommandText);;
                        INFO.Clear();
                        CommandText = "INSERT INTO Answear (ID_Q, ID_A, AnswN) VALUES('" + NewIDQuest + "', '" + NewID + "', '" + DataMassive[d] + "');";
                        ConnectDB(CommandText);
                    }
                    i = FinalQuestAdrr+1; 
                }                
            }
            NameQLabel.Opacity = 0.5;
            NameQ.IsEnabled = false;
            AddQ.Opacity = 0.3;
            AddQ.IsEnabled = false;
            AddQuestGD.Items.Clear();
            NameQuestLabel.Opacity = 0.9;           
            NameQuest.IsEnabled = true;
            EndQuest.Opacity = 0.3;
            EndQuest.IsEnabled = false;
            NameQuest.Focus();
            UpdateAll();
            CounterAnswear = 0;
            CounterQuestion = 0;
            AddQuestGD.Opacity = 0.5;
            AddQuestGD.IsEnabled = false;
            DataMassive.Clear();
            MessageBox.Show("Анкета '"+NameQuest.Text+"' успешно добавлена!", "Сообщение");
            NameQuest.Clear();
        }
        private void NameQ_KeyUp(object sender, KeyEventArgs e)
        {
            if (NameQ.Text!="")
            {
                if (e.Key == Key.Enter)
                {
                    AddQ_Click(this, new RoutedEventArgs());
                }
            }           
        }
        private void NameQuest_KeyUp(object sender, KeyEventArgs e)
        {
            if (NameQuest.Text!="")
            {
                if (e.Key == Key.Enter)
                {
                    NameQ.Focus();
                }
            }
        }
        private void NameA_KeyUp(object sender, KeyEventArgs e)
        {
            if (NameA.Text!="")
            {
                if (e.Key == Key.Enter)
                {
                    AddA_Click(this, new RoutedEventArgs());
                }
            }           
        }
        private void AddA_Click(object sender, RoutedEventArgs e) // выполнение функции добавления ответа на вопрос в создании анкеты
        {
            if (NameA.Text == "//Quest//" || NameA.Text == "//ENDQ\\")
            {
                MessageBox.Show("Данный ввод ответа недопустим", "Ошибка");
                return;
            }
            if (NameA.Text=="")
            {
                AddA.IsEnabled = false;
                NameA.Focus();
                return;
            }
            DataMassive.Add(NameA.Text);
            AddQuestGD.Items.Add(new Item() { QorA = "Ответ", Text = NameA.Text });           
            AddA.Opacity = 0.3;
            CounterAnswear++;            
            NumVar.Items.Clear();
            for (int i = 1; i <= CounterAnswear; i++)
            {
                NumVar.Items.Add(i);
            }
            if (CounterAnswear > 0 && NumVar.SelectedIndex!=-1)
            {
                EndA.Opacity = 0.5;
                EndA.IsEnabled = true;
            }
            if (NameA.Text=="Напишите свой ответ")
            {
                NumVar.SelectedIndex = 0;
                NumVar.Opacity = 0.5;
                NumVar.IsEnabled = false;
                NumVarLabel.Opacity = 0.5;
                CheckBlockNumVar = true;
            }
            else if(CheckBlockNumVar==false)
            {
                NumVar.Opacity = 0.9;
                NumVar.IsEnabled = true;
                NumVarLabel.Opacity = 0.9;
            }
            if (CheckBlockNumVar)
            {
                NumVar.SelectedIndex = 0;
            }
            NameA.Clear();
            NameA.Focus();
        }
    }
    public class Item // используется для заполнения таблиц
    {
        public string QorA { get; set; }
        public string Text { get; set; }
    }
    public class Item2 // используется для заполнения таблиц
    {        
        public string QuestN { get; set; }
    }
    public class Item3 // используется для заполнения таблиц
    {
        public string AnswN { get; set; }
    }
    public class DATA // позволяет получить путь к базе данныхиз главого окна при переходе
    {
        public static string FileDBWayFromMainWindow { get; set; }
    }
}
