using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database.Doc.Repositories
{
    public interface IDiagramRepository
    {
        Task<int> InsertAsync(DiagramEntity diagram);

        Task<DiagramEntity> UpdateAsync(DiagramEntity diagram);

        Task<List<DiagramEntity>> GetAllDiagramsAsync();

        Task UpdateDiagramClassAsync(DiagramClassEntity diagramClassEntity);
    }

    public class DiagramRepository : IDiagramRepository
    {
        private CodeGenDbContext _dbContext;

        public DiagramRepository(CodeGenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DiagramEntity>> GetAllDiagramsAsync()
        {
            return await _dbContext.Diagrams
                                   .Include(x => x.Classes)
                                   .ThenInclude(x => x.TypeClass)
                                   .ThenInclude(x => x.Properties)
                                   .AsNoTracking()
                                   .OrderBy(x => x.Name)
                                   .ToListAsync();
        }

        public async Task<int> InsertAsync(DiagramEntity diagram)
        {
            if (diagram.Id != 0)
            {
                return 0;
            }

            if (_dbContext == null)
            {
                return 0;
            }
            try
            {
                _dbContext.Diagrams.Add(diagram);
                await _dbContext.SaveChangesAsync();
                return diagram.Id;
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                throw;
            }
        }

        public void Update(DiagramEntity diagram)
        {
            var existingDiagram = _dbContext.Diagrams.SingleOrDefault(x => x.Id == diagram.Id);
            if (existingDiagram != null)
            {
                DiagramMapperExtensions.UpdateExistingDiagram(diagram, existingDiagram);
            }
        }

        public async Task<DiagramEntity?> GetDiagramByIdAsync(int id)
        {
            return await _dbContext.Diagrams
                                   .Include(x => x.Classes)
                                   .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DiagramEntity> UpdateAsync(DiagramEntity diagram)
        {
            var existing = await GetDiagramByIdAsync(diagram.Id);
            if (existing != null)
            {
                // new ones
                var diagrams = diagram.Classes.Where(x => x.Id == 0);
                foreach (var d in diagrams)
                {
                    d.Diagram = existing;
                    d.TypeClass = _dbContext.TypeClasses.Single(x => x.Id == d.TypeClassId);
                }

                foreach (var c in diagram.Classes.Where(x => x.Id > 0))
                {
                    var existingClass = existing.Classes.First(x => x.Id == c.Id);
                    existingClass.X = c.X;
                    existingClass.Y = c.Y;
                }

                existing.Classes.AddRange(diagrams);
            }

            await _dbContext.SaveChangesAsync();
            return existing!;
        }

        public async Task UpdateDiagramClassAsync(DiagramClassEntity diagramClass)
        {
            var existingDiagramClass = _dbContext.DiagramClasses.Single(x => x.Id == diagramClass.Id);
            existingDiagramClass.X = diagramClass.X;
            existingDiagramClass.Y = diagramClass.Y;

            await _dbContext.SaveChangesAsync();
        }
    }
}
