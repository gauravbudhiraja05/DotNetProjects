Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.Web.UI.HtmlTextWriter
Imports System.Type
Partial Class DataManager_DataPurging
    Inherits System.Web.UI.Page
    Dim con As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(con)
    Dim classobj As New Functions
    Dim tablevalue As String
    Dim Formula As String
    Dim grd As New GridView
    Dim fileName As String
    Dim tablevalue1 As String
    Dim Formula1 As String
    Dim ds As New DataSet
    Dim classobj1 As New DataManagerFunction
    Dim strQuery As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Ajax.Utility.RegisterTypeForAjax(GetType(DataManagerAjax))
        Me.ddlDept.Attributes.Add("onchange", "getclient();")
        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                spandisplay.Visible = True
                get_table.Visible = False
                btnProcess.Visible = True
                btnProcess_singleuser.Visible = False
                rdr.Close()
                Dim cmd1 As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd1)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    lblDept.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    lblClient.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    lblLob.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If
            Else
                spandisplay.Visible = False
                get_table.Visible = True
                btnProcess.Visible = False
                btnProcess_singleuser.Visible = True
            End If
        End If





        Me.ddlClient.Attributes.Add("onchange", "GetLOB();")
        Me.ddlLob.Attributes.Add("onchange", "bindlisttab();")
        Me.ddlTable.Attributes.Add("onchange", "bindtabcol();")
        'If Me.IsPostBack = False Then
        '    Me.ddlDept.DataTextField = "DepartmentName"
        '    ddlDept.DataValueField = "AutoId"
        '    ddlDept.DataSource = classobj.bind_Department()
        '    ddlDept.DataBind()
        '    ddlDept.Items.Insert(0, "--Select--")
        '    'Me.ddlClient.Items.Insert(0, "--Select--")
        '    ' Me.ddlLob.Items.Insert(0, "--Select--")
        '    'Me.ddlTable.Items.Insert(0, "--Select--")
        'End If
        connection.Close()
        Dim typeofuser = Session("typeofuser")
        If (Page.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                ddlDept.DataSource = cmd.ExecuteReader()
                ddlDept.DataTextField = "DepartmentName"
                ddlDept.DataValueField = "AutoID"
                ddlDept.Items.Insert(0, "-- Select--")
                ddlDept.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                connection.Open()
                ddlDept.DataSource = cmd.ExecuteReader()
                ddlDept.DataTextField = "DepartmentName"
                ddlDept.DataValueField = "AutoID"
                ddlDept.Items.Insert(0, "--Select--")
                ddlDept.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                connection.Open()
                ddlDept.DataSource = cmd.ExecuteReader()
                ddlDept.DataTextField = "DepartmentName"
                ddlDept.DataValueField = "AutoID"
                ddlDept.Items.Insert(0, "--Select--")
                ddlDept.DataBind()
            End If
            ddlDept.Items.Insert(0, "--select--")
        End If




    End Sub

    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "Showmsg", str.ToString)
    End Sub

    Protected Sub btnpurgey_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpurgey.ServerClick
        Dim recordsEffected As Integer
        recordsEffected = Delrec()
        If recordsEffected = 999999999 Then
            Showmsg("Conditions Has Been Deleted!!!")
            Exit Sub
        End If
        'code to Track
        Dim autoId As Int64
        Dim ClientId
        Dim LobId


        If hidClientId.Value = "" Then
            ClientId = "0"
        Else
            ClientId = hidClientId.Value
        End If
        If hidLobId.Value = "" Then
            LobId = "0"
        Else
            LobId = hidLobId.Value
        End If


        Dim objDmFunction As New DataManagerFunction()
        autoId = objDmFunction.trackDmMaster(Session("userid"), "Purge (No Backup)", "Table", hidTablename.Value, ddlDept.SelectedValue, ClientId, LobId)

        connection.Open()
        Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Purge (No Backup)' and EntityName='" + hidTablename.Value + "'", connection)
        cmm.ExecuteNonQuery()
        connection.Close()

        'code for slave table
        objDmFunction.trackDmSlave(autoId, "No. of records effected", recordsEffected)
        objDmFunction.trackDmSlave(autoId, "Query", strQuery)

        'End Track

        Showmsg("Data Purge Successfully!!!")
        Me.txtFormula.Value = ""
        Me.ddlDept.SelectedIndex = 0
    End Sub
    Private Function Delrec() As Integer
        tablevalue1 = Me.hidTablename.Value
        Formula1 = Me.txtFormula.Value
        If Formula1 = "" Then
            Showmsg("Conditions Has Been Deleted!!!")
            Return 999999999
        End If
        Return classobj1.rowdel(tablevalue1, Formula1, strQuery)
    End Function
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Protected Sub ddlDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDept.SelectedIndexChanged
        If (ddlDept.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select DepartmentName")
        End If
        connection.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + ddlDept.SelectedValue + "'", connection)
        dr = cmd.ExecuteReader()
        ddlClient.DataSource = dr
        ddlClient.DataTextField = "ClientName"
        ddlClient.DataValueField = "autoid"
        ddlClient.DataBind()
        ddlClient.Items.Insert(0, "--select--")
    End Sub
End Class
