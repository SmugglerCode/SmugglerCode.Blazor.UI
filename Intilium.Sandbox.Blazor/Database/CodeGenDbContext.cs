using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intilium.Sandbox.Blazor.Database
{
    public class CodeGenDbContext : DbContext
    {
        #region doc

        /// <summary>
        /// Gets or sets the document categories, needed to persist the data to the database.
        /// </summary>
        public DbSet<DocumentCategoryEntity> DocumentCategories { get; set; }

        public DbSet<DocumentPageEntity> DocumentPages { get; set; }

        public DbSet<TypeClassProperty> Properties { get; set; }

        #endregion

        public DbSet<DiagramEntity> Diagrams { get; set; }

        public DbSet<DiagramClassEntity> DiagramClasses { get; set; }
        public DbSet<TypeClass> TypeClasses { get; set; }

        public CodeGenDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeGenDbContext).Assembly);
        }
    }
}
