
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.HtmlTextWriter
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader
Imports System.Data

Partial Class QueryBuilder_Showdata

#Region "variable Declaration"
    Inherits System.Web.UI.Page
    Public htmlhead As String
    Public htmlheaddiv As String
    Public htmlhead1 As String
    Public Path As String
    Public sPath As String
    Dim con As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(con)
    Dim connection1 As New SqlConnection(con)
    Dim connection2 As New SqlConnection(con)
    Dim connection11 As New SqlConnection(con)
    Dim connection3 As New SqlConnection(con)
    Dim connectionh As New SqlConnection(con)
    Dim strsqlshow
    Dim strsqlwhere
    Dim strsqlgroup
    Dim strsql

    Dim tablename As String
    Dim tableid As Integer
    Dim rdrModules As SqlDataReader
    Dim str2rowrdrModules As SqlDataReader

    Dim rdrPrgms As SqlDataReader
    Dim strQryPrgms As String = ""
    Dim cmdPrgms As New SqlCommand
    Dim strcolumn As String = ""

    Dim rowdata As String = ""
    Dim rowdata1 As String = ""
    Dim rowdata3 As String = ""

    Dim drowdata As String = ""
    Dim drowdata3 As String = ""

    Dim rowdata1value As String = ""
    Dim rowdata3value As String = ""

    Dim rowdataarr As Array
    Dim rowdata1arr As Array
    Dim rowdata3arr As Array

    Dim rmm As Integer
    Dim rmm3 As Integer
    Dim rwom As Integer

    Dim formulaarr As Array
    Dim formulaarr2 As Array
    Dim formulaarr3 As Array
    Dim formualvalue As Integer
    Dim strfrumala As String

    Dim dshowdata As String = ""
    Dim showdata As String = ""

    Dim showdataarr As Array
    Dim smm As Integer = 0

    Dim fm As Integer

    Dim columnnamearr As Array

    Dim columnname As String = ""
    Dim columnname2 As String = ""
    Dim columnname3 As String = ""
    Dim columnname4 As String = ""

    Dim columnnamestring As String = ""
    Dim columnnamestring2 As String = ""
    Dim columnnamestring3 As String = ""
    Dim columnnamestring4 As String = ""

    Dim columnarr As Array
    Dim columnarr2 As Array
    Dim columnarr3 As Array
    Dim columnarr4 As Array

    Dim Tcolumnarr As Array
    Dim Tcolumnarr2 As Array
    Dim Tcolumnarr3 As Array
    Dim Tcolumnarr3C As Array '' for count in cacs calculate average
    Dim Tcolumnarr4 As Array
    Dim Tcolumnarr4C As Array '' for count in cacs calculate average
    Dim Tcolstring As String = ""
    Dim TColCountString As String

    Dim tcm As Integer
    Dim tcm2 As Integer
    Dim tcm3 As Integer
    Dim tcm4 As Integer

    Dim subtot1 As Double = 0.0
    Dim subtot2 As Double = 0.0
    Dim subtot3 As Double = 0.0
    Dim subtot4 As Double = 0.0

    Dim cm As Integer
    Dim cm2 As Integer
    Dim cm3 As Integer
    Dim cm4 As Integer

    Dim sqlrsdatac As SqlDataReader
    Dim rowhtmlstringc As String
    Dim strsql1c As String = ""
    Dim grtotal As Double = 0.0
    Dim sqlrsdata As SqlDataReader
    Dim rowhtmlstring As String
    Dim rowhtmlstring1 As String
    Dim strOperator As String = "()+-*/"
    'Chart table - var start 
    Dim dt As New DataTable
    ''Protected WithEvents txtstrdiv As System.Web.UI.HtmlControls.HtmlInputHidden
    ' ''Protected WithEvents hidData As System.Web.UI.HtmlControls.HtmlInputHidden
    ''Protected WithEvents txtname As System.Web.UI.WebControls.TextBox
    ''Protected WithEvents htmlheadtxt As System.Web.UI.WebControls.TextBox
    ''Protected WithEvents htmlqueryname As System.Web.UI.WebControls.TextBox
    ''Protected WithEvents cmdsave As System.Web.UI.WebControls.Button
    ''Protected WithEvents txtdivshow As System.Web.UI.HtmlControls.HtmlInputHidden
    ''Protected WithEvents divhtml As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents cbodept As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboclient As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cbolob As System.Web.UI.HtmlControls.HtmlSelect
    Dim row As DataRow
    Dim col0 As DataColumn
    Dim col1 As DataColumn
    Protected WithEvents dlDSC As System.Web.UI.WebControls.DataGrid
    ''Protected WithEvents ChartControl1 As WebChart.ChartControl
    ''Protected WithEvents ChartControl2 As WebChart.ChartControl
    Dim col2 As DataColumn
    Dim arrGrandRowTotal(3, 1)
    ''Protected WithEvents hidData As System.Web.UI.HtmlControls.HtmlInputHidden
    Dim intGrandRowTotal As Integer = 0
    Dim arrGrandRowCount(3, 1)

    ''Protected WithEvents lblhtmlname As System.Web.UI.WebControls.Label
    ''Protected WithEvents lblMain As System.Web.UI.WebControls.Label
    ''Protected WithEvents imgexl As System.Web.UI.WebControls.ImageButton
    ''Protected WithEvents queryname As System.Web.UI.HtmlControls.HtmlInputHidden
    ''Protected WithEvents txtheading As System.Web.UI.HtmlControls.HtmlInputHidden

    Dim intSubColTotal As Integer = 0

    'Chart table - end 
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Page Load"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        AjaxPro.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        If Me.IsPostBack = False Then
            Dim boolAVG As Boolean
            Dim cmdgetdept As New SqlCommand("select * from IdmsDepartment", connection)
            Dim dsgetdept As New DataSet
            Dim adpgetdept As New SqlDataAdapter
            adpgetdept.SelectCommand = cmdgetdept
            connection.Open()
            adpgetdept.Fill(dsgetdept)
            connection.Close()
            ddlDept.DataSource = dsgetdept
            ddlDept.DataTextField = "DepartmentName"
            ddlDept.DataValueField = "autoid"
            ddlDept.DataBind()
            cmdgetdept.Dispose()
            ddlDept.Items.Insert(0, "--Select--")
            Dim strOrderBy As String = ""
            Dim showdata1 As String()
            'Dim strUser As String = Session("UserName")
            Dim Quname As String
            Dim strheadof As String = ""
            formulaarr = Split(Trim(Request("txtaFormula")), "$")
            formulaarr2 = Split(Trim(Request("txtaFormula")), "$")
            formulaarr3 = Split(Trim(Request("txtaFormula")), "$")
            showdataarr = Split(Trim(Request("Showdata")), ",")
            showdata1 = Split(Trim(Request("Showdata1")), "$")
            rowdataarr = Split(Trim(Request("crdata")), ",")
            columnnamearr = Split(Trim(Request("column")), ",")
            Quname = Trim(Request("txtQueryNAme"))
            htmlqueryname.Text = Trim(Request("txtQueryNAme"))

            Dim sh1 As String = ""
            Dim hj = 0
            Dim ij As Integer = 0
            Dim b As Boolean = False
            Dim functi As String = ""

            '***********************************************************************
            If (showdataarr.Length = 0) Then
                For hj = 0 To UBound(showdata1)
                    If (sh1 = "") Then
                        sh1 = "Count(" + showdata1(hj) + ")"
                    Else
                        sh1 = sh1 + ",Count(" + showdata1(hj) + ")"
                    End If
                Next
                showdataarr = sh1.Split(",")
            Else


                For hj = 0 To UBound(showdata1)
                    For ij = 0 To UBound(showdataarr)
                        Dim s, a As String
                        s = showdata1(hj)
                        a = showdataarr(ij)
                        If a.Contains(s) Then
                            b = True
                            Exit For
                        End If

                    Next
                    If b = True Then
                        If functi = "" Then
                            functi = showdataarr(ij)
                        Else
                            functi = functi + "," + showdataarr(ij)
                        End If
                    Else
                        If functi = "" Then
                            ' functi = "Count(" + showdata1(hj) + ")"
                            functi = showdata1(hj)
                        Else
                            'functi = functi + ",Count(" + showdata1(hj) + ")"
                            functi = functi + "," + showdata1(hj)
                        End If
                    End If
                Next
                'Dim jk = 0

                'For jk = 0 To UBound(showdataarr)
                '    Dim bh As Boolean = False
                '    For hj = 0 To UBound(showdata1)
                '        If (showdataarr(jk).Contains(showdata1(hj))) Then
                '            bh = True
                '        End If
                '    Next
                '    If (bh = False) Then
                '        If (sh1.Length = 0) Then
                '            sh1 = "Count(" + showdata(hj) + ")"
                '        Else
                '            sh1 = sh1 + ",Count(" + showdata(hj) + ")"
                '        End If
                '    Else
                '        If (sh1.Length = 0) Then
                '            sh1 = showdataarr(jk)
                '        Else
                '            sh1 = sh1 + "," + showdataarr(jk)
                '        End If
                '    End If
                'Next
                showdataarr = functi.Split(",")
            End If
            '************************************************************************************
            If htmlqueryname.Text <> "" Then

                Me.queryname.Value = htmlqueryname.Text
            Else
                Me.queryname.Value = Request("queryname")

            End If
            Dim head As String = ""
            If htmlqueryname.Text <> "" Then
                Dim cmdgetheading As New SqlCommand("select subheading from subtotal where queryName='" & htmlqueryname.Text & "'", connectionh)
                Dim dr As SqlDataReader
                connectionh.Open()
                dr = cmdgetheading.ExecuteReader
                If dr.Read Then
                    head = dr("subheading")
                End If
                connectionh.Close()
                dr.Close()
            End If

            If Request("txtsubname") <> "" Then
                Me.txtheading.Value = Request("txtsubname")
            Else
                Me.txtheading.Value = head
            End If
            Session("heading") = Me.txtheading.Value
            '******************************************************************************************************
            'ShowConfirm(Me.txtheading.Value)
            'lblMain.Text = Quname
            'strheadof
            strheadof = strheadof & "<table Width=100% >"
            strheadof = strheadof & "<tr><td align=center>" & Quname & " </td></tr></table>"
            Try
                If showdataarr.Length = 1 Then
                    If UCase(Left(Trim(showdataarr(0)), 4)) = "AVG(" Then
                        boolAVG = True
                    Else
                        boolAVG = False
                    End If
                Else
                    boolAVG = False
                End If
                If columnnamearr.Length = 1 Then
                    columnname = columnnamearr(0)
                ElseIf columnnamearr.Length = 2 Then
                    columnname = columnnamearr(0)
                    columnname2 = columnnamearr(1)
                ElseIf columnnamearr.Length = 3 Then
                    columnname = columnnamearr(0)
                    columnname2 = columnnamearr(1)
                    columnname3 = columnnamearr(2)
                ElseIf columnnamearr.Length = 4 Then
                    columnname = columnnamearr(0)
                    columnname2 = columnnamearr(1)
                    columnname3 = columnnamearr(2)
                    columnname4 = columnnamearr(3)
                Else
                    columnname = columnnamearr(0)
                    columnname2 = columnnamearr(1)
                    columnname3 = columnnamearr(2)
                    columnname4 = columnnamearr(3)
                End If

                If Trim(Request("hidtablename")) <> "" Then
                    If columnnamearr.Length = 1 Then
                        If Trim(columnname) <> "" Then
                            Dim strQryMod As String
                            If Trim(Request("wheredata")) <> "" Then
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " order by " & columnname
                            End If
                            If Trim(Request("wheredata")) <> "" Then
                                If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                    strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                    strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                    strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                    strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                                Else
                                    strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname
                                End If
                            Else
                                If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                    strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                    strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                    strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                    strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                                Else
                                    strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " order by " & columnname
                                End If
                            End If
                            Dim cmdMod As New SqlCommand(strQryMod, connection)
                            connection.Open()
                            rdrModules = cmdMod.ExecuteReader
                            While rdrModules.Read
                                If Trim(rdrModules("" & columnname & "").ToString) <> "" Then
                                    If Trim(columnnamestring) = "" Then
                                        columnnamestring = Trim(rdrModules("" & columnname & "").ToString)
                                    Else
                                        columnnamestring = columnnamestring & "$" & Trim(rdrModules("" & columnname & "").ToString)
                                    End If
                                Else
                                    If Trim(columnnamestring) = "" Then
                                        columnnamestring = "0"
                                    Else
                                        columnnamestring = columnnamestring & "$" & "0"
                                    End If
                                End If
                            End While
                            connection.Close()
                        Else
                            columnnamestring = ""
                        End If
                    ElseIf columnnamearr.Length = 2 Then
                        Dim strQryMod As String
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " order by " & columnname
                            End If
                        End If
                        Dim cmdMod As New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname & "").ToString) <> "" Then
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = Trim(rdrModules("" & columnname & "").ToString)
                                Else
                                    columnnamestring = columnnamestring & "$" & Trim(rdrModules("" & columnname & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = "0"
                                Else
                                    columnnamestring = columnnamestring & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname2
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname2
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname2 & "").ToString) <> "" Then
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = Trim(rdrModules("" & columnname2 & "").ToString)
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & Trim(rdrModules("" & columnname2 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = "0"
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                    ElseIf columnnamearr.Length = 3 Then
                        Dim strQryMod As String
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " order by " & columnname
                            End If
                        End If
                        Dim cmdMod As New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname & "").ToString) <> "" Then
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = Trim(rdrModules("" & columnname & "").ToString)
                                Else
                                    columnnamestring = columnnamestring & "$" & Trim(rdrModules("" & columnname & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = "0"
                                Else
                                    columnnamestring = columnnamestring & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname2
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname2
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname2 & "").ToString) <> "" Then
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = Trim(rdrModules("" & columnname2 & "").ToString)
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & Trim(rdrModules("" & columnname2 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = "0"
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname3 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname3
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname3 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname3
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname3 & "").ToString) <> "" Then
                                If Trim(columnnamestring3) = "" Then
                                    columnnamestring3 = Trim(rdrModules("" & columnname3 & "").ToString)
                                Else
                                    columnnamestring3 = columnnamestring3 & "$" & Trim(rdrModules("" & columnname3 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring3) = "" Then
                                    columnnamestring3 = "0"
                                Else
                                    columnnamestring3 = columnnamestring3 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()

                    ElseIf columnnamearr.Length = 4 Then
                        Dim strQryMod As String
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname & ") from " & Trim(Request("hidtablename")) & " order by " & columnname
                            End If
                        End If
                        Dim cmdMod As New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname & "").ToString) <> "" Then
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = Trim(rdrModules("" & columnname & "").ToString)
                                Else
                                    columnnamestring = columnnamestring & "$" & Trim(rdrModules("" & columnname & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring) = "" Then
                                    columnnamestring = "0"
                                Else
                                    columnnamestring = columnnamestring & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname2
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname2), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname2 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname2 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname2
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname2 & "").ToString) <> "" Then
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = Trim(rdrModules("" & columnname2 & "").ToString)
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & Trim(rdrModules("" & columnname2 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring2) = "" Then
                                    columnnamestring2 = "0"
                                Else
                                    columnnamestring2 = columnnamestring2 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname3 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname3
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname3), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname3 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname3
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname3 & "").ToString) <> "" Then
                                If Trim(columnnamestring3) = "" Then
                                    columnnamestring3 = Trim(rdrModules("" & columnname3 & "").ToString)
                                Else
                                    columnnamestring3 = columnnamestring3 & "$" & Trim(rdrModules("" & columnname3 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring3) = "" Then
                                    columnnamestring3 = "0"
                                Else
                                    columnnamestring3 = columnnamestring3 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                        If Trim(Request("wheredata")) <> "" Then
                            If InStr(1, "," & Replace(UCase(columnname4), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname4), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname4 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname4 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & columnname4
                            End If
                        Else
                            If InStr(1, "," & Replace(UCase(columnname4), " ", "") & ",", ",MONTH,") > 0 Then
                                strOrderBy = Replace("," & Replace(UCase(columnname4), " ", "") & ",", ",MONTH,", ",intMonth,")
                                strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                                strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                                strQryMod = "select distinct(" & columnname4 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                            Else
                                strQryMod = "select distinct(" & columnname4 & ") from " & Trim(Request("hidtablename")) & " order by " & columnname4
                            End If
                        End If
                        cmdMod = New SqlCommand(strQryMod, connection)
                        connection.Open()
                        rdrModules = cmdMod.ExecuteReader
                        While rdrModules.Read
                            If Trim(rdrModules("" & columnname4 & "").ToString) <> "" Then
                                If Trim(columnnamestring4) = "" Then
                                    columnnamestring4 = Trim(rdrModules("" & columnname4 & "").ToString)
                                Else
                                    columnnamestring4 = columnnamestring4 & "$" & Trim(rdrModules("" & columnname4 & "").ToString)
                                End If
                            Else
                                If Trim(columnnamestring4) = "" Then
                                    columnnamestring4 = "0"
                                Else
                                    columnnamestring4 = columnnamestring4 & "$" & "0"
                                End If
                            End If
                        End While
                        connection.Close()
                    Else
                        Response.Write("Please reset and select only four field in column......!")
                        Response.End()
                    End If
                End If
                columnarr = Split(columnnamestring, "$")
                columnarr2 = Split(columnnamestring2, "$")
                columnarr3 = Split(columnnamestring3, "$")
                columnarr4 = Split(columnnamestring4, "$")

                Tcolumnarr = Split(columnnamestring, "$")
                Tcolumnarr2 = Split(columnnamestring2, "$")
                Tcolumnarr3 = Split(columnnamestring3, "$")
                Tcolumnarr3C = Split(columnnamestring3, "$")
                Tcolumnarr4 = Split(columnnamestring4, "$")
                Tcolumnarr4C = Split(columnnamestring4, "$")

                'Start- Code to declare dataset and table for chart
                Dim intCordinates
                Dim ds As DataSet = New DataSet
                Dim Table As DataTable = ds.Tables.Add("Table")
                Table.Columns.Add("Fields")
                If columnnamearr.Length = 1 Then
                    For intCordinates = 0 To columnarr.Length - 1
                        Table.Columns.Add(columnarr(intCordinates), GetType(System.Double))
                    Next
                ElseIf columnnamearr.Length = 2 Then
                    For intCordinates = 0 To columnarr2.Length - 1
                        Table.Columns.Add(columnarr2(intCordinates), GetType(System.Double))
                    Next
                ElseIf columnnamearr.Length = 3 Then
                    For intCordinates = 0 To columnarr3.Length - 1
                        Table.Columns.Add(columnarr3(intCordinates), GetType(System.Double))
                    Next
                ElseIf columnnamearr.Length = 4 Then
                    For intCordinates = 0 To columnarr4.Length - 1
                        Table.Columns.Add(columnarr4(intCordinates), GetType(System.Double))
                    Next
                End If

                'End - Code to declare dataset and table for chart

                ' start code for 1 level row data  form here 
                '****************************************
                If rowdataarr.Length = 1 Then
                    'ReDim Preserve arrGrandRowTotal(columnarr4.Length * columnarr3.Length * (columnarr2.Length - 1) * (columnarr.Length - 1))
                    rowdata = Trim(rowdataarr(0))
                    ''Dim rowarr = Split(rowdata, ",")
                    ''Dim ri As Integer
                    ''rowdata = ""
                    ''For ri = 0 To rowarr.length - 1
                    ''    If rowdata = "" Then
                    ''        rowdata = "isnull(" & rowarr(ri) & ",0)"
                    ''    Else
                    ''        rowdata = rowdata & "," & "isnull(" & rowarr(ri) & ",0)"
                    ''    End If
                    ''Next
                    rowhtmlstring1 = ""
                    Dim strsql1 As String = ""
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & rowdata
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata
                        End If
                    End If
                    Dim sqlcmd As New SqlCommand(strsql1, connection)
                    connection.Open()
                    sqlrsdata = sqlcmd.ExecuteReader
                    If sqlrsdata.HasRows Then
                        While sqlrsdata.Read
                            ' start code for data field form here 
                            '****************************************
                            '****************************************
                            For smm = 0 To showdataarr.Length - 1
                                showdata = showdataarr(smm)
                                If drowdata <> Trim(sqlrsdata("" & rowdata & "").ToString) Then
                                    drowdata = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    rowhtmlstring = rowhtmlstring & "<tr><td valign=top align=left rowspan=" & showdataarr.Length & " >" & Trim(sqlrsdata("" & rowdata & "").ToString) & "</td>"
                                Else
                                    rowhtmlstring = rowhtmlstring & "<tr>"
                                End If
                                rowhtmlstring1 = rowhtmlstring1 & "$" & Trim(sqlrsdata("" & rowdata & "").ToString)
                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Left(showdata, Len(showdata) - 1), "(", " of ") & "</td>"
                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Left(showdata, Len(showdata) - 1), "(", " of ")
                                ' start code for column field form here 
                                '****************************************
                                '****************************************
                                grtotal = 0.0
                                If columnnamearr.Length = 1 Then
                                    If Trim(Request("txtaFormula")) <> "" Then
                                        ReDim Preserve arrGrandRowTotal(3, columnarr.Length + 1)
                                        ReDim Preserve arrGrandRowCount(3, columnarr.Length + 1)
                                    Else
                                        ReDim Preserve arrGrandRowTotal(3, columnarr.Length)
                                        ReDim Preserve arrGrandRowCount(3, columnarr.Length)
                                    End If
                                    'Dim row1 As DataRow = Table.NewRow
                                    intGrandRowTotal = 0
                                    subtot1 = 0.0
                                    formulaarr2 = Split(Trim(Request("txtaFormula")), "$")
                                    formulaarr3 = Split(Trim(Request("txtaFormula")), "$")
                                    'Start - Code to define new row and fill data 
                                    Dim objRow As DataRow = Table.NewRow
                                    objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    'End - Code to define new row and fill data
                                    intSubColTotal = 0
                                    Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    If strRowData = "" Then
                                        strRowData = 0
                                    End If
                                    For cm = 0 To columnarr.Length - 1
                                        '****************************************
                                        If Trim(columnarr(cm)) <> "" Then
                                            If Trim(Request("wheredata")) <> "" Then
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,5)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                            Else
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,5)) as vdata,count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                            End If
                                        Else
                                            If Trim(Request("wheredata")) <> "" Then
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,5)) as vdata,count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                            Else
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,5)) as vdata,count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                            End If
                                        End If
                                        Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                        connection1.Open()
                                        sqlrsdatac = sqlcmdc.ExecuteReader
                                        While sqlrsdatac.Read
                                            'Start - Code to fill data 
                                            If columnarr(cm) <> "" Then
                                                objRow(columnarr(cm)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                            End If
                                            'End - Code to fill data 
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                            'Response.Write(CDbl(sqlrsdatac("vdata")) & "-" & CDbl(sqlrsdatac("cdata")) & "<BR>")
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                subtot1 = CDbl(subtot1) + (CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString))
                                            Else
                                                subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                            End If
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + (CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString))
                                            Else
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                            End If
                                            If CDbl(sqlrsdatac("vdata")) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            'chart value -start 
                                            'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                            'chart value -end  
                                            For fm = 0 To formulaarr.Length - 1
                                                If columnarr(cm) = formulaarr(fm) Then
                                                    formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                    formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                End If
                                            Next
                                            If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                End If
                                            End If
                                        End While
                                        connection1.Close()
                                        '****************************************
                                    Next
                                    'Start - Code to add row in the table 
                                    Table.Rows.Add(objRow)
                                    'End - Code to add row in the table 
                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                        If intSubColTotal > 0 Then
                                            'Response.Write(subtot1 & "/" & intSubColTotal)
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                        End If
                                    Else
                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                    End If
                                    If CDbl(subtot1) > 0.0 Then
                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                        End If
                                    End If
                                    intGrandRowTotal = intGrandRowTotal + 1
                                    ' Start Formula for column field form here 
                                    strfrumala = ""
                                    If Trim(Request("txtaFormula")) <> "" Then
                                        For fm = 0 To formulaarr.Length - 1
                                            strfrumala = strfrumala + formulaarr2(fm)
                                        Next
                                        If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                        End If
                                        strfrumala = ""
                                        For fm = 0 To formulaarr.Length - 1
                                            strfrumala = strfrumala + formulaarr3(fm)
                                        Next
                                        arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                        'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                        arrGrandRowCount(smm, intGrandRowTotal) = 0
                                        intGrandRowTotal = intGrandRowTotal + 1
                                        ''chart value -start 
                                        'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                        ''chart value -end  
                                    End If
                                    ' end formula for column field form here 
                                ElseIf columnnamearr.Length = 2 Then
                                    If Trim(Request("txtaFormula")) <> "" Then
                                        ReDim Preserve arrGrandRowTotal(3, (CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1)) + columnarr2.Length)
                                        ReDim Preserve arrGrandRowCount(3, (CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1)) + columnarr2.Length)
                                    Else
                                        ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                        ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                    End If
                                    intGrandRowTotal = 0
                                    Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    If strRowData = "" Then
                                        strRowData = 0
                                    End If
                                    For cm = 0 To columnarr.Length - 1
                                        subtot1 = 0
                                        'Start - Code to define new row and fill data 
                                        Dim objRow As DataRow = Table.NewRow
                                        objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & columnarr(cm)
                                        'End - Code to define new row and fill data
                                        intSubColTotal = 0
                                        For cm2 = 0 To columnarr2.Length - 1
                                            '****************************************
                                            If Trim(Request("wheredata")) <> "" Then
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata  from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                            Else
                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata  from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                            End If
                                            'Response.Write(columnarr(cm) & "-" & strsql1c & "<br>")
                                            '''''Check Data'''''
                                            Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                            connection1.Open()
                                            sqlrsdatac = sqlcmdc.ExecuteReader
                                            While sqlrsdatac.Read
                                                'Start - Code to fill data 
                                                objRow(columnarr2(cm2)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                'End - Code to fill data 
                                                'rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", "") & "-" & cm & "-" & cm2 & "</td>"
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                Else
                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                End If
                                                If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                'chart value -start 
                                                'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                                'chart value -end  
                                                For fm = 0 To formulaarr.Length - 1
                                                    If columnarr2(cm2) = formulaarr(fm) Then
                                                        formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                        formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                    End If
                                                Next
                                                If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                    End If
                                                End If
                                            End While
                                            connection1.Close()
                                            '****************************************
                                        Next
                                        'Start - Code to add row in the table 
                                        Table.Rows.Add(objRow)
                                        'End - Code to add row in the table 
                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                            If intSubColTotal > 0 Then
                                                'Response.Write(subtot1 & "/" & intSubColTotal)
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                            End If
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                        End If
                                        If CDbl(subtot1) > 0.0 Then
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                            End If
                                        End If
                                        intGrandRowTotal = intGrandRowTotal + 1
                                        ' Start Formula for column field form here 
                                        strfrumala = ""
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            For fm = 0 To formulaarr.Length - 1
                                                strfrumala = strfrumala + formulaarr2(fm)
                                            Next
                                            If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                            End If
                                            strfrumala = ""
                                            For fm = 0 To formulaarr.Length - 1
                                                strfrumala = strfrumala + formulaarr3(fm)
                                            Next
                                            arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                            'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                            arrGrandRowCount(smm, intGrandRowTotal) = 0
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            ''chart value -start 
                                            'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                            ''chart value -end  
                                        End If
                                        ' end formula for column field form here 
                                    Next
                                ElseIf columnnamearr.Length = 3 Then
                                    If Trim(Request("txtaFormula")) <> "" Then
                                        ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                        ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                    Else
                                        ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                        ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                    End If
                                    intGrandRowTotal = 0
                                    Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    If strRowData = "" Then
                                        strRowData = 0
                                    End If
                                    For cm = 0 To columnarr.Length - 1
                                        For tcm3 = 0 To Tcolumnarr3.Length - 1
                                            Tcolumnarr3(tcm3) = 0
                                            Tcolumnarr3C(tcm3) = 0
                                        Next
                                        For cm2 = 0 To columnarr2.Length - 1
                                            subtot1 = 0
                                            'Start - Code to define new row and fill data 
                                            Dim objRow As DataRow = Table.NewRow
                                            objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & columnarr(cm) & "-" & columnarr2(cm2)
                                            'End - Code to define new row and fill data
                                            intSubColTotal = 0
                                            For cm3 = 0 To columnarr3.Length - 1
                                                '****************************************
                                                If Trim(Request("wheredata")) <> "" Then
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                Else
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata  from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                                End If
                                                'Response.Write(columnarr(cm) & "-" & strsql1c & "<br>")
                                                Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                connection1.Open()
                                                sqlrsdatac = sqlcmdc.ExecuteReader
                                                While sqlrsdatac.Read
                                                    'Start - Code to fill data 
                                                    objRow(columnarr3(cm3)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                    'End - Code to fill data 
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    Else
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                    End If
                                                    If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    'chart value -start 
                                                    'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                                    'chart value -end  
                                                    'Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata"))
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        Tcolumnarr3C(cm3) = Tcolumnarr3C(cm3) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                    Else
                                                        Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                    End If
                                                    For fm = 0 To formulaarr.Length - 1
                                                        If columnarr3(cm3) = formulaarr(fm) Then
                                                            formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                            formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                        End If
                                                    Next
                                                    If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                        End If
                                                    End If
                                                End While
                                                connection1.Close()
                                                '****************************************
                                            Next
                                            'Start - Code to add row in the table 
                                            Table.Rows.Add(objRow)
                                            'End - Code to add row in the table 
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If intSubColTotal > 0 Then
                                                    'Response.Write(subtot1 & "/" & intSubColTotal)
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                            End If
                                            If CDbl(subtot1) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            ' Start Formula for column field form here 
                                            strfrumala = ""
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr2(fm)
                                                Next
                                                If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                End If
                                                strfrumala = ""
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr3(fm)
                                                Next
                                                arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                ''chart value -start 
                                                'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & "Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                                ''chart value -end  
                                            End If
                                            ' end formula for column field form here 
                                        Next
                                        '****************************************
                                        '****************************************
                                        ' for  total 
                                        Tcolstring = ""
                                        TColCountString = ""
                                        For cm3 = 0 To columnarr3.Length - 1
                                            Tcolstring = Tcolstring & "+" & Tcolumnarr3(cm3)
                                            TColCountString = TColCountString & "+" & Tcolumnarr3C(cm3)
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If calvalue(Tcolumnarr3C(cm3)) > 0 Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "")
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "")
                                            End If
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr3(cm3)))
                                            If CDbl(calvalue(Tcolumnarr3(cm3))) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            For fm = 0 To formulaarr.Length - 1
                                                If columnarr3(cm3) = formulaarr(fm) Then
                                                    formulaarr2(fm) = calvalue(Tcolumnarr3(cm3))
                                                    formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                End If
                                            Next
                                        Next
                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                            If calvalue(TColCountString) > 0 Then
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                            End If
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                        End If
                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                        If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                            End If
                                        End If
                                        intGrandRowTotal = intGrandRowTotal + 1
                                        strfrumala = ""
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            For fm = 0 To formulaarr.Length - 1
                                                strfrumala = strfrumala + formulaarr2(fm)
                                            Next
                                            If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                            End If
                                            strfrumala = ""
                                            For fm = 0 To formulaarr.Length - 1
                                                strfrumala = strfrumala + formulaarr3(fm)
                                            Next
                                            arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                            'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                            arrGrandRowCount(smm, intGrandRowTotal) = 0
                                            intGrandRowTotal = intGrandRowTotal + 1
                                        End If
                                        '****************************************
                                        '****************************************
                                        ' for  total 
                                    Next
                                ElseIf columnnamearr.Length = 4 Then
                                    If Trim(Request("txtaFormula")) <> "" Then
                                        ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                        ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                    Else
                                        ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                        ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                    End If
                                    intGrandRowTotal = 0
                                    Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                    If strRowData = "" Then
                                        strRowData = 0
                                    End If
                                    For cm = 0 To columnarr.Length - 1
                                        For cm2 = 0 To columnarr2.Length - 1
                                            For tcm3 = 0 To Tcolumnarr4.Length - 1
                                                Tcolumnarr4(tcm3) = 0
                                                Tcolumnarr4C(tcm3) = 0
                                            Next
                                            For cm3 = 0 To columnarr3.Length - 1
                                                subtot1 = 0
                                                'Start - Code to define new row and fill data 
                                                Dim objRow As DataRow = Table.NewRow
                                                objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3)
                                                'End - Code to define new row and fill data
                                                intSubColTotal = 0
                                                For cm4 = 0 To columnarr4.Length - 1
                                                    '****************************************
                                                    If Trim(Request("wheredata")) <> "" Then
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & columnname4 & ",0)='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                    Else
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & columnname4 & ",0)='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                                    End If
                                                    'Response.Write(columnarr(cm) & "-" & strsql1c & "<br>")
                                                    Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                    connection1.Open()
                                                    sqlrsdatac = sqlcmdc.ExecuteReader
                                                    While sqlrsdatac.Read
                                                        'Start - Code to fill data 
                                                        objRow(columnarr4(cm4)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        'End - Code to fill data 
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        Else
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                        End If
                                                        If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                            End If
                                                        End If
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        'chart value -start 
                                                        'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                                        'chart value -end  
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            Tcolumnarr4C(cm4) = Tcolumnarr4C(cm4) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                        Else
                                                            Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                        End If
                                                        For fm = 0 To formulaarr.Length - 1
                                                            If columnarr4(cm4) = formulaarr(fm) Then
                                                                formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                            End If
                                                        Next
                                                        If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                            End If
                                                        End If
                                                    End While
                                                    connection1.Close()
                                                    '****************************************
                                                Next
                                                'Start - Code to add row in the table 
                                                Table.Rows.Add(objRow)
                                                'End - Code to add row in the table 
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If intSubColTotal > 0 Then
                                                        'Response.Write(subtot1 & "/" & intSubColTotal)
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                End If
                                                If CDbl(subtot1) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                ' Start Formula for column field form here 
                                                strfrumala = ""
                                                If Trim(Request("txtaFormula")) <> "" Then
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr2(fm)
                                                    Next
                                                    If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                    End If
                                                    strfrumala = ""
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr3(fm)
                                                    Next
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                    arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    ''chart value -start 
                                                    'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4) & "-" & "Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                                    ''chart value -end  
                                                End If
                                                ' end formula for column field form here 
                                            Next
                                            '****************************************
                                            '****************************************
                                            ' for  total 
                                            Tcolstring = ""
                                            TColCountString = ""
                                            For cm4 = 0 To columnarr4.Length - 1
                                                Tcolstring = Tcolstring & "+" & Tcolumnarr4(cm4)
                                                TColCountString = TColCountString & "+" & Tcolumnarr4C(cm4)
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If calvalue(Tcolumnarr4C(cm4)) > 0 Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "")
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "")
                                                End If
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr4(cm4)))
                                                If CDbl(calvalue(Tcolumnarr4(cm4))) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                For fm = 0 To formulaarr.Length - 1
                                                    If columnarr4(cm4) = formulaarr(fm) Then
                                                        formulaarr2(fm) = calvalue(Tcolumnarr4(cm4))
                                                        formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                    End If
                                                Next
                                            Next
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If calvalue(TColCountString) > 0 Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                            End If
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                            If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            strfrumala = ""
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr2(fm)
                                                Next
                                                If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                    rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                End If
                                                strfrumala = ""
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr3(fm)
                                                Next
                                                arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                intGrandRowTotal = intGrandRowTotal + 1
                                            End If
                                            '****************************************
                                            '****************************************
                                            ' for  total 
                                        Next
                                    Next
                                End If
                                ' end code for column field form here 
                                '****************************************
                                'rowhtmlstring = rowhtmlstring & "<td align=center>" & grtotal & "</td>"
                                '****************************************
                                rowhtmlstring = rowhtmlstring & "</tr>"
                                'rowhtmlstring1 = rowhtmlstring1 & "$"
                            Next
                            ' end  code for data field form here 
                            '****************************************
                            '****************************************
                        End While
                    Else
                        Response.Write("No record found")
                        Exit Sub
                    End If
                    connection.Close()
                    ' end code for 1 level row data  form here 
                    '****************************************
                ElseIf rowdataarr.Length = 2 Then '*************************************************************************
                    ' start code for 2 level row data  form here 
                    '****************************************
                    rowdata = Trim(rowdataarr(0))
                    rowdata1 = Trim(rowdataarr(1))
                    Dim str2row As String
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata1 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & "  order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata1 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & "  order by " & rowdata1
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata1 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata1 & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata1
                        End If
                    End If
                    Dim str2rowcmdMod As New SqlCommand(str2row, connection3)
                    connection3.Open()
                    str2rowrdrModules = str2rowcmdMod.ExecuteReader
                    While str2rowrdrModules.Read
                        ' Trim(str2rowrdrModules("" & columnname & ""))
                        If Trim(str2rowrdrModules("" & rowdata1 & "").ToString) <> "" Then
                            If Trim(rowdata1value) = "" Then
                                rowdata1value = Trim(str2rowrdrModules("" & rowdata1 & "").ToString)
                            Else
                                rowdata1value = rowdata1value & "$" & Trim(str2rowrdrModules("" & rowdata1 & "").ToString)
                            End If
                        Else
                            If Trim(rowdata1value) = "" Then
                                rowdata1value = "0"
                            Else
                                rowdata1value = rowdata1value & "$" & "0"
                            End If
                        End If
                    End While
                    connection3.Close()
                    rowdata1arr = Split(Trim(rowdata1value), "$")
                    '****************************************
                    'dim sqlrsdatac As SqlDataReader
                    ' Dim sqlrsdata As SqlDataReader
                    rowhtmlstring = ""
                    rowhtmlstring1 = ""
                    drowdata = ""
                    dshowdata = ""
                    Dim strsql1 As String = ""
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & rowdata
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata
                        End If
                    End If
                    Dim sqlcmd As New SqlCommand(strsql1, connection)
                    connection.Open()
                    sqlrsdata = sqlcmd.ExecuteReader
                    If sqlrsdata.HasRows Then
                        While sqlrsdata.Read
                            ' start code for data field form here 
                            '****************************************
                            '****************************************
                            For rmm = 0 To rowdata1arr.Length - 1
                                rowdata1value = rowdata1arr(rmm)
                                dshowdata = ""
                                'Start second rwo data *****************
                                'Start second rwo data *****************
                                For smm = 0 To showdataarr.Length - 1
                                    showdata = showdataarr(smm)
                                    If drowdata <> Trim(sqlrsdata("" & rowdata & "").ToString) Then
                                        drowdata = Trim(sqlrsdata("" & rowdata & "").ToString)
                                        rowhtmlstring = rowhtmlstring & "<tr ><td align=left valign=top rowspan=" & (showdataarr.Length * rowdata1arr.Length) & " >" & Trim(sqlrsdata("" & rowdata & "").ToString) & "</td>"
                                    Else
                                        rowhtmlstring = rowhtmlstring & "<tr>"
                                    End If
                                    rowhtmlstring1 = rowhtmlstring1 & "$" & Trim(sqlrsdata("" & rowdata & "").ToString)
                                    Dim row1
                                    If dshowdata <> rowdata1value Then
                                        dshowdata = rowdata1value
                                        If rowdata1value = "0" Then
                                            row1 = " "
                                        Else
                                            row1 = rowdata1value
                                        End If
                                        rowhtmlstring = rowhtmlstring & "<td align=left valign=top rowspan=" & showdataarr.Length & " >" & row1 & "&nbsp;</td>"
                                    Else
                                        rowhtmlstring = rowhtmlstring & ""
                                    End If
                                    rowhtmlstring1 = rowhtmlstring1 & "," & row1

                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Left(showdata, Len(showdata) - 1), "(", " of ") & "</td>"
                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Left(showdata, Len(showdata) - 1), "(", " of ")
                                    ' start code for column field form here 
                                    '****************************************
                                    '****************************************
                                    grtotal = 0
                                    subtot1 = 0
                                    If columnnamearr.Length = 1 Then
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            ReDim Preserve arrGrandRowTotal(3, columnarr.Length + 1)
                                            ReDim Preserve arrGrandRowCount(3, columnarr.Length + 1)
                                        Else
                                            ReDim Preserve arrGrandRowTotal(3, columnarr.Length)
                                            ReDim Preserve arrGrandRowCount(3, columnarr.Length)
                                        End If
                                        intGrandRowTotal = 0
                                        'Start - Code to define new row and fill data 
                                        Dim objRow As DataRow = Table.NewRow
                                        objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value
                                        'End - Code to define new row and fill data
                                        Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                        If strRowData = "" Then
                                            strRowData = 0
                                        End If
                                        For cm = 0 To columnarr.Length - 1
                                            '****************************************
                                            If Trim(columnarr(cm)) <> "" Then
                                                If Trim(Request("wheredata")) <> "" Then
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                Else
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and  isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'" & RemoveFunction(showdata)
                                                End If
                                            Else
                                                If Trim(Request("wheredata")) <> "" Then
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where  isnull(" & rowdata & ",0)='" & strRowData & "' and " & rowdata1 & "='" & Trim(rowdata1value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                Else
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where  isnull(" & rowdata & ",0)='" & strRowData & "' and " & rowdata1 & "='" & Trim(rowdata1value) & "'" & RemoveFunction(showdata)
                                                End If
                                            End If
                                            Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                            connection1.Open()
                                            sqlrsdatac = sqlcmdc.ExecuteReader
                                            While sqlrsdatac.Read
                                                'Start - Code to fill data 
                                                If columnarr(cm) <> "" Then
                                                    objRow(columnarr(cm)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                End If
                                                'End - Code to fill data 
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                Else
                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                End If
                                                If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                For fm = 0 To formulaarr.Length - 1
                                                    If columnarr(cm) = formulaarr(fm) Then
                                                        formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                        formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                    End If
                                                Next
                                            End While
                                            connection1.Close()
                                            '****************************************
                                        Next
                                        'Start - Code to add row in the table 
                                        Table.Rows.Add(objRow)
                                        'End - Code to add row in the table 
                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                            If intSubColTotal > 0 Then
                                                'Response.Write(subtot1 & "/" & intSubColTotal)
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                            End If
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                        End If
                                        If CDbl(subtot1) > 0.0 Then
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                            End If
                                        End If
                                        intGrandRowTotal = intGrandRowTotal + 1
                                    ElseIf columnnamearr.Length = 2 Then
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            ReDim Preserve arrGrandRowTotal(3, (CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1)) + 1)
                                            ReDim Preserve arrGrandRowCount(3, (CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1)) + 1)
                                        Else
                                            ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                            ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                        End If
                                        intGrandRowTotal = 0
                                        Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                        If strRowData = "" Then
                                            strRowData = 0
                                        End If
                                        For cm = 0 To columnarr.Length - 1
                                            subtot1 = 0
                                            'Start - Code to define new row and fill data 
                                            Dim objRow As DataRow = Table.NewRow
                                            objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value & "-" & columnarr(cm)
                                            'End - Code to define new row and fill data
                                            For cm2 = 0 To columnarr2.Length - 1
                                                '****************************************
                                                If Trim(Request("wheredata")) <> "" Then
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                Else
                                                    strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'" & RemoveFunction(showdata)
                                                End If
                                                Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                connection1.Open()
                                                sqlrsdatac = sqlcmdc.ExecuteReader
                                                While sqlrsdatac.Read
                                                    'Start - Code to fill data 
                                                    objRow(columnarr2(cm2)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", "")
                                                    'End - Code to fill data 
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    Else
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                    End If
                                                    If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    For fm = 0 To formulaarr.Length - 1
                                                        If columnarr2(cm2) = formulaarr(fm) Then
                                                            formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                            formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                        End If
                                                    Next
                                                End While
                                                connection1.Close()
                                                '****************************************
                                            Next
                                            'Start - Code to add row in the table 
                                            Table.Rows.Add(objRow)
                                            'End - Code to add row in the table
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If intSubColTotal > 0 Then
                                                    'Response.Write(subtot1 & "/" & intSubColTotal)
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                            End If
                                            If CDbl(subtot1) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            ' Start Formula for column field form here 
                                            strfrumala = ""
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr2(fm)
                                                Next
                                                If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                End If
                                                strfrumala = ""
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr3(fm)
                                                Next
                                                arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                intGrandRowTotal = intGrandRowTotal + 1
                                            End If
                                            ' end formula for column field form here 
                                        Next
                                    ElseIf columnnamearr.Length = 3 Then
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                            ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                        Else
                                            ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                            ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                        End If
                                        intGrandRowTotal = 0
                                        Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                        If strRowData = "" Then
                                            strRowData = 0
                                        End If
                                        For cm = 0 To columnarr.Length - 1
                                            For tcm3 = 0 To Tcolumnarr3.Length - 1
                                                Tcolumnarr3(tcm3) = 0
                                                Tcolumnarr3C(tcm3) = 0
                                            Next
                                            For cm2 = 0 To columnarr2.Length - 1
                                                subtot1 = 0
                                                'Start - Code to define new row and fill data 
                                                Dim objRow As DataRow = Table.NewRow
                                                objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value & "-" & columnarr(cm) & "-" & columnarr2(cm2)
                                                'End - Code to define new row and fill data
                                                For cm3 = 0 To columnarr3.Length - 1
                                                    '****************************************
                                                    If Trim(Request("wheredata")) <> "" Then
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                    Else
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'" & RemoveFunction(showdata)
                                                    End If
                                                    Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                    connection1.Open()
                                                    sqlrsdatac = sqlcmdc.ExecuteReader
                                                    While sqlrsdatac.Read
                                                        'Start - Code to fill data 
                                                        objRow(columnarr3(cm3)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        'End - Code to fill data 
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        Else
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                        End If
                                                        If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                            End If
                                                        End If
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        'Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata"))
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            Tcolumnarr3C(cm3) = Tcolumnarr3C(cm3) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                        Else
                                                            Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                        End If
                                                        For fm = 0 To formulaarr.Length - 1
                                                            If columnarr3(cm3) = formulaarr(fm) Then
                                                                formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                            End If
                                                        Next
                                                    End While
                                                    connection1.Close()
                                                    '****************************************
                                                Next
                                                'Start - Code to add row in the table 
                                                Table.Rows.Add(objRow)
                                                'End - Code to add row in the table
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If intSubColTotal > 0 Then
                                                        'Response.Write(subtot1 & "/" & intSubColTotal)
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                End If
                                                If CDbl(subtot1) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                ' Start Formula for column field form here 
                                                strfrumala = ""
                                                If Trim(Request("txtaFormula")) <> "" Then
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr2(fm)
                                                    Next
                                                    If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                    End If
                                                    strfrumala = ""
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr3(fm)
                                                    Next
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                    arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                End If
                                                ' end formula for column field form here 
                                            Next
                                            '****************************************
                                            '****************************************
                                            ' for  total 
                                            Tcolstring = ""
                                            TColCountString = ""
                                            For cm3 = 0 To columnarr3.Length - 1
                                                Tcolstring = Tcolstring & "+" & Tcolumnarr3(cm3)
                                                TColCountString = TColCountString & "+" & Tcolumnarr3C(cm3)
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If calvalue(Tcolumnarr3C(cm3)) > 0 Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "")
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "")
                                                End If
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr3(cm3)))
                                                If CDbl(calvalue(Tcolumnarr3(cm3))) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                For fm = 0 To formulaarr.Length - 1
                                                    If columnarr3(cm3) = formulaarr(fm) Then
                                                        formulaarr2(fm) = calvalue(Tcolumnarr3(cm3))
                                                        formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                    End If
                                                Next
                                            Next
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If calvalue(TColCountString) > 0 Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                            End If
                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                            If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                            strfrumala = ""
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr2(fm)
                                                Next
                                                If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                End If
                                                strfrumala = ""
                                                For fm = 0 To formulaarr.Length - 1
                                                    strfrumala = strfrumala + formulaarr3(fm)
                                                Next
                                                arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                intGrandRowTotal = intGrandRowTotal + 1
                                            End If
                                            '****************************************
                                            '****************************************
                                            ' for  total 
                                        Next
                                    ElseIf columnnamearr.Length = 4 Then
                                        If Trim(Request("txtaFormula")) <> "" Then
                                            ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                            ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                        Else
                                            ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                            ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                        End If
                                        intGrandRowTotal = 0
                                        Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                        If strRowData = "" Then
                                            strRowData = 0
                                        End If
                                        For cm = 0 To columnarr.Length - 1
                                            For cm2 = 0 To columnarr2.Length - 1
                                                For tcm3 = 0 To Tcolumnarr4.Length - 1
                                                    Tcolumnarr4(tcm3) = 0
                                                    Tcolumnarr4C(tcm3) = 0
                                                Next
                                                For cm3 = 0 To columnarr3.Length - 1
                                                    subtot1 = 0
                                                    'Start - Code to define new row and fill data 
                                                    Dim objRow As DataRow = Table.NewRow
                                                    objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3)
                                                    'End - Code to define new row and fill data
                                                    intSubColTotal = 0
                                                    For cm4 = 0 To columnarr4.Length - 1
                                                        '****************************************
                                                        If Trim(Request("wheredata")) <> "" Then
                                                            strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname & ",0)='" & columnarr2(cm2) & "' and " & columnname3 & "='" & columnarr3(cm3) & "' and " & columnname4 & "='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                        Else
                                                            strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & columnname4 & ",0)='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                                        End If
                                                        'Response.Write(columnarr(cm) & "-" & strsql1c & "<br>")
                                                        Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                        connection1.Open()
                                                        sqlrsdatac = sqlcmdc.ExecuteReader
                                                        While sqlrsdatac.Read
                                                            'Start - Code to fill data 
                                                            objRow(columnarr4(cm4)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                            'End - Code to fill data 
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            Else
                                                                grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                                subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                            End If
                                                            If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                                End If
                                                            End If
                                                            intGrandRowTotal = intGrandRowTotal + 1
                                                            'chart value -start 
                                                            'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                                            'chart value -end  
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                Tcolumnarr4C(cm4) = Tcolumnarr4C(cm4) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                            Else
                                                                Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                            End If
                                                            For fm = 0 To formulaarr.Length - 1
                                                                If columnarr4(cm4) = formulaarr(fm) Then
                                                                    formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                    formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                                End If
                                                            Next
                                                            If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                    intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                                End If
                                                            End If
                                                        End While
                                                        connection1.Close()
                                                        '****************************************
                                                    Next
                                                    'Start - Code to add row in the table 
                                                    Table.Rows.Add(objRow)
                                                    'End - Code to add row in the table 
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        If intSubColTotal > 0 Then
                                                            'Response.Write(subtot1 & "/" & intSubColTotal)
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                        End If
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                    End If
                                                    If CDbl(subtot1) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    ' Start Formula for column field form here 
                                                    strfrumala = ""
                                                    If Trim(Request("txtaFormula")) <> "" Then
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr2(fm)
                                                        Next
                                                        If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                        End If
                                                        strfrumala = ""
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr3(fm)
                                                        Next
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                        'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                        arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        ''chart value -start 
                                                        'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4) & "-" & "Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                                        ''chart value -end  
                                                    End If
                                                    ' end formula for column field form here 
                                                Next
                                                '****************************************
                                                '****************************************
                                                ' for  total 
                                                Tcolstring = ""
                                                TColCountString = ""
                                                For cm4 = 0 To columnarr4.Length - 1
                                                    Tcolstring = Tcolstring & "+" & Tcolumnarr4(cm4)
                                                    TColCountString = TColCountString & "+" & Tcolumnarr4C(cm4)
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        If calvalue(Tcolumnarr4C(cm4)) > 0 Then
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "")
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                        End If
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "")
                                                    End If
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr4(cm4)))
                                                    If CDbl(calvalue(Tcolumnarr4(cm4))) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    For fm = 0 To formulaarr.Length - 1
                                                        If columnarr4(cm4) = formulaarr(fm) Then
                                                            formulaarr2(fm) = calvalue(Tcolumnarr4(cm4))
                                                            formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                        End If
                                                    Next
                                                Next
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If calvalue(TColCountString) > 0 Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                                End If
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                                If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                strfrumala = ""
                                                If Trim(Request("txtaFormula")) <> "" Then
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr2(fm)
                                                    Next
                                                    If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                        rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                    End If
                                                    strfrumala = ""
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr3(fm)
                                                    Next
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                    arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                End If
                                                '****************************************
                                                '****************************************
                                                ' for  total 
                                            Next
                                        Next
                                    End If
                                    ' end code for column field form here 
                                    '****************************************
                                    ' Start Formula for column field form here 
                                    'rowhtmlstring = rowhtmlstring & "<td align=center>" & grtotal & "</td>"
                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(grtotal)
                                    'intGrandRowTotal = intGrandRowTotal + 1
                                    rowhtmlstring = rowhtmlstring & "</tr>"
                                    'rowhtmlstring1 = rowhtmlstring1 & "$"
                                    ' end formula for column field form here 
                                    '****************************************
                                Next
                                'end  second rwo data *****************
                                'end  second rwo data *****************
                                rowhtmlstring = rowhtmlstring & "</tr>"
                                'rowhtmlstring1 = rowhtmlstring1 & "$"
                            Next
                            ' end  code for data field form here 
                            '****************************************
                            '****************************************
                        End While
                    Else
                        Response.Write("No record found")
                        Exit Sub
                    End If
                    connection.Close()
                    ' end code for 2 level row data  form here 
                    '****************************************
                ElseIf rowdataarr.Length = 3 Then '*************************************************************************
                    ' start code for 3 level row data  form here 
                    '****************************************
                    rowdata = Trim(rowdataarr(0))
                    rowdata1 = Trim(rowdataarr(1))
                    rowdata3 = Trim(rowdataarr(2))
                    Dim str2row As String = ""
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata1 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & "  order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata1 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & "  order by " & rowdata1
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata1), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata1 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata1 & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata1
                        End If
                    End If
                    Dim str2rowcmdMod As New SqlCommand(str2row, connection3)
                    connection3.Open()
                    str2rowrdrModules = str2rowcmdMod.ExecuteReader
                    While str2rowrdrModules.Read
                        ' Trim(str2rowrdrModules("" & columnname & ""))
                        If Trim(str2rowrdrModules("" & rowdata1 & "").ToString) <> "" Then
                            If Trim(rowdata1value) = "" Then
                                rowdata1value = Trim(str2rowrdrModules("" & rowdata1 & "").ToString)
                            Else
                                rowdata1value = rowdata1value & "$" & Trim(str2rowrdrModules("" & rowdata1 & "").ToString)
                            End If
                        Else
                            If Trim(rowdata1value) = "" Then
                                rowdata1value = "0"
                            Else
                                rowdata1value = rowdata1value & "$" & "0"
                            End If
                        End If
                    End While
                    connection3.Close()
                    rowdata1arr = Split(Trim(rowdata1value), "$")
                    'str2row = "select distinct(" & rowdata3 & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata3
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata3), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata3), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata3 & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & rowdata3
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata3), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata3), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            str2row = "select distinct(" & rowdata3 & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            str2row = "select distinct(" & rowdata3 & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata3
                        End If
                    End If
                    str2rowcmdMod = New SqlCommand(str2row, connection3)
                    connection3.Open()
                    str2rowrdrModules = str2rowcmdMod.ExecuteReader
                    While str2rowrdrModules.Read
                        If Trim(str2rowrdrModules("" & rowdata3 & "").ToString) <> "" Then
                            If Trim(rowdata3value) = "" Then
                                rowdata3value = Trim(str2rowrdrModules("" & rowdata3 & "").ToString)
                            Else
                                rowdata3value = rowdata3value & "$" & Trim(str2rowrdrModules("" & rowdata3 & "").ToString)
                            End If
                        Else
                            If Trim(rowdata3value) = "" Then
                                rowdata3value = "0"
                            Else
                                rowdata3value = rowdata3value & "$" & "0"
                            End If
                        End If
                    End While
                    connection3.Close()
                    rowdata3arr = Split(Trim(rowdata3value), "$")
                    '****************************************
                    'dim sqlrsdatac As SqlDataReader
                    ' Dim sqlrsdata As SqlDataReader
                    rowhtmlstring = ""
                    rowhtmlstring1 = ""
                    drowdata = ""
                    dshowdata = ""
                    Dim strsql1 As String = ""
                    If Trim(Request("wheredata")) <> "" Then
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " where " & Trim(Request("wheredata")) & " order by " & rowdata
                        End If
                    Else
                        If InStr(1, "," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,") > 0 Then
                            strOrderBy = Replace("," & Replace(UCase(rowdata), " ", "") & ",", ",MONTH,", ",intMonth,")
                            strOrderBy = Left(strOrderBy, strOrderBy.Length - 1)
                            strOrderBy = Right(strOrderBy, strOrderBy.Length - 1)
                            strsql1 = "select distinct(" & rowdata & "), intMonth = cast(CASE Upper(Month) WHEN 'JANUARY' THEN '1' WHEN 'FEBRUARY' THEN '2' WHEN 'MARCH' THEN '3' WHEN 'APRIL' THEN '4' WHEN 'MAY' THEN '5' WHEN 'JUNE' THEN '6' WHEN 'JULY' THEN '7' WHEN 'AUGUST' THEN '8' WHEN 'SEPTEMBER' THEN '9' WHEN 'OCTOBER' THEN '10' WHEN 'NOVEMBER' THEN '11' WHEN 'DECEMBER' THEN '12' END as numeric) from " & Trim(Request("hidtablename")) & " order by " & strOrderBy
                        Else
                            strsql1 = "select distinct(" & rowdata & ") from " & Trim(Request("hidtablename")) & " order by " & rowdata
                        End If
                    End If
                    Dim sqlcmd As New SqlCommand(strsql1, connection)
                    connection.Open()
                    sqlrsdata = sqlcmd.ExecuteReader
                    If sqlrsdata.HasRows Then
                        While sqlrsdata.Read
                            ' start code for data field form here 
                            '****************************************
                            '****************************************
                            For rmm = 0 To rowdata1arr.Length - 1
                                rowdata1value = rowdata1arr(rmm)
                                dshowdata = ""
                                For rmm3 = 0 To rowdata3arr.Length - 1
                                    rowdata3value = rowdata3arr(rmm3)
                                    drowdata3 = ""
                                    For smm = 0 To showdataarr.Length - 1
                                        showdata = showdataarr(smm)
                                        If drowdata <> Trim(sqlrsdata("" & rowdata & "").ToString) Then
                                            drowdata = Trim(sqlrsdata("" & rowdata & "").ToString)
                                            rowhtmlstring = rowhtmlstring & "<tr><td align=left valign=top rowspan=" & (showdataarr.Length * (rowdata1arr.Length * rowdata3arr.Length)) & " >" & Trim(sqlrsdata("" & rowdata & "").ToString) & "</td>"
                                        Else
                                            rowhtmlstring = rowhtmlstring & "<tr>"
                                        End If
                                        rowhtmlstring1 = rowhtmlstring1 & "$" & Trim(sqlrsdata("" & rowdata & "").ToString)
                                        Dim row1
                                        If dshowdata <> rowdata1value Then
                                            dshowdata = rowdata1value
                                            If rowdata1value = "0" Then
                                                row1 = " "
                                            Else
                                                row1 = rowdata1value
                                            End If
                                            rowhtmlstring = rowhtmlstring & "<td align=left valign=top rowspan=" & (showdataarr.Length * rowdata3arr.Length) & "> " & row1 & "&nbsp;</td>"
                                        Else
                                            rowhtmlstring = rowhtmlstring & ""
                                        End If
                                        rowhtmlstring1 = rowhtmlstring1 & "," & row1
                                        Dim row3
                                        If drowdata3 <> rowdata3value Then
                                            drowdata3 = rowdata3value
                                            If rowdata3value = "0" Then
                                                row3 = " "
                                            Else
                                                row3 = rowdata3value
                                            End If
                                            rowhtmlstring = rowhtmlstring & "<td align=left valign=top rowspan=" & showdataarr.Length & " >" & row3 & "&nbsp;</td>"
                                        Else
                                            rowhtmlstring = rowhtmlstring & ""
                                        End If
                                        rowhtmlstring1 = rowhtmlstring1 & "," & row3
                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Left(showdata, Len(showdata) - 1), "(", " of ") & "</td>"
                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Left(showdata, Len(showdata) - 1), "(", " of ")
                                        ' start code for column field form here 
                                        '****************************************
                                        '****************************************
                                        grtotal = 0
                                        subtot1 = 0
                                        If columnnamearr.Length = 1 Then
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                ReDim Preserve arrGrandRowTotal(3, columnarr.Length + 1)
                                                ReDim Preserve arrGrandRowCount(3, columnarr.Length + 1)
                                            Else
                                                ReDim Preserve arrGrandRowTotal(3, columnarr.Length)
                                                ReDim Preserve arrGrandRowCount(3, columnarr.Length)
                                            End If
                                            intGrandRowTotal = 0
                                            'Start - Code to define new row and fill data 
                                            Dim objRow As DataRow = Table.NewRow
                                            objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value & "-" & rowdata3value
                                            'End - Code to define new row and fill data
                                            Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                            If strRowData = "" Then
                                                strRowData = 0
                                            End If
                                            For cm = 0 To columnarr.Length - 1
                                                '****************************************
                                                If Trim(columnarr(cm)) <> "" Then
                                                    If Trim(Request("wheredata")) <> "" Then
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and " & rowdata1 & "='" & Trim(rowdata1value) & "' and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                    Else
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & rowdata & ",0)='" & strRowData & "' and " & rowdata1 & "='" & Trim(rowdata1value) & "' and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "'" & RemoveFunction(showdata)
                                                    End If
                                                Else
                                                    If Trim(Request("wheredata")) <> "" Then
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "' and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                    Else
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "' and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "'" & RemoveFunction(showdata)
                                                    End If
                                                End If
                                                Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                connection1.Open()
                                                sqlrsdatac = sqlcmdc.ExecuteReader
                                                While sqlrsdatac.Read
                                                    'Start - Code to fill data 
                                                    If columnarr(cm) <> "" Then
                                                        objRow(columnarr(cm)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                    End If
                                                    'End - Code to fill data 
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                    Else
                                                        grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                        subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                    End If
                                                    If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    For fm = 0 To formulaarr.Length - 1
                                                        If columnarr(cm) = formulaarr(fm) Then
                                                            formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                            formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                        End If
                                                    Next
                                                End While
                                                connection1.Close()
                                                '****************************************
                                            Next
                                            'Start - Code to add row in the table 
                                            Table.Rows.Add(objRow)
                                            'End - Code to add row in the table 
                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                If intSubColTotal > 0 Then
                                                    'Response.Write(subtot1 & "/" & intSubColTotal)
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                End If
                                            Else
                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                            End If
                                            If CDbl(subtot1) > 0.0 Then
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                End If
                                            End If
                                            intGrandRowTotal = intGrandRowTotal + 1
                                        ElseIf columnnamearr.Length = 2 Then
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 2))
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 2))
                                            Else
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) - 1)
                                            End If
                                            intGrandRowTotal = 0
                                            Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                            If strRowData = "" Then
                                                strRowData = 0
                                            End If
                                            For cm = 0 To columnarr.Length - 1
                                                subtot1 = 0
                                                'Start - Code to define new row and fill data 
                                                Dim objRow As DataRow = Table.NewRow
                                                objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value & "-" & rowdata3value & "-" & columnarr(cm)
                                                'End - Code to define new row and fill data
                                                For cm2 = 0 To columnarr2.Length - 1
                                                    '****************************************
                                                    If Trim(Request("wheredata")) <> "" Then
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                    Else
                                                        strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "'" & RemoveFunction(showdata)
                                                    End If
                                                    Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                    connection1.Open()
                                                    sqlrsdatac = sqlcmdc.ExecuteReader
                                                    While sqlrsdatac.Read
                                                        'Start - Code to fill data 
                                                        objRow(columnarr2(cm2)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        'End - Code to fill data 
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                        Else
                                                            grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                            subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                        End If
                                                        If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                            End If
                                                        End If
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        For fm = 0 To formulaarr.Length - 1
                                                            If columnarr2(cm2) = formulaarr(fm) Then
                                                                formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                            End If
                                                        Next
                                                    End While
                                                    connection1.Close()
                                                    '****************************************
                                                Next
                                                'Start - Code to add row in the table 
                                                Table.Rows.Add(objRow)
                                                'End - Code to add row in the table 
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If intSubColTotal > 0 Then
                                                        'Response.Write(subtot1 & "/" & intSubColTotal)
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                End If
                                                If CDbl(subtot1) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                ' Start Formula for column field form here 
                                                strfrumala = ""
                                                If Trim(Request("txtaFormula")) <> "" Then
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr2(fm)
                                                    Next
                                                    If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                    End If
                                                    strfrumala = ""
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr3(fm)
                                                    Next
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                    arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                End If
                                                ' end formula for column field form here 
                                            Next
                                        ElseIf columnnamearr.Length = 3 Then
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 2) - 1)
                                            Else
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length + 1) * CDbl(columnarr3.Length + 1) - 1)
                                            End If
                                            intGrandRowTotal = 0
                                            Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                            If strRowData = "" Then
                                                strRowData = 0
                                            End If
                                            For cm = 0 To columnarr.Length - 1
                                                For tcm3 = 0 To Tcolumnarr3.Length - 1
                                                    Tcolumnarr3(tcm3) = 0
                                                    Tcolumnarr3C(tcm3) = 0
                                                Next
                                                For cm2 = 0 To columnarr2.Length - 1
                                                    subtot1 = 0
                                                    'Start - Code to define new row and fill data 
                                                    Dim objRow As DataRow = Table.NewRow
                                                    objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & rowdata1value & "-" & rowdata3value & "-" & columnarr(cm) & "-" & columnarr2(cm2)
                                                    'End - Code to define new row and fill data
                                                    For cm3 = 0 To columnarr3.Length - 1
                                                        '****************************************
                                                        If Trim(Request("wheredata")) <> "" Then
                                                            strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                        Else
                                                            strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and isnull(" & rowdata1 & ",0)='" & Trim(rowdata1value) & "'and isnull(" & rowdata3 & ",0)='" & Trim(rowdata3value) & "'" & RemoveFunction(showdata)
                                                        End If
                                                        Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                        connection1.Open()
                                                        sqlrsdatac = sqlcmdc.ExecuteReader
                                                        While sqlrsdatac.Read
                                                            'Start - Code to fill data 
                                                            objRow(columnarr3(cm3)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                            'End - Code to fill data 
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                            Else
                                                                grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                                subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                            End If
                                                            If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                    arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                                End If
                                                            End If
                                                            intGrandRowTotal = intGrandRowTotal + 1
                                                            'Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata"))
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                Tcolumnarr3C(cm3) = Tcolumnarr3C(cm3) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                            Else
                                                                Tcolumnarr3(cm3) = Tcolumnarr3(cm3) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                            End If
                                                            For fm = 0 To formulaarr.Length - 1
                                                                If columnarr3(cm3) = formulaarr(fm) Then
                                                                    formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                    formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                                End If
                                                            Next
                                                        End While
                                                        connection1.Close()
                                                        '****************************************
                                                    Next
                                                    'Start - Code to add row in the table 
                                                    Table.Rows.Add(objRow)
                                                    'End - Code to add row in the table 
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        If intSubColTotal > 0 Then
                                                            'Response.Write(subtot1 & "/" & intSubColTotal)
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                        End If
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                    End If
                                                    If CDbl(subtot1) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    ' Start Formula for column field form here 
                                                    strfrumala = ""
                                                    If Trim(Request("txtaFormula")) <> "" Then
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr2(fm)
                                                        Next
                                                        If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                        End If
                                                        strfrumala = ""
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr3(fm)
                                                        Next
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                        'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                        arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                    End If
                                                    ' end formula for column field form here 
                                                Next
                                                '****************************************
                                                '****************************************
                                                ' for  total 
                                                Tcolstring = ""
                                                TColCountString = ""
                                                For cm3 = 0 To columnarr3.Length - 1
                                                    Tcolstring = Tcolstring & "+" & Tcolumnarr3(cm3)
                                                    TColCountString = TColCountString & "+" & Tcolumnarr3C(cm3)
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        If calvalue(Tcolumnarr3C(cm3)) > 0 Then
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)) / calvalue(Tcolumnarr3C(cm3)), 2), ",", ""), ".00", "")
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                        End If
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr3(cm3)), 2), ",", ""), ".00", "")
                                                    End If
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr3(cm3)))
                                                    If CDbl(calvalue(Tcolumnarr3(cm3))) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    For fm = 0 To formulaarr.Length - 1
                                                        If columnarr3(cm3) = formulaarr(fm) Then
                                                            formulaarr2(fm) = calvalue(Tcolumnarr3(cm3))
                                                            formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                        End If
                                                    Next
                                                Next
                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                    If calvalue(TColCountString) > 0 Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                    End If
                                                Else
                                                    rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                                    rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                                End If
                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                                If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                    End If
                                                End If
                                                intGrandRowTotal = intGrandRowTotal + 1
                                                strfrumala = ""
                                                If Trim(Request("txtaFormula")) <> "" Then
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr2(fm)
                                                    Next
                                                    If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                    End If
                                                    strfrumala = ""
                                                    For fm = 0 To formulaarr.Length - 1
                                                        strfrumala = strfrumala + formulaarr3(fm)
                                                    Next
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                    'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                    arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                End If
                                                '****************************************
                                                '****************************************
                                                ' for  total 
                                            Next
                                        ElseIf columnnamearr.Length = 4 Then
                                            If Trim(Request("txtaFormula")) <> "" Then
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 2) - 1)
                                            Else
                                                ReDim Preserve arrGrandRowTotal(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                                ReDim Preserve arrGrandRowCount(3, CDbl(columnarr.Length) * CDbl(columnarr2.Length) * CDbl(columnarr3.Length + 1) * CDbl(columnarr4.Length + 1) - 1)
                                            End If
                                            intGrandRowTotal = 0
                                            Dim strRowData = Trim(sqlrsdata("" & rowdata & "").ToString)
                                            If strRowData = "" Then
                                                strRowData = 0
                                            End If
                                            For cm = 0 To columnarr.Length - 1
                                                For cm2 = 0 To columnarr2.Length - 1
                                                    For tcm3 = 0 To Tcolumnarr4.Length - 1
                                                        Tcolumnarr4(tcm3) = 0
                                                        Tcolumnarr4C(tcm3) = 0
                                                    Next
                                                    For cm3 = 0 To columnarr3.Length - 1
                                                        subtot1 = 0
                                                        'Start - Code to define new row and fill data 
                                                        Dim objRow As DataRow = Table.NewRow
                                                        objRow("Fields") = Trim(sqlrsdata("" & rowdata & "").ToString) & "-" & columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3)
                                                        'End - Code to define new row and fill data
                                                        intSubColTotal = 0
                                                        For cm4 = 0 To columnarr4.Length - 1
                                                            '****************************************
                                                            If Trim(Request("wheredata")) <> "" Then
                                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & columnname4 & ",0)='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "' and " & Trim(Request("wheredata")) & RemoveFunction(showdata)
                                                            Else
                                                                strsql1c = "select cast(isnull(" & showdata & ", 0) as decimal(10,2)) as vdata, count(*) as cdata from " & Trim(Request("hidtablename")) & " where isnull(" & columnname & ",0)='" & columnarr(cm) & "' and isnull(" & columnname2 & ",0)='" & columnarr2(cm2) & "' and isnull(" & columnname3 & ",0)='" & columnarr3(cm3) & "' and isnull(" & columnname4 & ",0)='" & columnarr4(cm4) & "'  and isnull(" & rowdata & ",0)='" & strRowData & "'" & RemoveFunction(showdata)
                                                            End If
                                                            'Response.Write(columnarr(cm) & "-" & strsql1c & "<br>")
                                                            Dim sqlcmdc As New SqlCommand(strsql1c, connection1)
                                                            connection1.Open()
                                                            sqlrsdatac = sqlcmdc.ExecuteReader
                                                            While sqlrsdatac.Read
                                                                'Start - Code to fill data 
                                                                objRow(columnarr4(cm4)) = Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                                'End - Code to fill data 
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(sqlrsdatac("vdata").ToString, 2), ",", ""), ".00", "")
                                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                Else
                                                                    grtotal = CDbl(grtotal) + CDbl(sqlrsdatac("vdata").ToString)
                                                                    subtot1 = CDbl(subtot1) + CDbl(sqlrsdatac("vdata").ToString)
                                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(sqlrsdatac("vdata").ToString)
                                                                End If
                                                                If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                        arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(sqlrsdatac("cdata").ToString)
                                                                    End If
                                                                End If
                                                                intGrandRowTotal = intGrandRowTotal + 1
                                                                'chart value -start 
                                                                'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4), Replace(Replace(FormatNumber(sqlrsdatac("vdata"), 2), ",", ""), ".00", ""))
                                                                'chart value -end  
                                                                If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                    Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString) * CDbl(sqlrsdatac("cdata").ToString)
                                                                    Tcolumnarr4C(cm4) = Tcolumnarr4C(cm4) & "+" & CDbl(sqlrsdatac("cdata").ToString)
                                                                Else
                                                                    Tcolumnarr4(cm4) = Tcolumnarr4(cm4) & "+" & CDbl(sqlrsdatac("vdata").ToString)
                                                                End If
                                                                For fm = 0 To formulaarr.Length - 1
                                                                    If columnarr4(cm4) = formulaarr(fm) Then
                                                                        formulaarr2(fm) = FormatNumber(Trim(sqlrsdatac("vdata").ToString), 2)
                                                                        formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                                    End If
                                                                Next
                                                                If CDbl(sqlrsdatac("vdata").ToString) > 0.0 Then
                                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                        intSubColTotal = intSubColTotal + CDbl(sqlrsdatac("cdata").ToString)
                                                                    End If
                                                                End If
                                                            End While
                                                            connection1.Close()
                                                            '****************************************
                                                        Next
                                                        'Start - Code to add row in the table 
                                                        Table.Rows.Add(objRow)
                                                        'End - Code to add row in the table 
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            If intSubColTotal > 0 Then
                                                                'Response.Write(subtot1 & "/" & intSubColTotal)
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1 / intSubColTotal, 2), ",", ""), ".00", "")
                                                                arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                            Else
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                            End If
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(subtot1, 2), ",", ""), ".00", "")
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(subtot1)
                                                        End If
                                                        If CDbl(subtot1) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + intSubColTotal
                                                            End If
                                                        End If
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        ' Start Formula for column field form here 
                                                        strfrumala = ""
                                                        If Trim(Request("txtaFormula")) <> "" Then
                                                            For fm = 0 To formulaarr.Length - 1
                                                                strfrumala = strfrumala + formulaarr2(fm)
                                                            Next
                                                            If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                            Else
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                            End If
                                                            strfrumala = ""
                                                            For fm = 0 To formulaarr.Length - 1
                                                                strfrumala = strfrumala + formulaarr3(fm)
                                                            Next
                                                            arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                            'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                            arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                            intGrandRowTotal = intGrandRowTotal + 1
                                                            ''chart value -start 
                                                            'ChartTable(Trim(sqlrsdata("" & rowdata & "")), columnarr(cm) & "-" & columnarr2(cm2) & "-" & columnarr3(cm3) & "-" & columnarr4(cm4) & "-" & "Formula", Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""),".00",""))
                                                            ''chart value -end  
                                                        End If
                                                        ' end formula for column field form here 
                                                    Next
                                                    '****************************************
                                                    '****************************************
                                                    ' for  total 
                                                    Tcolstring = ""
                                                    TColCountString = ""
                                                    For cm4 = 0 To columnarr4.Length - 1
                                                        Tcolstring = Tcolstring & "+" & Tcolumnarr4(cm4)
                                                        TColCountString = TColCountString & "+" & Tcolumnarr4C(cm4)
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            If calvalue(Tcolumnarr4C(cm4)) > 0 Then
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)) / calvalue(Tcolumnarr4C(cm4)), 2), ",", ""), ".00", "")
                                                            Else
                                                                rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                                rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                            End If
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolumnarr4(cm4)), 2), ",", ""), ".00", "")
                                                        End If
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolumnarr4(cm4)))
                                                        If CDbl(calvalue(Tcolumnarr4(cm4))) > 0.0 Then
                                                            If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                                arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                            End If
                                                        End If
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                        For fm = 0 To formulaarr.Length - 1
                                                            If columnarr4(cm4) = formulaarr(fm) Then
                                                                formulaarr2(fm) = calvalue(Tcolumnarr4(cm4))
                                                                formulaarr3(fm) = FormatNumber(Trim(arrGrandRowTotal(smm, intGrandRowTotal - 1)), 2)
                                                            End If
                                                        Next
                                                    Next
                                                    If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                        If calvalue(TColCountString) > 0 Then
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring) / calvalue(TColCountString), 2), ",", ""), ".00", "")
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "")
                                                        End If
                                                    Else
                                                        rowhtmlstring = rowhtmlstring & "<td align=center>" & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "") & "</td>"
                                                        rowhtmlstring1 = rowhtmlstring1 & "," & Replace(Replace(FormatNumber(calvalue(Tcolstring), 2), ",", ""), ".00", "")
                                                    End If
                                                    arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(Tcolstring))
                                                    If CDbl(calvalue(Tcolstring)) > 0.0 Then
                                                        If InStr(1, UCase(Trim(showdata)), "AVG") > 0 Then
                                                            arrGrandRowCount(smm, intGrandRowTotal) = arrGrandRowCount(smm, intGrandRowTotal) + CDbl(calvalue(TColCountString))
                                                        End If
                                                    End If
                                                    intGrandRowTotal = intGrandRowTotal + 1
                                                    strfrumala = ""
                                                    If Trim(Request("txtaFormula")) <> "" Then
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr2(fm)
                                                        Next
                                                        If UCase(Trim(Request("chkPercentage"))) = "YES" Then
                                                            rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "%"
                                                        Else
                                                            rowhtmlstring = rowhtmlstring & "<td > " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "") & "</td>"
                                                            rowhtmlstring1 = rowhtmlstring1 & ", " & Replace(Replace(FormatNumber(calvalue(strfrumala), 2), ",", ""), ".00", "")
                                                        End If
                                                        strfrumala = ""
                                                        For fm = 0 To formulaarr.Length - 1
                                                            strfrumala = strfrumala + formulaarr3(fm)
                                                        Next
                                                        arrGrandRowTotal(smm, intGrandRowTotal) = calvalue(strfrumala)
                                                        'arrGrandRowTotal(smm, intGrandRowTotal) = CDbl(arrGrandRowTotal(smm, intGrandRowTotal)) + CDbl(calvalue(strfrumala))
                                                        arrGrandRowCount(smm, intGrandRowTotal) = 0
                                                        intGrandRowTotal = intGrandRowTotal + 1
                                                    End If
                                                    '****************************************
                                                    '****************************************
                                                    ' for  total 
                                                Next
                                            Next
                                        End If
                                        ' end code for column field form here 
                                        '****************************************
                                        'rowhtmlstring = rowhtmlstring & "<td align=center>" & grtotal & "</td>"
                                        '****************************************
                                    Next
                                    'end  second rwo data *****************
                                Next
                                'end  3 rwo data *****************
                                rowhtmlstring = rowhtmlstring & "</tr>"
                                'rowhtmlstring1 = rowhtmlstring1 & "$"
                            Next
                            ' end  code for data field form here 
                            '****************************************
                            '****************************************
                        End While
                    Else
                        Response.Write("No record found")
                        Exit Sub
                    End If
                    connection.Close()
                    ' end code for 3 level row data  form here 
                    '****************************************
                Else
                    Response.Write("Please reset and select only three field in row......!")
                End If
                '****************************************
                '****************************************
                '****************************************
                htmlhead = "<table border=1 cellspacing=0 cellspacing=0 class=grid>"
                htmlhead1 = ""
                Dim strFormulaName, strFormulaPlace As String
                Dim intFormulaPlace As Integer
                intFormulaPlace = 0
                If Trim(Request("hidFormulaName")) <> "" Then
                    strFormulaName = Trim(Request("hidFormulaName"))
                Else
                    strFormulaName = "Formula"
                End If

                If columnnamearr.Length = 1 Then
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        htmlhead = htmlhead & "<td ><b>" & rowdataarr(cm) & "<b></td>"
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                        intFormulaPlace = intFormulaPlace + 1
                    Next
                    htmlhead = htmlhead & "<td><b>Data</b></td>"
                    htmlhead1 = htmlhead1 & ",Data"
                    For cm = 0 To columnarr.Length - 1
                        Dim col1
                        If columnarr(cm) = "0" Then
                            col1 = " "
                        Else
                            col1 = columnarr(cm)
                        End If
                        htmlhead = htmlhead & "<td align=center><b>" & col1 & "<b></td>"
                        htmlhead1 = htmlhead1 & "," & columnarr(cm)
                        intFormulaPlace = intFormulaPlace + 1
                    Next

                    htmlhead = htmlhead & "<td align=center><b> " & Session("heading") & "</b></td>"
                    htmlhead1 = htmlhead1 & "," & Session("heading")
                    If Trim(Request("txtaFormula")) <> "" Then
                        If Trim(strFormulaPlace) <> "" Then
                            strFormulaPlace = strFormulaPlace & ", " & intFormulaPlace
                        Else
                            strFormulaPlace = intFormulaPlace
                        End If
                        htmlhead = htmlhead & "<td align=center><b>" & strFormulaName & "<b></td>"
                        htmlhead1 = htmlhead1 & "," & strFormulaName
                        intFormulaPlace = intFormulaPlace + 1
                    End If
                    'htmlhead = htmlhead & "<td align=center><b>Grand Total </b></td>"
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                ElseIf columnnamearr.Length = 2 Then
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        htmlhead = htmlhead & "<td valign=top rowspan=2 ><b>" & rowdataarr(cm) & "<b></td>"
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead = htmlhead & "<td valign=top  rowspan=2><b>Data</b></td>"
                    htmlhead1 = htmlhead1 & ",Data"
                    For cm = 0 To columnarr.Length - 1
                        Dim col1
                        If columnarr(cm) = "0" Then
                            col1 = " "
                        Else
                            col1 = columnarr(cm)
                        End If
                        If Trim(Request("txtaFormula")) <> "" Then
                            htmlhead = htmlhead & "<td align=center colspan=" & (columnarr2.Length + 2) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To columnarr2.Length + 2
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        Else
                            htmlhead = htmlhead & "<td align=center colspan=" & (columnarr2.Length + 1) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To columnarr2.Length + 1
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        End If
                    Next
                    'htmlhead = htmlhead & "<td valign=top align=center  rowspan=2><b>Grand Total </b></td>"
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1
                            Dim col2
                            If columnarr2(cm) = "0" Then
                                col2 = " "
                            Else
                                col2 = columnarr2(cm)
                            End If
                            htmlhead = htmlhead & "<td align=center><b>" & col2 & "<b></td>"
                            htmlhead1 = htmlhead1 & "," & columnarr2(cm)
                            intFormulaPlace = intFormulaPlace + 1
                        Next
                        htmlhead = htmlhead & "<td align=center><b>" & Session("heading") & "<b></td>"
                        htmlhead1 = htmlhead1 & "," & Session("heading")
                        intFormulaPlace = intFormulaPlace + 1
                        If Trim(Request("txtaFormula")) <> "" Then
                            If Trim(strFormulaPlace) <> "" Then
                                strFormulaPlace = strFormulaPlace & ", " & intFormulaPlace
                            Else
                                strFormulaPlace = intFormulaPlace
                            End If
                            htmlhead = htmlhead & "<td align=center><b>" & strFormulaName & "<b></td>"
                            htmlhead1 = htmlhead1 & "," & strFormulaName
                            intFormulaPlace = intFormulaPlace + 1
                        End If
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                ElseIf columnnamearr.Length = 3 Then
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        htmlhead = htmlhead & "<td valign=top rowspan=3 ><b>" & rowdataarr(cm) & "<b></td>"
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead = htmlhead & "<td valign=top  rowspan=3><b>Data</b></td>"
                    htmlhead1 = htmlhead1 & ",Data"
                    For cm = 0 To columnarr.Length - 1
                        Dim col1
                        If columnarr(cm) = "0" Then
                            col1 = " "
                        Else
                            col1 = columnarr(cm)
                        End If
                        If Trim(Request("txtaFormula")) <> "" Then
                            htmlhead = htmlhead & "<td align=center colspan=" & (columnarr2.Length * (columnarr3.Length + 2)) + (columnarr3.Length + 2) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To (columnarr2.Length * (columnarr3.Length + 2)) + (columnarr3.Length + 2)
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        Else
                            htmlhead = htmlhead & "<td align=center colspan=" & (columnarr2.Length * (columnarr3.Length + 1)) + (columnarr3.Length + 1) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To (columnarr2.Length * (columnarr3.Length + 1)) + (columnarr3.Length + 1)
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        End If
                    Next
                    'htmlhead = htmlhead & "<td valign=top align=center  rowspan=3><b>Grand Total </b></td>"
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1
                            Dim col2
                            If columnarr2(cm) = "0" Then
                                col2 = " "
                            Else
                                col2 = columnarr2(cm)
                            End If
                            If Trim(Request("txtaFormula")) <> "" Then
                                htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr3.Length) + 2) & "><b>" & col2 & "<b></td>"
                                Dim x As Integer
                                For x = 1 To ((columnarr3.Length) + 2)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm)
                                Next
                            Else
                                htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr3.Length) + 1) & "><b>" & col2 & "<b></td>"
                                Dim x As Integer
                                For x = 1 To ((columnarr3.Length) + 1)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm)
                                Next
                            End If
                        Next
                        If Trim(Request("txtaFormula")) <> "" Then
                            htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr3.Length) + 2) & "><b> " & columnarr(rmm) & " Total <b></td>"
                            Dim x As Integer
                            For x = 1 To ((columnarr3.Length) + 2)
                                htmlhead1 = htmlhead1 & "," & columnarr(rmm) & " Total"
                            Next
                        Else
                            htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr3.Length) + 1) & "><b> " & columnarr(rmm) & " Total <b></td>"
                            Dim x As Integer
                            For x = 1 To ((columnarr3.Length) + 1)
                                htmlhead1 = htmlhead1 & "," & columnarr(rmm) & " Total"
                            Next
                        End If
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1

                            For cm3 = 0 To columnarr3.Length - 1
                                Dim col3
                                If columnarr3(cm) = "0" Then
                                    col3 = " "
                                Else
                                    col3 = columnarr3(cm)
                                End If
                                htmlhead = htmlhead & "<td align=center><b>" & col3 & "<b></td>"
                                htmlhead1 = htmlhead1 & "," & columnarr3(cm3)
                                intFormulaPlace = intFormulaPlace + 1
                            Next
                            htmlhead = htmlhead & "<td align=center><b>" & Session("heading") & "<b></td>"
                            htmlhead1 = htmlhead1 & "," & Session("heading")
                            If Trim(Request("txtaFormula")) <> "" Then
                                If Trim(strFormulaPlace) <> "" Then
                                    strFormulaPlace = strFormulaPlace & ", " & intFormulaPlace
                                Else
                                    strFormulaPlace = intFormulaPlace
                                End If
                                htmlhead = htmlhead & "<td align=center><b>" & strFormulaName & "<b></td>"
                                htmlhead1 = htmlhead1 & "," & strFormulaName
                                intFormulaPlace = intFormulaPlace + 1
                            End If
                        Next
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                ElseIf columnnamearr.Length = 4 Then
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        htmlhead = htmlhead & "<td valign=top rowspan=4 ><b>" & rowdataarr(cm) & "<b></td>"
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead = htmlhead & "<td valign=top  rowspan=4><b>Data</b></td>"
                    htmlhead1 = htmlhead1 & ",Data"
                    For cm = 0 To columnarr.Length - 1
                        Dim col1
                        If columnarr(cm) = "0" Then
                            col1 = " "
                        Else
                            col1 = columnarr(cm)
                        End If
                        If Trim(Request("txtaFormula")) <> "" Then
                            htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr2.Length * (columnarr3.Length + 1)) * (columnarr4.Length + 2)) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To ((columnarr2.Length * (columnarr3.Length + 1)) * (columnarr4.Length + 2))
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        Else
                            htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr2.Length * (columnarr3.Length + 1)) * (columnarr4.Length + 1)) & "><b>" & col1 & "<b></td>"
                            Dim x As Integer
                            For x = 1 To ((columnarr2.Length * (columnarr3.Length + 1)) * (columnarr4.Length + 1))
                                htmlhead1 = htmlhead1 & "," & columnarr(cm)
                            Next
                        End If
                    Next
                    'htmlhead = htmlhead & "<td valign=top align=center  rowspan=4><b>Grand Total </b></td>"
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1
                            Dim col2
                            If columnarr2(cm) = "0" Then
                                col2 = " "
                            Else
                                col2 = columnarr2(cm)
                            End If
                            If Trim(Request("txtaFormula")) <> "" Then
                                htmlhead = htmlhead & "<td align=center colspan=" & (columnarr3.Length * ((columnarr4.Length) + 2)) + (columnarr4.Length + 2) & "><b>" & col2 & "<b></td>"
                                Dim x As Integer
                                For x = 1 To (columnarr3.Length * ((columnarr4.Length) + 2)) + (columnarr4.Length + 2)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm)
                                Next
                            Else
                                htmlhead = htmlhead & "<td align=center colspan=" & (columnarr3.Length * ((columnarr4.Length) + 1)) + (columnarr4.Length + 1) & "><b>" & col2 & "<b></td>"
                                Dim x As Integer
                                For x = 1 To (columnarr3.Length * ((columnarr4.Length) + 1)) + (columnarr4.Length + 1)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm)
                                Next
                            End If
                        Next
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1
                            For cm3 = 0 To columnarr3.Length - 1
                                Dim col3
                                If columnarr3(cm) = "0" Then
                                    col3 = " "
                                Else
                                    col3 = columnarr3(cm)
                                End If
                                If Trim(Request("txtaFormula")) <> "" Then
                                    htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr4.Length) + 2) & "><b>" & col3 & "<b></td>"
                                    Dim x As Integer
                                    For x = 1 To ((columnarr4.Length) + 2)
                                        htmlhead1 = htmlhead1 & "," & columnarr3(cm3)
                                    Next
                                Else
                                    htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr4.Length) + 1) & "><b>" & col3 & "<b></td>"
                                    Dim x As Integer
                                    For x = 1 To ((columnarr4.Length) + 1)
                                        htmlhead1 = htmlhead1 & "," & columnarr3(cm3)
                                    Next
                                End If
                            Next
                            If Trim(Request("txtaFormula")) <> "" Then
                                htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr4.Length) + 2) & "><b> " & columnarr2(cm) & " Total <b></td>"
                                Dim x As Integer
                                For x = 1 To ((columnarr4.Length) + 2)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm) & " Total"
                                Next
                            Else
                                htmlhead = htmlhead & "<td align=center colspan=" & ((columnarr4.Length) + 1) & "><b> " & columnarr2(cm) & " Total <b></td>"
                                Dim x As Integer
                                For x = 1 To ((columnarr4.Length) + 1)
                                    htmlhead1 = htmlhead1 & "," & columnarr2(cm) & " Total"
                                Next
                            End If
                        Next
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                    htmlhead = htmlhead & "<tr bgcolor=Gainsboro >"
                    htmlhead1 = htmlhead1 & "$"
                    For cm = 0 To rowdataarr.Length - 1
                        If cm = 0 Then
                            htmlhead1 = htmlhead1 & rowdataarr(cm)
                        Else
                            htmlhead1 = htmlhead1 & "," & rowdataarr(cm)
                        End If
                    Next
                    htmlhead1 = htmlhead1 & ",Data"
                    For rmm = 0 To columnarr.Length - 1
                        For cm = 0 To columnarr2.Length - 1
                            For cm3 = 0 To columnarr3.Length
                                For cm4 = 0 To columnarr4.Length - 1
                                    Dim col4
                                    If columnarr4(cm) = "0" Then
                                        col4 = " "
                                    Else
                                        col4 = columnarr4(cm)
                                    End If
                                    htmlhead = htmlhead & "<td align=center><b>" & col4 & "<b></td>"
                                    htmlhead1 = htmlhead1 & "," & columnarr4(cm4)
                                    intFormulaPlace = intFormulaPlace + 1
                                Next
                                htmlhead = htmlhead & "<td align=center><b>" & Session("heading") & "<b></td>"
                                htmlhead1 = htmlhead1 & "," & Session("heading")
                                If Trim(Request("txtaFormula")) <> "" Then
                                    If Trim(strFormulaPlace) <> "" Then
                                        strFormulaPlace = strFormulaPlace & ", " & intFormulaPlace
                                    Else
                                        strFormulaPlace = intFormulaPlace
                                    End If
                                    htmlhead = htmlhead & "<td align=center><b>" & strFormulaName & "<b></td>"
                                    htmlhead1 = htmlhead1 & "," & strFormulaName
                                    intFormulaPlace = intFormulaPlace + 1
                                End If
                            Next
                        Next
                    Next
                    htmlhead = htmlhead & "</tr>"
                    'htmlhead1 = htmlhead1 & "$"
                Else
                End If
                'htmlhead = htmlhead & rowhtmlstring ' puting data in table 
                htmlhead = fillData(Mid(htmlhead1 & rowhtmlstring1, 2), "$", ",") ' puting data in table 

                'Response.Write(arrGrandRowTotal.Length)
                'Response.End()
                Dim intRow As Integer
                For intRow = 0 To showdataarr.Length - 1
                    If UCase(Left(Trim(showdataarr(intRow)), 4)) = "AVG(" Then
                        boolAVG = True
                    Else
                        boolAVG = False
                    End If
                    'If showdataarr.Length = 2 Then
                    htmlhead = htmlhead & "<TR>"
                    If intRow = 0 Then
                        htmlhead = htmlhead & "<TD colspan=" & rowdataarr.Length + 1 & " rowspan=" & showdataarr.Length & " valign=top><B>Grand Total</B></TD>"
                    End If
                    Dim intTotal
                    strFormulaPlace = ", " & strFormulaPlace & ","
                    'For intTotal = 0 To arrGrandRowTotal.Length - 2
                    For intTotal = 0 To (arrGrandRowTotal.Length / 4) - 1 '17
                        Dim x, xsum As Integer
                        xsum = 0
                        For x = 0 To showdataarr.Length - 1
                            xsum = xsum + arrGrandRowTotal(x, intTotal)
                        Next
                        If xsum > 0 Then
                            'Response.Write(arrGrandRowTotal(intTotal) & "/" & arrGrandRowCount(intTotal) & " - ")
                            If Trim(arrGrandRowTotal(intRow, intTotal)) <> "" Then
                                If boolAVG = True Then
                                    If Trim(arrGrandRowCount(intRow, intTotal)) <> "" Then
                                        If CDbl(Trim(arrGrandRowCount(intRow, intTotal))) > 0.0 Then
                                            htmlhead = htmlhead & "<TD align=center>" & Replace(Replace(FormatNumber(arrGrandRowTotal(intRow, intTotal) / arrGrandRowCount(intRow, intTotal), 2), ",", ""), ".00", "") & "</TD>"
                                        Else
                                            htmlhead = htmlhead & "<TD align=center>" & Replace(Replace(FormatNumber(0, 2), ",", ""), ".00", "") & "</TD>"
                                        End If
                                    Else
                                        htmlhead = htmlhead & "<TD align=center>0</TD>"
                                    End If
                                Else
                                    htmlhead = htmlhead & "<TD align=center>" & Replace(Replace(FormatNumber(arrGrandRowTotal(intRow, intTotal), 2), ",", ""), ".00", "") & "</TD>"
                                End If
                            Else
                                htmlhead = htmlhead & "<TD align=center>0</TD>"
                            End If

                        End If
                    Next
                    htmlhead = htmlhead & "</TR>"
                    'End If
                Next
                htmlhead = htmlhead & "</table>"
                Response.Write(strheadof)
                Response.Write(htmlhead)
                '        bindtable()
                '<------------------------Creating A main Directory--------------------------------------->
                Dim i As Integer
                'Dim fp As StreamWriter
                'If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("UserId"))) Then
                '    '<----------------------Creating Directory for partcular user--------------------------------->
                '    Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("UserId")))
                '    '<----------------------End of Creating Directory for partcular user------------------------>
                'End If
                ''<------------------------End of Creating A main Directory--------------------------------------->
                'Path = "UsersSpace/" & Session("UserId") & "/" & Session.SessionID & ".xls"
                ''<--------------------Creating a new text file---------------------------------->
                'fp = File.CreateText(Server.MapPath(Path))
                'fp.WriteLine(htmlhead)
                'fp.Close()
                'Code written by pankaj
                htmlheadtxt.Text = htmlhead


                Session("strFinalData") = htmlhead
                Session("tab") = Table
                txtstrdiv.Value = htmlhead.ToString

            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                If UCase(Left(Trim(strmsg), 6)) = "COLUMN" Then
                    strmsg = "There is no data in selected row or column."
                End If
                ShowConfirm(strmsg)
            End Try
        End If
        '*************llllllll
    End Sub

