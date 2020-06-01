using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieDiary
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public int i;
        public SQLiteConnection conn=null;
        public MainWindow()
        {
            InitializeComponent();
            InitDB();
            ReadTable();


        }
        public void InitDB()
        {
            string dbpath;
            if (!System.IO.File.Exists(App.databasePath))
            {
                SQLiteConnection.CreateFile(App.databasePath);
                dbpath = @"Data Source=" + App.databasePath;
                conn = new SQLiteConnection(dbpath);
                conn.Open();
                CreateTable();
            }
            else
            {
                dbpath = @"Data Source=" + App.databasePath;
                conn = new SQLiteConnection(dbpath);
                conn.Open();
            }

        }
        public void CreateTable()
        {
            string sql = "CREATE TABLE movies (Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                " Title string, SubTitle string, DirectorName string, ActorName string,"
                + "ImageUri string, OpeningData string, Review string, Star Integer)";

            SQLiteCommand com = new SQLiteCommand(sql, conn);
            com.ExecuteNonQuery();
        }

        public void ReadTable()
        {

            string sql = "select * from movies";

            SQLiteCommand com = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = com.ExecuteReader();

            for (i = 0; i < 9; i++)
            {
                if (rdr.Read())
                {
                    MyMovieControl mymov = new MyMovieControl();
                    Contact c = new Contact();
                    c.Title = rdr["Title"].ToString();
                    c.ActorName = rdr["ActorName"].ToString();
                    c.OpeningData = rdr["openingData"].ToString();
                    c.DirectorName = rdr["directorName"].ToString();
                    c.SubTitle = rdr["subtitle"].ToString();
                    c.imageUri = rdr["ImageUri"].ToString();
                    c.Review = rdr["Review"].ToString();
                    c.Star = Convert.ToInt32(rdr["Star"].ToString());
                    mymov.MovieInfo = c;

                    MovieGrid.Children.Add(mymov);
                    Grid.SetRow(mymov, i / 3);
                    Grid.SetColumn(mymov, i % 3);
                }
                else
                {
                    break;
                }
            }
            rdr.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow();
            win.ShowDialog();
            ReadTable();

        }

        private void PrevButton_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void NextButton_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
