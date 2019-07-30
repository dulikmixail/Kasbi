Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Xml

Namespace Kasbi

    Public Class Config
        Implements IConfigurationSectionHandler


        Private Shared appSettings As NameValueCollection

        Private Shared sqlConString As String
        Private Shared companyName As String
        Private Shared documentsFolder As String
        Private Shared kasbi4_id As String
        Private Shared _jobsSmsSender As String

        Private Const _SQL_CONNECTIONSTRING$ = "DataAccess.SqlConnectionString"
        Private Const _COMPANYNAME = "Web.Company"
        Private Const _DOCPATH$ = "Web.DocumentsFolder"
        Private Const _KASBI4_ID$ = "Web.Kasbi04_ID"
        Private Const _JOBS_SMS_SENDER$ = "Jobs.Sms.Sender"

        Private Const DEFAULT_SQL_CONNECTIONSTRING$ = "data source=by-mn-dbsrv;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=by-mn-dbsrv;packet size=4096;password=webdb;"
        Private Const DEFAULT_COMPANYNAME$ = "УП 'Рамок'"
        Private Const DEFAULT_DOCPATH$ = "Docs"
        Private Const DEFAULT_KASBI4_ID$ = "120"
        Private Const DEFAULT_JOBS_SMS_SENDER$ = "false"

        Public Function Create( _
          ByVal parent As Object, ByVal configContext As Object, ByVal section As System.Xml.XmlNode) _
          As Object Implements System.Configuration.IConfigurationSectionHandler.Create

            Try
                Dim baseHandler As NameValueSectionHandler = New NameValueSectionHandler()
                appSettings = baseHandler.Create(parent, configContext, section)
            Catch ex As Exception
                appSettings = Nothing
            End Try

            If (appSettings Is Nothing) Then
                sqlConString = DEFAULT_SQL_CONNECTIONSTRING
                companyName = DEFAULT_COMPANYNAME
            Else
                sqlConString = ReadSetting(appSettings, _SQL_CONNECTIONSTRING, DEFAULT_SQL_CONNECTIONSTRING)
                companyName = ReadSetting(appSettings, _COMPANYNAME, DEFAULT_COMPANYNAME)
            End If
            Return appSettings
        End Function

        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As String) As String
            Try
                Dim setting As Object = settings.Item(key)
                If (setting Is Nothing) Then
                    Return defaultValue
                Else
                    Return setting.ToString()
                End If
            Catch
                Return defaultValue
            End Try
        End Function

        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As Boolean) As Boolean
            Try
                Dim setting As Object = settings.Item(key)
                If (setting Is Nothing) Then
                    Return defaultValue
                Else
                    Return Boolean.Parse(setting.ToString())
                End If
            Catch
                Return defaultValue
            End Try
        End Function

        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As Integer) As Integer
            Try
                Dim setting As Object = settings.Item(key)
                If (setting Is Nothing) Then
                    Return defaultValue
                Else
                    Return Integer.Parse(setting.ToString())
                End If
            Catch
                Return defaultValue
            End Try
        End Function

        Public Shared Sub OnApplicationStart(ByVal myAppPath As String)
            Try
                appSettings = System.Configuration.ConfigurationManager.GetSection("appSettings")

            Catch ex As Exception
                appSettings = Nothing
            End Try

            If (appSettings Is Nothing) Then
                sqlConString = DEFAULT_SQL_CONNECTIONSTRING
                companyName = DEFAULT_COMPANYNAME
            Else
                sqlConString = ReadSetting(appSettings, _SQL_CONNECTIONSTRING, DEFAULT_SQL_CONNECTIONSTRING)
                companyName = ReadSetting(appSettings, _COMPANYNAME, DEFAULT_COMPANYNAME)
            End If

        End Sub

        Public Shared ReadOnly Property SqlConnectionString() As String
            Get
                If (sqlConString Is Nothing) Then
                    sqlConString = ReadSetting(appSettings, _SQL_CONNECTIONSTRING, DEFAULT_SQL_CONNECTIONSTRING)
                End If
                Return sqlConString
            End Get
        End Property

        Public Shared ReadOnly Property Company() As String
            Get
                If (companyName Is Nothing) Then
                    companyName = ReadSetting(appSettings, _COMPANYNAME, DEFAULT_COMPANYNAME)
                End If
                Return companyName
            End Get
        End Property

        Public Shared ReadOnly Property DocsPath() As String
            Get
                If (documentsFolder Is Nothing) Then
                    documentsFolder = ReadSetting(appSettings, _DOCPATH, DEFAULT_DOCPATH)
                End If
                Return documentsFolder
            End Get
        End Property

        Public Shared ReadOnly Property Kasbi04_ID() As String
            Get
                If (kasbi4_id Is Nothing) Then
                    kasbi4_id = ReadSetting(appSettings, _KASBI4_ID, DEFAULT_KASBI4_ID)
                End If
                Return kasbi4_id
            End Get
        End Property

        Public Shared ReadOnly Property JobsSmsSender As String
            Get
                If (_jobsSmsSender Is Nothing)
                    _jobsSmsSender = ReadSetting(appSettings, _JOBS_SMS_SENDER, DEFAULT_JOBS_SMS_SENDER)
                End If
                Return _jobsSmsSender
            End Get
        End Property
    End Class

End Namespace
