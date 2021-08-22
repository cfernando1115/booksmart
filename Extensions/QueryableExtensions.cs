using BookSmart.Interfaces;
using BookSmart.Models;
using System;
using System.Linq;

namespace BookSmart.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Shipment> ResetUnconfirmedPastShipments(this IQueryable<Shipment> query, IUnitOfWork unitOfWork)
        {
            var unconfirmedPastShipments = query
                .Where(s => s.ShipDate != null && s.ShipDate < DateTime.Now.Date && !s.IsConfirmed);

            if (unconfirmedPastShipments.Any())
            {
                foreach (var shipment in unconfirmedPastShipments)
                {
                    unitOfWork.Shipments.Remove(shipment);
                    query = query.Where(s => s != shipment);
                }
            }

            return query;
        }
    }
}
