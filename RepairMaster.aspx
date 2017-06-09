<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.RepairMaster" Culture="ru-RU"
    CodeFile="RepairMaster.aspx.vb" %>

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
                    &nbsp;Ремонт кассовых аппаратов&nbsp;</td>
            </tr>
        </table>
        <table style="font-size:12px" id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Критерий&nbsp;поиска&nbsp;ККМ</td>
            </tr>
 
             <tr>
                <td width="100%">
                
                <table style="font-size:12px">
                    <tr>
                        <td>Номер кассового аппарата:</td>
                        <td><asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13" Width="150px"></asp:TextBox></td>
                        <td><asp:LinkButton ID="lnkFind" runat="server" CssClass="LinkButton">&nbsp;Найти&nbsp;</asp:LinkButton></td>
                        <td width="500" align="right"><asp:LinkButton ID="lnkFindRepair" runat="server" CssClass="LinkButton">&nbsp;Показать аппараты, которые были в ремонте&nbsp;</asp:LinkButton></td>
                    </tr>
                </table>
                <br />
                
                
                </td>
            </tr>           
            
             <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Найдено по кассовому оборудованию</td>
            </tr>
            <tr>
                <td>
                <br />
                     <asp:DataGrid ID="grdRepair"  runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px"> 
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            
                            <asp:TemplateColumn HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumGood" EnableViewState="true" ForeColor="#9C0001" runat="server" ></asp:Label>
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
                            <asp:TemplateColumn HeaderText="Операции">
                                <ItemTemplate>
                                 
                                 
                                    
                                   <asp:LinkButton ID="lnkSetRepair" runat="server">Принять&nbsp;в&nbsp;ремонт<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkOutRepair" runat="server">Отдать&nbsp;владельцу<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkEditRepair" runat="server">Редактировать&nbsp;ремонт<br /> </asp:LinkButton>
                                   <asp:LinkButton ID="lnkStatus" runat="server">История&nbsp;ремонтов<br /> </asp:LinkButton>
                                    
                                    
                                    
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последнее ТО" SortExpression="repair">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последний ремонт" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblCto_master" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.cto_master")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>                            
                        </Columns>
                    </asp:DataGrid>
                                    
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
