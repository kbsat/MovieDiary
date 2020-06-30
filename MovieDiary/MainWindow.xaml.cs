﻿using System;
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
        int page = 0;
        public SQLiteConnection conn=null;
        public int RowCount = 0; // DB에 저장된 ROW의 개수
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
            com.CommandText = "select count(id) from movies";
            // DB의 열의 개수( 저장된 영화의 수 )를 알아냄 -> 페이징 위함
            RowCount = Convert.ToInt32(com.ExecuteScalar());


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
                for (int i = (page * 9); i < (page * 9 + 9); i++) // 18번째영화부터 26번째 영화까지 
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
