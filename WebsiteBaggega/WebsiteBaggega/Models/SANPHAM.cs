//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBaggega.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CHITIETHD = new HashSet<CHITIETHD>();
        }
    
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public Nullable<int> DONGIA { get; set; }
        public string MOTA { get; set; }
        public Nullable<int> SOLUONGTON { get; set; }
        public string HINHANH { get; set; }
        public Nullable<int> MALSP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHD { get; set; }
        public virtual LOAISP LOAISP { get; set; }
    }
}