using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAAPI.Entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

    }
}
