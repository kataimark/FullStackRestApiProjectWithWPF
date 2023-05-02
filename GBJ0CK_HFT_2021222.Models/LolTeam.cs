using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GBJ0CK_HFT_2021222.Models
{
    [Table("LolTeam")]
    public class LolTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Owner { get; set; } //who's the team owner

        [ForeignKey(nameof(LolManager))]
        public int LolManager_Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual LolManager LolManager { get; set; }

        [NotMapped]
        public virtual ICollection<LolPlayer> LolPlayers { get; set; }

        public LolTeam()
        {
            LolPlayers = new HashSet<LolPlayer>();
        }

    }
}