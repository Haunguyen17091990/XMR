using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.Response;
using XRM.Business.DTO.Common;
using XRM.Business.DTO.Common.Request;
using XRM.Business.Service.Common;
using Core.DataAccess.Interface;
using System.Data;

namespace XRM.Business.Service.Common
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
        public IEnumerable<NewsRes> Search()
        {
            return _ObjReadOnlyRepository.Value.StoreProcedureQuery<NewsRes>("News_Search");
        }
        public NewsRes ReadByID(int _ID)
        {
            //return _ObjReadOnlyRepository.Value.StoreProcedureQuery<NewsRes>("SCM.Customer_Search", new
            //{
            //    ID = _ID
            //}, commandType: CommandType.StoredProcedure).ToArray()[0];
            return new NewsRes();
        }
        public IEnumerable<NewsRes> ReadByCate(int _ID)
        {
            //return _ObjReadOnlyRepository.Value.StoreProcedureQuery<NewsRes>("SCM.Customer_Search", new
            //{
            //    ID = _ID
            //}, commandType: CommandType.StoredProcedure).ToArray()[0];
            return new List<NewsRes>();
        }

        public CreateRes<int> Create(NewsInsertReq Obj, int UserID)
        {
            throw new NotImplementedException();
        }
        public DeleteRes Delete(int ID, int UserID)
        {
            throw new NotImplementedException();
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
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        ~NewsService()
        {
            Dispose(false);
        }
    }
}
