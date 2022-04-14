using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Data
{
    public class Tags
    {
        public int Id { get; set; }

        [Required][StringLength(100)] public string Name { get; set; }
    }
}
