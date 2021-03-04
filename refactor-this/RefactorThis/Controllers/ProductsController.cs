using System;
using System.Net;
using System.Web.Http;
using refactor_this.Models;
using ApplicationCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationCore.Enums;
using refactor_me.Models;

namespace refactor_this.Controllers
{

	[RoutePrefix("products")]
	public class ProductsController : ApiController
	{
		#region Fields
		private readonly IProductService _productService;
		#endregion

		#region Constructors 
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}
		#endregion

		#region new refactor code
		/// <summary>
		/// Get all product
		/// </summary>
		/// <returns>Api Response</returns>
		[Route]
		[HttpGet]
		public async Task<IHttpActionResult> GetAll()
		{
			DatabaseResponse response = await _productService.GetProductsAsync(null);

			return Ok(ApiResponse.OkResult(true, response.Results, (DbReturnValue)response.ResponseCode));
		}

		/// <summary>
		/// Search by product name
		/// </summary>
		/// <param name="name">product name</param>
		/// <returns>Api Response</returns>
		[Route]
		[HttpGet]
		public async Task<IHttpActionResult> SearchByName(string name)
		{
			string where = $"where lower(name) like '%{name.ToLower()}%'";
			DatabaseResponse response = await _productService.GetProductsAsync(where);

			return Ok(ApiResponse.OkResult(true, response.Results, (DbReturnValue)response.ResponseCode));
		}

		/// <summary>
		/// Get a product by id
		/// </summary>
		/// <param name="id">Product unique ID</param>
		/// <returns>Api Response</returns>
		[Route("{id}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetProduct(Guid id)
		{
			DatabaseResponse response = await _productService.GetProductAsync(id);

			return Ok(ApiResponse.OkResult(true, response.Results, (DbReturnValue)response.ResponseCode));
		}

		/// <summary>
		/// Create a product
		/// </summary>
		/// <param name="product">product object</param>
		/// <returns>Api Response</returns>
		[Route]
		[HttpPost]
		public async Task<IHttpActionResult> Create(CreateProductDto product)
		{
			product.Id = Guid.NewGuid();
			DatabaseResponse response = await _productService.CreateProductAsync(product);

			return Ok(ApiResponse.OkResult(true, response.Results, (DbReturnValue)response.ResponseCode));
		}

		#endregion

		#region un-refactor code
		[Route("{id}")]
		[HttpPut]
		public void Update(Guid id, Product product)
		{
			var orig = new Product(id)
			{
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				DeliveryPrice = product.DeliveryPrice
			};

			if (!orig.IsNew)
				orig.Save();
		}

		[Route("{id}")]
		[HttpDelete]
		public void Delete(Guid id)
		{
			var product = new Product(id);
			product.Delete();
		}

		[Route("{productId}/options")]
		[HttpGet]
		public ProductOptions GetOptions(Guid productId)
		{
			return new ProductOptions(productId);
		}

		[Route("{productId}/options/{id}")]
		[HttpGet]
		public ProductOption GetOption(Guid productId, Guid id)
		{
			var option = new ProductOption(id);
			if (option.IsNew)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			return option;
		}

		[Route("{productId}/options")]
		[HttpPost]
		public void CreateOption(Guid productId, ProductOption option)
		{
			option.ProductId = productId;
			option.Save();
		}

		[Route("{productId}/options/{id}")]
		[HttpPut]
		public void UpdateOption(Guid id, ProductOption option)
		{
			var orig = new ProductOption(id)
			{
				Name = option.Name,
				Description = option.Description
			};

			if (!orig.IsNew)
				orig.Save();
		}

		[Route("{productId}/options/{id}")]
		[HttpDelete]
		public void DeleteOption(Guid id)
		{
			var opt = new ProductOption(id);
			opt.Delete();
		}
		#endregion
	}
}
