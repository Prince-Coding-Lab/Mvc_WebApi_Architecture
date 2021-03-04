using ApplicationCore.Dto;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public sealed class ProductService : IProductService
	{
		#region Fields
		private int status;
		internal IDataAccessHelper _dataHelper = null;
		#endregion

		#region Constructors
		public ProductService(IDataAccessHelper dataHelper)
		{
			_dataHelper = dataHelper;
		}
		#endregion

		#region Public Methods
		public async Task<DatabaseResponse> CreateProductAsync(CreateProductDto productDto)
		{
			string query = $"insert into product (id, name, description, price, deliveryprice) values ('{productDto.Id}', '{productDto.Name}', '{productDto.Description}', {productDto.Price}, {productDto.DeliveryPrice})";
			_dataHelper.Command(query);

			int affectedrows = await _dataHelper.RunAsync();
			if (affectedrows > 0)
			{
				status = (int)DbReturnValue.CreateSuccess;
			}
			else
			{
				status = (int)DbReturnValue.CreationFailed;
			}
			return new DatabaseResponse { ResponseCode = status };
		}
		public async Task<DatabaseResponse> GetProductsAsync(string where)
		{
			List<ProductDto> Items = new List<ProductDto>();
			string query = $"select * from product {where}";
			_dataHelper.Command(query);

			SqlDataReader rdr = null;

			rdr = await _dataHelper.RunAsync(true);
			while (rdr.Read())
			{
				Items.Add(new ProductDto()
				{
					Id = (DBNull.Value == rdr["Id"]) ? Guid.NewGuid() : Guid.Parse(rdr["Id"].ToString()),
					Name = rdr["Name"].ToString(),
					Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
					Price = decimal.Parse(rdr["Price"].ToString()),
					DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
				});

			}
			if (Items.Count > 0)
			{
				status = (int)DbReturnValue.RecordExists;
			}
			else
			{
				status = (int)DbReturnValue.NotExists;
			}
			return new DatabaseResponse { ResponseCode = status, Results = Items };
		}
		public Task<DatabaseResponse> UpdateProductAsync(CreateProductDto productDto)
		{
			throw new NotImplementedException();
		}
		public async Task<DatabaseResponse> GetProductAsync(Guid Id)
		{
			List<ProductDto> Items = new List<ProductDto>();
			string query = $"select * from product where id = '{Id}'";
			_dataHelper.Command(query);

			SqlDataReader rdr = null;
			rdr = await _dataHelper.RunAsync(true);

			while (rdr.Read())
			{
				Items.Add(new ProductDto()
				{
					Id = (DBNull.Value == rdr["Id"]) ? Guid.NewGuid() : Guid.Parse(rdr["Id"].ToString()),
					Name = rdr["Name"].ToString(),
					Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
					Price = decimal.Parse(rdr["Price"].ToString()),
					DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
				});

			}
			if (Items.Count > 0)
			{
				status = (int)DbReturnValue.RecordExists;
			}
			else
			{
				status = (int)DbReturnValue.NotExists;
			}
			return new DatabaseResponse { ResponseCode = status, Results = Items };
		}
		#endregion
	}
}
