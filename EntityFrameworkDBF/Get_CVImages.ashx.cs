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
    /// Summary description for Get_CVImages
    /// </summary>
    public class Get_CVImages : IHttpHandler
    {
        VMSEntities DVSC = new VMSEntities();
        string constring = ConfigurationManager.ConnectionStrings["VMSEntities"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            int id = 0;
            try
            {
                if (context.Request.QueryString["id"] != null)
                    id = Convert.ToInt32(context.Request.QueryString["id"]);
                else
                {
                    throw new ArgumentException("No parameter specified");
                }
                Stream strm = DisplayImage(id);
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

        public Stream DisplayImage(int id)
        {
            try
            {
                var r = (from x in DVSC.VisitorTransactionDetails where x.VisitorTranID == id select x).First();
                return new MemoryStream(r.VisitiorPhoto.ToArray());
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