using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}