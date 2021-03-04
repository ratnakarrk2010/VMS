using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace EntityFrameworkDBF
{
    /// <summary>
    /// Summary description for GetApplicationData
    /// </summary>
    public class GetApplicationData : IHttpHandler
    {
        VMSEntities DVSC = new VMSEntities();
        string constring = ConfigurationManager.ConnectionStrings["VMSEntities"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            int Appid = 0;
            int PDF = 0;
            bool Flag = false;
            byte[] bytes;
            Stream strm;
            try
            {
                if (context.Request.QueryString["Appid"] != null && context.Request.QueryString["PDF"] != null)
                {
                    Appid = Convert.ToInt32(context.Request.QueryString["Appid"]);
                    PDF = Convert.ToInt32(context.Request.QueryString["PDF"]);
                    Flag = true;
                }
                if (Flag == false)
                {
                    if (context.Request.QueryString["Appid"] != null)
                        Appid = Convert.ToInt32(context.Request.QueryString["Appid"]);
                    else
                    {
                        throw new ArgumentException("No parameter specified");
                    }
                    strm = DisplayApplicationImage(Appid);
                    byte[] buffer = new byte[4096];
                    int byteSeq = strm.Read(buffer, 0, 4096);
                    while (byteSeq > 0)
                    {
                        context.Response.OutputStream.Write(buffer, 0, byteSeq);
                        byteSeq = strm.Read(buffer, 0, 4096);
                    }
                }
                else
                {
                    strm = DisplayApplicationImage(Appid);
                    var DATA = (from x in DVSC.APPLICATION_FORM where x.APP_ID == Appid select x).First();
                    bytes = DATA.APP_FORM;
                    context.Response.Buffer = true;
                    context.Response.Charset = "";
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //context.Response.ContentType = "application/pdf";
                    context.Response.BinaryWrite(bytes);
                    context.Response.Flush();
                    context.Response.End();
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