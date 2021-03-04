using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fargo.PrinterSDK;

namespace EntityFrameworkDBF
{
    public partial class Print_ContractorPass : System.Web.UI.Page
    {
        // Printer Integration Part Start 30-01-2018
        string[] m_startSentinels;
        string[] m_stopSentinels;

        int x = 0;
        int y = 0;
        int I_X = 0;
        int I_Y = 0;
        int ShiftByX = 0;
        int ShiftByY = 0;
        string ImagePath = "";
        string ImagePathFooter = "";
        string PerosnImagePath = "";
        string CardNo = "";
        // Printer Integration Part End 30-01-2018

        VMSEntities DVSC = new VMSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string CardNo = Session["Cont_CardNo"].ToString();
                    var PrintData = (from x in DVSC.CONTRACTOR_DETAIL
                                     join f in DVSC.FIRMMASTERs on x.Cont_FirmID equals f.FIRM_ID
                                     join d in DVSC.DESIGNATION_MASTER on x.Cont_DesignationID equals d.DESIGNATION_ID
                                     where (x.Cont_CardNo == CardNo)
                                     select new { x.Cont_Aadhaar, x.Cont_CardNo, x.Cont_Name, f.FIRM_NAME, d.DESIGNATION_NAME, x.Cont_IssueDate, x.Cont_PVCValidity, x.Cont_Photo, x.Cont_Id }).ToList();
                    txtContractorCardNo.Text = PrintData[0].Cont_CardNo;
                    txtContractorFirm.Text = PrintData[0].FIRM_NAME;
                    txtContractorName.Text = PrintData[0].Cont_Name;
                    txtDateOfIssue.Text = Convert.ToDateTime(PrintData[0].Cont_IssueDate).ToString("dd/MMM/yyyy").Replace("/", " ");
                    txtDesignation.Text = PrintData[0].DESIGNATION_NAME;
                    txtEscortType.Text = "UNESCORTED";
                    txtPVCDate.Text = PrintData[0].Cont_Aadhaar;// Convert.ToDateTime(PrintData[0].Cont_PVCValidity).ToString("dd/MMM/yyyy").Replace("/", " ");
                    imgContractor.ImageUrl = "~/GetImages.ashx?id=" + PrintData[0].Cont_Id;

                    // Printer Integration Part Start 30-01-2018
                    m_startSentinels = new string[3];
                    m_stopSentinels = new string[3];
                    // Set sentinels for ISO
                    SetStartSentinel(1, "%");
                    SetStartSentinel(2, ";");
                    SetStartSentinel(3, ";");
                    SetStopSentinel(1, "?");
                    SetStopSentinel(2, "?");
                    SetStopSentinel(3, "?");
                    // Printer Integration Part End 30-01-2018 
                }
                catch (Exception ee)
                {
                    Response.Write("<script> alert('Not able to get data due to some issue')</script>");
                }
            }
        }

        // Printer Integration Part Start 30-01-2018
        public void PrintData()
        {
            //PrintJob printJob = new PrintJob("HDP5600 Card Printer (Eth)");
            PrintJob printJob = new PrintJob("HDP5600 Card Printer");
            // Put "elements" into the print job.

            // Image element.

            // Need to go up three sub-directories due to explicit compile size / platform.
            //printJob.AddPrintImageElement("..\\..\\..\\55.jpg", 1, 0, 0, 0);

            printJob.AddPrintImageElement("..\\EntityFrameworkDBF\\Images\\55.jpg", 1, 0, 0, 0);
            // Text elements
            // (Front)

            //printJob.AddPrintTextElement("Text Front", 120, 180, 0, "Bell MT", (int)(PrintJob.FontStyles.FONT_ATTRIBUTE_ITALIC), 18, (int)PrintJob.FontColors.FONT_COLOR_GREEN);

            printJob.AddPrintTextElement("NAME :", 100, 120, 0, "Calibri", 0, 18, (int)PrintJob.FontColors.FONT_COLOR_RED);
            printJob.AddPrintTextElement("DESIG :", 100, 120, 0, "Calibri", 0, 18, (int)PrintJob.FontColors.FONT_COLOR_RED);
            printJob.AddPrintTextElement("DOI :", 100, 120, 0, "Calibri", 0, 18, (int)PrintJob.FontColors.FONT_COLOR_RED);
            printJob.AddPrintTextElement("FIRM :", 100, 120, 0, "Calibri", 0, 18, (int)PrintJob.FontColors.FONT_COLOR_RED);
            printJob.AddPrintTextElement("UID NO :", 100, 120, 0, "Calibri", 0, 18, (int)PrintJob.FontColors.FONT_COLOR_RED);


            // (Back)
            printJob.AddPrintTextElement("Text Back", 75, 75, 1, "Arial", (int)(PrintJob.FontStyles.FONT_ATTRIBUTE_UNDERLINE | PrintJob.FontStyles.FONT_ATTRIBUTE_BOLD), 20, (int)PrintJob.FontColors.FONT_COLOR_BLACK);

            // Mag elements (three tracks) - note that the "~1" is added for the application by the PrintJob class.
            //printJob.AddPrintMagElement(GetStartSentinel(1) + "JOE SAMPLE" + GetStopSentinel(1), 1);
            //printJob.AddPrintMagElement(GetStartSentinel(2) + "1357924680" + GetStopSentinel(2), 2);
            //printJob.AddPrintMagElement(GetStartSentinel(3) + "2468013579" + GetStopSentinel(3), 3);

            // Then call "DoPrint" to print all of the image, text and mag elements
            printJob.DoPrint("The Job Name");

            // Wait for it to either be ready or have exception.  The result tells what happened.
            CurrentActivity lastCurrentActivity = printJob.FinishDoc();
        }

        string GetStartSentinel(int trackNumber)
        {
            return (m_startSentinels[trackNumber - 1]);
        }

        string GetStopSentinel(int trackNumber)
        {
            return (m_stopSentinels[trackNumber - 1]);
        }

        void SetStartSentinel(int trackNumber, string value)
        {
            m_startSentinels[trackNumber - 1] = value;
        }

        void SetStopSentinel(int trackNumber, string value)
        {
            m_stopSentinels[trackNumber - 1] = value;
        }
        // Printer Integration Part End 30-01-2018
    }
}