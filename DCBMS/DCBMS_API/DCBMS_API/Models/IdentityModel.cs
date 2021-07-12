using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DCBMS_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class AddRoleToUser
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

    public class UserPasswordModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password Not match!")]
        public string ConfirmNewPassword { get; set; }
    }
    public class LogInViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class Response
    {
        public Response(HttpStatusCode httpStatusCode)
        {
            this.httpStatusCode = httpStatusCode;
        }
        public Response()
        {
            ttype = "success";
        }
        public HttpStatusCode httpStatusCode;

        public int status { get; set; }
        public int pageno { get; set; }
        public int pagesize { get; set; }
        public int totalcount { get; set; }
        public double totalSum { get; set; }
        public string message { get; set; }
        public dynamic results { get; set; }
        public string returnvalue { get; set; }
        public Boolean HasError { get; set; }
        public string ttype { get; set; }
    }

}
