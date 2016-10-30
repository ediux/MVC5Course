using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientLoginViewModel:IValidatableObject
    {
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大只能輸入 {1} 個字")]
        [DisplayName("名")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("中間名")]
        [StringLength(10, ErrorMessage = "Middle Name 最大只能輸入10個字")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("姓")]
        [StringLength(5, ErrorMessage = "Last Name 最大只能輸入10個字")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("性別")]
        [RegularExpression("[MF]", ErrorMessage = "只能輸入M或F!")]
        public string Gender { get; set; }
        [DisplayName("生日")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Gender != "M"|| this.Gender !="F") {
                yield return new ValidationResult("性別輸入格是錯誤!",new string[]{ "Gender"});
            }
        }
    }
}