using MediKlaudAPI6.FormQuery.Cafeteria;
using MediKlaudAPI6.FormQuery.Pharmacy;
using MediKlaudAPI6.Infrastructure;
using MediKlaudAPI6.Interface.Pharmacy;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace MediKlaudAPI6.Service.Pharmacy
{
    public class PharPurchaseRequisitionService : IPhrPurchaseRequisitionService
    {
        private readonly IMediklaudDBConnection _connection;

        public PharPurchaseRequisitionService(IMediklaudDBConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> getPhrPurReqDataList(GetPhrPurReqDataListQuery getPhrPurReqDataListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "pkg_pharmacy.sp_phr_pur_req_datalist";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_approve_flag", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_store_no", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_to_store_no", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_supplier_no", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_phr_pur_req_id", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_start_date", OracleDbType.Date, 30);
                cmd.Parameters.Add("p_end_date", OracleDbType.Date, 30);

                cmd.Parameters.Add("p_phr_pur_req_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getPhrPurReqDataListQuery.GID;
                cmd.Parameters["p_cid"].Value = getPhrPurReqDataListQuery.CID;
                cmd.Parameters["p_approve_flag"].Value = getPhrPurReqDataListQuery.ApproveFlag;
                cmd.Parameters["p_store_no"].Value = getPhrPurReqDataListQuery.StoreNo;
                cmd.Parameters["p_to_store_no"].Value = getPhrPurReqDataListQuery.ToStoreNo;
                cmd.Parameters["p_supplier_no"].Value = getPhrPurReqDataListQuery.SupplierNo;
                cmd.Parameters["p_phr_pur_req_id"].Value = getPhrPurReqDataListQuery.PurchaseRequisitionId;
                cmd.Parameters["p_start_date"].Value = getPhrPurReqDataListQuery.StartDate;
                cmd.Parameters["p_end_date"].Value = getPhrPurReqDataListQuery.EndDate;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetPhrPurchaseRequisitionDataListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static string GetPhrPurchaseRequisitionDataListJson(DataTable table)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);
            return JSONresult;
        }

        public async Task<string> getCmbCurrStoreList(GetCmbCurrStoreListQuery getCmbCurrStoreListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_pharmacy.sp_phr_user_wise_store_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_user_no", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_store_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getCmbCurrStoreListQuery.GID;
                cmd.Parameters["p_cid"].Value = getCmbCurrStoreListQuery.CID;
                cmd.Parameters["p_user_no"].Value = getCmbCurrStoreListQuery.UserNo;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getCmbCurrStoreListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getCmbCurrStoreListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }


        public async Task<string> getCmbSupplierList(GetCmbSupplierListQuery getCmbSupplierListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_pharmacy.sp_phr_supplier_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_supplier_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getCmbSupplierListQuery.GID;
                cmd.Parameters["p_cid"].Value = getCmbSupplierListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getCmbSupplierListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getCmbSupplierListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getConfigurationList(GetConfigurationListQuery getConfigurationListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "sp_phr_transaction_config";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_config", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getConfigurationListQuery.GID;
                cmd.Parameters["p_cid"].Value = getConfigurationListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetConfigurationListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string GetConfigurationListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getCmbToStoreList(GetCmbToStoreListQuery getCmbToStoreListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_pharmacy.sp_phr_warehouse_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_type_no", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_store_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getCmbToStoreListQuery.GID;
                cmd.Parameters["p_cid"].Value = getCmbToStoreListQuery.CID;
                cmd.Parameters["p_type_no"].Value = getCmbToStoreListQuery.TypeNo;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetCmbToStoreListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetCmbToStoreListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getCmbItemList(GetCmbItemListQuery getCmbItemListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _connection.getDBConn());

                OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_pharmacy.sp_phr_pur_order_item_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_supplier_no", OracleDbType.Varchar2, 300);
                cmd.Parameters.Add("p_supp_quot_flag", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_item_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getCmbItemListQuery.GID;
                cmd.Parameters["p_cid"].Value = getCmbItemListQuery.CID;
                cmd.Parameters["p_supplier_no"].Value = getCmbItemListQuery.SupplierNo;
                cmd.Parameters["p_supp_quot_flag"].Value = getCmbItemListQuery.SuppQuotFlag;


                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt;
                dt = ds.Tables[0];
                return GetCmbItemListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetCmbItemListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }
    }
}
