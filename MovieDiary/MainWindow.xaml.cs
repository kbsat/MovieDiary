using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieDiary
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MyMovieControl> mymovList = new List<MyMovieControl>();
        MyMovieControl[] mymovArray;
        int page = 0; // 현재 페이지의 위치를 나타냄
        public SQLiteConnection conn=null;
        public MainWindow()
        {
            InitializeComponent();
            InitDB(); // 처음열었을 때는 DB생성, 테이블생성 이미 생성되있을 때는 그냥 connection만 연결
            ReadTable();


        }
        public void InitDB()
        {
            string dbpath;
            if (!System.IO.File.Exists(App.databasePath)) // 디비파일이 없었을때
            {
                SQLiteConnection.CreateFile(App.databasePath);
                dbpath = @"Data Source=" + App.databasePath;
                conn = new SQLiteConnection(dbpath);
                conn.Open();
                CreateTable(); // 테이블이 생성
            }
            else
            {
                dbpath = @"Data Source=" + App.databasePath;
                conn = new SQLiteConnection(dbpath);
                conn.Open();
            }

        }
        public void CreateTable() // 디비파일이 존재하지 않았을 때 테이블 생성해주는 메소드
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
            
            string sql = "select * from movies";

            com = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = com.ExecuteReader();
            mymovList.Clear();

            while (rdr.Read())
            {
                MyMovieControl mymov = new MyMovieControl();
                MovieInfo nowMoive = new MovieInfo();
                nowMoive.Title = rdr["Title"].ToString();
                nowMoive.ActorName = rdr["ActorName"].ToString();
                nowMoive.OpeningData = rdr["openingData"].ToString();
                nowMoive.DirectorName = rdr["directorName"].ToString();
                nowMoive.SubTitle = rdr["subtitle"].ToString();
                nowMoive.imageUri = rdr["ImageUri"].ToString();
                nowMoive.Review = rdr["Review"].ToString();
                nowMoive.Star = Convert.ToInt32(rdr["Star"].ToString());
                mymov.movieInfo = nowMoive;

                mymovList.Add(mymov);
            }

            mymovArray = mymovList.ToArray(); // 인덱스 단위로 호출하기 위함
            for (int i = 0; i < 9; i++)
            {
                if(mymovArray.Length > i)
                {
                    MovieGrid.Children.Add(mymovArray[i]);
                    Grid.SetRow(mymovArray[i], i / 3);
                    Grid.SetColumn(mymovArray[i], i % 3);
                }
                else
                {
                    break;
                }
                
            }
            rdr.Close();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow();
            win.ShowDialog();

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
            if (mymovArray.Length - page * 9 > 9) 
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
