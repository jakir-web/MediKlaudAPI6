﻿namespace MediKlaudAPI6.FormQuery.Cafeteria
{


    public class GetCafeteriaDataQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? CategoryNo { get; set; }
    }


    public class GetCafeteriaDataListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? ActiveStatus { get; set; }
        public string? CategoryIdName { get; set; }
    }



    public class GetCafeteriaItemDataQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? ItemNo { get; set; }
    }


    public class GetCafeteriaItemDataListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
        public string? ActiveStat { get; set; }
        public string? CategoryNo { get; set; }
        public string? ManufactureNo { get; set; }
        public string? SupplierNo { get; set; }
        public string? ItemIdName { get; set; }
    }

    public class GetAutoGeneratedItemNoQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
    }

    public class GetAllCmbListQuery
    {
        public int? GID { get; set; }
        public int? CID { get; set; }
    }
}
