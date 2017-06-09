<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.RubrRequest" Culture="ru-RU" CodeFile="RubrRequest.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[Отчет по ремонтам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/datepicker.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class=PageTitle cellSpacing=1 cellPadding=2 width="100%" border=0>
				<tr>
					<td class=HeaderTitle width="100%">&nbsp;Отчеты&nbsp;-&gt; 
					    Статистика по рубрикам</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="12pt">Label</asp:label></TD>
				</TR>
				<TR>
					<TD>


                       <asp:DataGrid ID="grdGoodGroups" runat="server" PageSize="200" 
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" BorderColor="#CC9966"
                            BorderWidth="1px">
                            <ItemStyle CssClass="itemGrid"></ItemStyle>
                            <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Name">
                                    <HeaderTemplate>
                                        Наименование
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRubr" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Количество ККМ
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNumcash" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Количество клиентов
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNumclient" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        
					</TD>
				</TR>
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD class ="Unit" align="center" >
						<asp:ImageButton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:ImageButton>
					</TD>
				</TR>
			</TABLE>
			<uc1:footer id=Footer1 runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" HeaderText="Заполните обязательные поля :"
				ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>
