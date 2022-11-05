﻿using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetProductsWithAvailableItems()
        {
            List<ProductDto> result = await _productManager.GetProductsWithAvailableItems();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsWithItems()
        {
            List<ProductDto> result = await _productManager.GetProductsWithItems();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _productManager.InsertProduct(productDto);

                if (!result)
                {
                    ModelState.AddModelError("error", "Error al insertar nuevo producto");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();

        }
    }
}
