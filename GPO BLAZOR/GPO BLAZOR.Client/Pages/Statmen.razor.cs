using System.Data;
using System.Diagnostics;
using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;


namespace GPO_BLAZOR.Client.Pages
{
    public partial class Statmen
    {
        [Inject]
        public IAutorizationStruct? AutStruct { get; set; }


        [Parameter]
        public IStatmen? Date { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? Number { get; set; }
        [Parameter]
        [SupplyParameterFromQuery]
        public string Type { get; set; }
        [Inject]
        HttpClient  _httpClient { get; set; }

        [Parameter]
        public EventCallback Return { get; set; }

        private bool isLoading;

        private IPage SelectedPage;

        protected async Task ChangeValue(IPage selectPage)
        {
            SelectedPage = selectPage;

        }
        private async Task SetDate()
        {

            try
            {
                Console.WriteLine($"SendDateMessage -> {await Date.SendDate(jsr)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendDateViemException -> {ex.Message}");
            }
            finally
            {
                Navigation.NavigateTo("/");
                if (Return.HasDelegate)
                    await Return.InvokeAsync();
            }

        }

        protected override async Task OnInitializedAsync()
        {
            isLoading = false;
            try
            {
                string id;
                if (Number.HasValue)
                    id = (await StatmenTableModel.Create(jsr, _httpClient)).Lines[Number.Value].id;
                else
                    id = "New";
                 Date = await Class.Date.Statmen.Create(id, jsr, Type, _httpClient);
                 SelectedPage = Date.Date.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StatmenRenderingException -> {ex.Message}");
            }
        }

        protected override void OnParametersSet()
        {
            isLoading = true;
        }
    }
}
