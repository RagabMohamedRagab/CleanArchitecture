using CleanArchitecture.Data.Entities.Entity;
using CleanArchitecture.Service.Dtos.ElasticSearchDtos;
using CleanArchitecture.Service.ElasticSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers.v3
{
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ElasticSeachController(IElasticSearch<DepartmentElasticSearch> elasticSearch, IElasticSearch<StudentDto> elasticSearchStudent) : ControllerBase
    {
        private readonly IElasticSearch<DepartmentElasticSearch> _elasticSearch = elasticSearch;
        private readonly IElasticSearch<StudentDto> _elasticSearchStudent = elasticSearchStudent;
        [HttpPost]
        public async Task<IActionResult> CreateDepartMentDocument([FromBody] DepartmentElasticSearch departMent)
        {
            return Ok(await _elasticSearch.CreateDocument(departMent));
        }
        [HttpGet("{val}")]

        public async Task<IActionResult> SearchElasticSearch(string val)
        {
            return Ok(await _elasticSearch.Search(val));
        }

        [HttpGet("{size}")]
        public async Task<IActionResult> GetAllBaseOnInedex(int size)
        {
            return Ok(await _elasticSearch.GetAll(size));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentElasticSearch department)
        {
            return Ok(await _elasticSearch.Update(department, department.Id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _elasticSearch.Delete(id));
        }
        
    }
}
