using Microsoft.AspNetCore.Mvc;
using Research.Interface;
using Research.Models;
using ResearchWeb.Common;
using ResearchWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace soha.Controllers
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
    public class tinnhanh247Controller : Controller
    {
        private Utils objUtil = new Utils();

        //private IRepositories _ObjRepositories;
        //public tinnhanh247Controller(IRepositories ObjRepositories)
        //{
        //    _ObjRepositories = ObjRepositories;
        //}
        public ActionResult trangchu()
        {

            //string sql = "select NewsID,title,TypeID,MenuID,SourceID,ImageTH,ImageFull,ShortContent,Content,ViewNumber,CreatedDate from news order by CreatedDate desc limit 500";
            //List<NewsReponse> Lst = _ObjRepositories.ReadAll<NewsReponse>(sql);

            if (objUtil.fBrowserIsMobile())
            {
                return RedirectToAction("trangchu", "mtinnhanh247");
            }
            DBContext context = HttpContext.RequestServices.GetService(typeof(DBContext)) as DBContext;
            List<NewsRes> lstNews = context.GetAllNewsRes() as List<NewsRes>;
            ////lấy 1 mẫu tin cho phần 1
            //NewsRes it1News = lstNews.Where(c => c.TypeID == 10).Take(1).FirstOrDefault();
            ////lấy 1 mẫu tin cho phần 2
            //NewsRes it2News = lstNews.Where(c => c.TypeID == 2).Take(1).FirstOrDefault();
            ////lấy 8 mẫu tin cho phần 3
            //List<NewsRes> lst3News = lstNews.Where(c => c.TypeID == 7).Take(8).ToList();
            ////lấy 4 mẫu tin cho phần 4 
            //List<NewsRes> lst4News = lstNews.Where(c => c.TypeID == 10).Take(4).ToList();
            ////lấy 25 mẫu tin cho phần 5 (tin moi nhat) 
            //List<NewsRes> lst5News = lstNews.Where(c => c.TypeID == 6).Take(25).ToList();
            ////lấy 12 mẫu tin đừng bỏ lỡ
            //List<NewsRes> lst6News = lstNews.OrderByDescending(c => c.ViewNumber).Take(12).ToList();


            //lấy 1 mẫu tin cho phần 1
            NewsRes it1News = lstNews.Where(c => c.TypeID == 6).Take(1).FirstOrDefault();

            //lấy 1 mẫu tin cho phần 2
            NewsRes it2News = lstNews.Where(c => c.TypeID == 2).Take(1).FirstOrDefault();
            //lấy 8 mẫu tin cho phần 3
            List<NewsRes> lst3News = lstNews.Where(c => c.TypeID == 3).Take(8).ToList();
            //lấy 4 mẫu tin cho phần 4 
            List<NewsRes> lst4News = lstNews.Where(c => c.TypeID == 4).Take(4).ToList();
            //lấy 25 mẫu tin cho phần 5 (tin moi nhat) 
            List<NewsRes> lst5News = lstNews.Where(c => c.TypeID == 6).Take(25).ToList();
            //lấy 12 mẫu tin đừng bỏ lỡ
            List<NewsRes> lst6News = lstNews.OrderByDescending(c => c.ViewNumber).Take(12).ToList();
            ViewBag.it1 = it1News;
            ViewBag.it2 = it2News;
            ViewBag.it3 = lst3News;
            ViewBag.it4 = lst4News;
            ViewBag.it5 = lst5News;
            ViewBag.it6 = lst6News;
            return View();
        }
        public ActionResult chitiet(string ID)
        {
            try
            {
                if (objUtil.fBrowserIsMobile())
                {
                    return RedirectToAction("chitiet", "mtinnhanh247", new { ID = ID });
                }
                //var arrtemp = ID.Split('-');
                //string strFeedId = arrtemp[arrtemp.Length - 1];
                //lấy 1 mẫu tin cho phần 2
                DBContext context = HttpContext.RequestServices.GetService(typeof(DBContext)) as DBContext;
                List<NewsRes> lstNews = context.GetAllNewsRes() as List<NewsRes>;
                NewsRes it2News = lstNews.Where(c => c.NewsID == Convert.ToInt32(ID)).FirstOrDefault();
                //cung danh muc
                NewsRes it3News = lstNews.Where(c => c.TypeID == it2News.TypeID).Skip(1).OrderByDescending(c => c.ViewNumber).FirstOrDefault();
                NewsRes it4News = lstNews.Where(c => c.TypeID == it2News.TypeID).Skip(2).Take(1).OrderByDescending(c => c.ViewNumber).FirstOrDefault();
                List<NewsRes> lst5News = lstNews.Where(c => c.TypeID == it2News.TypeID).OrderByDescending(c => c.ViewNumber).Skip(3).Take(4).ToList();

                ViewBag.itDetail2 = it2News;
                ViewBag.itDetail3 = it3News;
                ViewBag.itDetail4 = it4News;
                ViewBag.itDetail5 = lst5News;

                //update viewnumber
                context.Update(Convert.ToInt32(ID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public ActionResult danhmuc(string Alias,string ID)
        {
            try
            {
                if (objUtil.fBrowserIsMobile())
                {
                    return RedirectToAction("danhmuc", "mtinnhanh247", new { Alias= Alias, ID = ID });
                }
                //var arrtemp = ID.Split('-');
                //string strFeedId = arrtemp[arrtemp.Length - 1];
                DBContext context = HttpContext.RequestServices.GetService(typeof(DBContext)) as DBContext;
                List<NewsRes> lstNews = context.GetAllNewsRes() as List<NewsRes>;
                //lấy 3 mẫu tin cho phần 1
                List<NewsRes> p1 = lstNews.Where(c => c.TypeID == Convert.ToInt32(ID)).Take(3).ToList();
                //lấy 3 mẫu tin cho phần 2
                List<NewsRes> p2 = lstNews.Where(c => c.TypeID == Convert.ToInt32(ID)).Skip(3).Take(3).ToList();
                //lấy 6 mẫu tin cho phần hot
                List<int> idP1 = p1.Select(c => c.NewsID).ToList();
                List<int> idP2 = p2.Select(c => c.NewsID).ToList();
                List<NewsRes> p3 = lstNews.Where(c => c.TypeID == Convert.ToInt32(ID) && !idP1.Contains(c.NewsID) && !idP2.Contains(c.NewsID)).OrderByDescending(c => c.ViewNumber).Take(5).ToList();
                //25 mẫu tin mới nhiều ng doc trong hnay va k chứ những tin đã có ở các phần trên
                List<int> idP3 = p3.Select(c => c.NewsID).ToList();
                List<NewsRes> p4 = lstNews.Where(c => c.TypeID == Convert.ToInt32(ID) && !idP1.Contains(c.NewsID) && !idP2.Contains(c.NewsID) && !idP3.Contains(c.NewsID) && c.CreatedDate == DateTime.Today).OrderByDescending(c => c.ViewNumber).Take(25).ToList();
                //List<NewsRes> p4 = lstNews.Where(c => c.TypeID == ID).OrderByDescending(c => c.ViewNumber).Take(25).ToList();

                ViewBag.p1 = p1;
                ViewBag.p2 = p2;
                ViewBag.p3 = p3;
                ViewBag.p4 = p4;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}