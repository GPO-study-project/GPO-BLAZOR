﻿using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

namespace GPO_BLAZOR.Client.Pages
{
    public partial class AutorizationForm
    {
        //[Parameter]
        //public CookieStorageAccessor cookieStorage {  get; set; }

        [Parameter]
        [Required]
        public required IAuthorizationDate AuthorizationInterface { get; set; }

        [Inject]
        IAutorizationStruct? Autorizer { get; set; }

        [Parameter]
        public EventCallback<IAuthorizationDate> AuthorizationInterfaceChanged { get; set; }

        private string message = null;

        private bool _isLoading = true;
        private bool isLoading {
            get
            {
                return _isLoading;
            }
            set 
            {
                _isLoading = value;
            }
        }

        [Parameter]
        public System.Timers.Timer timer { get; set; }

        private bool firstRender;

        async Task ButtonClicked()
        {

#if DEBUG            
            Console.WriteLine("Callback0: "+ AuthorizationInterface.IsCookies+ " "+ AuthorizationInterface.GetHashCode());
#endif
            try
            {
                Console.WriteLine(AuthorizationInterface);
                await AuthorizationInterface.GetValues(ReadCookies, timer, Autorizer);
                await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //await Checer();
                message = "Неверное имя пользователя или пароль";
            }

        }

        string value = "value";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
#if DEBUG
            Console.WriteLine("Рендер: " + firstRender);
            Console.WriteLine("Loading 1:  " + isLoading);
#endif
            this.firstRender = !firstRender;
            isLoading = false;
            try
            {
                if (AuthorizationInterface._writer != null)
                    if (firstRender)
                    {
#if DEBUG
                        Console.WriteLine("CheckCookie");
#endif
                        AuthorizationInterface = new AuthorizationDate(ReadCookies, WriteCookies);
                        await AuthorizationInterface.GetValues(ReadCookies, timer, Autorizer);
                        await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
                    }
                    else { }
                else
                {
                    Console.WriteLine(AuthorizationInterface._writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Checer();
            }
            finally
            {
#if DEBUG
                Console.WriteLine("Loading 2:  " + isLoading);
#endif
                if (firstRender)
                {
#if DEBUG
                    Console.WriteLine($"Refresh Page: {firstRender}");
#endif
                    this.StateHasChanged();
                }
            }
        }




        protected override async Task OnInitializedAsync()
        {
#if DEBUG
            //            Console.WriteLine("Загрузились");
#endif
            isLoading = true;

        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (AuthorizationInterface._writer==null)
            AuthorizationInterface = new AuthorizationDate(ReadCookies, WriteCookies);
        }


        protected async Task Checer()
        {
            await AuthorizationInterface.GetValues(ReadCookies, timer, Autorizer);
            await AuthorizationInterfaceChanged.InvokeAsync();
        }




        protected async Task WriteCookies(string key, string value)
        {
#if DEBUG
            Console.WriteLine("Write Cookie Time: "+DateTime.Now.AddMinutes(1));
#endif
                await cookieStorage.WriteCookieAsync(key, value, DateTime.Now.AddMinutes(20));
        }


        protected async Task<string> ReadCookies(string key)
        {
            string temp = await cookieStorage.ReadCookieAsync<string>(key) ?? "";
            try
            {
                if (temp != "")
                {
                    await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
                }
#if DEBUG
                Console.WriteLine($"Autorization read temp: {temp}");
#endif
                return temp ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ("ReadCookies Error");
            }
        }

        public async void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AuthorizationInterface.Send(value, timer, Autorizer); 
                await ButtonClicked();
            }
        }

    }
}
