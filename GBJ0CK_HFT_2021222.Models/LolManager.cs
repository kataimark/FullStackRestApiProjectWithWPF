using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Models
{
    [Table("LolManager")]
    public class LolManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public int Employees { get; set; }

        [NotMapped]
        public virtual ICollection<LolTeam> LolTeams { get; set; }

        public LolManager()
        {
            LolTeams = new HashSet<LolTeam>();
        }
    }
}
