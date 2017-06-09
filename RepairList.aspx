<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.RepairList" Culture="ru-RU"
    CodeFile="RepairList.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Рамок - [Ремонт]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>

    <script language="javascript">
		function isFind(s)
		{
			var theform = document.frmRepairList;
			theform.FindHidden.value = s;
		}
    </script>

</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmRepairList" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Ремонт&nbsp;</td>
            </tr>
        </table>
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Критерий&nbsp;поиска&nbsp;ККМ</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msgCashregister" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="SectionRow">
                                <asp:ListBox ID="lstCashType" runat="server" SelectionMode="Multiple" Rows="4"></asp:ListBox></td>
                            <td class="SectionRowLabel" style="font-size: 7pt">
                                <table style="font-size: 7pt">
                                    <tr>
                                        <td nowrap>
                                            <asp:LinkButton ID="btnFindGood" runat="server" EnableViewState="False" CssClass="PanelHider">Найти</asp:LinkButton>
                                            &nbsp;&nbsp;<i>№</i></td>
                                        <td>
                                            <asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <i>СК&nbsp;ЦТО</i></td>
                                        <td>
                                            <asp:TextBox ID="txtFindGoodCTO" runat="server" BorderWidth="1px" Height="18px" MaxLength="11"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <i>СК&nbsp;Изготовителя</i></td>
                                        <td>
                                            <asp:TextBox ID="txtFindGoodManufacturer" runat="server" BorderWidth="1px" Height="18px"
                                                MaxLength="11" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                       <td colspan="2" >
                                            <asp:LinkButton ID="lnkGarantia" runat="server" CssClass="LinkButton">
                                                <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                                    position: relative; left: 10;"></asp:Image>&nbsp;Гарантийный&nbsp;ремонт&nbsp;</asp:LinkButton>
                                        </td>
                                        <td colspan="2" >
                                            <asp:LinkButton ID="lnkAllRepair" runat="server" CssClass="LinkButton">
                                                <asp:Image runat="server" ID="Image4" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                                    position: relative; left: 10;"></asp:Image>
                                                &nbsp;ККМ находящиеся в ремонте</asp:LinkButton></td>
                                        <td colspan="2" >
                                            <asp:LinkButton ID="lnkAllRepaired" runat="server" CssClass="LinkButton">
                                                <asp:Image runat="server" ID="Image3" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                                    position: relative; left: 10;"></asp:Image>
                                                &nbsp;Ремонты за весь срок эксплуатации</asp:LinkButton>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td class="SectionRowLabel">
                                            <asp:Label ID="Label1" runat="server">Начальная дата:</asp:Label></td>
                                        <td class="SectionRow">
                                            <asp:TextBox ID="tbxBeginDate" BorderWidth="1px" runat="server"></asp:TextBox><a
                                                href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                                    src="Images/cal_date_picker.gif" border="0"></a><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                        runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата" ControlToValidate="tbxBeginDate">*</asp:RequiredFieldValidator>&nbsp;<asp:Label
                                                            ID="lblDateFormat2" runat="server" CssClass="text02"></asp:Label>
                                            <asp:CompareValidator ID="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
                                                EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:CompareValidator></td>
                                        <td class="SectionRowLabel" style="width: 127px">
                                            <asp:Label ID="Label3" runat="server">Конечная дата:</asp:Label></td>
                                        <td class="SectionRow">
                                            <asp:TextBox ID="tbxEndDate" BorderWidth="1px" runat="server"></asp:TextBox><a href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><img
                                                alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата "
                                                    ControlToValidate="tbxEndDate">*</asp:RequiredFieldValidator>&nbsp;<asp:Label ID="lblDateFormat3"
                                                        runat="server" CssClass="text02"></asp:Label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="ErrorMessage"
                                                ControlToValidate="tbxEndDate" EnableClientScript="False" Display="Dynamic" Type="Date"
                                                Operator="DataTypeCheck">Пожалуйста, введите корректные значение конечной даты</asp:CompareValidator></td>
                                        <td class="SectionRow">
                                            <p>
                                                <asp:LinkButton ID="lnkRepairs" runat="server" CssClass="LinkButton">
                                                    <asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                                        position: relative; left: 10;"></asp:Image>
                                                    &nbsp;Ремонт&nbsp;проводился&nbsp;</asp:LinkButton></p>
                                        </td>
                                    </tr>
                                </table>
                                <hr>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" colspan="2" height="5">
                                <asp:Label ID="lblFilterCaption" runat="server" Font-Size="8pt"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="SectionRow" colspan="2" height="5">
                                <asp:Label ID="lblRecordCount" runat="server" CssClass="SubTitleTextbox"></asp:Label></td>
                        </tr>
                        <tr class="Unit">
                            <td class="Unit" colspan="2">
                                &nbsp;Результат&nbsp;поиска&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:DataGrid ID="grdRepair" BorderWidth="1px" Width="98%" BorderColor="#CC9966"
                                    CellPadding="1" AllowSorting="True" AutoGenerateColumns="False" runat="server">
                                    <ItemStyle CssClass="itemGrid"></ItemStyle>
                                    <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                                    <FooterStyle CssClass="footerGrid"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="№">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumGood" ForeColor="#9C0001" EnableViewState="true" runat="server"
                                                    Font-Size="7pt"></asp:Label>
                                                <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="payerdogovor" HeaderText="№ дог">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDogovor" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <p class="SubTitleEditbox">
                                                    № дог:</p>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="payerInfo" HeaderText="Владелец">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGoodOwner" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="good_name" HeaderText="Товар">
                                            <ItemTemplate>
                                                <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="num_cashregister" HeaderText="№">
                                            <HeaderStyle Font-Underline="True"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbledtNum" runat="server" NavigateUrl='<%# "CashOwners.aspx?" & DataBinder.Eval(Container, "DataItem.good_sys_id") & "&cashowner="& DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'>
                                                </asp:HyperLink>
                                                <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                                    <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                                    <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ToolTip="На техобслуживании"
                                                        ImageUrl="Images/support.gif"> 
                                                    </asp:HyperLink>
                                                    <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                                        ToolTip="В ремонте" ImageUrl="Images/repair.gif">
                                                    </asp:HyperLink>
                                                    <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                                        ToolTip="Побывал в ремонте" ImageUrl="Images/repaired.gif">
                                                    </asp:HyperLink></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="num_control_cto" HeaderText="№ СК изг./ЦТО">
                                            <HeaderStyle Font-Underline="True"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbledtControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_mfp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Баланс">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn SortExpression="repairdate_in" HeaderText="Даты ремонта">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRepairDates" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Состояние ТО">
                                            <ItemTemplate>
                                                <p>
                                                    <asp:HyperLink ID="lnkStatus" runat="server" NavigateUrl='<%# "NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'>
                                                    </asp:HyperLink>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Последнее ТО">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid></td>
                        </tr>
                        <tr height="10">
                            <td width="100%" colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>
