using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database.Doc.Repositories;

public class DocumentCategoryRepository
{
    private readonly CodeGenDbContext _dbContext;

    public void Update(DocumentCategoryEntity category)
    {
        var dbCategory = _dbContext.DocumentCategories.Find(category.Id);

        if (dbCategory != null)
        {
            dbCategory.Title = category.Title;
            dbCategory.ParentId = category.ParentId;
            dbCategory.IsDocumentationPage = category.IsDocumentationPage;
        }
    }

    public async Task<DocumentCategoryEntity?> GetByIdAsync(int? id)
    {
        if (id == null) return null;

        return await _dbContext.DocumentCategories.SingleOrDefaultAsync(x => x.Id == id);
    }

    public DocumentCategoryRepository(CodeGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(DocumentCategoryEntity category)
    {
        category.Parent = null;
        _dbContext.DocumentCategories.Add(category);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = _dbContext.DocumentCategories
                                 .SingleOrDefault(x => x.Id == id);

        if (category != null)
        {
            DeleteCategory(category);
        }
    }

    public void DeleteCategory(DocumentCategoryEntity category)
    {
        var hasChildren = _dbContext.DocumentCategories
                                    .Any(x => x.ParentId == category.Id);

        if (!hasChildren)
        {
            _dbContext.Remove(category);
        }
    }

    public List<DocumentCategoryEntity> GetAll()
    {
        var allCategories = _dbContext.DocumentCategories
                         .OrderBy(x => x.Title)
                         .ToList();

        var rootCategories = allCategories.Where(c => c.ParentId == null)
                                          .ToList();

        foreach (var root in rootCategories)
        {
            BuildTree(root, allCategories);
        }

        return rootCategories;
    }

    public List<DocumentCategoryEntity> GetAllParentCategories()
    {
        return _dbContext.DocumentCategories
                         .Where(x => x.IsDocumentationPage == false)
                         .OrderBy(x => x.Title)
                         .AsNoTracking()
                         .ToList();
    }

    public void BuildTree(DocumentCategoryEntity parent, List<DocumentCategoryEntity> all)
    {
        var children = all
            .Where(c => c.ParentId == parent.Id)
            .ToList();

        parent.Children = children;

        foreach (var child in children)
        {
            BuildTree(child, all);
        }
    }
}
