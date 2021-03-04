using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Text;
using EntityFrameworkDBF;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using EntityFrameworkDBF.ServiceReference1;
using System.Net.NetworkInformation;
using Fargo.PrinterSDK;
using System.Web.SessionState;

namespace EntityFrameworkDBF.ISSUES_MODULE
{
    public partial class CONTRACTOR : System.Web.UI.Page
    {

        VMSEntities DVSC = new VMSEntities();
        SmartAccessEntities smartAccess = new SmartAccessEntities();
        CONTRACTOR_DETAIL cont = new CONTRACTOR_DETAIL();
        APPLICATION_FORM applicationform = new APPLICATION_FORM();
        VisitorTransactionDetail CV = new VisitorTransactionDetail();
        public byte[] ContPhoto;
        Byte[] ApplicationPhoto;
        byte[] bytes;
        string Name = "";
        int count = 0;
        int PassNo = 0;
        DataSet ContractorId = null;
        DropDownFunction ddl = new DropDownFunction();
        int id = 0;
        string PassType = "";
        string USERNAME = "";
        string Msg;
        int Cno;
        HiddenField hdn;
        string Controllerlist;
        int ReaderCount = 0;
        ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient();
        ServiceReference1.SmartWebServiceClient objclient = new ServiceReference1.SmartWebServiceClient();
        ServiceReference1.Enroll_Error_Code errcode = new ServiceReference1.Enroll_Error_Code();
        CardDetails objCardDetails = new CardDetails();
        //CardStatus[] objCardStatus = null;
        CardStatus objCardStatus = null;
        TemplateStatus[] ObjTemplateStatus = null;

        //Printer SDK Part 31-01-2018 Start
        string[] m_startSentinels;
        string[] m_stopSentinels;
        int x = 0;
        int y = 0;
        int I_X = 0;
        int I_Y = 0;
        int ShiftByX = 0;
        int ShiftByY = 0;
        string ImagePath = "";
        string ImagePathFooter = "";
        string PersonImagePath = "";
        string CardNo = "";
        //Printer SDK Part 31-01-2018 End

        // ServiceReference1.CardDetails objCardDetails = new ServiceReference1.CardDetails();
        // ServiceReference1.CardStatus[] objCardStatus = null;
        // ServiceReference1.TemplateStatus[] ObjTemplateStatus2 = null;

