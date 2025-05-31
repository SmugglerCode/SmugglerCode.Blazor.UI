using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.Snackbar;

public partial class IntiSnackbar : ComponentBase, IDisposable
{
    #region private fields

    private string? _message;
    private Timer? _timer;

    #endregion

    #region Injection

    [Inject]
    public IntiSnackbarService SnackbarService { get; set; } = null!;

    #endregion

    #region lifecycle functions

    protected override void OnInitialized()
    {
        SnackbarService.OnShow += ShowMessage;
    }

    public void Dispose()
    {
        SnackbarService.OnShow -= ShowMessage;
    }

    #endregion

    #region private functions

    private void ShowMessage(string message)
    {
        _message = message;
        InvokeAsync(StateHasChanged);

        //_timer?.Dispose();
        //_timer = new Timer(HideMessage, null, 3000, Timeout.Infinite);
    }

    private void HideMessage(object? state)
    {
        _message = null;
        InvokeAsync(StateHasChanged);
    }

    #endregion
}
