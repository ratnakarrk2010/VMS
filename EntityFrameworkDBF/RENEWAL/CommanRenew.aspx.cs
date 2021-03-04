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

namespace EntityFrameworkDBF.RENEWAL
{
    public partial class CommanRenew : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        CONTRACTOR_DETAIL cont = new CONTRACTOR_DETAIL();
        APPLICATION_FORM applicationform = new APPLICATION_FORM();
        public byte[] ContPhoto;
        Byte[] ApplicationPhoto;
        byte[] bytes;
        string Name = "";
        int count = 0;
        DropDownFunction ddl = new DropDownFunction();
        int id = 0;
        string PassType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PassType = Session["PASSTYPE"].ToString();
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                ddl.BindDesignationddl(ref ddlDesignation);
                ddl.BindStateddl(ref ddlstate);
                ddl.BindNationalityddl(ref ddlNationality);
                ddl.BindFirmddl(ref ddlFirm);
                ddl.BindDocumentddl(ref ddlother);
                ddl.BindGenderddl(ref ddlGender);
                ddl.BindReligionddl(ref ddlReligion);
                ddl.BindPSUddl(ref ddlPSUunit);
                if (Session["photo"] != null)
                {
                    Session["photo"] = null;
                }
                pnlAlreadyImage.Visible = true;
                CommanProperty();
            }
        }

        protected void btnhome_Click(object sender, EventArgs e)
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

        protected void btnpnlhome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ISSUES MODULE\\ISSUE_HOME.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
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
            if (ContPhoto == null)
            {
                ContPhoto = (byte[])Session["ImageData"];
                return;
            }
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
            count = DVSC.CONTRACTOR_DETAIL.Max(x => x.Cont_Id);
            int id = 0;
            if (count > 0)
            {
                id = DVSC.CONTRACTOR_DETAIL.Max(x => x.Cont_Id) + 1;
                count = 0;
            }
            else
            {
                id = 1;
            }
            try
            {
                if (Validation() == true)
                {
                    cont.Cont_Name = txtContractorName.Text.Trim();
                    cont.Cont_DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                    cont.Cont_Aadhaar = txtAADHAAR.Text.Trim();
                    cont.Cont_DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                    cont.Cont_Mobile = txtContactNo.Text.Trim();
                    cont.Cont_Photo = ContPhoto;
                    cont.Cont_FirmWO = txtFirmWorkOrderNo.Text.Trim();
                    cont.Cont_WOValidity = Convert.ToDateTime(txtWoValidity.Text);
                    cont.Cont_Unit = txtunit.Text.Trim();
                    cont.Cont_PmtAddress = txtPmtAdd.Text.Trim();
                    cont.Cont_StateID = Convert.ToInt32(ddlstate.SelectedValue);
                    cont.Cont_NationalityID = Convert.ToInt32(ddlNationality.SelectedValue);
                    cont.Cont_AppNo = txtapplicationno.Text.Trim();
                    cont.Cont_AppPhoto = ApplicationPhoto;
                    cont.Cont_PVCNO = txtpvcno.Text.Trim();
                    cont.Cont_RFIDNo = txtrfidno.Text.Trim();
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
                    cont.Cont_CancelReason = 9;
                    cont.Cont_BlackList = "N";
                    cont.Cont_Fine = "NA";
                    cont.Cont_PlaceOfLoss = "NA";
                    cont.Cont_CancelDate = System.DateTime.Now.AddYears(-100);
                    cont.Cont_DateOFLoss = System.DateTime.Now.AddYears(-100); ;
                    cont.Cont_Fir = "NA";
                    if (PassType == "1")
                    {
                        cont.Cont_PassType = "CONTACTOR";
                        cont.Cont_CardNo = "C" + id;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 6;
                    }
                    if (PassType == "2")
                    {
                        cont.Cont_PassType = "ESCORTED";
                        cont.Cont_CardNo = "E" + id;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 6;
                    }
                    if (PassType == "3")
                    {
                        cont.Cont_PassType = "BANK";
                        cont.Cont_CardNo = "B" + id;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var PSUid = (from x in DVSC.PSU_MASTER where x.PSU_FIRMFILENO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = PSUid.PSU_ID;
                        cont.Cont_PSUunitID = ID;// PSU ID;
                        cont.Cont_IcardrNo = txtUnitIcard.Text.Trim();
                        cont.Cont_FirmID = 18; // NA
                    }
                    if (PassType == "4")
                    {
                        cont.Cont_PassType = "CB";
                        cont.Cont_CardNo = "CB" + id;
                        cont.Cont_PVCValidity = Convert.ToDateTime(txtpvcValidity.Text);
                        cont.Cont_PSUunitID = 6;
                        cont.Cont_FirmID = 18; // NA
                        cont.Cont_UnitEmp = txtUnitEmp.Text.Trim();
                        cont.Cont_IcardrNo = "NA";
                    }
                    if (PassType == "5")
                    {
                        cont.Cont_PassType = "LABOUR";
                        cont.Cont_CardNo = "L" + id;
                        cont.Cont_PVCValidity = System.DateTime.Now;
                        cont.Cont_FirmFileNo = txtfirmfileno.Text.Trim();
                        var firmid = (from x in DVSC.FIRMMASTERs where x.FIRM_FILE_NO == txtfirmfileno.Text.Trim() select x).First();
                        int ID = firmid.FIRM_ID;
                        cont.Cont_FirmID = ID;// ddlFirm.SelectedValue;
                        cont.Cont_PSUunitID = 6;
                    }
                    DVSC.CONTRACTOR_DETAIL.AddObject(cont);
                    DVSC.SaveChanges();
                    lblContID.Text = cont.Cont_Id.ToString();
                    lblcno.Text = cont.Cont_CardNo;
                    imgPicture.ImageUrl = "~/GetImages.ashx?id=" + lblContID.Text;
                    SaveApplicationPhoto(cont.Cont_Id);
                    BindGriDForApplication(cont.Cont_Id);

                    FileStream fs = new FileStream("D:\\SAVED PHOTO\\" + cont.Cont_Id + "-" + cont.Cont_CardNo + ".jpg", FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryWriter br = new BinaryWriter(fs);
                    br.Write(ContPhoto);
                    br.Flush();
                    br.Close();
                    fs.Close();
                    Response.Write("<script> alert('Data saved successfully for contractor " + cont.Cont_Name + " and Contractor Id is " + cont.Cont_Id + "')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
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
            //lblid.Text = "";
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearControl();
            Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
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
            //var n = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Id == Convert.ToInt32(lblContID.Text) select x).First();
            //var N = DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Id == Convert.ToInt32(lblContID.Text));
            //Name = n.Cont_Name;
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
                Session["APPID"] = AppID;
                var App = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select x).First();
                Session["APPNO"] = App.APP_NUMBER;
                //int Flag = Convert.ToInt32(Session["PDF"]);
                //if (Flag == 1)
                //{
                //    var DATA = (from x in DVSC.APPLICATION_FORM where x.APP_ID == AppID select x).First();
                //    bytes = DATA.APP_FORM;
                //    Response.Buffer = true;
                //    Response.Charset = "";
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.ContentType = "application/pdf";
                //    Response.BinaryWrite(bytes);
                //    Response.Flush();
                //    Response.End();
                //}
                //else
                //{
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "basicPopup();", true);
                //}
                ///lnkbutton.Attributes.Add("onclick", "return basicPopup()");
            }
            catch (Exception)
            {
                throw;
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
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void txtAADHAAR_TextChanged(object sender, EventArgs e)
        {
            //var Aadhaar = "";
            count = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_Aadhaar == txtAADHAAR.Text.Trim());
            var Cancel = DVSC.CONTRACTOR_DETAIL.Count(x => ((x.Cont_CancelFLag == "LOSS" || x.Cont_CancelFLag == "CANCEL") && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()) || (x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim()));
            //var Delete = DVSC.CONTRACTOR_DETAIL.Count(x => x.Cont_Delete_Flag == "Y" && x.Cont_Aadhaar == txtAADHAAR.Text.Trim());
            try
            {
                if (Cancel > 0)
                {
                    var CancelFlag = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                    //var Aadhaar = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                    Response.Write("<script> alert('The card " + CancelFlag.Cont_CardNo + " is already Cancelled / Lost / Deleted, Please view Cancel Report.')</script>");
                    ClearControl();
                }
                else
                {
                    txtAADHAAR.Attributes.Add("pattern", "[0-9]{12}");
                    if (count > 0)
                    {
                        var Aadhaar = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Aadhaar == txtAADHAAR.Text.Trim() select x).First();
                        //txtAADHAAR.Text = Aadhaar.Cont_Aadhaar;
                        //txtapplicationno.Text = Aadhaar.Cont_AppNo;
                        //txtContactNo.Text = Aadhaar.Cont_Mobile;
                        //txtContractorName.Text = Aadhaar.Cont_Name;
                        //txtDateOfBirth.Text = Aadhaar.Cont_DOB.ToString();
                        //txtfirmfileno.Text = Aadhaar.Cont_FirmFileNo;
                        //txtFirmWorkOrderNo.Text = Aadhaar.Cont_FirmWO;
                        ////txtgst.Text = Aadhaar.Cont_FirmGstNo;
                        //txtPmtAdd.Text = Aadhaar.Cont_PmtAddress;
                        //txtpvcno.Text = Aadhaar.Cont_PVCNO;
                        //txtpvcValidity.Text = Aadhaar.Cont_PVCValidity.ToString();
                        //txtrfidno.Text = Aadhaar.Cont_RFIDNo;
                        //txtrfidValidity.Text = Aadhaar.Cont_RFIDValidity.ToString();
                        //txtunit.Text = Aadhaar.Cont_Unit;
                        //txtWoValidity.Text = Aadhaar.Cont_WOValidity.ToString();
                        //txtPin.Text = Aadhaar.Cont_Pin;
                        //txtDistrict.Text = Aadhaar.Cont_District;
                        //txtTaluka.Text = Aadhaar.Cont_Taluka;
                        //ddlDesignation.ClearSelection();
                        //ddlDesignation.Items.FindByValue(Aadhaar.Cont_DesignationID).Selected = true;
                        //ddlFirm.ClearSelection();
                        //ddlFirm.Items.FindByValue(Aadhaar.Cont_FirmID).Selected = true;
                        //ddlNationality.ClearSelection();
                        //ddlNationality.Items.FindByValue(Aadhaar.Cont_NationalityID).Selected = true;
                        //ddlstate.ClearSelection();
                        //ddlstate.Items.FindByValue(Aadhaar.Cont_StateID).Selected = true;
                        //ddlReligion.ClearSelection();
                        //ddlReligion.Items.FindByValue(Aadhaar.Cont_ReligionID).Selected = true;
                        //ddlGender.ClearSelection();
                        //ddlGender.Items.FindByText(Aadhaar.Cont_Gender).Selected = true;
                        ////ddlother.ClearSelection();
                        ////ddlother.SelectedItem.Text = Aadhaar.o
                        //imgPicture.ImageUrl = "~/GetImages.ashx?id=" + Aadhaar.Cont_Id;
                        //lblContID.Text = Convert.ToString(Aadhaar.Cont_Id);
                        //lblcno.Text = Aadhaar.Cont_CardNo;
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

        protected void txtOtherDoc_TextChanged(object sender, EventArgs e)
        {
            //if (ddlother.SelectedItem.Text.Trim() == "PAN CARD")
            //{
            //    txtOtherDoc.Attributes.Add("pattern", "[A-Z]{5}[0-9]{4}[A-Z]{1}");
            //    txtOtherDoc.MaxLength = 10;
            //    //txtOtherDoc.Text = "";
            //}
            //if (ddlother.SelectedItem.Text.Trim() == "AADHAR CARD")
            //{
            //    txtOtherDoc.Attributes.Add("pattern", "[0-9]{12}");
            //    txtOtherDoc.MaxLength = 12;
            //}
        }

        protected void ddlFirm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void txtDockyardID_TextChanged(object sender, EventArgs e)
        {

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
                //btnpnlhome.Visible = true;
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
            }
            if (PassType == "2")
            {
                ddlPvcYN.Visible = false;
                spnHeading.InnerText = "ESCORTED WORKERS PASS";
                // btnpnlhome.Visible = true;
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
            }
            if (PassType == "3")
            {
                ddlPvcYN.Visible = true;
                spnHeading.InnerText = "BANK's / PSU / BEL PASS";
                //btnpnlhome.Visible = true;
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
                spnHeading.InnerText = "CB's / CANTEEN / DHOBI PASS";
                //btnpnlhome.Visible = true;
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
                //btnpnlhome.Visible = false;
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
            }
        }
    }
}