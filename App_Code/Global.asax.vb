Imports System.Diagnostics
Imports System.Web
Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing
Imports System.Web.SessionState
Imports config
Imports Jobs


Namespace Kasbi


    Public Class [Global]
        Inherits System.Web.HttpApplication

        ReadOnly _smsScheduler As SmsScheduler = New SmsScheduler()

        

#Region " Component Designer Generated Code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

#End Region

        Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            'Application("ConnectionString") = "data source=srvmain;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=srvmain;packet size=4096;password=webdb;"
            'Application("ConnectionString") = "data source=BY-MN-DBSRV;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=BY-MN-DBSRV;packet size=4096;password=webdb;"
            'Application("ConnectionString") = "data source=192.168.11.14;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=198.168.11.14;packet size=4096;password=webdb;"
            Config.OnApplicationStart("")

            AreaRegistration.RegisterAllAreas()
            GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
            SmsScheduler.Start()
            EmailScheduler.Start()

            'FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            'RouteConfig.RegisterRoutes(RouteTable.Routes)
            'BundleConfig.RegisterBundles(BundleTable.Bundles)
        End Sub

        Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the session is started
            Session.Timeout = 900
            Session("dbSQL") = New MSSqlDB()
            Debug.WriteLine("Session_Start")
        End Sub

        Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires at the beginning of each request
        End Sub

        Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires upon attempting to authenticate the use
        End Sub

        Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when an error occurs
        End Sub

        Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the session ends
        End Sub

        Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the application ends
        End Sub

    End Class

End Namespace