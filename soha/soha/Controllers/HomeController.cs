using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XRM.Business.DTO.Common;
using XRM.Business.Service.Common;
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
    public class HomeController : Controller
    {
        private INewsService _ObjNewsService;

        public HomeController(INewsService objNewsService)
        {
            _ObjNewsService = objNewsService;
        }
        public ActionResult Index()
        {
            List<NewsRes> lstNews = _ObjNewsService.Search() as List<NewsRes>;
            //lấy 1 mẫu tin cho phần 1
            NewsRes it1News = lstNews.Where(c=>c.TypeID==10).Skip(0).Take(1).FirstOrDefault();
            //lấy 1 mẫu tin cho phần 2
            NewsRes it2News = lstNews.Where(c=>c.TypeID==2).Skip(0).Take(1).FirstOrDefault();
            //lấy 8 mẫu tin cho phần 3
            List<NewsRes> lst3News= lstNews.Where(c => c.TypeID == 6).Skip(0).Take(8).ToList();
            //lấy 4 mẫu tin cho phần 4 
            List<NewsRes> lst4News = lstNews.Where(c => c.TypeID == 11).Skip(0).Take(4).ToList();
            //lấy 25 mẫu tin cho phần 5 (tin moi nhat) 
            List<NewsRes> lst5News = lstNews.Where(c => c.TypeID == 25).Skip(0).Take(25).ToList();
            //lấy 12 mẫu tin đừng bỏ lỡ
            List<NewsRes> lst6News = lstNews.OrderByDescending(c=>c.ViewNumber).Skip(0).Take(12).ToList();
            ViewBag.it1 = it1News;
            ViewBag.it2 = it2News;
            ViewBag.it3 = lst3News;
            ViewBag.it4 = lst4News;
            ViewBag.it5 = lst5News;
            ViewBag.it6 = lst6News;
            return View();
        }
        public ActionResult Detail()
        {
            //lấy 1 mẫu tin cho phần 2
            List<NewsRes> lstNews = _ObjNewsService.Search() as List<NewsRes>;
            NewsRes it2News = lstNews.Where(c => c.TypeID == 2).Skip(0).Take(1).FirstOrDefault();
            //cung danh muc
            NewsRes it3News = lstNews.Where(c => c.TypeID == 3).Skip(1).FirstOrDefault();
            NewsRes it4News = lstNews.Where(c => c.TypeID == 3).Skip(2).Take(1).FirstOrDefault();
            List<NewsRes> lst5News = lstNews.Where(c => c.TypeID == 3).Skip(3).Take(4).ToList();

            ViewBag.itDetail2 = it2News;
            ViewBag.itDetail3 = it3News;
            ViewBag.itDetail4 = it4News;
            ViewBag.itDetail5 = lst5News;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}