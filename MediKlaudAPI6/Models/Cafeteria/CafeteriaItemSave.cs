namespace MediKlaudAPI6.Models.Cafeteria
{
    public class CafeteriaItemSave
    {
        public int? ActionType { get; set; }
        public string? ItemName { get; set; }
        public int? ItemTypeNo { get; set; }
        public int? UomNo { get; set; }
        public string? ManufactureNo { get; set; }
        public int? SupplierNo { get; set; }
        public int? PurchasePrice { get; set; }
        public int? SalesPrice { get; set; }
        public int? ActiveStat { get; set; }
        public int? DiscountApplicableFlag { get; set; }

        public int? DefaultDiscountPercentage { get; set; }
        public int? MaximumDiscountPercentage { get; set; }
        public int? MaximumDiscountAmount { get; set; }

        public string? CategoryNo { get; set; }
        public int? IpdSalesPrice { get; set; }
        public int? EmpSalesPrice { get; set; }
        public int? Vat { get; set; }


        public int? UrgentFeePct { get; set; }
        public int? UrgentFeeAmt { get; set; }
        public int? IpdUrgentPct { get; set; }
        public int? IpdUrgentAmt { get; set; }
        public int? IpdServicePct { get; set; }
        public int? IpdServiceAmt { get; set; }


        public int? EffectiveRate { get; set; }
        public string? EffectiveDate { get; set; }
        public int? MovAvgPrice { get; set; }


        public int? SaleableFlag { get; set; }
        public int? ExpirableFlag { get; set; }
        public int? ForeignFlag { get; set; }
        public int? CountryNo { get; set; }


        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? ModelInfo { get; set; }
        public string? Origin { get; set; }

        public string? EntryBy { get; set; }
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? EntryIp { get; set; }

        public int? ItemNo { get; set; }
        public string? ItemId { get; set; }
    }
}
