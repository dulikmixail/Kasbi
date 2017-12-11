
Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class CashRegistersForTO
        Inherits PageBase
        Dim CurrentCustomer%
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            'Common.SetUpLocalization()
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")

            If Not IsPostBack Then
                Try
                    'tbxBeginDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    'tbxEndDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                ShowContent()
            End If
        End Sub

        Private Sub ShowContent()
            LoadCashRegisterList()
        End Sub

        Private Sub LoadCashRegisterList()

        End Sub

    End Class

End Namespace
