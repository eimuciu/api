using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GroupController : Controller
    {
        GroupRepository _groupRepository;
        public GroupController(GroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public async Task<List<Group>> GetGroups()
        {
            return await _groupRepository.GetAllGroups();
        }
    }
}