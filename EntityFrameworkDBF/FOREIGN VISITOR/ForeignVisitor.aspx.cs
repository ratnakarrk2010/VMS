using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using Oracle.DataAccess.Client;

namespace EntityFrameworkDBF.FOREIGN_VISITOR
{
    public partial class ForeignVisitor : System.Web.UI.Page
    {
        byte[] data;
        protected ParameterDetails stParameterDetails;
        protected SqlParameterDetails sqlparamterDetaisl;
        DataSet ds = new DataSet();
        DataSet ds_employee = new DataSet();
        DataSet ds_host = new DataSet();
        public byte[] ImageSize;
        //DisplayLog objDisplayLog = new DisplayLog();
        string roll_typeID;
        DateTime FromDate;
        DateTime ToDate;
        protected void Page_Load(object sender, EventArgs e)
        {

            Lblsuccess.Text = "";
            lblcount.ForeColor = System.Drawing.Color.Red;
            try
            {
                roll_typeID = "2";
                Session["Dept"] = "DEO";
                Session["Cen_No"] = "DEO";
            }
            catch (Exception)
            {
            }
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                //PIMS();

                if (roll_typeID == "2")
                {
                    LblDate.Text = DateTime.Now.Date.ToString("dd-MMM-yy").Replace("-", " ").ToUpper();
                    get_data_from_TempCasualVisitor();
                    //Session["Cen_No"].ToString();
                    //Session["Dept"].ToString();
                    TxtEscortdept.Enabled = true;
                    txtdeptship.Enabled = true;

                    lblfile.Visible = true;
                    flileupload.Visible = true;
                }
                else
                {
                    LblDate.Text = DateTime.Now.Date.ToString("dd-MMM-yy").Replace("-", " ").ToUpper();
                    get_data_from_TempCasualVisitor();
                    TxtDeptName.Text = Session["Dept"].ToString();
                    txtdeptship.Text = Session["Dept"].ToString();
                    txtcno.Text = Session["Cen_No"].ToString();
                    TxtEscortdept.Text = Session["Dept"].ToString();
                    TxtEscortdept.Enabled = false;
                    txtdeptship.Enabled = false;

                    lblfile.Visible = false;
                    flileupload.Visible = false;
                }
                load_timer();
                //GridViewRow row = commander_apr.FooterRow;
                //DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("lbl_DURATION");

            }
        }
        protected void btnhome_Click(object sender, EventArgs e)
        {

            Response.Redirect("~\\DVSC_HOME.aspx");

        }
        public void load_timer()
        {
            GridViewRow row = commander_apr.FooterRow;
            DropDownList ddl_from_hr_obj = (DropDownList)row.Cells[2].FindControl("ddl_from_hr");
            DropDownList ddl_from_min_obj = (DropDownList)row.Cells[2].FindControl("ddl_from_min");

            DropDownList ddl_to_hr_obj = (DropDownList)row.Cells[2].FindControl("ddl_to_hr");
            DropDownList ddl_to_min_obj = (DropDownList)row.Cells[2].FindControl("ddl_to_min");

            //ddl_from_hr_obj.Items.Clear();
            //ddl_from_min_obj.Items.Clear();
            //ddl_to_hr_obj.Items.Clear();
            //ddl_to_min_obj.Items.Clear();

            //for (int i = 1; i <= 24; i++)
            //{
            //    if (i.ToString().Length == 1)
            //    {
            //        ddl_from_hr_obj.Items.Add("0" + i.ToString());
            //        ddl_to_hr_obj.Items.Add("0" + i.ToString());
            //    }
            //    else
            //    {
            //        ddl_from_hr_obj.Items.Add(i.ToString());
            //        ddl_to_hr_obj.Items.Add(i.ToString());
            //    }
            //}
            //for (int i = 0; i <= 60; i++)
            //{
            //    if (i.ToString().Length == 1)
            //    {
            //        ddl_from_min_obj.Items.Add("0" + i.ToString());
            //        ddl_to_min_obj.Items.Add("0" + i.ToString());
            //    }
            //    else
            //    {
            //        ddl_from_min_obj.Items.Add(i.ToString());
            //        ddl_to_min_obj.Items.Add(i.ToString());
            //    }
            //}

        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            bind_Country();
        }

        public void bind_Country()
        {
            DropDownList ddlnation = commander_apr.FooterRow.FindControl("ddlnation") as DropDownList;

            SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
            DataSet sds = new DataSet();
            SqlCommand scmd = new SqlCommand();
            SqlDataAdapter sadp = new SqlDataAdapter();
            DataTable sdt = new DataTable();

            string query = "SELECT COUNTRY_ID,NATIONALITY FROM COUNTRY_MASTER order by COUNTRY_NAME ASC ";

            using (scmd = new SqlCommand(query, scon))
            {
                sadp = new SqlDataAdapter(scmd);
                sadp.Fill(sds);
                DataView view = new DataView();
                view.Table = sds.Tables[0];
                Helper.PopulateDropDown(view, ddlnation, "COUNTRY_ID", "NATIONALITY");

                ddlnation.ClearSelection();
                //ddlnation.Items.FindByText("INDIAN").Selected = true;
            }
        }

