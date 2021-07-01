Imports System.Data

Partial Class AdvanceRightsManagement_RightsHome
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' display time, date and user id 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''If (Session("userid") = "") Then
        ''    Response.Redirect("Index.aspx")
        ''Else

        lblUserid.Text = Session("userid")
        lblDate.Text = System.DateTime.Now.ToString()

        'End If

    End Sub
End Class
