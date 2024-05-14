using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using test.Repositories;
using System;

namespace test.Controllers
{
    [ApiController]
    [Route("api/team-members")]
    public class TeamMembersController : ControllerBase
    {
        private readonly ILogger<TeamMembersController> _logger;
        private readonly ITeamMemberRepository _teamMemberRepository;

        public TeamMembersController(ILogger<TeamMembersController> logger, ITeamMemberRepository teamMemberRepository)
        {
            _logger = logger;
            _teamMemberRepository = teamMemberRepository;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeamMember(int id)
        {
            try
            {
                int deletedId = _teamMemberRepository.DeleteTeamMember(id);
                return Ok(deletedId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a team member");
                return StatusCode(500, "An error occurred while deleting a team member");
            }
        }
    }
}