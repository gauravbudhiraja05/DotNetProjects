Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ShowMenuRights
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim str As String = AppSettings("ConnectionString")
    Dim ddlDepartment As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlLob As DropDownList
    Dim fun As New Functions
    Dim DeptId As String
    Dim Client_Id As String
    Dim LobId As String
    Dim objMenu As New MenuRight
    Dim txtLink As String
    Dim loggedId As String
    Dim UserType As String
    Dim uspan As New UserSpan
    Dim txtlinkUserType As String

    Dim conn As New SqlConnection(str)
    ''' <summary>
    ''' fill the span using user control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.SmartNavigation = True

        If Session("userid") = "" Then
            Response.Redirect("~/SessionExpired.aspx")
        Else
            loggedId = Session("userid").ToString()
            UserType = Session("typeofuser").ToString()

        End If

        ddlDepartment = CType(DCLHUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLHUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLHUserControl1.FindControl("ddlLob"), DropDownList)
        Session("dp") = ddlDepartment.SelectedValue
        Session("cp") = ddlClient.SelectedValue
        Session("lp") = ddlLob.SelectedValue
        If Me.IsPostBack = False Then
            ' ddlDepartment is being populated on page load

            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")
        End If

    End Sub
    ''' <summary>
    '''  'Display Users whom rights have been assigned according to selection
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ImgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnSearch.Click

        gvMenuRights.Visible = False
        Bind_Rights() 'Display Users whom rights have been assigned according to selection
        If gvMenuRights.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If


    End Sub
    ''' <summary>
    ''' to get ids of span
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getId()
        If ddlLob.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 And ddlDepartment.SelectedIndex > 0 Then
            DeptId = ddlDepartment.SelectedValue
            Client_Id = ddlClient.SelectedValue
            LobId = ddlLob.SelectedValue
            Exit Sub

        End If
        If ddlDepartment.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 Then
            DeptId = ddlDepartment.SelectedValue
            Client_Id = ddlClient.SelectedValue
            LobId = 0.ToString()
            Exit Sub

        End If
        If ddlDepartment.SelectedIndex > 0 Then
            DeptId = ddlDepartment.SelectedValue
            Client_Id = 0.ToString()
            LobId = 0.ToString()

        End If
    End Sub
    ''' <summary>
    '''   'Page changing code 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMenuRights_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvMenuRights.PageIndexChanging
        If gvMenuRights.PageIndex < gvMenuRights.PageCount And gvMenuRights.PageIndex >= 0 Then
            gvMenuRights.PageIndex = e.NewPageIndex
            Bind_Rights()

        End If
    End Sub
    ''' <summary>
    ''' 'Method to Bind Rights according to selection
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Bind_Rights()
        gvMenuRights.Visible = True
        If UserType = "Super Admin" Then
            getId()
            gvMenuRights.DataSource = objMenu.Bind_AssignedMenuRights(DeptId, Client_Id, LobId, loggedId)
            gvMenuRights.DataBind()
        End If

        If UserType = "Admin" Then
            getId()
            gvMenuRights.DataSource = uspan.Bind_AssignedMenuRightsAdmin(DeptId, Client_Id, LobId, loggedId)
            gvMenuRights.DataBind()

        End If


    End Sub

    Protected Sub gvMenuRights_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenuRights.RowCommand

        If e.CommandName = "SortByUserId" Then
            txtLink = CType((e.CommandSource).Parent.FindControl("lnkUserId"), LinkButton).Text
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim type As String = row.RowIndex.ToString()
            txtlinkUserType = gvMenuRights.Rows(type).Cells(2).Text
            Session("linkUserType") = txtlinkUserType

            Session("linktext") = txtLink  'passing the userid of selected user to next page
            Response.Redirect("EditMenuRights.aspx?val=7")
        End If

        If e.CommandName = "Delete" Then
            txtLink = CType((e.CommandSource).Parent.FindControl("lnkUserId"), LinkButton).Text
            Session("linktext1") = txtLink  'passing the userid of selected user to next page
            pandelete.Visible = True

        End If
        
    End Sub
    ''' <summary>
    ''' confirm for deletion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMenuRights_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvMenuRights.RowDeleting
        txtLinkUserType = gvMenuRights.Rows(e.RowIndex).Cells(2).Text
        Session("LinkTextUserType") = txtLinkUserType

        pandelete.Visible = True 'confirmation box

    End Sub
    ''' <summary>
    '''  'Method called to cancel rights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        Dim txtlink1 As String
        Dim txtlinkUserType As String = Session("LinkTextUserType")
        txtlink1 = Session("linktext1").ToString()
        '*************change*************
        Dim cmdins2 = New SqlCommand("CancelAllRights", conn)
        cmdins2.CommandType = CommandType.StoredProcedure
        With cmdins2.Parameters


            .AddWithValue("@actionBY", Session("userid"))
            .AddWithValue("@Action", "Delete Rights")
            .AddWithValue("@Date", System.DateTime.Now)
            .AddWithValue("@AssignTo", txtlink1)
            .AddWithValue("@Entity", "Menu")
            .AddWithValue("@EntityName", "Cancel All")
            .AddWithValue("@DeptId", ddlDepartment.SelectedValue)
            Dim clta As Integer
            Dim lobt As Integer
            If IsNumeric(ddlClient.SelectedValue) Then
                clta = ddlClient.SelectedValue
            Else
                clta = 0
            End If
            If IsNumeric(ddlLob.SelectedValue) Then
                lobt = ddlLob.SelectedValue
            Else
                lobt = 0
            End If
            .AddWithValue("@ClientId", clta)
            .AddWithValue("@LOBId", lobt)
            .AddWithValue("@type", Session("usertype"))
        End With
        conn.Open()
        cmdins2.ExecuteNonQuery()
        conn.Close()
        cmdins2.Dispose()
        '*************change*************
        If txtlinkUserType = "Admin" Then
            txtlinkUserType = "2"
        ElseIf txtlinkUserType = "Super Admin" Then
            txtlinkUserType = "3"
        ElseIf txtlinkUserType = "User" Then
            txtlinkUserType = "1"
        End If
        objMenu.CancelMenuRights(txtlink1, txtlinkUserType)
        pandelete.Visible = False
        ShowConfirm("Menu Rights Has been Canceled")
        Bind_Rights()

    End Sub
    ''' <summary>
    ''' make pane visible false
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False

    End Sub
    ''' <summary>
    ''' make invisible gvMenuRights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLHUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLHUserControl1.Load
        If ddlDepartment.SelectedIndex = 0 Then
            gvMenuRights.Visible = False
        End If
    End Sub
    ''' <summary>
    ''' 'Method to show message
    ''' </summary>
    ''' <param name="strPassed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        Page.RegisterStartupScript("ShowConfirm", Script)
    End Function
End Class
