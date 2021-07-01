Imports System
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DCLUserControl
    Inherits System.Web.UI.UserControl
    Dim fun As New Functions
    Dim DeptId As Integer
    Dim Client_Id As Integer
    Dim loggedId As String
    Dim UserType As String
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As SqlCommand
        connection.Open()
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
            lblDepartment.Text = val1
            lblClient.Text = val2
            lblLob.Text = val3
        End If
        connection.Close()
    End Sub


    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        'ddlClient is being populated on selectedIndexChanged of ddlDepartment
        If ddlDepartment.SelectedIndex <> 0 Then

            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            UserType = Session("typeofuser").ToString()
            loggedId = Session("userid").ToString()

            ddlClient.DataTextField = "ClientName"
            ddlClient.DataValueField = "autoid"
            ddlClient.DataSource = fun.bind_client(DeptId)
            ddlClient.DataBind()
            ddlClient.Items.Insert(0, "--Select--")

        End If
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        'ddlLob is being populated on selectedIndexChanged of ddlClient
        If ddlClient.SelectedIndex <> 0 Then

            ddlLob.Items.Clear()
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            UserType = Session("typeofuser").ToString()
            loggedId = Session("userid").ToString()

            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "autoid"
            ddlLob.DataSource = fun.bind_lob(DeptId, Client_Id)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "--Select--")

        End If
    End Sub
End Class
