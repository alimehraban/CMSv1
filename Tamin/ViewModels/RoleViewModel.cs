using Tamin.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tamin.ViewModels
{
    public class RoleEditModel
    {
        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required(ErrorMessage = "نام نقش را وراد کنید")]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}