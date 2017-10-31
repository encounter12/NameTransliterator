namespace NameTransliterator.Models.IdentityModels
{
    using System;
    using Microsoft.AspNetCore.Identity;

    using NameTransliterator.Models.SystemModels;
    
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }
    }
}
