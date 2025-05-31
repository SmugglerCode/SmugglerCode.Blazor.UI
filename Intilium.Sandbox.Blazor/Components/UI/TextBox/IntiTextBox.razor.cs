using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Intilium.Sandbox.Blazor.Components.UI.TextBox
{
    public partial class IntiTextBox<TValue> : ComponentBase
    {
        #region parameters

        /// <summary>
        /// Gets or sets the label for the input field.
        /// </summary>
        [Parameter]
        public string? Label { get; set; } = null;

        /// <summary>
        /// Gets or sets the actual value for the input field.
        /// </summary>
        [Parameter]
        public TValue? Text { get; set; }

        /// <summary>
        /// Gets or sets the event call back for when the text changes.
        /// </summary>
        [Parameter]
        public EventCallback<TValue?> TextChanged { get; set; }

        [Parameter]
        public EventCallback<TValue?> OnEnter { get; set; }

        #endregion

        private async Task OnKeyPressed(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await OnEnter.InvokeAsync(Text);
            }
        }

        private async Task OnTextChanged(ChangeEventArgs e)
        {
            if (e.Value == null)
            {
                Text = default;
                await TextChanged.InvokeAsync(Text);
                return;
            }

            var stringValue = e.Value.ToString();

            try
            {
                var convertedValue = (TValue?)Convert.ChangeType(stringValue, typeof(TValue));
                Text = convertedValue;
                await TextChanged.InvokeAsync(convertedValue);
            }
            catch
            {
                // Optioneel: afhandelen bij conversiefouten (bijvoorbeeld foutmelding tonen)
            }
        }
    }
}


