﻿using System;

namespace BackOfficeMiniProject.DataAccess.DataModels
{
    /// <summary>
    /// Describes order for brand contains count of items and received time  
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets time when order was received.
        /// </summary>
        public DateTime TimeReceived { get; set; }

        /// <summary>
        /// Gets or sets quantity of items
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets reference to brand  
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Reference to order (1 to M) 
        /// </summary>
        public virtual Brand Brand { get; set; }
    }
}
