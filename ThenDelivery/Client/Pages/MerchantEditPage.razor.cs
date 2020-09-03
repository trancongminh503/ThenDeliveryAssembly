﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Exceptions;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Client.Pages
{
   public class MerchantEditPageBase : CustomComponentBase<MerchantEditPageBase>
   {
      #region Inject

      #endregion

      #region Parameters
      [Parameter] public int Id { get; set; }
      #endregion

      #region Properties
      public MerchantDto MerchantModel { get; set; }
      protected EditContext EditContext { get; set; }
      public CustomResponse ResponseModel { get; set; }
      #endregion

      #region Life Cycle
      protected override void OnInitialized()
      {
         base.OnInitialized();
      }
      protected override async Task OnInitializedAsync()
      {
         await base.OnInitializedAsync();
         MerchantModel = await HttpClientServer.CustomGetAsync<MerchantDto>($"api/merchant?merchantId={Id}");
         EditContext = new EditContext(MerchantModel);
         StateHasChanged();
      }
      #endregion

      #region Events
      protected async Task HandleOnSubmit()
      {
         if (EditContext.Validate())
         {
            if (EditContext.IsModified())
            {
               ResponseModel = await HttpClientServer.CustomPutAsync($"api/merchant", MerchantModel);
               ResponseModel.IsShowPopup = true;
            }
         }
      }

      protected void HandleReload()
      {
         ResponseModel.IsShowPopup = false;
         NavigationManager.NavigateTo($"/merchant-edit-info/{Id}");
      }

      protected void HandleMerchantNameChanged(string newValue)
      {
         MerchantModel.Name = newValue;
      }

      protected void HandleTaxCodeChanged(string newValue)
      {
         MerchantModel.TaxCode = newValue;
      }

      protected void HandlePhoneNumberChanged(string newValue)
      {
         MerchantModel.PhoneNumber = newValue;
      }

      protected void HandleSearchKeyChanged(string newValue)
      {
         MerchantModel.SearchKey = newValue;
      }

      protected void HandleDescriptionChanged(string newValue)
      {
         MerchantModel.Description = newValue;
      }

      protected async Task HandleOpenTimeChanged(string newTimeString)
      {
         if (string.IsNullOrWhiteSpace(newTimeString) == false)
         {

            MerchantModel.OpenTime.TimeString = newTimeString;
         }
         await InvokeAsync(StateHasChanged);
      }

      protected async Task HandleCloseTimeChanged(string newTimeString)
      {
         if (string.IsNullOrWhiteSpace(newTimeString) == false)
         {
            MerchantModel.CloseTime.TimeString = newTimeString;
         }
         await InvokeAsync(StateHasChanged);
      }

      protected void HandleSelectedCityChanged(CityDto newValue)
      {
         MerchantModel.City = newValue;
      }

      protected void HandleSelectedDistrictChanged(DistrictDto newValue)
      {
         MerchantModel.District = newValue;
      }

      protected void HandleSelectedWardChanged(WardDto newValue)
      {
         MerchantModel.Ward = newValue;
      }

      protected void HandleSelectedMerTypeChanged(IEnumerable<MerTypeDto> newValue)
      {
         Logger.LogInformation("HandleSelectedMerTypeChanged" + newValue.Count());
         if (MerchantModel.MerTypeList.Count() > Const.MaxMerTypePerMerchant)
         {
            StateHasChanged();
         }
         else
         {
            MerchantModel.MerTypeList = newValue.ToList();
         }
      }

      protected void HandleSelectedFeaturedDishChanged(IEnumerable<FeaturedDishDto> newValue)
      {
         if (MerchantModel.FeaturedDishList.Count() > Const.MaxFeaturedDishPerMerchant)
         {
            StateHasChanged();
         }
         else
         {
            MerchantModel.FeaturedDishList = newValue.ToList();
         }
      }

      protected void HandleHouseNumberChanged(string newValue)
      {
         MerchantModel.HouseNumber = newValue;
      }

      protected void HandleAvatarChanged(string newValue)
      {
         MerchantModel.Avatar = newValue;
      }

      protected void HandleCoverPictureChanged(string newValue)
      {
         MerchantModel.CoverPicture = newValue;
      }

      #endregion

      #region Methods
      protected async Task<IEnumerable<MerTypeDto>> HandleLoadMerchantTypeAsync(CancellationToken _ = default)
      {
         var result = await HttpClientServer.CustomGetAsync<IEnumerable<MerTypeDto>>("api/mertype");
         if (MerchantModel.MerTypeList.Count > 0)
         {
            for (int index = 0; index < MerchantModel.MerTypeList.Count; index++)
            {
               MerchantModel.MerTypeList[index] =
                  result.SingleOrDefault(e => e.Id == MerchantModel.MerTypeList[index].Id);
            }
         }
         return result;
      }

      protected async Task<IEnumerable<FeaturedDishDto>> HandleLoadFeaturedDishCategoryAsync(CancellationToken _ = default)
      {
         var result = await HttpClientServer.CustomGetAsync<IEnumerable<FeaturedDishDto>>("api/featureddish");
         if (MerchantModel.FeaturedDishList.Count > 0)
         {
            for (int index = 0; index < MerchantModel.FeaturedDishList.Count; index++)
            {
               MerchantModel.FeaturedDishList[index] =
                  result.SingleOrDefault(e => e.Id == MerchantModel.FeaturedDishList[index].Id);
            }
         }
         return result;
      }

      protected async Task<IEnumerable<CityDto>> HandleLoadCitiesAsync(CancellationToken _ = default)
      {
         return await HttpClientServer.CustomGetAsync<IEnumerable<CityDto>>("api/city");
      }

      protected async Task<IEnumerable<DistrictDto>> HandleLoadDistrictsAsync(CancellationToken _ = default)
      {
         return await HttpClientServer.CustomGetAsync<IEnumerable<DistrictDto>>("api/district");
      }

      protected async Task<IEnumerable<WardDto>> HandleLoadWardsAsync(CancellationToken _ = default)
      {
         return await HttpClientServer.CustomGetAsync<IEnumerable<WardDto>>("api/ward");
      }
      #endregion
   }
}