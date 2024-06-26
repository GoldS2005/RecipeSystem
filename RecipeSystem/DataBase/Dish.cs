//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipeSystem.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            this.Trackings = new HashSet<Tracking>();
        }
    
        public int DishID { get; set; }
        public string TitleDish { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> CaloriesDish { get; set; }
        public Nullable<decimal> ProteinsDish { get; set; }
        public Nullable<decimal> FatsDish { get; set; }
        public Nullable<decimal> СarbohydratesDish { get; set; }
        public Nullable<bool> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