        [WebMethod(EnableSession = true)]
        public static bool SaveCapturedImage(string data)
        {
            byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);
            HttpContext.Current.Session["photo"] = imageBytes;
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PassType = Session["PASSTYPE"].ToString();
            USERNAME = Session["USERNAME"].ToString();
            MaintainScrollPositionOnPostBack = true;
            if (Session["photo"] != null)
            {
                byte[] arr = (byte[])Session["photo"];
                imgPicture.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(arr);
            }
            else if(Session["ImageData"] != null)
            {
                byte[] arr = (byte[])Session["ImageData"];
                imgPicture.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(arr);
            }
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
                //BindUpdateController();
                if (Session["photo"] != null)
                {
                    Session["photo"] = null;
                }
                //pnlAlreadyImage.Visible = false;
                CommanProperty();
                btnDockyardid.Enabled = true;
                AddFig.Enabled = false;
                Btndownlaod.Enabled = false;
                btnapplicationlist.Enabled = false;
                btnsave.Enabled = false;
            }
        }

        protected void btnhome_Click(object sender, EventArgs e)
        {
            Session["photo"]= null;
            Session["ImageData"]= null;
            Response.Redirect("~\\DVSC_HOME.aspx");
        }

        protected void btnpnlhome_Click(object sender, EventArgs e)
        {
            Session["photo"]= null;
            Session["ImageData"]= null;
            Response.Redirect("~\\ISSUES MODULE\\ISSUE_HOME.aspx");
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
			Session["photo"]= null;
            Session["ImageData"]= null;
            try
            {
                Convert.ToDateTime(txtDateOfBirth.Text);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('Please check date and put it again corrctly.')</script>");
            }
            // Using Cont_Finger Column as a user login.
            //pnlAlreadyImage.Visible = false;
            if (Session["photo"] != null)
            {
                ContPhoto = (byte[])Session["photo"];
            }
            else
            {
                ContPhoto = (byte[])Session["ImageData"];
            }
            //if (pnl.Visible == true)
            //{
            //    ContPhoto = (byte[])Session["photo"];
            //    if (ContPhoto == null)
            //        ContPhoto = (byte[])Session["ImageData"];
            //}
            //else
            //{
            //    ContPhoto = (byte[])Session["ImageData"];
            //}

            if (PassType == "3" || PassType == "4")
            {
                txtgst.Text = "NA";
                ddlFirm.ClearSelection();
                ddl.BindFirmddl(ref ddlFirm);
                ddlFirm.SelectedItem.Text = "NA";
            }

            ApplicationPhoto = new byte[attachappno.PostedFile.ContentLength];
            attachappno.PostedFile.InputStream.Read(ApplicationPhoto, 0, attachappno.PostedFile.ContentLength);
            Session["App_image"] = ApplicationPhoto;
            int DockyardId = 0;
            DockyardId = Convert.ToInt32(txtDockyardID.Text.Trim());
            //count = (from x in DVSC.CONTRACTOR_DETAIL select x).Count();
            //int id = 0;
            //if (count > 0)
            //{
            //    //id = DVSC.CONTRACTOR_DETAIL.Max(x => x.Cont_Id) + 1;
            //    if (USERNAME == "deo1")
            //    {
            //        cont.Cont_Login = "deo1";
            //    }
            //    if (USERNAME == "deo2")
            //    {
            //        cont.Cont_Login = "deo2";
            //    }
            //    if (USERNAME == "deo")
            //    {
            //        cont.Cont_Login = "deo";
            //    }
            //    else
            //    {
            //        cont.Cont_Login = "admin";
            //    }

            //    ContractorId = ddl.get_data_from_DB("select top 1 A.[Cont_Id]+1 as [Cont_Id] from CONTRACTOR_DETAIL A where not exists (select * from CONTRACTOR_DETAIL B where B.[Cont_Id] = A.[Cont_Id]+1)");
            //    id = Convert.ToInt32(ContractorId.Tables[0].Rows[0]["Cont_Id"]);
            //    count = 0;
            //}
            //else
            //{
            //    id = 1;
            //}
            //
            id = Convert.ToInt32(ViewState["id"]);
            GetPassNumber(PassType);
            PassNo = Convert.ToInt32(ViewState["PASS_NO"]);
            cont = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == id);
            try
            {
                if (Validation() == true)
                {
                    //Response.Write("Validation true");
                    //cont.Cont_Id = id;
                    cont.Cont_Name = txtContractorName.Text.Trim();
                    cont.Cont_DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                    cont.Cont_Aadhaar = txtAADHAAR.Text.Trim();
                    cont.Cont_DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                    cont.Cont_Mobile = txtContactNo.Text.Trim();

                    //cont.Cont_Photo = ContPhoto;
                    if (ContPhoto == null)
                    {

                    }
                    else
                    {
                        cont.Cont_Photo = ContPhoto;
                    }

                    cont.Cont_FirmWO = txtFirmWorkOrderNo.Text.Trim();
                    cont.Cont_WOValidity = Convert.ToDateTime(txtWoValidity.Text);
                    cont.Cont_Unit = txtunit.Text.Trim();
                    cont.Cont_PmtAddress = txtPmtAdd.Text.Trim();
                    cont.Cont_StateID = Convert.ToInt32(ddlstate.SelectedValue);
                    cont.Cont_NationalityID = Convert.ToInt32(ddlNationality.SelectedValue);
                    cont.Cont_AppNo = txtapplicationno.Text.Trim();
                    cont.Cont_AppPhoto = ApplicationPhoto;
                    cont.Cont_PVCNO = txtpvcno.Text.Trim();
                    cont.Cont_RFIDValidity = Convert.ToDateTime(txtrfidValidity.Text);
                    cont.Cont_Delete_Flag = "N";
                    cont.Cont_CreatedDate = System.DateTime.Now;
                    cont.Cont_District = txtDistrict.Text.Trim();
                    cont.Cont_Gender = ddlGender.SelectedItem.Text.Trim();
                    cont.Cont_IssueDate = System.DateTime.Now.Date;
                    cont.Cont_Pin = txtPin.Text.Trim();
                    cont.Cont_ReligionID = Convert.ToInt32(ddlReligion.SelectedValue);
                    cont.Cont_Taluka = txtTaluka.Text.Trim();
                    cont.Cont_CancelFLag = "N";
                    cont.Cont_CancelReason = 1;
                    cont.Cont_BlackList = "N";
                    cont.Cont_Fine = "NA";
                    cont.Cont_PlaceOfLoss = "NA";
                    cont.Cont_CancelDate = Convert.ToDateTime("01/01/1900"); //System.DateTime.Now.AddYears(-100);
                    cont.Cont_DateOFLoss = Convert.ToDateTime("01/01/1900");//System.DateTime.Now.AddYears(-100); ;
                    cont.Cont_Fir = "NA";
                    cont.Cont_DocID = Convert.ToInt32(txtDockyardID.Text.Trim());
                    if (PassType == "1")
                    {
                        cont.Cont_PassType = "CONTRACTOR";
                        cont.Cont_CardNo = "C" + PassNo;
                        ViewState["Cont_CardNo"] = cont.Cont_CardNo;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 1;
                        cont.Cont_ShopId = 1;
                        cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                    }
                    if (PassType == "2")
                    {
                        cont.Cont_PassType = "ESCORTED";
                        cont.Cont_CardNo = "E" + PassNo;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 1;
                        cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                        cont.Cont_ShopId = 1;
                    }
                    if (PassType == "3")
                    {
                        cont.Cont_PassType = "BANK";
                        cont.Cont_CardNo = "B" + PassNo;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var PSUid = (from x in DVSC.PSU_MASTER where x.PSU_FIRMFILENO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = PSUid.PSU_ID;
                        cont.Cont_PSUunitID = ID;// PSU ID;
                        cont.Cont_IcardrNo = txtUnitIcard.Text.Trim();
                        cont.Cont_FirmID = 1; // NA
                        cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                        cont.Cont_ShopId = 1;
                    }
                    if (PassType == "4")
                    {
                        cont.Cont_PassType = "CB";
                        cont.Cont_CardNo = "CB" + PassNo;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_PSUunitID = 1;
                        cont.Cont_FirmID = 1; // NA
                        cont.Cont_UnitEmp = txtUnitEmp.Text.Trim();
                        cont.Cont_IcardrNo = "NA";
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var shop = (from x in DVSC.SHOP_MASTER where x.FILENO == txtfirmfileno.Text.Trim() select x).First();
                        cont.Cont_ShopId = shop.SHOP_ID;
                        cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                    }
                    if (PassType == "5")
                    {
                        cont.Cont_PassType = "LABOUR";
                        cont.Cont_CardNo = "L" + PassNo;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        // cont.Cont_PVCValidity = System.DateTime.Now;
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 1;
                        cont.Cont_ShopId = 1;
                    }
                    //Response.Write("PassType");
                    //Response.Write(PassType);
                    if (txtrfidno.Text.Trim().Length == 1)
                        cont.Cont_RFIDNo = "000000000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 2)
                        cont.Cont_RFIDNo = "00000000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 3)
                        cont.Cont_RFIDNo = "0000000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 4)
                        cont.Cont_RFIDNo = "000000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 5)
                        cont.Cont_RFIDNo = "00000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 6)
                        cont.Cont_RFIDNo = "0000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 7)
                        cont.Cont_RFIDNo = "000" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 8)
                        cont.Cont_RFIDNo = "00" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 9)
                        cont.Cont_RFIDNo = "0" + txtrfidno.Text.Trim();
                    if (txtrfidno.Text.Trim().Length == 10)
                        cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                    // cont.Cont_RFIDNo = txtrfidno.Text.Trim();
                    //DVSC.CONTRACTOR_DETAIL.AddObject(cont);
                    DVSC.SaveChanges();
                    GetMinDate(DockyardId);
                    lblContID.Text = cont.Cont_Id.ToString();
                    Response.Write(lblContID.Text);
                    lblcno.Text = cont.Cont_CardNo;
                    imgPicture.ImageUrl = "~/GetImages.ashx?id=" + lblContID.Text;
                    SaveApplicationPhoto(cont.Cont_Id);
                    BindGriDForApplication(cont.Cont_Id);
                    SaveDateForCasualVisitor(DockyardId);
                    //PrintPass(cont.Cont_CardNo);
                    ViewState["Cont_CardNo"] = cont.Cont_CardNo;
                    ViewState["MinDate"] = cont.Cont_MinDate;
                    ViewState["RFIDNo"] = cont.Cont_RFIDNo;
                    string IssueDate = cont.Cont_IssueDate.ToString();
                    string firm = GetFirmName(Convert.ToInt32(cont.Cont_PSUunitID), Convert.ToInt32(cont.Cont_ShopId), Convert.ToInt32(cont.Cont_FirmID));
                    var desi = (from x in DVSC.DESIGNATION_MASTER where x.DESIGNATION_ID == cont.Cont_DesignationID select x).FirstOrDefault();
                    string Designation = desi.DESIGNATION_NAME;
                    GetPrintData(cont.Cont_Aadhaar, cont.Cont_Name, firm, Convert.ToDateTime(IssueDate).ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper(), Convert.ToDateTime(cont.Cont_MinDate).ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper(), cont.Cont_CardNo, Designation, cont.Cont_Id, ContPhoto);
                    Response.Write("<script> alert('Pass ganerated successfuly ,Pass No is " + cont.Cont_CardNo + " and Expiry Date will be " + cont.Cont_MinDate + "')</script>");
                    DivDownload.Visible = true;
                    tblDownload.Visible = true;
                    btnapplicationlist.Enabled = true;
                    btnsave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('Error while saving record kindly clear browser history.')</script>");
                //Response.Write(ex.ToString());
                DivDownload.Visible = false;
                tblDownload.Visible = false;
            }
        }

        public void GetMinDate(int DockyardID)
        {
            var MinDate = (from x in DVSC.CONTRACTOR_DETAIL
                           where
                             x.Cont_DocID == DockyardID
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
            cont.Cont_MinDate = MinDate[0].MinDate;
            DVSC.SaveChanges();
            if (MinDate[0].MinDate <= System.DateTime.Now)
            {
                Response.Write("<script> alert('Card expiry date is Less than or Equal to current date it should be greater than current date kindly modify it before capturing finger / biometric.')</script>");
            }
        }

        public void ClearControl()
        {
            txtAADHAAR.Text = "";
            txtapplicationno.Text = "";
            txtContactNo.Text = "";
            txtContractorName.Text = "";
            txtDateOfBirth.Text = "";
            txtfirmfileno.Text = "";
            txtFirmWorkOrderNo.Text = "";
            txtgst.Text = "";
            txtPmtAdd.Text = "";
            txtpvcno.Text = "";
            txtpvcValidity.Text = "";
            txtrfidno.Text = "";
            txtrfidValidity.Text = "";
            txtunit.Text = "";
            txtWoValidity.Text = "";
            txtDistrict.Text = "";
            txtTaluka.Text = "";
            txtPin.Text = "";
            txtDockyardID.Text = "";
            ddlFirm.ClearSelection();
            ddlDesignation.ClearSelection();
            ddlNationality.ClearSelection();
            ddlstate.ClearSelection();
            ddlGender.ClearSelection();
            ddlother.ClearSelection();
            ddlReligion.ClearSelection();
            ddl.BindDesignationddl(ref ddlDesignation);
            ddl.BindStateddl(ref ddlstate);
            ddl.BindNationalityddl(ref ddlNationality);
            ddl.BindFirmddl(ref ddlFirm);
            ddl.BindDocumentddl(ref ddlother);
            ddl.BindGenderddl(ref ddlGender);
            ddl.BindReligionddl(ref ddlReligion);
            lblcno.Text = "";
            Gv_AppPhoto.DataSource = null;
            Gv_AppPhoto.DataBind();
            //lblid.Text = "";
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearControl();
            btnDockyardid.Enabled = true;
            btnsave.Enabled = false;
            //Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
        }

        private bool Validation()
        {
            try
            {
                if (string.IsNullOrEmpty(txtAADHAAR.Text))
                {
                    Response.Write("<script> alert('Please enter AADHAAR Number.')</script>");
                    txtAADHAAR.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtapplicationno.Text))
                {
                    Response.Write("<script> alert('Please enter Application Number.')</script>");
                    txtapplicationno.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtContactNo.Text))
                {
                    Response.Write("<script> alert('Please enter Contact Number.')</script>");
                    txtContactNo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtContractorName.Text))
                {
                    Response.Write("<script> alert('Please enter Contractor / Supervisor Name.')</script>");
                    txtContractorName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtDateOfBirth.Text))
                {
                    Response.Write("<script> alert('Please enter Date of Birth.')</script>");
                    txtDateOfBirth.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtfirmfileno.Text))
                {
                    Response.Write("<script> alert('Please enter Firm File Number.')</script>");
                    txtfirmfileno.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtFirmWorkOrderNo.Text))
                {
                    Response.Write("<script> alert('Please enter Work Order Number.')</script>");
                    txtFirmWorkOrderNo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtgst.Text))
                {
                    Response.Write("<script> alert('Please enter GST Number.')</script>");
                    txtgst.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtPmtAdd.Text))
                {
                    Response.Write("<script> alert('Please enter Permanent Address.')</script>");
                    txtPmtAdd.Focus();
                    return false;
                }
                if (PassType == "5")
                {

                }
                else
                {
                    if (string.IsNullOrEmpty(txtpvcno.Text))
                    {
                        Response.Write("<script> alert('Please enter PVC Number.')</script>");
                        txtpvcno.Focus();
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtpvcValidity.Text))
                    {
                        Response.Write("<script> alert('Please enter PVC Validity Date.')</script>");
                        txtpvcValidity.Focus();
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(txtrfidno.Text))
                {
                    Response.Write("<script> alert('Please enter RFID Number.')</script>");
                    txtrfidno.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtrfidValidity.Text))
                {
                    Response.Write("<script> alert('Please enter RFID Validity Date.')</script>");
                    txtrfidValidity.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtunit.Text))
                {
                    Response.Write("<script> alert('Please enter Unit Name.')</script>");
                    txtunit.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtWoValidity.Text))
                {
                    Response.Write("<script> alert('Please enter Work Order Validity Date.')</script>");
                    txtWoValidity.Focus();
                    return false;
                }
                if (ddlDesignation.SelectedItem.Text == "--SELECT--")
                {
                    Response.Write("<script> alert('Please select Designation.')</script>");
                    ddlDesignation.Focus();
                    return false;
                }
                if (ddlFirm.SelectedItem.Text == "--SELECT--")
                {
                    Response.Write("<script> alert('Please select Firm.')</script>");
                    ddlFirm.Focus();
                    return false;
                }
                if (ddlNationality.SelectedItem.Text == "--SELECT--")
                {
                    Response.Write("<script> alert('Please select Nationality.')</script>");
                    ddlNationality.Focus();
                    return false;
                }
                if (ddlstate.SelectedItem.Text == "--SELECT--")
                {
                    Response.Write("<script> alert('Please select State.')</script>");
                    ddlstate.Focus();
                    return false;
                }
                //if (!uploadePhoto.HasFile)
                //{
                //    Response.Write("<script> alert('Please select Photo.')</script>");
                //    //ddlstate.Focus();
                //    return false;
                //}
                if (!attachappno.HasFile)
                {
                    Response.Write("<script> alert('Please select Application.')</script>");
                    //ddlstate.Focus();
                    return false;
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error occurred while validatting records,please enter again.')</script>");
                ClearControl();
            }
            return true;
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

        protected void img_AppPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdrow = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton lnkbutton = (LinkButton)grdrow.FindControl("img_AppPhoto");
                HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
                int AppID = Convert.ToInt32(hdn.Value);
                ViewState["APPID"] = AppID;
                var App = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select x).First();
                ViewState["APPNO"] = App.APP_NUMBER;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "basicPopup();", true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void SaveApplicationPhoto(int Contid)
        {
            ViewState["PDF"] = 0;
            try
            {
                ApplicationPhoto = new byte[attachappno.PostedFile.ContentLength];
                if (attachappno.FileName.Contains(".pdf"))
                {
                    ViewState["PDF"] = 1;
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
                applicationform.DOCKYARDID = Convert.ToInt32(txtDockyardID.Text);
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

        public void BindGriDForApplication(int id)
        {
            //Flag = "N";
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

        protected void txtfirmfileno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PassType == "1" || PassType == "2" || PassType == "5")
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
                else if (PassType == "3")
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
                else if (PassType == "4")
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

        protected void txtAADHAAR_TextChanged(object sender, EventArgs e)
        {
            if (txtAADHAAR.Text.Trim().Length > 0)
            {
                //var Aadhaar = "";
                count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_Aadhaar == txtAADHAAR.Text.Trim());
                // var Cancel = DVSC.CONTRACTOR_DETAIL.Count(x => ((x.Cont_CancelFLag == "LOSS" || x.Cont_CancelFLag == "CANCEL") && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()) || (x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()));
                //var Delete = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim());
                var Cancel = DVSC.CONTRACTOR_DETAIL.Count(x => (x.Cont_CancelFLag == "CANCEL" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()) || (x.Cont_CancelFLag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()) || (x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()));

                try
                {
                    if (Cancel > 0)
                    {
                        var CancelFlag = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                        ////var Aadhaar = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                        //Response.Write("<script> alert('The card " + CancelFlag.Cont_CardNo + " is already Cancelled / Lost / Deleted, Please view Cancel Report.')</script>");
                        //ClearControl();
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

                    }
                }
                catch (Exception)
                {
                    Response.Write("<script> alert('Error while loading data.')</script>");
                }
            }
        }

        protected void txtOtherDoc_TextChanged(object sender, EventArgs e)
        {
        }

        protected void ddlFirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PassType == "1" || PassType == "2" || PassType == "5")
                {
                    int id = Convert.ToInt32(ddlFirm.SelectedItem.Value.Trim());
                    count = DVSC.FIRMMASTERs.Count(x => x.FIRM_ID == id && x.FLAG != "Y");
                    if (count > 0)
                    {
                        var firm = (from x in DVSC.FIRMMASTERs where x.FIRM_ID == id select x).First();
                        txtfirmfileno.Text = firm.FIRM_FILE_NO.Trim();
                        txtgst.Text = firm.FIRM_GST.Trim();
                        count = 0;
                    }
                    else
                    {
                        txtfirmfileno.Text = "";
                        txtgst.Text = "";
                        ddl.BindDesignationddl(ref ddlDesignation);
                        Response.Write("<script> alert('Please register designation or contact to admin or view deleted firm list.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void txtDockyardID_TextChanged(object sender, EventArgs e)
        {
            int CV_id = Convert.ToInt32(txtDockyardID.Text.Trim());
            var CV_Data1 = (from x in DVSC.VisitorTransactionDetails.Where(x => x.VisitorTranID == CV_id) select x).ToList();
            try
            {
                if (CV_Data1.Count > 0)
                {
                    var CV_Data = (from x in DVSC.VisitorTransactionDetails.Where(x => x.VisitorTranID == CV_id) select x).First();
                    var Data1 = (from x in DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Aadhaar == CV_Data.AADHAR_CARD || x.Cont_Aadhaar == CV_Data.ID_No) select x).ToList();
                    var Data = (from x in DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Aadhaar == CV_Data.AADHAR_CARD || x.Cont_Aadhaar == CV_Data.ID_No) select x).First();

                    //if (Data1.Count > 0)
                    //{
                    //   // Response.Write("<script> alert('Person is already registered with this addhaar no " + CV_Data.AADHAR_CARD + "')</script>");
                    //}
                    //else
                    //{
                    // check from visitor
                    string DOB = Data.Cont_DOB.ToString();//CV_Data.DOB.ToString();
                    if (!string.IsNullOrEmpty(Data.Cont_Aadhaar)) //CV_Data.AADHAR_CARD
                        txtAADHAAR.Text = Data.Cont_Aadhaar; //CV_Data.AADHAR_CARD;   
                    if (!string.IsNullOrEmpty(Data.Cont_Mobile))//CV_Data.MobileNumber
                        txtContactNo.Text = Data.Cont_Mobile;   //CV_Data.MobileNumber;
                    if (!string.IsNullOrEmpty(Data.Cont_Name)) //CV_Data.VisitorName
                        txtContractorName.Text = Data.Cont_Name;  // CV_Data.VisitorName;
                    if (DOB != "")
                        txtDateOfBirth.Text = DOB;
                    ddlNationality.ClearSelection();
                    if (!string.IsNullOrEmpty(Data.Cont_NationalityID.ToString()))
                        //ddlNationality.Items.FindByText(CV_Data.Nationality.Trim()).Selected = true;
                        ddlNationality.SelectedIndex = ddlNationality.Items.IndexOf(ddlNationality.Items.FindByValue(Data.Cont_NationalityID.ToString())); //CV_Data.Nationality.Trim()
                    ddlGender.ClearSelection();
                    if (!string.IsNullOrEmpty(Data.Cont_Gender.ToString()))//CV_Data.Sex
                                                                           //ddlGender.Items.FindByText(CV_Data.Sex.Trim()).Selected = true;
                        ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(Data.Cont_Gender.ToString())); //CV_Data.Nationality.Trim()
                    ddlother.ClearSelection();
                    if (!string.IsNullOrEmpty(Data.Cont_Aadhaar.ToString())) //CV_Data.ID_No
                    {

                        //if (Data.Cont_DocID.ToString() == "3")
                        //{
                        //txtAADHAAR.Text = CV_Data.ID_No;
                        txtAADHAAR.Text = Data.Cont_Aadhaar;
                        //}
                        //else
                        //{
                        //    ddlother.Items.FindByText(Data.Cont_DocID.ToString()).Selected = true;
                        //    txtOtherDoc.Text = Data.Cont_Aadhaar; //CV_Data.ID_No;
                        //}
                    }
                    //if (CV_Data.VisitiorPhoto != null)
                    //{
                    //    imgPicture.ImageUrl = "~/Get_CV_Image.ashx?id=" + CV_Data.VisitorTranID;
                    //    Session["ImageCasualVisitor"] = CV_Data.VisitiorPhoto;
                    //}

                    // check 

                    if (Data.Cont_Photo != null)
                    {
                        // imgPicture.ImageUrl = "~/Get_CV_Image.ashx?id=" + Data.Cont_Id;
                        imgPicture.ImageUrl = "~/GetImages.ashx?id=" + Data.Cont_Id;
                        // Session["ImageCasualVisitor"] = Data.Cont_Photo;
                    }

                    txtapplicationno.Text = "";
                    txtfirmfileno.Text = "";
                    txtFirmWorkOrderNo.Text = "";
                    txtgst.Text = "";
                    txtPmtAdd.Text = "";
                    txtpvcno.Text = "";
                    txtpvcValidity.Text = "";
                    txtrfidno.Text = "";
                    txtrfidValidity.Text = "";
                    txtunit.Text = "";
                    txtWoValidity.Text = "";
                    txtDistrict.Text = "";
                    txtTaluka.Text = "";
                    txtPin.Text = "";
                    ddlFirm.ClearSelection();
                    ddlDesignation.ClearSelection();
                    ddlstate.ClearSelection();
                    ddlReligion.ClearSelection();
                    ddl.BindDesignationddl(ref ddlDesignation);
                    ddl.BindStateddl(ref ddlstate);
                    ddl.BindFirmddl(ref ddlFirm);
                    ddl.BindDocumentddl(ref ddlother);
                    ddl.BindReligionddl(ref ddlReligion);
                }
                //}
                //else
                //{
                //    Response.Write("<script> alert('No data Found, Ganerate Dockyard ID to Proceed.')</script>");
                //}
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while fetching record.')</script>");
            }
        }

        protected void ddlPvcYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPvcYN.SelectedItem.Text == "YES")
            {
                txtpvcno.Enabled = true;
                txtpvcno.Text = "";
            }
            else
            {
                txtpvcno.Enabled = false;
                txtpvcno.Text = "NA".Trim();
            }
        }

        public void CommanProperty()
        {
            if (PassType == "1")
            {
                ddlPvcYN.Visible = false;
                spnHeading.InnerText = "CONTRACTOR / SUPERVISOR PASS";
                btnpnlhome.Visible = true;
                ddlPSUunit.Visible = false;
                lblPSUunit.Visible = false;
                lblfirm.Visible = true;
                ddlFirm.Visible = true;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblgst.Visible = true;
                txtgst.Visible = true;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblfirmfileno.Text = "Firm File No";
            }
            if (PassType == "2")
            {
                ddlPvcYN.Visible = false;
                spnHeading.InnerText = "ESCORTED WORKERS PASS";
                btnpnlhome.Visible = true;
                ddlPSUunit.Visible = false;
                lblPSUunit.Visible = false;
                lblfirm.Visible = true;
                ddlFirm.Visible = true;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblgst.Visible = true;
                txtgst.Visible = true;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblfirmfileno.Text = "Firm File No";
            }
            if (PassType == "3")
            {
                ddlPvcYN.Visible = true;
                spnHeading.InnerText = "BANK's / PSU / BEL PASS";
                btnpnlhome.Visible = true;
                ddlPSUunit.Visible = true;
                lblPSUunit.Visible = true;
                lblfirm.Visible = false;
                ddlFirm.Visible = false;
                lblfirmfileno.Text = "File No";
                lblUnitIcard.Visible = true;
                txtUnitIcard.Visible = true;
                lblgst.Visible = false;
                txtgst.Visible = false;
                ddl.BindPSUddl(ref ddlPSUunit);
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
            }
            if (PassType == "4")
            {
                ddlPvcYN.Visible = false;
                spnHeading.InnerText = "CANTEEN / SHIPS HIRED STAFFS";
                btnpnlhome.Visible = true;
                ddlPSUunit.Visible = false;
                lblPSUunit.Visible = false;
                lblfirm.Visible = false;
                ddlFirm.Visible = false;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblgst.Visible = false;
                txtgst.Visible = false;
                lblUnitEmp.Visible = true;
                txtUnitEmp.Visible = true;
                lblShop.Visible = true;
                ddlShop.Visible = true;
                ddl.BindShopddl(ref ddlShop);
                lblfirmfileno.Text = "File No";
            }
            if (PassType == "5")
            {
                spnHeading.InnerText = "LABOUR PASS";
                ddlPvcYN.Visible = true;
                btnpnlhome.Visible = false;
                ddlPSUunit.Visible = false;
                lblPSUunit.Visible = false;
                lblfirm.Visible = true;
                ddlFirm.Visible = true;
                lblUnitIcard.Visible = false;
                txtUnitIcard.Visible = false;
                lblgst.Visible = true;
                txtgst.Visible = true;
                lblUnitEmp.Visible = false;
                txtUnitEmp.Visible = false;
                lblShop.Visible = false;
                ddlShop.Visible = false;
                lblfirmfileno.Text = "Firm File No";
            }
        }

        protected void ddlPSUunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PassType == "3")
            {
                int id = Convert.ToInt32(ddlPSUunit.SelectedItem.Value.Trim());
                count = DVSC.PSU_MASTER.Count(x => x.PSU_ID == id && x.FLAG != "Y");
                if (count > 0)
                {
                    var firm = (from x in DVSC.PSU_MASTER where x.PSU_ID == id select x).First();
                    txtfirmfileno.Text = firm.PSU_FIRMFILENO.Trim();
                    txtgst.Text = "NA";
                    count = 0;
                }
            }
        }

        protected void ddlShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PassType == "4")
            {
                int id = Convert.ToInt32(ddlShop.SelectedItem.Value.Trim());
                count = DVSC.SHOP_MASTER.Count(x => x.SHOP_ID == id && x.FLAG != "Y");
                if (count > 0)
                {
                    var firm = (from x in DVSC.SHOP_MASTER where x.SHOP_ID == id select x).First();
                    txtfirmfileno.Text = firm.FILENO.Trim();
                    txtgst.Text = "NA";
                    count = 0;
                }
            }
        }

        public void GetContractorID()
        {
            DataSet DeleteNullData = null;
            if (USERNAME == "deo1")
            {
                cont.Cont_Login = "deo1";
            }
            if (USERNAME == "deo2")
            {
                cont.Cont_Login = "deo2";
            }
            if (USERNAME == "deo")
            {
                cont.Cont_Login = "deo";
            }
            else
            {
                cont.Cont_Login = "admin";
            }
            DeleteNullData = ddl.get_data_from_DB("select * from CONTRACTOR_DETAIL where Cont_login = '" + cont.Cont_Login + "' and Cont_Name is null and Cont_Aadhaar is null");
            if (DeleteNullData.Tables[0].Rows.Count > 0)
            {
                ddl.save_data_db("delete from CONTRACTOR_DETAIL where Cont_login = '" + cont.Cont_Login + "' and Cont_Name is null and Cont_Aadhaar is null");
            }
            ContractorId = ddl.get_data_from_DB("select top 1 A.[Cont_Id]+1 as [Cont_Id] from CONTRACTOR_DETAIL A where not exists (select * from CONTRACTOR_DETAIL B where B.[Cont_Id] = A.[Cont_Id]+1)");
            if (ContractorId.Tables[0].Rows.Count > 0)
            {
                id = Convert.ToInt32(ContractorId.Tables[0].Rows[0]["Cont_Id"]);
                cont.Cont_Id = id;
                ViewState["id"] = id;
                lblContID.Text = id.ToString();
                lblContID.Visible = true;
            }
            DVSC.CONTRACTOR_DETAIL.AddObject(cont);
            DVSC.SaveChanges();
        }

        protected void btnDockyardid_Click(object sender, EventArgs e)
        {
            DataSet DockyardID = null;
            DataSet DeleteNullData = null;
            int CV_DockyardID = 0;
            try
            {
                if (USERNAME == "deo1")
                {
                    CV.UserId = 3;
                }
                if (USERNAME == "deo2")
                {
                    CV.UserId = 4;
                }
                if (USERNAME == "deo")
                {
                    CV.UserId = 2;
                }
                else
                {
                    CV.UserId = 1;
                }
                DeleteNullData = ddl.get_data_from_DB("select * from dbo.VisitorTransactionDetail where VisitorName is null and AADHAR_CARD is null and Oragantization is null and MobileNumber is null and userid = " + CV.UserId + "");
                if (DeleteNullData.Tables[0].Rows.Count > 0)
                {
                    ddl.save_data_db("delete from dbo.VisitorTransactionDetail where VisitorName is null and AADHAR_CARD is null and Oragantization is null and MobileNumber is null and userid = " + CV.UserId + "");
                }
                DockyardID = ddl.get_data_from_DB("select top 1 A.VisitorTranID+1 as VisitorTranID from VisitorTransactionDetail A where not exists (select * from VisitorTransactionDetail B where B.VisitorTranID = A.VisitorTranID+1)");
                if (DockyardID.Tables[0].Rows.Count > 0)
                {
                    CV_DockyardID = Convert.ToInt32(DockyardID.Tables[0].Rows[0]["VisitorTranID"]);
                    txtDockyardID.Text = CV_DockyardID.ToString();
                    if (PassType == "5")
                    {
                        txtrfidno.Text = CV_DockyardID.ToString();
                    }
                    CV.VisitorTranID = CV_DockyardID;
                }
                else
                {
                    CV_DockyardID = 1;
                    txtDockyardID.Text = CV_DockyardID.ToString();
                }
                CV.VisitorTranID = CV_DockyardID;
                DVSC.VisitorTransactionDetails.AddObject(CV);
                DVSC.SaveChanges();
                btnDockyardid.Enabled = false;
                txtDockyardID.Enabled = false;
                btnsave.Enabled = true;
                GetContractorID();
                //GetPassNumber(PassType);
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Ganerating Dockyard ID.')</script>");
            }
        }

        public void SaveDateForCasualVisitor(int Cont_DocID)
        {
            CV = DVSC.VisitorTransactionDetails.First(x => x.VisitorTranID == Cont_DocID);
            //cont = 
            try
            {
                CV.VisitorTranID = Cont_DocID;
                CV.VisitorName = cont.Cont_Name;
                if (cont.Cont_Photo == null)
                {
                }
                else
                {
                    CV.VisitiorPhoto = cont.Cont_Photo;
                }
                CV.Sex = cont.Cont_Gender;
                CV.Oragantization = ddlFirm.SelectedItem.Text.Trim();
                CV.Nationality = ddlNationality.SelectedItem.Text.Trim();
                CV.MobileNumber = cont.Cont_Mobile;
                CV.DOB = cont.Cont_DOB;
                CV.AADHAR_CARD = cont.Cont_Aadhaar;
                DVSC.SaveChanges();
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Ganerating Dockyard ID.')</script>"); ;
            }
        }

        protected void btnFinger_Click(object sender, EventArgs e)
        {
            try
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "basicPopup_Finger();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void Btndownlaod_Click(object sender, EventArgs e)
        {
            string Rfidno = ViewState["RFIDNo"].ToString();
            DateTime MinDate = Convert.ToDateTime(ViewState["MinDate"]);
            try
            {
                objCardDetails.CardNo = Rfidno;
                //objCardDetails.CardNo = txtrfidno.Text.Trim();
                objCardDetails.AuthCode = "6";
                objCardDetails.ControllerNo = ddlDownload.SelectedItem.Value.ToString();
                objCardDetails.Expiry = MinDate;//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                                                //objCardDetails.Expiry =Convert.ToDateTime(txtrfidValidity.Text.Trim());
                objCardDetails.Name = txtContractorName.Text.Trim();
                objCardDetails.NoOfReader = 4;
                //objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);
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
                    //ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, Convert.ToInt32(objCardDetails.ControllerNo));
                    ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, objCardDetails.ControllerNo);
                    DDeaciiveSuccess.Visible = true;
                    DDeaciiveFail.Visible = false;
                }
                else
                {
                    Response.Write("<script> alert('CONTROLLER " + Ipaddress[0].ControllerName + " OFFLINE')</script>");
                }
            }
            catch
            {
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = true;
                Response.Write("<script> alert('BY CONTROLLER IS OFFLINE')</script>");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string CardNo = ViewState["Cont_CardNo"].ToString();
            PrintPass(CardNo);
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

        protected void AddFig_Click(object sender, EventArgs e)
        {
            if (txtrfidno.Text.Trim().Length > 0 && ddlDownload.SelectedIndex > 0)
            {
                Cno = Convert.ToInt32(ddlDownload.SelectedItem.Value);
                errcode = objclient.EnrollFinger(txtrfidno.Text.Trim(), Cno.ToString());
                if (errcode.ToString() == "Finger_Time_Out_108")
                    Msg = "FINGER TIMED OUT";
                else if (errcode.ToString() == "Socket_Timed_out")
                    Msg = "SOCKET TIMED OUT OR DEVICE IS OFLINE";
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

        protected void txtRFIDNO_TextChanged(object sender, EventArgs e)
        {
            count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_RFIDNo == txtrfidno.Text.Trim());
            if (count > 0)
            {
                txtrfidno.Text = "";
                Response.Write("<script>alert('This rfid is alreay assigned to someone else please change it.')</script>");
            }
            else
            {
                if (txtrfidno.Text.Trim().Length > 0 && ddlDownload.SelectedIndex > 0)
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

        public void GetPrintData(string Addhar, string Name, string Firm, string IssueDate, string ExpiryDate, string CardNo, string Designation, int Contid, byte[] Image)
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
            PrintCard.PrintCardID = PrintID;
            PrintCard.Cont_Id = Contid;
            PrintCard.Cont_IssueDate = IssueDate;
            PrintCard.Cont_MinDate = ExpiryDate;
            PrintCard.Cont_CardNo = CardNo;
            PrintCard.DESIGNATION_NAME = Designation;
            PrintCard.Cont_Photo = Image;
            DVSC.Print_Card.AddObject(PrintCard);
            DVSC.SaveChanges();
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

        public void GetPassNumber(string PASSTYPEID)
        {
            string PASSTYPE = "";
            if (PASSTYPEID == "1")
            {
                PASSTYPE = "CONTRACTOR";
            }
            if (PASSTYPEID == "2")
            {
                PASSTYPE = "ESCORTED";
            }
            if (PASSTYPEID == "3")
            {
                PASSTYPE = "BANK";
            }
            if (PASSTYPEID == "4")
            {
                PASSTYPE = "CB";
            }
            if (PASSTYPEID == "5")
            {
                PASSTYPE = "LABOUR";
            }

            DataSet ds = new DataSet("TimeRanges");
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Get_PassNo", conn);
                sqlComm.Parameters.AddWithValue("@PASSTYPE", PASSTYPE);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                ViewState["table1"] = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PassNo = Convert.ToInt32(ds.Tables[0].Rows[0]["PASS_NO"]);
                    ViewState["PASS_NO"] = PassNo;
                }
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDockyardID.Text.Trim().Length > 0)
            {
                int CV_id = Convert.ToInt32(txtDockyardID.Text.Trim());
                var CV_Data1 = (from x in DVSC.VisitorTransactionDetails.Where(x => x.VisitorTranID == CV_id) select x).ToList();
                try
                {
                    if (CV_Data1.Count > 0)
                    {
                        var CV_Data = (from x in DVSC.VisitorTransactionDetails.Where(x => x.VisitorTranID == CV_id) select x).First();
                        var Data1 = (from x in DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Aadhaar == CV_Data.AADHAR_CARD || x.Cont_Aadhaar == CV_Data.ID_No) select x).ToList();
                        var Data = (from x in DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Aadhaar == CV_Data.AADHAR_CARD || x.Cont_Aadhaar == CV_Data.ID_No) select x).First();

                        //if (Data1.Count > 0)
                        //{
                        //   // Response.Write("<script> alert('Person is already registered with this addhaar no " + CV_Data.AADHAR_CARD + "')</script>");
                        //}
                        //else
                        //{
                        // check from visitor
                        string DOB = Data.Cont_DOB.ToString();//CV_Data.DOB.ToString();
                        if (!string.IsNullOrEmpty(Data.Cont_Aadhaar)) //CV_Data.AADHAR_CARD
                            txtAADHAAR.Text = Data.Cont_Aadhaar; //CV_Data.AADHAR_CARD;   
                        if (!string.IsNullOrEmpty(Data.Cont_Mobile))//CV_Data.MobileNumber
                            txtContactNo.Text = Data.Cont_Mobile;   //CV_Data.MobileNumber;
                        if (!string.IsNullOrEmpty(Data.Cont_Name)) //CV_Data.VisitorName
                            txtContractorName.Text = Data.Cont_Name;  // CV_Data.VisitorName;
                        if (DOB != "")
                            txtDateOfBirth.Text = DOB;
                        ddlNationality.ClearSelection();
                        if (!string.IsNullOrEmpty(Data.Cont_NationalityID.ToString()))
                            //ddlNationality.Items.FindByText(CV_Data.Nationality.Trim()).Selected = true;
                            ddlNationality.SelectedIndex = ddlNationality.Items.IndexOf(ddlNationality.Items.FindByValue(Data.Cont_NationalityID.ToString())); //CV_Data.Nationality.Trim()
                        ddlGender.ClearSelection();
                        if (!string.IsNullOrEmpty(Data.Cont_Gender.ToString()))//CV_Data.Sex
                                                                               //ddlGender.Items.FindByText(CV_Data.Sex.Trim()).Selected = true;
                            ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByText(Data.Cont_Gender.ToString())); //CV_Data.Nationality.Trim()
                        ddlother.ClearSelection();
                        if (!string.IsNullOrEmpty(Data.Cont_Aadhaar.ToString())) //CV_Data.ID_No
                        {

                            //if (Data.Cont_DocID.ToString() == "3")
                            //{
                            //txtAADHAAR.Text = CV_Data.ID_No;
                            txtAADHAAR.Text = Data.Cont_Aadhaar;
                            //}
                            //else
                            //{
                            //    ddlother.Items.FindByText(Data.Cont_DocID.ToString()).Selected = true;
                            //    txtOtherDoc.Text = Data.Cont_Aadhaar; //CV_Data.ID_No;
                            //}
                        }
                        //if (CV_Data.VisitiorPhoto != null)
                        //{
                        //    imgPicture.ImageUrl = "~/Get_CV_Image.ashx?id=" + CV_Data.VisitorTranID;
                        //    Session["ImageCasualVisitor"] = CV_Data.VisitiorPhoto;
                        //}

                        // check 

                        if (Data.Cont_Photo != null)
                        {
                            // imgPicture.ImageUrl = "~/Get_CV_Image.ashx?id=" + Data.Cont_Id;
                            imgPicture.ImageUrl = "~/GetImages.ashx?id=" + Data.Cont_Id;
                            //pnlAlreadyImage.Visible = true;
                            // Session["ImageCasualVisitor"] = Data.Cont_Photo;
                            ViewState["ImageData"] = Data.Cont_Photo;
                        }



                        txtapplicationno.Text = "";
                        txtfirmfileno.Text = "";
                        txtFirmWorkOrderNo.Text = "";
                        txtgst.Text = "";
                        txtPmtAdd.Text = "";
                        txtpvcno.Text = "";
                        txtpvcValidity.Text = "";
                        txtrfidno.Text = "";
                        txtrfidValidity.Text = "";
                        txtunit.Text = "";
                        txtWoValidity.Text = "";
                        txtDistrict.Text = "";
                        txtTaluka.Text = "";
                        txtPin.Text = "";
                        ddlFirm.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlstate.ClearSelection();
                        ddlReligion.ClearSelection();
                        ddl.BindDesignationddl(ref ddlDesignation);
                        ddl.BindStateddl(ref ddlstate);
                        ddl.BindFirmddl(ref ddlFirm);
                        ddl.BindDocumentddl(ref ddlother);
                        ddl.BindReligionddl(ref ddlReligion);

                        btnSearch.Enabled = false;
                        txtDockyardID.Enabled = false;
                        btnsave.Enabled = true;
                        GetContractorID();
                    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script> alert('No data Found, Ganerate Dockyard ID to Proceed.')</script>");
                    //}
                }
                catch (Exception)
                {
                    Response.Write("<script> alert('Error while fetching record.')</script>");
                }
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