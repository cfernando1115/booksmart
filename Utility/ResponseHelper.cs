using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Utility
{
    public static class ResponseHelper
    {
        public static string BookAdded = "Book added successfully.";
        public static string BookUpdated = "Book updated successfully.";
        public static string BookDeleted = "Book deleted successfully.";
        public static string BookAddedToBag = "Book added to bag.";
        public static string BookAlreadyInBag = "Book has already been added to your bag.";
        public static string BookRemovedFromBag = "Book removed from bag.";
        public static string BookIsNotInBag = "Book is not in bag.";
        public static string ShipmentUpdated = "Shipment updated successfully.";
        public static string ShipmentAdded = "Shipment added successfully.";

        public static string BookAddError = "Failed to add book, Please try again.";
        public static string BookUpdateError = "Failed to update book, Please try again.";
        public static string BookDeleteError = "Failed to delete book, Please try again.";
        public static string BookAddToBagError = "Failed to add book to bag, Please try again.";
        public static string BookRemovedFromBagError = "Failed to remove book from bag, Please try again.";
        public static string ShipmentUpdateError = "Failed to update shipment, Please try again.";
        public static string ShipmentAddError = "Failed to add shipment, Please try again.";
        public static string SomethingWentWrong = "Something went wrong, Please try again.";

        public static int SuccessCode = 1;
        public static int FailureCode = 0;
    }
}
