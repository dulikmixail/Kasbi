<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.InternalCTO" CodeFile="InternalCTO.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[ЦТО "Рамок"]</title>

    <script language="javascript">
		function isFind(s)
		{
			var theform = document.frmInternalCTO;
			theform.FindHidden.value = s;
		}
    </script>
<script language="javascript">
  function ExportToExcel()        
  {


//      alert("ExportToExcel");
//      var sHTML = window.document.getElementById("grdTO").outerhtml;
//      alert(sHTML);
//      var m_objExcel = new Excel.Application();
//        var m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
//var m_objBook = (Excel._Workbook)(m_objBooks.Add());
//m_objBook.HTMLProject.HTMLProjectItems("Sheet1").Text = sHTML;
//      m_objBook.HTMLProject.RefreshDocument();
//     m_objExcel.Visible = true;
//      m_objExcel.UserControl = true; 
////      var oXL = CreateObject("Excel.Application");
////      alert(oXL);
////      var oBook = oXL.Workbooks.Add();
////      alert(oBook);
////      oBook.HTMLProject.HTMLProjectItems("Sheet1").Text = sHTML;
////      oBook.HTMLProject.RefreshDocument;
////      oXL.Visible = true;
////      oXL.UserControl = true;
//	
}
</script>

    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmInternalCTO" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;ЦТО&nbsp;"Рамок"&nbsp;</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" width="100%">&nbsp;Критерий&nbsp;поиска&nbsp;ККМ</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msgCashregister" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="SectionRow" >
                                <asp:ListBox ID="lstCashType" runat="server" SelectionMode="Multiple" Rows="4"></asp:ListBox></td>
                            <td class="SectionRowLabel" colspan="2">
                                <table style="font-size: 7pt">
                                    <tr >
                                        <td align =right nowrap>
                                            <asp:LinkButton ID="btnFindGood" runat="server" EnableViewState="False" CssClass="PanelHider">Найти</asp:LinkButton>
                                            &nbsp;&nbsp;<i>№</i></td>
                                        <td>
                                            <asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td align =right >
                                            <i>СК&nbsp;ЦТО</i></td>
                                        <td>
                                            <asp:TextBox ID="txtFindGoodCTO" runat="server" BorderWidth="1px" Height="18px" MaxLength="11"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td align =right >
                                            <i>СК&nbsp;Изготовителя</i></td>
                                        <td >
                                            <asp:TextBox ID="txtFindGoodManufacturer" runat="server" BorderWidth="1px" Height="18px"
                                                MaxLength="11" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr  >
                                        <td align =right  >
                                            <i>Место&nbsp;установки</i></td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtFindGoodSetPlace" runat="server" BorderWidth="1px" Height="18px"
                                                MaxLength="200" Width="100%"></asp:TextBox>
                                        </td >
                                        <td align =right  rowspan =2>
                                            <i>Район </i>
                                        </td>
                                        <td rowspan =2>
                                            <asp:ListBox ID="lstPlaceRegion" runat="server" BorderWidth="1px" Rows =3
                                                Width="208px" SelectionMode="Multiple">
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align =right >
                                            <i>Клиенты</i></td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtFindCustomer" runat="server" BorderWidth="1px" Height="18px"
                                                MaxLength="200" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr class="TitleTextbox">
                            <td class="SubTitleTextbox">
                                &nbsp;</td>
                            <td class="SubTitleTextbox" align="center">
                                <asp:Label ID="Label2" runat="server">Месяц</asp:Label>
                            </td>
                            <td class="SubTitleTextbox"align="center">
                                <asp:Label ID="Label1" runat="server">Год</asp:Label></td>
                            <td class="SubTitleTextbox" colspan ="7" >
                            </td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            <td class="SectionRow" >
                                Выберите&nbsp;период</td>
                            <td class="SectionRow">
                                <asp:DropDownList ID="lstMonth" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
                                    <asp:ListItem Value="1">Январь</asp:ListItem>
                                    <asp:ListItem Value="2">Февраль</asp:ListItem>
                                    <asp:ListItem Value="3">Март</asp:ListItem>
                                    <asp:ListItem Value="4">Апрель</asp:ListItem>
                                    <asp:ListItem Value="5">Май</asp:ListItem>
                                    <asp:ListItem Value="6">Июнь</asp:ListItem>
                                    <asp:ListItem Value="7">Июль</asp:ListItem>
                                    <asp:ListItem Value="8">Август</asp:ListItem>
                                    <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                    <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                    <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                    <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                           <td class="SectionRow">
                                <asp:DropDownList ID="lstYear" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
                                    <asp:ListItem Value="2003">2003</asp:ListItem>
                                    <asp:ListItem Value="2004">2004</asp:ListItem>
                                    <asp:ListItem Value="2005">2005</asp:ListItem>
                                    <asp:ListItem Value="2006">2006</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkConduct" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;ТО&nbsp;проведено&nbsp;</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkNotConduct" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;ТО&nbsp;не&nbsp;проведено&nbsp;</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkSupport" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image7" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;Поставленные&nbsp;на&nbsp;ТО</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkDismissal" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image3" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;Снятые&nbsp;с&nbsp;ТО</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkDelay" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image4" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;ТО&nbsp;приостановлено&nbsp;</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkBlackList" runat="server" CssClass="LinkButton" Visible="False">
                                    <asp:Image runat="server" ID="Image5" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;Черный&nbsp;список&nbsp;</asp:LinkButton>
                            </td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkRoute" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image6" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;Маршруты&nbsp;</asp:LinkButton></td>
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
                    <asp:Label ID="lblRecordCount" runat="server" Font-Size="8pt"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" colspan="2" height="15">
                <asp:LinkButton ID="lnkSelectKKM" cssClass="LinkButton" runat="server" >Выбрать ККМ</asp:LinkButton>
                <asp:LinkButton ID="lnkExportData" cssClass="LinkButton" Visible="false"  runat="server" >Экспортировать данные в Excel</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkRouteRequest" runat="server" CssClass="LinkButton">Сформировать по ККМ</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton
                        ID="lnkRouteRegionRequest" runat="server" CssClass="LinkButton">Сформировать по районам</asp:LinkButton></td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">&nbsp;Результат&nbsp;поиска&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:DataGrid ID="grdTO" BorderWidth="1px" Width="98%" runat="server" CellPadding="1"
                        AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CC9966">
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn Visible="False" HeaderText="Выбрать">
                                <HeaderTemplate>
                                    Выбрать<br>
                                    <asp:CheckBox ID="cbxSelectAll" runat="server" Text="Все" AutoPostBack="True" OnCheckedChanged="SelectAll">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumGood" EnableViewState="true" ForeColor="#9C0001" runat="server" ></asp:Label>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
  
                            <asp:TemplateColumn HeaderText="№ дог" SortExpression="payerdogovor">
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblDogovor" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Плательщик / Владелец" SortExpression="payerInfo"> 
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodOwner" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Товар" SortExpression="good_name" > 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                            ToolTip="На техобслуживании">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="Побывал в ремонте">
                                        </asp:HyperLink></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№ СК изг./ЦТО" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_mfp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Место установки" SortExpression="set_place"> 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtPlace" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.set_place")%>'>
                                    </asp:Label>
                                    <br>
                                    <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='Район установки:'></asp:Label>
                                    <b>
                                        <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region")%>'>
                                        </asp:Label></b>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Баланс" SortExpression="dolg"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ТО">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id")) %>'>
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
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>
