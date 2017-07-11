using System.Runtime.Serialization;

namespace NorthwindLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

	[DataContract(Name = "CATEGORY", Namespace = "")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }
		[DataMember(Name = "ID", Order = 0)]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
		[DataMember(Name = "NAME", Order = 1)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
		[DataMember(Name = "DESCRIPTION", Order = 2)]
        public string Description { get; set; }

        [Column(TypeName = "PICTURE")]
		[DataMember(Name = "STREET_NUM", Order = 3)]
        public byte[] Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
