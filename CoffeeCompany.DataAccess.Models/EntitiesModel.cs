﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
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
using CoffeeCompany.DataAccess.Models;

namespace CoffeeCompany.DataAccess.Models	
{
	public partial class MySQLEntitiesModel : OpenAccessContext, IMySQLEntitiesModelUnitOfWork
	{
		private static string connectionStringName = @"MySQLConnection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
				
		private static MetadataSource metadataSource = XmlMetadataSource.FromAssemblyResource("EntitiesModel.rlinq");
		
		public MySQLEntitiesModel()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public MySQLEntitiesModel(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public MySQLEntitiesModel(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public MySQLEntitiesModel(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public MySQLEntitiesModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }
			
		public IQueryable<Reports> Reports 
		{
			get
			{
				return this.GetAll<Reports>();
			}
		}
		
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MySql";
			backend.ProviderName = "MySql.Data.MySqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of MySQLEntitiesModel.
		/// </summary>
		/// <param name="config">The BackendConfiguration of MySQLEntitiesModel.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface IMySQLEntitiesModelUnitOfWork : IUnitOfWork
	{
		IQueryable<Reports> Reports
		{
			get;
		}
	}
}
#pragma warning restore 1591
