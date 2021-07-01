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
    Protected Sub Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login.Click
        Dim rdr As SqlDataReader
        Dim rdr1 As SqlDataReader
        If txtuserid.Value = "" Then
            lblmsg.Visible = True
            Me.lblmsg.Text = "UserId Field Should Not Be Blank"
            txtuserid.Focus()
            Exit Sub
        End If
        If txtpassword.Value = "" Then
            lblmsg.Visible = True
            Me.lblmsg.Text = "Password Field Should Not Be Blank"
            txtpassword.Focus()
            Exit Sub
        End If
        connection.Close()
        connection.Open()
        lblmsg.Visible = False

        cmdnew = New SqlCommand("select userid,usertype,status,CreatedBy,username,pwd,deptid,clientid,lobid from registration where userid='" & txtuserid.Value & "' and pwd='" + Crypto.Encrypt(Trim(txtpassword.Value)) + "'", connection)
        rdr = cmdnew.ExecuteReader
        If rdr.Read Then
            If rdr("usertype") = 1 Then
                Session("typeofuser") = "User"
                Session("CreatedBy") = rdr("CreatedBy")
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
                rdr.Close()
            ElseIf rdr("usertype") = 3 Then
                Session("typeofuser") = "Super Admin"
                Session("CreatedBy") = "CreatedBy"
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
            Else
                Session("typeofuser") = "Admin"
                Session("CreatedBy") = "CreatedBy"
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
            End If
        End If
        rdr.Close()
        cmdnew.Dispose()

        cmdnew = New SqlCommand("select lobid,deptid,clientid,adminid,usertype,adminname from masteradmin where adminid='" & Session("userid") & "'", connection)
        'connection.Open()
        rdr = cmdnew.ExecuteReader
        If rdr.Read Then
            Session("useradmincheck") = "yes"
        Else
            Session("useradmincheck") = "no"
        End If
        rdr.Close()

        Dim com As New SqlCommand("select EndDate from InternetProductDemo where userid='" & Trim(txtuserid.Value) & "'", connection)
        Dim rdrexp As SqlDataReader
        rdrexp = com.ExecuteReader
        If rdrexp.Read Then
            Dim dt As Date = rdrexp("EndDate")
            Dim exp = (DateDiff(DateInterval.Day, System.DateTime.Now, dt))
            If exp < 0 Then
                lblmsg.Visible = "True"
                lblmsg.Text = "Your Password has been Expired"
                rdrexp.Close()
                com.Dispose()
            ElseIf exp >= 0 Then
                rdrexp.Close()
                com.Dispose()
                cmdnew = New SqlCommand("select * from Registration where UserID='" + txtuserid.Value + "' and Pwd='" + Crypto.Encrypt(Trim(txtpassword.Value)) + "'", connection)
                rdr = cmdnew.ExecuteReader()
                If rdr.Read Then
                    Session("userid") = rdr("userid")
                    Session("prefix") = rdr("Prefix")
                    Session("username") = rdr("UserName")
                    Session("pwd") = rdr("Pwd").ToString()
                    Response.Redirect("~/Misc/Home.aspx")
                Else
                    lblmsg.Visible = True
                    lblmsg.Text = "Sorry !!! You are not Authenticated Person,Contact to Administrator."
                    txtuserid.Focus()
                End If
                rdr.Close()
                cmdnew.Dispose()
            End If
        End If
    End Sub
End Class
