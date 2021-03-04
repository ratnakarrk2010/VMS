<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ForeignVisitor.aspx.cs" Inherits="EntityFrameworkDBF.FOREIGN_VISITOR.ForeignVisitor"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.txtView').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });

        function isDate(txtDate) {
            var objDate;  // date object initialized from the txtDate string   
            var mSeconds; // milliseconds from txtDate   
            // date length should be 10 characters - no more, no less   
            if (txtDate.length != 10) return false;

            // extract day, month and year from the txtDate string   
            // expected format is mm/dd/yyyy   
            // subtraction will cast variables to integer implicitly   
            var day = txtDate.substring(0, 2) - 0;
            var month = txtDate.substring(3, 5) - 1; // because months in JS start with 0   
            var year = txtDate.substring(6, 10) - 0;

            // third and sixth character should be /   
            if (txtDate.substring(2, 3) != '/') return false;
            if (txtDate.substring(5, 6) != '/') return false;

            // test year range   
            if (year < 999 || year > 3000) return false;

            // convert txtDate to the milliseconds   
            mSeconds = (new Date(year, month, day)).getTime();

            // set the date object from milliseconds   
            objDate = new Date();
            objDate.setTime(mSeconds);

            // if there exists difference then date isn't valid   
            if (objDate.getFullYear() != year) return false;
            if (objDate.getMonth() != month) return false;
            if (objDate.getDate() != day) return false;

            // otherwise return true   
            return true;
        }
        function NumericCheck(e) {
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            keychar = keychar.toLowerCase();
            // control keys
            if ((key == null) || (key == 0) || (key == 8) ||
           (key == 9) || (key == 13) || (key == 46) || (key == 27) || (key == 32))
                return true;
            // alphas and numbers
            else if ((("0123456789-()").indexOf(keychar) > -1))
                return true;
            else
                return false;
        }
    </script>
    <%--<script type="text/javascript">
        function uppercase() {
        var res = 
        }
    </script>--%>
    <script type="text/javascript">
        function ShowHideDiv() {
            var DivGadget = document.getElementById("DivGadget");
            DivGadget.style.display = chkGadget.checked ? "block" : "none";
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#BtnAddnewRow').click(function (e) {
                e.preventDefault();

                var tableRow = $('#tbl_form tbody tr:nth-of-type(2)').clone();

                // alert('bawlat');
                var number = $('#tbl_form tbody tr:last-child td:first-child input').val();

                tableRow.find('input').val('');
                $('#tbl_form tbody').append(tableRow);
                var lastNo = Number(number) + 1;
                $('#tbl_form tbody tr:last-child td:first-child').find("input").val(lastNo);
                //.html("&nbsp;&nbsp;" + lastNo);

            });
        });
    </script>
    <style type="text/css">
        .tooltip
        {
            position: relative;
            display: inline-block;
        }
        .tooltip .tooltiptext
        {
            visibility: hidden;
            width: 100px;
            background-color: Black;
            color: #fff;
            text-align: justify;
            border-radius: 6px;
            padding: 1;
            position: absolute;
            z-index: 1;
        }
        .tooltip:hovor .tooltiptext
        {
            visibility: visible;
        }
        .main
        {
            font-size: 12px;
            color: #485565;
            font-family: Arial, Helvetica, sans-serif, Verdana;
            padding-left: 2;
            padding-right: 1;
            padding-top: 1;
            padding-bottom: 1;
            width: 100%;
        }
        .FIRM
        {
            width: 90%;
        }
        .NUMS
        {
            width: 8px;
        }
        .NUM
        {
            width: 5%;
        }
        .TXT
        {
            width: 90%;
        }
        .TXTS
        {
            width: 35px;
        }
        
        .td, th
        {
            border: 1px solid;
        }
        .style22
        {
            width: 100%;
        }
        .td tr
        {
            border: 1px solid;
            width: 100%;
        }
        .style23
        {
            width: 196px;
        }
        .style24
        {
        }
        .style26
        {
            height: 20px;
        }
        .style27
        {
            width: 196px;
            height: 20px;
        }
        .style35
        {
            width: 28px;
        }
        .style36
        {
            height: 20px;
            width: 28px;
        }
        .style38
        {
            width: 146px;
            height: 20px;
        }
        .style41
        {
            width: 162px;
        }
        .style42
        {
            width: 162px;
            height: 20px;
        }
        .style51
        {
            width: 135px;
        }
        .style52
        {
            width: 135px;
            height: 20px;
        }
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .btnhover:hover
        {
            text-decoration: underline;
            color: Black;
        }
        .style53
        {
            width: 241px;
        }
        .style54
        {
            width: 241px;
            height: 20px;
        }
        .style55
        {
            width: 236px;
        }
        .style56
        {
            width: 236px;
            height: 20px;
        }
        .style57
        {
            width: 153px;
        }
        .style58
        {
            width: 153px;
            height: 20px;
        }
        .uppercase
        {
            text-transform: uppercase;
        }
        
        .blockline *
        {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div style="">
        <div style="padding-top: 15px;">
            <center>
                <div class="header_02">
                    <span id="spnHeading" runat="server"></span>
                </div>
                <div style="padding-left: 600px">
                    <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="button" OnClick="btnhome_Click" />
                </div>
            </center>
            <asp:Label ID="Lblsuccess" runat="server" ForeColor="Green"></asp:Label>
            <br />
            <asp:Label ID="lblcount" runat="server" ForeColor="Red"></asp:Label>
            <table>
                <tr>
                    <td style="text-align: left; width: 916px">
                        <b>DATE :</b>
                        <asp:Label ID="LblDate" runat="server" Font-Bold="true" Style="text-align: right;"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <b>DEPT&nbsp:</b> &nbsp<asp:Label ID="TxtDeptName" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp
                        <%--<asp:TextBox ID="TxtDeptName" runat="server" Width="61px"></asp:TextBox>--%><b>
                            C NO&nbsp:</b> &nbsp
                        <%--<asp:TextBox ID="txtcno" runat="server" Width="61px"></asp:TextBox>--%>
                        <asp:Label ID="txtcno" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="padding: 3px; text-align: right;">
            </div>
            <div style="padding: 3px; text-align: left;">
            </div>
            <table class="style22">
                <tr>
                    <td style="text-align: center; font-weight: bold; font-size: 16px;">
                        FOREIGN VISITOR PASS
                    </td>
                </tr>
            </table>
            <br />
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="commander_apr"
                    DataKeyNames="ID" AutoGenerateColumns="false" Font-Size="Smaller" OnDataBound="OnDataBound"
                    SkinID="grid">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NEW">
                            <%--<ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>--%>
                            <FooterTemplate>
                                <asp:CheckBox ID="chkadd" runat="server" OnCheckedChanged="chkadd_OnCheckedChanged"
                                    AutoPostBack="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOCKYARD ID NO">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_pass_no" Text='<% #Eval("Passno") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" Width="40" OnTextChanged="txt_pass_no_textChanged" AutoPostBack="true"
                                    ID="txt_pass_no"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PASSPORT" ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblIdtype" Text='<% #Eval("ID_TYPE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:CheckBox runat="server" ID="chk_EMP_Type" /> --%>
                                <asp:DropDownList ID="drpIDType" runat="server" OnSelectedIndexChanged="drpIDType_SelectedIndexChanged"
                                    Width="80px" AutoPostBack="true">
                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                    <%-- <asp:ListItem Value="1">AADHAR CARD</asp:ListItem>
                                    <asp:ListItem Value="2">PAN CARD</asp:ListItem>--%>
                                    <asp:ListItem Selected="True" Value="3">PASSPORT</asp:ListItem>
                                    <%--  <asp:ListItem Value="4">DRIVING LICENCE</asp:ListItem>--%>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PASSPORT NO">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblidno" Text='<% #Eval("ID_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="TxtIdNo" Width="80px" OnTextChanged="TxtIdNo_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NAME">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_name_desig" Text='<% #Eval("Vname") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="temp_lbl_name_desig" Width="120px" CssClass="uppercase"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AGE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_age" Text='<% #Eval("Age") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="temp_lbl_age" Width="20px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GENDER">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_Gender" Text='<% #Eval("Gender") %>'></asp:Label>
                            </ItemTemplate>
                            <%--   <FooterTemplate>--%>
                            <FooterTemplate>
                                <%--<asp:CheckBox runat="server" ID="chk_EMP_Type" /> --%>
                                <asp:DropDownList ID="ddlgen" runat="server" Width="40px" AutoPostBack="true">
                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                    <asp:ListItem>MALE</asp:ListItem>
                                    <asp:ListItem>FEMALE</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                            <%--                            <asp:TextBox runat="server" ID="temp_lbl_Gender" Width="50"></asp:TextBox>
                        </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MOBILE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_CONTACT_NO" Text='<% #Eval("ContactNo") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="temp_lbl_CONTACT_NO" Width="80" MaxLength="10" OnTextChanged="OnTextChanged_temp_lbl_CONTACT_NO"
                                    AutoPostBack="true"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NATIONALITY">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_NATIONALITY" Text='<% #Eval("NATIONALITY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlnation" runat="server" Width="70">
                                </asp:DropDownList>
                                <%--    <asp:TextBox runat="server" ID="temp_lbl_NATIONALITY" Width="40"></asp:TextBox>--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FIRM TIN NO" HeaderStyle-Width="150px" ItemStyle-Width="150px"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirm_Tin" Text='<% #Eval("V_FIRM_TIN") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <span style="width: 127px; display: block;">
                                    <asp:CheckBox ID="chktin" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_chktin"
                                        ToolTip="If Tin not available" />
                                    <asp:TextBox runat="server" ID="temp_lblFirm_Tin" Width="100px" MaxLength="13" AutoPostBack="true"
                                        OnTextChanged="temp_lblFirm_Tin_textChanged"></asp:TextBox></span>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FIRM NAME">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_FIRM_NAME" Text='<% #Eval("FirmName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="temp_lbl_FIRM_NAME" Width="100px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FROM DATE" Visible="true">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_FROM" Text='<% #Eval("InTime") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txt_FROMDate" Width="120px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderFromDate" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txt_FROMDate">
                                </cc1:CalendarExtender>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TO DATE" Visible="true">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_To" Text='<% #Eval("OutTime") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txt_ToDate" Width="120px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderTodate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txt_ToDate">
                                </cc1:CalendarExtender>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PURPOSE OF VISIT" ItemStyle-Width="80px" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_PURPOSE_OF_VISIT" CssClass="uppercase" Text='<% #Eval("Purpose") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" CssClass="txtView" ID="txt_PURPOSE_OF_VISIT" Width="80px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VISITOR TYPE" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_EMP_Type" Text='<% #Eval("EMP_Type") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:CheckBox runat="server" ID="chk_EMP_Type" /> --%>
                                <asp:DropDownList ID="drpEmptype" runat="server" Width="60px">
                                    <%-- <asp:ListItem Value="-1">--SELECT--</asp:ListItem>--%>
                                    <asp:ListItem Value="1">CASUAL</asp:ListItem>
                                    <%--  <asp:ListItem>EXECUTIVE</asp:ListItem>
                                    <asp:ListItem>RETIRED</asp:ListItem>--%>
                                    <asp:ListItem Value="2" Selected="True">FOREIGN</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DURATION" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_DURATION" Text='<% #Eval("DURATION") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:CheckBox runat="server" ID="chk_EMP_Type" /> --%>
                                <asp:DropDownList ID="DDLDURATION" runat="server" Width="60px">
                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                    <asp:ListItem Selected="True">FOR 10 HRS</asp:ListItem>
                                    <asp:ListItem>FOR 2.5 HRS</asp:ListItem>
                                    <asp:ListItem>FOR 5 HRS</asp:ListItem>
                                    <asp:ListItem>FOR 7.5 HRS</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GADGET">
                            <ItemTemplate>
                                <asp:Button Text='<%#Eval("Gadget").ToString().ToUpper() %>' runat="server" ID="btn_view_Gadget"
                                    OnClick="btn_view_Gadget_click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VEHICLE">
                            <ItemTemplate>
                                <asp:Button Text='<%#Eval("Vehicle").ToString().ToUpper() %>' runat="server" ID="btn_view_vahical"
                                    OnClick="btn_view_vahical_click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ACTION">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btn_delete" OnClick="btn_delete_click" Text="REMOVE" />
                                <asp:HiddenField runat="server" ID="hdn_datakey" Value='<% #Eval("ID") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button runat="server" ID="btn_add" OnClick="btn_add_click" Text="ADD" BackColor="#ef6939" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="animation" BackColor="Black" ForeColor="White" />
                </asp:GridView>
                <asp:HiddenField runat="server" ID="hdn_latest_added_TempID" />
            </div>
            <div>
                <asp:HiddenField runat="server" ID="btnShowPopup" Visible="true" />
                <asp:HiddenField runat="server" ID="HiddenFieldGadget" Visible="true" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender_Vehiclegadget" runat="server" TargetControlID="btnShowPopup"
                    PopupControlID="tbl_Vehicle" CancelControlID="btn_Cancel" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="tbl_Vehicle" runat="server" BackColor="White" Style="display: none;
                    border: 1px solid #000; border-color: red; border-style: solid; padding: 10px;"
                    Width="665px">
                    <br />
                    <asp:Label ID="lblerr" Text="" runat="server" ForeColor="Red"></asp:Label>
                    <hr />
                    <br />
                    <asp:Label runat="server" Text="Is Visitor Bringing Vehicle" ID="lbl_Vehical"></asp:Label>
                    <asp:Button runat="server" ID="rdo_vehical_yes" AutoPostBack="true" OnClick="rdo_vehical_yes_click"
                        Text="YES" />
                    <%--GroupName="rdo_vehical"--%>
                    <asp:Button runat="server" ID="rdo_vehical_no" AutoPostBack="true" OnClick="rdo_vehical_no_click"
                        Text="RESET" Visible="true" /><%--GroupName="rdo_vehical"--%>
                    <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Grd_Vehicle_list"
                        DataKeyNames="SL" AutoGenerateColumns="false" Font-Size="Smaller" OnDataBound="OnDataBound"
                        SkinID="grid">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VEHICLE NAME">
                                <ItemTemplate>
                                    <%# Eval("NAME")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" CssClass="txtView" ID="txt_VehicleName" Width="100"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VEHICLE NO">
                                <ItemTemplate>
                                    <%# Eval("NUMBER")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txt_VehicleNo" Width="100"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PURPOSE">
                                <ItemTemplate>
                                    <%# Eval("PURPOSE")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txt_Vehicle_Purpose" TextMode="SingleLine" Width="200"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ACTION">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Delete" ID="btn_Vehicle_Delete" OnClick="btn_Vehicle_Delete_click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" Text="ADD" ID="btn_Vehicle" OnClick="btn_Vehicle_click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Label runat="server" Text="Is Visitor Bringing Electronic Device" ID="lbl_elect_Device"></asp:Label>
                    <asp:Button runat="server" ID="rdo_Gadget_yes" AutoPostBack="true" OnClick="rdo_Gadget_yes_click"
                        Text="YES" /><%--GroupName="rdo_Gadget"--%>
                    <asp:Button runat="server" ID="rdo_Gadget_no" AutoPostBack="true" OnClick="rdo_Gadget_no_click"
                        Text="RESET" Visible="true" /><br />
                    <%--GroupName="rdo_Gadget"--%>
                    <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Grd_Gadget_list"
                        DataKeyNames="SL" AutoGenerateColumns="false" Font-Size="Smaller" OnDataBound="OnDataBound"
                        SkinID="grid">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GADGET NAME">
                                <ItemTemplate>
                                    <%# Eval("NAME")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txt_GadgetName" Width="100"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GADGET NO">
                                <ItemTemplate>
                                    <%# Eval("NUMBER")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txt_GadgetNo" Width="100"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PURPOSE">
                                <ItemTemplate>
                                    <%# Eval("PURPOSE")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" ID="txt_Gadget_Purpose" TextMode="SingleLine" Width="200"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ACTION">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Delete" ID="btn_Delete_Gadget" OnClick="btn_Delete_Gadget_click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" Text="Add" ID="btn_Gadget" OnClick="btn_Gadget_click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="Msg" runat="server" Text="NOTE : IF VISITOR IS NOT BRINGING VEHICLE/DEVICE THEN CLICK CLOSE."></asp:Label>
                    <br />
                    <asp:Button runat="server" Text="CLOSE" ID="btn_Cancel" />
                </asp:Panel>
            </div>
            <br />
            <center>
                <table class="tbl-border" border="1px solid" cellpadding="5px">
                    <tr>
                        <th colspan="6">
                            &nbsp;DETAILS OF SECURITY CALL LETTER:&nbsp;
                        </th>
                    </tr>
                    <tr>
                        <td class="style35">
                            &nbsp;SL<br />
                        </td>
                        <td class="style23">
                            &nbsp; VISA
                        </td>
                        <td class="style23">
                            &nbsp; SECURITY CL
                        </td>
                        <td class="style41">
                            &nbsp; CL LETTER NO
                        </td>
                        <td class="style57">
                            &nbsp; SECURITY CL VALIDITY
                        </td>
                    </tr>
                    <tr>
                        <td class="style36">
                            &nbsp; 1
                        </td>
                        <td class=" style58">
                            &nbsp;
                            <asp:TextBox ID="txtVisa" ClientIDMode="Static" CssClass="txtView" runat="server"
                                Width="97px"></asp:TextBox>
                        </td>
                        <td class=" style58">
                            &nbsp;
                            <asp:TextBox ID="txtSecCl" ClientIDMode="Static" CssClass="txtView" runat="server"
                                Width="97px"></asp:TextBox>
                        </td>
                        <td class="style42">
                            &nbsp;
                            <asp:TextBox ID="txtCLletNo" runat="server" Width="136px"></asp:TextBox>
                        </td>
                        <td class="style27">
                            &nbsp;
                            <asp:TextBox ID="txtSecClValidity" runat="server" Width="172px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderSecClValidity" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtSecClValidity">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table class="tbl-border" border="1px solid" cellpadding="5px">
                    <tr>
                        <th colspan="6">
                            &nbsp;PARTICULARS OF ESCORTS:&nbsp;
                        </th>
                    </tr>
                    <tr>
                        <td class="style35">
                            &nbsp;SL<br />
                        </td>
                        <td class="style23">
                            &nbsp; T/P NO
                        </td>
                        <td class="style41">
                            &nbsp; RANK
                        </td>
                        <td class="style57">
                            &nbsp; NAME
                        </td>
                        <%--  <td class="style37">
                        &nbsp; I CARD NO
                    </td>--%>
                        <td>
                            &nbsp; DEPT/SHIP
                        </td>
                        <td class="style57">
                            &nbsp; MOBILE NUMBER
                        </td>
                    </tr>
                    <tr>
                        <td class="style36">
                            &nbsp; 1
                        </td>
                        <td class=" style58">
                            &nbsp;
                            <asp:TextBox ID="TxtEscorttoken" ClientIDMode="Static" CssClass="txtView" runat="server"
                                Width="97px" OnTextChanged="TxtEscorttoken_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td class="style42">
                            &nbsp;
                            <asp:TextBox ID="TxtEscortrank" runat="server" Width="136px"></asp:TextBox>
                        </td>
                        <td class="style27">
                            &nbsp;
                            <asp:TextBox ID="TxtEscortname" runat="server" Width="172px"></asp:TextBox>
                        </td>
                        <%--      <td class="style38">
                        &nbsp;
                        <asp:TextBox ID="TxtEscorticard" runat="server" Width="99px"></asp:TextBox>
                    </td>--%>
                        <td class="style26">
                            &nbsp;
                            <asp:TextBox ID="TxtEscortdept" runat="server"></asp:TextBox>
                        </td>
                        <td class="style26" valign="top">
                            <asp:TextBox ID="txtESMobile" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table class="tbl-border" border="1px solid" cellpadding="5px">
                    <tr>
                        <th colspan="6">
                            &nbsp;PARTICULARS OF RECEIVING OFFICER:&nbsp;
                        </th>
                    </tr>
                    <tr>
                        <td class="style35">
                            &nbsp; SL<br />
                        </td>
                        <td class="style23">
                            &nbsp; T/P NO
                        </td>
                        <td class="style41">
                            &nbsp; RANK
                        </td>
                        <td class="style57">
                            &nbsp; NAME
                        </td>
                        <td>
                            &nbsp; DEPT/SHIP
                        </td>
                        <%--  <td>
                    </td>
                    <td>
                    </td>--%>
                    </tr>
                    <tr>
                        <td class="style36">
                            &nbsp; 1
                        </td>
                        <td class="style58">
                            &nbsp;
                            <asp:TextBox ID="txtofftoken" runat="server" Width="97px" OnTextChanged="txtofftoken_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td class="style42">
                            &nbsp;
                            <asp:TextBox ID="Txtofcrdesignation" runat="server" Width="136px"></asp:TextBox>
                        </td>
                        <td class="style27">
                            &nbsp;
                            <asp:TextBox ID="TxtOfcrname" runat="server" Width="143px"></asp:TextBox>
                        </td>
                        <td class="style26">
                            &nbsp;
                            <asp:TextBox ID="txtdeptship" runat="server"></asp:TextBox>
                        </td>
                        <%--  <td class="style38">
                        &nbsp;
                    </td>
                    <td class="style26">
                        &nbsp;
                    </td>--%>
                    </tr>
                    <%--  <tr>
                    <td class="style24" colspan="6">
                        &nbsp;
                    </td>
                </tr>--%>
                </table>
            </center>
            <br />
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblfile" Text="CASUAL VISITOR APPLICATION LIST : "
                                Font-Bold="true" CssClass="style187"></asp:Label>
                        </td>
                        <td class="style176" colspan="4">
                            <asp:FileUpload runat="server" ID="flileupload" />
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <div align="center" style="padding-right: 80px" width="150px" height="30px">
                    <asp:Button ID="btnsubmit" runat="server" Text="FORWARD" Font-Bold="true" OnClick="btnsubmit_Click"
                        CssClass="button" />
                    <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="button" Font-Bold="true"
                        Visible="false" />
                    <asp:Button ID="BtnCancel" runat="server" Text="CANCEL" CssClass="button" Font-Bold="true"
                        OnClick="BtnCancel_Click" />
            </center>
        </div>
    </div>
    </div>
</asp:Content>
