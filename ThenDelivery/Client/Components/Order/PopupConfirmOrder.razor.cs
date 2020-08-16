﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Client.ExtensionMethods;

namespace ThenDelivery.Client.Components.Order
{
   public enum DisplayPopup
   {
      OrderConfirm,
      ChangeAddress
   }

   public class PopupConfirmOrderBase : CustomComponentBase<PopupConfirmOrder>
   {
      [Parameter] public List<OrderItem> OrderItemList { get; set; }
      [Parameter] public EventCallback OnClose { get; set; }
      public List<ShippingAddressDto> ShippingAddressList { get; set; }
      public ShippingAddressDto ShippingAddress { get; set; }
      public DisplayPopup SelectedPopup { get; set; }

      public decimal TotalPrice { get; set; }
      public int TotalProduct { get; set; }
      /// <summary>
      /// Temporary shipping fee
      /// </summary>
      public decimal ShippingFee { get; set; } = 10000;
      /// <summary>
      /// Final price will inclue all anothers cast like VAT, trans...
      /// </summary>
      public decimal FinalPrice { get; set; }
      /// <summary>
      /// currently will set Default delivery time
      /// will have service calculate this later
      /// </summary>
      public DateTime DeleveryDateTime { get; set; }

      protected override void OnInitialized()
      {
         base.OnInitialized();
         SelectedPopup = DisplayPopup.OrderConfirm;
         DeleveryDateTime = DateTime.Now;

         UpdateTotalPrice();
         UpdateFinalPrice();
         UpdateTotalProduct();
      }

      protected override async Task OnInitializedAsync()
      {
         ShippingAddressList = await HttpClientServer
            .CustomGetAsync<List<ShippingAddressDto>>("api/ShippingAddress");
         if (ShippingAddressList != null)
         {
            ShippingAddress = ShippingAddressList.FirstOrDefault();
         }
      }

      private void UpdateTotalProduct()
      {
         TotalProduct = OrderItemList.Sum(e => e.Quantity);
      }

      private void UpdateTotalPrice()
      {
         TotalPrice = 0;
         OrderItemList.ForEach(order =>
         {
            TotalPrice += order.OrderItemPrice;
         });
      }

      private void UpdateFinalPrice()
      {
         FinalPrice = TotalPrice + ShippingFee;
      }

      protected async Task HandleCloseConfirm()
      {
         await OnClose.InvokeAsync(null);
      }

      protected void HandleSubmitConfirm()
      {

      }

      protected void HandleChangeShippingAddress()
      {
         SelectedPopup = DisplayPopup.ChangeAddress;
      }

      protected void HandleCloseChangeAddress()
      {
         SelectedPopup = DisplayPopup.OrderConfirm;
      }

      protected void HandleConfirmChangeAddress()
      {
         DeleveryDateTime = DateTime.Now;
         SelectedPopup = DisplayPopup.OrderConfirm;
      }
   }
}