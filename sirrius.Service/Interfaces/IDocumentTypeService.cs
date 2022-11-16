using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IDocumentTypeService : IService
    {
        Task<IEnumerable<DocumentType>> GetAllAsync();
        Task<DocumentType> GetByIdAsync(int id);
        Task<DocumentType> CreateAsync(DocumentType model);
        Task<DocumentType> UpdateAsync(int id, DocumentType model);
        Task DeleteAsync(int id);
    }
}

