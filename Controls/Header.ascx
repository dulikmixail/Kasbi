<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kasbi.Controls.Header" CodeFile="Header.ascx.vb" %>
<table cellpadding="0" border="0" cellspacing="0" width="100%" height="100%" style="font-size:12px">
	<tr>
		<td height="61">
			<table id="header" border="0" cellSpacing="0" cellPadding="2" width="100%" height="100%" style="font-size:12px">
				<tr>
					<td  width="100%" class="HeaderPage">
					    <asp:Label ID="lblUser" Runat="server" Font-Size="10px" ForeColor="#ffffff" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:LinkButton id="lnkExitProgram" runat="server" Font-Bold="True" ForeColor="White" Font-Size="10px">Выйти из программы</asp:LinkButton></td>
				</tr>
				<tr>
					<td align="right" width="100%" bgColor="#d3c9c7">

						<font style="FONT-WEIGHT: bold; FONT-SIZE: 18pt; COLOR: white; display:none"><asp:Label ID =lblCopmpany runat =server ></asp:Label></font>
					</td>
				</tr>
				<tr>
					<td bgColor="#d3c9c7">
						<table id ="mnuMainMenu"  runat ="server" cellSpacing="0" cellPadding="0" style="font-size:12px" width="100%">
							<tr>
								<td align="center" style="padding:5px"><asp:hyperlink id="btnMain" runat="server" CssClass="PageSelector" NavigateUrl="~/Default.aspx"> Главная </asp:hyperlink></td><td style="color:#9c0001">|</td>
								<td align="center" visible="false" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnMarketing" runat="server" CssClass="PageSelector" NavigateUrl="~/Marketing.aspx"> Маркетинг </asp:hyperlink></span></td><td visible="false" style="color:#9c0001">|</td>								
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnCustomers" runat="server" CssClass="PageSelector" NavigateUrl="~/CustomerList.aspx"> Клиенты </asp:hyperlink></span></td><td style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnCTO" runat="server" CssClass="PageSelector" NavigateUrl="~/CTOList.aspx"> Дилеры </asp:hyperlink></span></td><td style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnCatalog" runat="server" CssClass="PageSelector" NavigateUrl="~/GoodList.aspx"> Товары </asp:hyperlink></span></td><td style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnTO" runat="server" CssClass="PageSelector" NavigateUrl="~/MasterCTO.aspx"> МастерЦТО </asp:hyperlink></span></td><td  style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnRepairMaster" runat="server" CssClass="PageSelector" NavigateUrl="~/RepairMaster.aspx"> Мастер-Ремонт </asp:hyperlink></span></td><td style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnMsater" runat="server" CssClass="PageSelector" NavigateUrl="~/InternalCTO.aspx"> ЦТО"Рамок" </asp:hyperlink></span></td><td style="color:#9c0001"></td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnRepairs" runat="server" CssClass="PageSelector" NavigateUrl="~/RepairList.aspx"> Ремонт </asp:hyperlink></span></td><td style="color:#9c0001"></td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnReport" runat="server" CssClass="PageSelector" NavigateUrl="~/Reports/default.aspx"> Отчеты </asp:hyperlink></span></td><td style="color:#9c0001">|</td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnAdmin" runat="server" CssClass="PageSelector" NavigateUrl="~/Admin/default.aspx"> Администрирование </asp:hyperlink></span></td><td style="color:#9c0001"></td>
								<td align="center" style="padding:5px"><span class="delimiter"><asp:hyperlink id="btnGoodprice" runat="server" CssClass="PageSelector" NavigateUrl="~/Admin/Goodprice.aspx"></asp:hyperlink></span></td>
                               							
							</tr>
						</table>
						<table id ="mnuExternalUser"  runat ="server" Visible=false cellSpacing="0" cellPadding="0" >
							<tr>
								<td>&nbsp;<asp:hyperlink id="btnExtMain" runat="server" CssClass="PageSelector" NavigateUrl="~/Default.aspx">Главная&nbsp;</asp:hyperlink></td>
								<td><span class="delimiter">|&nbsp;<asp:hyperlink id="btnExtCustomers" runat="server" CssClass="PageSelector" NavigateUrl="~/CustomerList.aspx">Клиенты&nbsp;</asp:hyperlink></span></td>
								<td><span class="delimiter">|&nbsp;<asp:hyperlink id="btnExtCatalog" runat="server" CssClass="PageSelector" NavigateUrl="~/GoodList.aspx">ККМ&nbsp;</asp:hyperlink>&nbsp;</span></td>
								<td><span class="delimiter">|&nbsp;<asp:hyperlink id="btnExtTO" runat="server" CssClass="PageSelector" NavigateUrl="~/InternalCTO.aspx">Техническое обслуживание</asp:hyperlink>&nbsp;</span></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr >
		<td  valign="top" width="100%" height="100%">

