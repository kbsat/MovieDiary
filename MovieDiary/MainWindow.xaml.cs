using System;
using System.Collections;
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
        public List<MyMovieControl> mymovList = new List<MyMovieControl>();
        MyMovieControl[] mymovArray;
        int page = 0;
        public SQLiteConnection conn=null;
        public int RowCount = 0;
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
            string sql = "CREATE TABLE movies (Id INTEGER PRIMARY KEY," +
                " Title string, SubTitle string, DirectorName string, ActorName string,"
                + "ImageUri string, OpeningData string, Review string, Star Integer)";

            SQLiteCommand com = new SQLiteCommand(sql, conn);
            com.ExecuteNonQuery();
        }

        public void ReadTable()
        {

            page = 0;
            SQLiteCommand com = new SQLiteCommand(conn);
            com.CommandText = "select count(id) from movies";
            // 열의 개수를 알아냄 -> 페이징 위함
            RowCount = Convert.ToInt32(com.ExecuteScalar());


            string sql = "select * from movies";

            com = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = com.ExecuteReader();
            mymovList.Clear();

            while (rdr.Read())
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

                mymovList.Add(mymov);
            }

            mymovArray = mymovList.ToArray();

            for (int i = 0; i < 9; i++)
            {
                if(mymovArray.Length > i)
                {
                    MovieGrid.Children.Add(mymovArray[i]);
                    Grid.SetRow(mymovArray[i], i / 3);
                    Grid.SetColumn(mymovArray[i], i % 3);
                }
                
            }
            rdr.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow();
            win.ShowDialog();
            MovieGrid.Children.Clear();
            ReadTable();

        }

        private void PrevButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (page >= 1)
            {
                page -= 1;
                MovieGrid.Children.Clear();

                for (int i = (page * 9); i < (page * 9 + 9); i++)
                {
                    if (i >= mymovArray.Length)
                    {
                        break;
                    }
                    MovieGrid.Children.Add(mymovArray[i]);
                    Grid.SetRow(mymovArray[i], (i % 9) / 3);
                    Grid.SetColumn(mymovArray[i], (i % 9) % 3);

                }
            }
            
        }

        private void NextButton_Click(object sender, MouseButtonEventArgs e)
        {
            if(mymovArray.Length - page*9 > 9)
            {
                page += 1;
                MovieGrid.Children.Clear();
                for (int i = (page * 9); i < (page * 9 + 9); i++)
                {
                    if (i >= mymovArray.Length)
                    {
                        break;
                    }
                    MovieGrid.Children.Add(mymovArray[i]);
                    Grid.SetRow(mymovArray[i], (i % 9) / 3);
                    Grid.SetColumn(mymovArray[i], (i % 9) % 3);
                }
            

            }
        }
    }
}
