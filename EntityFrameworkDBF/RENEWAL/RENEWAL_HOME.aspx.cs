using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityFrameworkDBF.ServiceReference1;
using System.Net.NetworkInformation;
using System.Configuration;
using Fargo.PrinterSDK;
using System.IO;
using System.Data.SqlClient;

namespace EntityFrameworkDBF.RENEWAL
{
    public partial class RENEWAL_HOME : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        CONTRACTOR_DETAIL cont = new CONTRACTOR_DETAIL();
        APPLICATION_FORM applicationform = new APPLICATION_FORM();
        SmartAccessEntities smartAccess = new SmartAccessEntities();
        EntityFrameworkDBF.ACTIVATION_MODULE.BULKACTIAVTE_HOME bulk = new ACTIVATION_MODULE.BULKACTIAVTE_HOME();
        ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient();
        ServiceReference1.Enroll_Error_Code errcode = new ServiceReference1.Enroll_Error_Code();

        CardDetails objCardDetails = new CardDetails();
        //CardStatus[] objCardStatus = null;
        TemplateStatus[] ObjTemplateStatus = null;
        CardStatus objCardStatus = null;
        DataSet dsDownloadStatus = new DataSet();
        int downloadcount = 0;

        //Printer SDK Part 31-01-2018 Start
        string CardNo = "";
        string PrvRfidno = "";
        string rfidno = "";
        string UpdateCardno;

        //Printer SDK Part 31-01-2018 End

