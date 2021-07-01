Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_CreateView
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cbodept1.Attributes.Add("onchange", "javascript:getclient1();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        Me.cboclient1.Attributes.Add("onchange", "javascript:getlob1();")
        Me.cbolob.Attributes.Add("onchange", "javascript:gettab2();")
        Me.cbolob1.Attributes.Add("onchange", "javascript:chksapnval2();")
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                Me.Gettable.Visible = False
                Me.savespan.Visible = True
                Me.cmdcreatetabmul.Visible = True
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
                End If
            Else
                Me.spandisplay.Visible = False
                Me.Gettable.Visible = True
                Me.savespan.Visible = False
                Me.cmdcreatetab.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
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
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
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
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
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
                cbodept.Items.Insert(0, "--Select--")
                cbodept1.Items.Insert(0, "--Select--")
            End If


        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub
    ''''**************this code is now run from ExecView.aspx*****************************************
    ''''**********************************************************************************************
    ''''Private Sub cmdcreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcreate.Click
    ''''    Dim query As New System.Text.StringBuilder
    ''''    Dim query1 As String = "Create View " & txtname.Text & " As select "
    ''''    Dim i As Integer
    ''''    Dim cols1 As String = Request("lsttab2cols")
    ''''    Dim arr1() As String
    ''''    Dim vcols As String = ""
    ''''    Dim vcols1
    ''''    arr1 = Split(cols1, ",")
    ''''    Dim cnt As Integer = 0
    ''''    Dim formula As String
    ''''    If Request("txtformula1") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula1")
    ''''        Else
    ''''            formula = Request("txtformula1")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula2") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula2")
    ''''        Else
    ''''            formula = Request("txtformula2")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula3") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula3")
    ''''        Else
    ''''            formula = Request("txtformula3")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula4") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula4")
    ''''        Else
    ''''            formula = Request("txtformula4")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula5") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula5")
    ''''        Else
    ''''            formula = Request("txtformula5")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula6") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula6")
    ''''        Else
    ''''            formula = Request("txtformula6")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula7") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula7")
    ''''        Else
    ''''            formula = Request("txtformula7")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula8") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula8")
    ''''        Else
    ''''            formula = Request("txtformula8")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula9") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula9")
    ''''        Else
    ''''            formula = Request("txtformula9")
    ''''        End If
    ''''    End If
    ''''    If Request("txtformula10") <> "" Then
    ''''        cnt = cnt + 1
    ''''        If formula <> "" Then
    ''''            formula = formula & "," & Request("txtformula10")
    ''''        Else
    ''''            formula = Request("txtformula10")
    ''''        End If
    ''''    End If
    ''''    cnt = cnt + arr1.Length
    ''''    Dim heading As String = ""
    ''''    For i = 1 To cnt
    ''''        If heading <> "" Then
    ''''            heading = heading & "," & Trim(Request("txtcol" & i))
    ''''        Else
    ''''            heading = Trim(Request("txtcol" & i))
    ''''        End If
    ''''    Next
    ''''    For i = 0 To arr1.Length - 1
    ''''        If query.ToString = "" Then
    ''''            query.Append(arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
    ''''        Else
    ''''            query.Append("," & arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
    ''''        End If
    ''''        vcols1 = Split(arr1(i), ".")
    ''''        If vcols = "" Then
    ''''            vcols = vcols1(1)
    ''''        Else
    ''''            vcols = vcols & "," & vcols1(1)
    ''''        End If
    ''''    Next
    ''''    Dim j As Integer = 0
    ''''    Dim cnt1 = 1
    ''''    If formula <> "" Then
    ''''        For j = i + 1 To cnt
    ''''            query.Append("," & Request("txtformula" & cnt1) & " as " & Request("txtcol" & j) & " ")
    ''''            cnt1 = cnt1 + 1
    ''''        Next
    ''''    End If
    ''''    query1 = query1 & query.ToString & "from "
    ''''    Dim arrtab = Split(Request("lsttab2"), "$")
    ''''    j = 0
    ''''    Dim tab As String = ""
    ''''    Dim arrtab1
    ''''    For j = 0 To arrtab.length - 1
    ''''        If j > 0 Then
    ''''            arrtab1 = Split(arrtab(j), ",")
    ''''            arrtab(j) = arrtab1(0)
    ''''            If tab = "" Then
    ''''                tab = arrtab(j)
    ''''            Else
    ''''                tab = tab & "," & arrtab(j)
    ''''            End If
    ''''        End If
    ''''    Next
    ''''    query1 = query1 & tab
    ''''    Dim qry1 As String = ""
    ''''    If Request("cbocol11") <> "--Select--" And cbojoin1.SelectedIndex <> 0 And Request("cbocol21") <> "--Select--" Then
    ''''        query1 = query1 & " where " & Request("cbocol11") & cbojoin1.SelectedValue & Request("cbocol21")
    ''''        qry1 = Request("cbocol11") & "$##$" & cbojoin1.SelectedValue & "$##$" & Request("cbocol21")
    ''''    End If
    ''''    If Request("cbocol12") <> "--Select--" And cbojoin2.SelectedIndex <> 0 And Request("cbocol22") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol12") & cbojoin2.SelectedValue & Request("cbocol22")
    ''''        qry1 = qry1 & "," & Request("cbocol12") & "$##$" & cbojoin2.SelectedValue & "$##$" & Request("cbocol22")
    ''''    End If
    ''''    If Request("cbocol13") <> "--Select--" And cbojoin3.SelectedIndex <> 0 And Request("cbocol23") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol13") & cbojoin3.SelectedValue & Request("cbocol23")
    ''''        qry1 = qry1 & "," & Request("cbocol13") & "$##$" & cbojoin3.SelectedValue & "$##$" & Request("cbocol23")
    ''''    End If
    ''''    If Request("cbocol14") <> "--Select--" And cbojoin4.SelectedIndex <> 0 And Request("cbocol24") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol14") & cbojoin4.SelectedValue & Request("cbocol24")
    ''''        qry1 = qry1 & "," & Request("cbocol14") & "$##$" & cbojoin4.SelectedValue & "$##$" & Request("cbocol24")
    ''''    End If
    ''''    If Request("cbocol15") <> "--Select--" And cbojoin5.SelectedIndex <> 0 And Request("cbocol25") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol15") & cbojoin5.SelectedValue & Request("cbocol25")
    ''''        qry1 = qry1 & "," & Request("cbocol15") & "$##$" & cbojoin5.SelectedValue & "$##$" & Request("cbocol25")
    ''''    End If
    ''''    If Request("cbocol16") <> "--Select--" And cbojoin6.SelectedIndex <> 0 And Request("cbocol26") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol16") & cbojoin6.SelectedValue & Request("cbocol26")
    ''''        qry1 = qry1 & "," & Request("cbocol16") & "$##$" & cbojoin6.SelectedValue & "$##$" & Request("cbocol26")
    ''''    End If
    ''''    If Request("cbocol17") <> "--Select--" And cbojoin7.SelectedIndex <> 0 And Request("cbocol27") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol17") & cbojoin7.SelectedValue & Request("cbocol27")
    ''''        qry1 = qry1 & "," & Request("cbocol17") & "$##$" & cbojoin7.SelectedValue & "$##$" & Request("cbocol27")
    ''''    End If
    ''''    If Request("cbocol18") <> "--Select--" And cbojoin8.SelectedIndex <> 0 And Request("cbocol28") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol18") & cbojoin8.SelectedValue & Request("cbocol28")
    ''''        qry1 = qry1 & "," & Request("cbocol18") & "$##$" & cbojoin8.SelectedValue & "$##$" & Request("cbocol28")
    ''''    End If
    ''''    If Request("cbocol19") <> "--Select--" And cbojoin9.SelectedIndex <> 0 And Request("cbocol29") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol19") & cbojoin9.SelectedValue & Request("cbocol29")
    ''''        qry1 = qry1 & "," & Request("cbocol19") & "$##$" & cbojoin9.SelectedValue & "$##$" & Request("cbocol29")
    ''''    End If
    ''''    If Request("cbocol20") <> "--Select--" And cbojoin10.SelectedIndex <> 0 And Request("cbocol30") <> "--Select--" Then
    ''''        query1 = query1 & " and " & Request("cbocol20") & cbojoin10.SelectedValue & Request("cbocol30")
    ''''        qry1 = qry1 & "," & Request("cbocol20") & "$##$" & cbojoin10.SelectedValue & "$##$" & Request("cbocol30")
    ''''    End If

    ''''    ''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''    Dim qry As String = ""
    ''''    If Request("cbocolA1") <> "--Select--" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
    ''''        If Request("cbocol11") <> "--Select--" And cbojoin1.SelectedIndex <> 0 And Request("cbocol21") <> "--Select--" Then
    ''''            query1 = query1 & " and " & Request("cbocolA1") & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
    ''''        Else
    ''''            query1 = query1 & " where " & Request("cbocolA1") & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
    ''''        End If
    ''''        qry = Request("cbocolA1") & "$##$" & cbofunc11.SelectedValue & "$##$'" & txtval11.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA2") <> "--Select--" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA2") & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA2") & "$##$" & cbofunc12.SelectedValue & "$##$'" & txtval12.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA3") <> "--Select--" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA3") & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA3") & "$##$" & cbofunc13.SelectedValue & "$##$'" & txtval13.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA4") <> "--Select--" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA4") & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA4") & "$##$" & cbofunc14.SelectedValue & "$##$'" & txtval14.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA5") <> "--Select--" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA5") & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA5") & "$##$" & cbofunc15.SelectedValue & "$##$'" & txtval15.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA6") <> "--Select--" And cbofunc16.SelectedIndex <> 0 And txtval16.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA6") & " " & cbofunc16.SelectedValue & " '" & txtval16.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA6") & "$##$" & cbofunc16.SelectedValue & "$##$'" & txtval16.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA7") <> "--Select--" And cbofunc17.SelectedIndex <> 0 And txtval17.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA7") & " " & cbofunc17.SelectedValue & " '" & txtval17.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA7") & "$##$" & cbofunc17.SelectedValue & "$##$'" & txtval17.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA8") <> "--Select--" And cbofunc18.SelectedIndex <> 0 And txtval18.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA8") & " " & cbofunc18.SelectedValue & " '" & txtval18.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA8") & "$##$" & cbofunc18.SelectedValue & "$##$'" & txtval18.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA9") <> "--Select--" And cbofunc19.SelectedIndex <> 0 And txtval19.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA9") & " " & cbofunc19.SelectedValue & " '" & txtval19.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA9") & "$##$" & cbofunc19.SelectedValue & "$##$'" & txtval19.Value & "'"
    ''''    End If
    ''''    If Request("cbocolA10") <> "--Select--" And cbofunc20.SelectedIndex <> 0 And txtval20.Value <> "" Then
    ''''        query1 = query1 & " and " & Request("cbocolA10") & " " & cbofunc20.SelectedValue & " '" & txtval20.Value & "'"
    ''''        qry = qry & "," & Request("cbocolA10") & "$##$" & cbofunc20.SelectedValue & "$##$'" & txtval20.Value & "'"
    ''''    End If
    ''''    If Request("lstgroup") <> "" Then
    ''''        query1 = query1 & " group by " & Request("lstgroup")
    ''''    End If
    ''''    Try
    ''''        Dim cmddata As New SqlCommand(query1, connection)
    ''''        connection.Open()
    ''''        cmddata.ExecuteNonQuery()
    ''''        connection.Close()
    ''''        cmddata.Dispose()
    ''''    Catch ex As Exception
    ''''        Dim strmsg As String
    ''''        strmsg = Replace(ex.Message.ToString, "'", "")
    ''''        strmsg = Replace(strmsg, vbCrLf, " ")
    ''''        Showmsg(strmsg)
    ''''    End Try
    ''''    Dim lob
    ''''    Dim client
    ''''    If Request("cbolob1") = "" Or Request("cbolob1") = "--Select--" Then
    ''''        lob = 0
    ''''    Else
    ''''        lob = Request("cbolob1")
    ''''    End If
    ''''    If Request("cboclient1") = "" Or Request("cboclient1") = "--Select--" Then
    ''''        client = 0
    ''''    Else
    ''''        client = Request("cboclient1")
    ''''    End If
    ''''    Try
    ''''        Dim cmdins As New SqlCommand("insert into warslobtablemaster values('" & lob & "','" & txtname.Text & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & heading & "','No','No','" & cbodept1.SelectedValue & "','" & client & "')", connection)
    ''''        connection.Open()
    ''''        cmdins.ExecuteNonQuery()
    ''''        connection.Close()
    ''''        cmdins.Dispose()
    ''''    Catch ex As Exception
    ''''        Dim strmsg As String
    ''''        strmsg = Replace(ex.Message.ToString, "'", "")
    ''''        strmsg = Replace(strmsg, vbCrLf, " ")
    ''''        Showmsg(strmsg)
    ''''    End Try
    ''''    qry = Trim(Replace(qry, "'", "$#$"))
    ''''    qry1 = Trim(qry1)
    ''''    Try
    ''''        Dim cmdinsview As New SqlCommand("insert into idmsviewmaster values('" & txtname.Text & "','" & cols1 & "','" & tab & "','" & qry1 & "','" & qry & "','" & cbodept1.SelectedValue & "','" & client & "','" & lob & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & heading & "','" & Request("lstgroup") & "','" & formula & "')", connection)
    ''''        connection.Open()
    ''''        cmdinsview.ExecuteNonQuery()
    ''''        connection.Close()
    ''''        cmdinsview.Dispose()
    ''''    Catch ex As Exception
    ''''        Dim strmsg As String
    ''''        strmsg = Replace(ex.Message.ToString, "'", "")
    ''''        strmsg = Replace(strmsg, vbCrLf, " ")
    ''''        Showmsg(strmsg)
    ''''    End Try
    ''''    Showmsg("View has been created successfully.")
    ''''End Sub


End Class
