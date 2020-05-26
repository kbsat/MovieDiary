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
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private Contact contactdata;
        public Contact ContactData // 밖으로 노출 시킬 property
        {
            get
            {
                return ContactData;
            }
            set
            {
                contactdata = value;
                titleBlock.Text = contactdata.Title;
                openingBlock.Text = contactdata.OpeningData;
                genreBlock.Text = contactdata.Genre;
                directorBlock.Text = contactdata.DirectorName;
                actorBlock.Text = contactdata.ActorName;
            }
        }
        public UserControl1()
        {
            InitializeComponent();
        }
    }
    public class Contact
    {
        public string Title { get; set; }
        public string OpeningData { get; set; }
        public string Genre { get; set; }
        public string DirectorName { get; set; }
        public string ActorName { get; set; }
    }
}




