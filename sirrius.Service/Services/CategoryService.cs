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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, int> categoryRepository;

        public CategoryService(IRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(Category model)
        {
            return await categoryRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await categoryRepository.FindByIdAsync(id);
            await categoryRepository.DbDeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await categoryRepository.FindAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await categoryRepository.FindByIdAsync(id);
        }


        public async Task<Category> UpdateAsync(int id, Category model)
        {
            var category = await categoryRepository.FindByIdAsync(id);

            //city.Code = model.Code;
            category.Name = model.Name;


            return await categoryRepository.UpdateAsync(category);
        }


        //public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        public async Task<(bool exist, string message)> IsExist(string name)
        {
            string message = string.Empty;

            var categories = await GetAllAsync();

            return (categories.Any(q => SirriusHelper.fullTextSearch(q.Name.ToLower(), name.ToLower())), "Category already exist");
        }
    }
}
