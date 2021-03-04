using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services.Description;
using System.IO;

namespace EntityFrameworkDBF
{
    public partial class visitorUpdate : System.Web.UI.Page
    {
        byte[] data;
        protected ParameterDetails stParameterDetails;
        protected SqlParameterDetails sqlparamterDetaisl;
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataSet ds_employee = new DataSet();
        DataSet ds_host = new DataSet();
        public byte[] ImageSize;
        //DisplayLog objDisplayLog = new DisplayLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    SetErrorMessage("", "");
                    bind_Country();
                    chk_visitor_type.Visible = false;
                    ENABLECONTROL();
                    // ImageConversions.photo = null;
                    if (Session["photo"] != null)
                    {
                        Session["photo"] = null;
                    }
                    pnlAlreadyImage.Visible = false;
                    // btnUploadPicture.Visible = false;
                    Session["ImageData"] = null;
                }
                catch (Exception ex)
                {
                    Response.Write("<script> alert('Error while loading page.')</script>");
                }
            }
        }

        protected void txtSearchVisitor_TextChanged(object sender, EventArgs e)
        {

        }

        public void bind_Country()
        {
            SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
            DataSet sds = new DataSet();
            SqlCommand scmd = new SqlCommand();
            SqlDataAdapter sadp = new SqlDataAdapter();
            DataTable sdt = new DataTable();

            string query = "SELECT ID,Nationality FROM CountryMaster order by Nationality";

            using (scmd = new SqlCommand(query, scon))
            {
                sadp = new SqlDataAdapter(scmd);
                sadp.Fill(sds);
                DataView view = new DataView();
                view.Table = sds.Tables[0];
                Helper.PopulateDropDown(view, drpnationality, "ID", "Nationality");


                drpnationality.ClearSelection();
                drpnationality.Items.FindByText("INDIAN").Selected = true;
            }
        }
        public static object ErrorMessage(string text, string type)
        {
            string strErrorMessage = "";
            switch (type)
            {
                case "Error":
                    strErrorMessage = "<font class='errormessagetype'>Error :</font><font class='errormessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Warning":
                    strErrorMessage = "<font class='warningmessagetype'>Warning :</font><font class='warningmessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Info":
                    strErrorMessage = "<font class='infomessagetype'>Info :</font><font class='infomessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "":
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
            }

            return strErrorMessage;
        }
        public static object ErrorClass(string text, string type)
        {
            string strErrorClass = "";
            switch (type)
            {
                case "Error":
                    strErrorClass = "errormessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Warning":
                    strErrorClass = "warningmessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Info":
                    strErrorClass = "infomessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "":
                    strErrorClass = "";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
            }
            return strErrorClass;
        }
        /// <summary>
        /// SetErrorMessage
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        private void SetErrorMessage(String text, String type)
        {
            lblErrorMessage.InnerHtml = Convert.ToString(ErrorMessage(text, type));
            td_message.Attributes.Add("class", Convert.ToString(ErrorClass(text, type)));
            lblErrorMessage.Visible = null != text && 0 < text.Length;
        }

        protected void btnVisitorSerach_Click(object sender, EventArgs e)
        {
            Search();
        }

        public void Search()
        {
            SetErrorMessage("", "");
            try
            {
                ParameterList.AddParameter.Clear();

                if (ddlSearchBy.SelectedItem.Text == "VISITOR ID")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "ByID";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "MOBILE NUMBER")
                {

                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "MOBILE";
                    //stParameterDetails.Value = "MOBILE NUMBER";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "AADHAR CARD")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.NVarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "AADHAR";
                    //stParameterDetails.Value = "AADHAR CARD";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "PAN NUMBER")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "PAN";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "PASSPORT")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "PASSPORT";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "DRIVING LICENCE")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "DRIVING";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ENABLE();
                    ds = db.CommonCollection.GetAsDataSet("dbo.SEARCH_VISITOR", ParameterList.AddParameter);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int TranID;
                        bind_Country();

                        data = Encoding.ASCII.GetBytes(ds.Tables[0].Rows[0]["VisitiorPhoto"].ToString());
                        Session["ImageData"] = data;
                        TranID = Convert.ToInt32(ds.Tables[0].Rows[0]["VisitorTranID"].ToString());
                        imgPicture.ImageUrl = "~/RetrivedImageHandler.ashx?TranID=" + TranID + "&PassType=VPASS";
                        txtvisitorid.Text = ds.Tables[0].Rows[0]["VisitorTranID"].ToString();
                        txtVisitorName.Text = ds.Tables[0].Rows[0]["VisitorName"].ToString();
                        txtOrganization.Text = ds.Tables[0].Rows[0]["Oragantization"].ToString();
                        TXTFRIMTIN.Text = ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString();
                        if (ds.Tables[0].Rows[0]["ID_Type"].ToString() == "")
                        {
                        }
                        else
                        {
                            ddlidentity.ClearSelection();
                            ddlidentity.Items.FindByText(ds.Tables[0].Rows[0]["ID_Type"].ToString()).Selected = true;
                        }
                        txtidno.Text = ds.Tables[0].Rows[0]["ID_No"].ToString();
                        txtage.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                        if (ds.Tables[0].Rows[0]["Sex"].ToString() == "")
                        {
                        }
                        else
                        {
                            ddlsex.ClearSelection();
                            ddlsex.Items.FindByText(ds.Tables[0].Rows[0]["Sex"].ToString().Trim()).Selected = true;
                        }
                        if (ds.Tables[0].Rows[0]["Nationality"].ToString() == "")
                        {
                        }
                        else
                        {
                            bind_Country();
                            drpnationality.ClearSelection();
                            drpnationality.Items.FindByText(ds.Tables[0].Rows[0]["Nationality"].ToString()).Selected = true;
                        }
                        txtMobileNumber.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                        TXTPURPOSE.Text = ds.Tables[0].Rows[0]["V_Purpose"].ToString();
                        TXTVISITORTYPE.Text = ds.Tables[0].Rows[0]["EmpType"].ToString();
                        TXTDUR.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                        TXTFRIMTIN.Text = ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString();
                        if ((ds.Tables[0].Rows[0]["Status"].ToString()).Contains("green"))
                        {
                            img_visitor_type_indi.ImageUrl = ds.Tables[0].Rows[0]["Status"].ToString();
                            chk_visitor_type.Checked = true;
                            chk_visitor_type.Visible = true;
                            lbl_visitor_color_code.Text = "Green";
                            lbl_visitor_color_code.ForeColor = Color.Green;
                        }
                        else
                        {
                            img_visitor_type_indi.ImageUrl = ds.Tables[0].Rows[0]["Status"].ToString();
                            chk_visitor_type.Checked = false;
                            lbl_visitor_color_code.Text = "YELLOW";
                            lbl_visitor_color_code.ForeColor = Color.Yellow;
                        }
                        pnlAlreadyImage.Visible = true;
                        ds.Reset();
                    }
                    else
                    {
                        //objDisplayLog.CustomMessage("No visitor found", this);
                        Response.Write("<script> alert('No Data Found.')</script>");
                        CLEARCONTROL();
                        lbl_aadhar_no.Text = "";
                        lbl_drivinglic_no.Text = "";
                        lbl_pancard_no.Text = "";
                        lbl_passport_no.Text = "";
                        txtSearchVisitor.Text = "";
                        img_aadhar.ImageUrl = "";
                        img_pancard.ImageUrl = "";
                        img_passport.ImageUrl = "";
                        img_driving_lic.ImageUrl = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('Error while loading page.')</script>");
                //objDisplayLog.CustomMessage("Error while Serach Visitor" + " " + txtVisitorName.Text, this);
            }
            get_visitor_proof_img();
            txtSearchVisitor.Text = "";
        }

        string filepath = "";
        string Filename = "";
        string filetype = "";
        string fstr = "";

        protected void BTNUPDATE_Click(object sender, EventArgs e)
        {
            string tranID = "";

            DataSet ds_test = new DataSet();
            try
            {
                if (VALIDATION() == true)
                {
                    if (pnl.Visible == true)
                    {
                        //Session["ImageData"] = ImageConversions.photo;
                        // ImageSize = ImageConversions.photo;
                        ImageSize = (byte[])Session["photo"];
                        //Session["ImageData"]
                        if (ImageSize == null)
                            ImageSize = (byte[])Session["ImageData"];
                    }
                    else
                    {
                        ImageSize = (byte[])Session["ImageData"];
                    }
                    if (Convert.ToString(flileupload.FileName) != string.Empty)
                    {
                        fstr = Path.GetExtension(flileupload.FileName).Substring(1);
                        if ((fstr == "jpg") || (fstr == "jpg") || (fstr == "gif"))
                            if (flileupload.HasFile)
                            {
                                Filename = Convert.ToString(flileupload.FileName).Substring(0, flileupload.FileName.IndexOf('.'));
                                string fpath = Convert.ToString(Filename + Path.GetExtension(flileupload.FileName));
                                //Save Image
                                flileupload.SaveAs(Server.MapPath("~/Document/" + fpath));
                                filepath = fpath;
                            }
                            else
                            {
                                filepath = "";
                            }
                    }
                    ParameterList.AddParameter.Clear();
                    tranID = txtvisitorid.Text;
                    stParameterDetails.Value = txtvisitorid.Text;
                    stParameterDetails.DataType = SqlDbType.Int;
                    ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

                    stParameterDetails.Value = txtVisitorName.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("VisitorName", stParameterDetails);

                    stParameterDetails.Value = txtOrganization.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("Oragantization", stParameterDetails);

                    stParameterDetails.Value = TXTFRIMTIN.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("Firm_Tin_No", stParameterDetails);

                    stParameterDetails.Value = ddlidentity.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("ID_Type", stParameterDetails);

                    stParameterDetails.Value = txtidno.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("ID_No", stParameterDetails);

                    stParameterDetails.Value = txtage.Text;
                    stParameterDetails.DataType = SqlDbType.Int;
                    ParameterList.AddParameter.Add("Age", stParameterDetails);

                    stParameterDetails.Value = ddlsex.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("Sex", stParameterDetails);

                    stParameterDetails.Value = drpnationality.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("Nationality", stParameterDetails);

                    stParameterDetails.Value = txtMobileNumber.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

                    stParameterDetails.Value = TXTPURPOSE.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("V_Purpose", stParameterDetails);

                    stParameterDetails.Value = TXTVISITORTYPE.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("EmpType", stParameterDetails);

                    stParameterDetails.Value = TXTDUR.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("DURATION", stParameterDetails);

                    ds.Reset();
                    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                    {
                        ds = db.CommonCollection.GetAsDataSet("dbo.UPDATE_VISITOR", ParameterList.AddParameter);
                        //tranID = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        //objDisplayLog.CustomMessage("Error While Updating " + txtVisitorName.Text.ToString().Trim(), this);
                        Response.Write("<script> alert('Error while updating data.')</script>");
                    }
                    {
                        SetErrorMessage("Record Updated Successfully", "Info");
                    }
                }
                else
                {
                    //objDisplayLog.CustomMessage("Error While Updating " + txtVisitorName.Text.ToString().Trim(), this);
                    Response.Write("<script> alert('Error while updating data.')</script>");
                }
            }
            catch (Exception EE)
            {
                //objDisplayLog.CustomMessage("Error While Updating " + txtVisitorName.Text.ToString().Trim(), this);
                Response.Write("<script> alert('Error while updating data.')</script>");
            }
            //Search();
        }

        public void CLEARCONTROL()
        {
            TXTVISITORTYPE.Text = "";
            txtVisitorName.Text = "";
            txtvisitorid.Text = "";
            TXTPURPOSE.Text = "";
            txtOrganization.Text = "";

            txtMobileNumber.Text = "";
            txtidno.Text = "";
            TXTFRIMTIN.Text = "";

            TXTDUR.Text = "";
            txtage.Text = "";

            // ddlidentity.SelectedItem.Text = ""; ;
            //  ddlsex.SelectedItem.Text = "";

        }

        public void ENABLECONTROL()
        {
            TXTVISITORTYPE.Enabled = false;
            txtVisitorName.Enabled = false;
            txtvisitorid.Enabled = false;
            TXTPURPOSE.Enabled = false;
            txtOrganization.Enabled = false;

            txtMobileNumber.Enabled = false;
            txtidno.Enabled = false;
            TXTFRIMTIN.Enabled = false;

            TXTDUR.Enabled = false;
            txtage.Enabled = false;
            drpnationality.Enabled = false;
            ddlidentity.Enabled = false;
            ddlsex.Enabled = false;
            flileupload.Enabled = false;
        }

        public void ENABLE()
        {
            TXTVISITORTYPE.Enabled = true;
            txtVisitorName.Enabled = true;
            txtvisitorid.Enabled = true;
            TXTPURPOSE.Enabled = true;
            txtOrganization.Enabled = true;

            txtMobileNumber.Enabled = true;
            txtidno.Enabled = true;
            TXTFRIMTIN.Enabled = true;

            TXTDUR.Enabled = true;
            txtage.Enabled = true;
            drpnationality.Enabled = true;
            ddlidentity.Enabled = true;
            ddlsex.Enabled = true;
            flileupload.Enabled = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CLEARCONTROL();
        }

        public bool VALIDATION()
        {
            try
            {
                if (txtVisitorName.Text == "")
                {
                    SetErrorMessage("Please Enter Name", "Error");
                    txtVisitorName.Focus();
                    return false;
                }

                if (txtOrganization.Text == "")
                {
                    SetErrorMessage("Please Enter Firm Name", "Error");
                    txtOrganization.Focus();
                    return false;
                }
                if (txtMobileNumber.Text == "")
                {
                    SetErrorMessage("Please Enter Mobile Number", "Error");
                    txtMobileNumber.Focus();
                    return false;
                }
                if (txtidno.Text == "")
                {
                    SetErrorMessage("Please Enter ID Number", "Error");
                    txtidno.Focus();
                    return false;
                }
                if (TXTFRIMTIN.Text == "")
                {
                    SetErrorMessage("Please Enter Firm Tin Number", "Error");
                    TXTFRIMTIN.Focus();
                    return false;
                }

                if (txtage.Text == "")
                {
                    SetErrorMessage("Please Enter The Age", "Error");
                    txtage.Focus();
                    return false;
                }
                if (drpnationality.SelectedItem.Text == "-1")
                {
                    SetErrorMessage("Please Select The Nationality", "Error");
                    txtVisitorName.Focus();
                    return false;
                }
                if (ddlidentity.SelectedItem.Text == "-1")
                {
                    SetErrorMessage("Please Select Valid Govt ID Type", "Error");
                    txtVisitorName.Focus();
                    return false;
                }
                if (ddlsex.SelectedItem.Text == "-1")
                {
                    SetErrorMessage("Please Select Gender", "Error");
                    txtVisitorName.Focus();
                    return false;
                }

            }
            catch (Exception EX)
            {
                SetErrorMessage("Please Enter Proper Record", "Error");
            }
            return true;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Button imgprint = sender as Button;
            string OFFTRAN = Session["OFFTRAN"].ToString();
            string url = "PrintPass_ForeignVisitor.aspx?VisitorTranID=" + txtvisitorid.Text + "&Off_tran_ID=" + "-1";
            string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }

        protected void btn_add_file_click(object sender, EventArgs e)
        {
            try
            {
                if (ddlidentity.SelectedItem.Value != "-1")
                {
                    if (flileupload.PostedFile.ContentType.Contains("image"))
                    {
                        if (ddlidentity.SelectedItem.Value == "AADHAR CARD")
                        {
                            update_visitor_proof_img("AADHARCARD", txtidno.Text);
                            //SetErrorMessage("Info","Sada saved successfully");
                        }
                        else if (ddlidentity.SelectedItem.Value == "PAN CARD")
                        {
                            update_visitor_proof_img("PANCARD", txtidno.Text);
                            //SetErrorMessage("Info", "Sada saved successfully");
                        }
                        else if (ddlidentity.SelectedItem.Value == "PASSPORT")
                        {
                            update_visitor_proof_img("PASSPORT", txtidno.Text);
                            //SetErrorMessage("Info", "Sada saved successfully");
                        }
                        else if (ddlidentity.SelectedItem.Value == "DRIVING LICENCE")
                        {
                            update_visitor_proof_img("DRIVINGLICENCE", txtidno.Text);
                            //SetErrorMessage("Info", "Sada saved successfully");
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ee)
            {

            }
        }

        public void update_visitor_proof_img(string type, string typeNo)
        {
            Byte[] bytes = new byte[flileupload.PostedFile.ContentLength];
            flileupload.PostedFile.InputStream.Read(bytes, 0, flileupload.PostedFile.ContentLength);
            try
            {
                ParameterList.AddParameter.Clear();

                stParameterDetails.Value = bytes;
                stParameterDetails.DataType = SqlDbType.Image;
                ParameterList.AddParameter.Add("proof_img", stParameterDetails);

                stParameterDetails.Value = type;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("type", stParameterDetails);

                stParameterDetails.Value = txtvisitorid.Text;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("visitorid", stParameterDetails);

                stParameterDetails.Value = typeNo;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("typeNo", stParameterDetails);

                ds.Reset();
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.UPDATE_VISITOR_photo", ParameterList.AddParameter);
                    string tranID = ds.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (Exception ee)
            {
            }
            get_visitor_proof_img();
        }

        public void get_visitor_proof_img()
        {
            try
            {
                ParameterList.AddParameter.Clear();

                if (ddlSearchBy.SelectedItem.Text == "VISITOR ID")
                {
                    if (txtSearchVisitor.Text == "")
                    {
                        txtSearchVisitor.Text = txtvisitorid.Text;
                        stParameterDetails.Value = txtSearchVisitor.Text;
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                        stParameterDetails.Value = "ByID";
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                    }
                    else
                    {
                        stParameterDetails.Value = txtSearchVisitor.Text;
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                        stParameterDetails.Value = "ByID";
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                    }
                }
                else if (ddlSearchBy.SelectedItem.Text == "MOBILE NUMBER")
                {

                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "MOBILE";
                    //stParameterDetails.Value = "MOBILE NUMBER";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "AADHAR CARD")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.NVarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "AADHAR";
                    //stParameterDetails.Value = "AADHAR CARD";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "PAN NUMBER")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "PAN";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "PASSPORT")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "PASSPORT";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                else if (ddlSearchBy.SelectedItem.Text == "DRIVING LICENCE")
                {
                    stParameterDetails.Value = txtSearchVisitor.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "DRIVING";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                }
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.SEARCH_VISITOR", ParameterList.AddParameter);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["AADHAR_DATA"].ToString()))
                        {
                            img_aadhar.ImageUrl = "~/Universal_imgHandler.ashx?visitorID=" + txtvisitorid.Text + "&img_proof_type=AADHAR_DATA";
                        }
                        else
                        {
                            img_aadhar.ImageUrl = "";
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PAN_CARD_DATA"].ToString()))
                        {
                            img_pancard.ImageUrl = "~/Universal_imgHandler.ashx?visitorID=" + txtvisitorid.Text + "&img_proof_type=PAN_CARD_DATA";
                        }
                        else
                        {
                            img_pancard.ImageUrl = "";
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PASSPORT_DATA"].ToString()))
                        {
                            img_passport.ImageUrl = "~/Universal_imgHandler.ashx?visitorID=" + txtvisitorid.Text + "&img_proof_type=PASSPORT_DATA";
                        }
                        else
                        {
                            img_passport.ImageUrl = "";
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DL_DATA"].ToString()))
                        {
                            img_driving_lic.ImageUrl = "~/Universal_imgHandler.ashx?visitorID=" + txtvisitorid.Text + "&img_proof_type=DL_DATA";
                        }
                        else
                        {
                            img_driving_lic.ImageUrl = "";
                        }
                        lbl_aadhar_no.Text = ds.Tables[0].Rows[0]["AADHAR_CARD"].ToString();
                        lbl_drivinglic_no.Text = ds.Tables[0].Rows[0]["DRIVING_LICENCE"].ToString();
                        lbl_pancard_no.Text = ds.Tables[0].Rows[0]["PAN_CARD"].ToString();
                        lbl_passport_no.Text = ds.Tables[0].Rows[0]["PASSPORT"].ToString();
                    }
                    else
                    {
                        img_aadhar.ImageUrl = "";
                        img_pancard.ImageUrl = "";
                        img_passport.ImageUrl = "";
                        img_driving_lic.ImageUrl = "";
                    }
                }
            }
            catch (Exception eee)
            {
            }
        }

        protected void ddlidentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlidentity.Text == "AADHAR CARD")
            {
                txtidno.Attributes.Add("pattern", "[0-9]{12}");
                txtidno.MaxLength = 12;
                txtidno.Text = "";
            }
            else if (ddlidentity.Text == "PAN CARD")
            {
                txtidno.Attributes.Add("pattern", "[A-Z]{5}[0-9]{4}[A-Z]{1}");
                txtidno.MaxLength = 10;
                txtidno.Text = "";
            }
            else if (ddlidentity.Text == "PASSPORT")
            {
                txtidno.Attributes.Remove("pattern");
                txtidno.Text = "";
            }
            else if (ddlidentity.Text == "DRIVING LICENCE")
            {
                txtidno.Attributes.Remove("pattern");
                txtidno.Text = "";
            }
        }

        protected void OnSelectedIndexChanged_ddlSearchBy(object sender, EventArgs e)
        {
            if (ddlSearchBy.Text == "VISITOR ID")
            {
                txtSearchVisitor.Attributes.Remove("pattern");
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
                img_aadhar.ImageUrl = "";
                img_pancard.ImageUrl = "";
                img_passport.ImageUrl = "";
                img_driving_lic.ImageUrl = "";
            }
            else if (ddlSearchBy.Text == "AADHAR CARD")
            {
                //query = "select AADHAR_CARD from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                txtSearchVisitor.Attributes.Add("pattern", "[0-9]{12}");
                txtSearchVisitor.MaxLength = 12;
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
                img_aadhar.ImageUrl = "";
                img_pancard.ImageUrl = "";
                img_passport.ImageUrl = "";
                img_driving_lic.ImageUrl = "";
            }
            else if (ddlSearchBy.Text == "PAN NUMBER")
            {
                //query = "select PAN_CARD from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                txtSearchVisitor.Attributes.Add("pattern", "[A-Z]{5}[0-9]{4}[A-Z]{1}");
                txtSearchVisitor.MaxLength = 10;
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
                img_aadhar.ImageUrl = "";
                img_pancard.ImageUrl = "";
                img_passport.ImageUrl = "";
                img_driving_lic.ImageUrl = "";
            }
            else if (ddlSearchBy.Text == "PASSPORT")
            {
                //query = "select PASSPORT from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                txtSearchVisitor.Attributes.Remove("pattern");
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
                img_aadhar.ImageUrl = "";
                img_pancard.ImageUrl = "";
                img_passport.ImageUrl = "";
                img_driving_lic.ImageUrl = "";
            }
            else if (ddlSearchBy.Text == "DRIVING LICENCE")
            {
                //query = "select DRIVING_LICENCE from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                txtSearchVisitor.Attributes.Remove("pattern");
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
            }
            else if (ddlSearchBy.Text == "MOBILE NUMBER")
            {
                //query = "select DRIVING_LICENCE from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                //txtSearchVisitor.Attributes.Add("pattern", "[0-9]{12}");
                txtSearchVisitor.Attributes.Remove("pattern");
                txtSearchVisitor.MaxLength = 10;
                CLEARCONTROL();
                lbl_aadhar_no.Text = "";
                lbl_drivinglic_no.Text = "";
                lbl_pancard_no.Text = "";
                lbl_passport_no.Text = "";
                txtSearchVisitor.Text = "";
                img_aadhar.ImageUrl = "";
                img_pancard.ImageUrl = "";
                img_passport.ImageUrl = "";
                img_driving_lic.ImageUrl = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (VALIDATION() == true)
                {
                    if (pnl.Visible == true)
                    {
                        //Session["ImageData"] = ImageConversions.photo;
                        // ImageSize = ImageConversions.photo;
                        ImageSize = (byte[])Session["photo"];
                        //Session["ImageData"]
                        if (ImageSize == null)
                            ImageSize = (byte[])Session["ImageData"];
                    }
                    else
                    {
                        ImageSize = (byte[])Session["ImageData"];
                    }

                    ParameterList.AddParameter.Clear();
                    string tranID = txtvisitorid.Text;
                    stParameterDetails.Value = txtvisitorid.Text;
                    stParameterDetails.DataType = SqlDbType.Int;
                    ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

                    stParameterDetails.Value = ImageSize;
                    stParameterDetails.DataType = SqlDbType.Image;
                    ParameterList.AddParameter.Add("VisitiorPhoto", stParameterDetails);

                    ds.Reset();
                    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                    {
                        ds = db.CommonCollection.GetAsDataSet("dbo.UPDATE_VISITORPHOTO", ParameterList.AddParameter);
                        //tranID = ds.Tables[0].Rows[0][0].ToString();
                    }

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        Response.Write("<script> alert('Error while updating data.')</script>");
                    }
                    {
                        SetErrorMessage("Photo Updated Successfully", "Info");
                    }
                }
                else
                {
                    Response.Write("<script> alert('Error while updating data.')</script>");
                }
            }
            catch (Exception)
            {


            }
        }
    }
}