<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.IMNS" CodeFile="IMNS.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
    <title>[ИМНС]</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Администрирование -&gt; Налоговые 
						инспекции</td>
				</tr>
				<tr>
					<td height="10">
						<asp:label id="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:label></td>
				</tr>
				<tr>
					<td class="SectionRow" height="10" align="center">&nbsp;
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="300" border="0">
							<TR>
								<TD class="TitleTextbox" style="HEIGHT: 21px">Фильтр:</TD>
								<TD style="HEIGHT: 21px">
									<asp:TextBox id="txtFilter" runat="server" Width="250px" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox></TD>
								<TD style="HEIGHT: 21px">
									<asp:LinkButton id="lnkShow" runat="server" Font-Size="8pt">Показать</asp:LinkButton></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td height="10"></td>
				</tr>
				<tr>
					<td align="center">
						<asp:datagrid id="grdIMNS" runat="server" ShowFooter="True" AutoGenerateColumns="False" 
							Width="75%" PageSize="100" AllowPaging="True" BorderColor="#CC9966" BorderWidth="1px">
							<AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Наименование">
									<ItemTemplate>
										<%# DataBinder.Eval(Container, "DataItem.imns_name") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtIMNSName" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="450px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtIMNSUpdateName runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="450px" Text='<%# DataBinder.Eval(Container, "DataItem.imns_name") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<FooterStyle HorizontalAlign="Center" Width="30"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ToolTip="Изменить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ToolTip="Удалить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="cmdUpdate" runat="server" CommandName="Update" ToolTip="Сохранить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdCancel" runat="server" CommandName="Cancel" ToolTip="Отменить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </EditItemTemplate>
									<FooterTemplate>
										&nbsp;&nbsp;
										<asp:LinkButton id="btnAddIMNS" runat="server" Font-Size="8pt" CommandName="AddIMNS">Добавить</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td height="5">&nbsp;</td>
				</tr>
				<TR>
					<TD class="Unit" align="center" colspan="2">
						<asp:imagebutton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:imagebutton>
					</TD>
				</TR>
				<tr>
					<td height="5">&nbsp;</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
