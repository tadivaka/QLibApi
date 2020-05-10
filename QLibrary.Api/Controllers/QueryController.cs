using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QLibrary.Dto;
using QLibrary.Api.Concrete;

namespace QLibrary.Api.Controllers
{
   [Route("api/[controller]/[action]")]
   [ApiController]
   public class QueryController : ApiAuthorizedControllerBase //Controller//
   {
      [HttpGet]
      public IEnumerable<QueryDto> Get()
      {
         DataModel ObjModel = new DataModel();
         return ObjModel.GetAllQueries();
      }

      [HttpGet("id")]
      public QueryDto Get(int id)
      {
         DataModel ObjModel = new DataModel();
         return ObjModel.GetQueryByQueryId(id);
      }

      [HttpPost]
      public int Post(QueryDto query)
      {
         DataModel ObjModel = new DataModel();
         return ObjModel.InsertQuery(query);
      }

      [HttpPut]
      public int Put(QueryDto query)
      {
         DataModel ObjModel = new DataModel();
         return ObjModel.UpdateQueryByQueryId(query);
      }

      [HttpDelete]
      public void Delete(int id)
      {
         DataModel ObjModel = new DataModel();
         ObjModel.DeleteQueryByQueryId(id);
      }
   }
}