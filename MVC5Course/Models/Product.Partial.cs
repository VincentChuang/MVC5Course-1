namespace MVC5Course.Models
{
    using MVC5Course.Models.DataTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject   //資料模型驗證
    {
        //資料模型驗證 ， 需 extends IValidatableObject
        //若無發生 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (this.Stock < 10 && this.Price > 1000)
                yield return new ValidationResult("Stock < 10 && price > 100 是 錯誤的資料設定",
                    new string[] { "Stock", "Price" });
            //throw new NotImplementedException();
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [身份證字號] //使用欄位驗證，由 身份證字號Attribute.cs 進行驗證
        public string ProductName { get; set; }

        [Required]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

        public Nullable<bool> isDelete { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
