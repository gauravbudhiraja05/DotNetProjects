Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_EditUpdStruct
    Inherits System.Web.UI.Page
    Dim usp As New UserSpan
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Dim selectRep As New ReportDesigner
    Dim dept = "0"
    Dim client = "0"
    Dim lob = "0"
    Dim i = 0
    Public Function greater()
        Return ">="
    End Function
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        i = i + 1
        'Me.lblmsg.Text = Session("nmsg")
        If i <> 0 And Session("nmsg") <> "" Then
            ShowConfirm(Session("nmsg"))
        End If
        If i = 1 Then
            Session("nmsg") = ""
            i = 0
        End If
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay2.Visible = True
                Me.spandisplay3.Visible = True
                Me.dcl.Visible = True
                Me.CmdSave.Visible = True
                Me.Cmdup.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
        connection.Close()
        Ajax.Utility.RegisterTypeForAjax(GetType(DataTransfer))
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cbodept1.Attributes.Add("onchange", "javascript:getclient1();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        Me.cboclient1.Attributes.Add("onchange", "javascript:getlob1();")
        Me.cboclient2.Attributes.Add("onchange", "javascript:getlob2();")
        Me.cbodept2.Attributes.Add("onchange", "javascript:getclient2();")
        Me.cbolob.Attributes.Add("onchange", "javascript:gettab();")
        Me.cbolob1.Attributes.Add("onchange", "javascript:gettab1();")
        Me.cbotab1.Attributes.Add("onchange", "javascript:gettab1cols();")
        Me.cbotab2.Attributes.Add("onchange", "javascript:gettab2cols();")
        'Me.lsttab1cols.Attributes.Add("onchange", "getlsttab1colsValue();")
        'Me.lsttab2cols.Attributes.Add("onchange", "gettab2colsValue();")
        hfUserType.Value = Session("typeofuser")
        hfUserId.Value = Session("userid")
        'Dim str As String = ">="
        If Me.IsPostBack = False Then



            Session("nmsg") = ""
            'hfFirstTabQueryString.Value = "NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL"
            'hfSecondTabQueryString.Value = "NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL"
            'hfJoinTab1QueryString.Value = "NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL"
            'hfJoinTab2QueryString.Value = "NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL#NULL"
            Dim typeofuser = Session("typeofuser")
            If (typeofuser.Equals("Super Admin")) Then
                connection.Open()
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

            Dim cmd As SqlCommand
            If (Session("typeofuser") = "Super Admin") Then
                cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
            Else
                cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
            End If
            Dim dsar As DataSet = New DataSet()
            Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
            daar.Fill(dsar)
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

            connection.Close()
            Dim dept1
            Dim dept2
            Dim client1
            Dim client2
            Dim lob1
            Dim lob2
            Dim tab1
            Dim tab2
            Dim col1 As String
            Dim col2 As String



            Dim wherejoin As String
            Dim wherecon As String
            Dim underdept
            Dim underclient
            Dim underlob
            Dim chkscope As String

            Try

                Dim cmdget As New SqlCommand("select * from IdmsUpdateTabStruct where CmdID='" & Request("recid") & "'", connection)
                'Dim cmdget As New SqlCommand("select * from IdmsUpdateTabStruct where CmdID =5", connection)
                Dim drget As SqlDataReader
                connection.Open()
                drget = cmdget.ExecuteReader
                If drget.Read Then
                    dept1 = drget("DeptIdTo").ToString
                    dept2 = drget("DeptIdFrom").ToString
                    client1 = drget("ClientIdTo").ToString
                    client2 = drget("ClientIdFrom").ToString
                    lob1 = drget("LOBIdTo").ToString
                    lob2 = drget("LOBIdFrom").ToString
                    tab1 = drget("TableTo").ToString
                    tab2 = drget("Tablefrom").ToString
                    col1 = drget("ColumnsTo").ToString
                    col2 = drget("Columnsfrom").ToString
                    wherejoin = drget("wheredatajoin").ToString
                    wherecon = drget("wheredatacon").ToString
                    lblname.Text = drget("CmdName").ToString
                    underdept = drget("DeptId").ToString
                    underclient = drget("ClientId").ToString
                    underlob = drget("LOBId").ToString
                    'For checking the scope of command
                    chkscope = drget("LocalCmd").ToString
                    If chkscope = "Local" Then
                        chklocal.Checked = True
                    Else
                        chklocal.Checked = False
                    End If
                    'Dim arrtab1(1) As String
                    'arrtab1(1) = tab1

                    hidQueryString.Value = dept1 + "#" + client1 + "#" + lob1 + "#" + (col1 + "$" + tab1)
                    hidQueryString1.Value = dept2 + "#" + client2 + "#" + lob2 + "#" + (col2 + "$" + tab2)
                    hidQueryString2.Value = underdept + "#" + underclient + "#" + underlob
                End If
                drget.Close()
                connection.Close()
                cmdget.Dispose()
                cbodept.SelectedValue = dept1
                cbodept1.SelectedValue = dept2
                cbodept2.SelectedValue = underdept



                bindclient()
                If client1 = 0 Then
                    cboclient.SelectedIndex = 0
                Else
                    cboclient.SelectedValue = client1
                End If
                bindclient1()
                If client2 = 0 Then
                    cboclient1.SelectedIndex = 0
                Else
                    cboclient1.SelectedValue = client2
                End If
                bindclient2()
                If underclient = 0 Then
                    cboclient2.SelectedIndex = 0
                Else
                    cboclient2.SelectedValue = underclient
                End If
                bindlob()
                If lob1 = 0 Then
                    cbolob.SelectedIndex = 0
                Else
                    cbolob.SelectedValue = lob1
                End If
                bindlob1()
                If lob2 = 0 Then
                    cbolob1.SelectedIndex = 0
                Else
                    cbolob1.SelectedValue = lob2
                End If
                bindlob2()
                If underlob = 0 Then
                    cbolob2.SelectedIndex = 0
                Else
                    cbolob2.SelectedValue = underlob
                End If
                Dim i As Integer
                bindtab()
                For i = 0 To cbotab1.Items.Count - 1
                    If cbotab1.Items(i).Text = tab1 Then
                        cbotab1.Items(i).Selected = True
                    End If
                Next
                bindtab1()
                For i = 0 To cbotab2.Items.Count - 1
                    If cbotab2.Items(i).Text = tab2 Then
                        cbotab2.Items(i).Selected = True
                    End If
                Next
                bindcol()
                bindcol1()

                If col1 <> "" Then
                    Dim arrcol = Split(col1, ",")
                    For i = 0 To arrcol.Length - 1
                        Select Case i
                            Case 0
                                cbocolA1.SelectedValue = arrcol(i)
                                Exit Select
                            Case 1
                                cbocolA2.SelectedValue = arrcol(i)
                                Exit Select
                            Case 2
                                cbocolA3.SelectedValue = arrcol(i)
                                Exit Select
                            Case 3
                                cbocolA4.SelectedValue = arrcol(i)
                                Exit Select
                            Case 4
                                cbocolA5.SelectedValue = arrcol(i)
                                Exit Select
                        End Select
                    Next
                End If
                If col2 <> "" Then
                    Dim arrcol1 = Split(col2, ",")
                    For i = 0 To arrcol1.Length - 1
                        Select Case i
                            Case 0
                                cbocolB1.SelectedValue = arrcol1(i)
                                Exit Select
                            Case 1
                                cbocolB2.SelectedValue = arrcol1(i)
                                Exit Select
                            Case 2
                                cbocolB3.SelectedValue = arrcol1(i)
                                Exit Select
                            Case 3
                                cbocolB4.SelectedValue = arrcol1(i)
                                Exit Select
                            Case 4
                                cbocolB5.SelectedValue = arrcol1(i)
                                Exit Select
                        End Select
                    Next
                End If
                If Trim(wherejoin) <> "" Then
                    Dim arrjoin = Split(wherejoin, ",")
                    For i = 0 To arrjoin.Length - 1
                        Dim arrjoin1 = Split(arrjoin(i), "$##$")
                        Dim arrjoin2 = Split(arrjoin1(0), ".")
                        Dim arrjoin3 = Split(arrjoin1(2), ".")
                        Select Case i
                            Case 0
                                cbocol11.SelectedValue = Trim(arrjoin2(1))
                                cbojoin1.SelectedValue = Trim(arrjoin1(1))
                                cbocol21.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 1
                                cbocol12.SelectedValue = Trim(arrjoin2(1))
                                cbojoin2.SelectedValue = Trim(arrjoin1(1))
                                cbocol22.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 2
                                cbocol13.SelectedValue = Trim(arrjoin2(1))
                                cbojoin3.SelectedValue = Trim(arrjoin1(1))
                                cbocol23.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 3
                                cbocol14.SelectedValue = Trim(arrjoin2(1))
                                cbojoin4.SelectedValue = Trim(arrjoin1(1))
                                cbocol24.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 4
                                cbocol15.SelectedValue = Trim(arrjoin2(1))
                                cbojoin5.SelectedValue = Trim(arrjoin1(1))
                                cbocol25.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 5
                                cbocol16.SelectedValue = Trim(arrjoin2(1))
                                cbojoin6.SelectedValue = Trim(arrjoin1(1))
                                cbocol26.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 6
                                cbocol17.SelectedValue = Trim(arrjoin2(1))
                                cbojoin7.SelectedValue = Trim(arrjoin1(1))
                                cbocol27.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                            Case 7
                                cbocol18.SelectedValue = Trim(arrjoin2(1))
                                cbojoin8.SelectedValue = Trim(arrjoin1(1))
                                cbocol28.SelectedValue = Trim(arrjoin3(1))
                                Exit Select
                        End Select
                    Next
                End If
                If Trim(wherecon) <> "" Then
                    Dim arrcon = Split(wherecon, ",")
                    Dim cnt As Integer = 0
                    Dim cnt1 As Integer = 0
                    For i = 0 To arrcon.Length - 1
                        Dim arrcon1
                        arrcon1 = Split(arrcon(i), "$##$")
                        Dim arrcon2 = Split(arrcon1(0), ".")
                        If arrcon2(0) = cbotab1.SelectedItem.Text Then
                            Select Case cnt1
                                Case 0
                                    cbocolA11.SelectedValue = Trim(arrcon2(1))
                                    cbofunc11.SelectedValue = Trim(arrcon1(1))
                                    txtval11.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 1
                                    cbocolA12.SelectedValue = Trim(arrcon2(1))
                                    cbofunc12.SelectedValue = Trim(arrcon1(1))
                                    txtval12.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 2
                                    cbocolA13.SelectedValue = Trim(arrcon2(1))
                                    cbofunc13.SelectedValue = Trim(arrcon1(1))
                                    txtval13.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 3
                                    cbocolA14.SelectedValue = Trim(arrcon2(1))
                                    cbofunc14.SelectedValue = Trim(arrcon1(1))
                                    txtval14.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 4
                                    cbocolA15.SelectedValue = Trim(arrcon2(1))
                                    cbofunc15.SelectedValue = Trim(arrcon1(1))
                                    txtval15.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                            End Select
                            cnt1 = cnt1 + 1
                        End If
                        If arrcon2(0) = cbotab2.SelectedItem.Text Then
                            Select Case cnt
                                Case 0
                                    cbocolB21.SelectedValue = Trim(arrcon2(1))
                                    cbofunc21.SelectedValue = Trim(arrcon1(1))
                                    txtval21.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 1
                                    cbocolB22.SelectedValue = Trim(arrcon2(1))
                                    cbofunc22.SelectedValue = Trim(arrcon1(1))
                                    txtval22.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 2
                                    cbocolB23.SelectedValue = Trim(arrcon2(1))
                                    cbofunc23.SelectedValue = Trim(arrcon1(1))
                                    txtval23.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 3
                                    cbocolB24.SelectedValue = Trim(arrcon2(1))
                                    cbofunc24.SelectedValue = Trim(arrcon1(1))
                                    txtval24.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                                Case 4
                                    cbocolB25.SelectedValue = Trim(arrcon2(1))
                                    cbofunc25.SelectedValue = Trim(arrcon1(1))
                                    txtval25.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                                    Exit Select
                            End Select
                            cnt = cnt + 1
                        End If
                    Next
                End If
                txttab11.Value = "where " & cbotab1.SelectedItem.Text & "."
                txttab12.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab13.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab14.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab15.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab16.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab17.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab18.Value = "and " & cbotab1.SelectedItem.Text & "."
                txttab21.Value = cbotab2.SelectedItem.Text & "."
                txttab22.Value = cbotab2.SelectedItem.Text & "."
                txttab23.Value = cbotab2.SelectedItem.Text & "."
                txttab24.Value = cbotab2.SelectedItem.Text & "."
                txttab25.Value = cbotab2.SelectedItem.Text & "."
                txttab26.Value = cbotab2.SelectedItem.Text & "."
                txttab27.Value = cbotab2.SelectedItem.Text & "."
                txttab28.Value = cbotab2.SelectedItem.Text & "."
                txtcon11.Value = "and " & cbotab1.SelectedItem.Text & "."
                txtcon12.Value = "and " & cbotab1.SelectedItem.Text & "."
                txtcon13.Value = "and " & cbotab1.SelectedItem.Text & "."
                txtcon14.Value = "and " & cbotab1.SelectedItem.Text & "."
                txtcon15.Value = "and " & cbotab1.SelectedItem.Text & "."
                txtcon21.Value = "and " & cbotab2.SelectedItem.Text & "."
                txtcon22.Value = "and " & cbotab2.SelectedItem.Text & "."
                txtcon23.Value = "and " & cbotab2.SelectedItem.Text & "."
                txtcon24.Value = "and " & cbotab2.SelectedItem.Text & "."
                txtcon25.Value = "and " & cbotab2.SelectedItem.Text & "."
                If Session("vis") = "1" Then
                    CmdSave.Visible = True
                    Cmdup.Visible = True
                End If
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                ShowConfirm(strmsg)
            End Try
        End If
        'chkRights()
        'ChkOwnership()

    End Sub

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        Page.RegisterStartupScript("ShowConfirm", Script)
    End Function
    'Public Sub Showmsg(ByVal strmsg As String)
    '    Dim str As New System.Text.StringBuilder
    '    str.Append("<Script language=javascript>")
    '    str.Append("alert('" + strmsg + "')")
    '    str.Append("</Script>")
    '    Page.RegisterStartupScript("Showmsg", str.ToString)
    'End Sub
    Public Sub bindclient()
        If cbodept.SelectedIndex <> 0 Then
            Dim cmdgetclient As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & cbodept.SelectedValue & "' order by ClientName", connection)
            Dim dsgetclient As New DataSet
            Dim adpgetclient As New SqlDataAdapter
            adpgetclient.SelectCommand = cmdgetclient
            connection.Open()
            adpgetclient.Fill(dsgetclient)
            connection.Close()
            cmdgetclient.Dispose()
            cboclient.DataSource = dsgetclient
            cboclient.DataTextField = "ClientName"
            cboclient.DataValueField = "autoid"
            cboclient.DataBind()
            cboclient.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindlob()
        If cboclient.SelectedIndex <> 0 And cboclient.SelectedIndex <> -1 Then
            Dim cmdgetlob As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & cbodept.SelectedValue & "' and ClientId='" & cboclient.SelectedValue & "' order by LOB", connection)
            Dim dsgetlob As New DataSet
            Dim adpgetlob As New SqlDataAdapter
            adpgetlob.SelectCommand = cmdgetlob
            connection.Open()
            adpgetlob.Fill(dsgetlob)
            connection.Close()
            cmdgetlob.Dispose()
            cbolob.DataSource = dsgetlob
            cbolob.DataTextField = "LOB"
            cbolob.DataValueField = "autoid"
            cbolob.DataBind()
            cbolob.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindtab()
        Dim client
        Dim lob
        If cboclient.SelectedIndex = 0 Or cboclient.SelectedIndex = -1 Then
            client = 0
        Else
            client = cboclient.SelectedValue
        End If
        If cbolob.SelectedIndex = 0 Or cbolob.SelectedIndex = -1 Then
            lob = 0
        Else
            lob = cbolob.SelectedValue
        End If
        Dim cmdgettab As New SqlCommand("select TableName,VisibleColumn=(VisibleColumn + '$' + TableName) from warslobtablemaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobid='" & lob & "' and Editable='yes' and Importable='yes' order by TableName", connection)
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        cbotab1.DataSource = dsgettab
        cbotab1.DataTextField = "TableName"
        cbotab1.DataValueField = "VisibleColumn"
        cbotab1.DataBind()
        cbotab1.Items.Insert(0, "--Select--")
    End Sub
    Public Sub bindcol()
        If cbotab1.SelectedIndex <> 0 And cbotab1.SelectedIndex <> -1 Then
            Dim arrcol = Split(cbotab1.SelectedValue, "$")
            Dim arrcol1 = Split(arrcol(0), ",")
            Dim cnt As Integer
            For cnt = 0 To arrcol1.length - 1
                cbocolA1.Items.Add(arrcol1(cnt))
                cbocolA2.Items.Add(arrcol1(cnt))
                cbocolA3.Items.Add(arrcol1(cnt))
                cbocolA4.Items.Add(arrcol1(cnt))
                cbocolA5.Items.Add(arrcol1(cnt))
                cbocol11.Items.Add(arrcol1(cnt))
                cbocol12.Items.Add(arrcol1(cnt))
                cbocol13.Items.Add(arrcol1(cnt))
                cbocol14.Items.Add(arrcol1(cnt))
                cbocol15.Items.Add(arrcol1(cnt))
                cbocol16.Items.Add(arrcol1(cnt))
                cbocol17.Items.Add(arrcol1(cnt))
                cbocol18.Items.Add(arrcol1(cnt))
                cbocolA11.Items.Add(arrcol1(cnt))
                cbocolA12.Items.Add(arrcol1(cnt))
                cbocolA13.Items.Add(arrcol1(cnt))
                cbocolA14.Items.Add(arrcol1(cnt))
                cbocolA15.Items.Add(arrcol1(cnt))
            Next
            cbocolA1.Items.Insert(0, "--Select--")
            cbocolA2.Items.Insert(0, "--Select--")
            cbocolA3.Items.Insert(0, "--Select--")
            cbocolA4.Items.Insert(0, "--Select--")
            cbocolA5.Items.Insert(0, "--Select--")
            cbocol11.Items.Insert(0, "--Select--")
            cbocol12.Items.Insert(0, "--Select--")
            cbocol13.Items.Insert(0, "--Select--")
            cbocol14.Items.Insert(0, "--Select--")
            cbocol15.Items.Insert(0, "--Select--")
            cbocol16.Items.Insert(0, "--Select--")
            cbocol17.Items.Insert(0, "--Select--")
            cbocol18.Items.Insert(0, "--Select--")
            cbocolA11.Items.Insert(0, "--Select--")
            cbocolA12.Items.Insert(0, "--Select--")
            cbocolA13.Items.Insert(0, "--Select--")
            cbocolA14.Items.Insert(0, "--Select--")
            cbocolA15.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindclient1()
        If cbodept1.SelectedIndex <> 0 Then
            Dim cmdgetclient1 As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & cbodept1.SelectedValue & "' order by ClientName", connection)
            Dim dsgetclient1 As New DataSet
            Dim adpgetclient1 As New SqlDataAdapter
            adpgetclient1.SelectCommand = cmdgetclient1
            connection.Open()
            adpgetclient1.Fill(dsgetclient1)
            connection.Close()
            cmdgetclient1.Dispose()
            cboclient1.DataSource = dsgetclient1
            cboclient1.DataTextField = "ClientName"
            cboclient1.DataValueField = "autoid"
            cboclient1.DataBind()
            cboclient1.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindlob1()
        If cboclient1.SelectedIndex <> 0 And cboclient1.SelectedIndex <> -1 Then
            Dim cmdgetlob1 As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & cbodept1.SelectedValue & "' and ClientId='" & cboclient1.SelectedValue & "' order by LOB", connection)
            Dim dsgetlob1 As New DataSet
            Dim adpgetlob1 As New SqlDataAdapter
            adpgetlob1.SelectCommand = cmdgetlob1
            connection.Open()
            adpgetlob1.Fill(dsgetlob1)
            connection.Close()
            cmdgetlob1.Dispose()
            cbolob1.DataSource = dsgetlob1
            cbolob1.DataTextField = "LOB"
            cbolob1.DataValueField = "autoid"
            cbolob1.DataBind()
            cbolob1.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindtab1()
        Dim client1
        Dim lob1
        If cboclient1.SelectedIndex = 0 Or cboclient1.SelectedIndex = -1 Then
            client1 = 0
        Else
            client1 = cboclient1.SelectedValue
        End If
        If cbolob1.SelectedIndex = 0 Or cbolob1.SelectedIndex = -1 Then
            lob1 = 0
        Else
            lob1 = cbolob1.SelectedValue
        End If
        Dim cmdgettab1 As New SqlCommand("select TableName,VisibleColumn=(VisibleColumn + '$' + TableName) from warslobtablemaster where DepartmentId='" & cbodept1.SelectedValue & "' and ClientId='" & client1 & "' and lobid='" & lob1 & "' and Editable='yes' and Importable='yes' order by TableName", connection)
        Dim dsgettab1 As New DataSet
        Dim adpgettab1 As New SqlDataAdapter
        adpgettab1.SelectCommand = cmdgettab1
        connection.Open()
        adpgettab1.Fill(dsgettab1)
        connection.Close()
        cmdgettab1.Dispose()
        cbotab2.DataSource = dsgettab1
        cbotab2.DataTextField = "TableName"
        cbotab2.DataValueField = "VisibleColumn"
        cbotab2.DataBind()
        cbotab2.Items.Insert(0, "--Select--")
    End Sub
    Public Sub bindcol1()
        If cbotab2.SelectedIndex <> 0 And cbotab2.SelectedIndex <> -1 Then
            Dim arrcol1 = Split(cbotab2.SelectedValue, "$")
            Dim arrcol11 = Split(arrcol1(0), ",")
            Dim cnt As Integer
            For cnt = 0 To arrcol11.length - 1
                cbocolB1.Items.Add(arrcol11(cnt))
                cbocolB2.Items.Add(arrcol11(cnt))
                cbocolB3.Items.Add(arrcol11(cnt))
                cbocolB4.Items.Add(arrcol11(cnt))
                cbocolB5.Items.Add(arrcol11(cnt))
                cbocol21.Items.Add(arrcol11(cnt))
                cbocol22.Items.Add(arrcol11(cnt))
                cbocol23.Items.Add(arrcol11(cnt))
                cbocol24.Items.Add(arrcol11(cnt))
                cbocol25.Items.Add(arrcol11(cnt))
                cbocol26.Items.Add(arrcol11(cnt))
                cbocol27.Items.Add(arrcol11(cnt))
                cbocol28.Items.Add(arrcol11(cnt))
                cbocolB21.Items.Add(arrcol11(cnt))
                cbocolB22.Items.Add(arrcol11(cnt))
                cbocolB23.Items.Add(arrcol11(cnt))
                cbocolB24.Items.Add(arrcol11(cnt))
                cbocolB25.Items.Add(arrcol11(cnt))
            Next
            cbocolB1.Items.Insert(0, "--Select--")
            cbocolB2.Items.Insert(0, "--Select--")
            cbocolB3.Items.Insert(0, "--Select--")
            cbocolB4.Items.Insert(0, "--Select--")
            cbocolB5.Items.Insert(0, "--Select--")
            cbocol21.Items.Insert(0, "--Select--")
            cbocol22.Items.Insert(0, "--Select--")
            cbocol23.Items.Insert(0, "--Select--")
            cbocol24.Items.Insert(0, "--Select--")
            cbocol25.Items.Insert(0, "--Select--")
            cbocol26.Items.Insert(0, "--Select--")
            cbocol27.Items.Insert(0, "--Select--")
            cbocol28.Items.Insert(0, "--Select--")
            cbocolB21.Items.Insert(0, "--Select--")
            cbocolB22.Items.Insert(0, "--Select--")
            cbocolB23.Items.Insert(0, "--Select--")
            cbocolB24.Items.Insert(0, "--Select--")
            cbocolB25.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindclient2()
        If cbodept2.SelectedIndex <> 0 Then
            Dim cmdgetclient2 As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & cbodept2.SelectedValue & "' order by ClientName", connection)
            Dim dsgetclient2 As New DataSet
            Dim adpgetclient2 As New SqlDataAdapter
            adpgetclient2.SelectCommand = cmdgetclient2
            connection.Open()
            adpgetclient2.Fill(dsgetclient2)
            connection.Close()
            cmdgetclient2.Dispose()
            cboclient2.DataSource = dsgetclient2
            cboclient2.DataTextField = "ClientName"
            cboclient2.DataValueField = "autoid"
            cboclient2.DataBind()
            cboclient2.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub bindlob2()
        If cboclient2.SelectedIndex <> 0 And cboclient2.SelectedIndex <> -1 Then
            Dim cmdgetlob2 As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & cbodept2.SelectedValue & "' and ClientId='" & cboclient2.SelectedValue & "' order by LOB", connection)
            Dim dsgetlob2 As New DataSet
            Dim adpgetlob2 As New SqlDataAdapter
            adpgetlob2.SelectCommand = cmdgetlob2
            connection.Open()
            adpgetlob2.Fill(dsgetlob2)
            connection.Close()
            cmdgetlob2.Dispose()
            cbolob2.DataSource = dsgetlob2
            cbolob2.DataTextField = "LOB"
            cbolob2.DataValueField = "autoid"
            cbolob2.DataBind()
            cbolob2.Items.Insert(0, "--Select--")
        End If
    End Sub
    

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        '' to chk if the user has right to copy the structure?
        dept = cbodept2.SelectedValue
        Dim uid = Session("userid")
        If cboclient2.SelectedIndex > 0 Then
            client = cboclient2.SelectedValue
        Else
            client = "0"
        End If
        If cbolob2.SelectedIndex > 0 Then
            lob = cbolob2.SelectedValue
        Else
            lob = "0"
        End If
        Dim rite = False
        Dim exist As Boolean = False
        If Session("typeofuser") = "Admin" Then
            exist = selectRep.chkAdminSpan(Session("userid"))
            If exist = False Then
                GoTo outofspan
            Else
                rite = True
            End If
        Else
