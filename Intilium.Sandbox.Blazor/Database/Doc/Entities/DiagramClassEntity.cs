using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DiagramClassEntity
    {
        #region properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public int Id { get; set; }

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        /// <summary>
        /// Gets or sets the id of the DiagramEntity <see cref="DiagramEntity"/> which acts as a container for all diagram data.
        /// </summary>
        public int DiagramId { get; set; }

        /// <summary>
        /// Gets or sets the id of the TypeClass <see cref="TypeClass"/>. The TypeClass contains the information about a specific class and its properties, methods, ....
        /// </summary>
        public int TypeClassId { get; set; }

        #endregion

        #region navigational properties

        public DiagramEntity Diagram { get; set; } = null!;

        public TypeClass TypeClass { get; set; } = null!;

        #endregion
    }
}
