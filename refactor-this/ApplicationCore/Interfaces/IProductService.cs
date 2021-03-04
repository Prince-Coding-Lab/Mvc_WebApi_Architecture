using ApplicationCore.Dto;
using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IProductService
	{
		Task<DatabaseResponse> CreateProductAsync(CreateProductDto productDto);
		Task<DatabaseResponse> UpdateProductAsync(CreateProductDto productDto);
		Task<DatabaseResponse> GetProductAsync(Guid Id);
		Task<DatabaseResponse> GetProductsAsync(string where);
	}
}
