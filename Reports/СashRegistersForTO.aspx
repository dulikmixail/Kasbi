<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.CashRegistersForTO" Culture="ru-RU" CodeFile="CashRegistersForTO.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1" runat ="server">
	<title >[Отчет по продажам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../styles.css" type="text/css" rel="stylesheet">

		<script type="text/javascript" src="../scripts/datepicker.js"></script>
        <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
        <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
        <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />

	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
            <table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt; Отчет по кассовым аппратам, которые находятся на ТО</td>
				</tr>
			</table>

        <asp:ListBox runat="server" ID="lstCashRegister" Rows="5" Width="250px" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true"/>
        <asp:Button runat="server" ID="Submit" Text="Показать"/>

        <asp:DataGrid runat="server" ID="grdReport">
            <ItemStyle CssClass="itemGrid"></ItemStyle>
            <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"/>
            <FooterStyle CssClass="footerGrid"></FooterStyle>
            <Columns>
                <asp:TemplateColumn HeaderText="№">
                 <ItemTemplate>
                     <%#DataBinder.Eval(Container, "DataItem.rekvisit")%>
                 </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="№">
                 <ItemTemplate>
                     <%#DataBinder.Eval(Container, "DataItem.rekvisit")%>
                 </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="№">
                 <ItemTemplate>
                     <%#DataBinder.Eval(Container, "DataItem.rekvisit")%>
                 </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            
        </asp:DataGrid>

			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" ShowMessageBox="True"
				ShowSummary="False" HeaderText="Заполните обязательные поля :"></asp:validationsummary>

		</form>
	</body>
</HTML>
