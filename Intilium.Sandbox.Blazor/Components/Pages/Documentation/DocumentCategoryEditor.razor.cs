using Intilium.Sandbox.Blazor.Components.UI.TreeView;
using Intilium.Sandbox.Blazor.Database;
using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Intilium.Sandbox.Blazor.Database.Doc.Repositories;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation
{
    public partial class DocumentCategoryEditor : ComponentBase
    {
        [Inject]
        public CodeGenDbContext DbContext { get; set; } = null!;

        private TreeViewItem<DocumentCategoryEntity> SelectedItem { get; set; } = null!;

        private DocumentCategoryRepository _repo = null!;

        private DocumentCategoryViewModel _category = new DocumentCategoryViewModel();

        private List<TreeViewItem<DocumentCategoryEntity>> _categories = new List<TreeViewItem<DocumentCategoryEntity>>();

        private List<DocumentCategoryEntity> _parentCategories = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _repo = new DocumentCategoryRepository(DbContext);

            LoadDropDown();
            LoadTreeView();
        }

        public void LoadDropDown()
        {
            _parentCategories = _repo.GetAllParentCategories();
        }

        public void LoadTreeView()
        {
            _categories.Clear();
            var categories = _repo.GetAll();
            CategoryHelper.ConvertCategoriesToTreeView(categories, _categories, null);
        }
        public void ChangeParentCategory(DocumentCategoryEntity category)
        {
            _category.ParentId = category.Id;
        }

        public void UpdateCategory()
        {
            if (CanSave())
            {
                _repo.Update(DocumentCategoryViewModel.ToEntity(_category));
                _repo.Save();
            }
        }

        public async Task InsertCategoryAsync()
        {
            if (CanSave())
            {
                _repo.Insert(DocumentCategoryViewModel.ToEntity(_category));
                await _repo.SaveAsync();

                _category = new DocumentCategoryViewModel();
                LoadDropDown();
                LoadTreeView();
            }
        }

        private bool CanSave()
        {
            var hasErrors = false;
            if (_category != null)
            {
                if (string.IsNullOrWhiteSpace(_category.Title))
                    hasErrors = true;

                if (!hasErrors && _category.Title.Length > 200)
                    hasErrors = true;
            }

            return !hasErrors;
        }
    }
}

public class DocumentCategoryViewModel
{
    /// <summary>
    /// Gets or sets the unique id for the document category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the category or document page.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the parent id of a category <see cref="DocumentCategoryEntity"/>. This is another 
    /// category which will act as the parent category in order 
    /// to create a category tree.
    /// </summary>
    public int ParentId { get; set; } = 0;

    /// <summary>
    /// Gets or sets the boolean which indicates if the category is the title for a document page. 
    /// If set to false then it is a category group.
    /// </summary>
    public bool IsDocumentationPage { get; set; }

    #region Navigation Properties

    /// <summary>
    /// gets or sets the navigational reference to the parent category, if any.
    /// </summary>
    public DocumentCategoryViewModel? Parent { get; set; }

    /// <summary>
    /// Gets or sets the list of children categories if any.
    /// </summary>
    public ICollection<DocumentCategoryViewModel> Children { get; set; } = new List<DocumentCategoryViewModel>();

    #endregion

    public static DocumentCategoryEntity ToEntity(DocumentCategoryViewModel vm)
    {
        return new DocumentCategoryEntity()
        {
            Id = vm.Id,
            ParentId = vm.ParentId == 0 ? null : vm.ParentId,
            Title = vm.Title,
            IsDocumentationPage = vm.IsDocumentationPage,
            Children = vm.Children.Select(x => DocumentCategoryViewModel.ToEntity(x)).ToList(),
            Parent = vm.Parent == null ? null : DocumentCategoryViewModel.ToEntity(vm.Parent)
        };
    }

    public static DocumentCategoryViewModel ToViewModel(DocumentCategoryEntity entity)
    {
        return new DocumentCategoryViewModel()
        {
            Id = entity.Id,
            ParentId = entity.ParentId ?? 0,
            Title = entity.Title,
            IsDocumentationPage = entity.IsDocumentationPage,
            Children = entity.Children.Select(x => DocumentCategoryViewModel.ToViewModel(x)).ToList(),
            Parent = entity.Parent == null ? null : DocumentCategoryViewModel.ToViewModel(entity.Parent)
        };
    }
}

public class CategoryHelper
{
    public static void ConvertCategoriesToTreeView(List<DocumentCategoryEntity> categories, List<TreeViewItem<DocumentCategoryEntity>> treeviewList, TreeViewItem<DocumentCategoryEntity>? parent)
    {
        foreach (var cat in categories)
        {
            var treeViewItem = new TreeViewItem<DocumentCategoryEntity>();
            treeViewItem.Item = cat;
            treeViewItem.Label = cat.Title;

            if (parent == null)
            {
                treeviewList.Add(treeViewItem);
            }
            else
            {
                parent.Children.Add(treeViewItem);
            }

            if (cat.HasChildren)
            {
                ConvertCategoriesToTreeView(cat.Children, treeviewList, treeViewItem);
            }
        }
    }

}