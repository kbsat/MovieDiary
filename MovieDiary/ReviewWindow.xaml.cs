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
using System.Windows.Shapes;

namespace MovieDiary
{
    /// <summary>
    /// ReviewWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReviewWindow : Window
    {
        int star_num;
        public ReviewWindow()
        {
            InitializeComponent();
        }

        private void star_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage star = new BitmapImage(new Uri("star.png", UriKind.Relative));
            BitmapImage unstar = new BitmapImage(new Uri("unstar.png", UriKind.Relative));
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

        private void star_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BitmapImage star = new BitmapImage(new Uri("star.png", UriKind.Relative));
            BitmapImage unstar = new BitmapImage(new Uri("unstar.png", UriKind.Relative));
            if (sender.Equals(star_1))
            {
                star_1.Source = star;
                star_2.Source = unstar;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;
            }
            else if (sender.Equals(star_2))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = unstar;
                star_4.Source = unstar;
                star_5.Source = unstar;

            }
            else if (sender.Equals(star_3))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = unstar;
                star_5.Source = unstar;
            }
            else if (sender.Equals(star_4))
            {
                star_1.Source = star;
                star_2.Source = star;
                star_3.Source = star;
                star_4.Source = star;
                star_5.Source = unstar;

            }
            else if (sender.Equals(star_5))
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
            DeleteAlert dw = new DeleteAlert();
            dw.ShowDialog();
            if(dw.isDelete == true)
            {
                MessageBox.Show("삭제버튼클릭!");
            }
            else
            {
                
            }

        }
    }
}
