#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace CoffeeCompany.DataAccess.Models	
{
	public partial class Reports
	{
		private int _reportID;
		public virtual int ReportID
		{
			get
			{
				return this._reportID;
			}
			set
			{
				this._reportID = value;
			}
		}
		
		private string _productName;
		public virtual string ProductName
		{
			get
			{
				return this._productName;
			}
			set
			{
				this._productName = value;
			}
		}
		
		private int _price;
		public virtual int Price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}
		
		private int _numberOfOrders;
		public virtual int NumberOfOrders
		{
			get
			{
				return this._numberOfOrders;
			}
			set
			{
				this._numberOfOrders = value;
			}
		}
		
		private int _totalRevenue;
		public virtual int TotalRevenue
		{
			get
			{
				return this._totalRevenue;
			}
			set
			{
				this._totalRevenue = value;
			}
		}
		
	}
}
#pragma warning restore 1591
