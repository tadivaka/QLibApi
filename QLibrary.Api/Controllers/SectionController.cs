using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QLibrary.Dto;
using  QLibrary.Api.Concrete;

namespace QLibrary.Api.Controllers
{
   [Route("api/[controller]/[action]")]
   [ApiController]
    public class SectionController : Controller// ApiAuthorizedControllerBase
    {
       // GET api/<controller>
       [HttpGet]
       public  IEnumerable<SectionDto> Get()
       {
          DataModel ObjModel = new DataModel();
          return ObjModel.GetAllSections();
       }

       // GET api/<controller>/5
       [HttpGet("id")]
       public SectionDto Get(int id)
       {
          DataModel ObjModel = new DataModel();
          return ObjModel.GetSectionBySectionId(id);
       }

       // POST api/<controller>
       [HttpPost]
       public int Post(SectionDto section)
       {
          DataModel ObjModel = new DataModel();
          return ObjModel.InsertSection(section);
       }

       // PUT api/<controller>/5
       [HttpPut]
       public int Put(SectionDto section)
       {
          DataModel ObjModel = new DataModel();
          return ObjModel.UpdateSectionBySectionId(section);
       }

       // DELETE api/<controller>/5
       [HttpDelete]
       public void Delete(int id)
       {
       }
    }
}