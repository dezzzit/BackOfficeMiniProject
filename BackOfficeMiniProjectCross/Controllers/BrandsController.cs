using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeMiniProject.Cache;
using BackOfficeMiniProject.Cache.AppSettings;
using BackOfficeMiniProject.DataAccess.DataModels;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProjectCross.CommonNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BackOfficeMiniProjectCross.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FormatFilter] // api/[controller]?format=xml
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        private readonly CacheList<Brand> _brandCacheList;

        public BrandsController(
            IBrandRepository brandRepository,
            IOptions<CacheSetting> cacheSetting,
            IMemoryCache memoryCache)
        {
            _brandRepository = brandRepository;

            _brandCacheList = new CacheList<Brand>(
                memoryCache,
                _brandRepository.Brands,
                CacheKeys.Brands.BrandsKey,
                cacheSetting.Value.ExpireMinutes);
        }

        // GET api/Brands
        [HttpGet(Name = "GetBrands")]
        [FormatFilter] // api/[controller]?format=xml
        public async Task<IActionResult> GetBrands()
        {
            return await Task.Run(
                () =>
                {
                    IEnumerable<Brand> brands = _brandCacheList.GetValues();

                    if (brands == null)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(brands);

                    return actionResult;
                });
        }

        // GET: api/Brands/[BrandId]
        [HttpGet("{BrandId}", Name = "GetBrand")]
        [FormatFilter] // api/[controller]?format=xml
        public async Task<IActionResult> GetBrand(int brandId)
        {
            return await Task.Run(
                () =>
                {
                    if (brandId < 1)
                    {
                        return BadRequest();
                    }

                    Brand brand = _brandRepository.Get(brandId);

                    if (brand == null)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(brand);

                    return actionResult;
                });
        }

        // POST: api/Brands
        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] Brand brand)
        {
            return await Task.Run(
                () =>
                {
                    if ((brand == null) ||
                        (brand.Id < 1))
                    {
                        return BadRequest();
                    }

                    int added = _brandRepository.Upsert(brand);

                    if (added == 0)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(added);

                    return actionResult;
                });
        }

        // PUT: api/Brands/
        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] Brand brand)
        {
            return await Task.Run(
                () =>
                {
                    if ((brand == null) ||
                        (brand.Id < 1))
                    {
                        return BadRequest();
                    }

                    int updated = _brandRepository.Upsert(brand);

                    if (updated == 0)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(updated);

                    return actionResult;
                });
        }

        // DELETE: api/Brands/[BrandId]
        [HttpDelete("{BrandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            return await Task.Run(
                () =>
                {
                    if (brandId < 1)
                    {
                        return BadRequest();
                    }

                    int deleted = _brandRepository.Delete(brandId);

                    if (deleted == 0)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(deleted);

                    return actionResult;
                });
        }
    }
}
