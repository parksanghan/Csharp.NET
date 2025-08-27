using BlazorServerSignalR.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorServerSignalR.Advanced.Blazor
{
    public partial class Split
    {
        [Inject]
        public BlazorTDBContext? TDBContext { get; set; }    
        public IEnumerable<string> Names =>TDBContext.Products.Select(n=>n.ProductName) ?? Enumerable.Empty<string>();  
    }
}
