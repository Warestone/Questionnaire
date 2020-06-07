using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using Microsoft.Win32;
using System.IO;
using System.Threading;
namespace Questionnaire
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public List<String> INFO = new List<String>();
        public List<String> QuestionsL = new List<String>();
        public List<String> AnswersL = new List<String>();
        public SQLiteDataReader Reader;
        public string FileDBWay, CommandText;
        public bool GetSomeData = false;
        public bool CheckClosing = true;
        public bool GetNewID = false;
        public bool FiledConnect = false;
        public int NumQuestion =0, TotalQuestions=0, TotalSleepCycle =0, NumVar=0;        
        public User()
        {
            InitializeComponent();            
            CommandText = "SELECT FormN FROM Form"; 
            GetSomeData = true;
            ConnectDB(CommandText); // вызов функции подключения к БД
            int i = 0;
            while (INFO.Count>i) // заполнение выпадающего списка с анкетами
            {
                ListOfQuest.Items.Add(INFO[i]);
                i++;
            }
            INFO.Clear();
            CommandText = "SELECT GroupN FROM [Group]";
            GetSomeData = true;
            ConnectDB(CommandText);
            i = 0;
            while (INFO.Count > i) // заполнение выпадающего списка с группами
            {
                ListOfGroup.Items.Add(INFO[i]);
                i++;
            }
            INFO.Clear();
        }
        private void Back_MouseEnter(object sender, MouseEventArgs e) // события при наведении мыши на кнопки
        {
            Back.Opacity = 0.9;
        }
        private void Back_MouseLeave(object sender, MouseEventArgs e)
        {
            Back.Opacity = 0.5;
        }
        private void StartQuest_MouseEnter(object sender, MouseEventArgs e)
        {
            StartQuest.Opacity = 0.9;
        }
        private void StartQuest_MouseLeave(object sender, MouseEventArgs e)
        {
            StartQuest.Opacity = 0.5;
        }
        private void NextQuest_MouseEnter(object sender, MouseEventArgs e)
        {
            NextQuest.Opacity = 0.9;
        }
        private void NextQuest_MouseLeave(object sender, MouseEventArgs e)
        {
            NextQuest.Opacity = 0.5;
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
                Conn = new SQLiteConnection("Data Source='QDB.db'; Password='230SDa51';"); // создание подключения к БД в папке с исполняемым файлом
            }
            else if (File.Exists(FileDBWay) == true)
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
            catch (InvalidOperationException) // событие, если БД занята другим подключением
            {
                if (TotalSleepCycle>12) // циклы ожидания по 5 секунд == минута
                {
                    if (MessageBox.Show("Превышено ожидание доступа к базе данных 'QDB.db!\nПовторить попытку?'", "Ошибка",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                    {
                        TotalSleepCycle = 0;
                    }
                    else
                    {
                        TotalSleepCycle = 0;
                        FiledConnect = true;
                        return;
                    }                   
                }
                TotalSleepCycle++;
                Thread.Sleep(5000); // задержка выполнения н 5 секунд
                ConnectDB(CommandText); //повторное подключение
            }           
            if (GetSomeData) // получение данных из БД
            {
                try
                {
                    Reader = Command.ExecuteReader(); // создание "читающего" оператора
                    while (Reader.Read())
                    {
                        INFO.Add(Reader.GetValue(0).ToString()); // занесение данных в массив
                    }
                }
                catch (Exception){ }
                GetSomeData = false;
            }           
            Conn.Close(); // закрытие подключения
        }        
        private void Back_Click(object sender, RoutedEventArgs e) // кнопка возвраат на Главное окно
        {
            if (MessageBox.Show("Вы действительно хотите вернуться на экран выбора режима работы?\nНесохранённые данные могут быть утеряны!", "Требуется подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow DS = new MainWindow();
                DS.Show(); // открытие Главного окна
                CheckClosing = false;
                this.Close(); // закрытие текущего
            }
        }
        private void ListOfQuest_SelectionChanged(object sender, SelectionChangedEventArgs e) // событие при выборе анкет, отвечает за блокировку кнопки, если обязательные параметры не выбраны
        {
            if (ListOfGroup.SelectedIndex != -1 && ListOfQuest.SelectedIndex != -1 && CourseList.SelectedIndex != -1)
            {
                StartQuest.Opacity = 0.5;
                StartQuest.IsEnabled = true;
            }
            else
            {
                StartQuest.Opacity = 0.3;
                StartQuest.IsEnabled = false;
            }
        }
        private void ListOfGroup_SelectionChanged(object sender, SelectionChangedEventArgs e) // событие при выборе групп, отвечает за блокировку кнопки, если обязательные параметры не выбраны
        {
            if (ListOfGroup.SelectedIndex != -1 && ListOfQuest.SelectedIndex != -1 && CourseList.SelectedIndex!=-1)
            {
                StartQuest.Opacity = 0.5;
                StartQuest.IsEnabled = true;
            }
            else
            {
                StartQuest.Opacity = 0.3;
                StartQuest.IsEnabled = false;
            }
        }
        private void GetAllAnswers() // функция получения ответов на вопрос
        {
            Question.Text = "Вопрос: " + QuestionsL[NumQuestion]; // показ вопроса
            CommandText = "SELECT NumVar FROM Question WHERE QuestN ='" + QuestionsL[NumQuestion] + "';";
            GetSomeData = true;
            ConnectDB(CommandText);
            NumVar = Convert.ToInt32(INFO[0]);
            INFO.Clear();
            CommandText = "SELECT AnswN FROM Answear WHERE ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + QuestionsL[NumQuestion] + "');";
            GetSomeData = true;
            ConnectDB(CommandText); // вызов функци подключения к БД с командой на получение ответов
            if (FiledConnect) // проверка пройденных циклов подключения
            {
                FiledConnect = false;
                return;
            }
            Answers.Items.Clear();
            foreach (var Answear in INFO)
            {
                Answers.Items.Add(new TextA() { Text = Answear }); // занесение ответов в массив
            }
        }
        private void StartQuest_Click(object sender, RoutedEventArgs e) // кнопка начала и окончания прохождения анкет
        {
            if (StartQuest.Content.ToString()== "Начать прохождение анкеты")
            {
                CommandText = "SELECT QuestN FROM Question WHERE ID_F = (SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuest.SelectedItem.ToString() + "');";
                GetSomeData = true;
                ConnectDB(CommandText); // вызов функци подключения к БД с командой на получение вопросов
                if (FiledConnect)
                {
                    FiledConnect = false;
                    return;
                }
                NameQuestLabel.Opacity = 0.5; // блокировка/разблокировка объектов интерфейса
                ListOfQuest.IsEnabled = false;
                ListOfQuest.Opacity = 0.5;
                NameGroupLabel.Opacity = 0.5;
                ListOfGroup.IsEnabled = false;
                ListOfGroup.Opacity = 0.5;
                Question.Opacity=0.9;
                Question.IsEnabled = true;
                Answers.Opacity = 0.8;
                Answers.IsEnabled = true;
                CourseLabel.Opacity = 0.5;
                CourseList.Opacity = 0.5;
                CourseList.IsEnabled = false;
                StartQuest.Content = "Завершить прохождение анкеты";
                StartQuest.Opacity = 0.3;
                StartQuest.IsEnabled = false;                                
                foreach (var Question in INFO)
                {
                    QuestionsL.Add(Question); // занесение вопросов в массив
                }
                INFO.Clear();
                TotalQuestions = QuestionsL.Count(); // подсчёт количества вопросов
                GetAllAnswers();  // вызов функции получения ответов на вопрос           
                if (NumVar>1)
                {
                    Answers.SelectionMode = SelectionMode.Multiple;
                }
                else
                {
                    Answers.SelectionMode = SelectionMode.Single;
                }
            }
            else
            {
                if (AnswearText.Text == "//ENDQ\\")
                {
                    MessageBox.Show("Данный ввод недопустим!", "Ошибка");
                    AnswearText.Clear();
                    return;
                }
                List<int> Index = new List<int>();
                foreach (object item in Answers.SelectedItems)
                {
                    Index.Add(Answers.Items.IndexOf(item));
                }
                if (Index.Count > NumVar)
                {
                    MessageBox.Show("Нельзя выбрать больше " + NumVar + " ответов!", "Ошибка");
                    Answers.SelectedIndex = -1;
                    return;
                }
                AnswersL.Add(QuestionsL[NumQuestion]); // занесение финальных значений перед отправкой в БД
                int d = 0;
                while (Index.Count > d)
                {
                    AnswersL.Add(INFO[Index[d]]); // занесение выбранного ответа в массив
                    d++;
                }                
                if (INFO[Answers.SelectedIndex]== "Напишите свой ответ")
                {
                    AnswersL.Add(AnswearText.Text);
                }
                AnswersL.Add("//ENDQ\\");
                int Course = 0;
                switch (CourseList.SelectedIndex) // проверка выбранного курса
                {
                    case 0:
                        Course = 1;
                        break;
                    case 1:
                        Course = 2;
                        break;
                    case 2:
                        Course = 3;
                        break;
                    case 3:
                        Course = 4;
                        break;
                }
                int FinalAddr = 0;
                for (int i = 0; i < AnswersL.Count; i++) // цикл отправки данных в БД
                {
                    if (AnswersL[i + 1] == "Напишите свой ответ")
                    {
                        CommandText = "INSERT INTO TextAnswear (ID_Q, ID_A, TextTA) VALUES ((SELECT ID_Q FROM Question WHERE QuestN ='" + AnswersL[i] + "'), (SELECT ID_A FROM Answear WHERE AnswN ='" + AnswersL[i + 1] + "' AND ID_Q = (SELECT ID_Q FROM Question WHERE QuestN = '" + AnswersL[i] + "')), '" + AnswersL[i + 2] + "')";
                        ConnectDB(CommandText); // отправка ответов, введённых вручную, в БД
                        CommandText = "INSERT INTO MainTable (ID_F, ID_Q, ID_A, ID_TA, ID_G, Course, Specialization) VALUES ((SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuest.SelectedItem.ToString() + "'), (SELECT ID_Q FROM Question WHERE QuestN ='" + AnswersL[i] + "'), (SELECT ID_A FROM Answear WHERE AnswN ='" + AnswersL[i + 1] + "'), (SELECT ID_TA FROM TextAnswear WHERE TextTA = '" + AnswersL[i + 2] + "'), (SELECT ID_G FROM [Group] WHERE GroupN ='" + ListOfGroup.SelectedItem.ToString() + "'), '" + Course + "', (SELECT Specialization FROM [Group] WHERE GroupN ='" + ListOfGroup.SelectedItem.ToString() + "')); ";
                        ConnectDB(CommandText); // отправка выбранных ответов
                        i += 3;
                    }
                    else
                    {
                        for (int s = i; s < AnswersL.Count; s++)
                        {
                            if (AnswersL[s] == "//ENDQ\\")
                            {
                                FinalAddr = s;
                                break;
                            }
                        }
                        for (int q = i; q < FinalAddr-1; q++)
                        {
                            CommandText = "INSERT INTO MainTable (ID_F, ID_Q, ID_A, ID_TA, ID_G, Course, Specialization) VALUES ((SELECT ID_F FROM Form WHERE FormN ='" + ListOfQuest.SelectedItem.ToString() + "'), (SELECT ID_Q FROM Question WHERE QuestN ='" + AnswersL[i] + "'), (SELECT ID_A FROM Answear WHERE AnswN ='" + AnswersL[q + 1] + "'), '0', (SELECT ID_G FROM [Group] WHERE GroupN ='" + ListOfGroup.SelectedItem.ToString() + "'), '" + Course + "', (SELECT Specialization FROM [Group] WHERE GroupN ='" + ListOfGroup.SelectedItem.ToString() + "')); ";
                            ConnectDB(CommandText); // отправка выбранных ответов
                        }
                        i = FinalAddr;
                    }                                     
                }
                if (FiledConnect)
                {
                    FiledConnect = false;
                    MessageBox.Show("Результаты не были сохранены из-за превышеного ожидания доступа к базе данных 'QDB.db'!\n","Ошибка");
                    return;
                }
                INFO.Clear(); // откат объектов интерфейса окна в изначальное состояние
                QuestionsL.Clear();
                AnswersL.Clear();
                StartQuest.Content = "Начать прохождение анкеты";
                NextQuest.Opacity = 0.3;
                NextQuest.IsEnabled = false;
                Answers.Items.Clear();
                Question.Text = "Вопрос:";
                Question.Opacity = 0.3;
                Question.IsEnabled = false;
                NameQuestLabel.Opacity = 0.9;
                ListOfQuest.Opacity = 0.9;
                ListOfQuest.IsEnabled = true;
                NameGroupLabel.Opacity = 0.9;
                ListOfGroup.Opacity = 0.9;
                ListOfGroup.IsEnabled = true;
                Answers.Opacity = 0.5;
                Answers.IsEnabled = false;
                ListOfGroup.SelectedIndex = -1;
                ListOfQuest.SelectedIndex = -1;
                CourseList.SelectedIndex = -1;
                StartQuest.Opacity = 0.3;
                StartQuest.IsEnabled = false;
                CourseLabel.Opacity = 0.9;
                CourseList.Opacity = 0.9;
                CourseList.IsEnabled = true;
                AnswearText.Clear();
                AnswearText.IsEnabled = false;
                Answers.SelectionMode = SelectionMode.Single;
                AnswearTextLabel.Opacity = 0.5;
                NumQuestion = 0;
                TotalQuestions = 0;
                TotalSleepCycle = 0;                
                MessageBox.Show("Анкета успешно пройдена, результаты сохранены!", "Сообщение");
    }
        }
        private void Answers_SelectionChanged(object sender, SelectionChangedEventArgs e) // событие отвечающее за блокировку/разблокировку кнопок при выборе ответа
        {
            if (Answers.SelectedIndex!=-1 && NumQuestion+1!=TotalQuestions && INFO[Answers.SelectedIndex]!= "Напишите свой ответ")
            {
                NextQuest.Opacity = 0.5;
                NextQuest.IsEnabled = true;
            }
            else if(Answers.SelectedIndex != -1 && NumQuestion + 1 == TotalQuestions && INFO[Answers.SelectedIndex] != "Напишите свой ответ")
            {
                NextQuest.Opacity = 0.3;
                NextQuest.IsEnabled = false;
                StartQuest.Opacity = 0.5;
                StartQuest.IsEnabled = true;
            }
            else
            {
                NextQuest.Opacity = 0.3;
                NextQuest.IsEnabled = false;
            }
            if (Answers.SelectedIndex != -1 && NumQuestion + 1 != TotalQuestions && INFO[Answers.SelectedIndex] == "Напишите свой ответ")
            {
                AnswearTextLabel.Opacity = 0.9;
                AnswearText.IsEnabled = true;
                AnswearText.Focus();
            }
            else if(Answers.SelectedIndex != -1 && NumQuestion + 1 == TotalQuestions && INFO[Answers.SelectedIndex] == "Напишите свой ответ")
            {
                AnswearTextLabel.Opacity = 0.9;
                AnswearText.IsEnabled = true;
                AnswearText.Focus();
            }
            else
            {
                AnswearTextLabel.Opacity = 0.5;
                AnswearText.IsEnabled = false;
            }
        }
        private void AnswearText_TextChanged(object sender, TextChangedEventArgs e) // событие отвечающее за блокировку/разблокировку кнопок при вводе ответа вручную
        {
            if (AnswearText.Text!="" && Answers.SelectedIndex != -1 && NumQuestion + 1 != TotalQuestions)
            {
                NextQuest.Opacity = 0.5;
                NextQuest.IsEnabled = true;
            }
            else if (Answers.SelectedIndex != -1 && AnswearText.Text != "" && NumQuestion + 1 == TotalQuestions)
            {
                NextQuest.Opacity = 0.3;
                NextQuest.IsEnabled = false;
                StartQuest.Opacity = 0.5;
                StartQuest.IsEnabled = true;
            }
            else
            {
                NextQuest.Opacity = 0.3;
                NextQuest.IsEnabled = false;
            }
        }
        private void AnswearText_KeyUp(object sender, KeyEventArgs e) // выполение функции перехода к следующему вопросу по нажатию Enter
        {
            if (e.Key == Key.Enter && AnswearText.Text!="" && NumQuestion + 1 != TotalQuestions && Answers.SelectedIndex != -1)
            {
                NextQuest_Click(this, new RoutedEventArgs());
            }
            else if(e.Key == Key.Enter && AnswearText.Text != "" && NumQuestion + 1 == TotalQuestions && Answers.SelectedIndex != -1)
            {
                StartQuest_Click(this, new RoutedEventArgs());
            }
        }
        private void Answers_KeyUp(object sender, KeyEventArgs e) // выполение функции перехода к следующему вопросу по нажатию Enter
        {
            if (e.Key==Key.Enter && NumQuestion+1==TotalQuestions && Answers.SelectedIndex!=-1 && INFO[Answers.SelectedIndex] != "Напишите свой ответ")
            {
                StartQuest_Click(this, new RoutedEventArgs());
            }
            else if(e.Key == Key.Enter && NumQuestion + 1 != TotalQuestions && Answers.SelectedIndex != -1 && INFO[Answers.SelectedIndex] != "Напишите свой ответ")
            {
                NextQuest_Click(this, new RoutedEventArgs());
            }           
        }
        private void CourseList_SelectionChanged(object sender, SelectionChangedEventArgs e) // событие при выборе анкет, отвечает за блокировку кнопки, если обязательные параметры не выбраны
        {
            if (ListOfGroup.SelectedIndex != -1 && ListOfQuest.SelectedIndex != -1 && CourseList.SelectedIndex != -1)
            {
                StartQuest.Opacity = 0.5;
                StartQuest.IsEnabled = true;
            }
            else
            {
                StartQuest.Opacity = 0.3;
                StartQuest.IsEnabled = false;
            }
        }
        private void NextQuest_Click(object sender, RoutedEventArgs e) // переход к следующему вопросу
        {
            if (AnswearText.Text== "//ENDQ\\")
            {
                MessageBox.Show("Данный ввод недопустим!","Ошибка");
                AnswearText.Clear();
                return;
            }
            List<int> Index = new List<int>();
            foreach (object item in Answers.SelectedItems)
            {
                Index.Add(Answers.Items.IndexOf(item));
            }
            if (Index.Count>NumVar)
            {
                MessageBox.Show("Нельзя выбрать больше "+NumVar+" ответов!","Ошибка");
                Answers.SelectedIndex = -1;
                return;
            }
            Question.Text = "Вопрос: " + QuestionsL[NumQuestion]; // новый вопрос
            AnswersL.Add(QuestionsL[NumQuestion]); // занесение вопроса в массив 
            int i = 0;
            while (Index.Count>i)
            {
                AnswersL.Add(INFO[Index[i]]); // занесение выбранного ответа в массив
                i++;
            }            
            if (INFO[Answers.SelectedIndex]== "Напишите свой ответ")
            {
                AnswersL.Add(AnswearText.Text);
                AnswearText.Clear();
            }
            AnswersL.Add("//ENDQ\\");
            NumQuestion++;
            INFO.Clear();
            GetAllAnswers();
            if (NumVar>1)
            {
                Answers.SelectionMode = SelectionMode.Multiple;
            }
            else
            {
                Answers.SelectionMode = SelectionMode.Single;
            }
            NextQuest.Opacity = 0.3;
            NextQuest.IsEnabled = false;
        }
        public class TextA // класс для занесения ответов в поле с ответами
        {
            public String Text { get; set; }
        }       
    }
}
