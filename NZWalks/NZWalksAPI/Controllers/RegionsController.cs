using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async  Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //Return DTO Region

            //            var regionsDTO = new List<Models.DTO.Region>();
            //            regions.ToList().ForEach(region =>
            //            {
            //                var regionDTO= new Models.DTO.Region()
            //                {
            //Id=region.Id,
            //Code=region.Code,
            //Name=region.Name,
            //Area=region.Area,
            //Lat=region.Lat,
            //Long=region.Long,
            //Population=region.Population

            //                };
            //                regionsDTO.Add(regionDTO);


            //            });
          var regionsDTO= mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult>GetRegionAsync(Guid id)
        {
         var  region= await regionRepository.GetAsync(id);
            if(region==null)
            { 
            return NotFound();
            }

           var regionDTO= mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult>AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Requset DTO to Domain

            var region = new Models.Domain.Region()
            { 
            Code= addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population

            };

            //Pass Details to Repository
          region= await regionRepository.AddAsync(region);


            //Convert Back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = region.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
           var region= await regionRepository.DeleteAsync(id);
            if(region==null)
            { return NotFound(); }
          var  regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population


            };
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult>UpdateRegion([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region=new Models.Domain.Region()
            {
                Code= updateRegionRequest.Code,
                Name= updateRegionRequest.Name,
                Lat= updateRegionRequest.Lat,
                Long= updateRegionRequest.Long,
                Population= updateRegionRequest.Population


            };
           region=  await regionRepository.UpdateAsync(id, region);

            if(region==null) { return NotFound(); }
            var regionDTO =new  Models.DTO.Region(){

                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population

            };
            return Ok(regionDTO);

        }

    }
}
