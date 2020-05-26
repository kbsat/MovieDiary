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
    /// SearchWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
            Contact contact = new Contact(); 
            contact.Title = "프리즌 이스케이프";
            contact.OpeningData = "2020.05.06";
            contact.Genre = "모험,스릴러";
            contact.DirectorName = "프란시스 아난";
            contact.DirectorName = "다니엘 래드클리프";

            myContact.ContactData = contact;




        }

    }
}
