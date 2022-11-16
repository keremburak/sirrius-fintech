using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.Service.Services
{
    public class OperationTypeService : IOperationTypeService
    {
        private readonly IRepository<OperationType, int> operationTypeRepository;

        public OperationTypeService(IRepository<OperationType, int> operationTypeRepository)
        {
            this.operationTypeRepository = operationTypeRepository;
        }

        public async Task<OperationType> CreateAsync(OperationType model)
        {
            return await operationTypeRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var OperationType = await operationTypeRepository.FindByIdAsync(id);
            await operationTypeRepository.DbDeleteAsync(OperationType);
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await operationTypeRepository.FindAllAsync();
        }

        public async Task<OperationType> GetByIdAsync(int id)
        {
            return await operationTypeRepository.FindByIdAsync(id);
        }

        public async Task<OperationType> UpdateAsync(int id, OperationType model)
        {
            var operationType = await operationTypeRepository.FindByIdAsync(id);

            operationType.Name = model.Name;
            operationType.Description = model.Description;

            return await operationTypeRepository.UpdateAsync(operationType);
        }

        public async Task<(bool exist, string message)> IsExist(string name)
        {
            string message = string.Empty;

            var cities = await GetAllAsync();

            return (cities.Any(q => SirriusHelper.fullTextSearch(q.Name.ToLower(), name.ToLower())), "Name already exist");
        }
    }
}
