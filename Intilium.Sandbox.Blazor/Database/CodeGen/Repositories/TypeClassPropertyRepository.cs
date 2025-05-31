using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.Repositories
{

    public interface ITypeClassPropertyRepository
    {
        Task<int> InsertPropertyAsync(TypeClassProperty property);
        Task<List<TypeClass>> GetTypeClassesByNameAsync(string name);

        Task<List<TypeClassProperty>> GetAllProperties();
    }

    public class TypeClassPropertyRepository : ITypeClassPropertyRepository
    {
        private readonly CodeGenDbContext _dbContext;

        public TypeClassPropertyRepository(CodeGenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TypeClassProperty>> GetAllProperties()
        {
            return await _dbContext.Properties.ToListAsync();
        }

        public async Task<List<TypeClass>> GetTypeClassesByNameAsync(string name)
        {
            var result = await _dbContext.TypeClasses.Where(x => x.Name == name).ToListAsync();
            return result;
        }

        public async Task<int> InsertPropertyAsync(TypeClassProperty property)
        {
            _dbContext.Entry(property).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return property.Id;
        }
    }
}
