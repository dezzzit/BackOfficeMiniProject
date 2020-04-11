namespace BackOfficeMiniProject.Reports.Models
{
    public class SumOfInventory
    {
        public SumOfInventory(int quantity, string brandName)
        {
            Quantity = quantity;
            BrandName = brandName;
        }

        /// <summary>
        ///     Gets or sets sum .
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        ///     Gets or sets time when order was received.
        /// </summary>
        public string BrandName { get; }
    }
}
