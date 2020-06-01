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
    /// MyMovieControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyMovieControl : UserControl
    {
        private Contact movInfo;

        public Contact MovieInfo // 밖으로 노출 시킬 property

        {

            get

            {
                return movInfo;
            }

            set

            {
                movInfo = value;
                if (!movInfo.imageUri.Equals("")) // 이미지 uri가 비어있지 않다면
                {
                    moviePoster.Source = new BitmapImage(new Uri(movInfo.imageUri));
                   
                }
                movieName.Content = movInfo.Title;

            }

        }

        public MyMovieControl()

        {

            InitializeComponent();

        }

        private void moviePoster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReviewWindow win = new ReviewWindow(movInfo);
            win.ShowDialog();
            ((MainWindow)System.Windows.Application.Current.MainWindow).MovieGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ReadTable();

        }
    }

}


