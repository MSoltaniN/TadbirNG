using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPPC.Tadbir.Model.ProductScope
{
    [Table("Version", Schema = "Core")]
    public class Version : PCoreEntity
    {
        [Column("VersionId")]
        public int Id { get; set; }

        [MaxLength(16)]
        public string Number { get; set; }
    }
}
