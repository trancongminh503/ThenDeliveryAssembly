﻿using System;

namespace ThenDelivery.Shared.Dtos
{
	public class DistrictDto
	{
		public string DistrictCode { get; set; }
		public string CityCode { get; set; }
		public string Name { get; set; }
		public byte DistrictLevelId { get; set; }
		public string DistrictLevelName { get; set; }
		public string DisplayText
		{
			get
			{
				return String.Format("{0} {1}", DistrictLevelName, Name);
			}
		}
	}
}
