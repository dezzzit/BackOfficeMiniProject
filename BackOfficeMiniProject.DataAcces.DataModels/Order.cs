using System;
using System.Collections.Generic;

namespace BackOfficeMiniProject.DataAccess.DataModels
{
    public class Order
    {
        /// <summary>
        ///     Gets or sets identifier.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        ///     Gets or sets time when order was received.
        /// </summary>
        public DateTime TimeReceived { get; set; }
        /// <summary>
        ///     Gets or sets quantity of items
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        ///     Gets or sets reference to brand  
        /// </summary>
        //public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }


        }
}
