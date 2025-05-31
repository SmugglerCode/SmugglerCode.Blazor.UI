namespace Intilium.Sandbox.Blazor.Database.Doc.Repositories;

public class DiagramLineRepository
{
    #region private fields

    private CodeGenDbContext _dbContext;

    #endregion

    #region constructors

    public DiagramLineRepository(CodeGenDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion
}
