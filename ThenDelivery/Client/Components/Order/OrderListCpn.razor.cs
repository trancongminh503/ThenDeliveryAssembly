using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components
{
   public class OrderListCpnBase : CustomComponentBase<OrderListCpnBase>
   {
      [Parameter] public List<OrderDto> OrderList { get; set; }
      public int Count { get; set; }

      protected override void OnInitialized()
      {
         base.OnInitialized();
         Count = 0;
      }
   }
}