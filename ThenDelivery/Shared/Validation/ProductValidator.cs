﻿using FluentValidation;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
   public class ProductValidator : AbstractValidator<ProductDto>
   {
      public ProductValidator()
      {
         RuleFor(e => e.Image).NotNull().WithMessage("Vui lòng chọn ảnh đại diện cho món ăn");
         RuleFor(e => e.MenuItem).NotNull().WithMessage("Vui lòng chọn menu cho món ăn");
         RuleFor(e => e.Name).NotNull().WithMessage("Vui lòng nhập tên cho món ăn");
         RuleFor(e => e.UnitPrice).GreaterThanOrEqualTo(Const.MinProductUnitPrice)
            .WithMessage($"Giá bán phải lớn hơn hoặc bằng {Const.MinProductUnitPrice:N0}đ");
      }
   }
}