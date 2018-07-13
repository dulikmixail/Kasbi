'===========================================================================
' This file was generated as part of an ASP.NET 2.0 Web project conversion.
' This code file 'App_Code\Migrated\Stub_Documents_aspx_vb.vb' was created and contains an abstract class 
' used as a base class for the class 'Migrated_Documents' in file 'Documents.aspx.vb'.
' This allows the the base class to be referenced by all code files in your project.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Imports Microsoft.Office.Interop


Namespace Kasbi

    Public MustInherit Class Documents
        Inherits PageBase

        Public MustOverride Function ProcessDocuments(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal sale As Integer, Optional ByVal rebill As Integer = 0, Optional ByVal sub_num As Integer = -1, Optional ByVal isRefresh As Boolean = True) As Boolean

        Public MustOverride Function Summa_propis(ByVal s As String, Optional ByVal b As Boolean = True, Optional ByVal b_cop As Boolean = True) As String

        Public MustOverride Function GetRussianDate(ByVal d As Date) As String

        Public MustOverride Function GetRussianDate1(ByVal d As Date) As String

        Public MustOverride Function GetRussianDate2(ByVal d As Date) As String

        Public MustOverride Function GetRussianDate3(ByVal d As Date) As String

        Public MustOverride Function ProcessSupportDocuments(ByVal doc_type As Integer, ByVal customer_sys_id_s As String, ByVal good_sys_id_s As String, Optional ByVal isRefresh As Boolean = False)

        Public MustOverride Function ProcessSingleDocuments(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal sale As Integer, ByVal cash As Integer, ByVal history As Integer, Optional ByVal sub_num As Integer = -1, Optional ByVal isRefresh As Boolean = True) As Boolean

        Public MustOverride Sub DeleteHistoryDocument(ByVal history As String)

        Public MustOverride Sub DeleteDocument(ByVal num_doc As Integer, ByVal customer As Integer, ByVal sale As Integer, ByVal cash As Integer, ByVal history As Integer)

        Public MustOverride Function ProcessReportQuartal(ByVal num_doc() As Integer, ByVal begin_date As DateTime, ByVal end_date As DateTime, Optional ByVal isRefresh As Boolean = True) As Boolean

    End Class


End Namespace