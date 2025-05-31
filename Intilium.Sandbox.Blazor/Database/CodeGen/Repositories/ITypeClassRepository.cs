using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.Repositories
{
    public interface ITypeClassRepository
    {
        Task<List<TypeClass>> GetTypeClassesAsync(TypeClassFilter? filter = null);
        Task<int> InsertAsync(TypeClass typeClass);
        Task<bool> UpdateAsync(TypeClass typeClass);
        Task SaveChangesAsync();
        Task<List<string>> GetAllNamespacesAsync();
    }
}
