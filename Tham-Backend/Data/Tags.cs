using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Data
{
    public class Tags
    {
        public int Id { get; set; }

        [Required] [StringLength(100)] public string Name { get; set; } = string.Empty;

        public List<ArticleTags> ArticleTags { get; set; }//for FK on ArticleTags table
    }
}
