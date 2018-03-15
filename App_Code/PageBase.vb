Imports Microsoft.VisualBasic

Imports System
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Threading
Imports System.Text.RegularExpressions

Imports Kasbi.MSSqlDB
Imports System.Data
Imports System.Collections

Namespace Kasbi

    Public Structure baseField
        Public fieldSize As Integer
        Public fieldName As String
        Public fieldType As SqlDbType

        Public Sub New(ByVal fieldName As String, ByVal fieldType As SqlDbType, ByVal fieldSize As Integer)
            Me.fieldName = fieldName
            Me.fieldSize = fieldSize
            Me.fieldType = fieldType
        End Sub
    End Structure



    Public Class CUser
        Protected fieldsData As ArrayList
        Public isLogged As Boolean = False

        Public Sub New()
            Dim i%
            fieldsData = New ArrayList()
            fieldsData.Clear()
            For i = 0 To 10
                fieldsData.Add(DBNull.Value)
            Next

        End Sub

        Dim fields() As baseField = { _
            New baseField("sys_id", SqlDbType.Int, 10), _
            New baseField("Name", SqlDbType.VarChar, 50), _
            New baseField("IP", SqlDbType.VarChar, 50), _
            New baseField("account", SqlDbType.VarChar, 50), _
            New baseField("Password", SqlDbType.VarChar, 50), _
            New baseField("document", SqlDbType.VarChar, 60), _
            New baseField("is_admin", SqlDbType.Bit, 1), _
            New baseField("role_id", SqlDbType.Int, 60), _
            New baseField("is_saler", SqlDbType.Bit, 1), _
            New baseField("inactive", SqlDbType.Bit, 1), _
            New baseField("permissions", SqlDbType.Int, 10) _
            }

        Public Sub FillData(ByVal data As DataRow)
            sys_id = data("sys_id")
            Name = IIf(IsDBNull(data("Name")), "", data("Name"))
            IP = IIf(IsDBNull(data("IP")), "", data("IP"))
            account = IIf(IsDBNull(data("account")), "", data("account"))
            Password = IIf(IsDBNull(data("Password")), "", data("Password"))
            document = IIf(IsDBNull(data("document")), "", data("document"))
            is_admin = IIf(IsDBNull(data("is_admin")), False, data("is_admin"))
            role_id = data("role_id")
            is_saler = IIf(IsDBNull(data("is_saler")), False, data("is_saler"))
            inactive = IIf(IsDBNull(data("inactive")), False, data("inactive"))
            permissions = IIf(IsDBNull(data("permissions")), 0, data("permissions"))

            isLogged = True
        End Sub

        Public Sub Data(ByVal fieldIndex As Integer, ByVal data As Object)
            If (data Is Nothing) Then data = DBNull.Value
            'If (fields(fieldIndex).fieldType = SqlDbType.Char And fields(fieldIndex).fieldSize = 1) Then  'bool
            '    If (Boolean.Parse(data.ToString()) = True) Then
            '        data = "T"
            '    Else : data = "F"
            '    End If
            'End If
            If (fieldsData(fieldIndex).Equals(data)) Then Return
            Try
                fieldsData(fieldIndex) = data
            Catch
            End Try
        End Sub

        Public Property sys_id() As Integer
            Get
                Try
                    Return (Integer.Parse(fieldsData(0).ToString()))
                Catch
                    Return 0
                End Try
            End Get
            Set(ByVal value As Integer)
                Data(0, value) '"sys_id"
            End Set
        End Property

        Public Property Name() As String
            Get
                Try
                    Return fieldsData(1).ToString()
                Catch
                    Return ""
                End Try
            End Get
            Set(ByVal value As String)
                Data(1, value) '"Name"
            End Set
        End Property

        Public Property IP() As String
            Get
                Try
                    Return fieldsData(2).ToString()
                Catch
                    Return ""
                End Try
            End Get
            Set(ByVal value As String)
                Data(2, value) '"IP"
            End Set
        End Property

        Public Property account() As String
            Get
                Try
                    Return fieldsData(3).ToString()
                Catch
                    Return ""
                End Try
            End Get
            Set(ByVal value As String)
                Data(3, value) '"account"
            End Set
        End Property

        Public Property Password() As String
            Get
                Try
                    Return fieldsData(4).ToString()
                Catch
                    Return ""
                End Try
            End Get
            Set(ByVal value As String)
                Data(4, value) '"Password"
            End Set
        End Property

        Public Property document() As String
            Get
                Try
                    Return fieldsData(5).ToString()
                Catch
                    Return ""
                End Try
            End Get
            Set(ByVal value As String)
                Data(5, value) '"document"
            End Set
        End Property

        Public Property is_admin() As Boolean
            Get
                Try
                    Return (Boolean.Parse(fieldsData(6).ToString()))
                Catch
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Data(6, value) '"is_admin"
            End Set
        End Property

        Public Property is_saler() As Boolean
            Get
                Try
                    Return (Boolean.Parse(fieldsData(7).ToString()))
                Catch
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Data(7, value) '"is_saler"
            End Set
        End Property

        Public Property inactive() As Boolean
            Get
                Try
                    Return (Boolean.Parse(fieldsData(8).ToString()))
                Catch
                    Return False
                End Try
            End Get
            Set(ByVal value As Boolean)
                Data(8, value) '"inactive"
            End Set
        End Property

        Public Property role_id() As Integer
            Get
                Try
                    Return (Integer.Parse(fieldsData(9).ToString()))
                Catch
                    Return 0
                End Try
            End Get
            Set(ByVal value As Integer)
                Data(9, value) '"role_id"
            End Set
        End Property

        Public Property permissions() As Integer
            Get
                Try
                    Return (Integer.Parse(fieldsData(10).ToString()))
                Catch
                    Return 0
                End Try
            End Get
            Set(ByVal value As Integer)
                Data(10, value) '"permissions"
            End Set
        End Property

    End Class

    Public Class PageError

        Public Shared Sub Error_Handler(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Dim ex As Exception = HttpContext.Current.Server.GetLastError()
            HttpContext.Current.Session("ErrorMessage") = "<B> Message:</B>" & ex.Message.ToString() + "<BR><B> Source:</B>" & ex.Source.ToString() & "<BR><B> StackTrace:</B>" & ex.StackTrace.ToString()
            Dim ErrorMessage As String = "\n Message:" & ex.Message.ToString() & "\n Source:" & ex.Source.ToString() & "\n StackTrace:" & ex.StackTrace.ToString()
            HttpContext.Current.Response.Redirect("Errors.aspx")
        End Sub

    End Class


    Public Class PageBase
        Inherits System.Web.UI.Page
        Private db As MSSqlDB
        Private curUser As CUser
        Private queryStr As NameValueCollection

        Public Sub New()
        End Sub

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            AddHandler Page.Error, AddressOf Kasbi.PageError.Error_Handler
            If (sender.GetType().FullName <> "ASP.login_aspx") Then
                If (CurrentUser Is Nothing) Then Response.Redirect(GetAbsoluteUrl("~/Login.aspx"))
                If (CurrentUser.isLogged = False) Then Response.Redirect(GetAbsoluteUrl("~/Login.aspx"))

            End If

            queryStr = New NameValueCollection()
            DecryptQueryString()
            Try
                Page.Title = Config.Company & " - " & Page.Title
            Catch
            End Try

            pageLoad()
        End Sub

        Protected Sub GetPageAccess(ByVal sID As String)
            Dim Err As Boolean = QueryString.Count = 0
            If (Err = False) Then
                Try
                    Integer.Parse(QueryString.Item(sID))
                Catch
                    Err = True
                End Try
            End If

            If Err = True Then Response.Redirect("Login.aspx")
        End Sub

        Protected Sub GetPageAccess(ByVal sID1 As String, ByVal sID2 As String)

            GetPageAccess(sID1, sID2, False)
        End Sub

        Protected Sub GetPageAccess(ByVal sID1 As String, ByVal sID2 As String, ByVal isString As Boolean)

            Dim Err As Boolean = QueryString.Count < 2
            If (Err = False) Then
                Try
                    Integer.Parse(QueryString.Item(sID1))
                    If (isString) Then
                        Convert.ToString(QueryString.Item(sID2))
                    Else
                        Integer.Parse(QueryString.Item(sID2))
                    End If
                Catch
                    Err = True
                End Try
            End If
            If Err = True Then Response.Redirect("Login.aspx")
        End Sub


        ''' <remarks>ѕолучение значение параметра из URL-адреса.</remarks>
        ''' <summary >ѕолучение значение параметра по имени </summary>
        ''' <returns>÷елочисленое значение параметра</returns>
        Public Function GetPageParam(ByVal paramName As String) As Integer
            Try
                Return Integer.Parse(Request.QueryString(paramName))

            Catch
                Return 0
            End Try
        End Function
        ''' <remarks>ѕолучение значение параметра из URL-адреса.</remarks>
        ''' <summary >ѕолучение значение параметра по индексу </summary>
        ''' <returns>÷елочисленое значение параметра</returns>
        Public Function GetPageParam(ByVal paramIndex As Integer) As Integer
            Try
                Return Integer.Parse(Request.QueryString(paramIndex))
            Catch
                Return 0
            End Try
        End Function

        Private Sub DecryptQueryString()
            queryStr = Request.QueryString
        End Sub

        Protected Sub SetUpLocalization()
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
        End Sub

        'public string LocalDateFormat
        '{
        '	get
        '	{
        '		return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper();
        '	}
        '}

        'public string ClientCultureInfoName
        '{
        '	get
        '	{
        '		/* Getting "Language info" from browser*/
        '		string localizeCodeFromClient = this.Request.UserLanguages[0];

        '		Regex expr = new Regex(@"^([a-zA-Z]{0,2})\-([a-zA-Z]{2,3})$", RegexOptions.Compiled);
        '          If (!expr.IsMatch(localizeCodeFromClient)) Then
        '		{
        '			localizeCodeFromClient = localizeCodeFromClient.Substring(0,2);
        '			foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
        '				if ((ci.Name.ToString().Substring(0,2) == localizeCodeFromClient) && (ci.Name.Length > 3))
        '				{
        '					localizeCodeFromClient = ci.Name;
        '					break;
        '				}
        '		}
        '		//return "ru-RU";
        '		return localizeCodeFromClient;
        '	}
        '}

        Public Function GetAbsoluteUrl(ByVal pageName As String) As String
            Return pageName 'Request.Url.Scheme & "://" & Request.Url.Host & ":" & Request.Url.Port & Request.ApplicationPath & "/" & pageName
        End Function

        Public ReadOnly Property QueryString() As NameValueCollection
            Get
                Return queryStr
            End Get
        End Property

        Protected Sub pageLoad()
        End Sub

        ReadOnly Property dbSQL() As MSSqlDB
            Get
                If (db Is Nothing) Then
                    db = Session("dbSQL")
                End If
                Return db
            End Get
        End Property

        ReadOnly Property CurrentUser() As CUser
            Get
                If (curUser Is Nothing) Then
                    curUser = Session("USER")
                End If
                Return curUser
            End Get
        End Property

        Protected Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End Sub

        'public static void log(string Message)
        '{
        '          Try
        '	{
        '		if (logBuilder == null) logBuilder = new StringBuilder();
        '                  Lock(logBuilder)
        '		{
        '			if (logWriter == null)
        '			{
        '				logWriter = new StreamWriter("C:\\Temp\\IRTError.log", true);
        '				logWriter.AutoFlush = true;
        '			}
        '			DateTime now = DateTime.Now;
        '			logBuilder.Length = 0;
        '			logBuilder.Append("[").Append(now).Append(" ").Append(now.Millisecond).Append("] ");
        '			logWriter.WriteLine(logBuilder + " " + Message);
        '			logWriter.Close();
        '			logWriter = null;
        '		}
        '	}
        '	catch
        '	{
        '		//HttpContext.Current.Response.Write(ex.ToString());
        '	}
        '}

        '		public bool ValidateDate(string s)
        '		{
        '                Try
        '			{
        '				DateTimeFormatInfo dtFI_USA = new CultureInfo("en-US", false).DateTimeFormat;
        '				DateTimeFormatInfo dtFI_Current = new CultureInfo(ClientCultureInfoName, false).DateTimeFormat;

        '				return (DateTime.Compare(DateTime.Parse(s, dtFI_Current), DateTime.Parse("01/01/1753", dtFI_USA)) > 0) &&
        '					(DateTime.Compare(DateTime.Parse("12/31/9999", dtFI_USA), DateTime.Parse(s, dtFI_Current)) >= 0);

        '//				return (DateTime.Compare(DateTime.Parse(s), DateTime.Parse("01/01/1754")) > 0) &&
        '//					(DateTime.Compare(DateTime.Parse("12/31/9999"), DateTime.Parse(s)) >= 0);
        '			}
        '            Catch
        '			{
        '				return false;
        '			}
        '		}
        '	}
    End Class

End Namespace



