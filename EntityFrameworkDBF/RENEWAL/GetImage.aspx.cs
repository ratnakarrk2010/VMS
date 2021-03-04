using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing.Imaging;
using System.Data;
using System.Runtime.InteropServices;

namespace EntityFrameworkDBF.RENEWAL
{
    public partial class GetImage : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        CONTRACTOR_DETAIL cont = new CONTRACTOR_DETAIL();
        string fileName = "";
        string filePath = "";
        string cropFileName = "";
        string cropFilePath = "";
        int count = 0;
        int CroppedID = 0;
        public byte[] ContPhoto;
        int ID = 0;
        string CardNo = "";
        DropDownFunction ddl = new DropDownFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                GetImage1();
            }
        }

        public void GetImage1()
        {
            string strPhoto = ConfigurationManager.AppSettings["Image"].ToString();
            ID = Convert.ToInt32(Session["Cont_ID"]);
            imgApplication.ImageUrl = "~/GetImages.ashx?id=" + ID;
            var Image = (from x in DVSC.CONTRACTOR_DETAIL where (x.Cont_Id == ID) select x).FirstOrDefault();
            byte[] photo = Image.Cont_Photo;
            FileStream fs = new FileStream(strPhoto + Image.Cont_CardNo + ".jpg", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter br = new BinaryWriter(fs);
            Session["FileName"] = fs.Name;
            br.Write(photo);
            br.Flush();
            br.Close();
            fs.Close();
            //imgApplication.ImageUrl = "~/D:\\SAVED PHOTO\\" + Image.Cont_CardNo + ".jpg";
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            // Crop Image Here & Save
            fileName = Path.GetFileName(Session["FileName"].ToString());
            filePath = ConfigurationManager.AppSettings["Image"].ToString() + fileName; //Path.Combine(Server.MapPath("~/UploadImages"), fileName);
            cropFileName = "";
            cropFilePath = "";
            CardNo = Session["Cont_CardNo"].ToString();
            ID = Convert.ToInt32(Session["Cont_ID"]);
            //if (File.Exists(filePath))
            //{
            System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
            Rectangle CropArea = new Rectangle(
                Convert.ToInt32(X.Value),
                Convert.ToInt32(Y.Value),
                Convert.ToInt32(W.Value),
                Convert.ToInt32(H.Value));
            try
            {
                Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                }
                cropFileName = "crop_" + fileName;
                cropFilePath = ConfigurationManager.AppSettings["Image"].ToString() + cropFileName;
                bitMap.Save(cropFilePath);
                //Response.Redirect("~/UploadImages/" + cropFileName, false);
                AdjustBrightness(bitMap, 10, fileName);

                MemoryStream ms = new MemoryStream();
                //bitMap.Save(ms, bitMap.RawFormat);
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ContPhoto = ms.ToArray();
                InsertCroppedImage(ID, CardNo, ContPhoto);

                Bitmap bitMap1 = ImageTrim(bitMap);
                bitMap1.Save(cropFilePath);
                Session["cropFilePath"] = cropFilePath;
            }
            catch (Exception ex)
            {
                throw;
            }
            //}
        }

        public static Bitmap AdjustBrightness(Bitmap Image, int Value, string FileName)
        {
            string fileName = FileName;
            string filePath = "";
            string cropFileName = "";
            string cropFilePath = "";
            Bitmap TempBitmap = Image;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float FinalValue = (float)Value / 255.0f;

            float[][] FloatColorMatrix ={

                     new float[] {1, 0, 0, 0, 0},

                     new float[] {0, 1, 0, 0, 0},

                     new float[] {0, 0, 1, 0, 0},

                     new float[] {0, 0, 0, 1, 0},

                     new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                 };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            cropFileName = "Bri" + fileName;
            cropFilePath = ConfigurationManager.AppSettings["Image"].ToString() + cropFileName;
            NewBitmap.Save(cropFilePath);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        public void GetImageFromDb()
        {

        }

        public void InsertCroppedImage(int Cont_ID, string Cont_CardNo, byte[] CropImage)
        {
            CroppedImage croppedImage = new CroppedImage();
            count = DVSC.CroppedImages.Count(x => x.Cont_ID == Cont_ID && x.Cont_CardNo == Cont_CardNo);
            if (count > 0)
            {
                croppedImage = DVSC.CroppedImages.First(x => x.Cont_ID == Cont_ID && x.Cont_CardNo == Cont_CardNo);
                croppedImage.CroppedImage1 = CropImage;
                DVSC.SaveChanges();
            }
            else
            {
                DataSet ds = new DataSet();
                ds = ddl.get_data_from_DB("select top 1 A.[CroppedID] + 1 as [CroppedID] from CroppedImage A where not exists (select * from CroppedImage B where B.[CroppedID] = A.[CroppedID] + 1)");
                CroppedID = Convert.ToInt32(ds.Tables[0].Rows[0]["CroppedID"]);
                if (CroppedID == 0)
                {
                    CroppedID = 1;
                }
                if (!string.IsNullOrEmpty(Cont_CardNo))
                {
                    Cont_CardNo = "0";
                }
                croppedImage.CroppedID = CroppedID;
                croppedImage.Cont_CardNo = Cont_CardNo;
                croppedImage.Cont_ID = Cont_ID;
                croppedImage.CroppedImage1 = CropImage;
                DVSC.CroppedImages.AddObject(croppedImage);
                DVSC.SaveChanges();
            }
        }

        public Bitmap ImageTrim1(Bitmap img)
        {
            //Get Image Data
            BitmapData bd = img.LockBits(new Rectangle(Point.Empty, img.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int[] rgbvalue = new int[img.Height * img.Width];
            Marshal.Copy(bd.Scan0, rgbvalue, 0, rgbvalue.Length);

            int left = bd.Width;
            int top = bd.Height;
            int right = 0;
            int bottom = 0;
            string cropFilePath = "";
            //Determine top
            for (int i = 0; i < rgbvalue.Length; i++)
            {
                int color = rgbvalue[i] & 0xffffff;
                if (color != 0xffffff)
                {
                    int r = i / bd.Width;
                    int c = i % bd.Width;

                    if (left > c)
                    {
                        left = c;
                    }
                    if (right < c)
                    {
                        right = c;
                    }
                    bottom = r;
                    top = r;
                    break;
                }
            }

            //determine bottom
            for (int j = 0; j < rgbvalue.Length; j++)
            {
                int color = rgbvalue[j] & 0xffffff;
                if (color != 0xffffff)
                {
                    int r = j / bd.Width;
                    int c = j % bd.Width;

                    if (left > c)
                    {
                        left = c;
                    }
                    if (right < c)
                    {
                        right = c;
                    }
                    bottom = r;
                    //top = r;
                    break;
                }
            }

            if (bottom > top)
            {
                for (int r = top + 1; r < bottom; r++)
                {
                    //Determine Left
                    for (int c = 0; c < left; c++)
                    {
                        int color = rgbvalue[r * bd.Width + c] & 0xffffff;
                        if (color != 0xffffff)
                        {
                            if (color != 0xffffff)
                            {
                                if (left > c)
                                {
                                    left = c;
                                    break;
                                }
                            }
                        }
                    }
                    //deretemine right
                    for (int k = bd.Width - 1; k < right; k++)
                    {
                        int color = rgbvalue[r * bd.Width + k] & 0xffffff;
                        if (color != 0xffffff)
                        {
                            if (color != 0xffffff)
                            {
                                if (right > k)
                                {
                                    right = k;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            int width = right - left + 1;
            int height = bottom - top + 1;

            // Copy Image Data
            int[] ImageData = new int[width * height];
            for (int z = top; z <= bottom; z++)
            {
                Array.Copy(rgbvalue, z * bd.Width + left, ImageData, (z - top) * width, width);
            }

            Bitmap Newimg = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //Bitmap ndb = Newimg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            BitmapData ndb = Newimg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(ImageData, 0, ndb.Scan0, ImageData.Length);
            Newimg.UnlockBits(ndb);
            //cropFilePath = Session["cropFilePath"].ToString();
            //Newimg.Save(@"D:\SAVED PHOTO\");
            return Newimg;
        }

        public Bitmap ImageTrim(Bitmap bmp)
        {
            var BackColor = GetMatchedBackColor(bmp);
            if (BackColor.HasValue)
            {
                var bounds = GetImageBound(bmp, BackColor);
                var Diffx = bounds[1].X = bounds[0].X + 1;
                var Diffy = bounds[1].Y = bounds[0].Y + 1;
                var croppedbmp = new Bitmap(Diffx, Diffy);
                var g = Graphics.FromImage(croppedbmp);
                var destrect = new Rectangle(0, 0, croppedbmp.Width, croppedbmp.Height);
                var srcrect = new Rectangle(bounds[0].X, bounds[0].Y, Diffx, Diffy);
                g.DrawImage(bmp, destrect, srcrect, GraphicsUnit.Pixel);
                return croppedbmp;
            }
            else
            {
                return null;
            }
        }

        public static Color? GetMatchedBackColor(Bitmap bmp)
        {
            //Getting background color by cheking corners of original image
            var corners = new Point[]
            {            
             new Point(0,0),
             new Point(0,bmp.Height-1),
             new Point(0,bmp.Width-1),
             new Point(bmp.Width-1,bmp.Height-10)
            };
            for (int i = 0; i < 4; i++)
            {
                var CornerMatched = 0;
                var Backcolor = bmp.GetPixel(corners[i].X, corners[i].Y);
                for (int j = 0; j < 4; j++)
                {
                    //check RGB with some offset
                    var cornercolor = bmp.GetPixel(corners[j].X, corners[j].Y);
                    if ((cornercolor.R <= Backcolor.R * 1.1 && cornercolor.R >= Backcolor.R * 0.9) &&
                        (cornercolor.G <= Backcolor.G * 1.1 && cornercolor.G >= Backcolor.G * 0.9) &&
                        (cornercolor.B <= Backcolor.B * 1.1 && cornercolor.B >= Backcolor.B * 0.9))
                    {
                        CornerMatched++;
                    }

                }
                if (CornerMatched > 2)
                {
                    return Backcolor;
                }
            }
            return null;
        }

        private static Point[] GetImageBound(Bitmap bmp, Color? backcolor)
        {
            Color c;
            int width = bmp.Width, height = bmp.Height;
            bool upperleftpointfounded = false;
            var bound = new Point[2];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    c = bmp.GetPixel(x, y);
                    bool Sameasbackcolor = ((c.R <= backcolor.Value.R * 1.1 && c.R >= backcolor.Value.R * 0.9) &&
                        (c.G <= backcolor.Value.G * 1.1 && c.G >= backcolor.Value.G * 0.9) &&
                        (c.B <= backcolor.Value.B * 1.1 && c.B >= backcolor.Value.B * 0.9));
                    if (!Sameasbackcolor)
                    {
                        if (!upperleftpointfounded)
                        {
                            bound[0] = new Point(x, y);
                            bound[1] = new Point(x, y);
                            upperleftpointfounded = true;
                        }
                        else
                        {
                            if (x > bound[1].X)
                                bound[1].X = x;
                            else if (x < bound[0].X)
                                bound[0].X = x;
                            if (y >= bound[1].Y)
                                bound[1].Y = y;
                        }
                    }
                }
            }

            return bound;
        }
    }
}