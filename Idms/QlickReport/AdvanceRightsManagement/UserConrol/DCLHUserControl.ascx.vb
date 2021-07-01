
Partial Class AdvanceRightsManagement_UserConrol_DCLHUserControl
    Inherits System.Web.UI.UserControl
    Dim fun As New Functions
    Dim DeptId As Integer
    Dim Client_Id As Integer
    Dim loggedId As String
    Dim UserType As String



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

            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "autoid"
            ddlLob.DataSource = fun.bind_lob(DeptId, Client_Id)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "--Select--")
           
        End If
    End Sub
End Class
