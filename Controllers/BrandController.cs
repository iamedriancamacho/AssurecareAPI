using AssurecareAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssurecareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _brandContext;
        public BrandController(BrandContext brandContext)
        {
            _brandContext = brandContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetResults()
        {
           if (_brandContext == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return await _brandContext.Brands.ToListAsync();
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Brand>> GetResult(string Id)
        {
            Brand? brand = await _brandContext.Brands.FindAsync(Id);
            if (_brandContext == null || brand == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return brand;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
                brand.BrandId = Guid.NewGuid().ToString(); //uniqueIdentifier
                brand.ActiveFlag= true;
                _brandContext.Brands.Add(brand);
                await _brandContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetResult), new { Id = brand.BrandId }, brand);
        }
        [HttpPut]
        public async Task<ActionResult<Brand>> UpdateBrand(String Id, [FromBody] Brand brand) 
        {
            if (Id != brand.BrandId)
            {
                return BadRequest(ModelState);
            }
            else 
            {
                _brandContext.Entry(brand).State = EntityState.Modified;

                try
                {
                    await _brandContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!isDataExist(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    };
                }
                return Ok(brand);
            }
        
        }

        [HttpDelete]
        public async Task<ActionResult<Brand>> DeleteBrand(string Id) 
        {
            if (_brandContext.Brands == null)
            {
                return NotFound(ModelState);
            }
            else
            {
                Brand? temp = await _brandContext.Brands.FindAsync(Id);
                if (temp == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    _brandContext.Brands.Remove(temp);
                    await _brandContext.SaveChangesAsync();
                    return Ok(temp.BrandName + " has been deleted.");
                }
            }
        }
        //checks wether the data exists in the table or not.
        private bool isDataExist(String Id)
        {
            return (_brandContext.Brands?.Any( x=> x.BrandId == Id)).GetValueOrDefault();
        }
        
    }
}
