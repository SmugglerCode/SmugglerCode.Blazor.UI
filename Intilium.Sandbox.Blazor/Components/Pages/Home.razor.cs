namespace Intilium.Sandbox.Blazor.Components.Pages
{
    public partial class Home
    {
        public List<WorkTask> Tasks { get; set; } = [];
 
        public Home() 
        {
            Tasks.Add(new WorkTask()
            {
                 CreationDate = "12/02/2025",
                 Title = "Import projects on new computer",
                 Description = "The initialk setup for the creation of a <span style='color:red'>BLazor UI library.</span>. This will be a sandbox, later on it will be moved to its own component library."
            });
            Tasks.Add(new WorkTask()
            {
                CreationDate = "12/02/2025",
                Title = "Start blazor library",
                Description = "The initialk setup for the creation of a <span style='color:red'>BLazor UI library.</span>. This will be a sandbox, later on it will be moved to its own component library.",
                IsCompleted = true

            });
        }
    }

    public class WorkTask 
    {
        public string CreationDate { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
