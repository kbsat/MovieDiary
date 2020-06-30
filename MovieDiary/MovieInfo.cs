using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiary
{
    public class MovieInfo // 영화의 정보를 담는 클래스
    {
        public string Title { get; set; }
        public string OpeningData { get; set; }
        public string SubTitle { get; set; }
        public string DirectorName { get; set; }
        public string ActorName { get; set; }
        public string imageUri { get; set; }
        public string Review { get; set; }
        public int Star { get; set; }
    }
}
