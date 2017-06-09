VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "������ ��������� ����������"
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
    
    If MsgBox("�� ������� ��� ������ ����������� ���� �������� ����������", vbYesNo) = vbNo Then End
    On Error GoTo ErrorHandler
    Set fso = New FileSystemObject
    Set wrd = New Word.Application
    Set doc = New Word.Document
    
    cmd = Command
        
    '������� �� ��
Retry6:
    If fso.FileExists(cmd & DocName6) Then
        Set doc = wrd.Documents.Open(cmd & DocName6)
        doc.PrintOut , , 4, , , , , 1, "1"
        doc.Close False
    Else
        i = MsgBox("������� �� �� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry6
    End If
    
    '�������
Retry1:
    If fso.FileExists(cmd & DocName1) Then
        Set doc = wrd.Documents.Open(cmd & DocName1)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("������� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry1
    End If
    
    '������ ���
Retry7:
    If fso.FileExists(cmd & DocName7) Then
        Set doc = wrd.Documents.Open(cmd & DocName7)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("������ ��� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry7
    End If

    '��� �������� ��������� ��������
Retry4:
    If Not fso.FileExists(cmd & "0" & DocName4) Then
        i = MsgBox("���� �������� ��������� ��������� �� ������" & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
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
    
    '��� ����������
Retry8:
    If Not fso.FileExists(cmd & "0" & DocName8) Then
        i = MsgBox("��� ���������� ��� �� ������" & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
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
    
    '������������� �������
Retry9:
    If Not fso.FileExists(cmd & "0" & DocName9) Then
        i = MsgBox("������������� ������� ��� �� ������" & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
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


    If MsgBox("��������� ������������ ���������" & vbCrLf & "� ������ ������ ������������� ������ �������� �������" & vbCrLf & "���������� ����� �������� �� �� ��� ������ ������� �����" & vbCrLf & "��� ����������� ������ ������� Ok", vbOKCancel + vbDefaultButton2) = vbCancel Then GoTo ClearObjects

    '������� �� ��
Retry62:
    If fso.FileExists(cmd & DocName6) Then
        Set doc = wrd.Documents.Open(cmd & DocName6)
        doc.PrintOut , , 4, , , , , 3, "2"
        doc.Close False
    Else
        i = MsgBox("������� �� �� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry62
    End If
    
    '�������
Retry12:
    If fso.FileExists(cmd & DocName1) Then
        Set doc = wrd.Documents.Open(cmd & DocName1)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("������� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry12
    End If
    
    '������ ���
Retry72:
    If fso.FileExists(cmd & DocName7) Then
        Set doc = wrd.Documents.Open(cmd & DocName7)
        doc.PrintOut
        doc.Close False
    Else
        i = MsgBox("������ ��� �� �����." & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
        If i = vbAbort Then GoTo ClearObjects
        If i = vbRetry Then GoTo Retry72
    End If

    '��� �������� ��������� ��������
Retry42:
    If Not fso.FileExists(cmd & "0" & DocName4) Then
        i = MsgBox("���� �������� ��������� ��������� �� ������" & vbCrLf & "����������� ���� �������� ����������", vbAbortRetryIgnore)
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
    
    MsgBox "������ ���������"
    
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

    MsgBox "��������� ��������� �� ������ � ���������� � ������������" & vbCrLf & "������ �" & Err.Number & ": " & Err.Description
    
End Sub
