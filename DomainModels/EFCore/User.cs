using System.ComponentModel.DataAnnotations;

namespace DomainModels.EFCore
{
    public class User : Common
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}