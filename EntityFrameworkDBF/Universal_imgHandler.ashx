<%@ WebHandler Language="C#" Class="Universal_imgHandler" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Web.SessionState;

public class Universal_imgHandler : IHttpHandler
{
    static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString);
    protected ParameterDetails stParameterDetails;
    protected SqlParameterDetails sqlparamterDetaisl;
    DataSet ds = new DataSet();
    public void ProcessRequest(HttpContext context)
    {
        string visitorID="", img_proof_type="";
        try
        {
            if (context.Request.QueryString["visitorID"] != null && context.Request.QueryString["img_proof_type"] != null)
            {
                visitorID = context.Request.QueryString["visitorID"].ToString();
                img_proof_type = context.Request.QueryString["img_proof_type"].ToString();
            }
            else
            {
             //   throw new ArgumentException("No parameter specified");
            } 
            context.Response.ContentType = " /jpeg";
            Stream strm = DisplayImage(visitorID, img_proof_type);
            byte[] buffer = new byte[2048];
            int byteSeq = strm.Read(buffer, 0, 2048);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 2048);
            } 
        }
        catch (Exception ex)
        {

        }
    } 
    public Stream DisplayImage(string visitorID, string img_proof_type)
    {
        try
        {
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                ds = getdata("select " + img_proof_type + " from VisitorTransactionDetail where VisitorTranID=" + visitorID);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                return new MemoryStream((byte[])ds.Tables[0].Rows[0][0]);//["Photo"]);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
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
}