#End Region

#Region "User Function"

    Function fillData(ByVal s As String, ByVal r As String, ByVal c As String) As String
        Dim intcolspan, introwspan As Integer
        Dim strColData, strRowData As String
        strColData = ""
        strRowData = ""
        Dim a, a1, t, u, y, x, x1, y1, n
        fillData = "<table border=1 cellspacing=0 cellspacing=0 class=grid>"
        '''''''''Start --- Code to make 2D array from Data Strind''''''''''''''''
        t = Split(s, r)
        If (UBound(t) > -1) Then
            u = Split(t(0), c)
            ReDim a(UBound(t), UBound(u))
            For x = 0 To UBound(t) ''rows
                u = Split(t(x), c)
                For y = 0 To UBound(u) ''cols
                    a(x, y) = u(y)
                    If a(x, y) = "" Then
                        a(x, y) = "&nbsp;"
                    End If
                Next
            Next
            '''''''''End --- Code to make 2D array from Data Strind''''''''''''''''
            '''''''''Start --- Code to filter Blank Row/Columns from 2D Array and Store values in new array''''''''''''''''
            ReDim a1(UBound(t), UBound(u))
            Dim indexX, indexY As Integer
            indexX = 0
            Dim intTot As Integer
            For x = 0 To UBound(t)  ''rows
                intTot = 0
                If x >= columnnamearr.Length Then
                    For y1 = rowdataarr.Length + 1 To UBound(u)
                        intTot = intTot + CType(Replace(a(x, y1), "%", ""), Integer)
                    Next
                End If
                If x < columnnamearr.Length Or intTot > 0 Then
                    strColData = ""
                    Dim intPosY As Integer = 0
                    indexY = 0
                    For y = 0 To UBound(u) ''cols
                        intTot = 0
                        If y >= rowdataarr.Length + 1 Then
                            For x1 = columnnamearr.Length To UBound(t)
                                intTot = intTot + CType(Replace(a(x1, y), "%", ""), Integer)
                            Next
                        End If
                        If y < rowdataarr.Length + 1 Or intTot > 0 Then
                            a1(indexX, indexY) = a(x, y)
                            indexY = indexY + 1
                        End If
                    Next
                    indexX = indexX + 1
                End If
            Next
            indexY = indexY - 1
            indexX = indexX - 1
            '''''''''End --- Code to filter Blank Row/Columns from 2D Array and Store values in new array''''''''''''''''
            '''''''''Start --- Code to format Rowdata in desired format''''''''''''''''
            For y = 0 To rowdataarr.Length  ''cols
                Dim intCount As Integer = 0
                Dim strTemp As String = ""
                For x = columnnamearr.Length To indexX  ''rows
                    For n = x To indexX  ''rows
                        If strTemp = "" Then
                            strTemp = a1(n, y)
                            intCount = 1
                        ElseIf strTemp = a1(n, y) Then
                            a1(n, y) = "--"
                            intCount = intCount + 1
                        Else
                            a1(x, y) = a1(x, y) & "," & intCount
                            strTemp = ""
                            x = n - 1
                            Exit For
                        End If
                        If n = indexX Then
                            a1(x, y) = a1(x, y) & "," & intCount
                            strTemp = ""
                        ElseIf y > 0 Then
                            If Left(a1(n + 1, y - 1), 2) <> "--" Then
                                a1(x, y) = a1(x, y) & "," & intCount
                                strTemp = ""
                                Exit For
                            End If
                        End If
                    Next
                Next
            Next
            '''''''''End --- Code to format Rowdata in desired format''''''''''''''''
            '''''''''Start --- Fill data in HTML Table''''''''''''''''
            For x = 0 To indexX  ''rows
                intTot = 0
                If x >= columnnamearr.Length Then
                    For y1 = rowdataarr.Length + 1 To indexY
                        intTot = intTot + CType(Replace(a1(x, y1), "%", ""), Integer)
                    Next
                End If
                If x < columnnamearr.Length Or intTot > 0 Then
                    fillData = fillData & "<tr>"
                    strColData = ""
                    Dim intPosY As Integer = 0
                    For y = 0 To indexY ''cols
                        intTot = 0
                        If y >= rowdataarr.Length + 1 Then
                            For x1 = columnnamearr.Length To indexX
                                intTot = intTot + CType(Replace(a1(x1, y), "%", ""), Integer)
                            Next
                        End If
                        If y < rowdataarr.Length + 1 Or intTot > 0 Then
                            If x = 0 And y < rowdataarr.Length + 1 Then
                                fillData = fillData & "<td rowspan=" & columnnamearr.Length & " bgcolor=gainsboro valign=top><b>" & a1(x, y) & "</b></td>"
                            ElseIf x < columnnamearr.Length And y < rowdataarr.Length + 1 Then
                                fillData = fillData & ""
                            ElseIf x < columnnamearr.Length Then
                                '''''''''Start --- Format Column Heading''''''''''''''''
                                If strColData = a1(x, y) Then
                                    a1(x, intPosY) = "--"
                                    intcolspan = intcolspan + 1
                                ElseIf strColData = "" Then
                                    strColData = a1(x, y)
                                    intcolspan = 1
                                Else
                                    fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
                                    strColData = a1(x, y)
                                    intcolspan = 1
                                End If
                                If y = indexY Then
                                    intcolspan = intcolspan + 1
                                    fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
                                    strColData = ""
                                    intcolspan = 0
                                ElseIf x > 0 Then
                                    If a1(x - 1, y) <> "--" Then
                                        fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
                                        strColData = ""
                                        intcolspan = 0
                                    End If
                                End If
                                intPosY = y
                                '''''''''End --- Format Column Heading''''''''''''''''
                            ElseIf y < rowdataarr.Length + 1 Then
                                'fillData = fillData & "<td bgcolor=gainsboro><b>" & a1(x, y) & "<b></td>"
                                Dim arr As Array
                                arr = Split(a1(x, y), ",")
                                If Left(arr(0), 2) <> "--" Then
                                    fillData = fillData & "<td bgcolor=gainsboro rowspan=" & arr(1) & " valign=top><b>" & arr(0) & "<b></td>"
                                End If
                            Else
                                fillData = fillData & "<td align=center>" & a1(x, y) & "</td>"
                            End If
                        End If
                    Next
                    fillData = fillData & "</tr>"
                End If
            Next
            '''''''''End --- Fill data in HTML Table''''''''''''''''
        End If


    End Function

    'Function fillData(ByVal s As String, ByVal r As String, ByVal c As String) As String
    '    Dim intcolspan, introwspan As Integer
    '    Dim strColData, strRowData As String
    '    strColData = ""
    '    strRowData = ""
    '    Dim a, a1, t, u, y, x, x1, y1, n
    '    fillData = "<table border=1 cellspacing=0 cellspacing=0 class=grid>"
    '    '''''''''Start --- Code to make 2D array from Data Strind''''''''''''''''
    '    t = Split(s, r)
    '    If (UBound(t) > -1) Then
    '        u = Split(t(0), c)
    '        ReDim a(UBound(t), UBound(u))
    '        For x = 0 To UBound(t) ''rows
    '            u = Split(t(x), c)
    '            For y = 0 To UBound(u) ''cols
    '                a(x, y) = u(y)
    '                If a(x, y) = "" Then
    '                    a(x, y) = "&nbsp;"
    '                End If
    '            Next
    '        Next
    '        '''''''''End --- Code to make 2D array from Data Strind''''''''''''''''
    '        '''''''''Start --- Code to filter Blank Row/Columns from 2D Array and Store values in new array''''''''''''''''
    '        ReDim a1(UBound(t), UBound(u))
    '        Dim indexX, indexY As Integer
    '        indexX = 0
    '        Dim intTot As Integer
    '        For x = 0 To UBound(t)  ''rows
    '            intTot = 0
    '            If x >= columnnamearr.Length Then
    '                For y1 = rowdataarr.Length To UBound(u)
    '                    intTot = intTot + CType(Replace(a(x, y1), "%", ""), Integer)
    '                Next
    '            End If
    '            If x < columnnamearr.Length Or intTot > 0 Then
    '                strColData = ""
    '                Dim intPosY As Integer = 0
    '                indexY = 0
    '                For y = 0 To UBound(u) ''cols
    '                    intTot = 0
    '                    If y >= rowdataarr.Length Then
    '                        For x1 = columnnamearr.Length To UBound(t)
    '                            intTot = intTot + CType(Replace(a(x1, y), "%", ""), Integer)
    '                        Next
    '                    End If
    '                    If y < rowdataarr.Length Or intTot > 0 Then
    '                        a1(indexX, indexY) = a(x, y)
    '                        indexY = indexY + 1
    '                    End If
    '                Next
    '                indexX = indexX + 1
    '            End If
    '        Next
    '        indexY = indexY - 1
    '        indexX = indexX - 1
    '        '''''''''End --- Code to filter Blank Row/Columns from 2D Array and Store values in new array''''''''''''''''
    '        '''''''''Start --- Code to format Rowdata in desired format''''''''''''''''
    '        For y = 0 To rowdataarr.Length - 1 ''cols
    '            Dim intCount As Integer = 0
    '            Dim strTemp As String = ""
    '            For x = columnnamearr.Length To indexX  ''rows
    '                For n = x To indexX  ''rows
    '                    If strTemp = "" Then
    '                        strTemp = a1(n, y)
    '                        intCount = 1
    '                    ElseIf strTemp = a1(n, y) Then
    '                        a1(n, y) = "--"
    '                        intCount = intCount + 1
    '                    Else
    '                        a1(x, y) = a1(x, y) & "," & intCount
    '                        strTemp = ""
    '                        x = n - 1
    '                        Exit For
    '                    End If
    '                    If n = indexX Then
    '                        a1(x, y) = a1(x, y) & "," & intCount
    '                        strTemp = ""
    '                    ElseIf y > 0 Then
    '                        If Left(a1(n + 1, y - 1), 2) <> "--" Then
    '                            a1(x, y) = a1(x, y) & "," & intCount
    '                            strTemp = ""
    '                            Exit For
    '                        End If
    '                    End If
    '                Next
    '            Next
    '        Next
    '        '''''''''End --- Code to format Rowdata in desired format''''''''''''''''
    '        '''''''''Start --- Fill data in HTML Table''''''''''''''''
    '        For x = 0 To indexX  ''rows
    '            intTot = 0
    '            If x >= columnnamearr.Length Then
    '                For y1 = rowdataarr.Length To indexY
    '                    intTot = intTot + CType(Replace(a1(x, y1), "%", ""), Integer)
    '                Next
    '            End If
    '            If x < columnnamearr.Length Or intTot > 0 Then
    '                fillData = fillData & "<tr>"
    '                strColData = ""
    '                Dim intPosY As Integer = 0
    '                For y = 0 To indexY ''cols
    '                    intTot = 0
    '                    If y >= rowdataarr.Length Then
    '                        For x1 = columnnamearr.Length To indexX
    '                            intTot = intTot + CType(Replace(a1(x1, y), "%", ""), Integer)
    '                        Next
    '                    End If
    '                    If y < rowdataarr.Length Or intTot > 0 Then
    '                        If x = 0 And y < rowdataarr.Length Then
    '                            fillData = fillData & "<td rowspan=" & columnnamearr.Length & " bgcolor=gainsboro valign=top><b>" & a1(x, y) & "</b></td>"
    '                        ElseIf x < columnnamearr.Length And y < rowdataarr.Length Then
    '                            fillData = fillData & ""
    '                        ElseIf x < columnnamearr.Length Then
    '                            '''''''''Start --- Format Column Heading''''''''''''''''
    '                            If strColData = a1(x, y) Then
    '                                a1(x, intPosY) = "--"
    '                                intcolspan = intcolspan + 1
    '                            ElseIf strColData = "" Then
    '                                strColData = a1(x, y)
    '                                intcolspan = 1
    '                            Else
    '                                fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
    '                                strColData = a1(x, y)
    '                                intcolspan = 1
    '                            End If
    '                            If y = indexY Then
    '                                intcolspan = intcolspan + 1
    '                                fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
    '                                strColData = ""
    '                                intcolspan = 0
    '                            ElseIf x > 0 Then
    '                                If a1(x - 1, y) <> "--" Then
    '                                    fillData = fillData & "<td colspan=" & intcolspan & " bgcolor=gainsboro align=center><b>" & strColData & "</b></td>"
    '                                    strColData = ""
    '                                    intcolspan = 0
    '                                End If
    '                            End If
    '                            intPosY = y
    '                            '''''''''End --- Format Column Heading''''''''''''''''
    '                        ElseIf y < rowdataarr.Length Then
    '                            'fillData = fillData & "<td bgcolor=gainsboro><b>" & a1(x, y) & "<b></td>"
    '                            Dim arr As Array
    '                            arr = Split(a1(x, y), ",")
    '                            If Left(arr(0), 2) <> "--" Then
    '                                fillData = fillData & "<td bgcolor=gainsboro rowspan=" & arr(1) & " valign=top><b>" & arr(0) & "<b></td>"
    '                            End If
    '                        Else
    '                            fillData = fillData & "<td align=center>" & a1(x, y) & "</td>"
    '                        End If
    '                    End If
    '                Next
    '                fillData = fillData & "</tr>"
    '            End If
    '        Next
    '        '''''''''End --- Fill data in HTML Table''''''''''''''''
    '    End If
    'End Function

    Public Sub ShowConfirm(ByVal strmsg As String)
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub

    Public Function calvalue(ByVal strvale)
        Dim calv As Double = 0.0
        Dim obj As New SqlCommand("Select (cast(" & Replace(strvale, ",", "") & " as decimal(10,2))) as Result", connection11)
        Dim sqlcmdc11 As SqlDataReader
        connection11.Open()
        sqlcmdc11 = obj.ExecuteReader
        If sqlcmdc11.Read Then
            calv = sqlcmdc11("Result")
        End If
        sqlcmdc11.Close()
        connection11.Close()
        obj.Dispose()
        calvalue = calv
    End Function

    Public Sub declarechartobj()
        Session.Remove("tab")
        col0 = New DataColumn("Row")
        col1 = New DataColumn("Column")
        col2 = New DataColumn("Value")
        dt.Columns.Add(col0)
        dt.Columns.Add(col1)
        dt.Columns.Add(col2)
    End Sub

    Public Sub ChartTable(ByVal chartrowdata, ByVal chartcoldata, ByVal chartvoldata)
        If IsNothing(Session("tab")) = False Then
            dt = Session("tab")
        End If
        row = dt.NewRow
        row(0) = chartrowdata & "-" & chartcoldata
        row(1) = chartcoldata
        row(2) = chartvoldata
        dt.Rows.Add(row)
        Session("tab") = dt
    End Sub

    Public Sub bindtable()
        'Dim tab As New DataTable
        'tab = Session("tab")
        'Dim view As DataView
        'view = dt.DefaultView
        'Dim prochart As New ColumnChart
        'prochart.DataSource = view
        'prochart.DataXValueField = "Row"
        'prochart.DataYValueField = "Value"
        'prochart.DataBind()
        'prochart.ShowLineMarkers = True
        'ChartControl2.Charts.Add(prochart)
        'ChartControl2.RedrawChart()
    End Sub

    Public Function RemoveFunction(ByVal WithFunction As String)
        If InStr(1, UCase(Trim(WithFunction)), "AVG") > 0 Then
            WithFunction = Replace(WithFunction, "Avg(", "")
            WithFunction = Replace(WithFunction, ")", "")
            RemoveFunction = " and not " & WithFunction & " is null"
        Else
            RemoveFunction = ""
        End If
    End Function


#End Region

#Region "Control Events"

    Private Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        'If queryname.Value = "" Then
        '    ShowConfirm("Please save this query first")
        '    Exit Sub
        'End If
        'If cbodept.SelectedIndex = 0 Then
        '    ShowConfirm("Please select department")
        '    Exit Sub
        'End If
        'If txtname.Text = "" Then
        '    ShowConfirm("Please enter report name")
        '    Exit Sub
        'End If
        'connection.Close()
        'Dim cmdchk As New SqlCommand("select * from IDMSSavedHTMLFile where SavedFilename='" & txtname.Text & "'", connection)
        'Dim drchk As SqlDataReader
        'Dim bool As Boolean
        'connection.Open()
        'drchk = cmdchk.ExecuteReader
        'If drchk.Read Then
        '    bool = True
        'End If
        'drchk.Close()
        'connection.Close()
        'cmdchk.Dispose()
        'If bool = True Then
        '    ShowConfirm("Report with this name already exists.\n Please save report by another name.")
        '    Exit Sub
        'End If
        Dim showrep
        showrep = Me.txtname.Text

        htmlheaddiv = "<table border=0 cellspacing=0 cellspacing=0 class=grid width=100%>"
        htmlheaddiv = htmlheaddiv & "<tr>"
        htmlheaddiv = htmlheaddiv & "<td align=center ><b>" & showrep & "<b></td>"
        htmlheaddiv = htmlheaddiv & "</tr>"
        htmlheaddiv = htmlheaddiv & "</table>"
        Me.txtdivshow.Value = htmlheaddiv.ToString
        If txtstrdiv.Value <> "" Then
            Dim str As String
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("userid"))) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("userid") & "/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "/IDMS/Menu/UsersSpace/" & Session("userid") & "/" & txtname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))

            fp.WriteLine(txtdivshow.Value)

            fp.WriteLine(txtstrdiv.Value)
            fp.Close()
            Response.Write(htmlheadtxt.Text)
            Dim client
            If Request("cboclient") = "" Or Request("cboclient") = "--Select--" Then
                client = 0
            Else
                client = Request("cboclient")
            End If
            Dim lob
            If Request("cbolob") = "" Or Request("cbolob") = "--Select--" Then
                lob = 0
            Else
                lob = Request("cbolob")
            End If
            '''SavedFilename()
            '''Path()
            '''DepartmentId()
            '''ClientID()
            '''LOBId()
            '''SavedBy()
            '''SavedOn()
            Dim cmdins As New SqlCommand("insert into IDMSSavedHTMLFile values('" & txtname.Text & "','" & Path & "','" & Request("cbodept") & "','" & client & "','" & lob & "','" & Session("userid") & "','" & FormatDateTime(Now, DateFormat.ShortDate) & "','Query','" & Me.queryname.Value & "')", connection)
            connection.Open()
            cmdins.ExecuteNonQuery()
            connection.Close()
            ShowConfirm("File has been created successfully.")
            cbodept.SelectedIndex = 0
            txtname.Text = ""
        Else
            ShowConfirm("File has not been created. Because it does not have any data.")
        End If

    End Sub

    Private Sub imgexl_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgexl.Click

        Dim fp As StreamWriter
        Dim i As Integer

        If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("UserId") & "/xls")) Then
            '<----------------------Creating Directory for partcular user--------------------------------->
            Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("UserId") & "/xls"))
            '<----------------------End of Creating Directory for partcular user------------------------>
        End If

        '<--------------------Deleting all the excel files---------------------------------->
        'Dim DelPath As String
        'DelPath = "UsersSpace/" & Session("UserId") & "/xls"
        'Dim path1 = Server.MapPath(DelPath)
        'Dim dir As New DirectoryInfo(path1)
        'Dim file1() As FileInfo
        'file1 = dir.GetFiles()
        'If file1.Length > 0 Then
        '    For i = 0 To file1.Length - 1
        '        file1(i).Delete()
        '    Next
        'End If
        '<-----------------------End Code-------------------------------------------------------->


        ''If htmlheadtxt.Text <> "" Then
        ''    Response.Write(htmlheadtxt.Text)
        ''End If



        ''If Not Directory.Exists(Server.MapPath("UsersSpace/" & Session("UserId"))) Then
        ''    '<----------------------Creating Directory for partcular user--------------------------------->
        ''    Directory.CreateDirectory(Server.MapPath("UsersSpace/" & Session("UserId")))
        ''    '<----------------------End of Creating Directory for partcular user------------------------>
        ''End If
        '''<------------------------End of Creating A main Directory--------------------------------------->

        If htmlqueryname.Text <> "" Then
            Dim strQyeryNAme As String
            strQyeryNAme = htmlqueryname.Text
            Path = "UsersSpace/" & Session("UserId") & "/" & "xls/" & strQyeryNAme & ".xls"
        Else
            Path = "UsersSpace/" & Session("UserId") & "/" & "xls/" & Session.SessionID & ".xls"
        End If

        '<--------------------Creating a new text file---------------------------------->
        fp = File.CreateText(Server.MapPath(Path))

        fp.WriteLine(htmlheadtxt.Text)
        fp.Close()

        Dim Script As New System.Text.StringBuilder
        With Script
            .Append("<Script language='javascript'>")
            .Append("xls()")
            .Append("</Script>")
        End With
        RegisterStartupScript("xls", Script.ToString())
    End Sub

#End Region

End Class

