using BEQLDT.Data;
using BEQLDT.Data.Infrastructure;
using BEQLDT.Data.Repositories;
using BEQLDT.Model;
using BEQLDT.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEQLDT.Service
{
    public interface IFileService
    {
        Filed Add(Filed file);
        void Update(Filed file);
        Filed Delete(int id);
        IEnumerable<Filed> GetAll();
        Filed GetById(int id);
        void SaveChange();
        PaginationSet<Filed> GetAllPage(int page, int pageSize);
    }
    public class FileService:IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
       
        
        public FileService(IFileRepository fileRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
            
        }
        public PaginationSet<Filed> GetAllPage(int page, int pageSize)
        {
            var results = _fileRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Filed> GetPagedResultForQuery(
       IEnumerable<Filed> query, int page, int pageSize)
        {
            var result = new PaginationSet<Filed>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public Filed Add(Filed file)
        {
            return _fileRepository.Add(file);
        }

        public Filed Delete(int id)
        {
            return _fileRepository.Delete(id);
        }

        public IEnumerable<Filed> GetAll()
        {
            return _fileRepository.GetAll();
        }


        public Filed GetById(int id)
        {
            return _fileRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Filed file)
        {
            _fileRepository.Update(file);
        }
    }
}
