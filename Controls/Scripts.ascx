<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kasbi.Controls.Scripts" CodeFile="Scripts.ascx.vb" %>
<script language="javascript">
<!--
//Основная функция, которая сабмитит форму (перенаправление со страницы на страницу)
	function ChangePage(s1,s2)
	{
		var theform = document.frmMain;
		theform.CurrentPage.value = s1;
		theform.Parameters.value = s2;
		
		theform.submit();
	}
function Exec() 
	{ 
		var WshShell = new ActiveXObject( "WScript.Shell" ); 
		var oExec = WshShell.Run( "notepad.exe", 3, true ); 
	} 
function isFind()
	{
		var theform = document.frmMain;
		theform.FindHidden.value = "1";
	}
-->
</script>