        //TxtIdNo_TextChanged
        protected void TxtIdNo_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = commander_apr.FooterRow;
            DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            Button btn_add_OBJ = (Button)row.Cells[5].FindControl("btn_add");
            TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_name_desig");
            TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
            TextBox temp_lbl_age_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_age");
            DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[2].FindControl("ddlgen");
            TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_CONTACT_NO");
            DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[2].FindControl("ddlnation");
            TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_FIRM_NAME");
            TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
            DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("lbl_DURATION");
            //DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
            DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[2].FindControl("drpEmptype");
            TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[2].FindControl("txt_PURPOSE_OF_VISIT");
            CheckBox temp_chknew_obj = (CheckBox)row.Cells[2].FindControl("chkadd");
            CheckBox temp_chktin_obj = (CheckBox)row.Cells[2].FindControl("chktin");
            // Button btn_add = (Button)row.Cells[0].FindControl("btn_add");
            TextBox TxtIdNo = (TextBox)sender;
            TextBox txt_FROMDate_obj = (TextBox)row.Cells[15].FindControl("txt_FROMDate");
            TextBox txt_ToDate_obj = (TextBox)row.Cells[16].FindControl("txt_ToDate");
            DataSet ex1 = get_data_from_DB("select * from TempCasualVisitor where ID_NUMBER='" + TxtIdNo.Text + "' and ID_TYPE='" + temp_drpIDType_obj.SelectedItem.Text + "' and EMP_Type = 'FOREIGN'");
            if (ex1.Tables[0].Rows.Count != 0)
            {
                txt_pass_no_obj.Text = ex1.Tables[0].Rows[0]["Passno"].ToString();
                temp_lbl_name_desig_obj.Text = "";
                temp_lbl_age_obj.Text = "";
                temp_lbl_CONTACT_NO_obj.Text = "";
                temp_drpIDType_obj.ClearSelection();
                temp_drpIDType_obj.SelectedItem.Value = "3";
                temp_TxtIdNo_obj.Text = "";
                temp_lbl_FIRM_NAME_obj.Text = "";
                temp_lblFirm_Tin_obj.Text = "";
                txt_PURPOSE_OF_VISIT_obj.Text = "";
                txt_pass_no_obj.Text = "";
                temp_chknew_obj.Checked = false;
                txt_pass_no_obj.Enabled = true;
                temp_lbl_Gender_obj.ClearSelection();
                temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                Lblsuccess.Text = "Visitor already in queue";
            }
            else
            {
                DataSet ds = get_data_from_DB("select * from Officer_Transaction where V_ID_NO='" + TxtIdNo.Text + "' and V_ID_TYPE='" + temp_drpIDType_obj.SelectedItem.Text + "' and V_Date=convert(date,GETDATE()) and Emp_Type = 'FOREIGN'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    temp_lbl_name_desig_obj.Text = "";
                    temp_lbl_age_obj.Text = "";
                    temp_lbl_CONTACT_NO_obj.Text = "";
                    temp_drpIDType_obj.ClearSelection();
                    temp_drpIDType_obj.SelectedItem.Value = "3";
                    temp_TxtIdNo_obj.Text = "";
                    temp_lbl_FIRM_NAME_obj.Text = "";
                    temp_lblFirm_Tin_obj.Text = "";
                    txt_PURPOSE_OF_VISIT_obj.Text = "";
                    txt_pass_no_obj.Text = "";
                    temp_lbl_Gender_obj.ClearSelection();
                    temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                    Lblsuccess.Text = "Visitor Pass already sent for approval";
                    btn_add_OBJ.Enabled = false;
                }
                else
                {
                    try
                    {
                        string a = TxtIdNo.Text;
                        //
                        //GridViewRow row = commander_apr.FooterRow;

                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            ds = get_data_from_DB("select * from VisitorTransactionDetail where ID_Type='" + temp_drpIDType_obj.SelectedItem.Text + "' and ID_No='" + TxtIdNo.Text + "' and PassType = 'FOREIGN'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txt_pass_no_obj.Enabled = true;

                                txt_pass_no_obj.Text = ds.Tables[0].Rows[0]["VisitorTranID"].ToString();
                                temp_lbl_name_desig_obj.Text = ds.Tables[0].Rows[0]["VisitorName"].ToString();
                                temp_lbl_age_obj.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                                FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]);
                                txt_FROMDate_obj.Text = FromDate.ToString("dd/MM/yyyy");
                                ToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]);
                                txt_ToDate_obj.Text = ToDate.ToString("dd/MM/yyyy");
                                if (ds.Tables[0].Rows[0]["sex"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_lbl_Gender_obj.ClearSelection();
                                    temp_lbl_Gender_obj.Items.FindByText(ds.Tables[0].Rows[0]["sex"].ToString().Trim()).Selected = true;
                                }
                                temp_lbl_CONTACT_NO_obj.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                                //temp_lbl_CONTACT_NO_obj.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                                if (ds.Tables[0].Rows[0]["Oragantization"].ToString() == "")
                                {
                                    temp_lbl_FIRM_NAME_obj.Enabled = true;
                                }
                                else
                                {
                                    temp_lbl_FIRM_NAME_obj.Text = ds.Tables[0].Rows[0]["Oragantization"].ToString();
                                    temp_lbl_FIRM_NAME_obj.Enabled = false;
                                }
                                if (ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString() == "")
                                {
                                    temp_lblFirm_Tin_obj.Text = "";
                                    temp_chktin_obj.Enabled = true;
                                    temp_lblFirm_Tin_obj.Enabled = true;
                                }
                                else
                                {
                                    temp_lblFirm_Tin_obj.Text = ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString();
                                    temp_chktin_obj.Enabled = false;
                                    temp_lblFirm_Tin_obj.Enabled = false;
                                }
                                if (ds.Tables[0].Rows[0]["ID_Type"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_drpIDType_obj.ClearSelection();
                                    temp_drpIDType_obj.Items.FindByText(ds.Tables[0].Rows[0]["ID_Type"].ToString()).Selected = true;
                                }
                                if (ds.Tables[0].Rows[0]["ID_No"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_TxtIdNo_obj.Text = ds.Tables[0].Rows[0]["ID_No"].ToString();
                                }
                                if (ds.Tables[0].Rows[0]["EmpType"].ToString() == "")
                                {
                                }
                                else
                                {
                                    chk_EMP_Type_obj.ClearSelection();
                                    chk_EMP_Type_obj.Items.FindByText(ds.Tables[0].Rows[0]["EmpType"].ToString()).Selected = true;
                                }
                                if (ds.Tables[0].Rows[0]["Nationality"].ToString() == "")
                                {
                                }
                                else
                                {
                                    //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "-1";
                                    //bind_Country();
                                    temp_lbl_NATIONALITY_obj.ClearSelection();
                                    temp_lbl_NATIONALITY_obj.Items.FindByText(ds.Tables[0].Rows[0]["Nationality"].ToString()).Selected = true;
                                }

                                temp_chknew_obj.Checked = false;
                                temp_lbl_name_desig_obj.Enabled = false;
                                temp_lbl_age_obj.Enabled = false;
                                temp_lbl_Gender_obj.Enabled = false;
                                //btn_add_OBJ.Enabled = true;
                                temp_lblFirm_Tin_obj.Enabled = false;
                            }
                        }
                        btn_add_OBJ.Enabled = true;
                    }
                    catch (Exception ee)
                    {
                    }
                }
            }
            try
            {
                ParameterList.AddParameter.Clear();
                stParameterDetails.Value = txt_pass_no_obj.Text;
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);
                ds.Reset();
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.usp_CountVisit", ParameterList.AddParameter);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblcount.Text = "<b>Visited Count</b> " + ds.Tables[0].Rows[0][0].ToString();

                        //if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) > 10)
                        //{
                        //    objDisplayLog.CustomMessage(txt_pass_no_obj.Text + " already visited " + ds.Tables[0].Rows[0][0] + " times please submit workorder detail.", this);
                        //    //ClearAllControls();
                        //    lblcount.Text = "";
                        //    txt_pass_no_obj.Text = "";
                        //}

                        //else
                        //{
                        //    //btn_add_OBJ.Enabled = false;
                        //}
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        protected void txt_pass_no_textChanged(object sender, EventArgs e)
        {
            GridViewRow row = commander_apr.FooterRow;
            Button btn_add_OBJ = (Button)row.Cells[5].FindControl("btn_add");
            TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_name_desig");
            TextBox temp_lbl_age_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_age");
            DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[2].FindControl("ddlgen");
            TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_CONTACT_NO");
            DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[2].FindControl("ddlnation");
            TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_FIRM_NAME");
            TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
            DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("lbl_DURATION");
            DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
            DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[2].FindControl("drpEmptype");
            TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[2].FindControl("txt_PURPOSE_OF_VISIT");
            //Button btn_add = (Button)row.Cells[0].FindControl("btn_add");
            TextBox txt_pass_no_obj = (TextBox)sender;
            CheckBox temp_chktin_obj = (CheckBox)row.Cells[2].FindControl("chktin");
            CheckBox temp_chknew_obj = (CheckBox)row.Cells[2].FindControl("chkadd");
            TextBox txt_FROMDate_obj = (TextBox)row.Cells[15].FindControl("txt_FROMDate");
            TextBox txt_ToDate_obj = (TextBox)row.Cells[16].FindControl("txt_ToDate");

            DataSet exists = get_data_from_DB("select * from TempCasualVisitor where Passno='" + txt_pass_no_obj.Text + "' and EMP_Type = 'FOREIGN'");
            if (exists.Tables[0].Rows.Count != 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Visitor already in queue')", true);
                temp_lbl_name_desig_obj.Text = "";
                temp_lbl_age_obj.Text = "";
                temp_lbl_CONTACT_NO_obj.Text = "";
                temp_drpIDType_obj.ClearSelection();
                temp_drpIDType_obj.SelectedItem.Value = "3";
                temp_TxtIdNo_obj.Text = "";
                temp_lbl_FIRM_NAME_obj.Text = "";
                temp_lblFirm_Tin_obj.Text = "";
                txt_PURPOSE_OF_VISIT_obj.Text = "";
                txt_pass_no_obj.Text = "";
                temp_chknew_obj.Checked = false;
                txt_pass_no_obj.Enabled = true;
                temp_lbl_Gender_obj.ClearSelection();
                temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                txt_ToDate_obj.Text = "";
                txt_FROMDate_obj.Text = "";
                Lblsuccess.Text = "Visitor already in queue";
            }
            else
            {
                DataSet ds1 = get_data_from_DB("select * from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'and PassType = 'FOREIGN'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = get_data_from_DB("select * from Officer_Transaction where V_PassNo='" + txt_pass_no_obj.Text + "' and V_Date=convert(date,GETDATE()) and Emp_Type = 'FOREIGN'");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        try
                        {
                            int a = Convert.ToInt32(txt_pass_no_obj.Text);
                            ParameterList.AddParameter.Clear();
                            stParameterDetails.Value = txt_pass_no_obj.Text;
                            stParameterDetails.DataType = SqlDbType.Int;
                            ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);
                            ds.Reset();
                            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.usp_CountVisit", ParameterList.AddParameter);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    lblcount.Text = "<b>Visited Count</b> " + ds.Tables[0].Rows[0][0].ToString();

                                    //if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) > 5)
                                    //{
                                    //    //objDisplayLog.CustomMessage(txt_pass_no_obj.Text + " already visited " + ds.Tables[0].Rows[0][0] + " times please submit workorder detail.", this);
                                    //    //ClearAllControls();
                                    //    //lblcount.Text = "";
                                    //    //txt_pass_no_obj.Text = "";
                                    //}
                                    //else
                                    //{
                                    ds = db.CommonCollection.GetAsDataSet("dbo.Select_Casualvisitor_Details_byTranID", ParameterList.AddParameter);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {

                                        temp_lbl_name_desig_obj.Text = ds.Tables[0].Rows[0]["VisitorName"].ToString();
                                        temp_lbl_age_obj.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                                        FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]);
                                        txt_FROMDate_obj.Text = FromDate.ToString("dd/MM/yyyy");
                                        ToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]);
                                        txt_ToDate_obj.Text = ToDate.ToString("dd/MM/yyyy");
                                        if (ds.Tables[0].Rows[0]["sex"].ToString() == "")
                                        {
                                        }
                                        else
                                        {
                                            temp_lbl_Gender_obj.ClearSelection();
                                            temp_lbl_Gender_obj.Items.FindByText(ds.Tables[0].Rows[0]["sex"].ToString().Trim()).Selected = true;
                                        }
                                        temp_lbl_CONTACT_NO_obj.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();

                                        if (ds.Tables[0].Rows[0]["Oragantization"].ToString() == "")
                                        {
                                            temp_lbl_FIRM_NAME_obj.Enabled = true;
                                            temp_lbl_FIRM_NAME_obj.Text = "";
                                        }
                                        else
                                        {
                                            temp_lbl_FIRM_NAME_obj.Text = ds.Tables[0].Rows[0]["Oragantization"].ToString();
                                            temp_lbl_FIRM_NAME_obj.Enabled = false;
                                        }
                                        if (ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString() == "")
                                        {
                                            temp_lblFirm_Tin_obj.Text = "";
                                            temp_chktin_obj.Enabled = true;
                                        }
                                        else
                                        {
                                            temp_lblFirm_Tin_obj.Text = ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString();
                                            temp_chktin_obj.Enabled = false;

                                        }
                                        if (ds.Tables[0].Rows[0]["ID_Type"].ToString() == "")
                                        {
                                        }
                                        else
                                        {
                                            temp_drpIDType_obj.ClearSelection();
                                            temp_drpIDType_obj.Items.FindByText(ds.Tables[0].Rows[0]["ID_Type"].ToString()).Selected = true;
                                        }
                                        //temp_drpIDType_obj.SelectedItem.Value = "-1";
                                        temp_TxtIdNo_obj.Text = "";
                                        if (ds.Tables[0].Rows[0]["ID_No"].ToString() == "")
                                        {
                                        }
                                        else
                                        {
                                            temp_TxtIdNo_obj.Text = ds.Tables[0].Rows[0]["ID_No"].ToString();
                                        }
                                        if (ds.Tables[0].Rows[0]["EmpType"].ToString() == "")
                                        {
                                        }
                                        else
                                        {
                                            chk_EMP_Type_obj.ClearSelection();
                                            chk_EMP_Type_obj.Items.FindByText(ds.Tables[0].Rows[0]["EmpType"].ToString()).Selected = true;
                                        }
                                        if (ds.Tables[0].Rows[0]["Nationality"].ToString() == "")
                                        {
                                            bind_Country();
                                        }
                                        else
                                        {
                                            temp_lbl_NATIONALITY_obj.ClearSelection();
                                            //bind_Country();
                                            temp_lbl_NATIONALITY_obj.Items.FindByText(ds.Tables[0].Rows[0]["Nationality"].ToString()).Selected = true;
                                        }

                                        btn_add_OBJ.Enabled = true;
                                        Txtofcrdesignation.Enabled = true;
                                        TxtOfcrname.Enabled = true;
                                        TxtEscortrank.Enabled = true;
                                        TxtEscortname.Enabled = true;
                                        temp_lbl_name_desig_obj.Enabled = false;
                                        temp_lbl_age_obj.Enabled = false;
                                        temp_lbl_Gender_obj.Enabled = false;
                                        //btn_add_OBJ.Enabled = true;
                                        temp_lblFirm_Tin_obj.Enabled = false;

                                    }
                                    else
                                    {
                                        btn_add_OBJ.Enabled = false;
                                    }
                                }
                            }
                            //}
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        txt_pass_no_obj.Text = "";
                        temp_lbl_name_desig_obj.Text = "";
                        temp_lbl_age_obj.Text = "";
                        //temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                        temp_lbl_Gender_obj.ClearSelection();
                        temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                        temp_lbl_CONTACT_NO_obj.Text = "";
                        //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "INDIAN";
                        temp_drpIDType_obj.ClearSelection();
                        temp_drpIDType_obj.SelectedItem.Value = "3";
                        temp_TxtIdNo_obj.Text = "";
                        temp_lbl_FIRM_NAME_obj.Text = "";
                        temp_lblFirm_Tin_obj.Text = "";
                        txt_PURPOSE_OF_VISIT_obj.Text = "";
                        txt_ToDate_obj.Text = "";
                        txt_FROMDate_obj.Text = "";
                        Lblsuccess.Text = "Visitor Pass already sent for approval";
                        //btn_add.Enabled = false; 
                    }
                }
                else
                {
                    temp_lbl_name_desig_obj.Text = "";
                    temp_lbl_age_obj.Text = "";
                    //temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                    temp_lbl_CONTACT_NO_obj.Text = "";
                    //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "INDIAN";
                    temp_drpIDType_obj.SelectedItem.Value = "-1";
                    temp_TxtIdNo_obj.Text = "";
                    temp_lbl_FIRM_NAME_obj.Text = "";
                    temp_lblFirm_Tin_obj.Text = "";
                    txt_PURPOSE_OF_VISIT_obj.Text = "";
                    //lbl_DURATION_OBJ.SelectedItem.Value = "-1";
                    //chk_EMP_Type_obj.SelectedItem.Value = "-1";
                    txt_ToDate_obj.Text = "";
                    txt_FROMDate_obj.Text = "";
                    Lblsuccess.Text = "This Is Not Registered or belongs to Casual Visitor Please search on Casual Visitor or select New";
                    lblcount.Text = "";

                }
            }
        }

        protected void OnTextChanged_temp_lbl_CONTACT_NO(object sender, EventArgs e)
        {
            GridViewRow row = commander_apr.FooterRow;
            DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            Button btn_add_OBJ = (Button)row.Cells[5].FindControl("btn_add");
            TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_name_desig");
            TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
            TextBox temp_lbl_age_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_age");
            DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[2].FindControl("ddlgen");
            //TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_CONTACT_NO");
            DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[2].FindControl("ddlnation");
            TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[2].FindControl("temp_lbl_FIRM_NAME");
            TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
            DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("lbl_DURATION");
            //DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
            DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[2].FindControl("drpEmptype");
            TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[2].FindControl("txt_PURPOSE_OF_VISIT");
            // Button btn_add = (Button)row.Cells[0].FindControl("btn_add");
            CheckBox temp_chktin_obj = (CheckBox)row.Cells[2].FindControl("chktin");
            CheckBox temp_chknew_obj = (CheckBox)row.Cells[2].FindControl("chkadd");
            TextBox temp_lbl_CONTACT_NO_obj = (TextBox)sender;
            TextBox txt_FROMDate_obj = (TextBox)row.Cells[15].FindControl("txt_FROMDate");
            TextBox txt_ToDate_obj = (TextBox)row.Cells[16].FindControl("txt_ToDate");

            DataSet ex1 = get_data_from_DB("select * from TempCasualVisitor where ContactNo='" + temp_lbl_CONTACT_NO_obj.Text + "' and EMP_Type = 'FOREIGN' ");
            if (ex1.Tables[0].Rows.Count != 0)
            {
                txt_pass_no_obj.Text = ex1.Tables[0].Rows[0]["Passno"].ToString();
                temp_lbl_name_desig_obj.Text = "";
                temp_lbl_age_obj.Text = "";
                temp_lbl_CONTACT_NO_obj.Text = "";
                temp_drpIDType_obj.ClearSelection();
                temp_drpIDType_obj.SelectedItem.Value = "3";
                temp_TxtIdNo_obj.Text = "";
                temp_lbl_FIRM_NAME_obj.Text = "";
                temp_lblFirm_Tin_obj.Text = "NA";
                txt_PURPOSE_OF_VISIT_obj.Text = "";
                lblcount.Text = "";
                txt_pass_no_obj.Text = "";
                temp_chknew_obj.Checked = false;
                txt_pass_no_obj.Enabled = true;
                temp_lbl_Gender_obj.ClearSelection();
                temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                txt_ToDate_obj.Text = "";
                txt_FROMDate_obj.Text = "";
                //Lblsuccess.Text = "This Is Not Registered or belongs to Casual Visitor Please search on Casual Visitor or select New";
                Lblsuccess.Text = "Visitor already in queue";
            }
            else
            {
                DataSet ds = get_data_from_DB("select * from Officer_Transaction where V_ContactNo='" + temp_lbl_CONTACT_NO_obj.Text + "' and  V_Date=convert(date,GETDATE()) and EMP_Type = 'FOREIGN'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    temp_lbl_name_desig_obj.Text = "";
                    temp_lbl_age_obj.Text = "";
                    temp_lbl_CONTACT_NO_obj.Text = "";
                    temp_drpIDType_obj.ClearSelection();
                    temp_drpIDType_obj.SelectedItem.Value = "3";
                    temp_TxtIdNo_obj.Text = "";
                    temp_lbl_FIRM_NAME_obj.Text = "";
                    temp_lblFirm_Tin_obj.Text = "NA";
                    txt_PURPOSE_OF_VISIT_obj.Text = "";
                    txt_pass_no_obj.Text = "";
                    temp_chknew_obj.Checked = false;
                    temp_lbl_Gender_obj.ClearSelection();
                    temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                    txt_ToDate_obj.Text = "";
                    txt_FROMDate_obj.Text = "";
                    //Lblsuccess.Text = "This Is Not Registered or belongs to Casual Visitor Please search on Casual Visitor or select New";
                    Lblsuccess.Text = "Visitor Pass already sent for approval";
                    btn_add_OBJ.Enabled = false;
                    //ClearData();
                }
                else
                {
                    try
                    {
                        string a = temp_lbl_CONTACT_NO_obj.Text;
                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            ds = get_data_from_DB("select * from VisitorTransactionDetail where MobileNumber='" + temp_lbl_CONTACT_NO_obj.Text + "' and PassType = 'FOREIGN'");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txt_pass_no_obj.Text = ds.Tables[0].Rows[0]["VisitorTranID"].ToString();
                                temp_lbl_name_desig_obj.Text = ds.Tables[0].Rows[0]["VisitorName"].ToString();
                                temp_lbl_age_obj.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                                FromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]);
                                txt_FROMDate_obj.Text = FromDate.ToString("dd/MM/yyyy");
                                ToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]);
                                txt_ToDate_obj.Text = ToDate.ToString("dd/MM/yyyy");
                                if (ds.Tables[0].Rows[0]["sex"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_lbl_Gender_obj.ClearSelection();
                                    temp_lbl_Gender_obj.Items.FindByText(ds.Tables[0].Rows[0]["sex"].ToString().Trim()).Selected = true;
                                }
                                temp_lbl_CONTACT_NO_obj.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                                if (ds.Tables[0].Rows[0]["Oragantization"].ToString() == "")
                                {
                                    temp_lbl_FIRM_NAME_obj.Enabled = true;
                                }
                                else
                                {
                                    temp_lbl_FIRM_NAME_obj.Text = ds.Tables[0].Rows[0]["Oragantization"].ToString();
                                    temp_lbl_FIRM_NAME_obj.Enabled = false;
                                }
                                if (ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString() == "")
                                {
                                    temp_lblFirm_Tin_obj.Text = "";
                                    temp_chktin_obj.Enabled = true;
                                }
                                else
                                {
                                    temp_lblFirm_Tin_obj.Text = ds.Tables[0].Rows[0]["Firm_Tin_No"].ToString();
                                    temp_chktin_obj.Enabled = false;

                                }
                                if (ds.Tables[0].Rows[0]["ID_Type"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_drpIDType_obj.ClearSelection();
                                    temp_drpIDType_obj.Items.FindByText(ds.Tables[0].Rows[0]["ID_Type"].ToString()).Selected = true;
                                }
                                if (ds.Tables[0].Rows[0]["ID_No"].ToString() == "")
                                {
                                }
                                else
                                {
                                    temp_TxtIdNo_obj.Text = ds.Tables[0].Rows[0]["ID_No"].ToString();
                                }
                                if (ds.Tables[0].Rows[0]["EmpType"].ToString() == "")
                                {
                                }
                                else
                                {
                                    chk_EMP_Type_obj.ClearSelection();
                                    chk_EMP_Type_obj.Items.FindByText(ds.Tables[0].Rows[0]["EmpType"].ToString()).Selected = true;
                                }
                                if (ds.Tables[0].Rows[0]["Nationality"].ToString() == "")
                                {
                                }
                                else
                                {
                                    //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "-1";
                                    //bind_Country();
                                    temp_lbl_NATIONALITY_obj.ClearSelection();
                                    temp_lbl_NATIONALITY_obj.Items.FindByText(ds.Tables[0].Rows[0]["Nationality"].ToString()).Selected = true;
                                }
                                temp_chknew_obj.Checked = false;
                                temp_lbl_name_desig_obj.Enabled = false;
                                temp_lbl_age_obj.Enabled = false;
                                temp_lbl_Gender_obj.Enabled = false;
                                //btn_add_OBJ.Enabled = true;
                                temp_lblFirm_Tin_obj.Enabled = false;

                            }
                        }
                        btn_add_OBJ.Enabled = true;
                        //temp_lbl_FIRM_NAME_obj.Enabled = true;
                        //temp_lblFirm_Tin_obj.Text = "NA";
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        protected void btn_add_click(object sender, EventArgs e)
        {
            //pending  
            string from = "";
            string to = "";
            string id = "";

            GridViewRow row = commander_apr.FooterRow;
            TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
            TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[3].FindControl("temp_lbl_name_desig");
            TextBox temp_lbl_age_obj = (TextBox)row.Cells[4].FindControl("temp_lbl_age");
            DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[5].FindControl("ddlgen");
            TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[6].FindControl("temp_lbl_CONTACT_NO");
            DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[7].FindControl("ddlnation");
            DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
            TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[10].FindControl("temp_lbl_FIRM_NAME");
            TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[11].FindControl("temp_lblFirm_Tin");
            TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[12].FindControl("txt_PURPOSE_OF_VISIT");
            DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[13].FindControl("DDLDURATION");
            DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[14].FindControl("drpEmptype");
            TextBox txt_FROMDate_obj = (TextBox)row.Cells[15].FindControl("txt_FROMDate");
            TextBox txt_ToDate_obj = (TextBox)row.Cells[16].FindControl("txt_ToDate");
            try
            {
                if (AddValidation() == true)
                {
                    ParameterList.AddParameter.Clear();
                    stParameterDetails.Value = txt_pass_no_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("pass_no", stParameterDetails);

                    stParameterDetails.Value = txt_FROMDate_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FROM", stParameterDetails);

                    stParameterDetails.Value = txt_ToDate_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("To", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_name_desig_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("name_desig", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_age_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("age", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_Gender_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("Gender", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_CONTACT_NO_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("CONTACT_NO", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_NATIONALITY_obj.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("NATIONALITY", stParameterDetails);

                    stParameterDetails.Value = temp_drpIDType_obj.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("ID_TYPE", stParameterDetails);

                    stParameterDetails.Value = temp_TxtIdNo_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("ID_NUMBER", stParameterDetails);

                    stParameterDetails.Value = temp_lbl_FIRM_NAME_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FIRM_NAME", stParameterDetails);

                    stParameterDetails.Value = temp_lblFirm_Tin_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.NVarChar;
                    ParameterList.AddParameter.Add("V_FIRM_TIN", stParameterDetails);

                    stParameterDetails.Value = lbl_DURATION_OBJ.SelectedItem.Text;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("DURATION", stParameterDetails);

                    stParameterDetails.Value = txt_PURPOSE_OF_VISIT_obj.Text.Trim();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("PURPOSE_OF_VISIT", stParameterDetails);

                    stParameterDetails.Value = Session["username"].ToString();
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("LoginBy", stParameterDetails);

                    if (chk_EMP_Type_obj.SelectedItem.Text == "EXECUTIVE")
                    {
                        stParameterDetails.Value = "EXECUTIVE";
                    }
                    else if (chk_EMP_Type_obj.SelectedItem.Text == "CASUAL")
                    {
                        stParameterDetails.Value = "CASUAL";
                    }
                    else if (chk_EMP_Type_obj.SelectedItem.Text == "RETIRED")
                    {
                        stParameterDetails.Value = "RETIRED";
                    }
                    else
                    {
                        stParameterDetails.Value = "FOREIGN";
                    }
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("EMP_Type", stParameterDetails);

                    ds.Reset();
                    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                    {
                        ds = db.CommonCollection.GetAsDataSet("dbo.TempForVisitor_DATA_Insert", ParameterList.AddParameter);
                    }
                    id = ds.Tables[0].Rows[0]["ID"].ToString();
                    hdn_latest_added_TempID.Value = id;
                    load_timer();
                    show_gadget_vehical_list(id);
                    get_data_from_TempCasualVisitor();
                }

            }
            catch (Exception ex)
            {

            }
            Firm_Insert(temp_lbl_FIRM_NAME_obj, temp_lblFirm_Tin_obj);
        }

        public DataSet get_data_from_TempCasualVisitor()
        {
            DataSet ds1 = new System.Data.DataSet();
            ParameterList.AddParameter.Clear();
            int i = 0;
            Session["username"] = "DEO";
            stParameterDetails.Value = Session["username"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("LoginId1", stParameterDetails);
            ds.Reset();
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                ds = db.CommonCollection.GetAsDataSet("dbo.Select_TempCasualVisitor_Foreign", ParameterList.AddParameter);
                ds1 = ds;
                DataTable dt = ds.Tables[0];
                DataRow row = dt.NewRow();
                row[1] = "";
                row[2] = "";
                row[3] = "";
                row[4] = "";
                row[5] = "";
                row[6] = "";
                row[7] = "";
                //row[8] =DateTime.nu;
                //row[9] = "";
                row[10] = "";
                row[11] = "";
                row[12] = "";
                row[13] = "";
                row[14] = "";
                dt.Rows.Add(row);
                commander_apr.DataSource = dt;
                commander_apr.DataBind();
                i = commander_apr.Rows.Count;
                Button btn_to_hide = (Button)commander_apr.Rows[i - 1].Cells[5].FindControl("btn_delete");
                btn_to_hide.Visible = false;
                commander_apr.Rows[i - 1].Cells[0].Text = "";
                commander_apr.Rows[i - 1].Visible = false;
            }
            return ds1;
        }

        public void show_gadget_vehical_list(string ID)
        {
            //   rdo_Gadget_no.Checked = true;
            //rdo_vehical_no.Checked = true;

            lbl_Vehical.Visible = true;
            rdo_vehical_yes.Visible = true;
            rdo_vehical_no.Visible = true;
            lbl_elect_Device.Visible = true;
            rdo_Gadget_yes.Visible = true;
            rdo_Gadget_no.Visible = true;

            Grd_Gadget_list.Visible = false;
            Grd_Vehicle_list.Visible = false;

            lbl_Vehical.Text = "IS VISITOR BRINGING VEHICLE";
            lbl_elect_Device.Text = "IS VISITOR BRINGING ELECTRONIC DEVICE";

            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void rdo_Gadget_no_click(object sender, EventArgs e) // add Vehicle
        {
            string query = "delete from TEMP_GV where type='GADGET' and TEMP_V_ID='" + hdn_latest_added_TempID.Value + "'";
            Update_Delete_DB(query);
            //ModalPopupExtender_Vehiclegadget.Show();
            //    show_gadget_vehical_list(hdn_latest_added_TempID.Value);

            lbl_elect_Device.Visible = true;
            rdo_Gadget_yes.Visible = true;
            rdo_Gadget_no.Visible = true;
            Grd_Gadget_list.Visible = false;
            get_data_from_TempCasualVisitor();
            ModalPopupExtender_Vehiclegadget.Show();
            //  Grd_Vehicle_list.Visible = true;
            // lbl_Vehical.Visible = true;
            //   rdo_vehical_no.Visible = true;
            // rdo_vehical_yes.Visible = true;
        }

        protected void rdo_vehical_no_click(object sender, EventArgs e) // add Vehicle
        {
            //Grd_Vehicle_list.Visible = false;

            string query = "delete from TEMP_GV where type='VEHICLE' and TEMP_V_ID='" + hdn_latest_added_TempID.Value + "'";
            Update_Delete_DB(query);
            //ModalPopupExtender_Vehiclegadget.Show();
            lbl_Vehical.Visible = true;
            rdo_vehical_no.Visible = true;
            rdo_vehical_yes.Visible = true;
            Grd_Vehicle_list.Visible = false;
            //  show_gadget_vehical_list(hdn_latest_added_TempID.Value);
            get_data_from_TempCasualVisitor();
            ModalPopupExtender_Vehiclegadget.Show();
            //Grd_Gadget_list.Visible = true;
            //lbl_elect_Device.Visible = true;
            //rdo_Gadget_no.Visible = true;
            //rdo_Gadget_yes.Visible = true;
        }

        public void Update_Delete_DB(string query)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        protected void rdo_Gadget_yes_click(object sender, EventArgs e) // add Vehicle
        {
            get_gadget_list(hdn_latest_added_TempID.Value);
            Grd_Gadget_list.Visible = true; ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void rdo_vehical_yes_click(object sender, EventArgs e) // add Vehicle
        {
            get_vahical_list(hdn_latest_added_TempID.Value);
            Grd_Vehicle_list.Visible = true;
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void btn_Vehicle_click(object sender, EventArgs e) // add Vehicle
        {
            GridViewRow row = Grd_Vehicle_list.FooterRow;
            TextBox txt_VehicleName_obj = (TextBox)row.FindControl("txt_VehicleName");
            TextBox txt_VehicleNo_obj = (TextBox)row.FindControl("txt_VehicleNo");
            TextBox txt_Vehicle_Purpose_obj = (TextBox)row.FindControl("txt_Vehicle_Purpose");
            if (string.IsNullOrEmpty(txt_VehicleName_obj.Text))
            {
                lblerr.Text = "Please Enter Vehicle Name";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(txt_VehicleNo_obj.Text))
            {
                lblerr.Text = "Please Enter Vehicle Number";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(txt_Vehicle_Purpose_obj.Text))
            {
                lblerr.Text = "Please Enter Purpose of Carrying Vehicle";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                save_data_db("insert into TEMP_GV(Type,NAME,NUMBER,PURPOSE,TEMP_V_ID) values('VEHICLE','" + txt_VehicleName_obj.Text + "','" + txt_VehicleNo_obj.Text + "','" + txt_Vehicle_Purpose_obj.Text + "','" + hdn_latest_added_TempID.Value + "')");
                save_data_db("update TempCasualVisitor set VechicleDetail='Yes' where ID='" + hdn_latest_added_TempID.Value + "'");
                get_data_from_TempCasualVisitor();
                //   get_gadget_list(hdn_latest_added_TempID.Value);
                get_vahical_list(hdn_latest_added_TempID.Value);

                lblerr.Text = "";
            }
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void btn_Gadget_click(object sender, EventArgs e) // add gadget
        {
            GridViewRow row = Grd_Gadget_list.FooterRow;

            TextBox txt_GadgetName_obj = (TextBox)row.FindControl("txt_GadgetName");
            TextBox txt_GadgetNo_obj = (TextBox)row.FindControl("txt_GadgetNo");
            TextBox txt_Gadget_Purpose_obj = (TextBox)row.FindControl("txt_Gadget_Purpose");
            if (string.IsNullOrEmpty(txt_GadgetName_obj.Text))
            {
                lblerr.Text = "Please Enter Gadget Name";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(txt_GadgetNo_obj.Text))
            {
                lblerr.Text = "Please Enter Gadget Number";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrEmpty(txt_Gadget_Purpose_obj.Text))
            {
                lblerr.Text = "Please Enter Purpose of carrying Gadget";
                lblerr.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                save_data_db("insert into TEMP_GV(Type,NAME,NUMBER,PURPOSE,TEMP_V_ID) values('GADGET','" + txt_GadgetName_obj.Text + "','" + txt_GadgetNo_obj.Text + "','" + txt_Gadget_Purpose_obj.Text + "','" + hdn_latest_added_TempID.Value + "')");
                save_data_db("update TempCasualVisitor set GadgetDetail='Yes' where ID='" + hdn_latest_added_TempID.Value + "'");
                get_data_from_TempCasualVisitor();
                get_gadget_list(hdn_latest_added_TempID.Value);

                //   get_vahical_list(hdn_latest_added_TempID.Value);

                lblerr.Text = "";
            }
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void btn_Vehicle_Delete_click(object sender, EventArgs e) // delete  perticular  Vehicle
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string sl = Grd_Vehicle_list.DataKeys[row.RowIndex].Values[0].ToString();
            save_data_db("Delete from TEMP_GV where SL='" + sl + "' and Type='VEHICLE'");
            get_data_from_TempCasualVisitor();
            get_vahical_list(hdn_latest_added_TempID.Value);
            ModalPopupExtender_Vehiclegadget.Show();

        }

        protected void btn_Delete_Gadget_click(object sender, EventArgs e) // delete  perticular  Gadget
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string sl = Grd_Gadget_list.DataKeys[row.RowIndex].Values[0].ToString();
            save_data_db("Delete from TEMP_GV where SL='" + sl + "' and Type='GADGET'");
            get_data_from_TempCasualVisitor();
            get_gadget_list(hdn_latest_added_TempID.Value);
            ModalPopupExtender_Vehiclegadget.Show();
        }

        public void get_gadget_list(string entry_id)
        {
            DataSet ds = new System.Data.DataSet();
            ds = get_data_from_DB("Select * from TEMP_GV where TEMP_V_ID='" + entry_id + "' and Type='GADGET'");
            DataTable dt = ds.Tables[0];
            DataRow row = dt.NewRow();
            row[1] = "";
            row[2] = "";
            row[3] = "";
            row[4] = "";
            row[5] = "";
            dt.Rows.Add(row);
            Grd_Gadget_list.DataSource = dt;
            Grd_Gadget_list.DataBind();
            int i = Grd_Gadget_list.Rows.Count;
            Button btn_to_hide = (Button)Grd_Gadget_list.Rows[i - 1].Cells[4].FindControl("btn_Delete_Gadget");
            btn_to_hide.Visible = false;
            Grd_Gadget_list.Rows[i - 1].Cells[0].Text = "";
            Grd_Gadget_list.Rows[i - 1].Visible = false;

            if (dt.Rows.Count > 1)
            {
                Grd_Gadget_list.Visible = true;
            }
            else
            {
                Grd_Gadget_list.Visible = false;
            }
        }

        public void get_vahical_list(string entry_id)
        {
            //        string query = "select *,(select count(*)   from [Gadget_Details] B where B.Ofcr_Tran_Id=A.Ofcr_Tran_Id) as Gadget,(select count(*)  from [VehicleDetails] c where c.Ofcr_Tran_Id=A.Ofcr_Tran_Id) as Vehicle  from [Officer_Transaction]  A where A.V_Status=" + Status + "and A.Emp_Type='Casual' and  A.V_Date=convert(date,GETDATE())";

            DataSet ds = new System.Data.DataSet();
            ds = get_data_from_DB("Select * from TEMP_GV where TEMP_V_ID='" + entry_id + "' and Type='VEHICLE'");
            DataTable dt = ds.Tables[0];
            DataRow row = dt.NewRow();
            row[1] = "";
            row[2] = "";
            row[3] = "";
            row[4] = "";
            row[5] = "";
            dt.Rows.Add(row);
            Grd_Vehicle_list.DataSource = dt;
            Grd_Vehicle_list.DataBind();
            int i = Grd_Vehicle_list.Rows.Count;
            Button btn_to_hide = (Button)Grd_Vehicle_list.Rows[i - 1].Cells[4].FindControl("btn_Vehicle_Delete");
            btn_to_hide.Visible = false;
            Grd_Vehicle_list.Rows[i - 1].Cells[0].Text = "";
            Grd_Vehicle_list.Rows[i - 1].Visible = false;

            if (dt.Rows.Count > 1)
            {
                Grd_Vehicle_list.Visible = true;
            }
            else
            {
                Grd_Vehicle_list.Visible = false;
            }

        }

        protected void btn_view_vahical_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow grdrow = btn.NamingContainer as GridViewRow;
            HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
            hdn_latest_added_TempID.Value = hdn.Value;
            string entry_id = Temp_VID = hdn.Value;
            if (btn.Text == "Yes")
            {
                //rdo_vehical_yes.Checked = true;
            }
            else
            {
                //rdo_vehical_no.Checked = true;
            }


            lbl_elect_Device.Visible = false;
            rdo_Gadget_yes.Visible = false;
            rdo_Gadget_no.Visible = false;
            Grd_Gadget_list.Visible = false;

            lbl_Vehical.Visible = true;
            rdo_vehical_yes.Visible = true;
            rdo_vehical_no.Visible = true;
            Grd_Vehicle_list.Visible = true;
            lbl_Vehical.Text = "IS VISITOR BRINGING VEHICLE";
            get_vahical_list(entry_id);
            ModalPopupExtender_Vehiclegadget.Show();

        }

        protected void btn_view_Gadget_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow grdrow = btn.NamingContainer as GridViewRow;
            HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
            hdn_latest_added_TempID.Value = hdn.Value;
            if (btn.Text == "Yes")
            {
                // rdo_Gadget_yes.Checked = true;
            }
            else
            {
                //  rdo_Gadget_no.Checked = true;
            }

            string entry_id = Temp_VID = hdn.Value;

            lbl_elect_Device.Visible = true;
            rdo_Gadget_yes.Visible = true;
            rdo_Gadget_no.Visible = true;
            Grd_Gadget_list.Visible = true;

            lbl_Vehical.Visible = false;
            rdo_vehical_yes.Visible = false;
            rdo_vehical_no.Visible = false;
            Grd_Vehicle_list.Visible = false;
            lbl_elect_Device.Text = "IS VISITOR BRINGING ELECTRONIC DEVICE";
            //lbl_elect_Device.Text = "Electronic Gadgegt Detail";
            get_gadget_list(entry_id);
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (roll_typeID == "2")
            {
                if (flileupload.HasFile)
                {
                    int fileID = save_file_toFiles();
                    if (fileID != 0)
                    {
                        ADD(fileID);
                    }
                    else
                    {
                        Lblsuccess.Text = "Unable To Upload File. ";
                    }

                }
                //else
                //{
                //    Lblsuccess.Text = "Please Select File. ";
                //}
                else
                {
                    ADD(0);
                }
            }
            else
            {
                ADD(0);
            }
        }

        public void ADD(int fileID)
        {
            try
            {
                if (Validation() == true)
                {
                    DataTable dynodt = new DataTable();
                    dynodt = GetDynamicdata();
                    if (dynodt.Rows.Count > 1)
                    {
                        for (int j = 0; j < dynodt.Rows.Count; j++)
                        {
                            ParameterList.AddParameter.Clear();
                            if (dynodt.Rows[j][0].ToString() != "")
                            {
                                stParameterDetails.Value = dynodt.Rows[j]["Vname"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_name", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["Passno"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_PassNo", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["Age"].ToString();
                                stParameterDetails.DataType = SqlDbType.Int;
                                ParameterList.AddParameter.Add("V_Age", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["Gender"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_Sex", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["ContactNo"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_ContactNo", stParameterDetails);

                                // Modified  on 17-03-2017
                                stParameterDetails.Value = txtESMobile.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("ES_MobNumber", stParameterDetails);
                                //End Modification

                                stParameterDetails.Value = dynodt.Rows[j]["Nationality"].ToString().ToUpper();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_Nationality", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["FirmName"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_Firm", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["Purpose"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_Purpose", stParameterDetails);

                                stParameterDetails.Value = LblDate.Text;
                                stParameterDetails.DataType = SqlDbType.Date;
                                ParameterList.AddParameter.Add("V_Date", stParameterDetails);

                                stParameterDetails.Value = TxtEscortname.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("ES_Name", stParameterDetails);

                                stParameterDetails.Value = TxtEscortrank.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("ES_Rank", stParameterDetails);

                                stParameterDetails.Value = TxtEscorttoken.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("ES_TokenNo", stParameterDetails);

                                stParameterDetails.Value = TxtEscortdept.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("ES_Dept", stParameterDetails);

                                // CHANGES BY PRAHALAD ON 22-11-2017
                                stParameterDetails.Value = dynodt.Rows[j]["InTime"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_FromTime", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["OutTime"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_ToTime", stParameterDetails);

                                stParameterDetails.Value = txtCLletNo.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("For_CL_Letter_no", stParameterDetails);

                                stParameterDetails.Value = txtSecCl.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("For_Security_CL", stParameterDetails);

                                stParameterDetails.Value = txtSecClValidity.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("For_CL_Validity", stParameterDetails);

                                stParameterDetails.Value = txtVisa.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("For_Visa", stParameterDetails);
                                //@For_Visa	,	
                                //@For_Security_CL,
                                //@For_CL_Letter_no,
                                //@For_CL_Validity
                                // END HERE
                                if (roll_typeID == "2")
                                {
                                    stParameterDetails.Value = "DEO";
                                    stParameterDetails.DataType = SqlDbType.VarChar;
                                    ParameterList.AddParameter.Add("Center_No", stParameterDetails);
                                }
                                else
                                {
                                    stParameterDetails.Value = txtcno.Text;
                                    stParameterDetails.DataType = SqlDbType.VarChar;
                                    ParameterList.AddParameter.Add("Center_No", stParameterDetails);
                                }
                                stParameterDetails.Value = TxtOfcrname.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("Officer_Name", stParameterDetails);

                                stParameterDetails.Value = Txtofcrdesignation.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("Officer_Designation", stParameterDetails);


                                if (roll_typeID == "4")
                                {
                                    stParameterDetails.Value = "1";

                                }
                                else if (roll_typeID == "6")
                                {
                                    stParameterDetails.Value = "3";

                                }
                                else if (roll_typeID == "2")
                                {
                                    stParameterDetails.Value = "2";
                                }
                                stParameterDetails.DataType = SqlDbType.Int;
                                ParameterList.AddParameter.Add("V_Status", stParameterDetails);

                                stParameterDetails.Value = TxtDeptName.Text;
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_Dept", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["EMP_Type"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("Emp_Type", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["ID_TYPE"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_ID_TYPE", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["ID_NUMBER"].ToString();
                                stParameterDetails.DataType = SqlDbType.VarChar;
                                ParameterList.AddParameter.Add("V_ID_NO", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["V_FIRM_TIN"].ToString();
                                stParameterDetails.DataType = SqlDbType.NVarChar;
                                ParameterList.AddParameter.Add("V_FIRM_TIN", stParameterDetails);

                                stParameterDetails.Value = dynodt.Rows[j]["DURATION"].ToString();
                                stParameterDetails.DataType = SqlDbType.NVarChar;
                                ParameterList.AddParameter.Add("DURATION", stParameterDetails);

                                stParameterDetails.Value = fileID;
                                stParameterDetails.DataType = SqlDbType.NVarChar;
                                ParameterList.AddParameter.Add("FIleID", stParameterDetails);

                                string id = dynodt.Rows[j]["ID"].ToString();
                                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                                {
                                    DataSet dsQ = db.CommonCollection.GetAsDataSet("dbo.OfficerTransactionForeign_Insert", ParameterList.AddParameter);
                                    string officer_tran_id = dsQ.Tables[0].Rows[0][0].ToString();
                                    add_gadget_and_vahical(id, officer_tran_id);
                                    delete_from_TempLabourList(id);
                                }
                            }
                        }
                        DataSet getoff = get_data_from_DB("select * from OfficerMaster where Officer_Token = '" + txtofftoken.Text.Trim() + "'");
                        if (getoff.Tables[0].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            SaveOfficerData();
                        }
                        DataSet getESC = get_data_from_DB("select * from EscortMaster where ES_TokenNo = '" + TxtEscorttoken.Text.Trim() + "'");
                        if (getESC.Tables[0].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            SaveEscortData();
                        }
                        lblcount.Text = "";
                        ClearData();
                        if (roll_typeID == "2")
                        {
                            Response.Redirect("~/Operator_Home.aspx");
                        }
                    }
                    else
                    {
                        //Response.Write("<Script> alert('Please Enter Atleast 1 Visitor Information.') </Script>");
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "alert('Please Enter Atleast 1 Visitor Information.')", true);
                    }
                }
            }

            catch (Exception ex)
            {
                // Lblsuccess.Text = "Insertion failed...";
            }
        }

        public int save_file_toFiles()
        {

            int result = 0;
            Byte[] bytes = new byte[flileupload.PostedFile.ContentLength];
            flileupload.PostedFile.InputStream.Read(bytes, 0, flileupload.PostedFile.ContentLength);

            try
            {
                ParameterList.AddParameter.Clear();

                stParameterDetails.Value = bytes;
                stParameterDetails.DataType = SqlDbType.Image;
                ParameterList.AddParameter.Add("File_Data", stParameterDetails);

                stParameterDetails.Value = flileupload.PostedFile.FileName;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("File_Name", stParameterDetails);

                stParameterDetails.Value = flileupload.PostedFile.ContentType;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("File_Type", stParameterDetails);

                stParameterDetails.Value = "From DEO FOR CASUAL VISITOR";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Remark", stParameterDetails);

                ds.Reset();
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.Insert_filesData", ParameterList.AddParameter);
                    string fileID = ds.Tables[0].Rows[0][0].ToString();
                    result = Convert.ToInt32(fileID);
                }

            }
            catch (Exception ee)
            {

                result = 0;
            }


            return result;
        }

        public void add_gadget_and_vahical(string id, string officer_tran_id)
        {
            DataSet GADGET_ds = get_data_from_DB("Select * from TEMP_GV where TEMP_V_ID='" + id + "' and Type='GADGET'");
            DataSet VEHICLE_ds = get_data_from_DB("Select * from TEMP_GV where TEMP_V_ID='" + id + "' and Type='VEHICLE'");
            try
            {


                foreach (DataRow row in GADGET_ds.Tables[0].Rows)
                {
                    save_data_db("insert into Gadget_Details(GadgetName,Gadget_SerialNo,Purpose,Ofcr_Tran_Id) values('" + row["NAME"] + "','" + row["NUMBER"] + "','" + row["PURPOSE"] + "','" + officer_tran_id + "')");
                }
            }
            catch (Exception ee)
            {


            }
            try
            {
                foreach (DataRow row in VEHICLE_ds.Tables[0].Rows)
                {
                    save_data_db("insert into VehicleDetails(VehicleName,VehicleNo,Purpose,Ofcr_Tran_Id) values('" + row["NAME"] + "','" + row["NUMBER"] + "','" + row["PURPOSE"] + "','" + officer_tran_id + "')");
                }

            }
            catch (Exception)
            {


            }

            delete_from_TEMP_GV(id);


        }

        public void get_gadget_vahical_list()
        {
            get_gadget_list(Temp_VID);
            get_vahical_list(Temp_VID);
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void btn_delete_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow grdrow = btn.NamingContainer as GridViewRow;
            HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
            string entry_id = hdn.Value;

            delete_from_TempLabourList(entry_id);

            delete_from_TEMP_GV(entry_id);

            get_data_from_TempCasualVisitor();
        }

        public void delete_from_TEMP_GV(string entry_id)
        {
            save_data_db("Delete from TEMP_GV where TEMP_V_ID='" + entry_id + "'");
        }

        public void delete_from_TempLabourList(string entry_id)
        {
            ParameterList.AddParameter.Clear();

            stParameterDetails.Value = entry_id;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ID", stParameterDetails);

            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                db.CommonCollection.GetAsDataSet("dbo.TempCasualVisitor_DATA_Delete", ParameterList.AddParameter);
            }
            //Lblsuccess.Text = "data inserted successfully...";
        }

        public DataTable GetDynamicdata()
        {
            DataSet ds = get_data_from_TempCasualVisitor();
            return ds.Tables[0];
        }

        public void ClearData()
        {
            GetDynamicdata().Clear();
            TxtEscortname.Text = "";
            //TxtEscorticard.Text = "";
            TxtEscortdept.Text = "";
            TxtEscortrank.Text = "";
            TxtEscorttoken.Text = "";
            TxtOfcrname.Text = "";
            txtdeptship.Text = "";
            Txtofcrdesignation.Text = "";
            txtofftoken.Text = "";
            //TxtOfcrname.Text = "";
        }

        public bool Validation()
        {
            try
            {
                if (txtdeptship.Text == "")
                {
                    Lblsuccess.Text = "Please Enter The Department/Ship";
                    txtdeptship.Focus();
                    return false;
                }
                if (Txtofcrdesignation.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Officer Rank";
                    Txtofcrdesignation.Focus();
                    return false;
                }
                if (TxtOfcrname.Text == "")
                {
                    Lblsuccess.Text = "Please Enter The Officer Name";
                    TxtOfcrname.Focus();
                    return false;
                }
                if (txtofftoken.Text == "")
                {
                    Lblsuccess.Text = "Please Enter The Officer T/P No";
                    txtofftoken.Focus();
                    return false;
                }

                if (TxtEscorttoken.Text == "")
                {
                    Lblsuccess.Text = "Please Enter The Escort T/P No";
                    TxtEscorttoken.Focus();
                    return false;
                }
                if (TxtEscortname.Text == "")
                {
                    Lblsuccess.Text = "Please Enter The Escort Name";
                    TxtEscortname.Focus();
                    return false;
                }
                if (TxtEscortrank.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Escort Rank";
                    TxtEscortrank.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        private bool AddValidation()
        {
            lblcount.Text = "";
            try
            {
                GridViewRow row = commander_apr.FooterRow;
                TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
                TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[3].FindControl("temp_lbl_name_desig");
                TextBox temp_lbl_age_obj = (TextBox)row.Cells[4].FindControl("temp_lbl_age");
                DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[5].FindControl("ddlgen");
                TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[6].FindControl("temp_lbl_CONTACT_NO");
                DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[7].FindControl("ddlnation");
                DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
                TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
                TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[10].FindControl("temp_lbl_FIRM_NAME");
                TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
                TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[2].FindControl("txt_PURPOSE_OF_VISIT");
                DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("DDLDURATION");
                DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[2].FindControl("drpEmptype");
                temp_lblFirm_Tin_obj.Text = "NA";
                if (txt_pass_no_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Dockyard ID Number";
                    txt_pass_no_obj.Focus();
                    // txt_pass_no_obj.Style.Add("border-color", "Red");
                    return false;
                }

                if (temp_lbl_name_desig_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Designation";
                    temp_lbl_name_desig_obj.Focus();
                    return false;
                }

                if (temp_lbl_age_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Age";
                    temp_lbl_age_obj.Focus();
                    return false;
                }

                if (temp_lbl_Gender_obj.SelectedItem.Value == "-1")
                {
                    Lblsuccess.Text = "Please Select Gender";
                    temp_lbl_Gender_obj.Focus();
                    return false;
                }

                if (temp_lbl_CONTACT_NO_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Mobile Number";
                    temp_lbl_CONTACT_NO_obj.Focus();
                    return false;
                }

                if (temp_lbl_NATIONALITY_obj.SelectedItem.Value == "-1")
                {
                    Lblsuccess.Text = "Please Select Nationality";
                    temp_lbl_NATIONALITY_obj.Focus();
                    return false;
                }

                if (temp_drpIDType_obj.SelectedItem.Value == "-1")
                {
                    Lblsuccess.Text = "Please Select Any Government ID Type";
                    temp_drpIDType_obj.Focus();
                    return false;
                }

                if (temp_TxtIdNo_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Government ID Number";
                    temp_TxtIdNo_obj.Focus();
                    return false;
                }

                if (temp_lbl_FIRM_NAME_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Firm Name";
                    temp_lbl_FIRM_NAME_obj.Focus();
                    return false;
                }

                if (temp_lblFirm_Tin_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Firm Tin Number";
                    temp_lblFirm_Tin_obj.Focus();
                    return false;
                }

                if (txt_PURPOSE_OF_VISIT_obj.Text == "")
                {
                    Lblsuccess.Text = "Please Enter Purpose of Visit";
                    txt_PURPOSE_OF_VISIT_obj.Focus();
                    return false;
                }

                if (lbl_DURATION_OBJ.SelectedItem.Value == "-1")
                {
                    Lblsuccess.Text = "Please Enter Visit Duration";
                    lbl_DURATION_OBJ.Focus();
                    return false;
                }

                if (chk_EMP_Type_obj.SelectedItem.Text == "--SELECT--")
                {
                    Lblsuccess.Text = "Please Select Visitor Type";
                    chk_EMP_Type_obj.Focus();
                    return false;
                }
            }
            catch (Exception)
            {

            }
            return true;
        }

        protected void OnCheckedChanged_chktin(object sender, EventArgs e)
        {
            //GridViewRow row = commander_apr.FooterRow;
            //TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
            //TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[10].FindControl("temp_lbl_FIRM_NAME");
            //TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
            //CheckBox chktin = (CheckBox)sender;
            //if (chktin.Checked == true)
            //{
            //    if (temp_lblFirm_Tin_obj.Text == "")
            //    {
            //        temp_lblFirm_Tin_obj.Text = "NA";
            //        temp_lblFirm_Tin_obj.Enabled = false;
            //        temp_lbl_FIRM_NAME_obj.Enabled = false;

            //        if (txt_pass_no_obj.Text.Contains("NEW") || txt_pass_no_obj.Text == "")
            //        {
            //            //chktin.Visible = true;


            //            temp_lblFirm_Tin_obj.Text = "NA";
            //            temp_lblFirm_Tin_obj.Enabled = false;
            //            temp_lbl_FIRM_NAME_obj.Text = "";
            //            temp_lbl_FIRM_NAME_obj.Enabled = true;
            //        }
            //    }
            //}
            //else
            //{
            //    //chktin.Visible = false;
            //    temp_lblFirm_Tin_obj.Text = "";
            //    temp_lblFirm_Tin_obj.Enabled = true;
            //    temp_lbl_FIRM_NAME_obj.Text = "";
            //    temp_lbl_FIRM_NAME_obj.Enabled = true;
            //}
        }

        protected void chkadd_OnCheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = commander_apr.FooterRow;
            CheckBox chkaddNew = (CheckBox)commander_apr.FooterRow.FindControl("chkadd");
            TextBox temp_lbl_name_desig_obj = (TextBox)row.Cells[3].FindControl("temp_lbl_name_desig");
            TextBox temp_lbl_age_obj = (TextBox)row.Cells[4].FindControl("temp_lbl_age");
            DropDownList temp_lbl_Gender_obj = (DropDownList)row.Cells[5].FindControl("ddlgen");
            TextBox temp_lbl_CONTACT_NO_obj = (TextBox)row.Cells[6].FindControl("temp_lbl_CONTACT_NO");
            DropDownList temp_lbl_NATIONALITY_obj = (DropDownList)row.Cells[7].FindControl("ddlnation");
            DropDownList temp_drpIDType_obj = (DropDownList)row.Cells[8].FindControl("drpIDType");//
            TextBox temp_TxtIdNo_obj = (TextBox)row.Cells[9].FindControl("TxtIdNo");//
            TextBox temp_lbl_FIRM_NAME_obj = (TextBox)row.Cells[10].FindControl("temp_lbl_FIRM_NAME");
            TextBox temp_lblFirm_Tin_obj = (TextBox)row.Cells[2].FindControl("temp_lblFirm_Tin");
            TextBox txt_PURPOSE_OF_VISIT_obj = (TextBox)row.Cells[2].FindControl("txt_PURPOSE_OF_VISIT");
            DropDownList lbl_DURATION_OBJ = (DropDownList)row.Cells[2].FindControl("DDLDURATION");
            DropDownList chk_EMP_Type_obj = (DropDownList)row.Cells[2].FindControl("drpEmptype");

            CheckBox chktin = (CheckBox)row.Cells[2].FindControl("chktin");
            if (chkaddNew.Checked == true)
            {
                TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");
                txt_pass_no_obj.Enabled = false;
                chktin.Enabled = true;
                //int num = 1;
                foreach (GridViewRow row1 in commander_apr.Rows)
                {
                    Label lbl_pass_no_obj = (Label)row1.Cells[1].FindControl("lbl_pass_no");
                    // if (lbl_pass_no_obj.Text.Contains("NEW VISITOR"))
                    //{
                    // num++;
                    // }
                    txt_pass_no_obj.Text = "NEW FOREIGN VISITOR";
                    temp_lbl_name_desig_obj.Text = "";
                    temp_lbl_age_obj.Text = "";
                    temp_lbl_Gender_obj.ClearSelection();
                    temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                    temp_lbl_CONTACT_NO_obj.Text = "";
                    //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "INDIAN";
                    //temp_drpIDType_obj.SelectedItem.Value = "-1";
                    temp_drpIDType_obj.ClearSelection();
                    temp_drpIDType_obj.Items.FindByValue("3").Selected = true;
                    temp_TxtIdNo_obj.Text = "";
                    temp_lbl_FIRM_NAME_obj.Text = "";
                    temp_lblFirm_Tin_obj.Text = "";
                    chktin.Checked = false;
                    txt_PURPOSE_OF_VISIT_obj.Text = "";
                    //lbl_DURATION_OBJ.SelectedItem.Value = "-1";
                    chk_EMP_Type_obj.SelectedItem.Value = "-1";
                    lblcount.Text = "";
                    temp_lbl_name_desig_obj.Enabled = true;
                    temp_lbl_age_obj.Enabled = true;
                    temp_lbl_Gender_obj.Enabled = true;
                    //btn_add_OBJ.Enabled = true;
                    temp_lblFirm_Tin_obj.Enabled = true;
                }
            }
            else
            {
                TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");

                txt_pass_no_obj.Enabled = true;
                txt_pass_no_obj.Text = "";
                chktin.Enabled = false;
                temp_lbl_name_desig_obj.Text = "";
                temp_lbl_age_obj.Text = "";
                //temp_lbl_Gender_obj.SelectedItem.Value = "-1";
                temp_drpIDType_obj.ClearSelection();
                temp_drpIDType_obj.Items.FindByValue("3").Selected = true;
                temp_lbl_CONTACT_NO_obj.Text = "";
                //temp_lbl_NATIONALITY_obj.SelectedItem.Value = "INDIAN";
                temp_drpIDType_obj.SelectedItem.Value = "3";
                temp_TxtIdNo_obj.Text = "";
                temp_lbl_FIRM_NAME_obj.Text = "";
                temp_lblFirm_Tin_obj.Text = "";
                txt_PURPOSE_OF_VISIT_obj.Text = "";
                //lbl_DURATION_OBJ.SelectedItem.Value = "-1";
                chk_EMP_Type_obj.SelectedItem.Value = "-1";
                lblcount.Text = "";
                chktin.Checked = false;
            }
        }

        static string Temp_VID;

        public void list_Vehiclegadget(object sender)
        {

            Button btn = sender as Button;
            GridViewRow grdrow = btn.NamingContainer as GridViewRow;
            HiddenField hdn = (HiddenField)grdrow.FindControl("hdn_datakey");
            string entry_id = Temp_VID = hdn.Value;
            get_gadget_list(entry_id);

            get_vahical_list(entry_id);
            ModalPopupExtender_Vehiclegadget.Show();
        }

        protected void drpIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = commander_apr.FooterRow;

            DropDownList drpIDType_obj = (DropDownList)commander_apr.FooterRow.FindControl("drpIDType");
            CheckBox chkaddNew = (CheckBox)commander_apr.FooterRow.FindControl("chkadd");

            TextBox TxtIdNo_obj = (TextBox)row.Cells[2].FindControl("TxtIdNo");
            TextBox txt_pass_no_obj = (TextBox)row.Cells[2].FindControl("txt_pass_no");

            string query = "";
            if (drpIDType_obj.Text == "AADHAR CARD")
            {
                query = "select AADHAR_CARD from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                TxtIdNo_obj.Attributes.Add("pattern", "[0-9]{12}");
                TxtIdNo_obj.MaxLength = 12;
            }
            else if (drpIDType_obj.Text == "PAN CARD")
            {
                query = "select PAN_CARD from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                TxtIdNo_obj.Attributes.Add("pattern", "[A-Z]{5}[0-9]{4}[A-Z]{1}");
                TxtIdNo_obj.MaxLength = 10;
            }
            else if (drpIDType_obj.Text == "PASSPORT")
            {
                query = "select PASSPORT from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                TxtIdNo_obj.Attributes.Remove("pattern");
                //TxtIdNo_obj.Attributes.Add("pattern", "[A-Z][A-Z0-9][0-9]");
                // txtidentityno.Attributes.Add("pattern", "^[A-PR-WYa-pr-wy][1-9]\\d\\s?\\d{4}[1-9]$");  //indian
                //^[A-Z]{1}-[0-9]{7}$ //india
                // txtidentityno.Attributes.Add("pattern", "^[A-Z0-9<]{9}[0-9]{1}[A-Z]{3}[0-9]{7}[A-Z]{1}[0-9]{7}[A-Z0-9<]{14}[0-9]{2}$"); //international
            }
            else if (drpIDType_obj.Text == "DRIVING LICENCE")
            {
                query = "select DRIVING_LICENCE from VisitorTransactionDetail where VisitorTranID='" + txt_pass_no_obj.Text + "'";
                TxtIdNo_obj.Attributes.Remove("pattern");
            }
            //txt_pass_no_obj
            if (txt_pass_no_obj.Text.Contains("NEW"))
            {
                TxtIdNo_obj.Text = "";
            }
            else
            {
                if (txt_pass_no_obj.Text == "")
                { }
                else
                {
                    if (drpIDType_obj.SelectedItem.Value != "-1")
                    {
                        TxtIdNo_obj.Text = get_data_from_DB(query).Tables[0].Rows[0][0].ToString();

                    }
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            TxtEscorttoken.Text = "";
            TxtEscortrank.Text = "";
            TxtEscortname.Text = "";
            //TxtEscortdept.Text = "";

            txtofftoken.Text = "";
            TxtOfcrname.Text = "";
            Txtofcrdesignation.Text = "";
            //txtdeptship.Text = "";
            if (roll_typeID == "2")
            {
                Response.Redirect("~/Operator_Home.aspx");
            }
            else
            {
                Response.Redirect("~/DM/DMHome.aspx");
            }
        }

        //object sender, EventArgs e
        public void Firm_Insert(TextBox temp_lbl_FIRM_NAME_obj, TextBox temp_lblFirm_Tin_obj)
        {
            try
            {
                ParameterList.AddParameter.Clear();
                DataTable dt1 = new DataTable();
                stParameterDetails.Value = temp_lbl_FIRM_NAME_obj.Text;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("V_Firm_Name", stParameterDetails);

                stParameterDetails.Value = temp_lblFirm_Tin_obj.Text;
                stParameterDetails.DataType = SqlDbType.NVarChar;
                ParameterList.AddParameter.Add("V_FIRM_TIN", stParameterDetails);

                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    db.CommonCollection.GetAsDataSet("dbo.FirmMaster_InsertFirmTin", ParameterList.AddParameter);
                }
            }
            catch (Exception ee)
            {
            }
        }

        protected void temp_lblFirm_Tin_textChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = commander_apr.FooterRow;
                TextBox temp_lblFirm_Tin = (TextBox)commander_apr.FooterRow.FindControl("temp_lblFirm_Tin");
                TextBox temp_lbl_FIRM_NAME = (TextBox)commander_apr.FooterRow.FindControl("temp_lbl_FIRM_NAME");
                CheckBox temp_chktin_obj = (CheckBox)row.Cells[2].FindControl("chktin");

                if (!string.IsNullOrEmpty(temp_lblFirm_Tin.Text))
                {

                    SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                    DataSet sds = new DataSet();
                    SqlCommand scmd = new SqlCommand();
                    SqlDataAdapter sadp = new SqlDataAdapter();
                    DataTable sdt = new DataTable();

                    string query = "SELECT Firm_Name FROM Firm_Master where Firm_Tin='" + temp_lblFirm_Tin.Text + "' order by Firm_ID DESC";


                    using (scmd = new SqlCommand(query, scon))
                    {
                        sadp = new SqlDataAdapter(scmd);
                        sadp.Fill(sds);
                        DataView view = new DataView();
                        view.Table = sds.Tables[0];
                        if (sds.Tables[0].Rows.Count > 0)
                        {
                            temp_lbl_FIRM_NAME.Enabled = false;
                            temp_lbl_FIRM_NAME.Text = sds.Tables[0].Rows[0]["Firm_Name"].ToString();
                            temp_chktin_obj.Enabled = false;

                        }
                        else
                        {
                            temp_lbl_FIRM_NAME.Enabled = true;
                            temp_lbl_FIRM_NAME.Text = "";
                            temp_chktin_obj.Enabled = true;
                        }
                    }
                }
                else
                {
                    temp_lbl_FIRM_NAME.Enabled = true;
                    temp_lbl_FIRM_NAME.Text = "";
                }
                //if (temp_lblFirm_Tin.Text == "AADHAR CARD")
                //{
                //    temp_lblFirm_Tin.Attributes.Add("pattern", "[0-9]{12}");
                //}
            }
            catch (Exception)
            {

            }
        }

        public DataSet get_data_from_DB(string query)
        {
            DataSet sds = new System.Data.DataSet();
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                SqlCommand scmd;
                SqlDataAdapter sadp;


                using (scmd = new SqlCommand(query, scon))
                {
                    sadp = new SqlDataAdapter(scmd);
                    sadp.Fill(sds);
                }
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

        public void PIMS()
        {
            try
            {
                DataSet getESC = get_data_from_DB("select * from EscortMaster where ES_TokenNo = '" + TxtEscorttoken.Text.Trim() + "'");
                if (getESC.Tables[0].Rows.Count > 0)
                {
                    TxtEscortname.Text = getESC.Tables[0].Rows[0]["ES_Name"].ToString();
                    TxtEscortrank.Text = getESC.Tables[0].Rows[0]["ES_Rank"].ToString();
                    txtESMobile.Text = getESC.Tables[0].Rows[0]["ES_MobNumber"].ToString();
                    //TxtEscortdept.Text = getESC.Tables[0].Rows[0]["ES_Dept"].ToString();
                    TxtEscortname.Enabled = false;
                    TxtEscortrank.Enabled = false;
                }
                //else
                //{
                //    OracleConnection ConPIMS = new OracleConnection(ConfigurationManager.ConnectionStrings["PIMS"].ConnectionString);
                //    string sql = "SELECT  (A.EMP_FNAME||' '||A.EMP_MNAME||' '||A.EMP_LNAME) AS USERNAME,A.EMP_TOKEN, A.EMP_ID,B.RANK_ABBR FROM  HRMS_EMP_OFFC A inner join  HRMS_RANK  B on A.EMP_RANK = B.RANK_ID WHERE EMP_TYPE IN(1,2,3)AND EMP_STATUS='S' AND A.EMP_TOKEN='" + TxtEscorttoken.Text + "'  ORDER BY EMP_TYPE";
                //    OracleCommand cmd = new OracleCommand();
                //    cmd.Connection = ConPIMS;
                //    cmd.CommandText = sql;
                //    cmd.CommandType = CommandType.Text;
                //    OracleDataAdapter da = new OracleDataAdapter(cmd);
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        //TxtEscorttoken.Text = ds.Tables[0].Rows[0]["EMP_TOKEN"].ToString();
                //        TxtEscortname.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                //        TxtEscortrank.Text = ds.Tables[0].Rows[0]["RANK_ABBR"].ToString();
                //        TxtEscortname.Enabled = false;
                //        TxtEscortrank.Enabled = false;
                //    }
                //    else
                //    {
                //        //TxtEscorttoken.Text = "";
                //        TxtEscortname.Text = "";
                //        TxtEscortrank.Text = "";
                //        TxtEscortname.Enabled = true;
                //        TxtEscortrank.Enabled = true;
                //    }
                //}
            }
            catch (Exception ee)
            {
            }
        }

        protected void TxtEscorttoken_TextChanged(object sender, EventArgs e)
        {
            PIMS();
        }

        protected void txtofftoken_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet getoff = get_data_from_DB("select * from OfficerMaster where Officer_Token = '" + txtofftoken.Text.Trim() + "'");
                if (getoff.Tables[0].Rows.Count > 0)
                {
                    TxtOfcrname.Text = getoff.Tables[0].Rows[0]["Officer_Name"].ToString();
                    Txtofcrdesignation.Text = getoff.Tables[0].Rows[0]["Officer_Designation"].ToString();
                    //txtdeptship.Text = getoff.Tables[0].Rows[0][""].ToString();
                    TxtOfcrname.Enabled = false;
                    Txtofcrdesignation.Enabled = false;
                }
                //else
                //{
                //    OracleConnection ConPIMS = new OracleConnection(ConfigurationManager.ConnectionStrings["SSO"].ConnectionString);
                //    string sql = "SELECT   O.EMP_ID,O.EMP_TOKEN,(o.EMP_FNAME|| ' '|| o.EMP_MNAME|| ' '|| o.EMP_LNAME)AS FULL_NAME,O.EMP_CENTER,D.DEPT_ABBR,H.RANK_ABBR " +
                //    "FROM   HRMS_EMP_OFFC@LNK_PIMS O," +
                //    "HRMS_CENTER@LNK_PIMS C," +
                //    "HRMS_DEPT@LNK_PIMS D," +
                //    "HRMS_DIVISION@LNK_PIMS DV," +
                //    "HRMS_GROUP@LNK_PIMS G," +
                //    "HRMS_RANK@LNK_PIMS H," +
                //    "USERS_DOMAIN s," +
                //    "HRMS_TITLE@LNK_PIMS T " +
                //    "WHERE " +
                //    "C.CENTER_ID = O.EMP_CENTER " +
                //    "AND D.DEPT_ID = C.CENTER_DEPT_ID " +
                //    "AND DV.DIV_ID = D.DEPT_DIV_CODE " +
                //    "AND G.GROUP_ID = DV.GROUP_ID " +
                //    "AND H.RANK_ID = O.EMP_RANK " +
                //    "AND s.EMP_ID = o.EMP_ID " +
                //    "AND o.EMP_TITLE_CODE = T.TITLE_CODE " +
                //    "AND o.EMP_TOKEN ='" + txtofftoken.Text + "'";
                //    OracleCommand cmd = new OracleCommand();
                //    cmd.Connection = ConPIMS;
                //    cmd.CommandText = sql;
                //    cmd.CommandType = CommandType.Text;
                //    OracleDataAdapter da = new OracleDataAdapter(cmd);
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        TxtOfcrname.Text = ds.Tables[0].Rows[0]["FULL_NAME"].ToString();
                //        Txtofcrdesignation.Text = ds.Tables[0].Rows[0]["RANK_ABBR"].ToString();
                //        TxtOfcrname.Enabled = false;
                //        Txtofcrdesignation.Enabled = false;
                //    }
                //    else
                //    {
                //        Txtofcrdesignation.Text = "";
                //        TxtOfcrname.Text = "";
                //        TxtOfcrname.Enabled = true;
                //        Txtofcrdesignation.Enabled = true;
                //    }
                //}
            }
            catch (Exception E)
            {
            }
        }

        public void SaveOfficerData()
        {
            ParameterList.AddParameter.Clear();

            stParameterDetails.Value = txtofftoken.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Officer_Token1", stParameterDetails);

            stParameterDetails.Value = TxtOfcrname.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Officer_Name1", stParameterDetails);

            stParameterDetails.Value = Txtofcrdesignation.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Officer_Designation1", stParameterDetails);

            stParameterDetails.Value = txtdeptship.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Officer_Dept", stParameterDetails);

            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                DataSet SaveData = db.CommonCollection.GetAsDataSet("dbo.InsertOfficer_Data", ParameterList.AddParameter);
            }
        }

        public void SaveEscortData()
        {
            ParameterList.AddParameter.Clear();

            stParameterDetails.Value = TxtEscortname.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ES_Name1", stParameterDetails);

            stParameterDetails.Value = TxtEscortrank.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ES_Rank1", stParameterDetails);

            stParameterDetails.Value = TxtEscorttoken.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ES_TokenNo1", stParameterDetails);

            stParameterDetails.Value = TxtEscortdept.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ES_Dept1", stParameterDetails);

            stParameterDetails.Value = txtESMobile.Text;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ES_MobNumber1", stParameterDetails);

            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                DataSet SaveData = db.CommonCollection.GetAsDataSet("dbo.InsertEscort_Data", ParameterList.AddParameter);
            }
        }
    }
}