using GPO_BLAZOR.Client.Class.Date;
using GPO_BLAZOR.Client.Parts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RoleController = GPO_BLAZOR.Client.Parts.RoleController;
using Navigation = Microsoft.AspNetCore.Components.NavigationManager;
using GPO_BLAZOR.Client.Parts;
using GPO_BLAZOR.Client.Class.Date;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;


namespace GPO_BLAZOR.Client.Pages
{
    public partial class Statmens
    {
        //private Statmens c { get; set; } = new Statmens();

        //

        [Inject]
        IJSRuntime jsr { get; set; }
        [Inject]
        Navigation Navigation { get; set; }
        [Inject]
        HttpClient httpClient { get; set; } 

        [Parameter]
        public EventCallback<(string, int)> ViemStatmen { get; set; }

        public IStatmenTableModel? Date { get; set; }

        private bool NewPostMenuViem = false;

        protected override async Task OnInitializedAsync()
        {
            
            isLoadind = false;

            try
            {
                Date ??= await StatmenTableModel.Create(jsr, httpClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StatmensTableCreatorException -> {ex.Message}");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            isLoadind = true;
        }

        private bool isLoadind { get; set; } = false;


        async Task Click((string id, int num, string TypeValue) item)
        {
            var command = (async (string id, int num, string TypeValue) => {
                string way = $"statmen?Number={num}&Type={TypeValue}";
                Navigation.NavigateTo(way);
            });

            command(item.id, item.num, item.TypeValue);

            await ViemStatmen.InvokeAsync((id: item.id, num: item.num));
        }


        async Task NewPost()
        {
            NewPostMenuViem = true;
            

            
        }

        private async Task PostWrotten ()
        {
            NewPostMenuViem = false;
        }
    }
}
