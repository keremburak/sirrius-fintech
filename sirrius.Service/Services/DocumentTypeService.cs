using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IRepository<DocumentType, int> documentTypeRepository;

        public DocumentTypeService(IRepository<DocumentType, int> documentTypeRepository)
        {
            this.documentTypeRepository = documentTypeRepository;
        }

        public async Task<DocumentType> CreateAsync(DocumentType model)
        {
            return await documentTypeRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var DocumentType = await documentTypeRepository.FindByIdAsync(id);
            await documentTypeRepository.DbDeleteAsync(DocumentType);
        }

        public async Task<IEnumerable<DocumentType>> GetAllAsync()
        {
            return await documentTypeRepository.FindAllAsync();
        }

        public async Task<DocumentType> GetByIdAsync(int id)
        {
            return await documentTypeRepository.FindByIdAsync(id);
        }

        public async Task<DocumentType> UpdateAsync(int id, DocumentType model)
        {
            var documentType = await documentTypeRepository.FindByIdAsync(id);

            documentType.Name = model.Name;
            documentType.Description = model.Description;

            return await documentTypeRepository.UpdateAsync(documentType);
        }
    }
}
