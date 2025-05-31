using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.Repositories
{
    public class TypeClassRepository : ITypeClassRepository
    {
        private readonly CodeGenDbContext _dbContext;

        public TypeClassRepository(CodeGenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetAllNamespacesAsync()
        {
            return await _dbContext.TypeClasses.OrderBy(x => x.Namespace).Select(x => x.Namespace).Distinct().ToListAsync();
        }

        public async Task<List<TypeClass>> GetTypeClassesAsync(TypeClassFilter? filter = null)
        {
            var typeClasses = _dbContext.TypeClasses.Include(tc => tc.Properties)
                                              // .ThenInclude(prop => prop.PropertyType)
                                              .OrderBy(tc => tc.Name)
                                              .AsNoTracking()
                                              .AsQueryable();
            if (filter != null)
            {
                if (filter.Namespace != null && filter.Namespace.HasValue)
                {
                    typeClasses = typeClasses.Where(x => x.Namespace == filter.Namespace.Value);
                }
            }

            var result = await typeClasses.ToListAsync();
            foreach (var typeClass in result)
            {
                typeClass.Properties = typeClass.Properties.OrderBy(tp => tp.Name).ToList();
            }

            return result;
        }

        public async Task<int> InsertAsync(TypeClass typeClass)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Entry(typeClass).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();

                foreach (var property in typeClass.Properties)
                {
                    property.TypeClassId = typeClass.Id;
                    _dbContext.Entry(property).State = EntityState.Added;
                }
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return typeClass.Id;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TypeClass typeClass)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Entry(typeClass).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                foreach (var property in typeClass.Properties)
                {
                    property.TypeClassId = typeClass.Id;
                    if (property.Id == 0)
                    {
                        _dbContext.Entry(property).State = EntityState.Added;
                    }
                    else
                    {
                        _dbContext.Entry(property).State = EntityState.Modified;
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
        }
    }
}
