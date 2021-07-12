using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DCBMS_API.Data;
using DCBMS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace DCBMS_API.Controllers
{
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _rollManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rollManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _rollManager = rollManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(7),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Role = "Admin"
                }) ;
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [Route("Registration")]
        [HttpPost]
        public  IActionResult Registration([FromBody] RegisterModel reg)
        {
            if (ModelState.IsValid && reg.Password != null)
            {
                try
                {
                    Task<IdentityResult> roleResult;
                    //Check that there is an Administrator role and create if not
                    Task<bool> hasAdminRole = _rollManager.RoleExistsAsync("Admin");
                    hasAdminRole.Wait();
                    if (!hasAdminRole.Result)
                    {
                        ApplicationRole roleCreate = new ApplicationRole();
                        roleCreate.Name = "Admin";
                        roleResult = _rollManager.CreateAsync(roleCreate);
                        roleResult.Wait();
                    }
                    //Check if the admin user exists and create it if not
                    Task<ApplicationUser> testUser = _userManager.FindByEmailAsync(reg.Email);
                    testUser.Wait();
                    if (testUser.Result == null)
                    {
                        ApplicationUser administrator = new ApplicationUser();
                        administrator.Email = reg.Email;
                        administrator.UserName = reg.Username;
                        Task<IdentityResult> newUser = _userManager.CreateAsync(administrator, reg.Password);
                        newUser.Wait();
                        if (newUser.Result.Succeeded)
                        {
                            Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(administrator, "Admin");
                            newUserRole.Wait();
                            return Ok();

                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            return BadRequest("Model State is not valid");
        }
      

        [Route("GetUser")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUser()
        {
            return await _userManager.Users.ToListAsync();
        }

        [Route("GetRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationRole>>> GetRole()
        {
            return await _rollManager.Roles.ToListAsync();
        }
       

        [Route("GetRoleById/{id}")]
        [HttpGet]
        public ActionResult<ApplicationRole> GetRoleById(string id)
        {
            var role =  _rollManager.Roles.Where(e=> e.Id == id).FirstOrDefault();
            if (role == null)
            {
                return NotFound();
            }

            return role;
        }


        [Route("RoleCreate/{name}")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(string name)
        {

            ApplicationRole role = new ApplicationRole()
            {
                Name = name,
                NormalizedName = name
            };
            IdentityResult result = await _rollManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok("New Role Created");
            }
            return BadRequest("Sorry for failed");
        }

        [Route("RoleDelete/{name}")]
        [HttpDelete]
        public async Task<ActionResult> RoleDelete(string name)
        {
            ApplicationRole role = await _rollManager.FindByNameAsync(name);

            if (role != null)
            {
                IdentityResult result = await _rollManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }


        [Route("getRoleListForAssign/{email}")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<List<ApplicationRole>>>> getRoleListForAssign(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                List<ApplicationRole> unassignedRole = new List<ApplicationRole>();
                List<ApplicationRole> assignedRole = new List<ApplicationRole>();
                foreach (ApplicationRole role in _rollManager.Roles)
                {
                    //bool res = await _userManager.IsInRoleAsync(user, role.Name);
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        assignedRole.Add(role);
                    }
                    else
                    {
                        unassignedRole.Add(role);
                    }
                }
                return new List<List<ApplicationRole>> { assignedRole, unassignedRole };
            }
            return BadRequest();
        }

        [Route("AssignRoleToUser")]
        [HttpPost]
        public async Task<ActionResult> AssignRoleToUser(AddRoleToUser addRoleToUser)
        {
            var user = await _userManager.FindByIdAsync(addRoleToUser.UserId);
            var role = await _rollManager.FindByIdAsync(addRoleToUser.RoleId);

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok(user.UserName + " is Assigned to " + role.Name + " Role");
            }
            return BadRequest(result.Errors);
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> ChangePassword(UserPasswordModel usermodel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(usermodel.UserId);

            if (user == null)
            {
                return NotFound("User not valid");
            }
            bool yesFound = await _userManager.CheckPasswordAsync(user, usermodel.OldPassword);
            if (!yesFound)
            {
                return NotFound("Incorrect old Password");
            }
            try
            {
                var RemoveResult = await _userManager.RemovePasswordAsync(user);
                if (RemoveResult.Succeeded)
                {
                    var AddResult = await _userManager.AddPasswordAsync(user, usermodel.ConfirmNewPassword);
                    if (AddResult.Succeeded)
                    {
                        return Ok("Successfully Change your Password.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Unfortunately your Password Not Change.");
        }

        [Route("DeleteUser/{id}")]
        [HttpDelete]
       // [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                ApplicationUser ExisttUser = await _userManager.FindByIdAsync(id);
                if (ExisttUser != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(ExisttUser);
                    if (result.Succeeded)
                    {
                       return Ok();

                    }
                }               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Not found your Id");
        }
    }
}
