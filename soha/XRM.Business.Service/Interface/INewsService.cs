using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRM.Business.DTO.Common;
using XRM.Business.DTO.Common.Request;

namespace XRM.Business.Service.Common.Interface
{
    public interface  INewsService : IDisposable
    {
        IEnumerable<NewsRes> Search(string Keyword, int UserID);
        NewsRes ReadByID(int ID, int UserID);
        CreateRes<int> Create(NewsInsertReq Obj, int UserID);
        UpdateRes Update(NewsUpdateReq Obj, int UserID);
        DeleteRes Delete(int ID, int UserID);
    }
}
