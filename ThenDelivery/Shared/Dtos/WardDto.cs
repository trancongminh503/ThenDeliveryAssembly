﻿using System;

namespace ThenDelivery.Shared.Dtos
{
	public class WardDto
	{
		public string WardCode { get; set; }
		public string DistrictCode { get; set; }
		public string Name { get; set; }
		public byte WardLevelId { get; set; }
		public string WardLevelName { get; set; }
		public string DisplayText
		{
			get
			{
				return String.Format("{0} {1}", WardLevelName, Name);
			}
		}
	}
}
