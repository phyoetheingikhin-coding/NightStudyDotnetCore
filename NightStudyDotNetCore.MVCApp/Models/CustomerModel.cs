using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NightStudyDotNetCore.MVCApp.Models
{
    [Table("Tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        public int CustomerId {  get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

    }
}
