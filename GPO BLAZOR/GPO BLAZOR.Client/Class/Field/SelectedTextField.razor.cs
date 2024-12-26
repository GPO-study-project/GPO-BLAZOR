using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GPO_BLAZOR.Client.Class.Field
{
    public partial class SelectedTextField: Field
    {

        private bool IsLoading;

        private CollectionValues? collection;

        [Inject]
        public IJSRuntime JSRuntime {get; set;}

        [Parameter]
        public string? IDValue { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = false;
            try
            {
                if (collection is null)
                {
                    Console.WriteLine("CollectionIsNull");
                    collection = await CollectionValues.Create(Date.Id, JSRuntime, IDValue, HttpClient);
                }
            }
            finally
            {
                await base.OnInitializedAsync();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                if (collection is null)
                {
                    Console.WriteLine("CollectionIsNull");
                    collection = await CollectionValues.Create(Date.Id, JSRuntime, IDValue, HttpClient);
                }
                if (collection.Values.Count()==1)
                {
                    Date.value = collection.Values[0];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"SelectedTextFieldException -> {ex.Message}");
            }
            finally
            {
                await base.OnParametersSetAsync();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            IsLoading = true;
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
