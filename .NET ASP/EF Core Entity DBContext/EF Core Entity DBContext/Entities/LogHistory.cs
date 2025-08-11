using System.ComponentModel.DataAnnotations;

namespace EF_Core_Entity_DBContext.Entities
{
    public class LogHistory
    {
        public LogHistory(string detail)
        {
            this.Detail = detail;   
        }
        [Key]
        public int Seq { get; set; }    
        public string Detail { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

        
    }
}
