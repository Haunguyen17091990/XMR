using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResearchWeb.Models
{
    public class NewsRes
    {
        private DBContext context;
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
        public string TypeName
        {
            #region id,name danh mục
            /// <summary>
            /// 1: thời sự
            /// 2: kinh doanh
            /// 3: quốc tế
            /// 4: quân sự
            /// 5: thể thao
            /// 6: cư dân mạng
            /// 7: giải trí
            /// 8: pháp luật
            /// 9: sức khỏe
            /// 10: đời sống 
            /// 11: khám phá
            /// </summary>
            #endregion
            get
            {
                if (TypeID != null)
                {
                    if (TypeID.ToString().Trim().Equals("1"))
                        return "thoi-su";
                    else if (TypeID.ToString().Trim().Equals("2"))
                        return "kinh-doanh";
                    else if (TypeID.ToString().Trim().Equals("3"))
                        return "quoc-te";
                    else if (TypeID.ToString().Trim().Equals("4"))
                        return "quan-su";
                    else if (TypeID.ToString().Trim().Equals("6"))
                        return "cu-dan-mang";
                    else if (TypeID.ToString().Trim().Equals("7"))
                        return "giai-tri";
                    else if (TypeID.ToString().Trim().Equals("8"))
                        return "phap-luat";
                    else if (TypeID.ToString().Trim().Equals("9"))
                        return "suc-khoe";
                    else if (TypeID.ToString().Trim().Equals("10"))
                        return "doi-song";
                    else if (TypeID.ToString().Trim().Equals("11"))
                        return "kham-pha";
                }
                return null;
            }

        }
        public string GetFriendlyTitle(bool remapToAscii = false, int maxlength = 80)
        {
            Title = FilterVietkey(Title);
            if (Title == null)
            {
                return string.Empty;
            }

            int length = Title.Length;
            bool prevdash = false;
            StringBuilder stringBuilder = new StringBuilder(length);
            char c;

            for (int i = 0; i < length; ++i)
            {
                c = Title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    stringBuilder.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lower-case
                    stringBuilder.Append((char)(c | 32));
                    prevdash = false;
                }
                else if ((c == ' ') || (c == ',') || (c == '.') || (c == '/') ||
                    (c == '\\') || (c == '-') || (c == '_') || (c == '='))
                {
                    if (!prevdash && (stringBuilder.Length > 0))
                    {
                        stringBuilder.Append('-');
                        prevdash = true;
                    }
                }
                else if (c >= 128)
                {
                    int previousLength = stringBuilder.Length;

                    if (remapToAscii)
                    {
                        stringBuilder.Append(RemapInternationalCharToAscii(c));
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }

                    if (previousLength != stringBuilder.Length)
                    {
                        prevdash = false;
                    }
                }

                if (i == maxlength)
                {
                    break;
                }
            }

            if (prevdash)
            {
                return stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
            }
            else
            {
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Remaps the international character to their equivalent ASCII characters. See
        /// http://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
        /// </summary>
        /// <param name="character">The character to remap to its ASCII equivalent.</param>
        /// <returns>The remapped character</returns>
        private string RemapInternationalCharToAscii(char character)
        {
            string s = character.ToString().ToLowerInvariant();
            if ("àåáâäãåąā".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (character == 'ř')
            {
                return "r";
            }
            else if (character == 'ł')
            {
                return "l";
            }
            else if (character == 'đ')
            {
                return "d";
            }
            else if (character == 'ß')
            {
                return "ss";
            }
            else if (character == 'Þ')
            {
                return "th";
            }
            else if (character == 'ĥ')
            {
                return "h";
            }
            else if (character == 'ĵ')
            {
                return "j";
            }
            else
            {
                return string.Empty;
            }
        }

        public string FilterVietkey(string strSource)
        {
            strSource = ConvertISOToUnicode(strSource);
            string result;
            if (strSource.Trim().Length == 0)
            {
                result = "";
            }
            else
            {
                string text = "á à ả ã ạ Á À Ả Ã Ạ ă ắ ằ ẳ ẵ ặ Ă Ắ Ằ Ẳ Ẵ Ặ â ấ ầ ẩ ẫ ậ Â Ấ Ầ Ẩ Ẫ Ậ đ Đ é è ẻ ẽ ẹ É È Ẻ Ẽ Ẹ ê ế ề ể ễ ệ Ê Ế Ề Ể Ễ Ệ í ì ỉ ĩ ị Í Ì Ỉ Ĩ Ị ó ò ỏ õ ọ Ó Ò Ỏ Õ Ọ ô ố ồ ổ ỗ ộ Ô Ố Ồ Ổ Ỗ Ộ ơ ớ ờ ở ỡ ợ Ơ Ớ Ờ Ở Ỡ Ợ ú ù ủ ũ ụ Ú Ù Ủ Ũ Ụ ư ứ ừ ử ữ ự Ư Ứ Ừ Ử Ữ Ự ý ỳ ỷ ỹ ỵ Ý Ỳ Ỷ Ỹ Ỵ";
                string text2 = "a a a a a A A A A A a a a a a a A A A A A A a a a a a a A A A A A A d d e e e e e E E E E E e e e e e e E E E E E E i i i i i I I I I I o o o o o O O O O O o o o o o o O O O O O O o o o o o o O O O O O O u u u u u U U U U U u u u u u u U U U U U U y y y y y Y Y Y Y Y";
                string[] array = text.Split(" ".ToCharArray());
                string[] array2 = text2.Split(" ".ToCharArray());
                string text3 = strSource;
                for (int i = 0; i < array.Length; i++)
                {
                    text3 = text3.Replace(array[i], array2[i]);
                }
                text = "À Á Â Ã Ä Å Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö Ø Ù Ú Û Ü Ý Þ ß à á â ã ä å æ ç è é ê ë ì í î ï ð ñ ò ó ô õ ö ø ù ú û ü ý þ ÿ";
                text2 = "A A A A A A Æ Ç E E E E I I I I D N O O O O O Ø U U U U Y Þ ß a a a a a a æ ç e e e e i i i i ð n o o o o o ø u u u u y þ y";
                string[] array3 = text.Split(" ".ToCharArray());
                string[] array4 = text2.Split(" ".ToCharArray());
                for (int i = 0; i < array3.Length; i++)
                {
                    text3 = text3.Replace(array3[i], array4[i]);
                }
                text3 = text3.Replace("\0", "");
                result = text3;
            }
            return result;
        }

        public string ConvertISOToUnicode(string strSource)
        {
            string text = "á à ả ã ạ Á À Ả Ã Ạ ă ắ ằ ẳ ẵ ặ Ă Ắ Ằ Ẳ Ẵ Ặ â ấ ầ ẩ ẫ ậ Â Ấ Ầ Ẩ Ẫ Ậ đ Đ é è ẻ ẽ ẹ É È Ẻ Ẽ Ẹ ê ế ề ể ễ ệ Ê Ế Ề Ể Ễ Ệ í ì ỉ ĩ ị Í Ì Ỉ Ĩ Ị ó ò ỏ õ ọ Ó Ò Ỏ Õ Ọ ô ố ồ ổ ỗ ộ Ô Ố Ồ Ổ Ỗ Ộ ơ ớ ờ ở ỡ ợ Ơ Ớ Ờ Ở Ỡ Ợ ú ù ủ ũ ụ Ú Ù Ủ Ũ Ụ ư ứ ừ ử ữ ự Ư Ứ Ừ Ử Ữ Ự ý ỳ ỷ ỹ ỵ Ý Ỳ Ỷ Ỹ Ỵ";
            string text2 = "á à &#7843; ã &#7841; Á À &#7842; Ã &#7840; &#259; &#7855; &#7857; &#7859; &#7861; &#7863; &#258; &#7854; &#7856; &#7858; &#7860; &#7862; â &#7845; &#7847; &#7849; &#7851; &#7853; Â &#7844; &#7846; &#7848; &#7850; &#7852; &#273; &#272; é è &#7867; &#7869; &#7865; É È &#7866; &#7868; &#7864; ê &#7871; &#7873; &#7875; &#7877; &#7879; Ê &#7870; &#7872; &#7874; &#7876; &#7878; í ì &#7881; &#297; &#7883; Í Ì &#7880; &#296; &#7882; ó ò &#7887; õ &#7885; Ó Ò &#7886; Õ &#7884; ô &#7889; &#7891; &#7893; &#7895; &#7897; Ô &#7888; &#7890; &#7892; &#7894; &#7896; &#417; &#7899; &#7901; &#7903; &#7905; &#7907; &#416; &#7898; &#7900; &#7902; &#7904; &#7906; ú ù &#7911; &#361; &#7909; Ú Ù &#7910; &#360; &#7908; &#432; &#7913; &#7915; &#7917; &#7919; &#7921; &#431; &#7912; &#7914; &#7916; &#7918; &#7920; ý &#7923; &#7927; &#7929; &#7925; Ý &#7922; &#7926; &#7928; &#7924;";
            string[] array = text.Split(" ".ToCharArray());
            string[] array2 = text2.Split(" ".ToCharArray());
            string text3 = strSource;
            for (int i = 0; i < array.Length; i++)
            {
                text3 = text3.Replace(array2[i], array[i]);
            }
            text = "À Á Â Ã Ä Å Æ Ç È É Ê Ë Ì Í Î Ï Ð Ñ Ò Ó Ô Õ Ö Ø Ù Ú Û Ü Ý Þ ß à á â ã ä å æ ç è é ê ë ì í î ï ð ñ ò ó ô õ ö ø ù ú û ü ý þ ÿ";
            text2 = "&#192; &#193; &#194; &#195; &#196; &#197; &#198; &#199; &#200; &#201; &#202; &#203; &#204; &#205; &#206; &#207; &#208; &#209; &#210; &#211; &#212; &#213; &#214; &#216; &#217; &#218; &#219; &#220; &#221; &#222; &#223; &#224; &#225; &#226; &#227; &#228; &#229; &#230; &#231; &#232; &#233; &#234; &#235; &#236; &#237; &#238; &#239; &#240; &#241; &#242; &#243; &#244; &#245; &#246; &#248; &#249; &#250; &#251; &#252; &#253; &#254; &#255;";
            string[] array3 = text.Split(" ".ToCharArray());
            string[] array4 = text2.Split(" ".ToCharArray());
            for (int i = 0; i < array3.Length; i++)
            {
                text3 = text3.Replace(array4[i], array3[i]);
            }
            return text3.Replace("\0", "");
        }
    }
}
