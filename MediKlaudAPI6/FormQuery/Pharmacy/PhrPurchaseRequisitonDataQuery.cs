namespace MediKlaudAPI6.FormQuery.Pharmacy
{
    public class GetPhrPurReqDataListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public int? ApproveFlag { get; set; }
        public string? StoreNo { get; set; }
        public string? ToStoreNo { get; set; }
        public string? SupplierNo { get; set; }
        public string? PurchaseRequisitionId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class GetCmbCurrStoreListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public int? UserNo { get; set; }
    }



    public class GetCmbSupplierListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
    }


    public class GetConfigurationListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
    }

    public class GetCmbToStoreListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public int? TypeNo { get; set; }
    }

    public class GetCmbItemListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? SupplierNo { get; set; }
        public string? SuppQuotFlag { get; set; }
    }

    //public class GetCmbItemSelectedListQuery
    //{
    //    public int? GID { get; set; }
    //    public int? CID { get; set; }
    //    public string? SupplierNo { get; set; }
    //    public string? StoreNo { get; set; }
    //    public string? ItemNo { get; set; }
    //    public string? SuppQuotFlag { get; set; }
    //}
}
