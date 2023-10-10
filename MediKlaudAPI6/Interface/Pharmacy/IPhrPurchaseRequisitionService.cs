using MediKlaudAPI6.FormQuery.Pharmacy;

namespace MediKlaudAPI6.Interface.Pharmacy
{
    public interface IPhrPurchaseRequisitionService
    {
        Task<string> getPhrPurReqDataList(GetPhrPurReqDataListQuery getPhrPurReqDataListQuery);
        Task<string> getCmbCurrStoreList(GetCmbCurrStoreListQuery getCmbCurrStoreListQuery);
        Task<string> getCmbSupplierList(GetCmbSupplierListQuery getCmbSupplierListQuery);
        Task<string> getConfigurationList(GetConfigurationListQuery getConfigurationListQuery);
        Task<string> getCmbToStoreList(GetCmbToStoreListQuery getCmbToStoreListQuery);
        Task<string> getCmbItemList(GetCmbItemListQuery getCmbItemListQuery);
    }
}
