using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Data.SQLite;

namespace MovieDiary
{
    /// <summary>
    /// ReviewWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReviewWindow : Window
    {
        int star_num=1;
        MovieInfo nowMovie;
        BitmapImage star = new BitmapImage(new Uri("star.png", UriKind.Relative));
        BitmapImage unstar = new BitmapImage(new Uri("unstar.png", UriKind.Relative));
        public ReviewWindow(MovieInfo movInfo)
        {
            nowMovie = movInfo;

            InitializeComponent();

            // 선택한 파일의 정보로 화면구성
            if (!nowMovie.imageUri.Equals("")) // 이미지 uri가 비어있지 않다면
            {
                poster.Source = new BitmapImage(new Uri(nowMovie.imageUri));

            }
            title.Content = nowMovie.Title;
            subtitle.Content = nowMovie.SubTitle;
            opening_data.Content = nowMovie.OpeningData;
            director_name.Content = nowMovie.DirectorName;
            actor_name.Content = nowMovie.ActorName;
            star_num = nowMovie.Star;
            reviewBox.Text = nowMovie.Review;
            StarInit();
        }

        private void star_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (sender.Equals(star_1))
            {
                star_1.Source = star;
                star_2.Source = unstar;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;
                star_num = 1;
                
            }
            else if (sender.Equals(star_2))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;
                star_num = 2;

            }
            else if (sender.Equals(star_3))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = unstar;
                star_5.Source = unstar;
                star_num = 3;
            }
            else if(sender.Equals(star_4))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = star;
                star_5.Source = unstar;
                star_num = 4;
            }
            else if(sender.Equals(star_5))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = star;
                star_5.Source = star;
                star_num = 5;
            }
            else
            {

            }
        }

        private void StarInit()
        {
            if (star_num == 1)
            {
                star_1.Source = star;
                star_2.Source = unstar;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;
            }
            else if (star_num == 2)
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;

            }
            else if (star_num == 3)
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = unstar;
                star_5.Source = unstar;
            }
            else if (star_num == 4)
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = star;
                star_5.Source = unstar;
            }
            else if (star_num == 5)
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = star;
                star_5.Source = star;
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteAlert deleteAlert = new DeleteAlert();
            deleteAlert.ShowDialog();
            if(deleteAlert.isDelete == true)
            {
                
                string dbpath = @"Data Source=" + App.databasePath;
                using (SQLiteConnection conn = new SQLiteConnection(dbpath))
                {
                    conn.Open();
                    string sql = "DELETE FROM movies WHERE Title='"+nowMovie.Title+"'";
                    SQLiteCommand com = new SQLiteCommand(sql, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
                MessageBox.Show("삭제 완료");
                Window.GetWindow(this).Close();
                ((MainWindow)System.Windows.Application.Current.MainWindow).MovieGrid.Children.Clear();
                ((MainWindow)System.Windows.Application.Current.MainWindow).ReadTable();
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string dbpath = @"Data Source=" + App.databasePath;
            using (SQLiteConnection conn = new SQLiteConnection(dbpath))
            {
                conn.Open();
                string sql = "UPDATE movies SET Review ='" + reviewBox.Text + "',Star=" + star_num + " WHERE Title='" + nowMovie.Title + "'";
                SQLiteCommand com = new SQLiteCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("저장 완료!");
            Window.GetWindow(this).Close();
        }
    }
}
