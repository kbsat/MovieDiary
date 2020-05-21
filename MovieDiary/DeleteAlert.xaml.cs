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
    /// DeleteAlert.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeleteAlert : Window
    {
        public bool isDelete = false;
        public DeleteAlert()
        {
            InitializeComponent();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            isDelete = false;
            this.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            isDelete = true;
            this.Close();
        }
    }
}
