﻿using System;
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
        /*UserControl1 use = new UserControl1();
        UserControl1 use2 = new UserControl1();
        UserControl1 use3 = new UserControl1();
        UserControl1 use4 = new UserControl1();*/
        UserControl1[] Users = new UserControl1[10];
        public SearchWindow()
        {
            InitializeComponent();
            /*
             * Contact contact = new Contact(); 
            contact.Title = "프리즌 이스케이프";
            contact.OpeningData = "2020.05.06";
            contact.Genre = "모험,스릴러";
            contact.DirectorName = "프란시스 아난";
            contact.ActorName = "다니엘 래드클리프";
            use.ContactData = contact;
            use2.ContactData = contact;

            myContact.Items.Add(use);
            myContact.Items.Add(use2);
            myContact.Items.Add(use3);
            myContact.Items.Add(use4);*/


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

                Example ex = (JsonConvert.DeserializeObject<Example>(text));


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
                        Contact cont = new Contact();
                        Users[i] = new UserControl1();
                        string title = ex.items[i].title.Replace("<b>", "");
                        title = title.Replace("</b>", "");
                        cont.Title = title;

                        string pubDate = ex.items[i].pubDate;
                        cont.OpeningData = pubDate;

                        string subtitle = ex.items[i].subtitle.Replace("<b>", "");
                        subtitle = subtitle.Replace("</b>", "");
                        cont.SubTitle = subtitle;

                        string directorName = ex.items[i].director;
                        cont.DirectorName = directorName;

                        string actorName = ex.items[i].actor;
                        cont.ActorName = actorName;

                        string imageUri = ex.items[i].image;
                        cont.imageUri = imageUri;

                        Users[i].ContactData = cont;
                        myContact.Items.Add(Users[i]);
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

        public class Example
        {
            public string lastBuildDate { get; set; }
            public int total { get; set; }
            public int start { get; set; }
            public int display { get; set; }
            public IList<Item> items { get; set; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UserControl1 selectedUser = (UserControl1)myContact.SelectedItem;
            Contact selectedContact = selectedUser.ContactData;

            string dbpath = @"Data Source=" + App.databasePath;
            using (SQLiteConnection conn = new SQLiteConnection(dbpath))
            {
                conn.Open();
                string sql = "INSERT INTO movies (Title,SubTitle,DirectorName,ActorName,ImageUri,OpeningData,Review,Star) " +
           "values ('" + selectedContact.Title + "','" + selectedContact.SubTitle + "','" + selectedContact.DirectorName + "','" + selectedContact.ActorName + "','" + selectedContact.imageUri + "'," +
           "'" + selectedContact.OpeningData + "','" + selectedContact.Review + "'," + selectedContact.Star + ")";
                SQLiteCommand com = new SQLiteCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }



            MessageBox.Show("추가완료");

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
