using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.Reports.Models;

namespace BackOfficeMiniProject.DataAccess.Repository
{
    /// <summary>
    ///     Brand repository.
    /// </summary>
    public interface ISumOfInventoryRepository
    {
        /// <summary>
        /// Gets all inventory sum.
        /// </summary>
        IEnumerable<SumOfInventory> SumOfInventory { get; }
    }
}