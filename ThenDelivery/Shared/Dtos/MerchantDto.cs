﻿using System;
using System.Collections.Generic;
using ThenDelivery.Shared.Helper;
using ThenDelivery.Shared.Helper.ExtensionMethods;

namespace ThenDelivery.Shared.Dtos
{
	public class MerchantDto
	{
		public MerchantDto()
		{
			OpenTime = new CustomTime();
			CloseTime = new CustomTime();
			MerchantTypeList = new List<MerTypeDto>();
			FeaturedDishCategoryList = new List<FeaturedDishDto>();
		}

		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public string CoverPicture { get; set; }
		public string TaxCode { get; set; }
		public string PhoneNumber { get; set; }
		public CustomTime OpenTime { get; set; }
		public CustomTime CloseTime { get; set; }
		public string Description { get; set; }
		public string SearchKey { get; set; } // Split by ","
		public CityDto City { get; set; }
		public DistrictDto District { get; set; }
		public WardDto Ward { get; set; }
		public List<FeaturedDishDto> FeaturedDishCategoryList { get; set; }
		public List<MerTypeDto> MerchantTypeList { get; set; }
		public string HouseNumber { get; set; }
		public string AddressString
		{
			get
			{
				if (City != null && District != null && Ward != null)
				{
					return String.Format("{0}, {1}, {2}, {3}", HouseNumber, Ward.DisplayText, District.DisplayText, City.DisplayText);
				}
				else
				{
					return String.Empty;
				}
			}
		}
	}
}
