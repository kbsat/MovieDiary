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
    public partial class SearchMovieControl : UserControl
    {
        private MovieInfo contactdata;
        public MovieInfo ContactData // 밖으로 노출 시킬 property
        {
            get
            {
                return contactdata;
            }
            set
            {
                contactdata = value;
                titleBlock.Text = contactdata.Title;
                openingBlock.Text = contactdata.OpeningData;
                subtitleBlock.Text = contactdata.SubTitle;
                directorBlock.Text = "감독 : " + contactdata.DirectorName;
                actorBlock.Text = "배우 : " + contactdata.ActorName;

                if (!contactdata.imageUri.Equals("")) // 이미지 uri가 비어있지 않다면
                {
                    imageBox.Source = new BitmapImage(new Uri(contactdata.imageUri));
                }

            }
        }
        public SearchMovieControl()
        {
            InitializeComponent();
        }
    }
}




