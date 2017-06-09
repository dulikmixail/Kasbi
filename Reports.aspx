<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports" EnableSessionState="False" enableViewState="False" CodeFile="Reports.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
    <title>[Reports]</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="frmReports" method="post" runat="server">
			<asp:label id="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label><br>
			<table style='FONT-FAMILY: Verdana'>
				<tr>
					<td colspan="2" align="middle"><asp:Label id="lblTitle" runat="server" Font-Size="10pt"></asp:Label></td>
				</tr>
				<asp:Literal id="table" runat="server"></asp:Literal>
			</table>
		</form>
	</body>
</HTML>
