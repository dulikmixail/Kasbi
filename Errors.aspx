<%@ Page Language="vb"  AutoEventWireup="false" Inherits="Kasbi.Errors" CodeFile="Errors.aspx.vb"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<head  runat ="server">
    <title>[Ошибка]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>

</head>
	<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="ErrorTable" align="center" cellSpacing="1" cellPadding="2" width="60%" border="0"
				style="HEIGHT: 65px">
				<TR>
					<TD align="center">
						<asp:Label id="lblErrorHeader" runat="server" Font-Bold="True">ErrorHeader</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center"></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Label id="lblErrorMessage" runat="server">ErrorMessage</asp:Label></TD>
				</TR>
				<TR>
					<TD align="center"></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:HyperLink id="lnkBack" runat="server">Back</asp:HyperLink></TD>
				</TR>
				<TR>
					<TD align="center"></TD>
				</TR>
			</TABLE>
		</form>

	</body>
</HTML>
