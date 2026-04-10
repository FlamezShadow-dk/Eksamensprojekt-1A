using System;
using System.Collections.Generic;
using System.Text;

namespace SnapPexOverview.DomainLayer
{
    public class Rental
    {
        public string CompanyName { get; set; }

        public static void StartDate()
        {
            DateOnly startDate = default; // DateOnly might not work; supposedly unavailable for .NET framework
        }

        public static void EndDate()
        {
            DateOnly? endDate = null;
        }
    }
}
