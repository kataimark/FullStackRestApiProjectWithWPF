using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Models
{
    [Table("LolPlayer")]
    public class LolPlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public int Price { get; set; }

        [ForeignKey(nameof(LolTeam))]
        public int LolTeam_Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual LolTeam LolTeam { get; set; }
    }
}
