Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_CreateCrossTab
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Ajax.Utility.RegisterTypeForAjax(GetType(DataTransfer))
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay1.Visible = True
                Me.spandisplay2.Visible = True
                Me.savespanmul.Visible = True
                Me.cmdcreatemul.Visible = True
                rdr.Close()
                Dim cmd As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    Dim val1 As String
                    Dim val2 As String
                    Dim val3 As String
                    val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                    lbl1.Text = val1
                    lbl2.Text = val2
                    lbl3.Text = val3
                    lbl4.Text = val1
                    lbl5.Text = val2
                    lbl6.Text = val3
                    lbl7.Text = val1
                    lbl8.Text = val2
                    lbl9.Text = val3
                End If
            Else
                Me.gettable.Visible = True
                Me.cmdcreate.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
        connection.Close()
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cbodept1.Attributes.Add("onchange", "javascript:getclient1();")
        Me.cbodept2.Attributes.Add("onchange", "javascript:getclient2();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        Me.cboclient1.Attributes.Add("onchange", "javascript:getlob1();")
        Me.cboclient2.Attributes.Add("onchange", "javascript:getlob2();")
        Me.cbolob.Attributes.Add("onchange", "javascript:gettab2();")
        Me.cbolob1.Attributes.Add("onchange", "javascript:gettab21();")
        Me.cbotab1.Attributes.Add("onchange", "javascript:gettab1cols();")
        Me.cbotab2.Attributes.Add("onchange", "javascript:gettab2cols();")
        Me.lsttab1cols.Attributes.Add("onchange", "getlsttab1colsValue();")
        Me.lsttab2cols.Attributes.Add("onchange", "gettab2colsValue();")
        'Gettable.Attributes.Add("onclick", "javascript:gettab();")
        'ClientScript.GetPostBackClientHyperlink(Button1, "")
        'hfUserType.Value = Session("typeofuser").ToString()
        hfUserId.Value = Session("userid").ToString()
        Dim typeofuser = Session("typeofuser")
        If (Me.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept1.DataSource = dsgetdept
                cbodept1.DataTextField = "DepartmentName"
                cbodept1.DataValueField = "autoid"
                cbodept1.DataBind()
                cbodept2.DataSource = dsgetdept
                cbodept2.DataTextField = "DepartmentName"
                cbodept2.DataValueField = "autoid"
                cbodept2.DataBind()
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
                cbodept2.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept1.DataSource = dsgetdept
                cbodept1.DataTextField = "DepartmentName"
                cbodept1.DataValueField = "autoid"
                cbodept1.DataBind()
                cbodept2.DataSource = dsgetdept
                cbodept2.DataTextField = "DepartmentName"
                cbodept2.DataValueField = "autoid"
                cbodept2.DataBind()
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
                cbodept2.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept1.DataSource = dsgetdept
                cbodept1.DataTextField = "DepartmentName"
                cbodept1.DataValueField = "autoid"
                cbodept1.DataBind()
                cbodept2.DataSource = dsgetdept
                cbodept2.DataTextField = "DepartmentName"
                cbodept2.DataValueField = "autoid"
                cbodept2.DataBind()
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
                cbodept2.Items.Insert(0, "--Select--")
            End If


            ' IsUpdate.Value = "False"

            '********For Track Management********************
            Dim cmdCal As New SqlCommand
            cmdCal.Connection = connection
            cmdCal.CommandText = "select Username from warsmemregistration where userid='" & Session("Userid") & "'"
            connection.Open()
            Dim UserName = cmdCal.ExecuteScalar
            connection.Close()
            'Dim cmdsaveTrack As New SqlCommand("Insert_IDMSTrackManagement", connection)
            'cmdsaveTrack.CommandType = CommandType.StoredProcedure
            'With cmdsaveTrack.Parameters
            '    .Add("@Userid", Trim(Session("userid")))
            '    .Add("@FormName", "createCrossTabl")
            '    .Add("@Comment", "For Create Cross Table")
            '    .Add("@Addedon", System.DateTime.Today)
            'End With
            'connection.Open()
            'cmdsaveTrack.ExecuteNonQuery()
            'connection.Close()
            'cmdsaveTrack.Dispose()
            '**************************************************

            'hfUserType.Value = Session("typeofuser").ToString()
            hfUserId.Value = Session("userid").ToString()
        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub

    Public Sub Clear()
        txtname.Text = ""
        cbotab1.SelectedValue = 0
        'cbotab1.SelectedIndex = 0
        cbotab2.SelectedValue = 0
        ' cbodept.SelectedValue = 0

        'cbodept1.SelectedValue = 0
        'cbodept2.SelectedValue = 0
        cbofunc11.SelectedValue = 0
        cbofunc12.SelectedValue = 0
        cbofunc13.SelectedValue = 0
        cbofunc14.SelectedValue = 0
        cbofunc15.SelectedValue = 0
        txtval11.Value = ""
        txtval12.Value = ""
        txtval13.Value = ""
        txtval14.Value = ""
        txtval15.Value = ""
        cbofunc21.SelectedValue = 0
        cbofunc22.SelectedValue = 0
        cbofunc23.SelectedValue = 0
        cbofunc24.SelectedValue = 0
        cbofunc25.SelectedValue = 0
        txtval21.Value = ""
        txtval22.Value = ""
        txtval22.Value = ""
        txtval24.Value = ""
        txtval25.Value = ""
    End Sub

    Protected Sub cmdcreatemul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcreatemul.Click
        Dim SpanA() As String
        Dim SpanB() As String
        Dim Span() As String
        Dim arrQueryString() As String
        Dim arrQueryString1() As String
        Dim arrQueryString2() As String
        Dim arrFirstTabColQueryString() As String
        Dim arrSecondTabColQueryString() As String
        Dim arrJoinTab1QueryString() As String
        Dim arrJoinTab2QueryString() As String
        Dim query As New System.Text.StringBuilder
        ' Dim query1 As New System.Text.StringBuilder
        SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
        SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrQueryString = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
        arrQueryString1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        arrQueryString2 = Split(hidQueryString2.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
        arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
        arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join

        '  Dim cbodept As String = SpanA(1)
        Dim cboclient As String = SpanA(1)
        Dim cbolob As String = SpanA(2)
        Dim cbotab1 As String = arrQueryString(3)

        ' Dim cbodept1 As String = SpanB(1)
        Dim cboclient1 As String = SpanB(1)
        Dim cbolob1 As String = SpanB(2)
        Dim cbotab2 As String = arrQueryString1(3)

        '  Dim cbodept2 As String = Span(1)
        Dim cboclient2 As String = Span(1)
        Dim cbolob2 As String = Span(2)


        Dim cbocol11 As String = arrJoinTab1QueryString(0)
        Dim cbocol12 As String = arrJoinTab1QueryString(1)
        Dim cbocol13 As String = arrJoinTab1QueryString(2)
        Dim cbocol14 As String = arrJoinTab1QueryString(3)
        Dim cbocol15 As String = arrJoinTab1QueryString(4)
        Dim cbocol16 As String = arrJoinTab1QueryString(5)
        Dim cbocol17 As String = arrJoinTab1QueryString(6)
        Dim cbocol18 As String = arrJoinTab1QueryString(7)
        Dim cbocolA1 As String = arrJoinTab1QueryString(8)
        Dim cbocolA2 As String = arrJoinTab1QueryString(9)
        Dim cbocolA3 As String = arrJoinTab1QueryString(10)
        Dim cbocolA4 As String = arrJoinTab1QueryString(11)
        Dim cbocolA5 As String = arrJoinTab1QueryString(12)

        Dim cbocol21 As String = arrJoinTab2QueryString(0)
        Dim cbocol22 As String = arrJoinTab2QueryString(1)
        Dim cbocol23 As String = arrJoinTab2QueryString(2)
        Dim cbocol24 As String = arrJoinTab2QueryString(3)
        Dim cbocol25 As String = arrJoinTab2QueryString(4)
        Dim cbocol26 As String = arrJoinTab2QueryString(5)
        Dim cbocol27 As String = arrJoinTab2QueryString(6)
        Dim cbocol28 As String = arrJoinTab2QueryString(7)
        Dim cbocolB1 As String = arrJoinTab2QueryString(8)
        Dim cbocolB2 As String = arrJoinTab2QueryString(9)
        Dim cbocolB3 As String = arrJoinTab2QueryString(10)
        Dim cbocolB4 As String = arrJoinTab2QueryString(11)
        Dim cbocolB5 As String = arrJoinTab2QueryString(12)




        Dim query1 As String = "Create table " & txtname.Text & "("
        Dim i As Integer
        Dim cols1 As String = ""

        'Dim cols1 As String = Request("lsttab1cols")
        'Dim cols1 As String = lsttab1cols.SelectedIndex.ToString
        For i = 0 To arrFirstTabColQueryString.Length - 2 'Function For Finding First Table Column
            If cols1 = "" Then
                cols1 = arrFirstTabColQueryString(i)
            Else
                cols1 = cols1 & "," & arrFirstTabColQueryString(i)

            End If

        Next

        Dim lsttab1cols As String = cols1
        Dim arr1() As String
        Dim arrtab1() As String

        'For i = 1 To arrQueryString.Length - 2
        '    If arrQueryString(i) <> "NULL" Then
        '        If i <= 1 Then
        '            Dim cbclient1 = cboclient1.SelectedValue

        '        End If
        '    End If
        'Next

        'arrtab1 = Split(Request("cbotab1"), "$")
        arrtab1 = Split(cbotab1, "$")
        If lsttab1cols <> "" Then


            'If Request("lsttab1cols") <> "" Then
            arr1 = Split(cols1, ",")
            For i = 0 To arr1.Length - 1
                If arr1(i) <> "" Then
                    If query.ToString = "" Then
                        query.Append(arr1(i) & " ")
                    Else
                        query.Append("," & arr1(i) & " ")
                    End If

                    ''''''retrieving datatype for a column'''''''''''''
                    Dim cmdgettype As New SqlCommand("select c.name,b.length from sysobjects a, syscolumns b, systypes c where a.id=b.id and b.xtype=c.xtype and a.name='" & arrtab1(1) & "' and b.name='" & arr1(i) & "'", connection)
                    Dim drgettype As SqlDataReader
                    connection.Open()
                    drgettype = cmdgettype.ExecuteReader
                    If drgettype.Read Then
                        query.Append(drgettype("name"))
                        If drgettype("name") <> "datetime" And drgettype("name") <> "numeric" And drgettype("name") <> "float" Then
                            query.Append("(" & drgettype("length") & ")")
                        End If
                    End If
                    drgettype.Close()
                    connection.Close()
                    cmdgettype.Dispose()
                End If
            Next
        End If

        'Dim cols2 As String = Request("lsttab2cols")
        Dim cols2 As String = ""

        For i = 0 To arrSecondTabColQueryString.Length - 2 ' Function For Finding Second Table Value
            If cols2 = "" Then
                cols2 = arrSecondTabColQueryString(i)
            Else
                cols2 = cols2 & "," & arrSecondTabColQueryString(i)
            End If

        Next

        Dim lsttab2cols As String = cols2
        Dim arr2() As String
        Dim arrtab2() As String
        'arrtab2 = Split(Request("cbotab2"), "$")
        arrtab2 = Split(cbotab2, "$")
        'If Request("lsttab2cols") <> "" Then
        If lsttab2cols <> "" Then

            arr2 = Split(cols2, ",")
            For i = 0 To arr2.Length - 1
                If arr2(i) <> "" Then
                    If query.ToString = "" Then
                        query.Append(arr2(i) & " ")
                    Else
                        query.Append("," & arr2(i) & " ")
                    End If

                    ''''''retrieving datatype for a column'''''''''''''
                    Dim cmdgettype As New SqlCommand("select c.name,b.length from sysobjects a, syscolumns b, systypes c where a.id=b.id and b.xtype=c.xtype and a.name='" & arrtab2(1) & "' and b.name='" & Trim(arr2(i)) & "'", connection)
                    Dim drgettype As SqlDataReader
                    connection.Open()
                    drgettype = cmdgettype.ExecuteReader
                    If drgettype.Read Then
                        query.Append(drgettype("name"))
                        If drgettype("name") <> "datetime" And drgettype("name") <> "numeric" And drgettype("name") <> "float" Then
                            query.Append("(" & drgettype("length") & ")")
                        End If
                    End If
                    drgettype.Close()
                    connection.Close()
                    cmdgettype.Dispose()
                End If
            Next
        End If
        query1 = query1 & query.ToString & ")"
        Try
            Dim cmdcreate As New SqlCommand(query1, connection)
            connection.Open()
            cmdcreate.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try


        ''''''''''''''''''insert data into the new table'''''''''''
        Dim qry As String
        Dim qry1 As String
        qry = "insert into " & txtname.Text & " select "
        For i = 0 To arr1.Length - 1
            If qry1 = "" Then
                qry1 = qry1 & "a." & arr1(i)
            Else
                qry1 = qry1 & ",a." & arr1(i)
            End If
        Next
        For i = 0 To arr2.Length - 1
            If qry1 = "" Then
                qry1 = qry1 & "b." & arr2(i)
            Else
                qry1 = qry1 & ",b." & arr2(i)
            End If
        Next
        qry = qry & qry1 & " from " & arrtab1(1) & " a, " & arrtab2(1) & " b"


        '''''''''''joins''''''''''''''' Chage By rohit For Finding selected Column Value
        'If Request("cbocol11") <> "--Select--" And cbojoin1.SelectedIndex <> 0 And Request("cbocol21") <> "--Select--" Then
        'qry = qry & " where a." & Request("cbocol11") & " " & cbojoin1.SelectedValue & " b." & Request("cbocol21")
        ' End If

        If cbocol11 <> "--Select--" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "--Select--" Then
            qry = qry & " where a." & cbocol11 & " " & cbojoin1.SelectedValue & " b." & cbocol21
        End If


        If cbocol12 <> "--Select--" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "--Select--" Then
            qry = qry & " and a." & cbocol12 & " " & cbojoin2.SelectedValue & " b." & cbocol22
        End If
        If cbocol13 <> "--Select--" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "--Select--" Then
            qry = qry & " and a." & cbocol13 & " " & cbojoin3.SelectedValue & " b." & cbocol23
        End If
        If cbocol14 <> "--Select--" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "--Select--" Then
            qry = qry & " and a." & cbocol14 & " " & cbojoin4.SelectedValue & " b." & cbocol24
        End If
        If cbocol15 <> "--Select--" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "--Select--" Then
            qry = qry & " and a." & cbocol15 & " " & cbojoin5.SelectedValue & " b." & cbocol25
        End If
        If cbocol16 <> "--Select--" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "--Select--" Then
            qry = qry & " and a." & cbocol16 & " " & cbojoin6.SelectedValue & " b." & cbocol26
        End If
        If cbocol17 <> "--Select--" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "--Select--" Then
            qry = qry & " and a." & cbocol17 & " " & cbojoin7.SelectedValue & " b." & cbocol27
        End If
        If cbocol18 <> "--Select--" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "--Select--" Then
            qry = qry & " and a." & cbocol18 & " " & cbojoin8.SelectedValue & " b." & cbocol28
        End If


        '''''''''''''''''conditions'''''''''''''''''
        If cbocolA1 <> "--Select--" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
            qry = qry & " and a." & cbocolA1 & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
        End If
        If cbocolA2 <> "--Select--" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
            qry = qry & " and a." & cbocolA2 & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & "'"
        End If
        If cbocolA3 <> "--Select--" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
            qry = qry & " and a." & cbocolA3 & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
        End If
        If cbocolA4 <> "--Select--" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
            qry = qry & " and a." & cbocolA4 & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
        End If
        If cbocolA5 <> "--Select--" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
            qry = qry & " and a." & cbocolA5 & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        If cbocolB1 <> "--Select--" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
            qry = qry & " and b." & cbocolB1 & " " & cbofunc21.SelectedValue & " '" & txtval21.Value & "'"
        End If
        If cbocolB2 <> "--Select--" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
            qry = qry & " and b." & cbocolB2 & " " & cbofunc22.SelectedValue & " '" & txtval22.Value & "'"
        End If
        If cbocolB3 <> "--Select--" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
            qry = qry & " and b." & cbocolB3 & " " & cbofunc23.SelectedValue & " '" & txtval23.Value & "'"
        End If
        If cbocolB4 <> "--Select--" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
            qry = qry & " and b." & cbocolB4 & " " & cbofunc24.SelectedValue & " '" & txtval24.Value & "'"
        End If
        If cbocolB5 <> "--Select--" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
            qry = qry & " and b." & cbocolB5 & " " & cbofunc25.SelectedValue & " '" & txtval25.Value & "'"
        End If



        ''''''''''''insert the entry of table created''''''
        Try
            Dim cmddata As New SqlCommand(qry, connection)
            connection.Open()
            cmddata.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try
        Dim vcols As String
        If lsttab1cols <> "" Then
            vcols = lsttab1cols
        End If
        If lsttab2cols <> "" Then
            If vcols = "" Then
                vcols = lsttab2cols
            Else
                vcols = vcols & "," & lsttab2cols
            End If
        End If
        Dim lob
        Dim client
        'If Request("cbolob2") = "" Or Request("cbolob2") = "--Select--" Then
        '    lob = 0
        'Else
        '    lob = Request("cbolob2")
        'End If
        'If Request("cboclient2") = "" Or Request("cboclient2") = "--Select--" Then
        '    client = 0
        'Else
        '    client = Request("cboclient2")
        'End If

        If cbolob2 = "" Or cbolob2 = "--Select--" Or cbolob2 = "NULL" Then
            lob = 0
        Else
            lob = cbolob2
        End If
        If cboclient2 = "" Or cboclient2 = "--Select--" Or cboclient2 = "NULL" Then
            client = 0
        Else
            client = cboclient2
        End If
        Try
            Dim cmdins As New SqlCommand("insert into warslobtablemaster (LOBId,TableName,CreatedOn,CreatedBy,LastModified,LastModifiedBy,currdate,Visiblecolumn,Editable,Importable,DepartmentId,ClientId)  values('" & lob & "','" & txtname.Text & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & vcols & "','Yes','Yes','" & cbodept2.SelectedValue & "','" & client & "')", connection)
            connection.Open()
            cmdins.ExecuteNonQuery()
            connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Merge'", connection)
            connection.Open()
            cmm.ExecuteNonQuery()
            connection.Close()

            '''''''''''''''Usertype check for track goes here:- By Suvidha

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try
        Clear()
        Showmsg("Table has been created successfully.")
        '************************************ Entry In TableRights
        If ConnectionState.Open Then
            connection.Close()
        End If
        Dim tabid1 As Integer
        Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txtname.Text & "'", connection)
        connection.Open()
        tabid1 = tabid.ExecuteScalar
        connection.Close()
        tabid.Dispose()

        Dim cmdsave As New SqlCommand("insert_tablerights", connection)
        cmdsave.CommandType = CommandType.StoredProcedure
        With cmdsave.Parameters
            .Add("@TableId", tabid1)
            .Add("@currdate", System.DateTime.Now.ToString("d"))
            .Add("@UserId", Session("Userid1"))
            .Add("@AssignedBy", Session("userid1"))
            'To be changed
        End With
        connection.Open()
        cmdsave.ExecuteNonQuery()
        connection.Close()
        cmdsave.Dispose()
        Clear()
        '************************************
    End Sub

    Protected Sub cmdcreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcreate.Click
        'Code for creating Table

        Dim SpanA() As String
        Dim SpanB() As String
        Dim Span() As String
        Dim arrQueryString() As String
        Dim arrQueryString1() As String
        Dim arrQueryString2() As String
        Dim arrFirstTabColQueryString() As String
        Dim arrSecondTabColQueryString() As String
        Dim arrJoinTab1QueryString() As String
        Dim arrJoinTab2QueryString() As String
        Dim query As New System.Text.StringBuilder
        ' Dim query1 As New System.Text.StringBuilder
        SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
        SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrQueryString = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
        arrQueryString1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        arrQueryString2 = Split(hidQueryString2.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
        arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
        arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join

        '  Dim cbodept As String = SpanA(1)
        Dim cboclient As String = SpanA(1)
        Dim cbolob As String = SpanA(2)
        Dim cbotab1 As String = arrQueryString(3)

        ' Dim cbodept1 As String = SpanB(1)
        Dim cboclient1 As String = SpanB(1)
        Dim cbolob1 As String = SpanB(2)
        Dim cbotab2 As String = arrQueryString1(3)

        '  Dim cbodept2 As String = Span(1)
        Dim cboclient2 As String = Span(1)
        Dim cbolob2 As String = Span(2)


        Dim cbocol11 As String = arrJoinTab1QueryString(0)
        Dim cbocol12 As String = arrJoinTab1QueryString(1)
        Dim cbocol13 As String = arrJoinTab1QueryString(2)
        Dim cbocol14 As String = arrJoinTab1QueryString(3)
        Dim cbocol15 As String = arrJoinTab1QueryString(4)
        Dim cbocol16 As String = arrJoinTab1QueryString(5)
        Dim cbocol17 As String = arrJoinTab1QueryString(6)
        Dim cbocol18 As String = arrJoinTab1QueryString(7)
        Dim cbocolA1 As String = arrJoinTab1QueryString(8)
        Dim cbocolA2 As String = arrJoinTab1QueryString(9)
        Dim cbocolA3 As String = arrJoinTab1QueryString(10)
        Dim cbocolA4 As String = arrJoinTab1QueryString(11)
        Dim cbocolA5 As String = arrJoinTab1QueryString(12)

        Dim cbocol21 As String = arrJoinTab2QueryString(0)
        Dim cbocol22 As String = arrJoinTab2QueryString(1)
        Dim cbocol23 As String = arrJoinTab2QueryString(2)
        Dim cbocol24 As String = arrJoinTab2QueryString(3)
        Dim cbocol25 As String = arrJoinTab2QueryString(4)
        Dim cbocol26 As String = arrJoinTab2QueryString(5)
        Dim cbocol27 As String = arrJoinTab2QueryString(6)
        Dim cbocol28 As String = arrJoinTab2QueryString(7)
        Dim cbocolB1 As String = arrJoinTab2QueryString(8)
        Dim cbocolB2 As String = arrJoinTab2QueryString(9)
        Dim cbocolB3 As String = arrJoinTab2QueryString(10)
        Dim cbocolB4 As String = arrJoinTab2QueryString(11)
        Dim cbocolB5 As String = arrJoinTab2QueryString(12)




        Dim query1 As String = "Create table " & txtname.Text & "("
        Dim i As Integer
        Dim cols1 As String = ""

        'Dim cols1 As String = Request("lsttab1cols")
        'Dim cols1 As String = lsttab1cols.SelectedIndex.ToString
        For i = 0 To arrFirstTabColQueryString.Length - 2 'Function For Finding First Table Column
            If cols1 = "" Then
                cols1 = arrFirstTabColQueryString(i)
            Else
                cols1 = cols1 & "," & arrFirstTabColQueryString(i)

            End If

        Next

        Dim lsttab1cols As String = cols1
        Dim arr1() As String
        Dim arrtab1() As String

        'For i = 1 To arrQueryString.Length - 2
        '    If arrQueryString(i) <> "NULL" Then
        '        If i <= 1 Then
        '            Dim cbclient1 = cboclient1.SelectedValue

        '        End If
        '    End If
        'Next

        'arrtab1 = Split(Request("cbotab1"), "$")
        arrtab1 = Split(cbotab1, "$")
        If lsttab1cols <> "" Then


            'If Request("lsttab1cols") <> "" Then
            arr1 = Split(cols1, ",")
            For i = 0 To arr1.Length - 1
                If arr1(i) <> "" Then
                    If query.ToString = "" Then
                        query.Append(arr1(i) & " ")
                    Else
                        query.Append("," & arr1(i) & " ")
                    End If

                    ''''''retrieving datatype for a column'''''''''''''
                    Dim cmdgettype As New SqlCommand("select c.name,b.length from sysobjects a, syscolumns b, systypes c where a.id=b.id and b.xtype=c.xtype and a.name='" & arrtab1(1) & "' and b.name='" & arr1(i) & "'", connection)
                    Dim drgettype As SqlDataReader
                    connection.Open()
                    drgettype = cmdgettype.ExecuteReader
                    If drgettype.Read Then
                        query.Append(drgettype("name"))
                        If drgettype("name") <> "datetime" And drgettype("name") <> "numeric" And drgettype("name") <> "float" Then
                            query.Append("(" & drgettype("length") & ")")
                        End If
                    End If
                    drgettype.Close()
                    connection.Close()
                    cmdgettype.Dispose()
                End If
            Next
        End If

        'Dim cols2 As String = Request("lsttab2cols")
        Dim cols2 As String = ""

        For i = 0 To arrSecondTabColQueryString.Length - 2 ' Function For Finding Second Table Value
            If cols2 = "" Then
                cols2 = arrSecondTabColQueryString(i)
            Else
                cols2 = cols2 & "," & arrSecondTabColQueryString(i)
            End If

        Next

        Dim lsttab2cols As String = cols2
        Dim arr2() As String
        Dim arrtab2() As String
        'arrtab2 = Split(Request("cbotab2"), "$")
        arrtab2 = Split(cbotab2, "$")
        'If Request("lsttab2cols") <> "" Then
        If lsttab2cols <> "" Then

            arr2 = Split(cols2, ",")
            For i = 0 To arr2.Length - 1
                If arr2(i) <> "" Then
                    If query.ToString = "" Then
                        query.Append(arr2(i) & " ")
                    Else
                        query.Append("," & arr2(i) & " ")
                    End If

                    ''''''retrieving datatype for a column'''''''''''''
                    Dim cmdgettype As New SqlCommand("select c.name,b.length from sysobjects a, syscolumns b, systypes c where a.id=b.id and b.xtype=c.xtype and a.name='" & arrtab2(1) & "' and b.name='" & Trim(arr2(i)) & "'", connection)
                    Dim drgettype As SqlDataReader
                    connection.Open()
                    drgettype = cmdgettype.ExecuteReader
                    If drgettype.Read Then
                        query.Append(drgettype("name"))
                        If drgettype("name") <> "datetime" And drgettype("name") <> "numeric" And drgettype("name") <> "float" Then
                            query.Append("(" & drgettype("length") & ")")
                        End If
                    End If
                    drgettype.Close()
                    connection.Close()
                    cmdgettype.Dispose()
                End If
            Next
        End If
        query1 = query1 & query.ToString & ")"
        Try
            Dim cmdcreate As New SqlCommand(query1, connection)
            connection.Open()
            cmdcreate.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try


        ''''''''''''''''''insert data into the new table'''''''''''
        Dim qry As String
        Dim qry1 As String
        qry = "insert into " & txtname.Text & " select "
        For i = 0 To arr1.Length - 1
            If qry1 = "" Then
                qry1 = qry1 & "a." & arr1(i)
            Else
                qry1 = qry1 & ",a." & arr1(i)
            End If
        Next
        For i = 0 To arr2.Length - 1
            If qry1 = "" Then
                qry1 = qry1 & "b." & arr2(i)
            Else
                qry1 = qry1 & ",b." & arr2(i)
            End If
        Next
        qry = qry & qry1 & " from " & arrtab1(1) & " a, " & arrtab2(1) & " b"


        '''''''''''joins''''''''''''''' Chage By rohit For Finding selected Column Value
        'If Request("cbocol11") <> "--Select--" And cbojoin1.SelectedIndex <> 0 And Request("cbocol21") <> "--Select--" Then
        'qry = qry & " where a." & Request("cbocol11") & " " & cbojoin1.SelectedValue & " b." & Request("cbocol21")
        ' End If

        If cbocol11 <> "--Select--" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "--Select--" Then
            qry = qry & " where a." & cbocol11 & " " & cbojoin1.SelectedValue & " b." & cbocol21
        End If


        If cbocol12 <> "--Select--" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "--Select--" Then
            qry = qry & " and a." & cbocol12 & " " & cbojoin2.SelectedValue & " b." & cbocol22
        End If
        If cbocol13 <> "--Select--" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "--Select--" Then
            qry = qry & " and a." & cbocol13 & " " & cbojoin3.SelectedValue & " b." & cbocol23
        End If
        If cbocol14 <> "--Select--" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "--Select--" Then
            qry = qry & " and a." & cbocol14 & " " & cbojoin4.SelectedValue & " b." & cbocol24
        End If
        If cbocol15 <> "--Select--" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "--Select--" Then
            qry = qry & " and a." & cbocol15 & " " & cbojoin5.SelectedValue & " b." & cbocol25
        End If
        If cbocol16 <> "--Select--" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "--Select--" Then
            qry = qry & " and a." & cbocol16 & " " & cbojoin6.SelectedValue & " b." & cbocol26
        End If
        If cbocol17 <> "--Select--" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "--Select--" Then
            qry = qry & " and a." & cbocol17 & " " & cbojoin7.SelectedValue & " b." & cbocol27
        End If
        If cbocol18 <> "--Select--" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "--Select--" Then
            qry = qry & " and a." & cbocol18 & " " & cbojoin8.SelectedValue & " b." & cbocol28
        End If


        '''''''''''''''''conditions'''''''''''''''''
        If cbocolA1 <> "--Select--" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
            qry = qry & " and a." & cbocolA1 & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
        End If
        If cbocolA2 <> "--Select--" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
            qry = qry & " and a." & cbocolA2 & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & "'"
        End If
        If cbocolA3 <> "--Select--" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
            qry = qry & " and a." & cbocolA3 & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
        End If
        If cbocolA4 <> "--Select--" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
            qry = qry & " and a." & cbocolA4 & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
        End If
        If cbocolA5 <> "--Select--" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
            qry = qry & " and a." & cbocolA5 & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        If cbocolB1 <> "--Select--" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
            qry = qry & " and b." & cbocolB1 & " " & cbofunc21.SelectedValue & " '" & txtval21.Value & "'"
        End If
        If cbocolB2 <> "--Select--" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
            qry = qry & " and b." & cbocolB2 & " " & cbofunc22.SelectedValue & " '" & txtval22.Value & "'"
        End If
        If cbocolB3 <> "--Select--" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
            qry = qry & " and b." & cbocolB3 & " " & cbofunc23.SelectedValue & " '" & txtval23.Value & "'"
        End If
        If cbocolB4 <> "--Select--" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
            qry = qry & " and b." & cbocolB4 & " " & cbofunc24.SelectedValue & " '" & txtval24.Value & "'"
        End If
        If cbocolB5 <> "--Select--" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
            qry = qry & " and b." & cbocolB5 & " " & cbofunc25.SelectedValue & " '" & txtval25.Value & "'"
        End If



        ''''''''''''insert the entry of table created''''''
        Try
            Dim cmddata As New SqlCommand(qry, connection)
            connection.Open()
            cmddata.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try
        Dim vcols As String
        If lsttab1cols <> "" Then
            vcols = lsttab1cols
        End If
        If lsttab2cols <> "" Then
            If vcols = "" Then
                vcols = lsttab2cols
            Else
                vcols = vcols & "," & lsttab2cols
            End If
        End If
        Dim lob
        Dim client
        'If Request("cbolob2") = "" Or Request("cbolob2") = "--Select--" Then
        '    lob = 0
        'Else
        '    lob = Request("cbolob2")
        'End If
        'If Request("cboclient2") = "" Or Request("cboclient2") = "--Select--" Then
        '    client = 0
        'Else
        '    client = Request("cboclient2")
        'End If

        If cbolob2 = "" Or cbolob2 = "--Select--" Or cbolob2 = "NULL" Then
            lob = 0
        Else
            lob = cbolob2
        End If
        If cboclient2 = "" Or cboclient2 = "--Select--" Or cboclient2 = "NULL" Then
            client = 0
        Else
            client = cboclient2
        End If
        Try
            Dim cmdins As New SqlCommand("insert into warslobtablemaster (LOBId,TableName,CreatedOn,CreatedBy,LastModified,LastModifiedBy,currdate,Visiblecolumn,Editable,Importable,DepartmentId,ClientId)  values('" & lob & "','" & txtname.Text & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & vcols & "','Yes','Yes','60','0')", connection)
            connection.Open()
            cmdins.ExecuteNonQuery()
            connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            '    Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Merge'", connection)
            '    connection.Open()
            '    cmm.ExecuteNonQuery()
            '    connection.Close()

            '    '''''''''''''''Usertype check for track goes here:- By Suvidha

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try
        Clear()
        Showmsg("Table has been created successfully.")
        '************************************ Entry In TableRights
        'If ConnectionState.Open Then
        '    connection.Close()
        'End If
        'Dim tabid1 As Integer
        'Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txtname.Text & "'", connection)
        'connection.Open()
        'tabid1 = tabid.ExecuteScalar
        'connection.Close()
        'tabid.Dispose()

        'Dim cmdsave As New SqlCommand("insert_tablerights", connection)
        'cmdsave.CommandType = CommandType.StoredProcedure
        'With cmdsave.Parameters
        '    .Add("@TableId", tabid1)
        '    .Add("@currdate", System.DateTime.Now.ToString("d"))
        '    .Add("@UserId", Session("Userid1"))
        '    .Add("@AssignedBy", Session("userid1"))
        '    'To be changed
        'End With
        'connection.Open()
        'cmdsave.ExecuteNonQuery()
        'connection.Close()
        'cmdsave.Dispose()
        'Clear()
        '************************************
    End Sub
End Class
