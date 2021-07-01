Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: Qlick Report                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Registration                       *
'*  Summary: Registers new User Accounts          *
'*  Created on: 18/06/2012                          *
'*  Created By: Mohit Tyagi                *
'**************************************************
Partial Class Accounts_Registration
    Inherits System.Web.UI.Page
    Dim strcon As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(strcon)
    Dim con As New SqlConnection(strcon)
    Dim cmdnew As SqlCommand
    Dim cmd2 As SqlCommand 
    Dim rdr As SqlDataReader
    Dim ObjLib As New Library
    'Dim connection1 As New SqlConnection(strcon)
    'Dim c As New Checks Commented on date-04/05/08
    Dim varuserid
    Dim varuserid1
    Dim date1 As String
    Dim code, databasetype, database, producttype As String
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    ''Proc to Bind Designation in the dropdownlist
    Private Sub WARSBindDesig()
        Dim cmd As New SqlCommand("Select * from WARSDesignationMaster where Designationname <> ''", connection)
        Dim adp As New SqlDataAdapter
        adp.SelectCommand = cmd
        Dim ds As New DataSet()
        connection.Open()
        adp.Fill(ds)
        connection.Close()
        cboDesignation.DataSource = ds
        cboDesignation.DataTextField = "Designationname"
        cboDesignation.DataValueField = "Designationname"
        cboDesignation.DataBind()
        cboDesignation.Items.Insert(0, "-----Select-----")
        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()
    End Sub
    ''Proc to bind BU in the dropdownlist
    Private Sub WARSBindBU()
        Dim cmdbu As New SqlCommand("select * from WARSBUmaster where BUName<>'' and BUName='BFI'", connection)
        Dim adpbu As New SqlDataAdapter
        Dim dsbu As New DataSet
        adpbu.SelectCommand = cmdbu
        connection.Open()
        adpbu.Fill(dsbu)
        connection.Close()
        CBOBU.DataSource = dsbu
        CBOBU.DataTextField = "BUName"
        CBOBU.DataValueField = "BUName"
        CBOBU.DataBind()
        'CBOBU.Items.Insert(0, "-----Select-----")
        dsbu.Dispose()
        adpbu.Dispose()
        cmdbu.Dispose()
    End Sub
    ''Proc to display messages  
    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Page.GetType(), "showmsg", str)
    End Sub
    '' Page_Load Event
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s As String = Session("userid")
        date1 = System.DateTime.Now.Date.ToString()
        If Me.IsPostBack = False Then

            '' Declare object of class Functions
            Dim classobj As New Functions
            ''Code to Bind Department Names in the dropdownlist 
            Dim cmd2 As New SqlCommand("select DepartmentName,AutoID from IdmsDepartment where SavedBy='" + Session("userid") + "'", connection)
            Dim adp As New SqlDataAdapter
            adp.SelectCommand = cmd2
            Dim ds As New DataSet()
            connection.Open()
            adp.Fill(ds)
            connection.Close()
            DepartmentName.DataSource = ds
            DepartmentName.DataTextField = "DepartmentName"
            DepartmentName.DataValueField = "AutoID"
            DepartmentName.DataBind()
            DepartmentName.Items.Insert(0, "-----Select-----")
            ds.Dispose()
            adp.Dispose()
            cmd2.Dispose()
            'DepartmentName.DataTextField = "DepartmentName"
            'DepartmentName.DataValueField = "AutoId"
            'DepartmentName.DataSource = classobj.bind_Department()
            'DepartmentName.DataBind()
            'DepartmentName.Items.Insert(0, "-----Select-----")
            ''Calling the function to bind Designations
            WARSBindDesig()
            ''Calling the function to bind BU
            WARSBindBU()
        End If
        Dim cmd As SqlCommand
        If (Session("typeofuser") = "Super Admin") Then
            cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
        Else
            cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
        End If
        Dim dsar As DataSet = New DataSet()
        Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
        daar.Fill(dsar)
        Dim val1 As String
        Dim val2 As String
        Dim val3 As String
        val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
        val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
        val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
        lbl1.Text = val1
        lbl2.Text = val2
        lbl3.Text = val3
    End Sub
    ''Proc to Bind Client dropdownlist on selecting a department  
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If DepartmentName.SelectedItem.Text = "-----Select-----" Then
            ClientName.Items.Clear()
            'ClientName.Items.Insert(0, "-----Select-----")
            cboLOBDept.Items.Clear()
        Else
            Dim classobj As New Functions
            ClientName.DataTextField = "clientname"
            ClientName.DataValueField = "autoid"
            ClientName.DataSource = classobj.bind_client(DepartmentName.SelectedValue)
            ClientName.DataBind()
            ClientName.Items.Insert(0, "-----Select-----")
            cboLOBDept.Items.Clear()
        End If
    End Sub
    ''Proc to Bind LOB dropdownlist on selecting a Client
    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If ClientName.SelectedItem.Text = "-----Select-----" Then
            cboLOBDept.Items.Clear()

            'cboLOBDept.Items.Insert(0, "-----Select-----")
        Else

            Dim classobj As New Functions
            cboLOBDept.DataTextField = "lobname"
            cboLOBDept.DataValueField = "autoid"
            cboLOBDept.DataSource = classobj.bind_lob(DepartmentName.SelectedValue, ClientName.SelectedValue)
            cboLOBDept.DataBind()
            cboLOBDept.Items.Insert(0, "-----Select-----")
        End If
    End Sub
    ''Function to check whether the designation already exists in the databse or not
    Private Function chkDesigExist() As Boolean
        Dim desg
        If Trim(txtDesig.Text) = "" Then
            Exit Function
        End If

        desg = Trim(txtDesig.Text)

        Dim com As New SqlCommand("select * from WARSDesignationMaster where designationname='" & desg & "'", connection)
        connection.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader

        If rdr.Read Then
            WARSShowMsg("Designation already exists,Please specify another!!!")
            WARSSetFocus(txtDesig)
            connection.Close()
            rdr.Close()
            com.Dispose()
            Return False
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
        Return True

    End Function
    ''Proc to save designation in the database table when enterted through textbox 
    Private Sub saveDesignation()
        Dim cmdsave As New SqlCommand("insert into WARSDesignationMaster values('" & txtDesig.Text & "','" & System.DateTime.Now.ToString("d") & "')", connection)
        connection.Open()
        cmdsave.ExecuteNonQuery()
        connection.Close()
        cmdsave.Dispose()
    End Sub
    '' Proc to set the focus on controls
    Public Sub WARSSetFocus(ByVal FocusControl As Control)
        'this function is for setting the focus
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Page.GetType(), "setFocus", Script.ToString())
    End Sub
    ''Function to check if the UserID already exists in the database or not
    Private Function WARSChkId() As Boolean
        'check for uniquesness of user id
        If txtUserId.Text = "" Then
            WARSShowMsg("Please enter a userid!!!")
            Exit Function
        End If
        Dim commId As New SqlCommand("select * from Registration where userid='" & txtUserId.Text & "'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            WARSShowMsg("Userid already exists,Please select another!!!")
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return False
        Else
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return True
        End If
    End Function

    ''change 12/06
    Private Function WARSEmailId() As Boolean
        'check for uniquesness of user id
        If txtEmail.Text = "" Then
            WARSShowMsg("Please enter a Emailid!!!")
            Exit Function
        End If
        Dim commId As New SqlCommand("select * from Registration where email='" & txtEmail.Text & "' and status='Active'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            WARSShowMsg("Email Id already exists,Please select another!!!")
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            txtEmail.Text = ""

            Return False
        Else
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return True
        End If
    End Function
    ''change 12/06
    ''Function to check whether the employee ID already exists in the database or not 
    Private Function WARSChkEmpId()
        'check for uniquesness of user id

        Dim commId As New SqlCommand("select * from Registration where empid='" & txtEmpId.Text & "'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            WARSShowMsg("Employeeid already exists,Please enter another!!!")
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return False
        Else
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return True
        End If
    End Function
    '' Function for the user to insert the right value in all the fields
    Public Function WARSRegChkBlankValid()
        'returns false if any mandatory field is blank otherwise returns true

        If ObjLib.chkvalidtxt1(txtName.Text) = False Then
            WARSShowMsg("Name should contain only alphabets and space or dot!!!")
            txtName.Text = ""
            WARSSetFocus(txtName)
            Return False
        End If
        'If txtPwd.Text = "" Or txtUserId.Text = "" Then
        '    WARSShowMsg("Userid Or Password cannot be Blank")
        'Else
        '    If UCase(txtPwd.Text).Contains(UCase(txtUserId.Text)) = True Then
        '        WARSShowMsg("Password Must Not Contain Userid.")
        '        txtPwd.Text = ""
        '        WARSSetFocus(txtPwd)
        '        Return False
        '    End If
        'End If

        
        'If WARSChkId() = False Then
        '    Return False
        'End If

        If txtName.Text = "" Then
            WARSShowMsg("Please fill name!!!")
            WARSSetFocus(txtName)
            Return False
        End If


        If txtEmpId.Text = "" Or ObjLib.AlphTrue1(txtEmpId.Text) = True Or ObjLib.NumericTrue1(txtEmpId.Text) = False Then
            WARSShowMsg("Please fill employeeid only in numbers!!!")
            WARSSetFocus(txtEmpId)
            Return False
        End If

        If cboDesignation.SelectedIndex = 0 And txtDesig.Text = "" Then
            WARSShowMsg("Please fill designation!!!")
            WARSSetFocus(cboDesignation)
            Return False
        End If

        If cboDesignation.SelectedIndex = 0 And ObjLib.chkvalidtxt1(txtDesig.Text) = False Then
            WARSShowMsg("Designation should contain only alphabets and space or dot!!!")
            WARSSetFocus(txtDesig)
            txtDesig.Text = ""
            Return False
        End If

        If DepartmentName.SelectedIndex = 0 Then
            WARSShowMsg("Please select department!!!")
            WARSSetFocus(DepartmentName)
            Return False
        End If
        If Me.txtEmail.Text = "" Then
            WARSShowMsg("Please fill emailid!!!")
            WARSSetFocus(txtEmail)
            Return False
        End If
        If txtUserId.Text = "" Then
            WARSShowMsg("Please fill userid!!!")
            WARSSetFocus(txtUserId)
            Return False
        End If
        If WARSChkId() = False Then
            Return False
        End If
        If WARSEmailId() = False Then
            Return False
        End If

        If txtPwd.Text = "" Then
            WARSShowMsg("Please fill password!!!")
            WARSSetFocus(txtPwd)
            Return False
        End If

        If ObjLib.chkpwd(txtPwd.Text) = True Then
            WARSShowMsg("Password length should be between eight and fifteen characters and must contain alphanumeric values!!!")
            WARSSetFocus(txtPwd)
            Return False
        End If



        If txtPwd.Text = "" Or txtUserId.Text = "" Then
            WARSShowMsg("Userid or password cannot be Blank!!!")
        Else
            If UCase(txtPwd.Text).Contains(UCase(txtUserId.Text)) = True Then
                WARSShowMsg("Password must not contain userid!!!")
                txtPwd.Text = ""
                WARSSetFocus(txtPwd)
                Return False
            End If
        End If


        If txtConfirmPwd.Text = "" Then
            WARSShowMsg("Please fill confirm password!!!")
            WARSSetFocus(txtPwd)
            Return False
        End If

        If txtPwd.Text <> txtConfirmPwd.Text Then
            WARSShowMsg("Password and confirm password should be same!!!")
            WARSSetFocus(txtConfirmPwd)
            Return False
        End If
        Return True
    End Function

    ''Proc to Save th values from the form into the tables 

    Protected Sub cmdsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsave.Click

        ''Calling the function to apply checks on fields while saving them into the database 
        'Check if any compulsory field id blank
        If WARSRegChkBlankValid() = False Then
            'DepartmentName.SelectedIndex = 0
            Exit Sub
        End If

        ''Code to check if the designation textbox is not blank call the function to check if it already exists in the dropdown else call the function to save it

        If Trim(txtDesig.Text) <> "" Then
            If chkDesigExist() = False Then
                'DepartmentName.SelectedIndex = 0
                Exit Sub
            End If

            ''Call function To save designation

            saveDesignation()

            ''Call function to bind designation

            WARSBindDesig()
            cboDesignation.SelectedValue = txtDesig.Text
            txtDesig.Text = ""
        End If
        ''Calling Function to chect whether the Employee Id Exist or Not
        If chkEmpId() = True Then
            Exit Sub
        End If
        Dim strUserid As String = txtUserId.Text
        '' Proc to insert the form values into the table Registration
        If Session("typeofuser") = "Admin" Then

            Dim dept, lob, clt As String
            If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                clt = "0"
            Else
                clt = ClientName.SelectedValue
            End If
            If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                lob = "0"
            Else
                lob = cboLOBDept.SelectedValue
            End If
            If DepartmentName.SelectedIndex = 0 Or DepartmentName.SelectedIndex = -1 Then
                dept = "0"
            Else
                dept = DepartmentName.SelectedValue
            End If
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                '.AddWithValue("@Deptid", dept)
                '.AddWithValue("@Clientid", clt)
                '.AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                Dim CrpStr As String = Crypto.Encrypt(txtPwd.Text)
                Dim cmdSave As New SqlCommand("Insert_Registration", connection) 'save data through procedure
                cmdSave.CommandType = CommandType.StoredProcedure
                With cmdSave.Parameters
                    'Common Parameters for registration
                    .AddWithValue("@UserType", "1")
                    .AddWithValue("@UserId", txtUserId.Text)
                    .AddWithValue("@Pwd", CrpStr)
                    .AddWithValue("@Prefix", cboPrefix.SelectedValue)
                    .AddWithValue("@Username", txtName.Text)
                    If Trim(txtDesig.Text) = "" Then
                        .AddWithValue("@Designation", cboDesignation.SelectedItem.Text)
                    Else
                        .AddWithValue("@Designation", txtDesig.Text)
                    End If
                    .AddWithValue("@BU", "NULL")
                    .AddWithValue("@EMail", txtEmail.Text)
                    .AddWithValue("@Status", "Active")
                    .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                    .AddWithValue("@EMPId", txtEmpId.Text)
                    If Session("userid") = "idmsadmin" Then
                        .AddWithValue("@CreatorId", 3)
                    Else
                        .AddWithValue("@CreatorId", 2)
                    End If
                    .AddWithValue("@CreatedBy", Session("userid"))
                    If chkSelectscope.Checked = True Then

                        .AddWithValue("@LocalUser", chkSelectscope.Text)
                    Else
                        .AddWithValue("@LocalUser", "NonLocal")
                    End If
                    .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                    If ClientName.SelectedItem.Text = "-----Select-----" Or ClientName.SelectedItem.Text = "" Then
                        .AddWithValue("@ClientId", "0")
                    Else
                        .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                    End If

                    If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                        .AddWithValue("@LOBID", "0")
                    Else
                        .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                    End If


                End With
                readerdata.Close()
                If connection.State = ConnectionState.Open Then
                    connection.Close()

                End If
                connection.Open()
                cmdSave.ExecuteNonQuery()
                con.Close()
                cmdSave.Dispose()
                connection.Close()
                cmdSave.Dispose()


                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + Me.txtUserId.Text + "' and Action='Create'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmdSave11 As New SqlCommand("Insert into warscountlogin values(@userid,0)", connection)
                cmdSave11.Parameters.AddWithValue("@userid", txtUserId.Text)
                connection.Open()
                cmdSave11.ExecuteNonQuery()
                connection.Close()
                ''Proc to insert form values into the table Buddy

                Dim cmdSave1 As New SqlCommand("Insert_buddy", connection) 'save data through procedure
                cmdSave1.CommandType = CommandType.StoredProcedure
                With cmdSave1.Parameters
                    'Common Parameters for registration

                    .AddWithValue("@UserId", txtUserId.Text)
                    .AddWithValue("@Username", txtName.Text)
                    If chkSelectscope.Checked = True Then

                        .AddWithValue("@LocalUser", "Local")
                    Else
                        .AddWithValue("@LocalUser", "Nonlocal")
                    End If

                    .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                    .AddWithValue("@Departmentname", CType(DepartmentName.SelectedItem.Text, String))
                    If ClientName.SelectedItem.Text = "-----Select-----" Or ClientName.SelectedItem.Text = "" Then
                        .AddWithValue("@ClientId", "0")
                        .AddWithValue("@Clientname", "0")
                    Else
                        .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                        .AddWithValue("@Clientname", CType(ClientName.SelectedItem.Text, String))
                    End If


                    If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                        .AddWithValue("@LOBID", "0")
                        .AddWithValue("@LOBname", "0")
                    Else
                        '.AddWithValue("@LOBID", CType(cboLOBDept.SelectedValue, Integer))
                        .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                        .AddWithValue("@LOBname", CType(cboLOBDept.SelectedItem.Text, String))
                    End If


                End With

                connection.Open()
                cmdSave1.ExecuteNonQuery()
                con.Close()
                cmdSave1.Dispose()
                clear()
                connection.Close()


                Dim myConnection As New SqlConnection(strcon)
                Dim UDC As New SqlCommand("sp_CreateDuration", myConnection)
                UDC.CommandType = CommandType.StoredProcedure
                With UDC.Parameters
                    .AddWithValue("@UserId", strUserid)
                    .AddWithValue("@Duration", 90)
                    .AddWithValue("@UpdBy", Session("userid"))
                    .AddWithValue("@CurrDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
                End With
                myConnection.Open()
                UDC.ExecuteNonQuery()
                myConnection.Close()

                'Save the password to history
                Dim com As New SqlCommand("insert_PWDHistory", myConnection)
                com.CommandType = CommandType.StoredProcedure
                With com.Parameters
                    .AddWithValue("@UserID", strUserid)
                    .AddWithValue("@Pwd", CrpStr)
                    .AddWithValue("@UpdateDate", System.DateTime.Now.ToString())
                    .AddWithValue("@UpdatedBy", Session("userid"))
                End With
                myConnection.Open()
                com.ExecuteNonQuery()
                myConnection.Close()
                com.Dispose()
                
                WARSShowMsg("Registeration has been completed successfully!!!")
            Else
                WARSShowMsg("You are not admin of this span!!!")
                Exit Sub
            End If
        ElseIf Session("typeofuser") = "Super Admin" Then
            Dim CrpStr As String = Crypto.Encrypt(txtPwd.Text)
            Dim cmdSave As New SqlCommand("Insert_Registration", connection) 'save data through procedure
            cmdSave.CommandType = CommandType.StoredProcedure
            With cmdSave.Parameters
                'Common Parameters for registration
                .AddWithValue("@UserType", "1")
                .AddWithValue("@UserId", txtUserId.Text)
                .AddWithValue("@Pwd", CrpStr)
                .AddWithValue("@Prefix", cboPrefix.SelectedValue)
                .AddWithValue("@Username", txtName.Text)
                If Trim(txtDesig.Text) = "" Then
                    .AddWithValue("@Designation", cboDesignation.SelectedItem.Text)
                Else
                    .AddWithValue("@Designation", txtDesig.Text)
                End If
                .AddWithValue("@BU", CBOBU.SelectedItem.Text)
                .AddWithValue("@EMail", txtEmail.Text)
                .AddWithValue("@Status", "Active")
                .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                .AddWithValue("@EMPId", txtEmpId.Text)
                If Session("userid") = "idmsadmin" Then
                    .AddWithValue("@CreatorId", 3)
                Else
                    .AddWithValue("@CreatorId", 2)
                End If
                .AddWithValue("@CreatedBy", Session("userid"))
                If chkSelectscope.Checked = True Then

                    .AddWithValue("@LocalUser", chkSelectscope.Text)
                Else
                    .AddWithValue("@LocalUser", "NonLocal")
                End If
                .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                If ClientName.SelectedItem.Text = "-----Select-----" Or ClientName.SelectedItem.Text = "" Then
                    .AddWithValue("@ClientId", "0")
                Else
                    .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                End If

                If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                    .AddWithValue("@LOBID", "0")
                Else
                    .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                End If
                .AddWithValue("@company", "")
                .AddWithValue("@mobile", "123")
            End With
            If connection.State = ConnectionState.Open Then
                connection.Close()

            End If
            connection.Open()
            cmdSave.ExecuteNonQuery()
            con.Close()
            cmdSave.Dispose()
            connection.Close()
            cmdSave.Dispose()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + Me.txtUserId.Text + "' and Action='Create'", connection)
            connection.Open()
            cmm.ExecuteNonQuery()
            connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            Dim cmdSave11 As New SqlCommand("Insert into warscountlogin values(@userid,0)", connection)
            cmdSave11.Parameters.AddWithValue("@userid", txtUserId.Text)
            connection.Open()
            cmdSave11.ExecuteNonQuery()
            connection.Close()
            ''Proc to insert form values into the table Buddy

            Dim cmdSave1 As New SqlCommand("Insert_buddy", connection) 'save data through procedure
            cmdSave1.CommandType = CommandType.StoredProcedure
            With cmdSave1.Parameters
                'Common Parameters for registration

                .AddWithValue("@UserId", txtUserId.Text)
                .AddWithValue("@Username", txtName.Text)
                If chkSelectscope.Checked = True Then

                    .AddWithValue("@LocalUser", "Local")
                Else
                    .AddWithValue("@LocalUser", "Nonlocal")
                End If
                .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                .AddWithValue("@Departmentname", CType(DepartmentName.SelectedItem.Text, String))
                If ClientName.SelectedItem.Text = "-----Select-----" Or ClientName.SelectedItem.Text = "" Then
                    .AddWithValue("@ClientId", "0")
                    .AddWithValue("@Clientname", "0")
                Else
                    .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                    .AddWithValue("@Clientname", CType(ClientName.SelectedItem.Text, String))
                End If


                If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                    .AddWithValue("@LOBID", "0")
                    .AddWithValue("@LOBname", "0")
                Else
                    '.AddWithValue("@LOBID", CType(cboLOBDept.SelectedValue, Integer))
                    .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                    .AddWithValue("@LOBname", CType(cboLOBDept.SelectedItem.Text, String))
                End If
            End With

            connection.Open()
            cmdSave1.ExecuteNonQuery()
            con.Close()
            cmdSave1.Dispose()
            ' clear()
            'connection.Close()

            Dim myConnection As New SqlConnection(strcon)
            Dim UDC As New SqlCommand("sp_CreateDuration", myConnection)
            UDC.CommandType = CommandType.StoredProcedure
            With UDC.Parameters
                .AddWithValue("@UserId", strUserid)
                .AddWithValue("@Duration", 90)
                .AddWithValue("@UpdBy", Session("userid"))
                .AddWithValue("@CurrDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
            End With
            myConnection.Open()
            UDC.ExecuteNonQuery()
            myConnection.Close()

            'Save the password to history
            Dim com As New SqlCommand("insert_PWDHistory", myConnection)

            com.CommandType = CommandType.StoredProcedure
            With com.Parameters
                .AddWithValue("@UserID", strUserid)
                .AddWithValue("@Pwd", CrpStr)
                .AddWithValue("@UpdateDate", System.DateTime.Now.ToString())
                .AddWithValue("@UpdatedBy", Session("userid"))
            End With
            myConnection.Open()
            com.ExecuteNonQuery()
            myConnection.Close()
            com.Dispose()

            Dim cmd5 As New SqlCommand("select DATEADD(Day,30,'" + date1 + "') as expiredate", connection)
            Dim dr5 As SqlDataReader
            dr5 = cmd5.ExecuteReader()
            dr5.Read()
            'Code to insert values in InternetProductDemo for purchased product
            Dim da As SqlDataAdapter
            Dim ds As DataSet=New DataSet 
            Dim expire As String
            expire = dr5("expiredate").ToString()
            dr5.Close()
            cmdnew = New SqlCommand("select ProductCode,ProductType,DatabaseType,Database1 from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
            da = New SqlDataAdapter(cmdnew)
            da.Fill(ds)
            code = ds.Tables(0).Rows(0)("ProductCode").ToString()
            producttype = ds.Tables(0).Rows(0)("ProductType").ToString()
            databasetype = ds.Tables(0).Rows(0)("DatabaseType").ToString()
            database = ds.Tables(0).Rows(0)("Database1").ToString()
            Dim cmd4 As New SqlCommand("InternetProductDemoInsert", connection)

            cmd4.CommandType = CommandType.StoredProcedure
            With cmd4.Parameters
                .AddWithValue("@UserId", txtUserId.Text)
                .AddWithValue("@ProductCode", code)
                .AddWithValue("@InsertDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
                .AddWithValue("@EndDate", expire)
                .AddWithValue("@Status", "Active")
                .AddWithValue("@ProductType", producttype)
                .AddWithValue("@DatabaseType", databasetype)
                .AddWithValue("@Database", Database)
            End With
            cmd4.ExecuteNonQuery()
            cmd4.Dispose()

            'code to insert the rights of the newly added user under the superadmin
            Dim dt As String
            Dim i As Integer
            Dim menu As Integer
            Dim cmd6 As New SqlCommand("select Rights from ProductMaster where ProductCode='" + code + "' and DatabaseType='" + databasetype + "' and UserType='" + producttype + "'", connection)
            Dim rdrights As SqlDataReader
            rdrights = cmd6.ExecuteReader
            If rdrights.Read Then
                dt = rdrights("Rights")
            End If
            Dim arrhead() As String = dt.Split(",")
            connection.Close()
            connection.Open()
            For i = 0 To arrhead.Length - 1
                If (arrhead(i) <> 1 And arrhead(i) <> 17) Then
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
                        .AddWithValue("@userid", txtUserId.Text)
                    End With
                    cmdins1.ExecuteNonQuery()
                    cmdins1.Dispose()
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
                                .AddWithValue("@userid", txtUserId.Text)
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
                                .AddWithValue("@userid", txtUserId.Text)
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
                                .AddWithValue("@userid", txtUserId.Text)
                            End With
                            cmdins2.ExecuteNonQuery()
                        Next
                    Else
                        Dim datacmd As New SqlCommand("select * from nlvl_menu where ParentID='" + arrhead(i) + "'", connection)
                        da = New SqlDataAdapter(datacmd)
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
                                .AddWithValue("@userid", txtUserId.Text)
                            End With
                            cmdins2.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next
            WARSShowMsg("Registeration has been completed successfully!!!")
        ElseIf Session("typeofuser") = "User" Then
                WARSShowMsg("User is not allowed to register new employee!!!")
                Exit Sub
        End If
        ''Dim cmdsave2 As New SqlCommand("Insert_Action", connection) 'save data through procedure

    End Sub
    Public Sub clear()

        txtName.Text = ""
        txtEmpId.Text = ""
        txtDesig.Text = ""
        txtEmail.Text = ""
        txtUserId.Text = ""
        txtPwd.Text = ""
        txtConfirmPwd.Text = ""

        cboDesignation.SelectedIndex = 0
        DepartmentName.SelectedIndex = 0
        ClientName.Items.Clear()

        cboLOBDept.Items.Clear()
        CBOBU.SelectedIndex = 0
        chkSelectscope.Checked = False


    End Sub
    Protected Sub btnAvailable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAvailable.Click
        Dim str As String = ""
        If txtUserId.Text = "" Then
            WARSShowMsg("Please enter the Emailid")
        Else
            Dim commId As New SqlCommand("select * from Registration where EMail='" & txtUserId.Text & "'", connection)
            Dim rdrId As SqlDataReader
            connection.Open()
            rdrId = commId.ExecuteReader
            If rdrId.Read Then
                WARSShowMsg("Emailid already exists,Please select another!!!")
                commId.Dispose()
                rdrId.Close()
                connection.Close()
            Else
                WARSShowMsg("Emailid does not exist,You may Proceed!!!")
                commId.Dispose()
                rdrId.Close()
                connection.Close()
            End If
        End If

    End Sub
    Public Function chkEmpId() As Boolean
        Dim str As String = ""
        Dim commId As New SqlCommand("select * from Registration where empId='" & txtEmpId.Text & "'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            WARSShowMsg("Employee already exists,Please select another!!!")
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return True
            Exit Function
        End If

        Return False
    End Function
    Protected Sub cboDesignation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDesignation.SelectedIndexChanged
        If cboDesignation.SelectedIndex = 0 Then
            txtDesig.Enabled = True
        Else
            txtDesig.Enabled = False
            txtDesig.Text = ""
        End If
    End Sub


End Class
