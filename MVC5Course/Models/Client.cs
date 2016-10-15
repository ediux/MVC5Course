//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Orders = new HashSet<Order>();
        }
    
        [DisplayName("客戶端識別碼")]
        public int ClientId { get; set; }
        [Required]
        [StringLength(10,ErrorMessage="{0} 最大只能輸入 {1} 個字")]
        [DisplayName("名")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("中間名")]
        [StringLength(10,ErrorMessage="Middle Name 最大只能輸入10個字")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("姓")]
        [StringLength(5, ErrorMessage = "Last Name 最大只能輸入10個字")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("性別")]
        [RegularExpression("[MF]",ErrorMessage="只能輸入M或F!")]
        public string Gender { get; set; }
        [DisplayName("生日")]
        [DataType( System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [DisplayName("信用等級")]
        [Range(0,9,ErrorMessage="信用等級僅能 0 到 9",ErrorMessageResourceName="CreditRating", ErrorMessageResourceType=typeof(string))]
        public Nullable<double> CreditRating { get; set; }
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public string TelephoneNumber { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }
    
        public virtual Occupation Occupation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
