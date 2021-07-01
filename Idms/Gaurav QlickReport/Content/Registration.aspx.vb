Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Net
Imports System.Net.Mail
Imports System.Data.DataSet
Partial Class Content_Registration
    Inherits System.Web.UI.Page
    Dim strcon As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(strcon)
    Dim date1 As String
    Dim name, pass, userid, company, email, mobile, pwd As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim code As String
        date1 = System.DateTime.Now.Date.ToString()
        code = Session("item")
        Session("code") = code
        selectitem.Text = code
    End Sub
    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        lblmsg.Visible = True
        If (txtname.Text = "") Then
            lblmsg.Text = "Please enter the name first"
            Exit Sub
        ElseIf (txtCmy.Text = "") Then
            lblmsg.Text = "Please enter company name first"
            Exit Sub
        ElseIf (txtemail.Text = "") Then
            lblmsg.Text = "Please enter e-mail id"
            Exit Sub
        ElseIf (txtmobile.Text = "") Then
            lblmsg.Text = "Please enter mobile number"
            Exit Sub
        End If
        connection.Open()
        name = txtname.Text
        company = txtCmy.Text
        mobile = txtmobile.Text.ToString()
        email = txtemail.Text
        Dim item, databasetype, producttype, database As String
        item = Session("item")
        databasetype = Session("databasetype")
        producttype = Session("producttype")
        database = Session("database")
        Dim var As Integer
        Dim cmd2 As New SqlCommand("select count(*) from Registration where EMail='" + email + "'", connection)
        var = Convert.ToInt32(cmd2.ExecuteScalar())
        If (var <> 0) Then
            finalmsg.Visible = True
            finalmsg.Text = "E-mail id are already exist !!"
            txtemail.Text = ""
        Else
            pwd = email.Substring(0, 3)
            pass = pwd + "@1234"
            userid = txtemail.Text
            Dim CrpStr As String = Crypto.Encrypt(pass)
            If (producttype = "Single User" And databasetype = "Single Database" Or producttype = "Single User" And databasetype = "Multiple Database") Then
                Dim cmdSave As New SqlCommand("Insert_Registration", connection) 'save data through procedure
                cmdSave.CommandType = CommandType.StoredProcedure
                With cmdSave.Parameters
                    'Common Parameters for registration
                    .AddWithValue("@UserType", "1")
                    .AddWithValue("@UserId", userid)
                    .AddWithValue("@Pwd", CrpStr)
                    .AddWithValue("@Prefix", "Null")
                    .AddWithValue("@Username", name)
                    .AddWithValue("@Designation", "Null")
                    .AddWithValue("@BU", "Null")
                    .AddWithValue("@EMail", email)
                    .AddWithValue("@Status", "Active")
                    .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                    .AddWithValue("@EMPId", "Null")
                    .AddWithValue("@CreatedBy", Session("userid"))
                    .AddWithValue("@LocalUser", "NonLocal")
                    .AddWithValue("@DeptId", "0")
                    .AddWithValue("@ClientId", "0")
                    .AddWithValue("@LOBID", "0")
                    .AddWithValue("@CreatorId", "3")
                    .AddWithValue("@company", company)
                    .AddWithValue("@mobile", mobile)
                End With
                cmdSave.ExecuteNonQuery()
                cmdSave.Dispose()
            Else
                Dim cmdSave As New SqlCommand("Insert_Registration", connection) 'save data through procedure
                cmdSave.CommandType = CommandType.StoredProcedure
                With cmdSave.Parameters
                    'Common Parameters for registration
                    .AddWithValue("@UserType", "3")
                    .AddWithValue("@UserId", userid)
                    .AddWithValue("@Pwd", CrpStr)
                    .AddWithValue("@Prefix", "Null")
                    .AddWithValue("@Username", name)
                    .AddWithValue("@Designation", "Null")
                    .AddWithValue("@BU", "Null")
                    .AddWithValue("@EMail", email)
                    .AddWithValue("@Status", "Active")
                    .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                    .AddWithValue("@EMPId", "Null")
                    .AddWithValue("@CreatedBy", Session("userid"))
                    .AddWithValue("@LocalUser", "NonLocal")
                    .AddWithValue("@DeptId", "0")
                    .AddWithValue("@ClientId", "0")
                    .AddWithValue("@LOBID", "0")
                    .AddWithValue("@CreatorId", userid)
                    .AddWithValue("@company", company)
                    .AddWithValue("@mobile", mobile)
                End With
                cmdSave.ExecuteNonQuery()
                cmdSave.Dispose()
            End If
            Dim cmd5 As New SqlCommand("select DATEADD(Day,30,'" + date1 + "') as expiredate", connection)
            Dim dr5 As SqlDataReader
            dr5 = cmd5.ExecuteReader()
            dr5.Read()

            'Code to insert values in InternetProductDemo for purchased product
            Dim expire As String
            expire = dr5("expiredate").ToString()
            dr5.Close()
            Dim cmd4 As New SqlCommand("InternetProductDemoInsert", connection)
            cmd4.CommandType = CommandType.StoredProcedure
            With cmd4.Parameters
                .AddWithValue("@UserId", userid)
                .AddWithValue("@ProductCode", item)
                .AddWithValue("@InsertDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
                .AddWithValue("@EndDate", expire)
                .AddWithValue("@Status", "Active")
                .AddWithValue("@ProductType", producttype)
                .AddWithValue("@DatabaseType", databasetype)
                .AddWithValue("@Database", database)
            End With
            cmd4.ExecuteNonQuery()
            cmd4.Dispose()


            'Code to insert values for password expiration
            Dim UDC As New SqlCommand("sp_CreateDuration", connection)
            UDC.CommandType = CommandType.StoredProcedure
            With UDC.Parameters
                .AddWithValue("@UserId", userid)
                .AddWithValue("@Duration", 30)
                .AddWithValue("@UpdBy", Session("userid"))
                .AddWithValue("@CurrDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
            End With
            UDC.ExecuteNonQuery()

            'Save the password to history
            Dim com As New SqlCommand("insert_PWDHistory", connection)
            com.CommandType = CommandType.StoredProcedure
            With com.Parameters
                .AddWithValue("@UserID", userid)
                .AddWithValue("@Pwd", CrpStr)
                .AddWithValue("@UpdateDate", System.DateTime.Now.ToString())
                .AddWithValue("@UpdatedBy", Session("userid"))
            End With
            com.ExecuteNonQuery()
            com.Dispose()

            'code to fetch the rights as per the selected product code
            Dim dt As String
            Dim cmd6 As New SqlCommand("select Rights from ProductMaster where ProductCode='" + selectitem.Text + "' and DatabaseType='" + databasetype + "' and UserType='" + producttype + "' ", connection)
            Dim rdrights As SqlDataReader
            rdrights = cmd6.ExecuteReader
            If rdrights.Read Then
                dt = rdrights("Rights")
            End If
            Dim arrhead() As String = dt.Split(",")
            connection.Close()
            connection.Open()
            For i = 0 To arrhead.Length - 1
                Dim cmdins1 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                cmdins1.CommandType = CommandType.StoredProcedure
                With cmdins1.Parameters
                    .AddWithValue("@LOB", "0")
                    .AddWithValue("@MenuId", arrhead(i))
                    .AddWithValue("@UserType", "1")
                    .AddWithValue("@Access", "")
                    .AddWithValue("@Currdate", System.DateTime.Now)
                    .AddWithValue("@AssignBy", "Null")
                    .AddWithValue("@parentid", "0")
                    .AddWithValue("@userid", userid)
                End With
                cmdins1.ExecuteNonQuery()
                Dim j As String
                j = arrhead(i).ToString()
                cmdins1.Dispose()

                'Fetch the sub menus on the basis of parent loop menu
                Dim menu As Integer
                If (arrhead(i).Equals("31") And database.Equals("Excel")) Then
                    Dim arrhead2() As Integer = {32, 33, 34}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("Oracle")) Then
                    Dim arrhead2() As Integer = {34, 159}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("MS-SQL")) Then
                    Dim arrhead2() As Integer = {34, 158}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("Excel,Oracle")) Then
                    Dim arrhead2() As Integer = {32, 33, 34, 159}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("Excel,MS-SQL")) Then
                    Dim arrhead2() As Integer = {32, 33, 34, 158}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("MS-SQL,Oracle")) Then
                    Dim arrhead2() As Integer = {158, 159}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("Excel,MS-SQL,Oracle")) Then
                    Dim arrhead2() As Integer = {32, 33, 34, 158, 159}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("6") And database.Equals("MS-SQL") Or arrhead(i).Equals("6") And database.Equals("Oracle") Or arrhead(i).Equals("6") And database.Equals("MySQL") Or arrhead(i).Equals("6") And database.Equals("PostgreSQL")) Then
                    Dim arrhead2() As Integer = {37, 149}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                Else
                    'Dim menu As Integer
                    If (j = 17) Then
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", "21")
                            .AddWithValue("@UserType", "1")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", "Null")
                            .AddWithValue("@parentid", "17")
                            .AddWithValue("@userid", userid)
                        End With
                        cmdins2.ExecuteNonQuery()
                        cmdins1.Dispose()
                    Else
                        Dim datacmd As New SqlCommand("select * from nlvl_menu where ParentID='" + j + "'", connection)
                        Dim da As SqlDataAdapter
                        da = New SqlDataAdapter(datacmd)
                        Dim ds As New DataSet
                        ds = New DataSet()
                        da.Fill(ds, "abc")
                        Dim rowentry As DataRow
                        For Each rowentry In ds.Tables("abc").Rows
                            menu = rowentry("MenuId").ToString()


                            Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters
                                .AddWithValue("@LOB", "0")
                                .AddWithValue("@MenuId", menu)
                                .AddWithValue("@UserType", "1")
                                .AddWithValue("@Access", "")
                                .AddWithValue("@Currdate", System.DateTime.Now)
                                .AddWithValue("@AssignBy", "Null")
                                .AddWithValue("@parentid", arrhead(i))
                                .AddWithValue("@userid", userid)
                            End With
                            cmdins2.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next
            'the code after changes for mltiple user are given here by Mr Mohit Tyagi


            connection.Close()
            finalmsg.Visible = True
            finalmsg.Text = "Your Registration has been successfully completed"
            'sendmail()
            txtname.Text = ""
            txtCmy.Text = ""
            txtemail.Text = ""
            txtmobile.Text = ""
        End If
    End Sub
    'Function to send userid and password on the mail after successful registration
    Private Function sendmail() As Boolean
        userid = txtemail.Text
        pwd = email.Substring(0, 3)
        pass = pwd + "@1234"
        Dim bp As String
        Dim enddate As String
        enddate = ""
        bp = txtname.Text
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        connection.Open()
        cmd = New SqlCommand("select EndDate from InternetProductDemo where UserID='" + userid + "'", connection)
        dr = cmd.ExecuteReader()
        While (dr.Read())
            enddate = dr("EndDate").ToString()
        End While
        dr.Close()
        connection.Close()
        Dim fromAddress As New MailAddress("donot@qlickreport.com", "QlickReport.com")
        Dim toAddress As New MailAddress(email, "Recipient")
        Dim subject As String = "Registration with QlickReport.com"
        'Dim Body As String = "Use QlickReport tool with the given userid and password" + "<br/>" + "Your Userid= " + userid + " Password is : " + pass
        Dim Body As String = "<table width=865><tr><td>"
        Body += "<img src='http://qlickreport.com/Content/Images/MiniTop.jpg'>" + "<br />"
        Body += "<hr height=5 COLOR =#0472A7>" + "<br /><br />"
        Body += "Dear " + bp + ",<br /><br />"
        Body += "Congratulation!! You are now registered User with QlickReport.com. You can access QlickReport BI Tool with following Login ID & Password.<br /><br />"
        Body += "<b>Login ID :</b>" + userid + "<br />"
        Body += "<b>Password :</b>" + pass + "<br /><br />"
        Body += "<b>Note :- Your UserId & Password will be expire on " + enddate + " .So kindly purchase license before expiry date to continue with QlickReport BI Tool benefits</b><br /><br />"
        Body += "This is an automatically generated message. Replies are not monitored or answered. <br /><br />"
        Body += "<hr height=5 COLOR =#0472A7>" + "<br /><br />"
        Body += "<b>Regards, </b><br> QlickReport Team <br>B M Project Engineers Pvt. Ltd.<br>A-346, South City - I,<br> Gurgaon, Haryana - 122001<br>E-mail: info@QlickReport.com<br>Phone: +91-0124-4210106" + "<br />"
        Body += "</td></tr></table>"
        Dim smtp As SmtpClient = New SmtpClient()
        smtp.Host = "smtp.gmail.com"
        smtp.Port = 587
        smtp.EnableSsl = True
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network
        smtp.Timeout = 200000
        ' Dim message As String
        Using message As New MailMessage(fromAddress, toAddress)
            message.Subject = subject
            message.Body = Body
            message.IsBodyHtml = True
            smtp.Send(message)
            mailmsg.Visible = True
        End Using
    End Function
    Protected Sub txtname_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtname.Unload

    End Sub

   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rdr As SqlDataReader
        Dim rdr1 As SqlDataReader
        Dim cmdnew As SqlCommand
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
