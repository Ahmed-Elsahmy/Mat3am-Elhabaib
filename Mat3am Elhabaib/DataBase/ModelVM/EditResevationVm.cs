using System.ComponentModel.DataAnnotations;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class EditResevationVm
    {
        [Key]
        public int Id { get; set; }
        public int NumberOftables { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime time { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
