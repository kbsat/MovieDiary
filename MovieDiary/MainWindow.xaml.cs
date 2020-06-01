using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            /*db에서 받고 받은 데이터로 항목 구성. 메소드화 필요*/
            MyMovieControl mymov = new MyMovieControl();
            Contact c = new Contact();
            c.Title = "다크나이트";
            c.ActorName = "니콜라스 케이지";
            c.DirectorName = "홍길동";
            c.imageUri = "https://upload.wikimedia.org/wikipedia/ko/thumb/0/00/%EB%8B%A4%ED%81%AC_%EB%82%98%EC%9D%B4%ED%8A%B8_%ED%8F%AC%EC%8A%A4%ED%84%B0.jpg/220px-%EB%8B%A4%ED%81%AC_%EB%82%98%EC%9D%B4%ED%8A%B8_%ED%8F%AC%EC%8A%A4%ED%84%B0.jpg";
            c.OpeningData = "2020";
            c.Star = 3;
            mymov.MovieInfo = c;

            MovieGrid.Children.Add(mymov);
            Grid.SetRow(mymov, 0);
            Grid.SetColumn(mymov, 0);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow win = new SearchWindow();
            win.ShowDialog();
            
        }

        
    }
}
