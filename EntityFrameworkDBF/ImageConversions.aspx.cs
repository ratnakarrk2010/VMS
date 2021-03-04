using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace EntityFrameworkDBF
{
    public partial class ImageConversions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreatePhoto();
        }

        void CreatePhoto()
        {
            try
            {
                string strPhoto = Request.Form["imageData"]; //Get the image from flash file
                Session["photo"] = Convert.FromBase64String(strPhoto);
                //FileStream fs = new FileStream("C:\\Webcam.jpg", FileMode.OpenOrCreate, FileAccess.Write);
                //BinaryWriter br = new BinaryWriter(fs);
                //br.Write(photo);
                //br.Flush();
                //br.Close();
                //fs.Close();
            }
            catch (Exception Ex)
            {

            }
        }
    }
}
