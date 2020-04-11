using System.Collections.Generic;

namespace BackOfficeMiniProject.DataAccess.DataModels
{
    public class Brand
    {
        /// <summary>
        ///     Gets or sets identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets brand name.
        /// </summary>
        public string Name { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
