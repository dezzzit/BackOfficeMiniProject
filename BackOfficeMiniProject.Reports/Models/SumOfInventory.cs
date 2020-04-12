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
        ///     Gets sum.
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        ///     Gets or brand name.
        /// </summary>
        public string BrandName { get; }
    }
}
