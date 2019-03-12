Imports System.IO
Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace Service
    Public Class ServiceLogger
        Const LogFileName = "LogFile.txt"

        Dim Shared ReadOnly _
            PathToLogFile As String = Path.Combine(Hosting.HostingEnvironment.MapPath("~/Log/"), LogFileName)

        Public Shared Function Write(сurrentMethod As MethodBase, strMessage As String) As Boolean
            Try
                Dim objFilestream As FileStream = New FileStream(PathToLogFile, FileMode.Append, FileAccess.Write)
                Dim objStreamWriter As StreamWriter = New StreamWriter(CType(objFilestream, Stream))
                objStreamWriter.WriteLine(String.Concat(сurrentMethod.DeclaringType.Name, ":", сurrentMethod.Name, "->",strMessage))
                objStreamWriter.Close()
                objFilestream.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace

