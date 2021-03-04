<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="REPORT_HOME.aspx.cs" EnableEventValidation="false" Inherits="EntityFrameworkDBF.REPORTS.REPORT_HOME"
    Theme="Admin_Basic" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtView').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    </script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            <br />
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            REPORTS
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btncancelhome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                OnClick="btncancelhome_Click" />
        </div>
    </center>
    <center>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSelectReport" runat="server" Text="SELECT REPORT :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlselectReport" runat="server" Style="font-weight: 700" OnSelectedIndexChanged="ddlselectReport_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Individual Report" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Loss Report" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Total Issue Report" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Total Cancel Report" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Total Valid Report" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Expired But Not Renewed" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Total In Out Summary Report" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Religion Wise Report" Value="8"></asp:ListItem>
                        <asp:ListItem Text="In Out Details Report" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Today's Pending Out Report" Value="10"></asp:ListItem>
                         <asp:ListItem Text="State Wise Report" Value="13"></asp:ListItem>
                      
                    </asp:DropDownList>
                      <!--<asp:ListItem Text="Biometric Status Report" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Tiger Gate Activated List" Value="11"></asp:ListItem> 
                        <asp:ListItem Text="Muster Gate Activated List" Value="12"></asp:ListItem> 
                   -->
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoAll" runat="server" AutoPostBack="true" Text="ALL" Checked="true" GroupName="PassType"
                        Font-Bold="True" oncheckedchanged="rdoAll_CheckedChanged" />
                    <asp:RadioButton ID="rdoContractor" runat="server" AutoPostBack="true" Text="CONTRACTOR" GroupName="PassType"
                        Font-Bold="True" oncheckedchanged="rdoContractor_CheckedChanged" />
                    <asp:RadioButton ID="rdoEscorted" runat="server"  AutoPostBack="true" 
                        Text="ESCORTED" GroupName="PassType"
                        Font-Bold="True" oncheckedchanged="rdoEscorted_CheckedChanged"  />
                    <asp:RadioButton ID="rdoBank" runat="server" AutoPostBack="true" Text="BANK" 
                        GroupName="PassType" Font-Bold="True" 
                        oncheckedchanged="rdoBank_CheckedChanged" />
                    <asp:RadioButton ID="rdoShips" runat="server"  AutoPostBack="true" Text="SHIPS" GroupName="PassType"
                        Font-Bold="True" oncheckedchanged="rdoShips_CheckedChanged" />
                    <asp:RadioButton ID="rdoLabour" runat="server" AutoPostBack="true" 
                        Text="LABOUR" GroupName="PassType"
                        Font-Bold="True" oncheckedchanged="rdoLabour_CheckedChanged" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <span id="lblCount" runat="server" style="color: Green; font-size: x-large; font-weight: bold">
                    </span>
                     
                </td>
                <td>
                    <span id="lblCount1" runat="server" style="color: Green; font-size: x-large; font-weight: bold">
                    </span> <br />
                     
                </td>
                <td align="right">
                    <span id="lblCount2" runat="server" style="color: Green; font-size: x-large; font-weight: bold">
                    </span>
                     
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblfromdate" runat="server" Text="FROM DATE" Style="font-size: large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" placeholder="FROM DATE" runat="server" Style="font-size: medium;
                        font-weight: bold;"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="lbltodate" runat="server" Text="TO DATE" Style="font-size: large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttoadte" placeholder="TO DATE" runat="server" Style="font-size: large;
                        font-weight: bold;"></asp:TextBox>
                    <cc1:CalendarExtender ID="txttoadte_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txttoadte">
                    </cc1:CalendarExtender>
                </td>
            </tr>
        </table>
        <div>
            <span>OR</span>
        </div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblFirm" runat="server" Text="FIRM NAME :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFirm" runat="server" Width="100%" Style="font-weight: 700">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblRelegion" runat="server" Text="SELECT RELIGION :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRelegion" runat="server" Style="font-weight: 700">
                    </asp:DropDownList>  
                     
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchby" runat="server" Text="SEARCH BY :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlsearch" runat="server" Style="font-weight: 700">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="PASS NO" Value="1"></asp:ListItem>
                        <asp:ListItem Text="AADHAAR NO" Value="2"></asp:ListItem>
                        <asp:ListItem Text="RFID ID NO" Value="3"></asp:ListItem>
                        <asp:ListItem Text="DOCKYARD ID NO" Value="4"></asp:ListItem>
                        <asp:ListItem Text="NAME" Value="5"></asp:ListItem>
                        <asp:ListItem Text="MOBILE NO" Value="6"></asp:ListItem>
                        <%--<asp:ListItem Text="PSU" Value="7"></asp:ListItem>
                        <asp:ListItem Text="UNIT" Value="8"></asp:ListItem>
                        <asp:ListItem Text="SHOP" Value="9"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" CssClass="txtView" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnSearch1" runat="server" Text="SHOW" CssClass="btnView" OnClick="btnSearch1_Click" />
                    </div>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat; width: 145px">
                        <asp:Button ID="btnExportToExcel" runat="server" Text="EXPORT TO EXCEL" CssClass="btnView"
                            OnClick="btnExportToExcel_Click" />
                    </div>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnReset" runat="server" Text="RESET" CssClass="btnView" OnClick="btnReset_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <div style="color: #a39a19; font-size: large; font-weight: bold" runat="server" id="DivSelectColumn">
            SELECT COLUMN TO DISPLAY DATA
        </div>
        <div style="color: #a39a19; font-size: large; font-weight: bold">
            <asp:CheckBox ID="chkAll" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
        </div>
        <table>
            <tr>
                <td>
                    <asp:CheckBox ID="chkName" Text="Full Name" runat="server" Checked="true" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkAadhaar" Text="Aadhaar No" runat="server" Checked="true" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkMobile" Text="Mobile No" runat="server" Checked="true" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFirmFileNo" Text="File No" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="ChkFirmName" Text="Firm Name" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkWONO" Text="Firm Work Order" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkWOValidity" Text="Work Order Validity" runat="server" Checked="true"
                        AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkunit" Text="SHIP Name" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkState" Text="State" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPVC" Text="PVC No" runat="server" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPVCvalidity" Text="PVC Validity" runat="server" Checked="true"
                        AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkRFIDNo" Text="RFID No" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkRFIDvalidity" Text="RFID Validity" runat="server" Checked="true"
                        AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkCardNo" Text="Card No" runat="server" Checked="true" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkDesignation" Text="Designation" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkReligion" Text="Religion" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkGender" Text="Gender" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIssueDate" Text="Issue Date" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkCancel" Text="Cancel Status" runat="server" AutoPostBack="true"
                        Checked="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkCancelReason" Text="Cancel Reason" runat="server" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPassType" Text="Pass Type" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkDateLoss" Text="Date Of Loss" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPlaceLoss" Text="Place Of Loss" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFine" Text="Fine" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFIR" Text="FIR No" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkCancelDate" Text="Cancel Date" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkBlackList" Text="Black List Status" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkICardNo" Text="I Card No" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPsuUnit" Text="PSU Unit" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkUnitEmploee" Text="Unit Employee" runat="server" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="ChkExpireDate" Text="Valid Till" runat="server" AutoPostBack="true"
                        Checked="true" />
                </td>
            </tr>
        </table>
        <div style="overflow-x: scroll">

            <asp:GridView ID="Gv_Demo" runat="server"  AutoGenerateColumns="true" AllowPaging="false" 
                 HeaderStyle-BackColor="#ff7e00">
                <Columns>
                    <asp:TemplateField HeaderText="Sr.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
        </div>
        <div style="overflow-x: scroll">
            <asp:GridView ID="Gv_Reports" runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true"  PageSize="50"
                AutoGenerateColumns="false" AllowPaging="true" SkinID="grid1" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
                CellPadding="3" OnPageIndexChanging="Gv_Reports_PageIndexChanging" OnRowDataBound="Gv_Reports_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="SL">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblCName" runat="server" Text='<%#Eval("Cont_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AADHAAR NO">
                        <ItemTemplate>
                            <asp:Label ID="lblAadhaar" runat="server" Text='<%#Eval("Cont_Aadhaar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOBILE NO">
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Cont_Mobile") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRM FILE NO">
                        <ItemTemplate>
                            <asp:Label ID="lblFirmFileNo" runat="server" Text='<%#Eval("Cont_FirmFileNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRM NAME">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFirmid" runat="server" Value='<%#Eval("Cont_FirmID") %>' />
                            <asp:Label ID="lblFirmName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRM WORK ORDER NO">
                        <ItemTemplate>
                            <asp:Label ID="lblFrimWO" runat="server" Text='<%#Eval("Cont_FirmWO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WORK ORDER VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblWOvalidity" runat="server" Text='<%#Eval("Cont_WOValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SHIP NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Cont_Unit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATE NAME">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnState" runat="server" Value='<%#Eval("Cont_StateID") %>' />
                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("STATE_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PVC NO">
                        <ItemTemplate>
                            <asp:Label ID="lblPVCNo" runat="server" Text='<%#Eval("Cont_PVCNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PVC VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblPVCvalidity" runat="server" Text='<%#Eval("Cont_PVCValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RFID NO">
                        <ItemTemplate>
                            <asp:Label ID="lblRfidNo" runat="server" Text='<%#Eval("Cont_RFIDNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RFID VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblRfidValidity" runat="server" Text='<%#Eval("Cont_RFIDValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CARD NO">
                        <ItemTemplate>
                            <asp:Label ID="lblCardno" runat="server" Text='<%#Eval("Cont_CardNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESIGANTION">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnDesignation" runat="server" Value='<%#Eval("Cont_DesignationID") %>' />
                            <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("DESIGNATION_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RELIGION">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnREligion" runat="server" Value='<%#Eval("Cont_ReligionID") %>' />
                            <asp:Label ID="lblreligion" runat="server" Text='<%#Eval("R_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GENDER">
                        <ItemTemplate>
                            <asp:Label ID="lblgender" runat="server" Text='<%#Eval("Cont_Gender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISSUE DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblissuedate" runat="server" Text='<%#Eval("Cont_IssueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CANCEL STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblcancelstatus" runat="server" Text='<%#Eval("Cont_CancelFLag") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CANCEL REASON">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCancelReason" runat="server" Value='<%#Eval("Cont_CancelReason") %>' />
                            <asp:Label ID="lblcancelreason" runat="server" Text='<%#Eval("CR_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PASS TYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblpasstype" runat="server" Text='<%#Eval("Cont_PassType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATE OF LOSS">
                        <ItemTemplate>
                            <asp:Label ID="lbldateloss" runat="server" Text='<%#Eval("Cont_DateOFLoss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PLACE OF LOSS">
                        <ItemTemplate>
                            <asp:Label ID="lblplaceloss" runat="server" Text='<%#Eval("Cont_PlaceOfLoss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FINE">
                        <ItemTemplate>
                            <asp:Label ID="lblfine" runat="server" Text='<%#Eval("Cont_Fine") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIR">
                        <ItemTemplate>
                            <asp:Label ID="lblfir" runat="server" Text='<%#Eval("Cont_Fir") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CANCEL DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblcanceldate" runat="server" Text='<%#Eval("Cont_CancelDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BLACK LIST STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblblacklist" runat="server" Text='<%#Eval("Cont_BlackList") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I CARD NO">
                        <ItemTemplate>
                            <asp:Label ID="lblIcardNo" runat="server" Text='<%#Eval("Cont_IcardrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PSU UNIT">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnPsuUnit" runat="server" Value='<%#Eval("Cont_PSUunitID") %>' />
                            <asp:Label ID="lblPSuunit" runat="server" Text='<%#Eval("PSU_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT EMPLOYEE">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitEmp" runat="server" Text='<%#Eval("Cont_UnitEmp") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VALID TILL">
                        <ItemTemplate>
                            <asp:Label ID="lblValidity" runat="server" Text='<%#Eval("Cont_MinDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Dockyard ID">
                        <ItemTemplate>
                            <asp:Label ID="lblDockyardID" runat="server" Text='<%#Eval("Cont_DocID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </center>
    <center>
        <table>
            <tr>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Visible="true">
        </rsweb:ReportViewer>
    </center>
    <table style="width: 1000px; visibility: hidden">
        <tr style="width: 1000px">
            <td style="width: 25px">
                <asp:RadioButton ID="rdoIndividual" runat="server" GroupName="Reports" Text="Individual Report"
                    Font-Bold="true" Font-Size="Large" ForeColor="#a39a19" />
            </td>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoLoss" runat="server" Text="Loss Report" GroupName="Reports"
                    Font-Bold="true" ForeColor="#a39a19" Font-Size="Large" />
            </td>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoTotalIssue" runat="server" Text="Total Issue Report" GroupName="Reports"
                    Font-Bold="true" ForeColor="#a39a19" Font-Size="Large" />
            </td>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoTotalCancel" runat="server" Text="Total Cancel Report" GroupName="Reports"
                    Font-Bold="true" ForeColor="#a39a19" Font-Size="Large" />
            </td>
        </tr>
        <tr>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoTotalValid" runat="server" GroupName="Reports" Text="Total Valid Report"
                    Font-Bold="true" Font-Size="Large" ForeColor="#a39a19" />
            </td>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoExpiredNotCancel" runat="server" Text="Expired But Not Cancelled"
                    GroupName="Reports" Font-Bold="true" ForeColor="#a39a19" Font-Size="Large" />
            </td>
            <td style="width: 25px">
                <asp:RadioButton ID="rdoTotalInOut" runat="server" Text="Total In Out Report" GroupName="Reports"
                    Font-Bold="true" ForeColor="#a39a19" Font-Size="Large" />
            </td>
        </tr>
    </table>
</asp:Content>
