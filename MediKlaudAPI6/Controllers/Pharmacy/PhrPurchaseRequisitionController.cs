using MediKlaudAPI6.FormQuery.Pharmacy;
using MediKlaudAPI6.Interface.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace MediKlaudAPI6.Controllers.Pharmacy
{
    [Route("api/[Controller]")]
    [ApiController]

    public class PhrPurchaseRequisitionController : Controller
    {
       private readonly IPhrPurchaseRequisitionService _phrPurchaseRequisitionService;
        public PhrPurchaseRequisitionController(IPhrPurchaseRequisitionService phrPurchaseRequisitionService)
        {
            _phrPurchaseRequisitionService= phrPurchaseRequisitionService;
        }


        [HttpGet]
        [Route("GetPhrPurchaseRequsitionDataList")]
        public  async Task<string> GetPhrPurchaseRequisitionDataList([FromQuery] GetPhrPurReqDataListQuery getPhrPurReqDataListQuery)
        {
            return await _phrPurchaseRequisitionService.getPhrPurReqDataList(getPhrPurReqDataListQuery);
        }


        [HttpGet]
        [Route("GetCmbCurrStoreList")]
        public async Task<string> GetCmbCurrStoreList([FromQuery] GetCmbCurrStoreListQuery getCmbCurrStoreListQuery)
        {
            return await _phrPurchaseRequisitionService.getCmbCurrStoreList(getCmbCurrStoreListQuery);
        }

        [HttpGet]
        [Route("GetCmbSupplierList")]
        public async Task<string> GetCmbSupplierList([FromQuery] GetCmbSupplierListQuery getCmbSupplierListQuery)
        {
            return await _phrPurchaseRequisitionService.getCmbSupplierList(getCmbSupplierListQuery);
        }

        [HttpGet]
        [Route("GetConfigurationList")]
        public async Task<string> GetConfigurationList([FromQuery] GetConfigurationListQuery getConfigurationListQuery)
        {
            return await _phrPurchaseRequisitionService.getConfigurationList(getConfigurationListQuery);
        }


        [HttpGet]
        [Route("GetCmbToStoreList")]
        public async Task<string> GetCmbToStoreList([FromQuery] GetCmbToStoreListQuery getCmbToStoreListQuery)
        {
            return await _phrPurchaseRequisitionService.getCmbToStoreList(getCmbToStoreListQuery);
        }

        [HttpGet]
        [Route("GetCmbItemList")]
        public async Task<string> GetCmbItemLIst([FromQuery] GetCmbItemListQuery getCmbItemListQuery)
        {
            return await _phrPurchaseRequisitionService.getCmbItemList(getCmbItemListQuery);
        }

    }
}
