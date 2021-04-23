using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product, ProductDTOs> _productService;

        public ProductsController(IGenericService<Product, ProductDTOs> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return ActionResultInstance(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTOs productDTOs)
        {
            return ActionResultInstance(await _productService.AddAsync(productDTOs));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTOs productDTOs)
        {
            return ActionResultInstance(await _productService.UpdateAsync(productDTOs, productDTOs.ID));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _productService.RemoveAsync(id));
        }

    }
}
