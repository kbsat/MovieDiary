using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Data.SQLite;

namespace MovieDiary
{
    /// <summary>
    /// SearchWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchWindow : Window
    {
        SearchMovieControl[] Movies = new SearchMovieControl[10];
        public SearchWindow()
        {
            InitializeComponent();


        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (myContact.Items.Count != 0)
            {
                myContact.Items.Clear();
            }
            string search_name = Movie_searchName.Text;

            string query = search_name; // 검색할 문자열
            string url = "https://openapi.naver.com/v1/search/movie?query=" + query; // 결과가 JSON 포맷

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", "Sz1m6GDnrPLbr52KCvhR"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "nsraifwQXD");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();

                MovieItems ex = (JsonConvert.DeserializeObject<MovieItems>(text));


                if (ex.items.Count >= 1) // 만약 입력값이 있을 시
                {
                    int repeatNum;
                    if (ex.items.Count >= 10) // 영화 검색출력값이 10개 이상인 경우
                    {
                        repeatNum = 10; // 10개만 출력
                    }
                    else
                    {
                        repeatNum = ex.items.Count; // 10개 이하인 경우 개수만큼 출력
                    }

                    for (int i = 0; i < repeatNum; i++)
                    {
                        MovieInfo movInfo = new MovieInfo();
                        Movies[i] = new SearchMovieControl();
                        string title = ex.items[i].title.Replace("<b>", "");
                        title = title.Replace("</b>", "");
                        movInfo.Title = title;

                        string pubDate = ex.items[i].pubDate;
                        movInfo.OpeningData = pubDate;

                        string subtitle = ex.items[i].subtitle.Replace("<b>", "");
                        subtitle = subtitle.Replace("</b>", "");
                        movInfo.SubTitle = subtitle;

                        string directorName = ex.items[i].director;
                        movInfo.DirectorName = directorName;

                        string actorName = ex.items[i].actor;
                        movInfo.ActorName = actorName;

                        string imageUri = ex.items[i].image;
                        movInfo.imageUri = imageUri;

                        Movies[i].movinfo = movInfo;
                        myContact.Items.Add(Movies[i]);
                    }

                    myContact.ScrollIntoView(myContact.Items[0]); // 맨위로 스크롤 옮긴다
                }


            }

        }
        public class Item
        {
            public string title { get; set; }
            public string link { get; set; }
            public string image { get; set; }
            public string subtitle { get; set; }
            public string pubDate { get; set; }
            public string director { get; set; }
            public string actor { get; set; }
            public string userRating { get; set; }
        }

        public class MovieItems
        {
            public string lastBuildDate { get; set; }
            public int total { get; set; }
            public int start { get; set; }
            public int display { get; set; }
            public IList<Item> items { get; set; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SearchMovieControl selectedUser = (SearchMovieControl)myContact.SelectedItem;
            MovieInfo selectedMovie = selectedUser.movinfo;

            string dbpath = @"Data Source=" + App.databasePath;
            using (SQLiteConnection conn = new SQLiteConnection(dbpath))
            {
                conn.Open();
                string sql = "INSERT INTO movies (Title,SubTitle,DirectorName,ActorName,ImageUri,OpeningData,Review,Star) " +
           "values ('" + selectedMovie.Title + "','" + selectedMovie.SubTitle + "','" + selectedMovie.DirectorName + "','" 
           + selectedMovie.ActorName + "','" + selectedMovie.imageUri + "'," +
           "'" + selectedMovie.OpeningData + "','" + selectedMovie.Review + "'," + selectedMovie.Star + ")";
                SQLiteCommand com = new SQLiteCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }


            MessageBox.Show("추가완료");
            ((MainWindow)System.Windows.Application.Current.MainWindow).MovieGrid.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ReadTable();

        }

        private void Movie_searchName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Enter))
            {
                SearchButton_Click(sender, e);
            }
        }
    }
}
