using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public Helper()
    {
        
    }
    /// <summary>
    /// CalculateAge
    /// </summary>
    /// <param name="Bday"></param>
    /// <param name="Cday"></param>
    /// <returns></returns>
    /// BY PRAHALAD BHAGAT 26/08/2016
    public static int CalculateAge(DateTime Bday, DateTime Cday)
    {
        int year;
        int month;
        int days;

        if ((Cday.Year - Bday.Year) > 0 || (((Cday.Year - Bday.Year) == 0)) && ((Bday.Month < Cday.Month) || ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day))))
        {

            int dayinBday_Month = DateTime.DaysInMonth(Bday.Year, Bday.Month);
            int DaysRemain = Cday.Day + (dayinBday_Month - Bday.Day);
            if (Cday.Month > Bday.Month)
            {
                year = Cday.Year - Bday.Year;
                month = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / dayinBday_Month);
            }
            else if (Cday.Month == Bday.Month)
            {
                if (Cday.Day >= Bday.Day)
                {
                    year = (Cday.Year - 1) - Bday.Year;
                    month = 0;
                    days = Cday.Day - Bday.Day;
                    //days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                }
                else
                {
                    year = (Cday.Year - 1) - Bday.Year;
                    month = 11;
                    days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                }
            }
            else
            {
                year = (Cday.Year - 1) - Bday.Year;
                month = Cday.Month + (11 - Bday.Month + Math.Abs(DaysRemain / dayinBday_Month));
                days = (DaysRemain % dayinBday_Month + dayinBday_Month) % dayinBday_Month;
            }
        }
        else
        {
            throw new ArgumentException("Please enter valid date.");
        }
        return year;
    }


    public static void PopulateDropDown(DataView dvSource, DropDownList ddlist, string strValueField, string strTextField)
    {
        ddlist.Items.Clear();
        ddlist.DataValueField = strValueField;
        ddlist.DataTextField = strTextField;
        ddlist.DataSource = dvSource;
        ddlist.DataBind();
        ddlist.Items.Insert(0, new ListItem("--SELECT--", "-1"));
    }

    string Result = "0";
    string Error = "";
    public DataSet read_file_to_dsDAL(System.Web.UI.WebControls.FileUpload File_uploader_Obj)
    {
        DataSet ds = new DataSet();
        try
        {
            //   string fileFullpath = File_uploader_Obj.PostedFile.FileName;//FileName; 
            string filename = Path.GetFileName(File_uploader_Obj.PostedFile.FileName);


            string sServerPath = HttpContext.Current.Server.MapPath(".");
            sServerPath = sServerPath + "\\dbf\\" + filename;
            File_uploader_Obj.PostedFile.SaveAs(sServerPath);
            string directory = sServerPath.Substring(0, (sServerPath.Length - filename.Length));

            ds = Import_file(sServerPath, directory, filename);
        }
        catch (Exception ee)
        {


        }


        return ds;
    }

    private DataSet Import_file(string fileFullpath, string directory, string filename)
    {
        OleDbConnection myConn = new OleDbConnection();
        DataSet ds = new DataSet();
        DataSet myDataset = new DataSet();
        try
        {
            string StrConn = "", StrSheetName = "";
            string[] tempArr;
            string[] tempArr1;
            int intI = 0;

            tempArr = fileFullpath.Split('\\');
            StrSheetName = tempArr[tempArr.Length - 1];
            tempArr1 = StrSheetName.Split('.');

            if (tempArr1[1].ToUpper() == "XLS")
            {
                StrSheetName = tempArr1[0] + "$";
                StrConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileFullpath + ";" + "Extended Properties=Excel 8.0;";  // work with excel
                intI = 1;
            }
            else if (tempArr1[1].ToUpper() == "XLSX")
            {
                StrSheetName = tempArr1[0] + "$";
                StrConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullpath + ";" + "Extended Properties=Excel 8.0;Persist Security Info=False";  // work with excel
                // OleDbConnection excel_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Excel_path + ";Extended Properties=Excel 8.0;Persist Security Info=False");

                intI = 1;
            }
            else if (tempArr1[1].ToUpper() == "DBF")
            {
                StrSheetName = tempArr1[0];
                StrConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + directory + ";" + "Extended Properties=dBASE IV;"; // work with DBF file
                intI = 1;
            }
            else
            {
                Result = "0";
                Error = "Invalid File Type";
            }
            if (intI == 1)
            {
                myConn = new OleDbConnection(StrConn);
                myConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + "Sheet1$" + "]", myConn);
                myDataset = new DataSet();
                myCommand.Fill(myDataset, StrSheetName);
                Result = "1";
            }

        }
        catch (OleDbException ole_ex)
        {
            Error = ole_ex.ErrorCode.ToString();
        }
        catch (Exception ee)
        {
            Exception ee1 = ee;

            Error = ee.StackTrace;
        }
        finally
        {
            myConn.Close();
        }
        return myDataset;
    }

    public static string encreptSubstution(string SRNO)
    {
        string ID = string.Concat(SRNO, DateTime.Now.Day, DateTime.Now.Month,"13"); 
        Array ar = ID.ToCharArray();//DateTime.Now.Second
        string finalkey = "";
        foreach (char a in ar)
        {
            if (a == '1')
            {
                finalkey += "W";
            }
            else if (a == '2')
            {
                finalkey += "N";
            }
            else if (a == '3')
            {
                finalkey += "C";
            }
            else if (a == '4')
            {
                finalkey += "M";
            }
            else if (a == '5')
            {
                finalkey += "B";
            }
            else if (a == '6')
            {
                finalkey += "V";
            }
            else if (a == '7')
            {
                finalkey += "I";
            }
            else if (a == '8')
            {
                finalkey += "R";
            }
            else if (a == '9')
            {
                finalkey += "A";
            }
            else if (a == '0')
            {
                finalkey += "T";
            }

        }

        return finalkey + "K";
    }

    public static string decriptSubstution(string encreptedSRNO)
    {
        Array ar = encreptedSRNO.ToCharArray();
        string finalkey = "";
        foreach (char a in ar)
        {
            if (a == 'W')
            {
                finalkey += "1";
            }
            else if (a == 'N')
            {
                finalkey += "2";
            }
            else if (a == 'C')
            {
                finalkey += "3";
            }
            else if (a == 'M')
            {
                finalkey += "4";
            }
            else if (a == 'B')
            {
                finalkey += "5";
            }
            else if (a == 'V')
            {
                finalkey += "6";
            }
            else if (a == 'I')
            {
                finalkey += "7";
            }
            else if (a == 'R')
            {
                finalkey += "8";
            }
            else if (a == 'A')
            {
                finalkey += "9";
            }
            else if (a == 'T')
            {
                finalkey += "0";
            }
            else if (a == 'K')
            {
                
            }
        }

        return finalkey;
    }

   
}