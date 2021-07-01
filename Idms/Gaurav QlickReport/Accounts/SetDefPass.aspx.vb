Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Set Default Password               *
'*  Summary: Sets the Default Password            *
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class Accounts_SetDefPass
    Inherits System.Web.UI.Page

    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If IsPostBack = False Then
            Dim cmd As New SqlCommand("select pass from IDMS_DefPass", conn)
            Dim reader As SqlDataReader
            Dim str As String
            conn.Open()
            reader = cmd.ExecuteReader
            If reader.Read Then
                str = Crypto.Decrypt(reader("pass"))
                txtSetPassword.Text = str
            Else
                WARSShowMsg("Default password does not exist!!!")
            End If
            reader.Close()
            cmd.Dispose()
            conn.Close()
        End If
        

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Session("typeofuser") = "Super Admin" Then

            Dim str1 As String
            Dim rowFlag As Boolean
            Dim cmdchk As New SqlCommand("select pass from IDMS_DefPass", conn)
            Dim readerchk As SqlDataReader
            conn.Open()
            readerchk = cmdchk.ExecuteReader
            If readerchk.HasRows Then
                rowFlag = True
            End If
            conn.Close()
            str1 = Crypto.Encrypt(txtSetPassword.Text)
            Dim sqlstr As String = ""
            If rowFlag Then
                sqlstr = "update IDMS_DefPass set pass='" & str1 & "'"
            Else
                sqlstr = "insert into IDMS_DefPass values('" & str1 & "')"
            End If
            Dim commandobj As New SqlCommand(sqlstr, conn)
            commandobj.CommandType = CommandType.Text
            conn.Open()
            commandobj.ExecuteNonQuery()
            WARSShowMsg("Default password has been set successful!!!")
            commandobj.Dispose()
            conn.Close()
            sqlstr = "insert into LogAccountMaster values('" + Session("userid") + "'," + "'Set Default Password','" + System.DateTime.Now() + "','Password','Password',0,0,0)"
            commandobj = New SqlCommand(sqlstr, conn)
            conn.Open()
            commandobj.ExecuteNonQuery()
            conn.Close()

            sqlstr = ("insert into LogAccountslave select MAX(Autoid),'SuperAdmin','Set Default Password' from logaccountmaster where EntityName='Password' and Action='Set Default Password'")
            commandobj = New SqlCommand(sqlstr, conn)
            conn.Open()
            commandobj.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            conn.Open()
            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='Password' and Action='Set Default Password'", conn)
            cmm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
        Else

            WARSShowMsg("You are not superadmin!!!")
            Exit Sub

        End If
    End Sub

    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "WARSShowMsg", str)
    End Sub
End Class
