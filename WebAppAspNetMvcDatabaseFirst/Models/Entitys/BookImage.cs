//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppAspNetMvcDatabaseFirst.Models.Entitys
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookImage
    {
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public string FileName { get; set; }
    
        public virtual Book Book { get; set; }
    }
}
