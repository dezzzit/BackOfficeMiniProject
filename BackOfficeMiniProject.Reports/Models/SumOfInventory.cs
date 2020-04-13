namespace BackOfficeMiniProject.Reports.Models
{
    /// <summary>
    /// Describes sum of inventory report
    /// </summary>
    public class SumOfInventory
    {
        /// <summary>
        /// Initialization of new SumOfInventory item
        /// </summary>
        /// <param name="quantity">Sum of brand's items </param>
        /// <param name="brandName">Brand's name</param>
        public SumOfInventory(int quantity, string brandName)
        {
            Quantity = quantity;
            BrandName = brandName;
        }

        /// <summary>
        /// Gets sum of brand's items 
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Gets brand's name
        /// </summary>
        public string BrandName { get; }
    }
}
