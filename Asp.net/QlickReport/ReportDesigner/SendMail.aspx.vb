Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.Mail
Partial Class ReportDesigner_SendMail
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Public strReport

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Me.IsPostBack = False Then
            'Me.txtemaildata.Text = Me.hidData.Value
            Me.txtemaildata.Value = Session("strFinalData")
            Session.Remove("strFinalData")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Trim(Me.txtemailid.Text) = "" Then
                showmsg("Please fill the mailid.")
                Exit Sub
            End If
             Dim strdomain As String = ""
            Dim i As Integer = 0
            Dim arrMailId As Array = Split(Me.txtemailid.Text, ",")
            For i = 0 To UBound(arrMailId)
                strdomain = Trim(arrMailId(i))
                strdomain = Mid(strdomain, InStr(strdomain, "@"))
                If LCase(strdomain) <> "@daksh.com" And LCase(strdomain) <> "@in.ibm.com" And LCase(strdomain) <> "@in.daksh.com" Then
                    showmsg("Sorry! You can send mail only to recipients residing at @daksh.com , @in.ibm.com and @in.daksh.com")
                    Exit Sub
                End If
            Next

            If Trim(Me.txtemailid.Text) = "" Then
                showmsg("Please supply recipients' emailid.")
                Exit Sub
            End If

            If Trim(Me.txtsubject.Text) = "" Then
                showmsg("Please supply a subject.")
                Exit Sub
            End If
            Dim mailFrom As String = ""
            Dim rdrQuery
            Dim strQryMod As String = "select email from registration where userid='" & Session("userid") & "'"
            Dim cmdMod As New SqlCommand(strQryMod, connection)
            connection.Open()
            rdrQuery = cmdMod.ExecuteReader
            While rdrQuery.Read
                mailFrom = rdrQuery("email")
            End While
            mailFrom = mailFrom ''''' to be removed when IBM smtp starts
            'Dim objEmail As New System.Net.Mail.MailMessage()
            'Dim recipients As System.Net.Mail.MailAddress()
            ' recipients = arrMailId
           
            'Dim str As String = "info@bmprojects.co.in"
            'Dim from1 As Array = str.Split(",")
            'Dim from As System.Net.Mail.MailAddress()
            'from = from1
            ' objEmail.Priority = Net.Mail.MailPriority.High
            ' SmtpMail.SmtpServer = Session("ServerName") '"smtp.net4india.com"
            ' SmtpMail.Send(objEmail)
            Dim recipients As String = Me.txtemailid.Text
            Dim from As String = "info@bmprojects.co.in"
            Dim Subject As String = Me.txtsubject.Text

            Dim body As String = Replace(Me.txtemaildata.Value & "<br><br>" & Me.txtcomments.Text, vbCrLf, "<br>")
            Dim smtpServer As New System.Net.Mail.SmtpClient(Session("ServerName"))
           
            smtpServer.Send(from, recipients, Subject, body)
            showmsg("Report has been sent.")
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            showmsg(strmsg)
        End Try
    End Sub

    Public Sub showmsg(ByVal strmsg As String)
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "showmsg", str)
    End Sub
End Class
