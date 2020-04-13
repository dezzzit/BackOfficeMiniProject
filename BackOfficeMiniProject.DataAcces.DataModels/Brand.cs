using System.Collections.Generic;

namespace BackOfficeMiniProject.DataAccess.DataModels
{
    /// <summary>
    /// Describes brand's datamodel 
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// Gets or sets identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets brand name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Reference to orders table (1 to many)
        /// </summary>
        public virtual List<Order> Orders { get; set; }
    }
}
