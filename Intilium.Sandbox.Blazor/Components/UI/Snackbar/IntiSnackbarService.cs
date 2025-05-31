namespace Intilium.Sandbox.Blazor.Components.UI.Snackbar
{
    public class IntiSnackbarService
    {
        public event Action<string>? OnShow;

        public void Show(string message)
        {
            var numberOfSubscriptions = OnShow?.GetInvocationList().Length ?? 0;
            OnShow?.Invoke(message);
        }
    }
}