        public byte[] ContPhoto;
        Byte[] ApplicationPhoto;
        byte[] bytes;
        string id = "";
        int count = 0;
        int Cno;
        string Msg;
        DropDownFunction ddl = new DropDownFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            Button1.Visible = false;
            if (!IsPostBack)
            {
                //spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                ddl.BindDesignationddl(ref ddlDesignation);
                ddl.BindStateddl(ref ddlstate);
                ddl.BindNationalityddl(ref ddlNationality);
                ddl.BindFirmddl(ref ddlFirm);
                ddl.BindDocumentddl(ref ddlother);
                ddl.BindGenderddl(ref ddlGender);
                ddl.BindReligionddl(ref ddlReligion);
                ddl.BindPSUddl(ref ddlPSUunit);
                ddl.BindShopddl(ref ddlShop);
                ddl.BindCOntrollerddl(ref ddlDownload);
                PageLoad();
                btnUpdate.Visible = false;
                btnReset.Visible = false;
                btnModify.Visible = false;

                if (Session["photo"] != null)
                {
                    Session["photo"] = null;
                }

                pnlAlreadyImage.Visible = true;
                Btndownlaod.Enabled = false;
            }
            imgPicture.Visible = true;
        }
        /// <summary>
        /// Note In Session["PASSTYPE"] 1 for Contractor ,2 For Escorted ,3 for Bank ,4 for Canteen ,5 For Labour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnissuehome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\DVSC_HOME.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = false;
                LblDownload.Visible = false;
                ControlVisibility();
                EnableControl();
                btnReset.Visible = true;
                btnModify.Visible = true;
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data')</script>");
            }
        }

        public void ControlVisibility()
        {
            int Dockyardid = 0;
            int firmid = 0;
            if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
            {
                Dockyardid = Convert.ToInt32(txtSearch.Text.Trim());
            }
            var Cont = (from x in DVSC.CONTRACTOR_DETAIL
                        where (x.Cont_CancelFLag != "CANCEL") && ((x.Cont_Aadhaar == txtSearch.Text.Trim())
                              || (x.Cont_CardNo == txtSearch.Text.Trim())
                              || (x.Cont_Mobile == (txtSearch.Text.Trim())) ||
                              (x.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                              (x.Cont_Name == (txtSearch.Text.Trim())))
                        select x).First();
            //(x.Cont_DocID == Dockyardid)
            downloadcount = GetDownloadStatus(CardNo);
            if (downloadcount > 0)
                LblDownload.Visible = false;
            else
                LblDownload.Visible = true;

            //var Cont = (from x in DVSC.CONTRACTOR_DETAIL
            //            where (x.Cont_Aadhaar.Contains(txtSearch.Text.Trim()))
            //                  || (x.Cont_CardNo == (txtSearch.Text.Trim()))
            //                  || (x.Cont_Mobile == (txtSearch.Text.Trim())) ||
            //                  (x.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
            //                  (x.Cont_Name == (txtSearch.Text.Trim()))
            //            select x).First();
            // MIN DATE QUERY -- 1 01-11-2017
            //         DataSet DS = ddl.get_data_from_DB("select [Cont_Id],[Cont_CardNo],[earliest date] = Min(aa.Date) from (" +
            //"select [Cont_Id],[Cont_CardNo], Date = [Cont_WOValidity] from [dbo].[CONTRACTOR_DETAIL] where [Cont_WOValidity] is not null union all  " +
            //"select [Cont_Id],[Cont_CardNo], Date = [Cont_PVCValidity] from [dbo].[CONTRACTOR_DETAIL] where [Cont_PVCValidity] is not null union all  " +
            //"select [Cont_Id],[Cont_CardNo], Date = [Cont_RFIDValidity] from [dbo].[CONTRACTOR_DETAIL] where [Cont_RFIDValidity] is not null) aa  " +
            //"where aa.[Cont_CardNo] = '" + Cont.Cont_CardNo + "' group by aa.[Cont_Id],aa.[Cont_CardNo]");

            var MinDate = (from x in DVSC.CONTRACTOR_DETAIL
                           where
                             x.Cont_Id == Cont.Cont_Id
                           select new
                           {
                               x.Cont_DocID,
                               MinDate =
                               x.Cont_RFIDValidity <= x.Cont_PVCValidity &&
                               x.Cont_RFIDValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_RFIDValidity :
                               x.Cont_PVCValidity <= x.Cont_RFIDValidity &&
                               x.Cont_PVCValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_PVCValidity :
                               x.Cont_WOValidity <= x.Cont_RFIDValidity &&
                               x.Cont_WOValidity <= x.Cont_PVCValidity ? (System.DateTime?)x.Cont_WOValidity : null
                           }).ToList();

            //var MinDate = (from x in DVSC.CONTRACTOR_DETAIL
            //               where
            //                 x.Cont_DocID == Cont.Cont_DocID
            //               select new
            //               {
            //                   x.Cont_DocID,
            //                   MinDate =
            //                   x.Cont_RFIDValidity <= x.Cont_PVCValidity &&
            //                   x.Cont_RFIDValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_RFIDValidity :
            //                   x.Cont_PVCValidity <= x.Cont_RFIDValidity &&
            //                   x.Cont_PVCValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_PVCValidity :
            //                   x.Cont_WOValidity <= x.Cont_RFIDValidity &&
            //                   x.Cont_WOValidity <= x.Cont_PVCValidity ? (System.DateTime?)x.Cont_WOValidity : null
            //               }).ToList();

            ViewState["PrevRfid"] = Cont.Cont_RFIDNo;
            PrvRfidno = ViewState["PrevRfid"].ToString();
            if (MinDate[0].MinDate == Cont.Cont_RFIDValidity)
            {
                lblmsg.Text = "RFID date(Validity) is about to expire";
                txtrfidValidity.BorderColor = System.Drawing.Color.Red;
                txtpvcValidity.BorderColor = System.Drawing.Color.Black;
                txtWoValidity.BorderColor = System.Drawing.Color.Black;
            }
            if (MinDate[0].MinDate == Cont.Cont_PVCValidity)
            {
                lblmsg.Text = "PVC date(Validity) is about to expire";
                txtpvcValidity.BorderColor = System.Drawing.Color.Red;
                txtrfidValidity.BorderColor = System.Drawing.Color.Black;
                txtWoValidity.BorderColor = System.Drawing.Color.Black;
            }
            if (MinDate[0].MinDate == Cont.Cont_WOValidity)
            {
                lblmsg.Text = "Work Order date(Validity) is about to expire";
                txtWoValidity.BorderColor = System.Drawing.Color.Red;
                txtpvcValidity.BorderColor = System.Drawing.Color.Black;
                txtrfidValidity.BorderColor = System.Drawing.Color.Black;
            }

            if (Cont.Cont_DocID == null)
            {
            }
            else
            {
                txtDockyardID.Text = Cont.Cont_DocID.ToString();
            }
            if (Cont.Cont_MinDate == null)
            {
            }
            else
            {

            }

            if (Cont.Cont_Aadhaar == null)
            { }
            else
            {
                txtAADHAAR.Text = Cont.Cont_Aadhaar;
            }
            if (Cont.Cont_AppNo == null)
            { }
            else
            {
                txtapplicationno.Text = Cont.Cont_AppNo;
            }
            if (Cont.Cont_Mobile == null)
            { }
            else
            {
                txtContactNo.Text = Cont.Cont_Mobile;
            }
            if (Cont.Cont_Name == null)
            { }
            else
            {
                txtContractorName.Text = Cont.Cont_Name;
            }
            if (Cont.Cont_DOB == null)
            { }
            else
            {
                txtDateOfBirth.Text = Cont.Cont_DOB.ToString();
            }
            if (Cont.Cont_FirmFileNo == null)
            { }
            else
            {
                txtfirmfileno.Text = Cont.Cont_FirmFileNo;
            }
            if (Cont.Cont_FirmWO == null)
            { }
            else
            {
                txtFirmWorkOrderNo.Text = Cont.Cont_FirmWO;
            }
            //txtgst.Text = Aadhaar.Cont_FirmGstNo;
            if (Cont.Cont_PmtAddress == null)
            { }
            else
            {
                txtPmtAdd.Text = Cont.Cont_PmtAddress;
            }
            if (Cont.Cont_PVCNO == null)
            { }
            else
            {
                txtpvcno.Text = Cont.Cont_PVCNO;
            }
            if (Cont.Cont_PVCValidity == null)
            { }
            else
            {
                txtpvcValidity.Text = Cont.Cont_PVCValidity.ToString();
            }
            if (Cont.Cont_RFIDNo == null)
            { }
            else
            {

                //if (Cont.Cont_RFIDNo.Length > 5)
                //{
                //    // ADDED BY MSJ ON 17 JUNE 2019 START
                //    string FirstFive = Cont.Cont_RFIDNo.Substring(0, 5);
                //    if (FirstFive == "00000")
                //    {
                //        txtrfidno.Text = Cont.Cont_RFIDNo.Substring(5, 5);                        
                //    }
                //    else
                //    {
                //        txtrfidno.Text = Cont.Cont_RFIDNo; //.Substring(5, 5);
                //    }
                //    // ADDED BY MSJ ON 17 JUNE 2019 END
                //}
                //else
                //{
                    txtrfidno.Text = Cont.Cont_RFIDNo;
                //}
            }
            if (Cont.Cont_RFIDValidity == null)
            { }
            else
            {
                txtrfidValidity.Text = Cont.Cont_RFIDValidity.ToString();
            }
            if (Cont.Cont_Unit == null)
            { }
            else
            {
                txtunit.Text = Cont.Cont_Unit;
            }
            if (Cont.Cont_WOValidity == null)
            { }
            else
            {
                txtWoValidity.Text = Cont.Cont_WOValidity.ToString();
            }
            if (Cont.Cont_Pin == null)
            { }
            else
            {
                txtPin.Text = Cont.Cont_Pin;
            }
            if (Cont.Cont_District == null)
            { }
            else
            {
                txtDistrict.Text = Cont.Cont_District;
            }
            if (Cont.Cont_Taluka == null)
            { }
            else
            {
                txtTaluka.Text = Cont.Cont_Taluka;
            }
            if (Cont.Cont_DesignationID == null)
            { }
            else
            {
                ddlDesignation.ClearSelection();
                id = Cont.Cont_DesignationID.ToString();
                ddlDesignation.Items.FindByValue(id).Selected = true;
                id = "";
            }
            if (Cont.Cont_FirmID == null)
            { }
            else
            {
                if (Cont.Cont_PassType == "CONTRACTOR" || Cont.Cont_PassType == "ESCORTED" || Cont.Cont_PassType == "LABOUR")
                {
                    {
                        ddlFirm.ClearSelection();
                        id = Cont.Cont_FirmID.ToString();
                        // ddlFirm.Items.FindByValue(id).Selected = true;

                        ddlFirm.SelectedIndex = ddlFirm.Items.IndexOf(ddlFirm.Items.FindByValue(id));
                        var firm = (from x in DVSC.FIRMMASTERs where x.FIRM_ID == Cont.Cont_FirmID select x).First();
                        txtgst.Text = firm.FIRM_GST.Trim();
                    }
                }
            }

            if (Cont.Cont_PSUunitID == null)
            { }
            else
            {
                if (Cont.Cont_PassType == "BANK")
                {
                    {
                        ddlPSUunit.ClearSelection();
                        id = Cont.Cont_PSUunitID.ToString();
                        ddlPSUunit.Items.FindByValue(id).Selected = true;
                        var firm = (from x in DVSC.PSU_MASTER where x.PSU_ID == Cont.Cont_PSUunitID select x).First();
                        txtgst.Text = "NA";
                    }
                }
            }

            if (Cont.Cont_IcardrNo == null)
            {
            }
            else
            {
                txtUnitIcard.Text = Cont.Cont_IcardrNo;
            }

            if (Cont.Cont_NationalityID == null)
            { }
            else
            {
                ddlNationality.ClearSelection();
                id = Cont.Cont_NationalityID.ToString();
                ddlNationality.Items.FindByValue(id).Selected = true;
            }
            if (Cont.Cont_StateID == null)
            { }
            else
            {
                ddlstate.ClearSelection();
                id = Cont.Cont_StateID.ToString();
                ddlstate.Items.FindByValue(id).Selected = true;
            }
            if (Cont.Cont_ReligionID == null)
            { }
            else
            {
                ddlReligion.ClearSelection();
                id = Cont.Cont_ReligionID.ToString();
                ddlReligion.Items.FindByValue(id).Selected = true;
            }
            if (Cont.Cont_Gender == null)
            { }
            else
            {
                ddlGender.ClearSelection();
                ddlGender.Items.FindByText(Cont.Cont_Gender).Selected = true;
            }
            if (Cont.Cont_ShopId == null)
            { }
            else
            {
                if (Cont.Cont_PassType == "CB")
                {
                    {
                        ddlShop.ClearSelection();
                        id = Cont.Cont_ShopId.ToString();
                        ddlShop.Items.FindByValue(id).Selected = true;
                        var firm = (from x in DVSC.SHOP_MASTER where x.SHOP_ID == Cont.Cont_ShopId select x).First();
                        txtgst.Text = "NA";
                    }
                }
            }

            if (Cont.Cont_PVCNO == "NA")
            {
                ddlPvcYN.ClearSelection();
                //id = Cont.Cont_PVCNO;
                ddlPvcYN.SelectedItem.Text = "NO";
            }
            else
            {
                ddlPvcYN.SelectedItem.Text = "YES";
            }
            //ddlother.ClearSelection();
            //ddlother.SelectedItem.Text = Aadhaar.o
            ViewState["Cont_ID"] = Cont.Cont_Id;
            imgPicture.ImageUrl = "~/GetImages.ashx?id=" + Cont.Cont_Id;
            lblContID.Text = Convert.ToString(Cont.Cont_Id);
            if (Cont.Cont_CardNo == null)
            { }
            else
            {
                lblcno.Text = Cont.Cont_CardNo;
                ViewState["Cont_CardNo"] = Cont.Cont_CardNo;
                ViewState["Cont_CardNo"] = Cont.Cont_CardNo;
            }

            BindGriDForApplication(Cont.Cont_Id);
            pnlAlreadyImage.Visible = true;
            if ((Cont.Cont_PassType.Trim() == "CONTRACTOR") || (Cont.Cont_PassType.Trim() == "ESCORTED"))
            {
                BasicDetailDiv.Visible = true;
                lblContID.Visible = false;
                PmtAddressDiv.Visible = true;
                ApplicationDiv.Visible = true;
                ddlPvcYN.Visible = false;
                ValidityDiv.Visible = true;

                DivFirmHeader.Visible = true;
                lblPSUunit.Visible = false;
                ddlPSUunit.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                lblfirmfileno.Visible = true;
                txtfirmfileno.Visible = true;
                lblfirmfileno.Text = "Firm File No";
                lblUnit.Visible = true;
                txtunit.Visible = true;
                lblwovalidity.Visible = true;
                txtWoValidity.Visible = true;
                lblFirmWONo.Visible = true;
                txtFirmWorkOrderNo.Visible = true;
                lblgst.Visible = true;
                txtgst.Visible = true;
                lblfirm.Visible = true;
                ddlFirm.Visible = true;
            }
            if (Cont.Cont_PassType.Trim() == "LABOUR")
            {
                BasicDetailDiv.Visible = true;
                lblContID.Visible = false;
                PmtAddressDiv.Visible = true;
                ApplicationDiv.Visible = true;
                lblPSUunit.Visible = false;
                ddlPSUunit.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                ddlPvcYN.Visible = true;
                ValidityDiv.Visible = true;
                lblfirmfileno.Visible = true;
                txtfirmfileno.Visible = true;
                lblfirmfileno.Text = "Firm File No";
                lblUnit.Visible = true;
                txtunit.Visible = true;
                lblwovalidity.Visible = true;
                txtWoValidity.Visible = true;
                lblFirmWONo.Visible = true;
                txtFirmWorkOrderNo.Visible = true;
                DivFirmHeader.Visible = true;
                lblgst.Visible = true;
                txtgst.Visible = true;
                ddlFirm.Visible = true;
                lblfirm.Visible = true;
            }
            if (Cont.Cont_PassType.Trim() == "BANK")
            {
                BasicDetailDiv.Visible = true;
                lblContID.Visible = false;
                PmtAddressDiv.Visible = true;
                ApplicationDiv.Visible = true;
                lblPSUunit.Visible = true;
                ddlPSUunit.Visible = true;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblUnitIcard.Visible = true;
                txtUnitIcard.Visible = true;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                ddlPvcYN.Visible = true;
                lblgst.Visible = false;
                txtgst.Visible = false;
                lblfirm.Visible = false;
                ddlFirm.Visible = false;
                ValidityDiv.Visible = true;
                lblfirmfileno.Visible = true;
                txtfirmfileno.Visible = true;
                lblfirmfileno.Text = "File No";
                lblUnit.Visible = true;
                txtunit.Visible = true;
                lblwovalidity.Visible = true;
                txtWoValidity.Visible = true;
                lblFirmWONo.Visible = true;
                txtFirmWorkOrderNo.Visible = true;
                DivFirmHeader.Visible = true;
            }
            if (Cont.Cont_PassType.Trim() == "CB")
            {
                BasicDetailDiv.Visible = true;
                lblContID.Visible = false;
                PmtAddressDiv.Visible = true;
                ApplicationDiv.Visible = true;
                lblPSUunit.Visible = false;
                ddlPSUunit.Visible = false;
                lblShop.Visible = true;
                ddlShop.Visible = true;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblUnitEmp.Visible = true;
                txtUnitEmp.Visible = true;
                ddlPvcYN.Visible = false;
                lblgst.Visible = false;
                txtgst.Visible = false;
                lblfirm.Visible = false;
                ddlFirm.Visible = false;
                ValidityDiv.Visible = true;
                lblfirmfileno.Visible = true;
                txtfirmfileno.Visible = true;
                lblfirmfileno.Text = "File No";
                lblUnit.Visible = true;
                txtunit.Visible = true;
                lblwovalidity.Visible = true;
                txtWoValidity.Visible = true;
                lblFirmWONo.Visible = true;
                txtFirmWorkOrderNo.Visible = true;
                DivFirmHeader.Visible = true;
            }
        }

        protected void img_AppPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton lnkbutton = (LinkButton)grdrow.FindControl("img_AppPhoto");
                HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
                int AppID = Convert.ToInt32(hdn.Value);
                Session["APPID"] = AppID;
                var App = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select x).First();
                Session["APPNO"] = App.APP_NUMBER;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "basicPopup();", true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BindGriDForApplication(int id)
        {
            try
            {
                if (DVSC.CANCEL_REASON_MASTER.Count() > 0)
                {
                    Gv_AppPhoto.DataSource = DVSC.APPLICATION_FORM.Where(x => x.CONT_ID == id);
                    Gv_AppPhoto.DataBind();
                }
                else
                {
                    Gv_AppPhoto.DataSource = null;
                    Gv_AppPhoto.DataBind();
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        public void EnableControl()
        {
            txtAADHAAR.Enabled = true;
            txtapplicationno.Enabled = true;
            txtContactNo.Enabled = true;
            txtContractorName.Enabled = true;
            txtDateOfBirth.Enabled = true;
            txtDistrict.Enabled = true;
            txtDockyardID.Enabled = true;
            txtfirmfileno.Enabled = true;
            txtFirmWorkOrderNo.Enabled = true;
            txtgst.Enabled = true;
            txtPin.Enabled = true;
            txtPmtAdd.Enabled = true;
            txtpvcno.Enabled = true;
            txtpvcValidity.Enabled = true;
            txtrfidno.Enabled = true;
            txtrfidValidity.Enabled = true;
            txtTaluka.Enabled = true;
            txtunit.Enabled = true;
            txtUnitEmp.Enabled = true;
            txtUnitIcard.Enabled = true;
            txtWoValidity.Enabled = true;
            txtOtherDoc.Enabled = true;

            ddlDesignation.Enabled = true;
            ddlFirm.Enabled = true;
            ddlGender.Enabled = true;
            ddlNationality.Enabled = true;
            ddlPSUunit.Enabled = true;
            ddlReligion.Enabled = true;
            ddlShop.Enabled = true;
            ddlstate.Enabled = true;
            ddlPvcYN.Enabled = true;
            ddlother.Enabled = true;

            btnapplicationlist.Enabled = true;
            attachappno.Enabled = true;
        }

        protected void rdoRenew_CheckedChanged(object sender, EventArgs e)
        {
            PageLoad();
        }

        protected void rdoModify_CheckedChanged(object sender, EventArgs e)
        {
            PageLoad();
        }

        public void PageLoad()
        {
            BasicDetailDiv.Visible = false;
            lblContID.Visible = false;
            PmtAddressDiv.Visible = false;
            ApplicationDiv.Visible = false;
            ValidityDiv.Visible = false;
            lblPSUunit.Visible = false;
            ddlPSUunit.Visible = false;
            lblShop.Visible = false;
            ddlShop.Visible = false;
            lblUnitIcard.Visible = false;
            txtUnitIcard.Visible = false;
            lblUnitEmp.Visible = false;
            txtUnitEmp.Visible = false;
            lblfirmfileno.Visible = false;
            txtfirmfileno.Visible = false;
            lblUnit.Visible = false;
            txtunit.Visible = false;
            lblwovalidity.Visible = false;
            txtWoValidity.Visible = false;
            lblFirmWONo.Visible = false;
            txtFirmWorkOrderNo.Visible = false;
            lblfirm.Visible = false;
            ddlFirm.Visible = false;
            lblgst.Visible = false;
            txtgst.Visible = false;
            DivFirmHeader.Visible = false;

            txtSearch.Text = "";
            ddlsearch.ClearSelection();
            ddlsearch.SelectedItem.Text = "--SELECT--";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int contid = Convert.ToInt32(ViewState["Cont_ID"]);
            var UpdateData = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == contid);
            try
            {
                if (rdoRenew.Checked == true)
                {

                    if ((!string.IsNullOrEmpty(txtpvcValidity.Text.Trim())) && (!string.IsNullOrEmpty(txtWoValidity.Text.Trim())) && (!string.IsNullOrEmpty(txtrfidValidity.Text.Trim())))
                    {
                        UpdateData.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text.Trim());
                        UpdateData.Cont_WOValidity = Convert.ToDateTime(txtWoValidity.Text.Trim());
                        UpdateData.Cont_RFIDValidity = Convert.ToDateTime(txtrfidValidity.Text.Trim());
                        DVSC.SaveChanges();
                        Response.Write("<script> alert('Pass renewed successfully.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Updating data')</script>");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PageLoad();
        }
        private bool IsValidRFDID()
        {
            bool IsRFDID;
            string RFIDNO = string.Empty;
            if (txtrfidno.Text.Trim().Length > 0)
            {
                if (txtrfidno.Text.Trim().Length == 5)
                    RFIDNO = "00000" + txtrfidno.Text.Trim();
                else
                    RFIDNO = txtrfidno.Text.Trim();

                count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_RFIDNo == RFIDNO);
                IsRFDID = true;
                if (count > 0)
                {
                    var Rfidno = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_RFIDNo == RFIDNO select x).First();
                    // rfidno = Rfidno.Cont_RFIDNo;
                    if (Rfidno.Cont_CardNo == lblcno.Text)
                        IsRFDID = true;
                    else
                    {
                        Response.Write("<script> alert('This  RFIDNO :" + txtrfidno.Text.Trim() + " already assigned with another person , please assign another RFID NO.')</script>");
                        IsRFDID = false;
                    }


                }
            }
            else
            {
                Response.Write("<script> alert('Please enter RFID NO.')</script>");

                IsRFDID = false;
            }
            return IsRFDID;
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            //if (IsValidRFDID() == true)
            //{
            int contid = Convert.ToInt32(ViewState["Cont_ID"]);

            // ADDED BY MSJ ON 14 JUNE 2019 START
            txtrfidno.Text = ConverttoTenDig(txtrfidno.Text.Trim());
            //txtrfidno.Text = ConverttoSixteenDig(txtrfidno.Text.Trim());
            // ADDED BY MSJ ON 14 JUNE 2019 END
            var UpdateData = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == contid);


            ApplicationPhoto = new byte[attachappno.PostedFile.ContentLength];
            attachappno.PostedFile.InputStream.Read(ApplicationPhoto, 0, attachappno.PostedFile.ContentLength);

            pnlAlreadyImage.Visible = false;
            if (pnl.Visible == true)
            {
                ContPhoto = (byte[])Session["photo"];
                if (ContPhoto == null)
                    ContPhoto = (byte[])Session["ImageData"];
            }
            else
            {
                ContPhoto = (byte[])Session["ImageData"];
            }

            Session["App_image"] = ApplicationPhoto;

            try
            {
                count = 0;
                count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_RFIDNo == txtrfidno.Text.Trim());
                if (count > 0)
                {
                    var Rfidno = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_RFIDNo == txtrfidno.Text.Trim() select x).First();
                    rfidno = Rfidno.Cont_RFIDNo;
                    Response.Write("<script> alert('This person " + Rfidno.Cont_Name + " is registered with this " + Rfidno.Cont_CardNo + " Rfid / Card Number.')</script>");
                }
                else
                {
                    if (UpdateData.Cont_RFIDNo != txtrfidno.Text.Trim())
                    {
                        UpdateSmartAccessCardNo(UpdateData.Cont_RFIDNo, txtrfidno.Text.Trim());
                    }
                }
                UpdateData.Cont_Aadhaar = txtAADHAAR.Text.Trim();
                UpdateData.Cont_AppNo = txtapplicationno.Text.Trim();
                if (attachappno.HasFile == false)
                {
                }
                else
                {
                    UpdateData.Cont_AppPhoto = ApplicationPhoto;
                }
                UpdateData.Cont_BlackList = "N";
                UpdateData.Cont_CancelDate = Convert.ToDateTime("01/01/1900"); //System.DateTime.Now.AddYears(-100);
                UpdateData.Cont_CancelFLag = "N";
                UpdateData.Cont_CancelReason = 1;
                UpdateData.Cont_CreatedDate = System.DateTime.Now;
                UpdateData.Cont_DateOFLoss = Convert.ToDateTime("01/01/1900");//System.DateTime.Now.AddYears(-100);
                UpdateData.Cont_Delete_Flag = "N";
                UpdateData.Cont_DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                UpdateData.Cont_District = txtDistrict.Text.Trim();
                UpdateData.Cont_DOB = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
                UpdateData.Cont_Fine = "NA";
                UpdateData.Cont_Fir = "NA";
                if (UpdateData.Cont_PassType == "CONTRACTOR" || UpdateData.Cont_PassType == "ESCORTED" || UpdateData.Cont_PassType == "LABOUR")
                {
                    UpdateData.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                    var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                    int ID = firmid.FIRM_ID;
                    UpdateData.Cont_FirmID = ID;
                }
                else if (UpdateData.Cont_PassType == "BANK")
                {
                    UpdateData.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                    var firmid = (from x in DVSC.PSU_MASTER where x.PSU_FIRMFILENO == txtfirmfileno.Text.Trim() select x).First();
                    int ID = firmid.PSU_ID;
                    UpdateData.Cont_PSUunitID = ID;
                }
                else if (UpdateData.Cont_PassType == "CB")
                {
                    UpdateData.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                    var firmid = (from x in DVSC.SHOP_MASTER where x.FILENO == txtfirmfileno.Text.Trim() select x).First();
                    int ID = firmid.SHOP_ID;
                    UpdateData.Cont_ShopId = ID;
                }
                UpdateData.Cont_FirmWO = txtFirmWorkOrderNo.Text.Trim();
                UpdateData.Cont_Gender = ddlGender.SelectedItem.Text.Trim();
                UpdateData.Cont_IcardrNo = txtUnitIcard.Text.Trim();
                UpdateData.Cont_IssueDate = System.DateTime.Now;
                UpdateData.Cont_Mobile = txtContactNo.Text.Trim();
                UpdateData.Cont_Name = txtContractorName.Text.Trim();
                UpdateData.Cont_NationalityID = Convert.ToInt32(ddlNationality.SelectedValue);

                UpdateData.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text.Trim());
                UpdateData.Cont_WOValidity = Convert.ToDateTime(txtWoValidity.Text.Trim());
                UpdateData.Cont_RFIDValidity = Convert.ToDateTime(txtrfidValidity.Text.Trim());

                if (ContPhoto == null)
                {

                }
                else
                {
                    UpdateData.Cont_Photo = ContPhoto;
                }
                UpdateData.Cont_Pin = txtPin.Text.Trim();
                UpdateData.Cont_PlaceOfLoss = "NA";
                UpdateData.Cont_PmtAddress = txtPmtAdd.Text.Trim();
                if (ddlPSUunit.SelectedItem.Text == "--SELECT--")
                {
                    UpdateData.Cont_PSUunitID = 1;
                }
                else
                {
                    UpdateData.Cont_PSUunitID = Convert.ToInt32(ddlPSUunit.SelectedValue);
                }
                UpdateData.Cont_PVCNO = txtpvcno.Text.Trim();
                UpdateData.Cont_ReligionID = Convert.ToInt32(ddlReligion.SelectedValue);
                UpdateData.Cont_RFIDNo = txtrfidno.Text.Trim();

                UpdateData.Cont_StateID = Convert.ToInt32(ddlstate.SelectedValue);
                UpdateData.Cont_Taluka = txtTaluka.Text.Trim();
                UpdateData.Cont_Unit = txtunit.Text.Trim();
                UpdateData.Cont_UnitEmp = txtUnitEmp.Text.Trim();

                if (Convert.ToDateTime(txtrfidValidity.Text.Trim()) < Convert.ToDateTime(txtpvcValidity.Text.Trim()))
                {
                    UpdateData.Cont_MinDate = Convert.ToDateTime(txtrfidValidity.Text.Trim());
                }
                else
                {
                    UpdateData.Cont_MinDate = Convert.ToDateTime(txtpvcValidity.Text.Trim());
                }

                if (UpdateData.Cont_MinDate < Convert.ToDateTime(txtWoValidity.Text.Trim()))
                {
                    UpdateData.Cont_MinDate = UpdateData.Cont_MinDate;
                }
                else
                {
                    UpdateData.Cont_MinDate = Convert.ToDateTime(txtWoValidity.Text.Trim());
                }

                DVSC.SaveChanges();

                //  GetMinDate(contid);

                var desi = (from x in DVSC.DESIGNATION_MASTER where x.DESIGNATION_ID == UpdateData.Cont_DesignationID select x).FirstOrDefault();
                string Designation = desi.DESIGNATION_NAME;
                string firm = GetFirmName(Convert.ToInt32(UpdateData.Cont_PSUunitID), Convert.ToInt32(UpdateData.Cont_ShopId), Convert.ToInt32(UpdateData.Cont_FirmID));
                GetPrintData(UpdateData.Cont_Aadhaar, UpdateData.Cont_Name, firm, Convert.ToDateTime(UpdateData.Cont_IssueDate).ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper(), Convert.ToDateTime(UpdateData.Cont_MinDate).ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper(), UpdateData.Cont_CardNo, Designation, UpdateData.Cont_Id, UpdateData.Cont_Photo);

                Response.Write("<script> alert('Data modified successfully.')</script>");
                imgPicture.ImageUrl = "~/GetImages.ashx?id=" + UpdateData.Cont_Id;
                pnlAlreadyImage.Visible = true;
                //PrintPass(UpdateData.Cont_CardNo);
                ViewState["Cont_CardNo"] = UpdateData.Cont_CardNo;
                ViewState["Cont_ID"] = UpdateData.Cont_Id;
                // GetMinDate(Convert.ToInt32(txtDockyardID.Text.Trim()));

                // MODIFIED BY MSJ ON 17 JUNE 2019 START
                //txtrfidno.Text = txtrfidno.Text.Substring(5, 5).Trim();
                txtrfidno.Text = txtrfidno.Text.Trim(); //.Substring(5, 5).Trim();
                // MODIFIED BY MSJ ON 17 JUNE 2019 END


                //ClientScript.RegisterStartupScript(this.GetType(), "Popup1", "basicPopup1();", true);



            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Updating data')</script>");
            }
            // }
            //else
            //{
            //    Response.Write("<script> alert('This  RFIDNO :" + txtrfidno.Text.Trim() + " already assigned with another person , please assign another RFID NO.')</script>");
            //}
        }

        public void GetMinDate(int contid)
        {// Session["Cont_CardNo"]
            //var UpdateMinTime = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_DocID == DockyardID);

            // var UpdateData = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == contid);
            var UpdateMinTime = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == contid);
            var MinDate = (from x in DVSC.CONTRACTOR_DETAIL
                           where
                             x.Cont_Id == contid
                           select new
                           {
                               x.Cont_DocID,
                               MinDate =
                               x.Cont_RFIDValidity <= x.Cont_PVCValidity &&
                               x.Cont_RFIDValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_RFIDValidity :
                               x.Cont_PVCValidity <= x.Cont_RFIDValidity &&
                               x.Cont_PVCValidity <= x.Cont_WOValidity ? (System.DateTime?)x.Cont_PVCValidity :
                               x.Cont_WOValidity <= x.Cont_RFIDValidity &&
                               x.Cont_WOValidity <= x.Cont_PVCValidity ? (System.DateTime?)x.Cont_WOValidity : null
                           }).ToList();
            UpdateMinTime.Cont_MinDate = MinDate[0].MinDate;
            DVSC.SaveChanges();

        }

        protected void btnapplicationlist_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblContID.Text);
            if (id > 0)
            {
                //Response.Write("<script> alert('You are adding document for '" + Name + "' And ' Contractor Id is" + n.Cont_Id + "'')</script>");
                SaveApplicationPhoto(id);
            }
            else
            {
                Response.Write("<script> alert('You cannot add document')</script>");
            }
        }

        public void SaveApplicationPhoto(int Contid)
        {
            Session["PDF"] = 0;
            try
            {
                ApplicationPhoto = new byte[attachappno.PostedFile.ContentLength];
                if (attachappno.FileName.Contains(".pdf"))
                {
                    Session["PDF"] = 1;
                    attachappno.PostedFile.InputStream.Read(ApplicationPhoto, 0, attachappno.PostedFile.ContentLength);
                }
                else
                {
                    attachappno.PostedFile.InputStream.Read(ApplicationPhoto, 0, attachappno.PostedFile.ContentLength);
                }
                if (cont.Cont_AppPhoto == null)
                {
                    applicationform.APP_FORM = ApplicationPhoto;
                }
                else
                {
                    applicationform.APP_FORM = cont.Cont_AppPhoto;
                }
                applicationform.APP_NUMBER = txtapplicationno.Text.Trim();
                applicationform.CONT_ID = Contid; //Convert.ToInt32(lblContID.Text);
                applicationform.DELETE_FLAG = "N";
                applicationform.APP_DATE = System.DateTime.Now;
                DVSC.APPLICATION_FORM.AddObject(applicationform);
                DVSC.SaveChanges();
                BindGriDForApplication(applicationform.CONT_ID);
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while saving application form')</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CardNo = ViewState["Cont_CardNo"].ToString();
            PrintPass(CardNo);
        }

        protected void txtfirmfileno_TextChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtDockyardID.Text.Trim());
            var Getdata = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_DocID == id);
            try
            {
                if (Getdata.Cont_PassType == "CONTRACTOR" || Getdata.Cont_PassType == "ESCORTED" || Getdata.Cont_PassType == "LABOUR")
                {
                    count = DVSC.FIRMMASTERs.Count(x => x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() && x.FLAG != "Y");
                    if (count > 0)
                    {
                        var Firm = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        ddlFirm.SelectedItem.Text = Firm.FIRM_NAME;
                        txtgst.Text = Firm.FIRM_GST;
                        ddlFirm.Enabled = false;
                        txtgst.Enabled = false;
                    }
                    else
                    {
                        ddlFirm.Enabled = true;
                        ddl.BindFirmddl(ref ddlFirm);
                        txtgst.Enabled = true;
                        txtgst.Text = "";
                        txtfirmfileno.Text = "";
                        Response.Write("<script> alert('Please register firm or contact to admin.')</script>");
                    }
                }
                else if (Getdata.Cont_PassType == "BANK")
                {
                    count = DVSC.PSU_MASTER.Count(x => x.PSU_FIRMFILENO == txtfirmfileno.Text.Trim() && x.FLAG != "Y");
                    if (count > 0)
                    {
                        var PSU = (from x in DVSC.PSU_MASTER where x.PSU_FIRMFILENO == txtfirmfileno.Text.Trim() select x).First();
                        ddlPSUunit.SelectedItem.Text = PSU.PSU_NAME;
                        txtgst.Text = "";
                        ddlPSUunit.Enabled = false;
                        txtgst.Enabled = false;
                    }
                    else
                    {
                        ddlPSUunit.Enabled = true;
                        ddl.BindPSUddl(ref ddlFirm);
                        txtgst.Enabled = true;
                        txtgst.Text = "";
                        txtfirmfileno.Text = "";
                        Response.Write("<script> alert('Please register firm or contact to admin.')</script>");
                    }
                }
                else if (Getdata.Cont_PassType == "CB")
                {
                    count = DVSC.SHOP_MASTER.Count(x => x.FILENO == txtfirmfileno.Text.Trim() && x.FLAG != "Y");
                    if (count > 0)
                    {
                        var PSU = (from x in DVSC.SHOP_MASTER where x.FILENO == txtfirmfileno.Text.Trim() select x).First();
                        ddlShop.SelectedItem.Text = PSU.SHOP_NAME;
                        txtgst.Text = "";
                        ddlPSUunit.Enabled = false;
                        txtgst.Enabled = false;
                    }
                    else
                    {
                        ddlPSUunit.Enabled = true;
                        ddl.BindPSUddl(ref ddlFirm);
                        txtgst.Enabled = true;
                        txtgst.Text = "";
                        txtfirmfileno.Text = "";
                        Response.Write("<script> alert('Please register firm or contact to admin.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void ddlDownload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDownload.SelectedIndex > 0 && txtrfidno.Text.Trim() != string.Empty)
            {
                AddFig.Enabled = true;
                Btndownlaod.Enabled = true;
            }
            else
            {
                AddFig.Enabled = false;
                Btndownlaod.Enabled = false;
            }
        }

        protected void Btndownlaod_Click(object sender, EventArgs e)
        {
            DDeaciiveSuccess.Visible = false;
            DDeaciiveFail.Visible = false;
            int Dockid = Convert.ToInt32(txtDockyardID.Text);
            int ContID = Convert.ToInt32(lblContID.Text);
            try
            {

                if (lblContID.Text.Trim() != string.Empty)
                {
                    var Cont1 = (from x in DVSC.CONTRACTOR_DETAIL
                                 where (x.Cont_Id == ContID)
                                 select x).First();
                    //var Cont1 = (from x in DVSC.CONTRACTOR_DETAIL
                    //             where (x.Cont_Aadhaar.Contains(txtSearch.Text.Trim()))
                    //   || (x.Cont_CardNo == (txtSearch.Text.Trim()))
                    //   || (x.Cont_Mobile == (txtSearch.Text.Trim())) ||
                    //   (x.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                    //   (x.Cont_Name == (txtSearch.Text.Trim())) ||
                    //   (x.Cont_DocID == Dockid)
                    //             select x).First();

                    // ADDED BY MSJ ON 14 JUNE 2019 START
                    objCardDetails.CardNo = ConverttoTenDig(Cont1.Cont_RFIDNo);
                    //objCardDetails.CardNo = ConverttoSixteenDig(Cont1.Cont_RFIDNo);
                    // ADDED BY MSJ ON 14 JUNE 2019 End


                    objCardDetails.AuthCode = "6";
                    objCardDetails.ControllerNo = ddlDownload.SelectedItem.Value.ToString();
                    objCardDetails.Expiry = Cont1.Cont_MinDate;//Convert.ToDateTime("2018/10/01");//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                    objCardDetails.Name = txtContractorName.Text.Trim();
                    objCardDetails.NoOfReader = 4;

                    objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);

                    Cno = Convert.ToInt32(ddlDownload.SelectedItem.Value);
                    var Ipaddress = (from em in smartAccess.Controllers
                                     where em.ControllerNo == Cno
                                     select new { em.IPAddress, em.ControllerName }).ToList();


                    Ping myPing = new Ping();


                    PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                    if (reply.Status == IPStatus.Success)
                    {
                        ObjTemplateStatus = ObjServiceClient.DownLoadTemplate(objCardDetails.CardNo, objCardDetails.ControllerNo);


                        DDeaciiveFail.Visible = false;

                        ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, objCardDetails.ControllerNo);
                        //ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, Convert.ToInt32(objCardDetails.ControllerNo));
                    }
                    else
                    {
                        Response.Write("<script> alert('CONTROLLER " + Ipaddress[0].ControllerName + " OFFLINE')</script>");
                    }

                    downloadcount = GetDownloadStatus(objCardDetails.CardNo);
                    if (downloadcount > 0)
                    {
                        LblDownload.Visible = false;
                        DDeaciiveSuccess.Visible = true;
                    }
                    else
                    {
                        LblDownload.Visible = true;
                        DDeaciiveSuccess.Visible = false;
                        DDeaciiveSuccess.InnerText = string.Empty;
                    }
                }
            }
            catch (Exception Msg)
            {
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = true;
                ErrorLogFile(txtrfidno.Text.Trim() + " " + Msg);
                Response.Write("<script> alert(' " + Msg + "')</script>");
            }
        }

        protected void AddFig_Click(object sender, EventArgs e)
        {
            if (txtrfidno.Text.Trim().Length > 0 && ddlDownload.SelectedIndex > 0)
            {
                //txtrfidno.Text = txtrfidno.Text.Substring(5, 5);
                Cno = Convert.ToInt32(ddlDownload.SelectedItem.Value);
                errcode = ObjServiceClient.EnrollFinger(txtrfidno.Text.Trim(), Cno.ToString());

                if (errcode.ToString() == "Finger_Time_Out_108")
                    Msg = "FINGER TIMED OUT";
                else if (errcode.ToString() == "Socket_Timed_out")
                    Msg = "SOCKET TIMED OUT OR DEVICE IS OFLINE";
                else if (errcode.ToString() == "Finger_Exists_Code_134")
                    Msg = "FINGER EXIST";
                else if (errcode.ToString() == "User_Enrolled_Successfully_Code_97")
                {
                    Msg = "USER ENROLLED SUCCESSFULLY";
                    Btndownlaod.Enabled = true;
                }
                else
                    Msg = errcode.ToString();
                ErrorLogFile(txtrfidno.Text.Trim() + " " + Msg);
                Response.Write("<script> alert(' " + Msg + "')</script>");
            }
            else
            {
                Response.Write("<script> alert('Controller is Not Selected Or RFIDNO Is not Mention')</script>");
            }
        }

        public void PrintPass(string cardNo)
        {
            CardNo = cardNo;
            ViewState["Cont_CardNo"] = cardNo;
            try
            {
                if (ViewState["Cont_CardNo"] != null)
                {
                    var PrintData = (from c in DVSC.CONTRACTOR_DETAIL
                                     join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                     join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                     join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                     join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                     join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                     join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                     join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                     join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                     where c.Cont_CardNo == cardNo
                                     select new
                                     {
                                         c.Cont_CardNo,
                                         c.Cont_Name,
                                         f.FIRM_NAME,
                                         d.DESIGNATION_NAME,
                                         c.Cont_Aadhaar,
                                         c.Cont_IssueDate,
                                         c.Cont_PassType
                                     }).ToList();

                    if (PrintData[0].Cont_PassType == "CONTRACTOR")
                    {
                        string url = "../Print_ContractorPass.aspx?TranID=" + PrintData[0].Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData[0].Cont_PassType == "ESCORTED")
                    {
                        string url = "../Print_EscortedPass.aspx?TranID=" + PrintData[0].Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData[0].Cont_PassType == "BANK")
                    {
                        string url = "../Print_BankPass.aspx?TranID=" + PrintData[0].Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData[0].Cont_PassType == "CB")
                    {
                        string url = "../Print_DBPass.aspx?TranID=" + PrintData[0].Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData[0].Cont_PassType == "LABOUR")
                    {
                        Response.Write("<script>alert('You cannot print pass for labours.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Loading data.')</script>");
            }
        }

        //protected void txtRFIDNO_TextChanged(object sender, EventArgs e)
        //{
        //    count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_RFIDNo == txtrfidno.Text.Trim());
        //    if (count > 0)
        //    {
        //        var Rfidno = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_RFIDNo == txtrfidno.Text.Trim() select x).First();
        //        rfidno = Rfidno.Cont_RFIDNo;
        //        Response.Write("<script> alert('This person " + Rfidno.Cont_Name + " is already registered with this " + Rfidno.Cont_CardNo + " Rfid / Card Number.')</script>");
        //    }
        //    else
        //    {
        //        if (PrvRfidno != rfidno)
        //        {

        //            UpdateSmartAccessCardNo(PrvRfidno, rfidno);
        //        }
        //    }
        //}

        protected void txtAADHAAR_TextChanged(object sender, EventArgs e)
        {
            if (txtAADHAAR.Text.Trim().Length > 0)
            {
                count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_Aadhaar == txtAADHAAR.Text.Trim());
                var Cancel = DVSC.CONTRACTOR_DETAIL.Count(x => ((x.Cont_CancelFLag == "LOSS" || x.Cont_CancelFLag == "CANCEL") && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()) || (x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()));
                try
                {
                    if (Cancel > 0)
                    {
                        //var CancelFlag = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                        //Response.Write("<script> alert('The card " + CancelFlag.Cont_CardNo + " is already Cancelled / Lost / Deleted, Please view Cancel Report.')</script>");
                    }
                    else
                    {
                        txtAADHAAR.Attributes.Add("pattern", "[0-9]{12}");
                        if (count > 0)
                        {
                            var Aadhaar = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                            Response.Write("<script> alert('This person " + Aadhaar.Cont_Name + " is already registered with this " + Aadhaar.Cont_Aadhaar + " AADHAAR number.')</script>");
                            count = 0;
                            txtAADHAAR.Text = "";
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script> alert('Error while loading data.')</script>");
                }
            }
        }

        void UpdateSmartAccessCardNo(string PrvRfdNo, string RfidNo)
        {
            DeviceTemplate Dt = new DeviceTemplate();
            var data = smartAccess.DeviceTemplates.Count(x => x.CardNo == PrvRfdNo);
            if (data > 0)
            {
                var ControllerList = smartAccess.Controllers.ToList();
                foreach (var item in ControllerList)
                {
                    objCardDetails.CardNo = PrvRfdNo;
                    objCardDetails.Name = "";
                    objCardDetails.AuthCode = "6";
                    objCardDetails.ControllerNo = item.ControllerNo.ToString();
                    objCardDetails.Expiry = Convert.ToDateTime("01-01-1900");
                    objCardDetails.NoOfReader = 1;// ReaderCount;//GvContoller.Rows.Count;
                    objCardStatus = ObjServiceClient.DeactivateCard(objCardDetails);
                }
                ddl.save_data_db("update [SmartAccess].dbo.DeviceTemplates set CardNo='" + RfidNo + "' where CardNo='" + PrvRfdNo + "'");
            }
        }

        public void GetPrintData(string Addhar, string Name, string Firm, string IssueDate, string ExpiryDate, string CardNo, string Designation, int Contid, byte[] Image)
        {
            int count = 0;
            count = DVSC.Print_Card.Count(x => x.Cont_Id == Contid && x.Cont_CardNo == CardNo);
            if (count > 0)
            {
                var save = DVSC.Print_Card.First(x => x.Cont_Id == Contid && x.Cont_CardNo == CardNo);
                Print_Card PrintCard = new Print_Card();
                DataSet ds = new DataSet();
                save.Cont_Name = Name;
                save.FIRM_NAME = Firm;
                save.Cont_IssueDate = IssueDate;
                save.Cont_MinDate = ExpiryDate;
                save.Cont_Photo = Image;
                save.DESIGNATION_NAME = Designation;
                DVSC.SaveChanges();
            }
            else
            {
                Print_Card PrintCard = new Print_Card();
                DataSet ds = new DataSet();
                ds = ddl.get_data_from_DB("select top 1 A.PrintCardID + 1 as PrintCardID from Print_Card A where not exists (select * from Print_Card B where B.PrintCardID = A.PrintCardID + 1)");
                int PrintID = Convert.ToInt32(ds.Tables[0].Rows[0]["PrintCardID"]);
                if (PrintID == 0)
                {
                    PrintID = 1;
                }
                PrintCard.Cont_Aadhaar = Addhar;
                PrintCard.Cont_Name = Name;
                PrintCard.FIRM_NAME = Firm;
                PrintCard.Cont_Id = Contid;
                PrintCard.PrintCardID = PrintID;
                PrintCard.Cont_IssueDate = IssueDate;
                PrintCard.Cont_MinDate = ExpiryDate;
                PrintCard.Cont_CardNo = CardNo;
                PrintCard.DESIGNATION_NAME = Designation;
                DVSC.Print_Card.AddObject(PrintCard);
                DVSC.SaveChanges();
            }
        }

        public string GetFirmName(int Psu, int Shop, int Firm)
        {
            //string Psu1 = "";
            //string Shop1 = "";
            //string Firm1 = "";
            var Psu1 = (from x in DVSC.PSU_MASTER where (x.PSU_ID == Psu) select x).FirstOrDefault();
            var Shop1 = (from x in DVSC.SHOP_MASTER where (x.SHOP_ID == Shop) select x).FirstOrDefault();
            var Firm1 = (from x in DVSC.FIRMMASTERs where (x.FIRM_ID == Firm) select x).FirstOrDefault();
            string dtFirm = "";

            if (Psu1.PSU_NAME != "NA")
                dtFirm = Psu1.PSU_NAME;
            if (Shop1.SHOP_NAME != "NA")
                dtFirm = Shop1.SHOP_NAME;
            if (Firm1.FIRM_NAME != "NA")
                dtFirm = Firm1.FIRM_NAME;
            return dtFirm;
        }

        //// ADDED BY MSJ ON 14 JUNE 2019 START
        //public string ConverttoSixteenDig(string CrdNo)
        //{
        //    UpdateCardno = CrdNo.PadLeft(10, '0');          

        //    return UpdateCardno;
        //}
        //// ADDED BY MSJ ON 14 JUNE 2019 END

        public string ConverttoTenDig(string CrdNo)
        {
            if (CrdNo.Trim().Length == 1)
                UpdateCardno = "000000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 2)
                UpdateCardno = "00000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 3)
                UpdateCardno = "0000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 4)
                UpdateCardno = "000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 5)
                UpdateCardno = "00000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 6)
                UpdateCardno = "0000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 7)
                UpdateCardno = "000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 8)
                UpdateCardno = "00" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 9)
                UpdateCardno = "0" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 10)
                UpdateCardno = CrdNo.Trim();

            return UpdateCardno;
        }

        public string ConverttoFiveDig(string CrdNoDigfive)
        {
            if (CrdNoDigfive.Trim().Length == 10)
                UpdateCardno = CrdNoDigfive.Substring(6, 5).Trim();
            return UpdateCardno;
        }

        public void ErrorLogFile(string strError)
        {
            string path = "";
            path = System.Configuration.ConfigurationManager.AppSettings["MessageLog"].ToString();
            try
            {

                // check if directory exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + DateTime.Today.ToString("dd-MMM-yy") + ".log";
                // check if file exist
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                // log the error now
                using (StreamWriter writer = File.AppendText(path))
                {

                    string error = "\r\nLog written at : " + DateTime.Now.ToString() +
                         "\r\nMessage : " + strError;


                    writer.WriteLine(error);
                    writer.WriteLine("===========================================================================================================================================================");
                    writer.Flush();
                    writer.Close();

                }

            }
            catch
            {
                // throw;
                path = path + DateTime.Today.ToString("ddMMyyhhmmss") + ".log";
                // check if file exist
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                // log the error now
                using (StreamWriter writer = File.AppendText(path))
                {

                    string error = "\r\nLog written at : " + DateTime.Now.ToString() +
                      "\r\nMessage : " + strError;

                    writer.WriteLine(error);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();

                }

            }
            // return strError;
        }

        public void DownloadImage(int Cont_id)
        {
            int id = Cont_id;
            //byte[] bytes1;
            string filename = "";
            string contenttype = "";

            var getData = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Id == Cont_id select new { x.Cont_CardNo, x.Cont_Name, x.Cont_Photo }).ToList();
            if (getData.Count > 0)
            {
                bytes = getData[0].Cont_Photo;
                filename = getData[0].Cont_Name + getData[0].Cont_CardNo;
                contenttype = ".jpg";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contenttype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void LnkDownload_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblContID.Text);
            DownloadImage(id);
        }

        protected void Lnk_DownloadApp_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (sender as LinkButton).NamingContainer as GridViewRow;
            HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey1");
            int AppID = Convert.ToInt32(hdn.Value);
            ViewState["APPID"] = AppID;
            //int id =  //int.Parse((sender as LinkButton).CommandArgument);
            //byte[] bytes1;
            string filename = "";
            string contenttype = "";

            var getData = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select new { x.APP_NUMBER, x.APP_FORM }).ToList();
            if (getData.Count > 0)
            {
                bytes = getData[0].APP_FORM;
                filename = getData[0].APP_NUMBER;
                contenttype = ".jpg";
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contenttype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        public int GetDownloadStatus(string Cardno)
        {
            try
            {
                if (smartAccess.Controllers.Count() > 0)
                {
                    try
                    {
                        SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = SqlCon;
                        using (SqlCon)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                SqlCmd.Parameters.AddWithValue("@CardNo", Cardno);
                                SqlCmd.CommandText = "dbo.GetDownloadStatus";
                                da.SelectCommand = SqlCmd;
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.Fill(dsDownloadStatus);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }

                }
                return Convert.ToInt32(dsDownloadStatus.Tables[0].Rows[0][0]);
            }
            catch (Exception EX)
            {
                return 0;
            }
        }

        protected void Lnk_DeleteApp_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton lnkbutton = (LinkButton)grdrow.FindControl("Lnk_DeleteApp");
                HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey2");
                int APP_ID = Convert.ToInt32(hdn.Value);

                APPLICATION_FORM APP_Form = DVSC.APPLICATION_FORM.First(i => i.APP_ID == APP_ID);
                DVSC.APPLICATION_FORM.DeleteObject(APP_Form);
                DVSC.SaveChanges();
                BindGriDForApplication(Convert.ToInt32(ViewState["Cont_ID"]));
                //Session["APPID"] = AppID;
                //var App = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select x).First();
                ////Session["APPNO"] = App.APP_NUMBER;
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "basicPopup();", true);
            }
            catch (Exception)
            {
                throw;
            }
        }



        //protected void Gv_AppPhoto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        int APP_ID = Convert.ToInt32(Gv_AppPhoto.DataKeys[e.RowIndex].Value);
        //        APPLICATION_FORM APP_Form = DVSC.APPLICATION_FORM.First(i => i.APP_ID == APP_ID);
        //        DVSC.APPLICATION_FORM.DeleteObject(APP_Form);
        //        DVSC.SaveChanges();
        //        BindGriDForApplication(applicationform.CONT_ID);



        //    }
        //    catch (Exception)
        //    {
        //        Response.Write("<script> alert('Error while deleting records please try later.')</script>");
        //    }
        //}


    }
}