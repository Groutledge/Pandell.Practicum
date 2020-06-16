using System;

namespace Pandell.Practicum.App.Domain
{
    public abstract class AbstractDomain : ICloneable
    {
        public DateTime DateInserted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string LastModifiedBy { get; set; }

        protected AbstractDomain CloneCommonProperties(AbstractDomain clonedDomain)
        {
            clonedDomain.DateInserted = DateInserted;
            clonedDomain.DateUpdated = DateUpdated;
            clonedDomain.LastModifiedBy = LastModifiedBy;
            return clonedDomain;
        }
        
        #region Abstract Methods
        
        public abstract object Clone();
        
        #endregion
    }
}