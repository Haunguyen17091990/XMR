using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Research.Models
{
    public class NewsReponse
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public int? TypeID { get; set; }
        public int? MenuID { get; set; }
        public int? SourceID { get; set; }
        public string ImageTH { get; set; }
        public string ImageFull { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public int? ViewNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
