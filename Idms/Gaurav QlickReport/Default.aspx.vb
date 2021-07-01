Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Net.Sockets
Imports System.Reflection
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(conn)
    Dim connection1 As New SqlConnection(conn)
    Dim connection11 As New SqlConnection(conn)
    Dim connupdate As New SqlConnection(conn)
    Dim connCheck As New SqlConnection(conn)
    Dim objCmdMarquee1 As New SqlCommand
    Public strMarquee As String
    Dim read As SqlDataReader
    Dim read1 As SqlDataReader
    Dim usertyperdr As SqlDataReader
    Dim b As Boolean
    Dim str1 As String
    Dim str As String
    Dim cmdnew As New SqlCommand

    Private Sub DecryptPassword()
        Dim pwd As String = Crypto.Decrypt("2ASa4GPxU4YG+gjAtCiSBuu8iG4xqjjBuZjueWpZQNg=")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        DecryptPassword()
        Session("useradmincheck") = ""
        txtuserid.Focus()
        Session("usertype") = ""
        If Me.IsPostBack = False Then
            Session("Savegp") = ""
            Session("logincount") = "0"
            Session("userid") = ""
            Session("nmsg") = ""
            Session("userid1") = ""
            Session("userid") = ""
            Session("username") = ""
            Session("username1") = ""
            Session("logintime") = ""
            Session("usertype") = ""
            Session("usertype1") = ""
            Session("lob") = ""
            Session("lobid") = ""
            Session("FromEmailId") = ""
            Session("ServerName") = ""
            Session("typeofuser") = ""
        End If
    End Sub



    Protected Sub Login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login.ServerClick
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
    Private Sub SaveAudits(ByVal result As String)
        'Dim Client As TcpClient
        Dim PublicIP As String = Request.ServerVariables("REMOTE_HOST")
        'Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
        'Dim ipend As Net.IPEndPoint = client.Client.
        'PublicIP = ipend.Address.ToString
        Dim cmdSave As New SqlCommand("insert_WARSPasswordAudit", connection1) 'save data through procedure
        cmdSave.CommandType = CommandType.StoredProcedure
        With cmdSave.Parameters
            'Common Parameters for registration
            .AddWithValue("@UId", txtuserid.Value)
            .AddWithValue("@PwdEntered", Crypto.Encrypt(txtpassword.Value))
            .AddWithValue("@Result", result)
            .AddWithValue("@CurrDate", System.DateTime.Now.ToString("d"))
            .AddWithValue("@Time", System.DateTime.Now.ToShortTimeString)
            '.AddWithValue("@ipaddress", h.AddressList.GetValue(0).ToString)
            '.AddWithValue("@ipaddress", Request.ServerVariables("REMOTE_ADDR"))
            .AddWithValue("@ipaddress", PublicIP)
        End With
        connection1.Open()
        cmdSave.ExecuteNonQuery()
        connection1.Close()
        cmdSave.Dispose()
    End Sub
    Private Function WARSCheckExpire() As String
        Dim warn = ""
        Dim com As New SqlCommand("select EndDate from InternetProductDemo where userid='" & Trim(txtuserid.Value) & "'", connection)
        Dim rdrexp As SqlDataReader
        connection.Open()
        rdrexp = com.ExecuteReader
        If rdrexp.Read Then
            Dim dt As Date = rdrexp("EndDate")
            Dim exp = Abs(DateDiff(DateInterval.Day, System.DateTime.Now, dt))
            If exp < 0 Then
                warn = "Expired"
            ElseIf exp <= 10 And exp >= 0 Then
                warn = "Warn"
            End If
        End If
        'connection.Close()
        rdrexp.Close()
        com.Dispose()
        Return warn
    End Function

    Private Function WARSGetDays()
        lbldays.Text = ""
        Dim com As New SqlCommand("select duration from WARSPasswordDuration where userid='" + Trim(txtuserid.Value) + "'", connection)
        Dim rdr As SqlDataReader
        'connection.Open()
        rdr = com.ExecuteReader
        If rdr.Read Then
            lbldays.Text = rdr("duration")
            'Else
            '    lbldays.Text = "90"
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
        Return 0
    End Function
End Class
