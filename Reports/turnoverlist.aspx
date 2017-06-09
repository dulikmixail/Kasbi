<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.TurnoverList" CodeFile="TurnoverList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
		<head   runat ="server">
	<title >[Оборотная ведомость]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="HeaderTitle" width="100%">&nbsp;Отчеты -&gt; Оборотная ведомость</td>
					</tr>
					<tr>
						<td height="10"><asp:label id="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label></td>
					</tr>
					<TR>
						<TD>
							<table id="table" width="90%" align="center">
								<TR>
									<TD class="SectionRowLabel" align="right">Группы товаров:</TD>
									<TD class="SectionRow"><asp:dropdownlist id="lstGoodGroup" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="350px"
											AutoPostBack="True"></asp:dropdownlist></TD>
									<!--<td class="SectionRow"><asp:linkbutton id="lnkRestReport" runat="server" CssClass="LinkButton">
						<asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>
						&nbsp;Экспорт данных в Excel&nbsp;</asp:linkbutton></td>-->
								</TR>
							</table>
						</TD>
					</TR>
					<!--<tr>
						<td class="SectionRow" height="10" align="center">&nbsp;
							<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="300" border="0">
								<TR>
									<TD class="TitleTextbox" style="HEIGHT: 21px">Товар:</TD>
									<TD style="HEIGHT: 21px">
										<asp:TextBox id="txtFilter" runat="server" Width="250px" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox></TD>
									<TD style="HEIGHT: 21px">
										<asp:LinkButton id="LinkButton1" runat="server" Font-Size="8pt">Показать</asp:LinkButton></TD>
								</TR>
							</TABLE>
						</td>
					</tr> -->
					<tr>
						<td height="10"></td>
					</tr>
					<TR>
						<td class="SectionRow" colSpan="2"><asp:repeater id="repTurnoverList" runat="server">
								<ItemTemplate>
									<asp:Panel id="GroupSection" runat="server">
										<tr>
											<td colspan="6" style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: gray; TEXT-ALIGN: center; ">
												<asp:Label id="lblGoodGroupName" runat="server"></asp:Label></td>
										</tr>
									</asp:Panel>
									<tr style="FONT-SIZE: 9pt; TEXT-ALIGN: center; ">
										<td><asp:Label id="lblRecordCount" runat="server"></asp:Label></td>
										<td align="left"><asp:HyperLink id="lnkGood" runat="server">
												<%# DataBinder.Eval(Container.DataItem, "good_name") %>
											</asp:HyperLink></td>
										<!--<td class='SubCaption'><b><%# DataBinder.Eval(Container.DataItem, "prichod_Kol") %></b></td>
										<td class='SubCaption'><b><%# DataBinder.Eval(Container.DataItem, "rashod_Kol") %></b></td>
										<td class='SubCaption'><b><%# DataBinder.Eval(Container.DataItem, "Ostatok_Kol") %></b></td>-->
										<td><asp:Label id="lblPrichod" runat="server" CssClass='SubCaption' Font-Bold="True"></asp:Label></td>
										<td><asp:Label id="lblRashod" runat="server" CssClass='SubCaption' Font-Bold="True"></asp:Label></td>
										<td><asp:Label id="lblOstatok" runat="server" CssClass='SubCaption' Font-Bold="True"></asp:Label></td>
										<td class='SubCaption'><b><%# DataBinder.Eval(Container.DataItem, "RealOstatok_Kol") %></b></td>
									</tr>
								</ItemTemplate>
								<HeaderTemplate>
									<TABLE align="center" id="Table4" cellSpacing="0" cellPadding="0" width="90%" border=".1"
										BorderColor="#CC9966" style="border-color:#CC9966;border-width:1px;border-style:solid;font-family:Verdana;font-size:9pt;width:98%;border-collapse:collapse;">
										<tr style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: center; ">
											<td width="3%">№</td>
											<td width="62%"><font size="2">Наименование</font></td>
											<td width="15%"><font size="2">Приход</font></td>
											<td width="15%"><font size="2">Расход</font></td>
											<td width="15%"><font size="2">Остаток</font></td>
											<td width="15%"><font size="2">Реальный остаток</font></td>
										</tr>
								</HeaderTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:repeater></TD></TR></TR>
			<tr>
				<td height="5">&nbsp;</td>
			</tr>
			<TR>
				<TD class="Unit" align="center" colSpan="2"><asp:imagebutton id="btnBack" runat="server" CausesValidation="False" ImageUrl="../Images/back.gif"></asp:imagebutton></TD>
			</TR>
			<tr>
				<td height="5">&nbsp;</td>
			</tr>
			</TBODY></TABLE><uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
