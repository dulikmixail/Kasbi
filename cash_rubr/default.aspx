<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports._Default1" CodeFile="Default.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[Отчеты]</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Выбор рубрик для кассового аппарата&nbsp;</td>
				</tr>
			</table>
			<table width="100%" border="0" cellpadding="2" cellspacing="1">
				<tr>
					<td class="Unit" vAlign="top">Рубрики</td>
				</tr>
				<tr>
					<td class="SectionRow">
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
                                        <asp:CheckBox ID="cbxSelect" Checked="False" runat="server" Text=""></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
					</td>
				</tr>

				<TR>
					<td class="Unit" align="center" colSpan="2" height="19">
						<asp:imagebutton id="cmdCancel" runat="server" ImageUrl="../Images/cancel.gif" CommandName="Cancel"
							CausesValidation="False" OnClientClick="javascript:window.close()"></asp:imagebutton>&nbsp;&nbsp;
						<asp:imagebutton id="cmdEdit" runat="server" ImageUrl="../Images/update.gif" CommandName="Edit"></asp:imagebutton></td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>

