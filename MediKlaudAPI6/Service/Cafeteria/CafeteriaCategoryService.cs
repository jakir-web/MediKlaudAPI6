using MediKlaudAPI6.FormQuery;
using MediKlaudAPI6.FormQuery.Cafeteria;
using MediKlaudAPI6.Infrastructure;
using MediKlaudAPI6.Interface.Cafeteria;
using MediKlaudAPI6.Models.Cafeteria;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MediKlaudAPI6.Service.Cafeteria
{
    public class CafeteriaCategoryService : ICafeteriaService
    {
        private readonly IMediklaudDBConnection _dbConnection;
        public CafeteriaCategoryService(IMediklaudDBConnection dbConn)
        {
            _dbConnection = dbConn;
        }


        //CafCategory
        //Get
        public async Task<string> getCafeteriaData(GetCafeteriaDataQuery dataQuery)
        {

            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "pkg_pharmacy.sp_phrs_ItemMaster_data";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_item_no", OracleDbType.Int64, 30);
                cmd.Parameters.Add("p_item_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = dataQuery.GID;
                cmd.Parameters["p_cid"].Value = dataQuery.CID;
                cmd.Parameters["p_item_no"].Value = dataQuery.CategoryNo;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetCafeteriaJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetCafeteriaJson(DataTable table)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);
            return JSONresult;
        }


        //Get
        public async Task<string> getCafeteriaDataList(GetCafeteriaDataListQuery dataQuery)
        {

            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "sp_cafcategory_datalist";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_active_stat", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_cafcategoryidname", OracleDbType.Varchar2, 300);
                cmd.Parameters.Add("p_cafcategory_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = dataQuery.GID;
                cmd.Parameters["p_cid"].Value = dataQuery.CID;
                cmd.Parameters["p_active_stat"].Value = dataQuery.ActiveStatus;
                cmd.Parameters["p_cafcategoryidname"].Value = dataQuery.CategoryIdName;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetCafeteriaDataListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetCafeteriaDataListJson(DataTable table)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);
            return JSONresult;
        }


        //Post
        public async Task<String> addCafeteria(CafeteriaSave cafeteria)
        {
            OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());
            try
            {
                using (oracleConnection = new OracleConnection(await _dbConnection.getDBConn()))
                {



                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand("sp_cafcategory_save", oracleConnection))
                    {
                        cmd.BindByName = true;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_action_type", OracleDbType.Int32, cafeteria.ActionType, ParameterDirection.Input);
                        cmd.Parameters.Add("p_caf_category_name", OracleDbType.Varchar2, cafeteria.CafCategoryName, ParameterDirection.Input);
                        cmd.Parameters.Add("p_active_stat", OracleDbType.Varchar2, cafeteria.ActiveStat, ParameterDirection.Input);

                        cmd.Parameters.Add("p_entryby", OracleDbType.Int32, cafeteria.EntryBy, ParameterDirection.Input);
                        cmd.Parameters.Add("p_gid", OracleDbType.Int32, cafeteria.GID, ParameterDirection.Input);
                        cmd.Parameters.Add("p_cid", OracleDbType.Int32, cafeteria.CID, ParameterDirection.Input);
                        cmd.Parameters.Add("p_entryip", OracleDbType.Varchar2, cafeteria.EntryIp, ParameterDirection.Input);

                        cmd.Parameters.Add("p_caf_category_no", OracleDbType.Varchar2, 300, cafeteria.CafCategoryNo, ParameterDirection.InputOutput);
                        cmd.Parameters.Add("p_caf_category_id", OracleDbType.Varchar2, 500, cafeteria.CafCategoryId, ParameterDirection.InputOutput);

                        cmd.Parameters.Add("p_error", OracleDbType.Varchar2, 3000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("p_action", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;

                        
                        cmd.ExecuteNonQuery();
                        oracleConnection.Close();

                        string? p_caf_category_no = cmd.Parameters["p_caf_category_no"].Value.ToString();
                        string? p_caf_category_id = cmd.Parameters["p_caf_category_id"].Value.ToString();


                        string? p_action = cmd.Parameters["p_action"].Value.ToString();
                        string? p_error = cmd.Parameters["p_error"].Value.ToString();



                        return GetCafeteriaJson(p_error);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static String GetCafeteriaJson(string p_error)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(p_error);
            return JSONresult;
        }



        //CafCategoryItem
        //Get
        public async Task<string> getCafeteriaItemData(GetCafeteriaItemDataQuery getCafeteriaDataQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "pkg_cafeteria.sp_itemmaster_data";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_item_no", OracleDbType.Int64, 30);
                cmd.Parameters.Add("p_item_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getCafeteriaDataQuery.GID;
                cmd.Parameters["p_cid"].Value = getCafeteriaDataQuery.CID;
                cmd.Parameters["p_item_no"].Value = getCafeteriaDataQuery.ItemNo;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getCafeteriaItemDataJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getCafeteriaItemDataJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        //Get
        public async Task<string> getCafeteriaItemDataList(GetCafeteriaItemDataListQuery getCafeteriaDataListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.CommandText = "pkg_cafeteria.sp_itemmaster_datalist";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_active_stat", OracleDbType.Varchar2, 30);
                cmd.Parameters.Add("p_category_no", OracleDbType.Varchar2, 300);

                cmd.Parameters.Add("p_manufacturer_no", OracleDbType.Varchar2, 300);
                cmd.Parameters.Add("p_supplier_no", OracleDbType.Varchar2, 300);
                cmd.Parameters.Add("p_itemidname", OracleDbType.Varchar2, 300);
                cmd.Parameters.Add("p_item_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Parameters["p_gid"].Value = getCafeteriaDataListQuery.GID;
                cmd.Parameters["p_cid"].Value = getCafeteriaDataListQuery.CID;
                cmd.Parameters["p_active_stat"].Value = getCafeteriaDataListQuery.ActiveStat;
                cmd.Parameters["p_category_no"].Value = getCafeteriaDataListQuery.CategoryNo;

                cmd.Parameters["p_manufacturer_no"].Value = getCafeteriaDataListQuery.ManufactureNo;
                cmd.Parameters["p_supplier_no"].Value = getCafeteriaDataListQuery.SupplierNo;
                cmd.Parameters["p_itemidname"].Value = getCafeteriaDataListQuery.ItemIdName;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return GetCafeteriaItemDataListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetCafeteriaItemDataListJson(DataTable table)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);
            return JSONresult;
        }


        //Post
        public async Task<string> addCafeteriaItem(CafeteriaItemSave cafeteria)
        {
            string inputDate = "";

            if(string.IsNullOrEmpty(cafeteria.EffectiveDate))
            {
                inputDate = "";
            }
            else
            {
                string formattedDate = FormatDate(cafeteria.EffectiveDate);
                inputDate = formattedDate;
            }

            int uom;

            if(cafeteria.UomNo == null)
            {
                uom = 10001;
            }
            else
            {
                uom = Convert.ToInt32(cafeteria.UomNo);
            }


            int country;

            if (cafeteria.CountryNo == null)
            {
                country = 1000007;
            }
            else
            {
                country = Convert.ToInt32(cafeteria.CountryNo);
            }            
            
            int supplier;

            if (cafeteria.SupplierNo == null)
            {
                supplier = 3000893;
            }
            else
            {
                supplier = Convert.ToInt32(cafeteria.SupplierNo);
            }

            string mnf = "";

            if (cafeteria.ManufactureNo == "-1")
            {
                mnf = "3000267";
            }
            else
            {
                mnf = cafeteria.ManufactureNo;
            }


            string category = "";

            if (cafeteria.CategoryNo == "-1")
            {
                category = "1000033";
            }
            else
            {
                category = cafeteria.CategoryNo;
            }


            OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());
            try
            {
                using (oracleConnection = new OracleConnection(await _dbConnection.getDBConn()))
                {



                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand("pkg_cafeteria.sp_itemmaster_save", oracleConnection))
                    {
                        cmd.BindByName = true;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_action_type", OracleDbType.Varchar2, cafeteria.ActionType, ParameterDirection.Input);
                        cmd.Parameters.Add("p_item_name", OracleDbType.Varchar2, cafeteria.ItemName, ParameterDirection.Input);
                        cmd.Parameters.Add("p_itemtype_no", OracleDbType.Varchar2, cafeteria.ItemTypeNo, ParameterDirection.Input);
                        cmd.Parameters.Add("p_uom_no", OracleDbType.Varchar2, uom, ParameterDirection.Input);
                        cmd.Parameters.Add("p_manufacturer_no", OracleDbType.Varchar2, mnf, ParameterDirection.Input);
                        cmd.Parameters.Add("p_supplier_no", OracleDbType.Varchar2, supplier, ParameterDirection.Input);
                        cmd.Parameters.Add("p_purchase_price", OracleDbType.Varchar2, cafeteria.PurchasePrice, ParameterDirection.Input);
                        cmd.Parameters.Add("p_sales_price", OracleDbType.Varchar2, cafeteria.SalesPrice, ParameterDirection.Input);
                        cmd.Parameters.Add("p_active_stat", OracleDbType.Varchar2, cafeteria.ActiveStat, ParameterDirection.Input);
                        cmd.Parameters.Add("p_disc_applicable_flag", OracleDbType.Varchar2, cafeteria.DiscountApplicableFlag, ParameterDirection.Input);
                        cmd.Parameters.Add("p_default_discount_pct", OracleDbType.Varchar2, cafeteria.DefaultDiscountPercentage, ParameterDirection.Input);
                        cmd.Parameters.Add("p_max_disc_pct", OracleDbType.Varchar2, cafeteria.MaximumDiscountPercentage, ParameterDirection.Input);
                        cmd.Parameters.Add("p_max_disc_amt", OracleDbType.Varchar2, cafeteria.MaximumDiscountAmount, ParameterDirection.Input);

                        cmd.Parameters.Add("p_category_no", OracleDbType.Varchar2, category, ParameterDirection.Input);
                        cmd.Parameters.Add("p_ipd_sales_price", OracleDbType.Varchar2, cafeteria.IpdSalesPrice, ParameterDirection.Input);
                        cmd.Parameters.Add("p_emp_sales_price", OracleDbType.Varchar2, cafeteria.EmpSalesPrice, ParameterDirection.Input);
                        cmd.Parameters.Add("p_vat", OracleDbType.Varchar2, cafeteria.Vat, ParameterDirection.Input);

                        cmd.Parameters.Add("p_urgent_fee_pct", OracleDbType.Varchar2, cafeteria.UrgentFeePct, ParameterDirection.Input);
                        cmd.Parameters.Add("p_urgent_fee_amt", OracleDbType.Varchar2, cafeteria.IpdUrgentAmt, ParameterDirection.Input);
                        cmd.Parameters.Add("p_ipd_urg_pct", OracleDbType.Varchar2, cafeteria.IpdUrgentPct, ParameterDirection.Input);
                        cmd.Parameters.Add("p_ipd_urg_amt", OracleDbType.Varchar2, cafeteria.IpdUrgentAmt, ParameterDirection.Input);
                        cmd.Parameters.Add("p_ipd_service_pct", OracleDbType.Varchar2, cafeteria.IpdServicePct, ParameterDirection.Input);
                        cmd.Parameters.Add("p_ipd_service_amt", OracleDbType.Varchar2, cafeteria.IpdServiceAmt, ParameterDirection.Input);

                        cmd.Parameters.Add("p_effective_rate", OracleDbType.Varchar2, cafeteria.EffectiveRate, ParameterDirection.Input);
                        cmd.Parameters.Add("p_effective_date", OracleDbType.Varchar2, inputDate, ParameterDirection.Input);
                        cmd.Parameters.Add("p_mov_avg_price", OracleDbType.Varchar2, cafeteria.MovAvgPrice, ParameterDirection.Input);

                        cmd.Parameters.Add("p_saleable_flag", OracleDbType.Varchar2, cafeteria.SaleableFlag, ParameterDirection.Input);
                        cmd.Parameters.Add("p_expireable_flag", OracleDbType.Varchar2, cafeteria.ExpirableFlag, ParameterDirection.Input);
                        cmd.Parameters.Add("p_foreign_flag", OracleDbType.Varchar2, cafeteria.ForeignFlag, ParameterDirection.Input);
                        cmd.Parameters.Add("p_country_no", OracleDbType.Varchar2, country, ParameterDirection.Input);

                        cmd.Parameters.Add("p_descr", OracleDbType.Varchar2, cafeteria.Description, ParameterDirection.Input);
                        cmd.Parameters.Add("p_brand", OracleDbType.Varchar2, cafeteria.Brand, ParameterDirection.Input);
                        cmd.Parameters.Add("p_model_info", OracleDbType.Varchar2, cafeteria.ModelInfo, ParameterDirection.Input);
                        cmd.Parameters.Add("p_origin", OracleDbType.Varchar2, cafeteria.Origin, ParameterDirection.Input);

                        cmd.Parameters.Add("p_entryby", OracleDbType.Int32, cafeteria.EntryBy, ParameterDirection.Input);
                        cmd.Parameters.Add("p_gid", OracleDbType.Int32, cafeteria.GID, ParameterDirection.Input);
                        cmd.Parameters.Add("p_cid", OracleDbType.Int32, cafeteria.CID, ParameterDirection.Input);
                        cmd.Parameters.Add("p_entryip", OracleDbType.Varchar2, cafeteria.EntryIp, ParameterDirection.Input);

                        cmd.Parameters.Add("p_item_no", OracleDbType.Varchar2, 300, cafeteria.ItemNo, ParameterDirection.InputOutput);
                        cmd.Parameters.Add("p_item_id", OracleDbType.Varchar2, 500, cafeteria.ItemId, ParameterDirection.InputOutput);

                        cmd.Parameters.Add("p_error", OracleDbType.Varchar2, 3000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("p_action", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        oracleConnection.Close();

                        string? p_item_no = cmd.Parameters["p_item_no"].Value.ToString();
                        string? p_item_id = cmd.Parameters["p_item_id"].Value.ToString();

                        string? p_action = cmd.Parameters["p_action"].Value.ToString();
                        string? p_error = cmd.Parameters["p_error"].Value.ToString();


                        return addCafeteriaItemJson(p_error);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static String addCafeteriaItemJson(string p_error)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(p_error);
            return JSONresult;
        }

        //Item No Generation
        public async Task<string> getAutoGenerateItemNo(GetAutoGeneratedItemNoQuery getAutoGeneratedItemNoQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                oracleConnection.Open();

                cmd.CommandText = "pkg_cafeteria.sp_itemmaster_id_auto_gen";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_id", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Output;

                cmd.Parameters["p_gid"].Value = getAutoGeneratedItemNoQuery.GID;
                cmd.Parameters["p_cid"].Value = getAutoGeneratedItemNoQuery.CID;

                cmd.ExecuteNonQuery();

                string? p_id = cmd.Parameters["p_id"].Value.ToString();

                oracleConnection.Close();

                return getAutoGenerateItemNoJson(p_id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getAutoGenerateItemNoJson(string p_id)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(p_id);
            return JSONresult;
        }

        //Dropdown
        public async Task<string> getUomList(GetAllCmbListQuery getAllCmbListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_cafeteria.sp_unit_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_uom_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getAllCmbListQuery.GID;
                cmd.Parameters["p_cid"].Value = getAllCmbListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getUomListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getUomListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getManufacturerList(GetAllCmbListQuery getAllCmbListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_cafeteria.sp_manufacturer_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_manufacturer_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getAllCmbListQuery.GID;
                cmd.Parameters["p_cid"].Value = getAllCmbListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getManufacturerListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getManufacturerListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getSupplierList(GetAllCmbListQuery getAllCmbListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_cafeteria.sp_supplier_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_supplier_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getAllCmbListQuery.GID;
                cmd.Parameters["p_cid"].Value = getAllCmbListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getSupplierListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getSupplierListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        public async Task<string> getCategoryList(GetAllCmbListQuery getAllCmbListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "pkg_cafeteria.sp_category_list";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_category_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getAllCmbListQuery.GID;
                cmd.Parameters["p_cid"].Value = getAllCmbListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getCategoryListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getCategoryListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }


        public async Task<string> getCounryList(GetAllCmbListQuery getAllCmbListQuery)
        {
            try
            {
                OracleConnection oracleConnection = new OracleConnection(await _dbConnection.getDBConn());

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.CommandText = "sp_country_list ";
                cmd.Parameters.Add("p_gid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_cid", OracleDbType.Int32, 30);
                cmd.Parameters.Add("p_country_list", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters["p_gid"].Value = getAllCmbListQuery.GID;
                cmd.Parameters["p_cid"].Value = getAllCmbListQuery.CID;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                DataTable dt;
                dt = ds.Tables[0];
                return getCountryListJson(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getCountryListJson(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }


        static string FormatDate(string inputDate)
        {
            DateTime dateObject = DateTime.Parse(inputDate);
            string formattedDate = $"{dateObject.Month}/{dateObject.Day}/{dateObject.Year} 12:00:00 AM";
            return formattedDate;
        }
    }
}
