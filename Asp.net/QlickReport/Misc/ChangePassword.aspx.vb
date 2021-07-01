Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Misc_ChangePassword
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(conn)
    Dim connection1 As New SqlConnection(conn)
    Dim connection11 As New SqlConnection(conn)
    Dim connupdate As New SqlConnection(conn)
    Dim connCheck As New SqlConnection(conn)
    Dim objCmdMarquee As New SqlCommand
    Dim objCmdMarquee1 As New SqlCommand
    Public strMarquee As String
    Dim read As SqlDataReader
    Dim read1 As SqlDataReader
    Dim b As Boolean
    Dim str1 As String
    Dim str As String
    Dim cmdnew As New SqlCommand
    Dim rdr As SqlDataReader
    Protected Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        connection.Open()

        If (txtoldPassword.Text = "") Then
            lblmsg.Visible = True
            lblmsg.Text = "Please Enter Old Password."
        ElseIf (txtnewPassword.Text = "") Then
            lblmsg.Visible = True
            lblmsg.Text = "Please Enter New Password."
        ElseIf (txtnewPassword.Text <> txtreEnterPassword.Text) Then
            lblmsg.Visible = True
            lblmsg.Text = "Re-Enter Password should be same with New Password."
        Else
            cmdnew = New SqlCommand("select Pwd from Registration where Pwd='" + Crypto.Encrypt(Trim(txtoldPassword.Text)) + "'", connection)
            rdr = cmdnew.ExecuteReader()
            If rdr.HasRows Then
                rdr.Read()
                Dim str As String = rdr("Pwd").ToString()
                rdr.Close()
                cmdnew.Dispose()
                Dim strnewpwd As String = Crypto.Encrypt(txtnewPassword.Text)
                cmdnew = New SqlCommand("update Registration set Pwd='" + strnewpwd + "' where UserID='" + Session("userid").ToString() + "'", connection)
                cmdnew.ExecuteNonQuery()
                lblmsg.Visible = True
                lblmsg.Text = "Password is successfully Changed..."
                'End If
            Else
                lblmsg.Visible = True
                lblmsg.Text = "Old Password Is Not Correct"
            End If
            connection.Close()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' txtoldPassword.Focus()
        'txtUserName.Text = Session("userid").ToString()
        If Me.IsPostBack = False Then
            If Session("Userid") = "" Then
                Response.Redirect("~/Default.aspx")
            End If
        End If
        'txtoldPassword.Text = Session("pwd").ToString()
    End Sub
End Class
