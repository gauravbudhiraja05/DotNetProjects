Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Net.Sockets
Imports System.Reflection
Partial Class WebControl_LeftPanel
    Inherits System.Web.UI.UserControl
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

    'Protected Sub Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login.ServerClick
    '    Dim rdr As SqlDataReader
    '    If txtuserid.Value = "" Then
    '        lblmsg.Visible = True
    '        Me.lblmsg.Text = "UserId Field Should Not Be Blank"
    '        txtuserid.Focus()
    '        Exit Sub
    '    End If

    '    If txtpassword.Value = "" Then
    '        lblmsg.Visible = True
    '        Me.lblmsg.Text = "Password Field Should Not Be Blank"
    '        txtpassword.Focus()
    '        Exit Sub
    '    End If
    '    lblmsg.Visible = False
    '    connection.Open()
    '    cmdnew = New SqlCommand("select * from Registration where UserID='" + txtuserid.Value + "' and Pwd='" + Crypto.Encrypt(Trim(txtpassword.Value)) + "'", connection)

    '    rdr = cmdnew.ExecuteReader()

    '    If rdr.Read Then
    '        Session("userid") = rdr("userid")
    '        Session("prefix") = rdr("Prefix")
    '        Session("username") = rdr("UserName")
    '        'Dim str As String = rdr("Pwd").ToString()
    '        'Session("pwd") = Crypto.Decrypt(str).ToString()
    '        Session("pwd") = rdr("Pwd").ToString()
    '        Response.Redirect("~/Misc/Home.aspx")

    '    Else
    '        lblmsg.Visible = True
    '        lblmsg.Text = "Sorry !!! You are not Authenticated Person,Contact to Administrator."
    '        txtuserid.Focus()
    '    End If
    '    rdr.Close()
    '    connection.Close()
    'End Sub
End Class
