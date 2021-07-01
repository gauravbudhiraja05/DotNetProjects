Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_ReplaceTableData
    Inherits System.Web.UI.Page
    Dim cmd As SqlCommand
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)

    Dim rdr As SqlDataReader
    Dim rowcount As Integer
    Dim msg = ""
    Dim i = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        i = i + 1
        Ajax.Utility.RegisterTypeForAjax(GetType(DataTransfer))
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spndisplay1.Visible = True
                Me.spndisplay2.Visible = True
                Me.savespanmul.Visible = True
                Me.Cmdupmul.Visible = True
            Else
                Me.Gettable.Visible = True
                Me.Cmdup.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
        'Session("userid") = "22499"
        'Session("typeofuser") = "Admin"

        Dim userid As String
        userid = Session("userid")
        hfUserType.Value = Session("typeofuser")
        hfUserId.Value = Session("userid")
        'lblmsg.Text = Session("nmsg")
        If i <> 0 And Session("nmsg") <> "" Then
            ShowConfirm(Session("nmsg"))
        End If
        If i = 1 Then

            Session("nmsg") = ""
            i = 0

        End If


        '''''entityname = cbotab1.SelectedItem.Text

        'Dim str = ">="
        If Me.IsPostBack = False Then
            'Dim userid As Integer
            'userid = Session("userid")
            Session("nmsg") = ""
            Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
            Me.cbodept1.Attributes.Add("onchange", "javascript:getclient1();")
            Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
            Me.cboclient1.Attributes.Add("onchange", "javascript:getlob1();")
            Me.cboclient2.Attributes.Add("onchange", "javascript:getlob2();")
            Me.cbodept2.Attributes.Add("onchange", "javascript:getclient2();")
            Me.cbolob.Attributes.Add("onchange", "javascript:gettab2();")
            Me.cbolob1.Attributes.Add("onchange", "javascript:gettab12();")
            Me.cbotab1.Attributes.Add("onchange", "javascript:gettab1cols();")
            Me.cbotab2.Attributes.Add("onchange", "javascript:gettab2cols();")
            panConfirm.Visible = False
           
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
            End If
            ' IsUpdate.Value = "False"
        End If
        If (IsUpdate.Value = "True") Then
            panConfirm.Visible = True
        End If


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
    End Sub
    Public Sub Showmsg(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Me.GetType, "Showmsg", Script)
    End Sub


    Public Sub Clear()
        cbotab1.SelectedValue = 0
        cbotab2.SelectedValue = 0
        cbodept.SelectedIndex = 0
        cbodept1.SelectedIndex = 0
        cbofunc11.SelectedIndex = 0
        cbofunc12.SelectedIndex = 0
        cbofunc13.SelectedIndex = 0
        cbofunc14.SelectedIndex = 0
        cbofunc15.SelectedIndex = 0
        txtval11.Value = ""
        txtval12.Value = ""
        txtval13.Value = ""
        txtval14.Value = ""
        txtval15.Value = ""
        cbofunc21.SelectedIndex = 0
        cbofunc22.SelectedIndex = 0
        cbofunc23.SelectedIndex = 0
        cbofunc24.SelectedIndex = 0
        cbofunc25.SelectedIndex = 0
        txtval21.Value = ""
        txtval22.Value = ""
        txtval22.Value = ""
        txtval24.Value = ""
        txtval25.Value = ""
        txtname.Text = ""
        cbodept2.SelectedIndex = 0
        cboclient2.Items.Clear()
        cbolob2.Items.Clear()
    End Sub
    Protected Sub Cmdupmul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmdupmul.Click
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
        Dim arrConTab1QueryString() As String
        Dim arrconTab2QueryString() As String
        Dim query1 As New System.Text.StringBuilder

        SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
        SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob


        arrQueryString = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
        arrQueryString1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        'arrQueryString2 = Split(hidQueryString2.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
        arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
        arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrConTab1QueryString = Split(hfconTab1QueryString.Value.ToString(), "#") 'Array for finding columns of first table for condition
        arrconTab2QueryString = Split(hfconTab2QueryString.Value.ToString(), "#") 'Array for finding columns of second table for condition

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

        If cbocolA1 <> "" And cbocolB1 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
            End If
        End If
        If cbocolA2 <> "" And cbocolB2 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
            End If
        End If
        If cbocolA3 <> "" And cbocolB3 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
            End If
        End If
        If cbocolA4 <> "" And cbocolB4 <> "--Select--" Or cbocolB4 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
            End If
        End If
        If cbocolA5 <> "" And cbocolB5 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
            End If
        End If
        qry = qry & " from " & arrtab1(1) & "," & arrtab2(1)

        ''''''''Join Part
        Dim wherejoin As String = ""
        If cbocol11 <> "--Select--" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "--Select--" Then
            qry = qry & " where " & arrtab1(1) & "." & cbocol11 & " " & cbojoin1.SelectedValue & "  " & arrtab2(1) & "." & cbocol21
            wherejoin = wherejoin & arrtab1(1) & "." & cbocol11 & "$##$" & cbojoin1.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol21
        End If
        If cbocol12 <> "--Select--" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol12 & " " & cbojoin2.SelectedValue & "  " & arrtab2(1) & "." & cbocol22
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol12 & "$##$" & cbojoin2.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol22
        End If
        If cbocol13 <> "--Select--" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol13 & " " & cbojoin3.SelectedValue & "  " & arrtab2(1) & "." & cbocol23
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol13 & "$##$" & cbojoin3.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol23
        End If
        If cbocol14 <> "--Select--" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol14 & " " & cbojoin4.SelectedValue & "  " & arrtab2(1) & "." & cbocol24
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol14 & "$##$" & cbojoin4.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol24
        End If
        If cbocol15 <> "--Select--" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol15 & " " & cbojoin5.SelectedValue & "  " & arrtab2(1) & "." & cbocol25
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol15 & "$##$" & cbojoin5.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol25
        End If
        If cbocol16 <> "--Select--" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol16 & " " & cbojoin6.SelectedValue & "  " & arrtab2(1) & "." & cbocol26
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol16 & "$##$" & cbojoin6.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol26
        End If
        If cbocol17 <> "--Select--" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol17 & " " & cbojoin7.SelectedValue & "  " & arrtab2(1) & "." & cbocol27
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol17 & "$##$" & cbojoin7.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol27
        End If
        If cbocol18 <> "--Select--" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol18 & " " & cbojoin8.SelectedValue & "  " & arrtab2(1) & "." & cbocol28
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol18 & "$##$" & cbojoin8.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol28
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''CondtionPart
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim wherecon As String = ""
        If cbocolA11 <> "--Select--" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA11 & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
            wherecon = arrtab1(1) & "." & cbocolA11 & "$##$" & cbofunc11.SelectedValue & "$##$ " & txtval11.Value & " "
        End If
        If cbocolA12 <> "--Select--" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA12 & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & ""
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA12 & "$##$" & cbofunc12.SelectedValue & "$##$" & txtval12.Value & ""
        End If
        If cbocolA13 <> "--Select--" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA13 & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA13 & "$##$" & cbofunc13.SelectedValue & "$##$" & txtval13.Value & ""
        End If
        If cbocolA14 <> "--Select--" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA14 & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA14 & "$##$" & cbofunc14.SelectedValue & "$##$" & txtval14.Value & ""
        End If
        If cbocolA15 <> "--Select--" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA15 & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA15 & "$##$" & cbofunc15.SelectedValue & "$##$" & txtval15.Value & "'"
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        If cbocolB21 <> "--Select--" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB21 & " " & cbofunc21.SelectedValue & " '" & txtval21.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB21 & "$##$" & cbofunc21.SelectedValue & "$##$" & txtval21.Value & ""
        End If
        If cbocolB22 <> "--Select--" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB22 & " " & cbofunc22.SelectedValue & " '" & txtval22.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB22 & "$##$" & cbofunc22.SelectedValue & "$##$" & txtval22.Value & ""
        End If
        If cbocolB23 <> "--Select--" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB23 & " " & cbofunc23.SelectedValue & " '" & txtval23.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB23 & "$##$" & cbofunc23.SelectedValue & "$##$" & txtval23.Value & ""
        End If
        If cbocolB24 <> "--Select--" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB24 & " " & cbofunc24.SelectedValue & " '" & txtval24.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB24 & "$##$" & cbofunc24.SelectedValue & "$##$" & txtval24.Value & ""
        End If
        If cbocolB25 <> "--Select--" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
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
        If cbocolA1 <> "--Select--" And cbocolA1 <> "" Then
            colsto = cbocolA1
        End If
        If cbocolA2 <> "--Select--" And cbocolA2 <> "" Then
            colsto = colsto & "," & cbocolA2
        End If
        If cbocolA3 <> "--Select--" And cbocolA3 <> "" Then
            colsto = colsto & "," & cbocolA3
        End If
        If cbocolA4 <> "--Select--" And cbocolA4 <> "" Then
            colsto = colsto & "," & cbocolA4
        End If
        If cbocolA5 <> "--Select--" And cbocolA5 <> "" Then
            colsto = colsto & "," & cbocolA5
        End If
        Dim colsfrom As String
        If cbocolB1 <> "--Select--" And cbocolB1 <> "" Then
            colsfrom = cbocolB1
        End If
        If cbocolB2 <> "--Select--" And cbocolB2 <> "" Then
            colsfrom = colsfrom & "," & cbocolB2
        End If
        If cbocolB3 <> "--Select--" And cbocolB3 <> "" Then
            colsfrom = colsfrom & "," & cbocolB3
        End If
        If cbocolB4 <> "--Select--" And cbocolB4 <> "" Then
            colsfrom = colsfrom & "," & cbocolB4
        End If
        If cbocolB5 <> "--Select--" And cbocolB5 <> "" Then
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
            'connection.Open()
            rowcount = cmd.ExecuteNonQuery()
            'connection.Close()
            cmd.Dispose()
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
            'connection.Open()
            cmdins2.ExecuteNonQuery()
            'connection.Close()
            cmdins2.Dispose()
            '*************change*************
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Run'", connection)
            'connection.Open()
            cmm.ExecuteNonQuery()
            'connection.Close()

            Dim sqlstr1 As String = ""
            sqlstr1 = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + rowcount.ToString() + "' from logDataTransferMaster where EntityName='" & txtname.Text & "'  and Action='Run'")
            Dim cmdins21 = New SqlCommand(sqlstr1, connection)
            'connection.Open()
            cmdins21.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmdins As New SqlCommand("insert into IdmsUpdateTabStruct(CmdName,LocalCmd,DeptIDTo,ClientIDTo,LOBIDTo,TableTo,DeptIDFrom,ClientIDFrom,LOBIDFrom,TableFrom,ColumnsTo,ColumnsFrom,WhereDataJoin,WhereDataCon,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,DeptID,ClientID,LOBID)  Values ('" & txtname.Text & "','Local','" & cbodept.SelectedValue & "','" & client & "','" & lob & "','" & arrtab1(1) & "','" & cbodept1.SelectedValue & "','" & client1 & "','" & lob1 & "','" & arrtab2(1) & "','" & colsto & "','" & colsfrom & "','" & wherejoin & "','" & wherecon & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "''" & cbodept2.SelectedValue & "','" & client2 & "','" & lob2 & "')", connection)
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
                '.AddWithValue("@ClientID", )
                .AddWithValue("@LOBID", lob2)

            End With
            'connection.Open()
            cmdins.ExecuteNonQuery()


            IsUpdate.Value = "True"
            'panConfirm.Visible = True
            Dim msg As String = "Table has been updated successfully with no. of record(s) = " + rowcount.ToString()
            i = 0
            Me.lblmsg.Text = msg
            Session("nmsg") = msg
            'connection.Close()
            cmdins.Dispose()
            Dim sqlstr As String = ""
            sqlstr = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + rowcount.ToString() + "' from logDataTransferMaster where EntityName='" & txtname.Text & "'  and Action='Save As'")
            cmdins2 = New SqlCommand(sqlstr, connection)
            'connection.Open()
            cmdins2.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            Dim cmm1 As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Save As'", connection)
            'connection.Open()
            cmm1.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            ShowConfirm(msg)
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Session("nmsg") = ex.Message.ToString()
            ShowConfirm(strmsg.ToString())
            'i = 0
        End Try

        'Clear()
    End Sub

    Protected Sub Cmdup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmdup.Click

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
        Dim arrConTab1QueryString() As String
        Dim arrconTab2QueryString() As String
        Dim query1 As New System.Text.StringBuilder

        SpanA = Split(hdSpanA.Value, "#") 'Array for finding First Table Department,Client & Lob
        SpanB = Split(hdSpanB.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        Span = Split(hdSpan.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob


        arrQueryString = Split(hidQueryString.Value.ToString(), "#") 'Array for finding First Table Department,Client & Lob
        arrQueryString1 = Split(hidQueryString1.Value.ToString(), "#") 'Array for finding Second Table Department,Client & Lob
        'arrQueryString2 = Split(hidQueryString2.Value.ToString(), "#") 'Array for finding Third Table Department,Client & Lob
        arrFirstTabColQueryString = Split(hfFirstTabQueryString.Value.ToString(), "#") 'Array for finding Selected First Table Columns
        arrSecondTabColQueryString = Split(hfSecondTabQueryString.Value.ToString(), "#") 'Array for finding Selected Second Table Columns
        arrJoinTab1QueryString = Split(hfJoinTab1QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrJoinTab2QueryString = Split(hfJoinTab2QueryString.Value.ToString(), "#") 'Array for finding  colums of first table for join
        arrConTab1QueryString = Split(hfconTab1QueryString.Value.ToString(), "#") 'Array for finding columns of first table for condition
        arrconTab2QueryString = Split(hfconTab2QueryString.Value.ToString(), "#") 'Array for finding columns of second table for condition

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

        If cbocolA1 <> "" And cbocolB1 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA1 & " = " & arrtab2(1) & "." & cbocolB1
            End If
        End If
        If cbocolA2 <> "" And cbocolB2 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA2 & " = " & arrtab2(1) & "." & cbocolB2
            End If
        End If
        If cbocolA3 <> "" And cbocolB3 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA3 & " = " & arrtab2(1) & "." & cbocolB3
            End If
        End If
        If cbocolA4 <> "" And cbocolB4 <> "--Select--" Or cbocolB4 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA4 & " = " & arrtab2(1) & "." & cbocolB4
            End If
        End If
        If cbocolA5 <> "" And cbocolB5 <> "" Then
            If qry = "" Then
                qry = " " & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
            Else
                qry = qry & " ," & arrtab1(1) & "." & cbocolA5 & " = " & arrtab2(1) & "." & cbocolB5
            End If
        End If
        qry = qry & " from " & arrtab1(1) & "," & arrtab2(1)

        ''''''''Join Part
        Dim wherejoin As String = ""
        If cbocol11 <> "--Select--" And cbojoin1.SelectedIndex <> 0 And cbocol21 <> "--Select--" Then
            qry = qry & " where " & arrtab1(1) & "." & cbocol11 & " " & cbojoin1.SelectedValue & "  " & arrtab2(1) & "." & cbocol21
            wherejoin = wherejoin & arrtab1(1) & "." & cbocol11 & "$##$" & cbojoin1.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol21
        End If
        If cbocol12 <> "--Select--" And cbojoin2.SelectedIndex <> 0 And cbocol22 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol12 & " " & cbojoin2.SelectedValue & "  " & arrtab2(1) & "." & cbocol22
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol12 & "$##$" & cbojoin2.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol22
        End If
        If cbocol13 <> "--Select--" And cbojoin3.SelectedIndex <> 0 And cbocol23 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol13 & " " & cbojoin3.SelectedValue & "  " & arrtab2(1) & "." & cbocol23
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol13 & "$##$" & cbojoin3.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol23
        End If
        If cbocol14 <> "--Select--" And cbojoin4.SelectedIndex <> 0 And cbocol24 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol14 & " " & cbojoin4.SelectedValue & "  " & arrtab2(1) & "." & cbocol24
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol14 & "$##$" & cbojoin4.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol24
        End If
        If cbocol15 <> "--Select--" And cbojoin5.SelectedIndex <> 0 And cbocol25 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol15 & " " & cbojoin5.SelectedValue & "  " & arrtab2(1) & "." & cbocol25
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol15 & "$##$" & cbojoin5.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol25
        End If
        If cbocol16 <> "--Select--" And cbojoin6.SelectedIndex <> 0 And cbocol26 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol16 & " " & cbojoin6.SelectedValue & "  " & arrtab2(1) & "." & cbocol26
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol16 & "$##$" & cbojoin6.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol26
        End If
        If cbocol17 <> "--Select--" And cbojoin7.SelectedIndex <> 0 And cbocol27 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol17 & " " & cbojoin7.SelectedValue & "  " & arrtab2(1) & "." & cbocol27
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol17 & "$##$" & cbojoin7.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol27
        End If
        If cbocol18 <> "--Select--" And cbojoin8.SelectedIndex <> 0 And cbocol28 <> "--Select--" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocol18 & " " & cbojoin8.SelectedValue & "  " & arrtab2(1) & "." & cbocol28
            wherejoin = wherejoin & "," & arrtab1(1) & "." & cbocol18 & "$##$" & cbojoin8.SelectedValue & "$##$" & arrtab2(1) & "." & cbocol28
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''CondtionPart
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim wherecon As String = ""
        If cbocolA11 <> "--Select--" And cbofunc11.SelectedIndex <> 0 And txtval11.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA11 & " " & cbofunc11.SelectedValue & " '" & txtval11.Value & "'"
            wherecon = arrtab1(1) & "." & cbocolA11 & "$##$" & cbofunc11.SelectedValue & "$##$ " & txtval11.Value & " "
        End If
        If cbocolA12 <> "--Select--" And cbofunc12.SelectedIndex <> 0 And txtval12.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA12 & " " & cbofunc12.SelectedValue & " '" & txtval12.Value & ""
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA12 & "$##$" & cbofunc12.SelectedValue & "$##$" & txtval12.Value & ""
        End If
        If cbocolA13 <> "--Select--" And cbofunc13.SelectedIndex <> 0 And txtval13.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA13 & " " & cbofunc13.SelectedValue & " '" & txtval13.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA13 & "$##$" & cbofunc13.SelectedValue & "$##$" & txtval13.Value & ""
        End If
        If cbocolA14 <> "--Select--" And cbofunc14.SelectedIndex <> 0 And txtval14.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA14 & " " & cbofunc14.SelectedValue & " '" & txtval14.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA14 & "$##$" & cbofunc14.SelectedValue & "$##$" & txtval14.Value & ""
        End If
        If cbocolA15 <> "--Select--" And cbofunc15.SelectedIndex <> 0 And txtval15.Value <> "" Then
            qry = qry & " and " & arrtab1(1) & "." & cbocolA15 & " " & cbofunc15.SelectedValue & " '" & txtval15.Value & "'"
            wherecon = wherecon & "," & arrtab1(1) & "." & cbocolA15 & "$##$" & cbofunc15.SelectedValue & "$##$" & txtval15.Value & "'"
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        If cbocolB21 <> "--Select--" And cbofunc21.SelectedIndex <> 0 And txtval21.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB21 & " " & cbofunc21.SelectedValue & " '" & txtval21.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB21 & "$##$" & cbofunc21.SelectedValue & "$##$" & txtval21.Value & ""
        End If
        If cbocolB22 <> "--Select--" And cbofunc22.SelectedIndex <> 0 And txtval22.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB22 & " " & cbofunc22.SelectedValue & " '" & txtval22.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB22 & "$##$" & cbofunc22.SelectedValue & "$##$" & txtval22.Value & ""
        End If
        If cbocolB23 <> "--Select--" And cbofunc23.SelectedIndex <> 0 And txtval23.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB23 & " " & cbofunc23.SelectedValue & " '" & txtval23.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB23 & "$##$" & cbofunc23.SelectedValue & "$##$" & txtval23.Value & ""
        End If
        If cbocolB24 <> "--Select--" And cbofunc24.SelectedIndex <> 0 And txtval24.Value <> "" Then
            qry = qry & " and " & arrtab2(1) & "." & cbocolB24 & " " & cbofunc24.SelectedValue & " '" & txtval24.Value & "'"
            wherecon = wherecon & "," & arrtab2(1) & "." & cbocolB24 & "$##$" & cbofunc24.SelectedValue & "$##$" & txtval24.Value & ""
        End If
        If cbocolB25 <> "--Select--" And cbofunc25.SelectedIndex <> 0 And txtval25.Value <> "" Then
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
        If cbocolA1 <> "--Select--" And cbocolA1 <> "" Then
            colsto = cbocolA1
        End If
        If cbocolA2 <> "--Select--" And cbocolA2 <> "" Then
            colsto = colsto & "," & cbocolA2
        End If
        If cbocolA3 <> "--Select--" And cbocolA3 <> "" Then
            colsto = colsto & "," & cbocolA3
        End If
        If cbocolA4 <> "--Select--" And cbocolA4 <> "" Then
            colsto = colsto & "," & cbocolA4
        End If
        If cbocolA5 <> "--Select--" And cbocolA5 <> "" Then
            colsto = colsto & "," & cbocolA5
        End If
        Dim colsfrom As String
        If cbocolB1 <> "--Select--" And cbocolB1 <> "" Then
            colsfrom = cbocolB1
        End If
        If cbocolB2 <> "--Select--" And cbocolB2 <> "" Then
            colsfrom = colsfrom & "," & cbocolB2
        End If
        If cbocolB3 <> "--Select--" And cbocolB3 <> "" Then
            colsfrom = colsfrom & "," & cbocolB3
        End If
        If cbocolB4 <> "--Select--" And cbocolB4 <> "" Then
            colsfrom = colsfrom & "," & cbocolB4
        End If
        If cbocolB5 <> "--Select--" And cbocolB5 <> "" Then
            colsfrom = colsfrom & "," & cbocolB5
        End If
        Dim local As String
        'If chklocal.Checked Then
        '    local = "Local"
        'Else
        '    local = "NonLocal"
        'End If

        Try

            Dim cmd As New SqlCommand(query, connection)
            connection.Open()
            rowcount = cmd.ExecuteNonQuery()
            connection.Close()
            cmd.Dispose()
            '****************************Change*********************************

            Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters


                .AddWithValue("@actionBY", Session("userid"))
                .AddWithValue("@Action", "Run")
                .AddWithValue("@Date", System.DateTime.Now)
                .AddWithValue("@Entity", "Update Command")
                .AddWithValue("@EntityName", txtname.Text)
                .AddWithValue("@DeptId", 60)
                .AddWithValue("@ClientId", client2)
                .AddWithValue("@LOBId", lob2)

            End With
            connection.Open()
            cmdins2.ExecuteNonQuery()
            connection.Close()
            cmdins2.Dispose()
            '*************change*************
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            'Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Run'", connection)
            'connection.Open()
            'cmm.ExecuteNonQuery()
            'connection.Close()

            'Dim sqlstr1 As String = ""
            'sqlstr1 = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + rowcount.ToString() + "' from logDataTransferMaster where EntityName='" & txtname.Text & "'  and Action='Run'")
            'Dim cmdins21 = New SqlCommand(sqlstr1, connection)
            'connection.Open()
            'cmdins21.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmdins As New SqlCommand("insert into IdmsUpdateTabStruct(CmdName,LocalCmd,DeptIDTo,ClientIDTo,LOBIDTo,TableTo,DeptIDFrom,ClientIDFrom,LOBIDFrom,TableFrom,ColumnsTo,ColumnsFrom,WhereDataJoin,WhereDataCon,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,DeptID,ClientID,LOBID)  Values ('" & txtname.Text & "','Local','" & cbodept.SelectedValue & "','" & client & "','" & lob & "','" & arrtab1(1) & "','" & cbodept1.SelectedValue & "','" & client1 & "','" & lob1 & "','" & arrtab2(1) & "','" & colsto & "','" & colsfrom & "','" & wherejoin & "','" & wherecon & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "''" & cbodept2.SelectedValue & "','" & client2 & "','" & lob2 & "')", connection)
            Dim cmdins As New SqlCommand("idms_UpdateTabStruct ", connection)
            cmdins.CommandType = CommandType.StoredProcedure
            With cmdins.Parameters
                .AddWithValue("@CmdName", txtname.Text)
                .AddWithValue("@LocalCmd", "NonLocal")
                .AddWithValue("@DeptIDTo", 60)
                .AddWithValue("@ClientIDTo", 0)
                .AddWithValue("@LOBIDTo", 0)
                .AddWithValue("@TableTo", arrtab1(1))
                .AddWithValue("@DeptIDFrom", 60)
                .AddWithValue("@ClientIDFrom", 0)
                .AddWithValue("@LOBIDFrom", 0)
                .AddWithValue("@TableFrom", arrtab2(1))
                .AddWithValue("@ColumnsTo", colsto)
                .AddWithValue("@ColumnsFrom", colsfrom)
                .AddWithValue("@WhereDataJoin", wherejoin)
                .AddWithValue("@WhereDataCon", wherecon)
                .AddWithValue("@CreatedOn", System.DateTime.Now)
                .AddWithValue("@CreatedBy", Session("userid"))
                .AddWithValue("@ModifiedOn", System.DateTime.Now)
                .AddWithValue("@ModifiedBy", Session("userid"))
                .AddWithValue("@DeptID", 60)

                .AddWithValue("@ClientID", 0)
                '.AddWithValue("@ClientID", )
                .AddWithValue("@LOBID", 0)

            End With
            connection.Open()
            cmdins.ExecuteNonQuery()


            IsUpdate.Value = "True"
            'panConfirm.Visible = True
            Dim msg As String = "Table has been updated successfully "
            i = 0
            Me.lblmsg.Text = msg
            Session("nmsg") = msg
            connection.Close()
            cmdins.Dispose()
            Dim sqlstr As String = ""
            sqlstr = ("insert into logDatatransferslave select MAX(Autoid),'No.of records effected','" + rowcount.ToString() + "' from logDataTransferMaster where EntityName='" & txtname.Text & "'  and Action='Save As'")
            cmdins2 = New SqlCommand(sqlstr, connection)
            connection.Open()
            cmdins2.ExecuteNonQuery()
            connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            'Dim cmm1 As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Save As'", connection)
            'connection.Open()
            'cmm1.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            ShowConfirm(msg)
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Session("nmsg") = ex.Message.ToString()
            ShowConfirm(strmsg.ToString())
            'i = 0
        End Try

        'Clear()
    End Sub

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1
    End Function


    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        panConfirm.Visible = False
        IsUpdate.Value = "False"
    End Sub
End Class
