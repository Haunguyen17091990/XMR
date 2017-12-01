using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.Response;
using XRM.Business.DTO.Common;
using XRM.Business.DTO.Common.Request;
using XRM.Business.Service.Common.Interface;
using Core.DataAccess.Interface;
using System.Data;

namespace XRM.Business.Service.Common.Implement
{
    public class NewsService : INewsService
    {
        private readonly Lazy<IRepository> _ObjRepository;
        private readonly Lazy<IReadOnlyRepository> _ObjReadOnlyRepository;

        private bool disposedValue = false;

        public NewsService(Lazy<IRepository> ObjRepository, Lazy<IReadOnlyRepository> ObjReadOnlyRepository)
        {
            _ObjRepository = ObjRepository;
            _ObjReadOnlyRepository = ObjReadOnlyRepository;
        }
        public CreateRes<int> Create(NewsInsertReq Obj, int UserID)
        {
            throw new NotImplementedException();
        }

        public DeleteRes Delete(int ID, int UserID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public NewsRes ReadByID(int ID, int UserID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewsRes> Search(string Keyword, int UserID)
        {
            return _ObjReadOnlyRepository.Value.StoreProcedureQuery<NewsRes>("SCM.Customer_Search", new
            {
                Keyword = Keyword
            });
            
        }

        public UpdateRes Update(NewsUpdateReq Obj, int UserID)
        {
            throw new NotImplementedException();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_ObjRepository.IsValueCreated)
                        _ObjRepository.Value.Dispose();
                    if (_ObjReadOnlyRepository.IsValueCreated)
                        _ObjReadOnlyRepository.Value.Dispose();
                }
                disposedValue = true;
            }
        }
        ~NewsService()
        {
            Dispose(false);
        }
    }
}
