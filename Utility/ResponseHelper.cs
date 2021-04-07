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

        public static string BookAddError = "Failed to add book, Please try again.";
        public static string BookUpdateError = "Failed to update book, Please try again.";
        public static string BookDeleteError = "Failed to delete book, Please try again.";
        public static string BookAddToBagError = "Faile to add book to bag, Please try again.";
        public static string SomethingWentWrong = "Something went wrong, Please try again.";

        public static int SuccessCode = 1;
        public static int FailureCode = 0;
    }
}
