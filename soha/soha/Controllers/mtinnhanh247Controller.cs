﻿using System;
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
    public class mtinnhanh247Controller : Controller
    {
        private INewsService _ObjNewsService;

        public mtinnhanh247Controller(INewsService objNewsService)
        {
            _ObjNewsService = objNewsService;
        }
        // GET: mtinnhanh247
        public ActionResult trangchu()
        {
            List<NewsRes> lstNews = _ObjNewsService.Search() as List<NewsRes>;
            //lấy 1 mẫu tin cho phần 1
            NewsRes it1News = lstNews.Where(c => c.TypeID == 7).Take(1).FirstOrDefault();
            //lấy 8 mẫu tin cho phần 2
            List<NewsRes> it2News = lstNews.Where(c => c.TypeID == 7).Skip(1).Take(8).ToList();
            //lấy 8 mẫu tin cho phần 3 
            List<NewsRes> lst3News = lstNews.Where(c => c.TypeID == 6).Take(8).ToList();
            //lấy 8 mẫu tin cho phần 4 (mới nhất)
            List<NewsRes> lst4News = lstNews.Where(c => c.TypeID == 10).Take(5).ToList();
            //lấy 50 mẫu tin cho phần 5 
            List<int> id2 = it2News.Select(c => c.NewsID).ToList();
            List<int> id3 = lst3News.Select(c => c.NewsID).ToList();
            List<int> id4 = lst4News.Select(c => c.NewsID).ToList();
            List<NewsRes> lst5News = lstNews.Where(c => !id2.Contains(c.NewsID) && !id3.Contains(c.NewsID) && !id4.Contains(c.NewsID)).Take(50).ToList();
            
            ViewBag.it1 = it1News;
            ViewBag.it2 = it2News;
            ViewBag.it3 = lst3News;
            ViewBag.it4 = lst4News;
            ViewBag.it5 = lst5News;
            return View();
        }
        public ActionResult chitiet(int ID)
        {
            //lấy 1 mẫu tin cho phần 2
            List<NewsRes> lstNews = _ObjNewsService.Search() as List<NewsRes>;
            NewsRes it1News = lstNews.Where(c => c.NewsID == ID).FirstOrDefault();
            //cung danh muc
            List<NewsRes> lst2News = lstNews.Where(c => c.TypeID == it1News.TypeID && c.NewsID!=it1News.NewsID).OrderByDescending(c => c.ViewNumber).Take(3).ToList();
            //dang hot
            List<NewsRes> lst3News = lstNews.OrderByDescending(c => c.ViewNumber).Take(3).ToList();
            ViewBag.it1 = it1News;
            ViewBag.it2 = lst2News;
            ViewBag.it3 = lst3News;
            return View();
        }
        public ActionResult danhmuc(int ID)
        {
            List<NewsRes> lstNews = _ObjNewsService.Search() as List<NewsRes>;
            //lấy 1 mẫu tin cho phần 1
            NewsRes it1News = lstNews.Where(c => c.TypeID == ID).Take(1).FirstOrDefault();
            //lấy 8 mẫu tin cho phần 2
            List<NewsRes> it2News = lstNews.Where(c => c.TypeID == ID && c.NewsID!=it1News.NewsID).OrderByDescending(c=>c.ViewNumber).Take(8).ToList();
            //lấy 50 mẫu tin cho phần 5 
            List<int> id2 = it2News.Select(c => c.NewsID).ToList();
            List<NewsRes> lst3News = lstNews.Where(c =>c.TypeID==ID && c.NewsID!=it1News.NewsID && !id2.Contains(c.NewsID)).OrderByDescending(c => c.ViewNumber).Take(50).ToList();
            ViewBag.it1 = it1News;
            ViewBag.it2 = it2News;
            ViewBag.it3 = lst3News;
            return View();
        }
    }
}