outofspan:
            Dim ts = "select * from idmsUpdateTabStruct where (cmdname='" + lblname.Text + "' and createdby='" + uid + "' and deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "') or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[saveas]='True') "
            Dim cmd12 As New SqlCommand(ts, connection)
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmd12.ExecuteReader
            rite = readerdata.HasRows
            connection.Close()
        End If
        '''''''''''''
        If rite = True Then


            '' to check if the structure name already exists?
            Dim Span() As String
            Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
            '  Dim cbodept2 As String = Span(1)
            Dim cboclient2 As String = Span(1)
            Dim cbolob2 As String = Span(2)
            Dim lob2
            Dim client2
            If cbolob2 = "" Or cbolob2 = "--Select--" Or cbolob2 = "NULL" Then
                lob2 = 0
            Else
                lob2 = cbolob2
            End If
            If cboclient2 = "" Or cboclient2 = "--Select--" Or cboclient2 = "NULL" Then
                client2 = 0
            Else
                client2 = cboclient2
            End If
            Dim stnm = usp.chkstructname(txtname.Text, Me.cbodept2.SelectedValue, client2, lob2)
            ''''''''''''''
            If stnm = "N" Then


                Dim SpanA() As String
                Dim SpanB() As String
                'Dim Span() As String
                Dim arrquerystring() As String
                Dim arrquerystring1() As String
                'Dim arrquerystring2() As String
                ' Dim arrFirstTabColQueryString() As String
                'Dim arrSecondTabColQueryString() As String
                Dim arrJoinTab1QueryString() As String
                Dim arrJoinTab2QueryString() As String
                Dim arrConTab1QueryString() As String
                Dim arrconTab2QueryString() As String

                Dim query1 As New System.Text.StringBuilder
                SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
                SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
                'Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
                arrquerystring = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
                arrquerystring1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob

                '  arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
                ' arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
                arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
                arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
                arrConTab1QueryString = Split(hfconTab1QueryString.Value.ToString(), "#") 'Array for finding columns of first table for condition
                arrconTab2QueryString = Split(hfconTab2QueryString.Value.ToString(), "#") 'Array for finding columns of second table for condition

                '  Dim cbodept As String = SpanA(1)
                Dim cboclient As String = SpanA(1)
                Dim cbolob As String = SpanA(2)
                Dim cbotab1 As String = arrquerystring(3)

                ' Dim cbodept1 As String = SpanB(1)
                Dim cboclient1 As String = SpanB(1)
                Dim cbolob1 As String = SpanB(2)
                Dim cbotab2 As String = arrquerystring1(3)

                '  Dim cbodept2 As String = Span(1)
                ' Dim cboclient2 As String = Span(1)
                'Dim cbolob2 As String = Span(2)

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


                Dim cbocolA11 As String = arrConTab1QueryString(0)
                Dim cbocolA12 As String = arrConTab1QueryString(1)
                Dim cbocolA13 As String = arrConTab1QueryString(2)
                Dim cbocolA14 As String = arrConTab1QueryString(3)
                Dim cbocolA15 As String = arrConTab1QueryString(4)

                Dim cbocolB21 As String = arrconTab2QueryString(0)
                Dim cbocolB22 As String = arrconTab2QueryString(1)
                Dim cbocolB23 As String = arrconTab2QueryString(2)
                Dim cbocolB24 As String = arrconTab2QueryString(3)
                Dim cbocolB25 As String = arrconTab2QueryString(4)

                Dim arrtab1() As String
                arrtab1 = Split(cbotab1, "$")
                Dim arrtab2() As String
                arrtab2 = Split(cbotab2, "$")
                Dim query As String
                Dim qry As String
                query = "update " & arrtab1(1) & " set"
                'Commented As Some Time Value is comes As Select By Rohit
                'query = "update " & arrtab1(0) & " set"

                'If (cbocolA1 <> "" And cbocolB1 <> "") Then
                '    If qry = "" Then
                '        qry = " " & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                '    Else
                '        qry = qry & " ," & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                '    End If
                'End If
                'If (cbocolA2 <> "" And cbocolB2 <> "") Then
                '    If qry = "" Then
                '        qry = " " & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                '    Else
                '        qry = qry & " ," & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                '    End If
                'End If
                'If (cbocolA3 <> "" And cbocolB3 <> "") Then
                '    If qry = "" Then
                '        qry = " " & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                '    Else
                '        qry = qry & " ," & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                '    End If
                'End If
                'If (cbocolA4 <> "" And cbocolB4 <> "") Then
                '    If qry = "" Then
                '        qry = " " & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                '    Else
                '        qry = qry & " ," & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                '    End If
                'End If
                'If (cbocolA5 <> "" And cbocolB5 <> "") Then
                '    If qry = "" Then
                '        qry = " " & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                '    Else
                '        qry = qry & " ," & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                '    End If
                'End If

                '***********************************Date 22/09/2008 By Rohit
                If (cbocolA1 <> "" And cbocolA1 <> "--Select--") And (cbocolB1 <> "" And cbocolB1 <> "--Select--") Then
                    If qry = "" Then
                        qry = " " & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                    Else
                        qry = qry & " ," & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                    End If
                End If
                If (cbocolA2 <> "" And cbocolA2 <> "--Select--") And (cbocolB2 <> "" And cbocolB2 <> " --Select--") Then
                    If qry = "" Then
                        qry = " " & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                    Else
                        qry = qry & " ," & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                    End If
                End If
                If (cbocolA3 <> "" And cbocolA3 <> "--Select--") And (cbocolB3 <> "" And cbocolB3 <> "--Select--") Then
                    If qry = "" Then
                        qry = " " & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                    Else
                        qry = qry & " ," & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                    End If
                End If
                If (cbocolA4 <> "" And cbocolA4 <> "--Select--") And (cbocolB4 <> "" And cbocolB4 <> "--Select--") Then
                    If qry = "" Then
                        qry = " " & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                    Else
                        qry = qry & " ," & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                    End If
                End If
                If (cbocolA5 <> "" And cbocolA5 <> "--Select--") And (cbocolB5 <> "" And cbocolB5 <> "--Select--") Then
                    If qry = "" Then
                        qry = " " & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                    Else
                        qry = qry & " ," & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                    End If
                End If
                qry = qry & " from " & arrtab1(1) & "," & arrtab2(1)


                Dim wherejoin As String = ""
                If cbocol11 <> "" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "" Then
                    qry = qry & " where " & arrtab1(1) & "." & cbocol11 & " " & cbojoin1.SelectedValue & "  " & arrtab2(1) & "." & cbocol21
                    wherejoin = wherejoin & arrtab1(1) & "." & cbocol11 & "$##$" & cbojoin1.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol21
                End If
                If cbocol12 <> "" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol12 & " " & cbojoin2.SelectedValue & "  " & arrtab2(1) & "." & cbocol22
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol12 & "$##$" & cbojoin2.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol22
                End If
                If cbocol13 <> "" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol13 & " " & cbojoin3.SelectedValue & "  " & arrtab2(1) & "." & cbocol23
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol13 & "$##$" & cbojoin3.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol23
                End If

                If cbocol14 <> "" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol14 & " " & cbojoin4.SelectedValue & "  " & arrtab2(1) & "." & cbocol24
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol14 & "$##$" & cbojoin4.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol24
                End If

                If cbocol15 <> "" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol15 & " " & cbojoin5.SelectedValue & "  " & arrtab2(1) & "." & cbocol25
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol15 & "$##$" & cbojoin5.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol25
                End If
                If cbocol16 <> "" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol16 & " " & cbojoin6.SelectedValue & "  " & arrtab2(1) & "." & cbocol26
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol16 & "$##$" & cbojoin6.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol26
                End If
                If cbocol17 <> "" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol17 & " " & cbojoin7.SelectedValue & "  " & arrtab2(1) & "." & cbocol27
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol17 & "$##$" & cbojoin7.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol27
                End If
                If cbocol18 <> "" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocol18 & " " & cbojoin8.SelectedValue & "  " & arrtab2(1) & "." & cbocol28
                    wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol18 & "$##$" & cbojoin8.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol28
                End If


                ''''''''''''''''''''''''''''''''''''''''''''''''''CondtionPart
                ''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim wherecon As String = ""
                If cbocolA11 <> "" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocolA11 & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
                    wherecon = arrtab1(1) & "." & cbocolA11 & "$##$" & cbofunc11.SelectedValue & "$##$ " & txtval11.Value & " "
                End If
                If cbocolA12 <> "" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocolA12 & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & ""
                    wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA12 & "$##$" & cbofunc12.SelectedValue & "$##$" & txtval12.Value & ""
                End If
                If cbocolA13 <> "" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocolA13 & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
                    wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA13 & "$##$" & cbofunc13.SelectedValue & "$##$" & txtval13.Value & ""
                End If
                If cbocolA14 <> "" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocolA14 & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
                    wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA14 & "$##$" & cbofunc14.SelectedValue & "$##$" & txtval14.Value & ""
                End If
                If cbocolA15 <> "" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
                    qry = qry & " and " & arrtab1(1) & "." & cbocolA15 & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
                    wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA15 & "$##$" & cbofunc15.SelectedValue & "$##$" & txtval15.Value & "'"
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''
                If cbocolB21 <> "" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
                    qry = qry & " and " & arrtab2(1) & "." & cbocolB21 & " " & cbofunc21.SelectedValue & " '" & txtval21.Value & "'"
                    wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB21 & "$##$" & cbofunc21.SelectedValue & "$##$" & txtval21.Value & ""
                End If
                If cbocolB22 <> "" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
                    qry = qry & " and " & arrtab2(1) & "." & cbocolB22 & " " & cbofunc22.SelectedValue & " '" & txtval22.Value & "'"
                    wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB22 & "$##$" & cbofunc22.SelectedValue & "$##$" & txtval22.Value & ""
                End If
                If cbocolB23 <> "" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
                    qry = qry & " and " & arrtab2(1) & "." & cbocolB23 & " " & cbofunc23.SelectedValue & " '" & txtval23.Value & "'"
                    wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB23 & "$##$" & cbofunc23.SelectedValue & "$##$" & txtval23.Value & ""
                End If
                If cbocolB24 <> "" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
                    qry = qry & " and " & arrtab2(1) & "." & cbocolB24 & " " & cbofunc24.SelectedValue & " '" & txtval24.Value & "'"
                    wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB24 & "$##$" & cbofunc24.SelectedValue & "$##$" & txtval24.Value & ""
                End If
                If cbocolB25 <> "" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
                    qry = qry & " and " & arrtab2(1) & "." & cbocolB25 & " " & cbofunc25.SelectedValue & " '" & txtval25.Value & "'"
                    wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB25 & "$##$" & cbofunc25.SelectedValue & "$##$" & txtval25.Value & ""
                End If

                query = query & qry
                ' wherecon = wherecon.Replace '", "$#$
                Dim lob
                Dim client
                If cbolob = "" Or cbolob = "--Select--" Or cbolob = "NULL" Then
                    lob = 0
                Else
                    lob = cbolob
                End If
                If cboclient = "" Or cboclient = "--Select--" Or cboclient = "NULL" Then
                    client = 0
                Else
                    client = cboclient
                End If
                Dim lob1
                Dim client1
                If cbolob1 = "" Or cbolob1 = "--Select--" Or cbolob1 = "NULL" Then
                    lob1 = 0
                Else
                    lob1 = cbolob1
                End If
                If cboclient1 = "" Or cboclient1 = "--Select--" Or cboclient1 = "NULL" Then
                    client1 = 0
                Else
                    client1 = cboclient1
                End If
                'Dim lob2
                'Dim client2
                'If cbolob2 = "" Or cbolob2 = "--Select--" Or cbolob2 = "NULL" Then
                'lob2 = 0
                ' Else
                'lob2 = cbolob2
                ' End If
                'If cboclient2 = "" Or cboclient2 = "--Select--" Or cboclient2 = "NULL" Then
                'client2 = 0
                ' Else
                'client2 = cboclient2
                'End If
                ' Commented As on Date 22/09/2008 As Per Select Problem
                'Dim colsto As String
                'If cbocolA1 <> "" Then
                '    colsto = cbocolA1
                'End If
                'If cbocolA2 <> "" Then
                '    colsto = colsto & "," & cbocolA2
                'End If
                'If cbocolA3 <> "" Then
                '    colsto = colsto & "," & cbocolA3
                'End If
                'If cbocolA4 <> "" Then
                '    colsto = colsto & "," & cbocolA4
                'End If
                'If cbocolA5 <> "" Then
                '    colsto = colsto & "," & cbocolA5
                'End If
                'Dim colsfrom As String
                'If cbocolB1 <> "" Then
                '    colsfrom = cbocolB1
                'End If
                'If cbocolB2 <> "" Then
                '    colsfrom = colsfrom & "," & cbocolB2
                'End If
                'If cbocolB3 <> "" Then
                '    colsfrom = colsfrom & "," & cbocolB3
                'End If
                'If cbocolB4 <> "" Then
                '    colsfrom = colsfrom & "," & cbocolB4
                'End If
                'If cbocolB5 <> "" Then
                '    colsfrom = colsfrom & "," & cbocolB5
                'End If
                'Commented On Date 22/09/2008 By Rohit

                Dim colsto As String
                If (cbocolA1 <> "" And cbocolA1 <> "--Select--") Then
                    colsto = cbocolA1
                End If
                If (cbocolA2 <> "" And cbocolA2 <> "--Select--") Then
                    colsto = colsto & "," & cbocolA2
                End If
                If (cbocolA3 <> "" And cbocolA3 <> "--Select--") Then
                    colsto = colsto & "," & cbocolA3
                End If
                If (cbocolA4 <> "" And cbocolA4 <> "--Select--") Then
                    colsto = colsto & "," & cbocolA4
                End If
                If (cbocolA5 <> "" And cbocolA4 <> "--Select--") Then
                    colsto = colsto & "," & cbocolA5
                End If
                Dim colsfrom As String
                If (cbocolB1 <> "" And cbocolB1 <> "--Select--") Then
                    colsfrom = cbocolB1
                End If
                If (cbocolB2 <> "" And cbocolB2 <> "--Select--") Then
                    colsfrom = colsfrom & "," & cbocolB2
                End If
                If (cbocolB3 <> "" And cbocolB3 <> "--Select--") Then
                    colsfrom = colsfrom & "," & cbocolB3
                End If
                If (cbocolB4 <> "" And cbocolB4 <> "--Select") Then
                    colsfrom = colsfrom & "," & cbocolB4
                End If
                If (cbocolB5 <> "" And cbocolB5 <> "--Select--") Then
                    colsfrom = colsfrom & "," & cbocolB5
                End If
                Dim local As String
                If chklocal.Checked = True Then
                    local = "Local"
                Else
                    local = "NonLocal"
                End If

                Try
                    Dim cmd1 As New SqlCommand(query, connection)
                    connection.Open()
                    Dim rowcount = cmd1.ExecuteNonQuery()
                    connection.Close()
                    cmd1.Dispose()
                    Dim cmd As New SqlCommand(query, connection)
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()
                    cmd.Dispose()
                    'Dim cmdins As New SqlCommand("insert into IdmsUpdateTabStruct(CmdName,LocalCmd,DeptIDTo,ClientIDTo,LOBIDTo,TableTo,DeptIDFrom,ClientIDFrom,LOBIDFrom,TableFrom,ColumnsTo,ColumnsFrom,WhereDataJoin,WhereDataCon,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,DeptID,ClientID,LOBID)  Values('" & txtname.Text & "','Local','" & cbodept.SelectedValue & "','" & client & "','" & lob & "','" & arrtab1(1) & "','" & cbodept1.SelectedValue & "','" & client1 & "','" & lob1 & "','" & arrtab2(1) & "','" & colsto & "','" & colsfrom & "','" & wherejoin & "','" & wherecon & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & cbodept2.SelectedValue & "','" & client2 & "','" & lob2 & "')", connection)
                    Dim cmdins As New SqlCommand("idms_UpdateTabStruct ", connection)
                    cmdins.CommandType = CommandType.StoredProcedure
                    With cmdins.Parameters
                        .AddWithValue("@CmdName", txtname.Text)
                        .AddWithValue("@LocalCmd", local)
                        .AddWithValue("@DeptIDTo", cbodept.SelectedValue)
                        .AddWithValue("@ClientIDTo", client)
                        .AddWithValue("@LOBIDTo", lob)
                        .AddWithValue("@TableTo", arrtab1(1))
                        .AddWithValue("@DeptIDFrom", cbodept1.SelectedValue)
                        .AddWithValue("@ClientIDFrom", client1)
                        .AddWithValue("@LOBIDFrom", lob1)
                        .AddWithValue("@TableFrom", arrtab2(1))
                        .AddWithValue("@ColumnsTo", colsto)
                        .AddWithValue("@ColumnsFrom", colsfrom)
                        .AddWithValue("@WhereDataJoin", wherejoin)
                        .AddWithValue("@WhereDataCon", wherecon)
                        .AddWithValue("@CreatedOn", System.DateTime.Now)
                        .AddWithValue("@CreatedBy", Session("userid"))
                        .AddWithValue("@ModifiedOn", System.DateTime.Now)
                        .AddWithValue("@ModifiedBy", Session("userid"))
                        .AddWithValue("@DeptID", cbodept2.SelectedValue)
                        .AddWithValue("@ClientID", client2)
                        .AddWithValue("@LOBID", lob2)

                    End With
                    connection.Open()
                    cmdins.ExecuteNonQuery()
                    connection.Close()
                    cmdins.Dispose()
                    Dim msg = "Table has been updated successfully with no. of record =" + rowcount.ToString()
                    Me.lblmsg.Text = msg
                    Session("nmsg") = msg
                    Dim sqlstr As String = ""
                    sqlstr = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + rowcount.ToString() + "' from logDataTransferMaster where EntityName='" + txtname.Text + "' and Action='Save As'")
                    cmdins = New SqlCommand(sqlstr, connection)
                    connection.Open()
                    cmdins.ExecuteNonQuery()
                    connection.Close()

                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    Dim cmm1 As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Save As'", connection)
                    connection.Open()
                    cmm1.ExecuteNonQuery()
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    '****************************Change*********************************

                    Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
                    cmdins2.CommandType = CommandType.StoredProcedure
                    With cmdins2.Parameters


                        .AddWithValue("@actionBY", Session("userid"))
                        .AddWithValue("@Action", "Run")
                        .AddWithValue("@Date", System.DateTime.Now)
                        .AddWithValue("@Entity", "Update Command")
                        .AddWithValue("@EntityName", txtname.Text)
                        .AddWithValue("@DeptId", cbodept2.SelectedValue)
                        .AddWithValue("@ClientId", client2)
                        .AddWithValue("@LOBId", lob2)

                    End With
                    connection.Open()
                    cmdins2.ExecuteNonQuery()
                    connection.Close()
                    cmdins2.Dispose()
                    '*************change*************
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    Dim cmm2 As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Run'", connection)
                    connection.Open()
                    cmm2.ExecuteNonQuery()
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha

                    ShowConfirm(msg)
                Catch ex As Exception
                    Dim strmsg As String
                    strmsg = Replace(ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    Me.lblmsg.Text = strmsg
                    Session("nmsg") = strmsg
                    i = 0
                End Try
            Else
                ShowConfirm("Structure name already exists.")
            End If
        Else
            i = 0
            Me.lblmsg.Text = "You do not have right to perform this action."
            Session("nmsg") = "You do not have right to perform this action."
        End If
    End Sub

   

    Protected Sub Cmdup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmdup.Click
        '' to chk if the user has right to copy the structure?
        dept = cbodept2.SelectedValue
        Dim uid = Session("userid")
        If cboclient2.SelectedIndex > 0 Then
            client = cboclient2.SelectedValue
        Else
            client = "0"
        End If
        If cbolob2.SelectedIndex > 0 Then
            lob = cbolob2.SelectedValue
        Else
            lob = "0"
        End If
        Dim rite = False
        Dim exist As Boolean = False
        If Session("typeofuser") = "Admin" Then
            exist = selectRep.chkAdminSpan(Session("userid"))
            If exist = False Then
                GoTo outofspan
            Else
                rite = True
            End If
        Else
outofspan:
            Dim ts = "select * from idmsUpdateTabStruct where (cmdname='" + lblname.Text + "' and createdby='" + uid + "' and deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "') or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True') "
            Dim cmd12 As New SqlCommand(ts, connection)
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmd12.ExecuteReader
            rite = readerdata.HasRows
            connection.Close()
        End If
        ''''''''''''
        If rite = True Then


            Dim SpanA() As String
            Dim SpanB() As String
            Dim Span() As String

            Dim arrQueryString() As String
            Dim arrQueryString1() As String
            ' Dim arrQueryString2() As String
            'Dim arrFirstTabColQueryString() As String
            'Dim arrSecondTabColQueryString() As String
            Dim arrJoinTab1QueryString() As String
            Dim arrJoinTab2QueryString() As String
            Dim arrConTab1QueryString() As String
            Dim arrconTab2QueryString() As String

            Dim query1 As New System.Text.StringBuilder

            SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
            SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
            Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lobent & Lob


            arrQueryString = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
            arrQueryString1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
            ' arrQueryString2 = Split(hidQueryString2.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
            'arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
            'arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
            arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
            arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
            arrConTab1QueryString = Split(hfconTab1QueryString.Value.ToString(), "#") 'Array for finding columns of first table for condition
            arrconTab2QueryString = Split(hfconTab2QueryString.Value.ToString(), "#") 'Array for finding columns of second table for condition


            Dim cboclient As String = SpanA(1)
            Dim cbolob As String = SpanA(2)
            Dim cbotab1 As String = arrQueryString(3)

            Dim cboclient1 As String = SpanB(1)
            Dim cbolob1 As String = SpanB(2)
            Dim cbotab2 As String = arrQueryString1(3)


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


            Dim cbocolA11 As String = arrConTab1QueryString(0)
            Dim cbocolA12 As String = arrConTab1QueryString(1)
            Dim cbocolA13 As String = arrConTab1QueryString(2)
            Dim cbocolA14 As String = arrConTab1QueryString(3)
            Dim cbocolA15 As String = arrConTab1QueryString(4)

            Dim cbocolB21 As String = arrconTab2QueryString(0)
            Dim cbocolB22 As String = arrconTab2QueryString(1)
            Dim cbocolB23 As String = arrconTab2QueryString(2)
            Dim cbocolB24 As String = arrconTab2QueryString(3)
            Dim cbocolB25 As String = arrconTab2QueryString(4)

            Dim arrtab1() As String
            arrtab1 = Split(cbotab1, "$")
            Dim arrtab2() As String
            arrtab2 = Split(cbotab2, "$")
            Dim query As String
            Dim qry As String
            query = "update " & arrtab1(1) & " set"
            'query = "update " & arrtab1(0) & " set"

            If (cbocolA1 <> "" And cbocolA1 <> "--Select--") And (cbocolB1 <> "" And cbocolB1 <> "--Select--") Then
                If qry = "" Then
                    qry = " " & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                Else
                    qry = qry & " ," & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
                End If
            End If
            If (cbocolA2 <> "" And cbocolA2 <> "--Select--") And (cbocolB2 <> "" And cbocolB2 <> " --Select--") Then
                If qry = "" Then
                    qry = " " & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                Else
                    qry = qry & " ," & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
                End If
            End If
            If (cbocolA3 <> "" And cbocolA3 <> "--Select--") And (cbocolB3 <> "" And cbocolB3 <> "--Select--") Then
                If qry = "" Then
                    qry = " " & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                Else
                    qry = qry & " ," & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
                End If
            End If
            If (cbocolA4 <> "" And cbocolA4 <> "--Select--") And (cbocolB4 <> "" And cbocolB4 <> "--Select--") Then
                If qry = "" Then
                    qry = " " & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                Else
                    qry = qry & " ," & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
                End If
            End If
            If (cbocolA5 <> "" And cbocolA5 <> "--Select--") And (cbocolB5 <> "" And cbocolB5 <> "--Select--") Then
                If qry = "" Then
                    qry = " " & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                Else
                    qry = qry & " ," & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
                End If
            End If
            qry = qry & " from " & arrtab1(1) & "," & arrtab2(1)


            Dim wherejoin As String = ""
            If cbocol11 <> "" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "" Then
                qry = qry & " where " & arrtab1(1) & "." & cbocol11 & " " & cbojoin1.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol21
                wherejoin = wherejoin & arrtab1(1) & "." & cbocol11 & "$##$" & cbojoin1.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol21
            End If
            If cbocol12 <> "" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol12 & " " & cbojoin2.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol22
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol12 & "$##$" & cbojoin2.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol22
            End If
            If cbocol13 <> "" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol13 & " " & cbojoin3.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol23
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol13 & "$##$" & cbojoin3.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol23
            End If

            If cbocol14 <> "" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol14 & " " & cbojoin4.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol24
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol14 & "$##$" & cbojoin4.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol24
            End If

            If cbocol15 <> "" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol15 & " " & cbojoin5.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol25
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol15 & "$##$" & cbojoin5.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol25
            End If
            If cbocol16 <> "" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol16 & " " & cbojoin6.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol26
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol16 & "$##$" & cbojoin6.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol26
            End If
            If cbocol17 <> "" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol17 & " " & cbojoin7.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol27
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol17 & "$##$" & cbojoin7.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol27
            End If
            If cbocol18 <> "" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocol18 & " " & cbojoin8.SelectedItem.Text & "  " & arrtab2(1) & "." & cbocol28
                wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol18 & "$##$" & cbojoin8.SelectedItem.Text & "$##$" & arrtab2(1) & "." & cbocol28
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''CondtionPart
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim wherecon As String = ""
            If cbocolA11 <> "" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocolA11 & " " & cbofunc11.SelectedItem.Text & " '" & txtval11.Value & "'"
                wherecon = arrtab1(1) & "." & cbocolA11 & "$##$" & cbofunc11.SelectedItem.Text & "$##$ " & txtval11.Value & " "
            End If
            If cbocolA12 <> "" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocolA12 & " " & cbofunc12.SelectedItem.Text & " '" & txtval12.Value & ""
                wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA12 & "$##$" & cbofunc12.SelectedItem.Text & "$##$" & txtval12.Value & ""
            End If
            If cbocolA13 <> "" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocolA13 & " " & cbofunc13.SelectedItem.Text & " '" & txtval13.Value & "'"
                wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA13 & "$##$" & cbofunc13.SelectedItem.Text & "$##$" & txtval13.Value & ""
            End If
            If cbocolA14 <> "" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocolA14 & " " & cbofunc14.SelectedItem.Text & " '" & txtval14.Value & "'"
                wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA14 & "$##$" & cbofunc14.SelectedItem.Text & "$##$" & txtval14.Value & ""
            End If
            If cbocolA15 <> "" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
                qry = qry & " and " & arrtab1(1) & "." & cbocolA15 & " " & cbofunc15.SelectedItem.Text & " '" & txtval15.Value & "'"
                wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA15 & "$##$" & cbofunc15.SelectedItem.Text & "$##$" & txtval15.Value & "'"
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If cbocolB21 <> "" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
                qry = qry & " and " & arrtab2(1) & "." & cbocolB21 & " " & cbofunc21.SelectedItem.Text & " '" & txtval21.Value & "'"
                wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB21 & "$##$" & cbofunc21.SelectedItem.Text & "$##$" & txtval21.Value & ""
            End If
            If cbocolB22 <> "" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
                qry = qry & " and " & arrtab2(1) & "." & cbocolB22 & " " & cbofunc22.SelectedItem.Text & " '" & txtval22.Value & "'"
                wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB22 & "$##$" & cbofunc22.SelectedItem.Text & "$##$" & txtval22.Value & ""
            End If
            If cbocolB23 <> "" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
                qry = qry & " and " & arrtab2(1) & "." & cbocolB23 & " " & cbofunc23.SelectedItem.Text & " '" & txtval23.Value & "'"
                wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB23 & "$##$" & cbofunc23.SelectedItem.Text & "$##$" & txtval23.Value & ""
            End If
            If cbocolB24 <> "" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
                qry = qry & " and " & arrtab2(1) & "." & cbocolB24 & " " & cbofunc24.SelectedItem.Text & " '" & txtval24.Value & "'"
                wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB24 & "$##$" & cbofunc24.SelectedItem.Text & "$##$" & txtval24.Value & ""
            End If
            If cbocolB25 <> "" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
                qry = qry & " and " & arrtab2(1) & "." & cbocolB25 & " " & cbofunc25.SelectedItem.Text & " '" & txtval25.Value & "'"
                wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB25 & "$##$" & cbofunc25.SelectedItem.Text & "$##$" & txtval25.Value & ""
            End If

            query = query & qry
            ' wherecon = wherecon.Replace '", "$#$
            Dim lob
            Dim client
            If cbolob = "" Or cbolob = "--Select--" Or cbolob = "NULL" Then
                lob = 0
            Else
                lob = cbolob
            End If
            If cboclient = "" Or cboclient = "--Select--" Or cboclient = "NULL" Then
                client = 0
            Else
                client = cboclient
            End If
            Dim lob1
            Dim client1
            If cbolob1 = "" Or cbolob1 = "--Select--" Or cbolob1 = "NULL" Then
                lob1 = 0
            Else
                lob1 = cbolob1
            End If
            If cboclient1 = "" Or cboclient1 = "--Select--" Or cboclient1 = "NULL" Then
                client1 = 0
            Else
                client1 = cboclient1
            End If
            Dim lob2
            Dim client2
            If cbolob2 = "" Or cbolob2 = "--Select--" Or cbolob2 = "NULL" Then
                lob2 = 0
            Else
                lob2 = cbolob2
            End If
            If cboclient2 = "" Or cboclient2 = "--Select--" Or cboclient2 = "NULL" Then
                client2 = 0
            Else
                client2 = cboclient2
            End If
            Dim colsto As String
            If (cbocolA1 <> "" And cbocolA1 <> "--Select--") Then
                colsto = cbocolA1
            End If
            If (cbocolA2 <> "" And cbocolA2 <> "--Select--") Then
                colsto = colsto & "," & cbocolA2
            End If
            If (cbocolA3 <> "" And cbocolA3 <> "--Select--") Then
                colsto = colsto & "," & cbocolA3
            End If
            If (cbocolA4 <> "" And cbocolA4 <> "--Select--") Then
                colsto = colsto & "," & cbocolA4
            End If
            If (cbocolA5 <> "" And cbocolA4 <> "--Select--") Then
                colsto = colsto & "," & cbocolA5
            End If
            Dim colsfrom As String
            If (cbocolB1 <> "" And cbocolB1 <> "--Select--") Then
                colsfrom = cbocolB1
            End If
            If (cbocolB2 <> "" And cbocolB2 <> "--Select--") Then
                colsfrom = colsfrom & "," & cbocolB2
            End If
            If (cbocolB3 <> "" And cbocolB3 <> "--Select--") Then
                colsfrom = colsfrom & "," & cbocolB3
            End If
            If (cbocolB4 <> "" And cbocolB4 <> "--Select") Then
                colsfrom = colsfrom & "," & cbocolB4
            End If
            If (cbocolB5 <> "" And cbocolB5 <> "--Select--") Then
                colsfrom = colsfrom & "," & cbocolB5
            End If
            Dim local As String
            If chklocal.Checked Then
                local = "Local"
            Else
                local = "NonLocal"
            End If
            Try
                Dim cmd As New SqlCommand(query, connection)
                connection.Open()
                Dim k = cmd.ExecuteNonQuery()
                connection.Close()
                cmd.Dispose()
                Dim cmdupd As New SqlCommand("update IdmsUpdateTabStruct set DeptIdTo='" & cbodept.SelectedValue & "',ClientIdTo='" & client & "',LOBIdTo='" & lob & "',TableTo='" & arrtab1(1) & "',DeptIdFrom='" & cbodept1.SelectedValue & "',ClientIdFrom='" & client1 & "',LobIdFrom='" & lob1 & "',TableFrom='" & arrtab2(1) & "',ColumnsTo='" & colsto & "',ColumnsFrom='" & colsfrom & "',wheredatajoin='" & wherejoin & "',wheredatacon='" & wherecon & "',LocalCmd='" & local & "' where CmdName='" & lblname.Text & "'", connection)
                ',DeptID='" & cbodept2.SelectedValue & "',ClientID='" & client2 & "',LOBID='" & lob2 & "'
                connection.Open()
                cmdupd.ExecuteNonQuery()
                connection.Close()
                cmdupd.Dispose()
                ShowConfirm("Table has been updated successfully with no records= " + k.ToString())

                '****************************Change*********************************

                Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters
                    

                    .AddWithValue("@actionBY", Session("userid"))
                    .AddWithValue("@Action", "Edit")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@Entity", "Update Command")
                    .AddWithValue("@EntityName", lblname.Text)
                    .AddWithValue("@DeptId", cbodept2.SelectedValue)
                    .AddWithValue("@ClientId", client2)
                    .AddWithValue("@LOBId", lob2)

                End With
                connection.Open()
                cmdins2.ExecuteNonQuery()
                connection.Close()
                cmdins2.Dispose()
                Dim sqlstr As String = ""
                sqlstr = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + k.ToString() + "' from logDataTransferMaster where EntityName='" + lblname.Text + "' and Action='Edit'")
                cmdins2 = New SqlCommand(sqlstr, connection)
                connection.Open()
                cmdins2.ExecuteNonQuery()
                connection.Close()

                '*************change*************
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & lblname.Text & "' and Action='Edit'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                '****************************Change*********************************

                cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", Session("userid"))
                    .AddWithValue("@Action", "Run")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@Entity", "Update Command")
                    .AddWithValue("@EntityName", lblname.Text)
                    .AddWithValue("@DeptId", cbodept2.SelectedValue)
                    .AddWithValue("@ClientId", client2)
                    .AddWithValue("@LOBId", lob2)

                End With
                connection.Open()
                cmdins2.ExecuteNonQuery()
                connection.Close()
                cmdins2.Dispose()
                '*************change*************
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim cmm2 As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & lblname.Text & "' and Action='Run'", connection)
                connection.Open()
                cmm2.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                ShowConfirm(strmsg)
            End Try
        Else
            ShowConfirm("You dont have right to perform this action.")
        End If
    End Sub
    '********************change to check Rights********************************
    Public Sub chkRights()
        Dim SaveRights As String
        Dim RunRights As String
        Dim btnrights As New SqlCommand("Select SaveAs,Run  from IDMS_UpdateCommandRights where userid='" & Session("userid") & "' and CmdID='" & Request("recid") & "' ", connection)
        connection.Open()
        Dim btnreader As SqlDataReader = btnrights.ExecuteReader
        While btnreader.Read
            SaveRights = (btnreader("SaveAs")).ToString
            If (SaveRights = "True") Then
                CmdSave.Visible = True
                Label2.Visible = True
                txtname.Visible = True

            Else
                'dcl.Enabled = False
                CmdSave.Visible = False
                Label2.Visible = False
                txtname.Visible = False

            End If
            RunRights = (btnreader("Run")).ToString
            If (RunRights = "True") Then
                Cmdup.Visible = True
            Else

                Cmdup.Visible = False
            End If
            If (SaveRights = "True") And (RunRights = "True") Then
                'above_where.Disabled = False
            ElseIf (LCase(SaveRights) = "false" Or SaveRights = "") And (RunRights = "True") Then
                above_where.Enabled = False
            Else
                above_where.Enabled = False
                
            End If
        End While
        connection.Close()
        btnreader.Close()

    End Sub
    Public Sub ChkOwnership()
        Dim ChkOwner As String
        Dim btnOwner As New SqlCommand("Select createdby  from IdmsUpdateTabStruct where createdby='" & Session("userid") & "' and CmdID='" & Request("recid") & "' ", connection)
        connection.Open()
        Dim btnreader As SqlDataReader = btnOwner.ExecuteReader
        While btnreader.Read
            ChkOwner = (btnreader("createdby")).ToString
            If (ChkOwner = Session("userid")) Then
                Cmdup.Visible = True
            Else
                Cmdup.Visible = False

            End If
        End While
        connection.Close()
        btnreader.Close()

    End Sub
    '********************change to check Rights********************************
End Class
