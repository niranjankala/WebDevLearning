using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebDevAspNet.WebApiCors.Models;

namespace WebDevAspNet.WebApiCors.Controllers
{
    public class ProjectsController : ApiController
    {
        [HttpGet]
        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = null;
            projects = await (Task<List<Project>>.Run(() => new List<Project>()
            {
                new Project(){ Name="Abc", ProjectId=Guid.NewGuid(), Site="Noida"}
            }));
            return projects;
        }
    }
}
