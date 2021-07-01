Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'Imports System.Web.Mail
'Imports System.Net.Mail.MailMessage
Imports System.Web.Mail
Partial Class ForgotPassword
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("connectionString")
    Dim connection1 As New SqlConnection(constr)
    Dim connection As New SqlConnection(constr)
    Dim crypt As New Crypto

    Public Sub showmsg(ByVal strmsg As String)
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Me.GetType, "showmsg", str)
    End Sub


    Public Function SendMail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMsg As String) As Boolean

        'connection.Open()
        Dim cmdgetid As New SqlCommand("select Smtpid,Mailfrom,MailToName,MailFromName from SMTPid", connection)
        Dim drgetid As SqlDataReader
        drgetid = cmdgetid.ExecuteReader
        Dim MailServer As String = "smtp.net4india.com"
        Dim ToEmail As String = Trim(strTo)
        Dim ToName As String = "QlickReport Webmaster"
        Dim FromEmail As String = "info@bmprojects.co.in"
        Dim FromName As String = "QlickReport.com"
        While drgetid.Read
            MailServer = drgetid("Smtpid")
            ToName = drgetid("MailToName")
            FromEmail = drgetid("Mailfrom")
            FromName = drgetid("MailFromName")
        End While
        drgetid.Close()
        connection.Close()
        Dim Subject As String = strSubject
        Dim Body As String = strMsg
        Dim o_Client As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient(MailServer)
        Dim o_FromAddress As System.Net.Mail.MailAddress = New System.Net.Mail.MailAddress(FromEmail, FromName)
        Dim o_ToAddress As System.Net.Mail.MailAddress = New System.Net.Mail.MailAddress(ToEmail, ToName)
        Dim o_Message As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage(o_FromAddress, o_ToAddress)
        o_Message.Subject = Subject
        o_Message.Body = Body
        Try
            o_Client.Send(o_Message)
            Return True
        Catch exc As Exception
            Return False
        End Try
    End Function


    Protected Sub cmdSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim pwd = ""
        Dim email = ""
        txtuserid.Text = Trim(txtuserid.Text)
        Dim objCmd As New SqlCommand("select pwd,email from registration where userid='" & txtuserid.Text & "'", connection)
        Dim rdr As SqlDataReader
        connection.Open()
        rdr = objCmd.ExecuteReader
        If rdr.Read Then
            pwd = rdr("pwd")
            email = rdr("email")
            rdr.Close()
        Else
            rdr.Close()
            showmsg("This User does not exist. So please check it.")
            Exit Sub
        End If
        If IsDBNull(email) Or email.ToString = "" Then
            showmsg("Email ID Not Found. Please Contact Administrator")
            Exit Sub
        End If
       Try
            Dim comm1 As New SqlCommand("select top 1 * from WarsSessionTable", connection1)
            Dim rdrS As SqlDataReader
            connection1.Open()
            rdrS = comm1.ExecuteReader
            If rdrS.Read Then
                Session("FromEmailId") = rdrS("FromMailId") '"info@bmprojects.co.in"
                Session("FromSamparkEmailId") = rdrS("FromMailId") '"info@bmprojects.co.in"
                Session("ServerName") = rdrS("smtp") '"smtp.net4india.com"
                Session("projName") = rdrS("Project") '"WARS"
            End If
            connection1.Close()
            comm1.Dispose()
            rdrS.Close()
            Dim strFrom, strTo, strSubject, strMsg As String
            strFrom = Session("FromEmailId")
            strTo = email
            strSubject = "Your QlickReport.com Password"
            pwd = Crypto.Decrypt(pwd)
            strMsg = "Your QlickReport Password is" & vbCr & pwd
            If SendMail(strFrom, strTo, strSubject, strMsg) = True Then
                showmsg("Your password has send to your mail id .")
            Else
                showmsg("Please check UserId")
            End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            showmsg(strmsg)
        Finally
            Dim win As New System.Text.StringBuilder
            With win
                .Append("<script language='javascript'>")
                .Append("window.close('RePassword.aspx','win', 'height=200,width=500,top=100,left=400,scrollbars=yes')")
                .Append("</script>")
            End With
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", win.ToString())
        End Try

    End Sub
End Class
