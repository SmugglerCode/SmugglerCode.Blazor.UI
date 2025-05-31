using Intilium.Sandbox.Blazor.Components.Pages.CodeGen;
using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Intilium.Sandbox.Blazor.Database.Doc.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Intilium.Sandbox.Blazor.Components.UI.DiagramCanvas
{
    public partial class IntiDiagramCanvas : ComponentBase
    {
        #region Injections

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        public IDiagramRepository DiagramRepo { get; set; } = null!;

        #endregion

        #region Parameters

        [Parameter]
        public string CanvasId { get; set; } = null!;

        [Parameter]
        public SizeInfo Width { get; set; } = new(100, SizeUnit.Percent);

        [Parameter]
        public SizeInfo Height { get; set; } = new(400, SizeUnit.Px);

        [Parameter]
        public int CanvasWidth { get; set; } = 2800;

        [Parameter]
        public int CanvasHeight { get; set; } = 1000;

        [Parameter]
        public DiagramCanvasViewModel ViewModel { get; set; } = new();

        #endregion

        #region Lifecycle methods

        protected override void OnInitialized()
        {
        }

        protected override void OnParametersSet()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var result = await JSRuntime.InvokeAsync<MessageResult>("canvashelper.init", CanvasId);
                StateHasChanged();
            }

            ViewModel.UpdateCanvasSize(new(100, SizeUnit.Percent), new(1000, SizeUnit.Px));
        }

        #endregion

        private async Task HandleCanvasClick(MouseEventArgs e)
        {
            if (e.CtrlKey)
            {
                await ClosePath();
                ViewModel.PreviousPoint = null;
                return;
            }

            var p = ViewModel.AddPoint(e.OffsetX, e.OffsetY, true);

            await SetStrokeColor("red");
            await SetLineWidth(1);
            await drawLineTo(p);
        }

        private async Task drawLineTo(Point p)
        {
            await JSRuntime.InvokeVoidAsync("canvashelper.drawLineTo", p.X, p.Y);
        }

        private async Task ClosePath()
        {
            await JSRuntime.InvokeVoidAsync("canvashelper.closePath");
        }

        private async Task SetStrokeColor(string color)
        {
            await JSRuntime.InvokeVoidAsync("canvashelper.setStrokeColor", color);
        }

        private async Task ClearCanvas()
        {
            await JSRuntime.InvokeVoidAsync("canvashelper.clear");
        }

        private async Task SetLineWidth(int thickness)
        {
            await JSRuntime.InvokeVoidAsync("canvashelper.setLineWidth", thickness);
        }
        private async Task AddTypeClass()
        {
            if (ViewModel.SelectedTypeClass != null)
            {
                ViewModel.SelectedCard = new TypeClassCardViewModel()
                {
                    TypeClass = ViewModel.SelectedTypeClass,
                    X = new SizeInfo(40, SizeUnit.Px),
                    Y = new SizeInfo(40, SizeUnit.Px)
                };
                ViewModel.SelectedCard.UpdateStyle();
                ViewModel.Cards.Add(ViewModel.SelectedCard);

                ViewModel.CurrentDiagram.Classes.Add(new()
                {
                    DiagramId = ViewModel.CurrentDiagram.Id,
                    TypeClassId = ViewModel.SelectedTypeClass.Id,
                    X = (int)ViewModel.SelectedCard.X.Size,
                    Y = (int)ViewModel.SelectedCard.Y.Size
                });

                await UpdateDiagramAsync();
            }
        }

        private async Task UpdateDiagramAsync()
        {
            var d = await DiagramRepo.UpdateAsync(ViewModel.CurrentDiagram);

            ViewModel.Cards.Clear();
            foreach (var @class in ViewModel.CurrentDiagram.Classes)
            {
                var card = new TypeClassCardViewModel()
                {
                    TypeClass = @class.TypeClass,
                    X = new SizeInfo(@class.X, SizeUnit.Px),
                    Y = new SizeInfo(@class.Y, SizeUnit.Px),
                };
                card.UpdateStyle();
                ViewModel.Cards.Add(card);
            }
        }

        private async Task UpdateCanvas()
        {
            await ClearCanvas();
            if (ViewModel.SelectedCard != null)
            {
                ViewModel.SelectedCard.UpdateStyle();

                var dc = new DiagramClassEntity()
                {
                    Id = ViewModel.SelectedCard.DiagramClassId,
                    X = (int)ViewModel.SelectedCard.X.Size,
                    Y = (int)ViewModel.SelectedCard.Y.Size
                };
                await DiagramRepo.UpdateDiagramClassAsync(dc);

            }
        }
        private void SelectCard(TypeClass typeClass)
        {
            ViewModel.SelectedCard = ViewModel.Cards.Single(x => x.TypeClass.Id == typeClass.Id);
        }
    }
}