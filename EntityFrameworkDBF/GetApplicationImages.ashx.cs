using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFrameworkDBF;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace EntityFrameworkDBF
{
    /// <summary>
    /// Summary description for GetImages
    /// </summary>
    public class GetApplicationImages : IHttpHandler
    {
        VMSEntities DVSC = new VMSEntities();
        string constring = ConfigurationManager.ConnectionStrings["PASS_MANAGEMENT_SYSTEMEntities1"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            //int id = 0;
            int Appid = 0;
            try
            {
                if (context.Request.QueryString["Appid"] != null)
                    Appid = Convert.ToInt32(context.Request.QueryString["Appid"]);
                else
                {
                    throw new ArgumentException("No parameter specified");
                }
                //context.Response.ContentType = " /jpeg";
                Stream strm = DisplayApplicationImage(Appid);
                byte[] buffer = new byte[4096];
                int byteSeq = strm.Read(buffer, 0, 4096);
                while (byteSeq > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, byteSeq);
                    byteSeq = strm.Read(buffer, 0, 4096);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Stream DisplayApplicationImage(int Appid)
        {
            try
            {
                var r = (from x in DVSC.APPLICATION_FORM where x.APP_ID == Appid select x).First();
                //var r = DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_Id == id);
                return new MemoryStream(r.APP_FORM.ToArray());
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
    }
}