namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 20000 && this.Stock > 200)
            {
                yield return new ValidationResult("你買太多高單價的商品了喔!一人限訂作多能買200個!", new string[] {
                "Price","Stock"
                });
            }

            if (this.IsDeleted)
            {
                yield return new ValidationResult("此商品已下架!", new string[] { "IsDeleted" });
            }

            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
