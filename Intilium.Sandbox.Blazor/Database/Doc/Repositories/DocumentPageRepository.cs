using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database.Doc.Repositories;

public class DocumentPageRepository
{
    private readonly CodeGenDbContext _dbContext;

    public DocumentPageRepository(CodeGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DocumentPageEntity?> GetPageByCategoryIdAsync(int categoryId)
    {
        return await _dbContext.DocumentPages
            .Include(x => x.PageParts)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DocumentCategoryId == categoryId);
    }

    public async Task Update(DocumentPageEntity page)
    {
        var existingPage = _dbContext.DocumentPages
                                     .Include(x => x.PageParts)
                                     .SingleOrDefault(x => x.Id == page.Id);

        if (existingPage != null)
        {
            existingPage.PageParts = existingPage.PageParts.Where(x => page.PageParts.Select(y => y.Id).Contains(x.Id)).ToList();

            foreach (var pagePart in existingPage.PageParts)
            {
                var source = page.PageParts.Single(x => x.Id == pagePart.Id);
                pagePart.Row = source.Row;
                pagePart.Column = source.Column;
                pagePart.ColumnSpan = source.ColumnSpan;
                pagePart.RowSpan = source.RowSpan;
                pagePart.Width = source.Width;
                pagePart.Height = source.Height;
            }

            // Add new ones
            foreach (var pagePart in page.PageParts.Where(x => x.Id == 0))
            {
                existingPage.PageParts.Add(pagePart);
            }


            await _dbContext.SaveChangesAsync();
        }
;
    }

    public async Task<DocumentPageEntity> GetOrCreatePageByCategoryId(int categoryId)
    {
        var page = await GetPageByCategoryIdAsync(categoryId);

        if (page != null)
        {
            if (page.PageParts.Count == 0)
            {
                page.PageParts.Add(CreateDefaultPagePart());
                await _dbContext.SaveChangesAsync();
            }

            return page;
        }

        var newPage = new DocumentPageEntity() { DocumentCategoryId = categoryId };
        newPage.PageParts.Add(CreateDefaultPagePart());

        _dbContext.DocumentPages.Add(newPage);
        await _dbContext.SaveChangesAsync();

        return newPage;
    }

    private DocumentPagePartEntity CreateDefaultPagePart()
    {
        return new DocumentPagePartEntity()
        {
            Column = 1,
            Row = 1,
            ColumnSpan = 1,
            RowSpan = 1,
            Width = "1fr"
        };

    }
}