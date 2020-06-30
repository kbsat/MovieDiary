using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovieDiary
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchMovieControl : UserControl
    {
        private MovieInfo movInfo;
        public MovieInfo movinfo // 밖으로 노출 시킬 property
        {
            get
            {
                return movInfo;
            }
            set
            {
                movInfo = value;
                titleBlock.Text = movInfo.Title;
                openingBlock.Text = movInfo.OpeningData;
                subtitleBlock.Text = movInfo.SubTitle;
                directorBlock.Text = "감독 : " + movInfo.DirectorName;
                actorBlock.Text = "배우 : " + movInfo.ActorName;

                if (!movInfo.imageUri.Equals("")) // 이미지 uri가 비어있지 않다면
                {
                    imageBox.Source = new BitmapImage(new Uri(movInfo.imageUri));
                }

            }
        }
        public SearchMovieControl()
        {
            InitializeComponent();
        }
    }
}




