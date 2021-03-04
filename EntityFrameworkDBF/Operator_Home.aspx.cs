using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EntityFrameworkDBF
{
    public partial class Operator_Home : System.Web.UI.Page
    {
        SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());

        DataSet sds = new DataSet();
        SqlCommand scmd = new SqlCommand();
        SqlDataAdapter sadp = new SqlDataAdapter();
        DataTable sdt = new DataTable();
        protected ParameterDetails stParameterDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            //  btnShowPopup.Visible = false;
            if (!IsPostBack)
            {
                DataSet ds = GetCasualVisitorByStatus("2");
                GvCasual.DataSource = ds;
                GvCasual.DataBind();

                DataSet ds1 = GetForeignVisitorByStatus("2");
                GvCasual.DataSource = ds1;
                GvCasual.DataBind();

                DataSet sd = GetExecutiveVisitorByStatus("2");
                GvExeVisitorList.DataSource = sd;
                GvExeVisitorList.DataBind();

                DataSet dd = GetRetiredVisitorByStatus("2");
                GvRetiredPrintList.DataSource = dd;
                GvRetiredPrintList.DataBind();

                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yy").Replace("-", " ").ToUpper();
                get_count();
            }
            try
            {
                Session["Dept"] = "DEO";
                Session["Cen_No"] = "DEO";
            }
            catch (Exception)
            {
                Response.Redirect("~/errorpage.aspx");
            }
        }

        public static SqlConnection con;

        public void get_count()
        {
            DataSet ds = new DataSet();
            ds = getdata(get_query("Casual"));
            btnCasualvisi.Text = Convert.ToString(ds.Tables[0].Rows.Count);

            //DataSet ds = new DataSet();
            ds = getdata(get_query("FOREIGN"));
            lnk_ForVisitorPass.Text = Convert.ToString(ds.Tables[0].Rows.Count);

            ds = getdata(get_query("Executive"));
            btnexce_visi.Text = Convert.ToString(ds.Tables[0].Rows.Count);

            ds = getdata(get_query("Retired"));
            btnretired_visi.Text = Convert.ToString(ds.Tables[0].Rows.Count);

            btn_labour.Text = "0";
            btn_store.Text = "0";
        }

        public string get_query(string emp_type)
        {
            return "select * from Officer_Transaction where Emp_Type='" + emp_type + "' and Print_status is NULL  and convert(date,V_Date)=convert(date,GETDATE()) and V_Status='2'";
        }

        public DataSet getdata(string query)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
            con.Open();
            SqlDataAdapter sadp = new SqlDataAdapter(query, con);
            DataSet sds = new DataSet();
            sadp.Fill(sds);
            return sds;
        }

        public DataSet GetCasualVisitorByStatus(string Status)
        {
            string query = "select Center_No,ES_Dept,1 as GroupID,count(*) as Total,0 as Ofcr_Tran_Id from Officer_Transaction where V_Status=" + Status + " and Emp_Type='Casual' and  V_Date=convert(date,GETDATE()) and Print_Status is NULL group by Center_No,ES_Dept";
            using (scmd = new SqlCommand(query, scon))
            {
                sadp = new SqlDataAdapter(scmd);
                sadp.Fill(sds);
            }
            return sds;
        }

        public DataSet GetForeignVisitorByStatus(string Status)
        {
            string query = "select Center_No,ES_Dept,1 as GroupID,count(*) as Total,0 as Ofcr_Tran_Id from Officer_Transaction where V_Status=" + Status + " and Emp_Type='FOREIGN' and  V_Date=convert(date,GETDATE()) and Print_Status is NULL group by Center_No,ES_Dept";
            using (scmd = new SqlCommand(query, scon))
            {
                sadp = new SqlDataAdapter(scmd);
                sadp.Fill(sds);
            }
            return sds;
        }

        public DataSet GetExecutiveVisitorByStatus(string Status)
        {
            string query = "select Center_No,ES_Dept,count(*) as Total,GroupID,Ofcr_Tran_Id from Officer_Transaction where V_Status=" + Status + " and Emp_Type='Executive'and  V_Date=convert(date,GETDATE()) and Print_Status is NULL group by Center_No,ES_Dept,GroupID,Ofcr_Tran_Id";
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            using (cmd = new SqlCommand(query, scon))
            {
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet GetRetiredVisitorByStatus(string Status)
        {
            string query = "select Center_No,ES_Dept,count(*) as Total,GroupID,Ofcr_Tran_Id from Officer_Transaction where V_Status=" + Status + " and Emp_Type='Retired' and V_Date=convert(date,GETDATE()) and Print_Status is NULL group by Center_No,ES_Dept,GroupID,Ofcr_Tran_Id";
            DataSet sd = new DataSet();
            SqlCommand cmdd = new SqlCommand();
            SqlDataAdapter ad = new SqlDataAdapter();
            DataTable dta = new DataTable();

            using (cmdd = new SqlCommand(query, scon))
            {
                ad = new SqlDataAdapter(cmdd);
                ad.Fill(sd);
            }
            return sd;
        }

        protected void imExegprint_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgprint = sender as ImageButton;
            GridViewRow row = imgprint.NamingContainer as GridViewRow;
            string CenterNo = GvExeVisitorList.DataKeys[row.RowIndex].Values[0].ToString();
            // string status = "";   

            HiddenField hdn_groupID = (HiddenField)row.FindControl("hdn_group_id");
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");

            string passlist = "";
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "select V_name,V_PassNo,V_Sex,V_ContactNo,V_Date,V_Purpose,Ofcr_Tran_Id from Officer_Transaction where V_Status='2' and Center_No='" + CenterNo + "'and Emp_Type='Executive'  and V_Date=convert(date,GETDATE()) and GroupID='" + hdn_groupID.Value + "'";
                using (cmd = new SqlCommand(query, scon))
                {
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        passlist = passlist + dr[1].ToString() + "@" + dr[6].ToString() + ",";
                    }
                    Response.Redirect("~/visitorpass.aspx?tranID=" + hdn_Ofcr_Tran_Id_obj.Value + "&CNO=" + CenterNo + "&passlist=" + passlist);
                    // Response.Redirect("~/visitorpass.aspx?passlist=" + passlist);
                }
            }
            catch
            {

            }
        }

        protected void imgprint_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgprint = sender as ImageButton;
            GridViewRow row = imgprint.NamingContainer as GridViewRow;
            string CenterNo = GvCasual.DataKeys[row.RowIndex].Values[0].ToString();
            HiddenField hdn_groupID = (HiddenField)row.FindControl("hdn_group_id");
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
            // string status = "";   
            string passlist = "";
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "select V_name,V_PassNo,V_Sex,V_ContactNo,V_Date,V_Purpose,Ofcr_Tran_Id from Officer_Transaction where V_Status='2' and Center_No='" + CenterNo + "' and Emp_Type='Casual' and V_Date=convert(date,GETDATE()) and GroupID='" + hdn_groupID.Value + "'";
                using (cmd = new SqlCommand(query, scon))
                {
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        passlist = passlist + dr[1].ToString() + "@" + dr[6].ToString() + ",";
                    }
                    Response.Redirect("~/visitorpass.aspx?tranID=" + hdn_Ofcr_Tran_Id_obj.Value + "&CNO=" + CenterNo + "&passlist=" + passlist);
                }
            }
            catch
            {

            }
        }

        protected void imRetigprint_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgprint = sender as ImageButton;
            GridViewRow row = imgprint.NamingContainer as GridViewRow;
            string CenterNo = GvRetiredPrintList.DataKeys[row.RowIndex].Values[0].ToString();
            HiddenField hdn_groupID = (HiddenField)row.FindControl("hdn_group_id");
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
            // string status = "";   

            string passlist = "";
            try
            {
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                string query = "select V_name,V_PassNo,V_Sex,V_ContactNo,V_Date,V_Purpose,Ofcr_Tran_Id from Officer_Transaction where V_Status='2' and Center_No='" + CenterNo + "' and Emp_Type='Retired' and V_Date=convert(date,GETDATE()) and GroupID='" + hdn_groupID.Value + "'";
                using (cmd = new SqlCommand(query, scon))
                {
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        passlist = passlist + dr[1].ToString() + "@" + dr[6].ToString() + ",";
                    }
                    Response.Redirect("~/visitorpass.aspx?tranID=" + hdn_Ofcr_Tran_Id_obj.Value + "&CNO=" + CenterNo + "&passlist=" + passlist);
                }

            }
            catch (Exception ee)
            {

            }
        }

        protected void btnCasualvisi_Click(object sender, EventArgs e)
        {
            get_casual_list();
            Session["VisitorType"] = 1;

        }

        protected void lnk_ForVisitorPass_Click(object sender, EventArgs e)
        {
            get_Foreign_list();
            Session["VisitorType"] = 2;

        }

        public void get_casual_list()
        {
            DataSet ds = getdata(get_query("Casual"));
            Gv_EMP_LIST_TOPRINT.DataSource = ds;
            Gv_EMP_LIST_TOPRINT.DataBind();
            ModalPopupExtender1.Show();
        }

        public void get_Foreign_list()
        {
            DataSet ds = getdata(get_query("FOREIGN"));
            Gv_EMP_LIST_TOPRINT.DataSource = ds;
            Gv_EMP_LIST_TOPRINT.DataBind();

            ModalPopupExtender1.Show();

        }

        protected void btnexce_visi_Click(object sender, EventArgs e)
        {
            DataSet ds = getdata(get_query("Executive"));

            Gv_EMP_LIST_TOPRINT.DataSource = ds;
            Gv_EMP_LIST_TOPRINT.DataBind();
            ModalPopupExtender1.Show();
        }

        protected void btnretired_visi_Click(object sender, EventArgs e)
        {
            DataSet ds = getdata(get_query("Retired"));
            Gv_EMP_LIST_TOPRINT.DataSource = ds;
            Gv_EMP_LIST_TOPRINT.DataBind();
            ModalPopupExtender1.Show();
        }

        protected void btn_store_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btn_labour_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btnCancel_click(object sender, EventArgs e)
        {

            ModalPopupExtender1.Hide();
        }

        protected void Gv_EMP_LIST_TOPRINT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string status = "";
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Table tbl_ashish_obj = (Table)e.Row.FindControl("tbl_ashish");
                    Button btn_print_obj = (Button)e.Row.FindControl("btn_print");
                    Button btn_new_print_obj = (Button)e.Row.FindControl("btn_new_print");
                    if (e.Row.Cells[1].Text.ToUpper().Contains("NEW"))
                    {
                        btn_new_print_obj.Visible = true;
                        btn_print_obj.Visible = false;

                        tbl_ashish_obj.Visible = false;
                    }
                    else
                    {
                        btn_new_print_obj.Visible = false;
                        btn_print_obj.Visible = true;

                        tbl_ashish_obj.Visible = true;
                    }
                }
            }
            catch (Exception ee)
            {

            }
        }

        //protected void btn_new_print_clicked(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;
        //    HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
        //    TextBox txt_card_no_obj = new TextBox(); //(TextBox)row.FindControl("txt_card_no");
        //    txt_card_no_obj.Text = "NIL";
        //    string card_no = txt_card_no_obj.Text;
        //    DataSet ds = getdata("SELECT * FROM Officer_Transaction where Ofcr_Tran_Id='" + hdn_Ofcr_Tran_Id_obj.Value + "'");

        //    ParameterList.AddParameter.Clear();

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_name"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("VisitorName", stParameterDetails);

        //    stParameterDetails.Value = "NIL";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("CardNumber", stParameterDetails);

        //    DataSet Auto = getdata("select  ISNULL(max(VisitorTranID)+1,1) VisitorTranID from [dbo].[VisitorTransactionDetail]");
        //    stParameterDetails.Value = Auto.Tables[0].Rows[0]["VisitorTranID"];
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

        //    stParameterDetails.Value = "";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Designation", stParameterDetails);

        //    stParameterDetails.Value = "";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("VehicleNumber", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_FIRM_TIN"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Firm_Tin_No", stParameterDetails);


        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Firm"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Organization", stParameterDetails);

        //    // Modified on 17-03-2017
        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ContactNo"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_MobNumber"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["DURATION"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("DURATION", stParameterDetails);
        //    //End Modification
        //    stParameterDetails.Value = "Daily";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("PassType", stParameterDetails);

        //    stParameterDetails.Value = "";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Address", stParameterDetails);

        //    DateTime curr_date = Convert.ToDateTime(getdata("select GETDATE()").Tables[0].Rows[0][0]);
        //    stParameterDetails.Value = curr_date;
        //    stParameterDetails.DataType = SqlDbType.DateTime;
        //    ParameterList.AddParameter.Add("FromDate", stParameterDetails);

        //    stParameterDetails.Value = curr_date;
        //    stParameterDetails.DataType = SqlDbType.DateTime;
        //    ParameterList.AddParameter.Add("ToDate", stParameterDetails);

        //    string currtime = getdata("select CONVERT(varchar,getdate(),108)").Tables[0].Rows[0][0].ToString();
        //    stParameterDetails.Value = currtime;
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("TimeIn", stParameterDetails);

        //    stParameterDetails.Value = DBNull.Value;
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("VisitiorPhoto", stParameterDetails);

        //    string Offi_Name = ds.Tables[0].Rows[0]["Officer_Designation"].ToString() + " " + ds.Tables[0].Rows[0]["Officer_Name"].ToString();
        //    stParameterDetails.Value = Offi_Name;
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("HostID", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Dept"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("DepartmentId", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_TokenNo"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("EmployeeCode", stParameterDetails);

        //    string ES_Name = ds.Tables[0].Rows[0]["ES_Name"].ToString() + " ," + ds.Tables[0].Rows[0]["ES_Rank"].ToString();
        //    stParameterDetails.Value = ES_Name;
        //    //stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Name"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("EmployeeName", stParameterDetails);

        //    stParameterDetails.Value = Session["UserID"];
        //    stParameterDetails.DataType = SqlDbType.Int;
        //    ParameterList.AddParameter.Add("UserId", stParameterDetails);

        //    stParameterDetails.Value = curr_date;
        //    stParameterDetails.DataType = SqlDbType.DateTime;
        //    ParameterList.AddParameter.Add("UserCreateDate", stParameterDetails);

        //    stParameterDetails.Value = "1";
        //    stParameterDetails.DataType = SqlDbType.Int;
        //    ParameterList.AddParameter.Add("numberOfPerson", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Nationality"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Nationality", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("ID_Type", stParameterDetails);

        //    if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "AADHAR CARD")
        //    {
        //        stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
        //    }
        //    else
        //    {
        //        stParameterDetails.Value = "";
        //    }
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("AADHAR_CARD", stParameterDetails);

        //    if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PAN CARD")
        //    {
        //        stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
        //    }
        //    else
        //    {
        //        stParameterDetails.Value = "";
        //    }
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("PAN_CARD", stParameterDetails);

        //    if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PASSPORT")
        //    {
        //        stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
        //    }
        //    else
        //    {
        //        stParameterDetails.Value = "";
        //    }

        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("PASSPORT", stParameterDetails);

        //    if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "DRIVING LICENCE")
        //    {
        //        stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
        //    }
        //    else
        //    {
        //        stParameterDetails.Value = "";
        //    }
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("DRIVING_LICENCE", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("ID_No", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Age"].ToString();
        //    stParameterDetails.DataType = SqlDbType.Int;
        //    ParameterList.AddParameter.Add("Age", stParameterDetails);

        //    stParameterDetails.Value = DBNull.Value;
        //    stParameterDetails.DataType = SqlDbType.DateTime;
        //    ParameterList.AddParameter.Add("DOB", stParameterDetails);

        //    stParameterDetails.Value = "";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("File_Upload", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Sex"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("Sex", stParameterDetails);

        //    stParameterDetails.Value = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("EmpType", stParameterDetails);

        //    string emp_type = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
        //    if (emp_type.ToUpper() == "Casual".ToUpper())
        //    {
        //        stParameterDetails.Value = "~/Images/red-triangle.png";
        //        stParameterDetails.DataType = SqlDbType.VarChar;
        //        ParameterList.AddParameter.Add("Status", stParameterDetails);
        //    }
        //    else if (emp_type.ToUpper() == "Executive".ToUpper())
        //    {
        //        stParameterDetails.Value = "~/Images/green-square.png";
        //        stParameterDetails.DataType = SqlDbType.VarChar;
        //        ParameterList.AddParameter.Add("Status", stParameterDetails);
        //    }
        //    string tranID = "";
        //    DataSet ds1 = new DataSet();
        //    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //    {
        //        ds1 = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Insert", ParameterList.AddParameter);
        //        tranID = ds1.Tables[0].Rows[0][0].ToString();
        //    }

        //    VisitorLog(ds, curr_date, tranID);

        //    ParameterList.AddParameter.Clear();
        //    stParameterDetails.Value = tranID;
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

        //    stParameterDetails.Value = "ByID";
        //    stParameterDetails.DataType = SqlDbType.VarChar;
        //    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
        //    DataSet ds2 = new DataSet();
        //    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //    {
        //        ds2 = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Select_Print", ParameterList.AddParameter);
        //    }

        //    if (ds2.Tables[0].Rows.Count == 0)
        //    {
        //        // objDisplayLog.CustomMessage("No data " + txtVisitorName.Text.ToString().Trim(), this);
        //    }
        //    else
        //    {
        //        Session["LoadDetail"] = ds2.Tables[0];
        //        make_printed(hdn_Ofcr_Tran_Id_obj.Value);
        //        string url = "PrintPass_Visitor.aspx?TranID=" + ds2.Tables[0].Rows[0]["VisitorTranID"] + "&Off_tran_ID=" + hdn_Ofcr_Tran_Id_obj.Value;
        //        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //        //   get_casual_list();
        //    }
        //    get_seleted_center_calsuallist();

        //}

        protected void btn_new_print_clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
            HiddenField hdnPassType_obj = (HiddenField)row.FindControl("hdnPassType");
            TextBox txt_card_no_obj = new TextBox(); //(TextBox)row.FindControl("txt_card_no");
            txt_card_no_obj.Text = "NIL";
            string card_no = txt_card_no_obj.Text;
            DataSet ds = getdata("SELECT * FROM Officer_Transaction where Ofcr_Tran_Id='" + hdn_Ofcr_Tran_Id_obj.Value + "'");
            ParameterList.AddParameter.Clear();
            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_name"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VisitorName", stParameterDetails);

            stParameterDetails.Value = "NIL";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("CardNumber", stParameterDetails);

            DataSet Auto = getdata("select top 1 A.VisitorTranID+1 as VisitorTranID from VisitorTransactionDetail A where not exists (select * from VisitorTransactionDetail B where B.VisitorTranID = A.VisitorTranID+1)");
            stParameterDetails.Value = "";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

            // Modified on 17-03-2017
            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ContactNo"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_MobNumber"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["DURATION"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("DURATION", stParameterDetails);
            //End Modification

            stParameterDetails.Value = "";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Designation", stParameterDetails);

            stParameterDetails.Value = "";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VehicleNumber", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_FIRM_TIN"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Firm_Tin_No", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Firm"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Organization", stParameterDetails);

            //stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ContactNo"].ToString();
            //stParameterDetails.DataType = SqlDbType.VarChar;
            //ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);
            DateTime curr_date = Convert.ToDateTime(getdata("select convert(date,GETDATE(),103)").Tables[0].Rows[0][0]);
            if (hdnPassType_obj.Value == "FOREIGN")
            {
                stParameterDetails.Value = "FOREIGN";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PassType", stParameterDetails);

                curr_date = Convert.ToDateTime(getdata("select convert(date,GETDATE(),103)").Tables[0].Rows[0][0]);
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_FromTime"].ToString();
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("FromDate", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ToTime"].ToString();
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("ToDate", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["For_Visa"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_VISA", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["For_Security_CL"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_SECURITY_CL", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["For_CL_Letter_no"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_CL_LETTER_NO", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["For_CL_Validity"].ToString();
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("FOR_SEC_CL_VALIDTY", stParameterDetails);

                stParameterDetails.Value = "Y";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOREIGN_FLAG", stParameterDetails);
            }
            else
            {
                stParameterDetails.Value = "Daily";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PassType", stParameterDetails);

                stParameterDetails.Value = curr_date;
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("FromDate", stParameterDetails);

                stParameterDetails.Value = curr_date;
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("ToDate", stParameterDetails);

                stParameterDetails.Value = "NA";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_VISA", stParameterDetails);

                stParameterDetails.Value = "NA";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_SECURITY_CL", stParameterDetails);

                stParameterDetails.Value = "NA";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOR_CL_LETTER_NO", stParameterDetails);

                stParameterDetails.Value = DateTime.Now.ToString("dd/MM/yyyy");
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("FOR_SEC_CL_VALIDTY", stParameterDetails);

                stParameterDetails.Value = "N";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("FOREIGN_FLAG", stParameterDetails);
            }
            stParameterDetails.Value = "";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Address", stParameterDetails);

            string currtime = getdata("select CONVERT(varchar,getdate(),108)").Tables[0].Rows[0][0].ToString();
            stParameterDetails.Value = currtime;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("TimeIn", stParameterDetails);

            stParameterDetails.Value = DBNull.Value;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VisitiorPhoto", stParameterDetails);

            string Offi_Name = ds.Tables[0].Rows[0]["Officer_Designation"].ToString() + " " + ds.Tables[0].Rows[0]["Officer_Name"].ToString();
            stParameterDetails.Value = Offi_Name;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("HostID", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Dept"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("DepartmentId", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_TokenNo"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("EmployeeCode", stParameterDetails);

            string ES_Name = ds.Tables[0].Rows[0]["ES_Name"].ToString() + " ," + ds.Tables[0].Rows[0]["ES_Rank"].ToString();
            stParameterDetails.Value = ES_Name;
            //stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Name"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("EmployeeName", stParameterDetails);

            //stParameterDetails.Value = "";
            //stParameterDetails.DataType = SqlDbType.VarChar;
            //ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);

            stParameterDetails.Value = Session["UserID"];
            stParameterDetails.DataType = SqlDbType.Int;
            ParameterList.AddParameter.Add("UserId", stParameterDetails);

            stParameterDetails.Value = curr_date;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("UserCreateDate", stParameterDetails);

            stParameterDetails.Value = "1";
            stParameterDetails.DataType = SqlDbType.Int;
            ParameterList.AddParameter.Add("numberOfPerson", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Nationality"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Nationality", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ID_Type", stParameterDetails);

            //stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString();
            //stParameterDetails.DataType = SqlDbType.VarChar;
            //ParameterList.AddParameter.Add("ID_Type", stParameterDetails);

            if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "AADHAR CARD")
            {
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
            }
            else
            {
                stParameterDetails.Value = "";
            }
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("AADHAR_CARD", stParameterDetails);

            if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PAN CARD")
            {
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
            }
            else
            {
                stParameterDetails.Value = "";
            }
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("PAN_CARD", stParameterDetails);

            if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PASSPORT")
            {
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
            }
            else
            {
                stParameterDetails.Value = "";
            }

            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("PASSPORT", stParameterDetails);

            if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "DRIVING LICENCE")
            {
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
            }
            else
            {
                stParameterDetails.Value = "";
            }
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("DRIVING_LICENCE", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("ID_No", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Age"].ToString();
            stParameterDetails.DataType = SqlDbType.Int;
            ParameterList.AddParameter.Add("Age", stParameterDetails);

            stParameterDetails.Value = DBNull.Value;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("DOB", stParameterDetails);

            stParameterDetails.Value = "";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("File_Upload", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Sex"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("Sex", stParameterDetails);

            stParameterDetails.Value = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("EmpType", stParameterDetails);

            string emp_type = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
            if (emp_type.ToUpper() == "Casual".ToUpper() || emp_type.ToUpper() == "FOREIGN".ToUpper())
            {
                stParameterDetails.Value = "~/Images/red-triangle.png";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Status", stParameterDetails);
            }
            else if (emp_type.ToUpper() == "Executive".ToUpper())
            {
                stParameterDetails.Value = "~/Images/green-square.png";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Status", stParameterDetails);
            }
            string tranID = "";
            DataSet ds1 = new DataSet();
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                ds1 = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Insert", ParameterList.AddParameter);
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    tranID = ds1.Tables[0].Rows[0][0].ToString();

                    ParameterList.AddParameter.Clear();
                    stParameterDetails.Value = tranID;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "ByID";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
                    DataSet Update = new DataSet();
                    using (Project.Db.DbConnection update = new Project.Db.DbConnection())
                    {
                        Update = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_InsertLogUpdate", ParameterList.AddParameter);
                    }
                }
            }
            ParameterList.AddParameter.Clear();
            stParameterDetails.Value = tranID;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

            stParameterDetails.Value = "ByID";
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);
            DataSet ds2 = new DataSet();
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                //VisitorLogDetail_InsertLogUpdate
                ds2 = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Select_Print", ParameterList.AddParameter);
            }
            if (ds2.Tables[0].Rows.Count == 0)
            {
                // objDisplayLog.CustomMessage("No data " + txtVisitorName.Text.ToString().Trim(), this);
            }
            else
            {
                if (hdnPassType_obj.Value == "FOREIGN")
                {
                    Session["LoadDetail"] = ds2.Tables[0];
                    make_printed(hdn_Ofcr_Tran_Id_obj.Value);
                    string url = "PrintPass_ForeignVisitor.aspx?TranID=" + ds2.Tables[0].Rows[0]["VisitorTranID"] + "&Off_tran_ID=" + hdn_Ofcr_Tran_Id_obj.Value;
                    string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else
                {
                    Session["LoadDetail"] = ds2.Tables[0];
                    make_printed(hdn_Ofcr_Tran_Id_obj.Value);
                    string url = "PrintPass_Visitor.aspx?TranID=" + ds2.Tables[0].Rows[0]["VisitorTranID"] + "&Off_tran_ID=" + hdn_Ofcr_Tran_Id_obj.Value;
                    string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                //   get_casual_list();
                //VisitorLog(ds, curr_date);
            }

            get_seleted_center_calsuallist();
            get_count();
        }

        public void make_printed(string off_tran_id)
        {
            try
            {

                string query = "update Officer_Transaction set Print_status='Y' , Print_Date=GetDate() where Ofcr_Tran_Id='" + off_tran_id + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
            }
        }

        public void VisitorLog(DataSet ds, DateTime cur_date, string tranID)
        {
            try
            {
                ParameterList.AddParameter.Clear();

                stParameterDetails.Value = tranID;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_name"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("VisitorName", stParameterDetails);

                stParameterDetails.Value = "NIL";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("CardNumber", stParameterDetails);

                stParameterDetails.Value = "";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Designation", stParameterDetails);

                stParameterDetails.Value = "";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("VehicleNumber", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Firm"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Organization", stParameterDetails);

                //Modified On 17-03-2017
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ContactNo"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_MobNumber"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);
                //End Modification

                stParameterDetails.Value = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PassType", stParameterDetails);

                stParameterDetails.Value = "";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Address", stParameterDetails);

                stParameterDetails.Value = cur_date;
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("fromDate", stParameterDetails);

                stParameterDetails.Value = cur_date;
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("ToDate", stParameterDetails);

                string currtime = getdata("select CONVERT(varchar,getdate(),108)").Tables[0].Rows[0][0].ToString();
                stParameterDetails.Value = currtime;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("TimeIn", stParameterDetails);

                stParameterDetails.Value = DBNull.Value;
                stParameterDetails.DataType = SqlDbType.Image;
                ParameterList.AddParameter.Add("VisitiorPhoto", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["Officer_Name"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("HostID", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Dept"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("DepartmentId", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["Emp_Type"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeCode", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Name"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeName", stParameterDetails);

                //stParameterDetails.Value = "";
                //stParameterDetails.DataType = SqlDbType.VarChar;
                //ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);

                stParameterDetails.Value = Session["UserID"];
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("UserId", stParameterDetails);

                stParameterDetails.Value = cur_date;
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("UserCreateDate", stParameterDetails);

                stParameterDetails.Value = "1";
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("numberOfPerson", stParameterDetails);

                ds.Reset();
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_InsertLog", ParameterList.AddParameter);
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    // objDisplayLog.CustomMessage("Error While Loading the log Details" + txtVisitorName.Text, this);
                }
            }
            catch (Exception ex)
            {
                // objDisplayLog.CustomMessage("Error While Loading the log Details" + txtVisitorName.Text, this);
            }
        }

        protected void btn_view_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow grdrow = btn.NamingContainer as GridViewRow;
            string center_no = grdrow.Cells[0].Text;
            string department = grdrow.Cells[1].Text;
            HiddenField hdn_group_id_obj = (HiddenField)grdrow.FindControl("hdn_group_id");

            hdn_seleted_center.Value = center_no;
            hdn_seleted_deptName.Value = department;
            get_seleted_center_calsuallist();
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)grdrow.FindControl("hdn_Ofcr_Tran_Id");
        }

        public void get_seleted_center_calsuallist()
        {
            //string VisitorType = Session["VisitorType"].ToString();
            string center_no = hdn_seleted_center.Value;
            string DeptName = hdn_seleted_deptName.Value;
            string query;
            string Vtype = "";
            //if (VisitorType == "1")
            //{
            //    Vtype = "Casual";
            //}
            //else if (VisitorType == "2")
            //{
            //    Vtype = "FOREIGN";
            //}
            if (center_no == "")
            {
                query = "select * from Officer_Transaction where Emp_Type IN ('Casual','FOREIGN') and Print_status is NULL  and convert(date,V_Date)=convert(date,GETDATE()) and V_Status='2'";
            }
            else
            {
                query = "select * from Officer_Transaction where Emp_Type IN ('Casual','FOREIGN') and Print_status is NULL  and convert(date,V_Date)=convert(date,GETDATE()) and V_Status='2' and Center_No='" + center_no + "' and ES_Dept='" + DeptName + "'";
            }

            DataSet ds = getdata(query);
            Gv_EMP_LIST_TOPRINT.DataSource = ds;
            Gv_EMP_LIST_TOPRINT.DataBind();

            ModalPopupExtender1.Show();
        }

        protected void btn_print_old_clicked(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            //GridViewRow row = (GridViewRow)btn.NamingContainer;
            //HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
            //TextBox txt_card_no_obj = (TextBox)row.FindControl("txt_card_no");
            //saveandprint(hdn_Ofcr_Tran_Id_obj, txt_card_no_obj);
            ////      get_casual_list();
            //get_seleted_center_calsuallist();

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HiddenField hdn_Ofcr_Tran_Id_obj = (HiddenField)row.FindControl("hdn_Ofcr_Tran_Id");
            TextBox txt_card_no_obj = (TextBox)row.FindControl("txt_card_no");
            HiddenField hdnPassType_obj = (HiddenField)row.FindControl("hdnPassType");
            //if (txt_card_no_obj.Text == "NIL")
            //{
            //    saveandprint(hdn_Ofcr_Tran_Id_obj, txt_card_no_obj);
            //}
            //else
            //{
            //    DataSet card = getdata("select * from VisitorLogDetail where CardNumber='" + txt_card_no_obj.Text + "' and convert(date,UserCreateDate,103) =convert(date,getdate(),103)");
            //    if (card.Tables[0].Rows.Count == 0)
            //    {
            //        saveandprint(hdn_Ofcr_Tran_Id_obj, txt_card_no_obj);
            //    }
            //    else
            //    {
            //        Response.Write("<Script>alert('Card Number Already Given to Visitor.')</Script>");
            //    }
            //}
            saveandprint(hdn_Ofcr_Tran_Id_obj, txt_card_no_obj, hdnPassType_obj);
            get_seleted_center_calsuallist();
            //      get_casual_list();
        }

        public void saveandprint(HiddenField hdn_Ofcr_Tran_Id_obj, TextBox txt_card_no_obj, HiddenField hdnPassType_obj)
        {
            string card_no = txt_card_no_obj.Text;
            DataSet ds = getdata("SELECT * FROM Officer_Transaction where Ofcr_Tran_Id='" + hdn_Ofcr_Tran_Id_obj.Value + "'");
            string visitorID = "";
            if (txt_card_no_obj.Text != "")
            {
                Session["FromDate"] = ds.Tables[0].Rows[0]["V_FromTime"];
                Session["ToDate"] = ds.Tables[0].Rows[0]["V_ToTime"];
                Session["Date"] = ds.Tables[0].Rows[0]["V_Date"];
                Session["Visa"] = ds.Tables[0].Rows[0]["For_Visa"].ToString();
                Session["SecCl"] = ds.Tables[0].Rows[0]["For_Security_CL"].ToString();
                Session["SecLetterNo"] = ds.Tables[0].Rows[0]["For_CL_Letter_no"].ToString();
                Session["SecCLValidity"] = ds.Tables[0].Rows[0]["For_CL_Validity"].ToString();
                ParameterList.AddParameter.Clear();
                visitorID = ds.Tables[0].Rows[0]["V_PassNo"].ToString();
                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_PassNo"];
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("VisitorTranID", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_name"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("VisitorName", stParameterDetails);

                //stParameterDetails.Value = ds.Tables[0].Rows[0]["V_FIRM_TIN"];
                //stParameterDetails.DataType = SqlDbType.VarChar;
                //ParameterList.AddParameter.Add("Firm_Tin_No", stParameterDetails);

                stParameterDetails.Value = txt_card_no_obj.Text;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("CardNumber", stParameterDetails);

                stParameterDetails.Value = "";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Designation", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Firm"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Organization", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ContactNo"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

                //EScort
                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_MobNumber"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeMobile", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["DURATION"].ToString();
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("DURATION", stParameterDetails);
                // End Modification 

                string Offi_Name = ds.Tables[0].Rows[0]["Officer_Designation"].ToString() + " " + ds.Tables[0].Rows[0]["Officer_Name"].ToString();
                stParameterDetails.Value = Offi_Name;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("HostID", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_Dept"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("DepartmentId", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["ES_TokenNo"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeCode", stParameterDetails);

                string EscortName = ds.Tables[0].Rows[0]["ES_Name"].ToString() + " ," + ds.Tables[0].Rows[0]["ES_Rank"].ToString();
                stParameterDetails.Value = EscortName;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("EmployeeName", stParameterDetails);

                stParameterDetails.Value = Session["UserID"];
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("UserId", stParameterDetails);

                stParameterDetails.Value = hdn_Ofcr_Tran_Id_obj.Value;
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("Ofcr_Tran_Id", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Nationality"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Nationality", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("ID_No", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_TYPE"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("ID_Type", stParameterDetails);

                if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "AADHAR CARD")
                {
                    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
                }
                else
                {
                    stParameterDetails.Value = "";
                }
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("AADHAR_CARD", stParameterDetails);

                if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PAN CARD")
                {
                    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
                }
                else
                {
                    stParameterDetails.Value = "";
                }
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PAN_CARD", stParameterDetails);

                if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "PASSPORT")
                {
                    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
                }
                else
                {
                    stParameterDetails.Value = "";
                }

                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PASSPORT", stParameterDetails);

                if (ds.Tables[0].Rows[0]["V_ID_TYPE"].ToString() == "DRIVING LICENCE")
                {
                    stParameterDetails.Value = ds.Tables[0].Rows[0]["V_ID_NO"].ToString();
                }
                else
                {
                    stParameterDetails.Value = "";
                }
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("DRIVING_LICENCE", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Age"];
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("Age", stParameterDetails);

                stParameterDetails.Value = ds.Tables[0].Rows[0]["V_Sex"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Sex", stParameterDetails);
                ds.Reset();

                DataSet ds1 = getdata("SELECT * FROM VisitorTransactionDetail where VisitorTranID='" + visitorID + "'");
                stParameterDetails.Value = ds1.Tables[0].Rows[0]["PassType"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("PassType", stParameterDetails);

                stParameterDetails.Value = ds1.Tables[0].Rows[0]["Address"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("Address", stParameterDetails);

                string currtime = getdata("select CONVERT(varchar,getdate(),108)").Tables[0].Rows[0][0].ToString();
                stParameterDetails.Value = currtime;//DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("TimeIn", stParameterDetails);

                // Modified on 17-03-2017
                //stParameterDetails.Value = ds1.Tables[0].Rows[0]["MobileNumber"];
                //stParameterDetails.DataType = SqlDbType.VarChar;
                //ParameterList.AddParameter.Add("MobileNumber", stParameterDetails);

                stParameterDetails.Value = DateTime.Now.ToString();
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("UserCreateDate", stParameterDetails);

                stParameterDetails.Value = ds1.Tables[0].Rows[0]["numberOfPerson"];
                stParameterDetails.DataType = SqlDbType.Int;
                ParameterList.AddParameter.Add("numberOfPerson", stParameterDetails);

                stParameterDetails.Value = ds1.Tables[0].Rows[0]["DOB"];
                stParameterDetails.DataType = SqlDbType.DateTime;
                ParameterList.AddParameter.Add("DOB", stParameterDetails);

                stParameterDetails.Value = ds1.Tables[0].Rows[0]["VehicleNumber"];
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("VehicleNumber", stParameterDetails);

                if (hdnPassType_obj.Value == "FOREIGN")
                {
                    stParameterDetails.Value = "Y";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOREIGN_FLAG", stParameterDetails);


                    stParameterDetails.Value = Session["FromDate"];
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("FromDate", stParameterDetails);

                    stParameterDetails.Value = Session["ToDate"];
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("ToDate", stParameterDetails);

                    stParameterDetails.Value = Session["Visa"];
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_VISA", stParameterDetails);

                    stParameterDetails.Value = Session["SecCl"];
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_SECURITY_CL", stParameterDetails);

                    stParameterDetails.Value = Session["SecLetterNo"];
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_CL_LETTER_NO", stParameterDetails);

                    stParameterDetails.Value = Session["SecCLValidity"];
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("FOR_SEC_CL_VALIDTY", stParameterDetails);
                }
                else
                {
                    stParameterDetails.Value = "N";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOREIGN_FLAG", stParameterDetails);

                    stParameterDetails.Value = Session["Date"];
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("FromDate", stParameterDetails);

                    stParameterDetails.Value = Session["Date"];
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("ToDate", stParameterDetails);

                    stParameterDetails.Value = "NA";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_VISA", stParameterDetails);

                    stParameterDetails.Value = "NA";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_SECURITY_CL", stParameterDetails);

                    stParameterDetails.Value = "NA";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("FOR_CL_LETTER_NO", stParameterDetails);

                    stParameterDetails.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    stParameterDetails.DataType = SqlDbType.DateTime;
                    ParameterList.AddParameter.Add("FOR_SEC_CL_VALIDTY", stParameterDetails);
                }
                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Update_Log", ParameterList.AddParameter);
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    // objDisplayLog.CustomMessage("Error While Updating " + txtVisitorName.Text.ToString().Trim(), this);
                }
                {
                    ParameterList.AddParameter.Clear();
                    stParameterDetails.Value = visitorID;
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                    stParameterDetails.Value = "ByID";
                    stParameterDetails.DataType = SqlDbType.VarChar;
                    ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);

                    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                    {
                        ds = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_Select_Print", ParameterList.AddParameter);
                    }
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // objDisplayLog.CustomMessage("No data " + txtVisitorName.Text.ToString().Trim(), this);
                    }
                    else
                    {
                        if (hdnPassType_obj.Value == "FOREIGN")
                        {
                            Session["LoadDetail"] = ds.Tables[0];
                            string url = "PrintPass_ForeignVisitor.aspx?TranID=" + ds.Tables[0].Rows[0]["VisitorTranID"] + "&Off_tran_ID=" + hdn_Ofcr_Tran_Id_obj.Value;
                            string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        }
                        else
                        {
                            Session["LoadDetail"] = ds.Tables[0];
                            string url = "PrintPass_Visitor.aspx?TranID=" + ds.Tables[0].Rows[0]["VisitorTranID"] + "&Off_tran_ID=" + hdn_Ofcr_Tran_Id_obj.Value;
                            string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=800,width=750,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        }
                    }
                }
            }
            else
            {
                //   objDisplayLog.CustomMessage("Please Save the Record first and Capture Image", this);
            }
        }

        protected void btnNewForeign_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\FOREIGN VISITOR\\ForeignVisitor.aspx");
        }

        protected void lnk_ForReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\VisitorVPass.aspx");
        }

        protected void lnk_UpdateVisitor_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\FOREIGN VISITOR\\visitorUpdate.aspx");
        }

        protected void btnhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\DVSC_HOME.aspx");
        }
    }
}