Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_EditViewt
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd2 As SqlCommand
    Dim rdr As SqlDataReader
    Public colname As String
    Public formula
    Public heading
    Dim groupby1 As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dept_id.Visible = False
        client_id.Visible = False
        lob_id.Visible = False

        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        connection.Open()
        cmd2 = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd2.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplaymul.Visible = True
                Me.savespandisplay.Visible = True
                Me.cmdsaveasmul.Visible = True
                Me.cmdcreatetabmul.Visible = True
                dept_id.Visible = True
                client_id.Visible = True
                lob_id.Visible = True

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
                Me.Gettable.Visible = True
                Me.cmdsaveas.Visible = True
                Me.cmdcreatetab.Visible = True
            End If
        End If
        rdr.Close()
        cmd2.Dispose()
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cbodept1.Attributes.Add("onchange", "javascript:getclient1();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        Me.cboclient1.Attributes.Add("onchange", "javascript:getlob1();")
        Me.cbolob1.Attributes.Add("onchange", "javascript:chklocalenable();")
        Me.cbolob.Attributes.Add("onchange", "javascript:gettab2();")
        'Me.chkLocalView.Attributes.Add("OnLoad", "javascript:disablechkLocalView();")


        Dim str1 As String = ">="
        bindDdlWithGreaterEqualTo()
        'If Me.IsPostBack = False Then
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

        Dim strid = Request("recid")
        Dim strtabname As String = ""
        Dim wherejoin As String = ""
        Dim wherecon As String
        Dim ViewType As String
        Dim deptid As Integer
        Dim clientid As Integer
        Dim lobid As Integer

        Try
            Dim cmdget As New SqlCommand("select * from idmsviewmaster where viewid='" & strid & "'", connection)
            Dim drget As SqlDataReader
            'connection.Open()
            drget = cmdget.ExecuteReader
            If drget.Read Then
                strtabname = Trim(drget("tablename").ToString)

                colname = Trim(drget("colname").ToString)
                wherejoin = Trim(drget("wheredatajoin").ToString)
                wherecon = Trim(drget("wheredatacon").ToString)
                deptid = CType(Trim(drget("deptid").ToString), Integer)
                clientid = CType(Trim(drget("clientid").ToString), Integer)
                lobid = CType(Trim(drget("lobid").ToString), Integer)
                lblname.Text = drget("ViewName").ToString
                lblname1.Value = drget("ViewName").ToString
                ViewType = drget("LocalView").ToString
                heading = drget("headings").ToString
                formula = drget("formula").ToString
                groupby1 = drget("groupby").ToString
                groupby1 = Trim(groupby1.Replace("group by ", ""))
            End If
            drget.Close()
            'connection.Close()
            cmdget.Dispose()
            formula = formula.Replace("$#$", "'")
            cbodept1.SelectedValue = deptid
            bindclient()
            If clientid = 0 Then
                cboclient1.SelectedIndex = 0
            Else
                cboclient1.SelectedValue = clientid
            End If
            bindlob()
            If lobid = 0 Then
                cbolob1.SelectedIndex = 0
            Else
                cbolob1.SelectedValue = lobid
            End If
            Dim i As Integer = 0
            Dim j As Integer

            Dim arrtab = Split(strtabname, ",")
            For i = 0 To arrtab.Length - 1
                Dim cmdgetcol As New SqlCommand("select Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where TableName='" & arrtab(i) & "' union all select null as Visiblecolumn,Headings as Visiblecolumn from idmsviewmaster where viewname='" & arrtab(i) & "'", connection)
                Dim drgetcol As SqlDataReader
                'connection.Open()
                drgetcol = cmdgetcol.ExecuteReader
                Dim arrlength As Int16
                'below code  altered by atul to create view from view
                If drgetcol.Read Then
                    Dim arrcol()
                    lsttab2.Items.Add(arrtab(i))
                    If (drgetcol("Headings").ToString <> "#@DEWQA45tec") Then
                        lsttab2.Items(i).Value = Trim(drgetcol("Headings").ToString & "$" & arrtab(i))
                        arrcol = Split(Trim(drgetcol("Headings").ToString), ",")
                        arrlength = arrcol.Length - 2
                    Else
                        lsttab2.Items(i).Value = Trim(drgetcol("Visiblecolumn").ToString & "$" & arrtab(i))
                        arrcol = Split(Trim(drgetcol("Visiblecolumn").ToString), ",")
                        arrlength = arrcol.Length - 1
                    End If
                    'above code  altered by atul to create view from view

                    For j = 0 To arrlength
                        arrcol(j) = arrtab(i) & "." & arrcol(j)
                        arrcol(j) = Trim(arrcol(j))
                        lsttab2cols.Items.Add(arrcol(j))
                        lstgroup.Items.Add(arrcol(j))
                        '''''''''''''''''''''''''''''''''''''''''''''''''''
                        cbocol11.Items.Add(arrcol(j))
                        cbocol12.Items.Add(arrcol(j))
                        cbocol13.Items.Add(arrcol(j))
                        cbocol14.Items.Add(arrcol(j))
                        cbocol15.Items.Add(arrcol(j))
                        cbocol16.Items.Add(arrcol(j))
                        cbocol17.Items.Add(arrcol(j))
                        cbocol18.Items.Add(arrcol(j))
                        cbocol19.Items.Add(arrcol(j))
                        cbocol20.Items.Add(arrcol(j))
                        ''''''''''''''''''''''''''''''''''''''''''''''''''''
                        cbocol21.Items.Add(arrcol(j))
                        cbocol22.Items.Add(arrcol(j))
                        cbocol23.Items.Add(arrcol(j))
                        cbocol24.Items.Add(arrcol(j))
                        cbocol25.Items.Add(arrcol(j))
                        cbocol26.Items.Add(arrcol(j))
                        cbocol27.Items.Add(arrcol(j))
                        cbocol28.Items.Add(arrcol(j))
                        cbocol29.Items.Add(arrcol(j))
                        cbocol30.Items.Add(arrcol(j))
                        ''''''''''''''''''''''''''''''''''''''''''''''''''''
                        cbocolA1.Items.Add(arrcol(j))
                        cbocolA2.Items.Add(arrcol(j))
                        cbocolA3.Items.Add(arrcol(j))
                        cbocolA4.Items.Add(arrcol(j))
                        cbocolA5.Items.Add(arrcol(j))
                        cbocolA6.Items.Add(arrcol(j))
                        cbocolA7.Items.Add(arrcol(j))
                        cbocolA8.Items.Add(arrcol(j))
                        cbocolA9.Items.Add(arrcol(j))
                        cbocolA10.Items.Add(arrcol(j))
                    Next
                End If
                drgetcol.Close()
                'connection.Close()
                cmdgetcol.Dispose()
            Next
            ''''''''''''''''''''''''''''''''''''''''''''''''''''
            cbocol11.Items.Insert(0, "--Select--")
            cbocol12.Items.Insert(0, "--Select--")
            cbocol13.Items.Insert(0, "--Select--")
            cbocol14.Items.Insert(0, "--Select--")
            cbocol15.Items.Insert(0, "--Select--")
            cbocol16.Items.Insert(0, "--Select--")
            cbocol17.Items.Insert(0, "--Select--")
            cbocol18.Items.Insert(0, "--Select--")
            cbocol19.Items.Insert(0, "--Select--")
            cbocol20.Items.Insert(0, "--Select--")
            ''''''''''''''''''''''''''''''''''''''''''''''''''''
            cbocol21.Items.Insert(0, "--Select--")
            cbocol22.Items.Insert(0, "--Select--")
            cbocol23.Items.Insert(0, "--Select--")
            cbocol24.Items.Insert(0, "--Select--")
            cbocol25.Items.Insert(0, "--Select--")
            cbocol26.Items.Insert(0, "--Select--")
            cbocol27.Items.Insert(0, "--Select--")
            cbocol28.Items.Insert(0, "--Select--")
            cbocol29.Items.Insert(0, "--Select--")
            cbocol30.Items.Insert(0, "--Select--")
            ''''''''''''''''''''''''''''''''''''''''''''''''''''
            cbocolA1.Items.Insert(0, "--Select--")
            cbocolA2.Items.Insert(0, "--Select--")
            cbocolA3.Items.Insert(0, "--Select--")
            cbocolA4.Items.Insert(0, "--Select--")
            cbocolA5.Items.Insert(0, "--Select--")
            cbocolA6.Items.Insert(0, "--Select--")
            cbocolA7.Items.Insert(0, "--Select--")
            cbocolA8.Items.Insert(0, "--Select--")
            cbocolA9.Items.Insert(0, "--Select--")
            cbocolA10.Items.Insert(0, "--Select--")

            If Trim(colname) <> "" Then
                For i = 0 To lsttab2cols.Items.Count - 1
                    'Dim arrcol1 = Split(lsttab2cols.Items(i).Value, ".")
                    If InStr("," & colname & ",", "," & lsttab2cols.Items(i).Value & ",") > 0 Then
                        lsttab2cols.Items(i).Selected = True
                    End If
                Next
            End If
            For i = 0 To lstgroup.Items.Count - 1
                If InStr("," & groupby1 & ",", "," & lstgroup.Items(i).Value & ",") > 0 Then
                    lstgroup.Items(i).Selected = True
                End If
            Next

            'If ViewType = "Local" Then
            '    chkLocalView.Checked = True

            'Else
            '    chkLocalView.Checked = False

            'End If


            If formula <> "" Then
                Dim arrformula = Split(formula, ",")
                For i = 0 To arrformula.Length - 1
                    arrformula(i) = Replace(arrformula(i), "~~", ",")
                    'lstformula.Items.Add(arrformula(i))
                    'lstgroup.Items.Add(arrformula(i))

                Next
            End If
            If Trim(wherejoin) <> "" Then
                Dim arrjoin = Split(wherejoin, ",")
                For i = 0 To arrjoin.Length - 1
                    Dim arrjoin1 = Split(arrjoin(i), "$##$")
                    Select Case i
                        Case 0
                            cbocol11.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin1.SelectedIndex = getindex(arrjoin1(1))
                            cbocol21.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 1
                            cbocol12.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin2.SelectedIndex = getindex(arrjoin1(1))
                            cbocol22.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 2
                            cbocol13.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin3.SelectedIndex = getindex(arrjoin1(1))
                            cbocol23.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 3
                            cbocol14.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin4.SelectedIndex = getindex(arrjoin1(1))
                            cbocol24.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 4
                            cbocol15.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin5.SelectedIndex = getindex(arrjoin1(1))
                            cbocol25.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 5
                            cbocol16.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin6.SelectedIndex = getindex(arrjoin1(1))
                            cbocol26.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 6
                            cbocol17.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin7.SelectedIndex = getindex(arrjoin1(1))
                            cbocol27.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 7
                            cbocol18.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin8.SelectedIndex = getindex(arrjoin1(1))
                            cbocol28.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 8
                            cbocol19.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin9.SelectedIndex = getindex(arrjoin1(1))
                            cbocol29.Items.FindByValue(arrjoin1(2)).Selected = True
                            Exit Select
                        Case 9
                            cbocol20.Items.FindByValue(Trim(arrjoin1(0))).Selected = True
                            cbojoin10.SelectedIndex = getindex(arrjoin1(1))
                            cbocol30.Items.FindByValue(arrjoin1(2)).Selected = True

                            Exit Select
                    End Select
                Next
            End If
            If Trim(wherecon) <> "" Then
                Dim arrcon = Split(wherecon, ",")
                For i = 0 To arrcon.Length - 1
                    Dim arrcon1
                    arrcon1 = Split(arrcon(i), "$##$")
                    Select Case i
                        Case 0
                            cbocolA1.SelectedValue = Trim(arrcon1(0))
                            cbofunc11.SelectedValue = Trim(arrcon1(1))
                            txtval11.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 1
                            cbocolA2.SelectedValue = Trim(arrcon1(0))
                            cbofunc12.SelectedValue = Trim(arrcon1(1))
                            txtval12.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 2
                            cbocolA3.SelectedValue = Trim(arrcon1(0))
                            cbofunc13.SelectedValue = Trim(arrcon1(1))
                            txtval13.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 3
                            cbocolA4.SelectedValue = Trim(arrcon1(0))
                            cbofunc14.SelectedValue = Trim(arrcon1(1))
                            txtval14.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 4
                            cbocolA5.SelectedValue = Trim(arrcon1(0))
                            cbofunc15.SelectedValue = Trim(arrcon1(1))
                            txtval15.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 5
                            cbocolA6.SelectedValue = Trim(arrcon1(0))
                            cbofunc16.SelectedValue = Trim(arrcon1(1))
                            txtval16.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 6
                            cbocolA7.SelectedValue = Trim(arrcon1(0))
                            cbofunc17.SelectedValue = Trim(arrcon1(1))
                            txtval17.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 7
                            cbocolA8.SelectedValue = Trim(arrcon1(0))
                            cbofunc18.SelectedValue = Trim(arrcon1(1))
                            txtval18.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 8
                            cbocolA9.SelectedValue = Trim(arrcon1(0))
                            cbofunc19.SelectedValue = Trim(arrcon1(1))
                            txtval19.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                        Case 9
                            cbocolA10.SelectedValue = Trim(arrcon1(0))
                            cbofunc20.SelectedValue = Trim(arrcon1(1))
                            txtval20.Value = Replace(Trim(arrcon1(2)), "$#$", "")
                            Exit Select
                    End Select
                Next
            End If
            Dim cnt As Integer = 0
            Dim cnt1 As Integer = 0
            Dim cnt2 As Integer = 1
            Dim str As String
            'Dim formulaarr = formula.Split(",")
            Dim columnarr = colname.Split(",")
            Dim arrhead = heading.Split(",")
            Dim arrform = formula.Split(",")
            str = "<table width=100%>"
            If Trim(colname) <> "" Then
                For i = 0 To columnarr.Length - 1
                    If (columnarr(i) <> "") Then
                        str = str + "<tr><td width=30%>" & cnt2 & ". " & columnarr(i) & ":</td><td><label for=txtcol" & cnt2 & " ></label><input type=text id=txtcol" & cnt2 & " name=txtcol" & cnt2 & " maxlength=50 value=" & arrhead(i) & "></td></tr>"
                        cnt1 = cnt1 + 1
                        cnt2 = cnt2 + 1
                    End If

                Next
            End If
            If formula <> "" Then
                For i = 0 To arrform.length - 1
                    If (arrform(i) <> "") Then
                        cnt = cnt + 1
                        str = str + "<tr><td>" & cnt2 & ". " & arrform(i) & ":</td><td><label for=txtcol" & cnt2 & " ></label><input type=text id=txtcol" & cnt2 & " name=txtcol" & cnt2 & " maxlength=50 value=" & arrhead(cnt2 - 1) & "></td></tr>"
                        lstformula.Items.Add(arrhead(cnt2 - 1))
                        'lstformula.Items.Insert(Convert.ToInt32(arrform(i)), arrhead(cnt2 - 1))

                        cnt2 = cnt2 + 1
                    End If

                Next
            End If
            'chkRights()
            str = str + "</table>"
            Me.divname.InnerHtml = str
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Showmsg(strmsg)
        End Try
        'End If
    End Sub
    Public Sub bindclient()
        If cbodept1.SelectedIndex <> 0 Then
            Dim cmdgetclient As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & cbodept1.SelectedValue & "' order by ClientName", connection)
            Dim dsgetclient As New DataSet
            Dim adpgetclient As New SqlDataAdapter
            adpgetclient.SelectCommand = cmdgetclient
            'connection.Open()
            adpgetclient.Fill(dsgetclient)
            'connection.Close()
            cmdgetclient.Dispose()
            cboclient1.DataSource = dsgetclient
            cboclient1.DataTextField = "ClientName"
            cboclient1.DataValueField = "autoid"
            cboclient1.DataBind()
            cboclient1.Items.Insert(0, "--Select--")
        End If
    End Sub
    'function added by atul to get >= in the dropdown,so at the bindtime & edit view properly using >= 
    Public Sub bindDdlWithGreaterEqualTo()
        cbojoin1.Items.Insert(4, ">=")
        cbojoin2.Items.Insert(4, ">=")
        cbojoin3.Items.Insert(4, ">=")
        cbojoin4.Items.Insert(4, ">=")
        cbojoin5.Items.Insert(4, ">=")
        cbojoin6.Items.Insert(4, ">=")
        cbojoin7.Items.Insert(4, ">=")
        cbojoin8.Items.Insert(4, ">=")
        cbojoin9.Items.Insert(4, ">=")
        cbojoin10.Items.Insert(4, ">=")

        cbofunc11.Items.Insert(4, ">=")
        cbofunc12.Items.Insert(4, ">=")
        cbofunc13.Items.Insert(4, ">=")
        cbofunc14.Items.Insert(4, ">=")
        cbofunc15.Items.Insert(4, ">=")
        cbofunc16.Items.Insert(4, ">=")
        cbofunc17.Items.Insert(4, ">=")
        cbofunc18.Items.Insert(4, ">=")
        cbofunc19.Items.Insert(4, ">=")
        cbofunc20.Items.Insert(4, ">=")

    End Sub
    Public Sub bindlob()
        If cboclient1.SelectedIndex <> 0 And cboclient1.SelectedIndex <> -1 Then
            Dim cmdgetlob As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & cbodept1.SelectedValue & "' and ClientId='" & cboclient1.SelectedValue & "' order by LOB", connection)
            Dim dsgetlob As New DataSet
            Dim adpgetlob As New SqlDataAdapter
            adpgetlob.SelectCommand = cmdgetlob
            'connection.Open()
            adpgetlob.Fill(dsgetlob)
            'connection.Close()
            cmdgetlob.Dispose()
            cbolob1.DataSource = dsgetlob
            cbolob1.DataTextField = "LOB"
            cbolob1.DataValueField = "autoid"
            cbolob1.DataBind()
            cbolob1.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub

    Public Function getindex(ByVal expression As String)
        Dim index As Integer = -1
        If expression = "--Select--" Then
            index = 0
        End If
        If expression = "=" Then
            index = 1
        End If
        If expression = "*=" Then
            index = 2
        End If
        If expression = "=*" Then
            index = 3
        End If
        If expression = ">=" Then
            index = 4
        End If
        If expression = "<=" Then
            index = 5
        End If
        Return index
    End Function


    '********************change to check Rights********************************
    Public Sub chkRights()
        If (Session("typeofuser") = "Admin") Or (Session("typeofuser") = "Super Admin") Then
            'cmdsaveas.Visible = True
            lblview.Visible = True
            txtname.Visible = True
            'cmdcreatetab.Visible = True
        Else

            Dim SaveRights As String
            Dim EditRights As String
            Dim btnrights As New SqlCommand("Select Edit,SaveAs  from IDMS_ViewRights where userid='" & Session("userid") & "' and ViewID='" & Request("recid") & "' ", connection)
            connection.Open()
            Dim btnreader As SqlDataReader = btnrights.ExecuteReader
            If btnreader.Read Then
                SaveRights = (btnreader("SaveAs")).ToString
                If (SaveRights = "True") Or (SaveRights = "true") Then
                    cmdsaveas.Visible = True
                    lblview.Visible = True
                    txtname.Visible = True
                Else
                    cmdsaveas.Visible = False
                    lblview.Visible = False
                    txtname.Visible = False
                End If
                EditRights = (btnreader("Edit")).ToString
                If (EditRights = "True") Or (EditRights = "true") Then
                    cmdcreatetab.Visible = True
                Else

                    cmdcreatetab.Visible = False
                End If

            Else

                cmdsaveas.Visible = False
                lblview.Visible = False
                txtname.Visible = False
                cmdcreatetab.Visible = False
                'End If

            End If
            connection.Close()
            btnreader.Close()
        End If


    End Sub
    '********************change to check Rights********************************

   
End Class
