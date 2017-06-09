VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Печать комплекта документов"
   ClientHeight    =   4530
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7080
   LinkTopic       =   "Form1"
   ScaleHeight     =   4530
   ScaleWidth      =   7080
   StartUpPosition =   3  'Windows Default
   Visible         =   0   'False
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
    Const DocName0 = "InvoiceNDS.doc"
    Const DocName1 = "Dogovor.doc"
    Const DocName2 = "Zayavlenie_Na_Knigu_Kassira.doc"
    Const DocName3 = "Zayavlenie.doc"
    Const DocName4 = "Akt_Pokazaniy.doc"
    Const DocName5 = "TTN.doc"
    Const DocName6 = "Dogovor_Na_TO.doc"
    Const DocName7 = "Spisok_KKM.doc"
    Const DocName8 = "Teh_Zaklyuchenie.doc"
    Const DocName9 = "Udostoverenie_Kassira.doc"

Private Sub Form_Load()
    Dim fso As FileSystemObject
    Dim wrd As Word.Application
    Dim doc As Word.Document
    Dim i
    
    If MsgBox("Вы уверены что хотите распечатать весь комплект документов", vbYesNo) = vbNo Then End
    On Error GoTo ErrorHandler
    Set fso = New FileSystemObject
    Set wrd = New Word.Application
    Set doc = New Word.Document
    
    cmd = Command
        
    'Договор на ТО
Retry6:
    If fso.FileExists(cmd & DocName6) Then
        Set doc = wrd.Documents.Open(cmd & DocName6)
        doc.PrintOut , , 4, , , , , 1, "1"
        doc.Close False
    Else
        i = MsgBox("Договор на ТО не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry6
    End If
    
    'Договор
Retry1:
    If fso.FileExists(cmd & DocName1) Then
        Set doc = wrd.Documents.Open(cmd & DocName1)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("Договор не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry1
    End If
    
    'Список ККМ
Retry7:
    If fso.FileExists(cmd & DocName7) Then
        Set doc = wrd.Documents.Open(cmd & DocName7)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("Список ККМ не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry7
    End If

    'Акт списания показаний счетчика
Retry4:
    If Not fso.FileExists(cmd & "0" & DocName4) Then
        i = MsgBox("Акты списания показаний счетчиков не готовы" & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry4
    Else
        i = 0
        While fso.FileExists(cmd & i & DocName4)
            Set doc = wrd.Documents.Open(cmd & i & DocName4)
            doc.PrintOut
            doc.Close False
        Wend
    End If
    
    'Тех заключение
Retry8:
    If Not fso.FileExists(cmd & "0" & DocName8) Then
        i = MsgBox("Тех заключение еще не готово" & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry8
    Else
        i = 0
        While fso.FileExists(cmd & i & DocName8)
            Set doc = wrd.Documents.Open(cmd & i & DocName8)
            doc.PrintOut
            doc.Close False
        Wend
    End If
    
    'Удостоверение кассира
Retry9:
    If Not fso.FileExists(cmd & "0" & DocName9) Then
        i = MsgBox("Удостоверение кассира еще не готово" & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry9
    Else
        i = 0
        While fso.FileExists(cmd & i & DocName9)
            Set doc = wrd.Documents.Open(cmd & i & DocName9)
            doc.PrintOut
            doc.Close False
        Wend
    End If


    If MsgBox("Проверьте напечатанные документы" & vbCrLf & "В случае ошибки перепечатайте нужный документ вручную" & vbCrLf & "Переложите листы договора на ТО для печати второго листа" & vbCrLf & "Для продолжения печати нажмите Ok", vbOKCancel + vbDefaultButton2) = vbCancel Then GoTo ClearObjects

    'Договор на ТО
Retry62:
    If fso.FileExists(cmd & DocName6) Then
        Set doc = wrd.Documents.Open(cmd & DocName6)
        doc.PrintOut , , 4, , , , , 3, "2"
        doc.Close False
    Else
        i = MsgBox("Договор на ТО не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry62
    End If
    
    'Договор
Retry12:
    If fso.FileExists(cmd & DocName1) Then
        Set doc = wrd.Documents.Open(cmd & DocName1)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("Договор не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry12
    End If
    
    'Список ККМ
Retry72:
    If fso.FileExists(cmd & DocName7) Then
        Set doc = wrd.Documents.Open(cmd & DocName7)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("Список ККМ не готов." & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry72
    End If

    'Акт списания показаний счетчика
Retry42:
    If Not fso.FileExists(cmd & "0" & DocName4) Then
        i = MsgBox("Акты списания показаний счетчиков не готовы" & vbCrLf & "Сформируйте весь комплект документов", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry42
    Else
        i = 0
        While fso.FileExists(cmd & i & DocName4)
            Set doc = wrd.Documents.Open(cmd & i & DocName4)
            doc.PrintOut
            doc.Close False
        Wend
    End If
    
    MsgBox "Печать завершена"
    
ClearObjects:
    
    wrd.Quit False
    
    Set wrd = Nothing
    Set doc = Nothing
    
    End
    
    Exit Sub
    
ErrorHandler:
    
    If Not doc Is Nothing Then
        doc.Close False
    End If
            
    If Not wrd Is Nothing Then
        wrd.Quit False
    End If

    Set wrd = Nothing
    Set doc = Nothing

    MsgBox "Запомните сообщение об ошибке и обратитесь к разработчику" & vbCrLf & "Ошибка №" & Err.Number & ": " & Err.Description
    
End Sub
