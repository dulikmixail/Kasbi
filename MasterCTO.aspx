<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.MasterCTO" CodeFile="MasterCTO.aspx.vb" %>

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
    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmInternalCTO" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">&nbsp;ЦТО&nbsp; Мастера "Рамок"&nbsp; </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="2" width="100%" border="0">
            <tr class="Unit" style="display:none">
                <td class="Unit" width="100%"><asp:hyperlink ID="lnkAddSupport" runat="server" CssClass="LinkButton" NavigateUrl="~/AddTO.aspx">&nbsp;&nbsp;&nbsp;Добавить оборудование на ТО</asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:CheckBox ID="chk_show_kkm" EnableTheming="true" EnableViewState="true" runat="server" Checked="true" AutoPostBack="true" Text=" Кассовое оборудование" />&nbsp;
                      <asp:CheckBox ID="chk_show_torg" EnableTheming="true" EnableViewState="true" runat="server" Checked="false" AutoPostBack="true" Text=" Прочее торговое оборудование" />                                
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:12px">
                    <asp:Label ID="msgCashregister" runat="server" EnableViewState="false" ForeColor="#ff0000" Font-Bold="true" Font-Size="14px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" style="font-size:12px">
                        <tr>
                            <td class="SectionRowLabel" valign="top">
                                   &nbsp;&nbsp;<i>№</i><br />
                                   <asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13"
                                   Width="150px"></asp:TextBox><br />
                                   <i>СК&nbsp;ЦТО</i><br />
                                   <asp:TextBox ID="txtFindGoodCTO" runat="server" BorderWidth="1px" Height="18px" MaxLength="11"
                                    Width="150px"></asp:TextBox><br />
                                   <i>СК&nbsp;Изготовителя</i><br />
                                   <asp:TextBox ID="txtFindGoodManufacturer" runat="server" BorderWidth="1px" Height="18px"
                                   MaxLength="11" Width="150px"></asp:TextBox><br />
                            </td>
                            <td>
                                  <asp:LinkButton ID="btnFindGood" runat="server" EnableViewState="False" CssClass="PanelHider"><div align="center">Показать свои<br /> аппараты</div></asp:LinkButton>
                            </td>                            
                            <td class="SectionRowLabel" valign="top">
                                   <i>Оборудование </i><br />
                                   <asp:ListBox ID="lstGoodType" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td> 
                            <td class="SectionRowLabel" valign="top">
                                    <asp:Label ID="lbl_otv_master" runat="server">
                                   <i>Ответственные мастера </i><br /></asp:Label>
                                   <asp:ListBox ID="lstEmployee" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Single" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td>                            
                            <td class="SectionRowLabel" valign="top">
                                   <i>Район установки</i><br />
                                   <asp:ListBox ID="lstPlaceRegion" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px" style="font-size:12">
                     <asp:LinkButton ID="lnkNeraspl" runat="server" CssClass="LinkButton">&nbsp;Показать нераспределенные&nbsp;</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkSetEmployee" cssClass="LinkButton" runat="server">Закрепить ККМ за мастером</asp:LinkButton> |
                <asp:Label ID="adm_panel" runat="server">
                     <asp:LinkButton ID="lnkRaspl" runat="server" CssClass="LinkButton">&nbsp;Показать распределенные&nbsp;</asp:LinkButton> | 
                     <asp:LinkButton ID="lnk_show_no_comfirmed" runat="server" CssClass="LinkButton">&nbsp;Показать неподтвержденные&nbsp;</asp:LinkButton> |                                     
                     <asp:LinkButton Visible="false" ID="lnkNotTO" runat="server" CssClass="LinkButton">&nbsp;Не обслуживаемые последние 3 месяца</asp:LinkButton>   
                     <asp:LinkButton ID="lnkDelTO" cssClass="LinkButton" runat="server">&nbsp;Отменить последний ТО</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkConfirmEmployee" cssClass="LinkButton" runat="server">&nbsp;Подтвердить ККМ мастера</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkDelEmployee" cssClass="LinkButton" runat="server">&nbsp;Снять ККМ с мастера</asp:LinkButton> |                    
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                       <div style="background-color:Silver; width:600; padding:5px; font-size:12; padding:8px">
                                <b>Фильтр:&nbsp;</b>
                                 
                                <asp:LinkButton ID="lnkConduct" runat="server" CssClass="LinkButton">
                                    &nbsp;ТО&nbsp;проведено&nbsp;</asp:LinkButton>&nbsp;|&nbsp;
                                
                                <asp:LinkButton ID="lnkNotConduct" runat="server" CssClass="LinkButton">
                                    &nbsp;ТО&nbsp;не&nbsp;проведено&nbsp;</asp:LinkButton>&nbsp;|&nbsp;                      
                     
                                <asp:LinkButton ID="lnkBlackList" runat="server" CssClass="LinkButton" Visible="False">
                                    &nbsp;Черный&nbsp;список&nbsp;&nbsp;|&nbsp;  </asp:LinkButton>                   
                     
                                <asp:LinkButton ID="lnk_onTO" runat="server" CssClass="LinkButton">
                                    &nbsp;На ТО&nbsp;</asp:LinkButton> &nbsp;|&nbsp;  
                                    
                                <asp:LinkButton ID="lnk_stopTO" runat="server" CssClass="LinkButton">
                                    &nbsp;ТО приостановлено&nbsp;</asp:LinkButton> &nbsp;|&nbsp;  
                                    
                                <asp:LinkButton ID="lnk_delTO" runat="server" CssClass="LinkButton">
                                    &nbsp;Снято с ТО&nbsp;</asp:LinkButton>                                                                                               
                      </div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" height="15" valign="top" style="padding:5px; font-size:12px">
                <div style="background-color:Silver; width:980; padding:5px">                         
                <b>Укажите&nbsp;дату ТО:&nbsp;</b><asp:textbox id="tbxCloseDate" Runat="server" BorderWidth="1px"></asp:textbox>
                    <%--<A href="javascript:showdatepicker('tbxCloseDate', 0, false,'MM.DD.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                <asp:DropDownList ID="lstMonth" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
                                    <asp:ListItem Value="01">Январь</asp:ListItem>
                                    <asp:ListItem Value="02">Февраль</asp:ListItem>
                                    <asp:ListItem Value="03">Март</asp:ListItem>
                                    <asp:ListItem Value="04">Апрель</asp:ListItem>
                                    <asp:ListItem Value="05">Май</asp:ListItem>
                                    <asp:ListItem Value="06">Июнь</asp:ListItem>
                                    <asp:ListItem Value="07">Июль</asp:ListItem>
                                    <asp:ListItem Value="08">Август</asp:ListItem>
                                    <asp:ListItem Value="09">Сентябрь</asp:ListItem>
                                    <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                    <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                    <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                </asp:DropDownList>                      
                      
                                <asp:DropDownList  ID="lstYear" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
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
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                </asp:DropDownList>                  
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkSetTO" cssClass="LinkButton" runat="server">Провести ТО</asp:LinkButton>&nbsp;|&nbsp;
                <asp:LinkButton ID="LinkButton1" cssClass="LinkButton" runat="server">Сформировать док-ты</asp:LinkButton>&nbsp;|&nbsp;               
                <asp:LinkButton ID="lnkExportData" cssClass="LinkButton" runat="server">Экспорт в Excel</asp:LinkButton>&nbsp;|&nbsp;
                <asp:LinkButton ID="lnkSetRaon" cssClass="LinkButton" runat="server">Привязать к району</asp:LinkButton>            
                
                </div>   
                <br />
               
               </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">&nbsp;Найдено по кассовому оборудованию&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:DataGrid ID="grdTO"  runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px"> 
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Выбрать">
                                <HeaderTemplate>
                                    Выбрать<br>
                                    <asp:CheckBox ID="cbxSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="SelectAll">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                    <asp:Label Visible="false" ID="lblPayerId" Text='<%# DataBinder.Eval(Container, "DataItem.payer_sys_id") %>' runat="server"></asp:Label>
                                    <asp:Label Visible="false" ID="lblGoodType" Text='<%# DataBinder.Eval(Container, "DataItem.good_type_sys_id") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink Target="_blank" id="lblNumGood" runat="server" ForeColor="black"></asp:HyperLink>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
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
                                    <asp:HyperLink ID="lbledtNum" Target="_blank" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                         ToolTip="На техобслуживании">
                                        </asp:HyperLink>
                                        
                                        <asp:HyperLink ID="imgRepair" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
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
                            <asp:TemplateColumn HeaderText="Место установки" SortExpression="place_rn_id"> 
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
                            <asp:TemplateColumn HeaderText="Долг" SortExpression="dolg"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ТО">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" Target="_blank" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id")) %>'>
                                        </asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последнее ТО" SortExpression="lastTO">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ответственный" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblCto_master" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.cto_master")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>                            
                        </Columns>
                    </asp:DataGrid>
                    
                                    
                
                </td>
            </tr>

            <tr class="Unit" style="display:none">
                <td class="Unit" colspan="2">&nbsp;Найдено по торговому оборудованию&nbsp;</td>
            </tr>            
            <tr>
                <td align="center" colspan="2">
                
                    <asp:DataGrid ID="grdTO_prod" BorderWidth="1px" Width="100%" runat="server" CellPadding="1"
                        AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CC9966" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Выбрать">
                                <HeaderTemplate>
                                    Выбрать<br>
                                    <asp:CheckBox ID="cbxSelectAll" runat="server" AutoPostBack="True"  OnCheckedChanged="SelectAll_prod">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumGood" EnableViewState="true" ForeColor="#9C0001" runat="server" ></asp:Label>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Плательщик / Владелец" SortExpression="payerInfo"> 
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodOwner" runat="server"  Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Товар" SortExpression="good_name" ItemStyle-HorizontalAlign="center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_num") %>'
                                        NavigateUrl=''>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                            ToolTip="На техобслуживании">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.goodto_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.goodto_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="Побывал в ремонте">
                                        </asp:HyperLink></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Место установки" SortExpression="set_place" ItemStyle-HorizontalAlign="center"> 
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
                            <asp:TemplateColumn HeaderText="ТО" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" Target="_blank" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct2.aspx?" &amp; DataBinder.Eval(Container, "DataItem.goodto_sys_id")) %>'>Посмотреть</asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последнее ТО" ItemStyle-HorizontalAlign="center"  SortExpression="lastTO"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>                
                </td>
            </tr>  
            
                        
            <tr height="10">
                <td width="100%" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>

        <script type="text/javascript">

            jQuery(function () {

                jQuery('#tbxCloseDate').datetimepicker({
                    lang: 'ru',
                    timepicker: false,
                    format: 'd.m.Y',
                    closeOnDateSelect: true,
                    scrollMonth: false,
                });

            });

        </script>

        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>
