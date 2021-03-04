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
    public class GetImages : IHttpHandler
    {
        VMSEntities DVSC = new VMSEntities();
        string constring = ConfigurationManager.ConnectionStrings["VMSEntities"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            int id = 0;
            //int Appid = 0;
            try
            {
                if (context.Request.QueryString["id"] != null)
                    id = Convert.ToInt32(context.Request.QueryString["id"]);
                else
                {
                    throw new ArgumentException("No parameter specified");
                }
                //context.Response.ContentType = " /jpeg";
                Stream strm = DisplayImage(id);
                byte[] buffer = new byte[4096];
                if (strm != null)
                {
                    int byteSeq = strm.Read(buffer, 0, 4096);
                    while (byteSeq > 0)
                    {
                        context.Response.OutputStream.Write(buffer, 0, byteSeq);
                        byteSeq = strm.Read(buffer, 0, 4096);
                    }
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
                var r = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Id == id select x).First();
                return new MemoryStream(r.Cont_Photo.ToArray());
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