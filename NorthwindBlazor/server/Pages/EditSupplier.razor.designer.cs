﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class EditSupplierComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected NorthwindService Northwind { get; set; }

        [Parameter]
        public dynamic SupplierID { get; set; }

        NorthwindBlazor.Models.Northwind.Supplier _supplier;
        protected NorthwindBlazor.Models.Northwind.Supplier supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                if(!object.Equals(_supplier, value))
                {
                    _supplier = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetSupplierBySupplierIdResult = await Northwind.GetSupplierBySupplierId(int.Parse($"{SupplierID}"));
            supplier = northwindGetSupplierBySupplierIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Supplier args)
        {
            try
            {
                var northwindUpdateSupplierResult = await Northwind.UpdateSupplier(int.Parse($"{SupplierID}"), supplier);
                DialogService.Close(supplier);
            }
            catch (Exception northwindUpdateSupplierException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Supplier");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
