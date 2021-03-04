using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.EntityModel;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Net.NetworkInformation;

namespace EntityFrameworkDBF
{
    public class DropDownFunction
    {
        VMSEntities DVSC = new VMSEntities();
        SmartAccessEntities SE = new SmartAccessEntities();
        COUNTRY_MASTER country = new COUNTRY_MASTER();
        STATE_MASTER state = new STATE_MASTER();
        FIRMMASTER firm = new FIRMMASTER();
        DESIGNATION_MASTER designation = new DESIGNATION_MASTER();
        CANCEL_REASON_MASTER cancel = new CANCEL_REASON_MASTER();
        PASSTYPE_MASTER passtype = new PASSTYPE_MASTER();
        DOCUMENT_MASTER document = new DOCUMENT_MASTER();
        SHOP_MASTER shop = new SHOP_MASTER();
        int DownloadControllerNo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DownloadControllerNo"]);

        public void BindDesignationddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.DESIGNATION_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "DESIGNATION_ID";
                ddl.DataTextField = "DESIGNATION_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindCOntrollerddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = (from em in SE.Controllers
                                  where em.ControllerNo == DownloadControllerNo
                                  select new { em.ControllerID, em.ControllerName, em.ControllerNo,em.IPAddress }).ToList();
                ddl.DataValueField = "ControllerNo";
                ddl.DataTextField = "ControllerName";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");         
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindPSUddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.PSU_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "PSU_ID";
                ddl.DataTextField = "PSU_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindCountryddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.COUNTRY_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "COUNTRY_ID";
                ddl.DataTextField = "COUNTRY_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindReligionddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.RELIGION_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "R_ID";
                ddl.DataTextField = "R_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindReligionddl1(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.RELIGION_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "R_ID";
                ddl.DataTextField = "R_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--ALL--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindGenderddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.GENDER_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "G_ID";
                ddl.DataTextField = "G_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindRoleddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.ROLEMASTERs.Where(x => x.FLAG == "N");
                ddl.DataValueField = "ROLEID";
                ddl.DataTextField = "ROLENAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindNationalityddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.COUNTRY_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "COUNTRY_ID";
                ddl.DataTextField = "NATIONALITY";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
                ddl.Items.FindByText("INDIAN").Selected = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindStatedReportl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.STATE_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "STATE_ID";
                ddl.DataTextField = "STATE_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--ALL--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindStateddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.STATE_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "STATE_ID";
                ddl.DataTextField = "STATE_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindShopddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.SHOP_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "SHOP_ID";
                ddl.DataTextField = "SHOP_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//--ALL--
        public void BindShopddlReport(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.SHOP_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "SHOP_ID";
                ddl.DataTextField = "SHOP_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--ALL--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//--ALL--
        public void BindFirmddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.FIRMMASTERs.OrderBy(x => x.FIRM_NAME).Where(x => x.FLAG == "N"); //DVSC.FIRMMASTERs.Where(x => x.FLAG == "N").OrderBy(
                ddl.DataValueField = "FIRM_ID";
                ddl.DataTextField = "FIRM_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindFirmddl1(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.FIRMMASTERs.OrderBy(x => x.FIRM_NAME).Where(x => x.FLAG == "N");
                //ddl.DataSource = DVSC.FIRMMASTERs.Where(x => x.FLAG == "N");
                ddl.DataValueField = "FIRM_ID";
                ddl.DataTextField = "FIRM_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--ALL--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindPassTypeddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.PASSTYPE_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "PASSTYPE_ID";
                ddl.DataTextField = "PASSTYPE_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindDocumentddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.DOCUMENT_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "DOCUMENT_ID";
                ddl.DataTextField = "DOCUMENT_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindCancelReasonddl(ref DropDownList ddl)
        {
            try
            {
                ddl.DataSource = DVSC.CANCEL_REASON_MASTER.Where(x => x.FLAG == "N");
                ddl.DataValueField = "CR_ID";
                ddl.DataTextField = "CR_NAME";
                ddl.DataBind();
                ddl.Items.Insert(0, "--SELECT--");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_data_from_DB(string query)
        {
            DataSet sds = new System.Data.DataSet();
            VMSEntities DVSC = new VMSEntities();
            string constring = ConfigurationManager.ConnectionStrings["VMSEntities"].ConnectionString;
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString);
                SqlCommand scmd;
                SqlDataAdapter sadp;
                scon.Open();
                using (scmd = new SqlCommand(query, scon))
                {
                    sadp = new SqlDataAdapter(scmd);
                    sadp.Fill(sds);
                }
                scon.Close();
            }
            catch (Exception ee)
            {
            }
            return sds;
        }

        public void save_data_db(string query)
        {
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                DataSet sds = new System.Data.DataSet();
                scon.Open();
                SqlCommand cmd = new SqlCommand(query, scon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
            }
        }

        //insert into [dbo].[STATE_MASTER] ([STATE_NAME],[FLAG]) values ('NA','Y')
        //insert into [dbo].[SHOP_MASTER] ([SHOP_NAME],[FLAG],[FILENO]) values ('NA','Y','NA')
        //insert into [dbo].[RELIGION_MASTER] ([R_NAME],[FLAG]) values ('NA','Y')
        //insert into [dbo].[PASSTYPE_MASTER] ([PASSTYPE_NAME],[FLAG]) values ('NA','Y')
        //insert into [dbo].[PSU_MASTER] ([PSU_NAME],[FLAG],[PSU_FIRMFILENO]) values ('NA','Y','NA')
        //insert into [dbo].[FIRMMASTER] ([FIRM_NAME],[FLAG],[FIRM_FILE_NO],[FIRM_GST],[FIRM_PROPRITER],[FIRM_ADDRESS]) values ('NA','Y','NA','NA','NA','NA')
        //insert into [dbo].[DOCUMENT_MASTER] ([DOCUMENT_NAME],[FLAG]) values ('NA','Y')
        //insert into [dbo].[DESIGNATION_MASTER] ([DESIGNATION_NAME],[FLAG]) values ('NA','Y')
        //insert into [dbo].[COUNTRY_MASTER] ([COUNTRY_NAME],[FLAG],[NATIONALITY]) values ('NA','Y','NA')
        //insert into [dbo].[CANCEL_REASON_MASTER] ([CR_NAME],[FLAG]) values ('NA','Y')
    }
}