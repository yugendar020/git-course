using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkersApi.Data;
using WalkersApi.DTO;
using WalkersApi.Models.Domain;
using WalkersApi.Repositories;

namespace WalkersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly Walk_DB_Context dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(Walk_DB_Context dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          
           var regions = await regionRepository.GetAllAsync();
            //var regionDTO = new List<Region>();
            //foreach (var region in regions)
            //{
            //    regionDTO.Add(new Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,

            //    });

            //}
              var regionDto= mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionDto);
            
        }
        [HttpGet]
        [Route("{id}")]
        public async Task< IActionResult> Get(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {

                return NotFound();
            }
            
                //var regionDTO= new RegionDTO
                //{
                //    Id=region.Id,
                //    Name = region.Name,
                //    Code = region.Code,
                //    RegionImageUrl = region.RegionImageUrl,
                //};
                var regionDTO= mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
            

        }
        [HttpPost]
        public async Task< IActionResult> Post(AddRegionRequestDto region)
        {
            //AddRegionRequestDto to Region
            var RegionDomainModel = mapper.Map<Region>(region);

            //var RegionDomainModel = new Region
            //{
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,


            //};
            RegionDomainModel= await regionRepository.PostAsync(RegionDomainModel);
           
            //map Domain model to Dto
            //var regionDTO = new RegionDTO
            //{
            //    Id=RegionDomainModel.Id,
            //    Name= RegionDomainModel.Name,
            //    Code = RegionDomainModel.Code,
            //    RegionImageUrl = RegionDomainModel.RegionImageUrl,
            //};
            var regionDTO=mapper.Map<RegionDTO>(RegionDomainModel);
            return CreatedAtAction("Get", new {id=regionDTO.Id},regionDTO);

        }
        //Update method
        //PUT:https://localhost:portnumber/api/region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task< IActionResult> Put(Guid id, UpdateRegionResuestDto regionDTO)
        {
            //updateregionResultDto to region
            var data=mapper.Map<Region>(regionDTO);

            //var data = new Region
            //{
            //    Code = regionDTO.Code,
            //    Name = regionDTO.Name,
            //    RegionImageUrl  = regionDTO.RegionImageUrl,
            //};
            data = await regionRepository.PutAsync(id, data);
            

            //Convert Domain model to dto
            //var regionDto = new RegionDTO
            //{
            //    Id=data.Id,
            //    Name = data.Name, 
            //    Code = data.Code,
            //    RegionImageUrl = data.RegionImageUrl,

            //};
            var regionDto=mapper.Map<RegionDTO>(data);
            return Ok(regionDto);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task< IActionResult> Delete(Guid id)
        {

           var data=await regionRepository.DeleteAsync(id);
            if (data == null)
                return NotFound();
            

            //Convert Domain model to dto
            //var regionDto = new RegionDTO

            //{
            //    Id = data.Id,
            //    Name = data.Name,
            //    Code=data.Code,
            //    RegionImageUrl = data.RegionImageUrl,
            //};
            var regionDto= mapper.Map<RegionDTO>(data);
            return Ok(regionDto);
        }


    }
}
