Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_ExcelView
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cbocol11 As String
    Dim cbocol12 As String
    Dim cbocol13 As String
    Dim cbocol14 As String
    Dim cbocol15 As String
    Dim cbocol16 As String
    Dim cbocol17 As String
    Dim cbocol18 As String
    Dim cbocol19 As String
    Dim cbocol20 As String
    Dim cbojoin1 As String
    Dim cbojoin2 As String
    Dim cbojoin3 As String
    Dim cbojoin4 As String
    Dim cbojoin5 As String
    Dim cbojoin6 As String
    Dim cbojoin7 As String
    Dim cbojoin8 As String
    Dim cbojoin9 As String
    Dim cbojoin10 As String
    Dim cbocol21 As String
    Dim cbocol22 As String
    Dim cbocol23 As String
    Dim cbocol24 As String
    Dim cbocol25 As String
    Dim cbocol26 As String
    Dim cbocol27 As String
    Dim cbocol28 As String
    Dim cbocol29 As String
    Dim cbocol30 As String
    Dim cbocolA1 As String
    Dim cbocolA2 As String
    Dim cbocolA3 As String
    Dim cbocolA4 As String
    Dim cbocolA5 As String
    Dim cbocolA6 As String
    Dim cbocolA7 As String
    Dim cbocolA8 As String
    Dim cbocolA9 As String
    Dim cbocolA10 As String
    Dim cbofunc11 As String
    Dim cbofunc12 As String
    Dim cbofunc13 As String
    Dim cbofunc14 As String
    Dim cbofunc15 As String
    Dim cbofunc16 As String
    Dim cbofunc17 As String
    Dim cbofunc18 As String
    Dim cbofunc19 As String
    Dim cbofunc20 As String
    Dim txtval11 As String
    Dim txtval12 As String
    Dim txtval13 As String
    Dim txtval14 As String
    Dim txtval15 As String
    Dim txtval16 As String
    Dim txtval17 As String
    Dim txtval18 As String
    Dim txtval19 As String
    Dim txtval20 As String
    Dim actualformula As String = ""










    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strimg As String = ""
        Dim arrrr = Request("txtcol1")
        strimg = "<img src=../Images/progress2.gif border=0 alt='progress image'><br><img src=../Images/loading.gif border=0 alt='loading image'>"
        divimg.InnerHtml = strimg

        Dim local As String = Request("ViewType")
        Dim i As Integer

        Dim arrform() As String

        Dim formula As String = Request("lstformula")

        Dim arrformula() As String = Split(formula, "$")
        For i = 0 To arrformula.Length - 1
            If (actualformula = "") Then
                actualformula = arrformula(i)
            Else
                actualformula = actualformula + "+" + arrformula(i)
            End If
        Next
        Dim lformula As Integer = actualformula.Length
        If actualformula.EndsWith(",") Then
            actualformula = actualformula.Substring(0, lformula - 1)
        End If

        arrform = Split(actualformula, ",")

        'Begin Join Conditions Values of Formula 
        Dim joinsarr As String = Request("joindata")

        Dim joindata() As String = Split(joinsarr, "$")

        'Assigning values to variables  of array joindata()
        For i = 20 To 29
            If joindata(i) = "<%=str %>" Then
                joindata(i) = ">="
            End If
        Next

        cbocol11 = joindata(0)
        cbocol12 = joindata(1)
        cbocol13 = joindata(2)
        cbocol14 = joindata(3)
        cbocol15 = joindata(4)
        cbocol16 = joindata(5)
        cbocol17 = joindata(6)
        cbocol18 = joindata(7)
        cbocol19 = joindata(8)
        cbocol20 = joindata(9)
        cbocol21 = joindata(10)
        cbocol22 = joindata(11)
        cbocol23 = joindata(12)
        cbocol24 = joindata(13)
        cbocol25 = joindata(14)
        cbocol26 = joindata(15)
        cbocol27 = joindata(16)
        cbocol28 = joindata(17)
        cbocol29 = joindata(18)
        cbocol30 = joindata(19)
        cbojoin1 = joindata(20)
        cbojoin2 = joindata(21)
        cbojoin3 = joindata(22)
        cbojoin4 = joindata(23)
        cbojoin5 = joindata(24)
        cbojoin6 = joindata(25)
        cbojoin7 = joindata(26)
        cbojoin8 = joindata(27)
        cbojoin9 = joindata(28)
        cbojoin10 = joindata(29)


        'End Join 
        'Begin of Conditions Values of Formula 
        Dim conditiondata As String = Request("conditionsdata")

        Dim conditionarr() As String = Split(conditiondata, "$")

        For i = 10 To 29
            If (i < 20) Then
                If conditionarr(i) = "0" Then
                    conditionarr(i) = "--Select--"
                End If
            End If
            If (i > 19) Then
                If conditionarr(i) = "--Select--" Then
                    conditionarr(i) = " "
                End If
            End If

        Next

        'Assigning values to variables of array conditionarr()
        For i = 10 To 19
            If conditionarr(i) = "<%=str %>" Then
                conditionarr(i) = ">="
            End If
        Next

        cbocolA1 = conditionarr(0)
        cbocolA2 = conditionarr(1)
        cbocolA3 = conditionarr(2)
        cbocolA4 = conditionarr(3)
        cbocolA5 = conditionarr(4)
        cbocolA6 = conditionarr(5)
        cbocolA7 = conditionarr(6)
        cbocolA8 = conditionarr(7)
        cbocolA9 = conditionarr(8)
        cbocolA10 = conditionarr(9)
        cbofunc11 = conditionarr(10)
        cbofunc12 = conditionarr(11)
        cbofunc13 = conditionarr(12)
        cbofunc14 = conditionarr(13)
        cbofunc15 = conditionarr(14)
        cbofunc16 = conditionarr(15)
        cbofunc17 = conditionarr(16)
        cbofunc18 = conditionarr(17)
        cbofunc19 = conditionarr(18)
        cbofunc20 = conditionarr(19)
        txtval11 = conditionarr(20)
        txtval12 = conditionarr(21)
        txtval13 = conditionarr(22)
        txtval14 = conditionarr(23)
        txtval15 = conditionarr(24)
        txtval16 = conditionarr(25)
        txtval17 = conditionarr(26)
        txtval18 = conditionarr(27)
        txtval19 = conditionarr(28)
        txtval20 = conditionarr(29)

        'End Condition

        If Request("type") = "create" Then
            Dim query As New System.Text.StringBuilder
            Dim query1 As String = "Create View " & Request("txtname") & " As select "

            ' Dim cols1 As String = Request("lsttab2cols")
            Dim cols1 As String = Request("lsttab2cols")
            If cols1 = "" Then
                Showmsg("Please Select Columns.")
                WinClose()
                Exit Sub
            End If
            '*********************group by changes******************
            Dim cols2 As String = Request("lstgroup")
            Dim arr2 As String = ""
            Dim count As Integer = 0
            If (Request("lsttab2cols") <> "") Then
                count = arr2.Length
            Else
                count = 0
            End If
            '*********************group by changes******************
            Dim arr1() As String
            Dim vcols As String = ""
            Dim vcols1
            arr1 = Split(cols1, ",")
            Dim cnt As Integer = 0

            Dim str As String
            str = Request("cbodept1").ToString() + Request("cbolob1").ToString() + Request("cboclient1").ToString()
            If formula <> "" Then
                If (Request("lsttab2cols") <> "") Then
                    cnt = arrform.Length + arr1.Length
                Else
                    cnt = arrform.Length
                End If

            Else
                If (Request("lsttab2cols") <> "") Then
                    cnt = arr1.Length
                Else
                    cnt = 0
                End If

            End If
            Dim heading As String = ""
            Dim menedia As Integer = 0

            For i = 1 To cnt - 1
                Dim chkdata As String = Trim(Request("txtcol" & i))
                For menedia = 1 To cnt - 1
                    If i <> menedia Then


                        If LCase(chkdata) = LCase(Trim(Request("txtcol" & menedia))) Then
                            Showmsg("Columns Should Have Unique Name.")
                            WinClose()
                            Exit Sub
                        End If
                    End If
                Next

            Next
            For i = 1 To cnt
                If heading <> "" Then
                    heading = heading & "," & Trim(Request("txtcol" & i))
                Else
                    heading = Trim(Request("txtcol" & i))
                End If
            Next
            If (Request("lsttab2cols") = "") Then
                i = cnt
            End If

            If (Request("lsttab2cols") <> "") Then
                For i = 0 To arr1.Length - 2
                    If query.ToString = "" Then
                        query.Append(arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
                    Else
                        query.Append("," & arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
                    End If
                    vcols1 = Split(arr1(i), ".")
                    If vcols = "" Then
                        vcols = vcols1(1)
                    Else
                        vcols = vcols & "," & vcols1(1)
                    End If
                Next
            End If

            Dim j As Integer = 0
            Dim cnt1 = 1
            Dim strFor As String = ""




            ''*****************changes made by vini on 20/11/07****************************************************************
            If formula <> "" Then

                If (Request("lsttab2cols") <> "") Then

                    For j = 0 To arrform.Length - 1
                        i = i + 1
                        strFor = arrform(j)
                        arrform(j) = Replace(strFor, "~~", ",")
                        query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
                        cnt1 = cnt1 + 1

                    Next
                ElseIf (Request("lsttab2cols") = "") Then
                    i = 1
                    For j = 0 To arrform.Length - 1

                        strFor = arrform(j)
                        arrform(j) = Replace(strFor, "~~", ",")
                        If j = 0 Then
                            query.Append(arrform(j) & " as " & Request("txtcol" & i) & " ")
                        Else
                            query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
                        End If
                        i = i + 1
                        cnt1 = cnt1 + 1
                    Next

                End If
            End If
            ''**************************End Changes***************************************************************************
            query1 = query1 & query.ToString & "from "
            'Dim arrtab = Split(Request("lsttab2"), "$")
            'j = 0
            'Dim tab As String = ""
            'Dim arrtab1
            'For j = 0 To arrtab.length - 1
            '    If j > 0 Then
            '        arrtab1 = Split(arrtab(j), ",")
            '        arrtab(j) = arrtab1(0)
            '        If tab = "" Then
            '            tab = arrtab(j)
            '        Else
            '            tab = tab & "," & arrtab(j)
            '        End If
            '    End If
            'Next
            'query1 = query1 & tab
            Dim tab As String = Request("lsttab2").ToString()
            query1 = query1 & tab
            Dim qry1 As String = ""
            If cbocol11 <> "--Select--" And cbojoin1 <> "--Select--" And cbocol21 <> "--Select--" Then
                query1 = query1 & " where " & cbocol11 & cbojoin1 & cbocol21
                qry1 = cbocol11 & "$##$" & cbojoin1 & "$##$" & cbocol21
            End If
            If cbocol12 <> "--Select--" And cbojoin2 <> "--Select--" And cbocol22 <> "--Select--" Then
                query1 = query1 & " and " & cbocol12 & cbojoin2 & cbocol22
                qry1 = qry1 & "," & cbocol12 & "$##$" & cbojoin2 & "$##$" & cbocol22
            End If
            If cbocol13 <> "--Select--" And cbojoin3 <> "--Select--" And cbocol23 <> "--Select--" Then
                query1 = query1 & " and " & cbocol13 & cbojoin3 & cbocol23
                qry1 = qry1 & "," & cbocol13 & "$##$" & cbojoin3 & "$##$" & cbocol23
            End If
            If cbocol14 <> "--Select--" And cbojoin4 <> "--Select--" And cbocol24 <> "--Select--" Then
                query1 = query1 & " and " & cbocol14 & cbojoin4 & cbocol24
                qry1 = qry1 & "," & cbocol14 & "$##$" & cbojoin4 & "$##$" & cbocol24
            End If
            If cbocol15 <> "--Select--" And cbojoin5 <> "--Select--" And cbocol25 <> "--Select--" Then
                query1 = query1 & " and " & cbocol15 & cbojoin5 & cbocol25
                qry1 = qry1 & "," & cbocol15 & "$##$" & cbojoin5 & "$##$" & cbocol25
            End If
            If cbocol16 <> "--Select--" And cbojoin6 <> "--Select--" And cbocol26 <> "--Select--" Then
                query1 = query1 & " and " & cbocol16 & cbojoin6 & cbocol26
                qry1 = qry1 & "," & cbocol16 & "$##$" & cbojoin6 & "$##$" & cbocol26
            End If
            If cbocol17 <> "--Select--" And cbojoin7 <> "--Select--" And cbocol27 <> "--Select--" Then
                query1 = query1 & " and " & cbocol17 & cbojoin7 & cbocol27
                qry1 = qry1 & "," & cbocol17 & "$##$" & cbojoin7 & "$##$" & cbocol27
            End If
            If cbocol18 <> "--Select--" And cbojoin8 <> "--Select--" And cbocol28 <> "--Select--" Then
                query1 = query1 & " and " & cbocol18 & cbojoin8 & cbocol28
                qry1 = qry1 & "," & cbocol18 & "$##$" & cbojoin8 & "$##$" & cbocol28
            End If
            If cbocol19 <> "--Select--" And cbojoin9 <> "--Select--" And cbocol29 <> "--Select--" Then
                query1 = query1 & " and " & cbocol19 & cbojoin9 & cbocol29
                qry1 = qry1 & "," & cbocol19 & "$##$" & cbojoin9 & "$##$" & cbocol29
            End If
            If cbocol20 <> "--Select--" And cbojoin10 <> "--Select--" <> 0 And cbocol30 <> "--Select--" Then
                query1 = query1 & " and " & cbocol20 & cbojoin10 & cbocol30
                qry1 = qry1 & "," & cbocol20 & "$##$" & cbojoin10 & "$##$" & cbocol30
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry As String = ""
            If cbocolA1 <> "--Select--" And cbofunc11 <> "--Select--" And txtval11 <> "" Then
                If cbocol11 <> "--Select--" And cbojoin1 <> "--Select--" And cbocol21 <> "--Select--" Then
                    query1 = query1 & " and " & cbocolA1 & " " & cbofunc11 & " '" & txtval11 & "'"
                Else
                    query1 = query1 & " where " & cbocolA1 & " " & cbofunc11 & " '" & txtval11 & "'"
                End If
                qry = cbocolA1 & "$##$" & cbofunc11 & "$##$'" & txtval11 & "'"
            End If
            If cbocolA2 <> "--Select--" And cbofunc12 <> "--Select--" And txtval12 <> "" Then
                query1 = query1 & " and " & cbocolA2 & " " & cbofunc12 & " '" & txtval12 & "'"
                qry = qry & "," & cbocolA2 & "$##$" & cbofunc12 & "$##$'" & txtval12 & "'"
            End If
            If cbocolA3 <> "--Select--" And cbofunc13 <> "--Select--" And txtval13 <> "" Then
                query1 = query1 & " and " & cbocolA3 & " " & cbofunc13 & " '" & txtval13 & "'"
                qry = qry & "," & cbocolA3 & "$##$" & cbofunc13 & "$##$'" & txtval13 & "'"
            End If
            If cbocolA4 <> "--Select--" And cbofunc14 <> "--Select--" And txtval14 <> "" Then
                query1 = query1 & " and " & cbocolA4 & " " & cbofunc14 & " '" & txtval14 & "'"
                qry = qry & "," & cbocolA4 & "$##$" & cbofunc14 & "$##$'" & txtval14 & "'"
            End If
            If cbocolA5 <> "--Select--" And cbofunc15 <> "--Select--" And txtval15 <> "" Then
                query1 = query1 & " and " & cbocolA5 & " " & cbofunc15 & " '" & txtval15 & "'"
                qry = qry & "," & cbocolA5 & "$##$" & cbofunc15 & "$##$'" & txtval15 & "'"
            End If
            If cbocolA6 <> "--Select--" And cbofunc16 <> "--Select--" And txtval16 <> "" Then
                query1 = query1 & " and " & cbocolA6 & " " & cbofunc16 & " '" & txtval16 & "'"
                qry = qry & "," & cbocolA6 & "$##$" & cbofunc16 & "$##$'" & txtval16 & "'"
            End If
            If cbocolA7 <> "--Select--" And cbofunc17 <> "--Select--" And txtval17 <> "" Then
                query1 = query1 & " and " & cbocolA7 & " " & cbofunc17 & " '" & txtval17 & "'"
                qry = qry & "," & cbocolA7 & "$##$" & cbofunc17 & "$##$'" & txtval17 & "'"
            End If
            If cbocolA8 <> "--Select--" And cbofunc18 <> "--Select--" And txtval18 <> "" Then
                query1 = query1 & " and " & cbocolA8 & " " & cbofunc18 & " '" & txtval18 & "'"
                qry = qry & "," & cbocolA8 & "$##$" & cbofunc18 & "$##$'" & txtval18 & "'"
            End If
            If cbocolA9 <> "--Select--" And cbofunc19 <> "--Select--" And txtval19 <> "" Then
                query1 = query1 & " and " & cbocolA9 & " " & cbofunc19 & " '" & txtval19 & "'"
                qry = qry & "," & cbocolA9 & "$##$" & cbofunc19 & "$##$'" & txtval19 & "'"
            End If
            If cbocolA10 <> "--Select--" And cbofunc20 <> "--Select--" And txtval20 <> "" Then
                query1 = query1 & " and " & cbocolA10 & " " & cbofunc20 & " '" & txtval20 & "'"
                qry = qry & "," & cbocolA10 & "$##$" & cbofunc20 & "$##$'" & txtval20 & "'"
            End If
            If Request("groupby") <> " group by" Then
                query1 = query1 & Request("groupby")
            End If
            Dim s As String = Request("groupby")
            'Response.Write(query1)
            Try
                Dim cmddata As New SqlCommand(query1, connection)
                connection.Open()
                cmddata.ExecuteNonQuery()
                connection.Close()
                cmddata.Dispose()
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Showmsg(strmsg)
                WinClose()
                Exit Sub
            End Try
            Dim lob
            Dim client
            If Request("cbolob1") = "" Or Request("cbolob1") = "--Select--" Then
                lob = 0
            Else
                lob = Request("cbolob1")
            End If
            If Request("cboclient1") = "" Or Request("cboclient1") = "--Select--" Then
                client = 0
            Else
                client = Request("cboclient1")
            End If
            'Try
            '    Dim cmdins As New SqlCommand("insert into warslobtablemaster(LOBId,TableName,CreatedOn,CreatedBy,LastModified,LastModifiedBy,Currdate,Visiblecolumn,Editable,Importable,DepartmentId,ClientId) values('" & lob & "','" & Request("txtname") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & System.DateTime.Now & "','" & heading & "','No','No','" & Request("cbodept1") & "','" & client & "')", connection)
            '    connection.Open()
            '    cmdins.ExecuteNonQuery()
            '    connection.Close()
            '    cmdins.Dispose()

            'Catch ex As Exception
            '    Dim strmsg As String
            '    strmsg = Replace(ex.Message.ToString, "'", "")
            '    strmsg = Replace(strmsg, vbCrLf, " ")
            '    Showmsg(strmsg)
            'End Try
            qry = Trim(Replace(qry, "'", "$#$"))
            qry1 = Trim(qry1)
            'Dim tab As String = Request("lsttab2").ToString()
            Try
                If formula <> "" Then
                    formula = formula.Replace("'", "$#$")
                End If
                Dim cmdinsview As New SqlCommand("insert into idmsviewmaster(ViewName,Colname,TableName,WhereDataJoin,WhereDataCon,DeptId,ClientId,LobId,LocalView,Createdate,CreatedBy,ModifiedBy,Modifydate,Headings,GroupBy,Formula)  values(@vname, @cols ,@tab,@qry1,@qry,'" & Request("cbodept1") & "','" & client & "','" & lob & "','" & local & "','" & System.DateTime.Now & "','" & Session("userid") & "','" & Session("userid") & "','" & System.DateTime.Now & "',@hd,@gp,@form)", connection)
                cmdinsview.Parameters.AddWithValue("@vname", Request("txtname"))
                cmdinsview.Parameters.AddWithValue("@cols", cols1)
                cmdinsview.Parameters.AddWithValue("@tab", tab)
                cmdinsview.Parameters.AddWithValue("@qry1", qry1)
                cmdinsview.Parameters.AddWithValue("@qry", qry)
                cmdinsview.Parameters.AddWithValue("@form", actualformula)
                cmdinsview.Parameters.AddWithValue("@hd", heading)
                cmdinsview.Parameters.AddWithValue("@gp", Request("groupby"))
                connection.Open()
                cmdinsview.ExecuteNonQuery()
                connection.Close()
                cmdinsview.Dispose()
                'saving  default right of user on view
                'Dim cmdgetid As New SqlCommand("select viewid from idmsviewmaster where ViewName = '" & Request("txtname") & "'", connection)
                'connection.Open()
                'Dim viewid As Integer = CType(cmdgetid.ExecuteScalar, Integer)
                'Dim strquery As String = "insert into  idms_viewrights(viewid,userid,view,edit,delete,saveas,currdate,assignedby) values" & "('" & viewid & "','" & Session("userid") & "','true','true','true','true','" & System.DateTime.Now & "','ByDefault')"
                'Dim cmdSaveRight As New SqlCommand("insert into  idms_viewrights(viewid,userid,[view],edit,[delete],saveas,currdate,assignedby) values('" & viewid & "','" & Session("userid") & "','true','true','true','true','" & System.DateTime.Now & "','ByDefault')", connection)
                'cmdSaveRight.ExecuteNonQuery()
                'connection.Close()
                'cmdSaveRight.Dispose()
                'end
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                'Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & Request("txtname") & "' and Action='Create'", connection)
                'connection.Open()
                'cmm.ExecuteNonQuery()
                'connection.Close()

                ''changes done by smitha to add details 
                'Dim cmm1 As New SqlCommand("insert into logDataTransferSlave select MAX(Autoid),'" + Request("viewname") + "  is Saved as ', '" + Request("txtname") + " '  from logDataTransferMaster where EntityName='" & Request("txtname") & "' and Action='Create'", connection)
                'connection.Open()
                'cmm1.ExecuteNonQuery()
                'connection.Close()
                ''changes done by smitha to add details 
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Showmsg(strmsg)
            End Try
            Showmsg("View has been created successfully.")
            WinClose()


            '*********************Update Code*************************

        ElseIf Request("type") = "update" Then
            Dim query As New System.Text.StringBuilder
            Dim query1 As String = "Alter View " & Request("viewname") & " As select "
            ' Dim i As Integer
            Dim cols1 As String = Request("lsttab2cols")
            Dim arr1() As String
            Dim vcols As String = ""
            Dim vcols1
            arr1 = Split(cols1, ",")
            Dim cnt As Integer = 0
            '  Dim formula As String = Request("lstformula")
            '  Dim arrform = Split(formula, ",")

            If formula <> "" Then
                If (Request("lsttab2cols") <> "") Then
                    cnt = arrform.Length + arr1.Length
                Else
                    cnt = arrform.Length
                End If

            Else
                If (Request("lsttab2cols") <> "") Then
                    cnt = arr1.Length
                Else
                    cnt = 0
                End If

            End If

            Dim heading As String = ""
            For i = 1 To cnt
                If heading <> "" Then
                    heading = heading & "," & Trim(Request("txtcol" & i))
                Else
                    heading = Trim(Request("txtcol" & i))
                End If
            Next
            If (Request("lsttab2cols") = "") Then
                i = cnt
            End If
            If (Request("lsttab2cols") <> "") Then
                For i = 0 To arr1.Length - 2
                    If query.ToString = "" Then
                        query.Append(arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
                    Else
                        query.Append("," & arr1(i) & " as " & Request("txtcol" & i + 1) & " ")
                    End If

                    vcols1 = Split(arr1(i), ".")
                    If vcols = "" Then
                        vcols = vcols1(1)
                    Else
                        vcols = vcols & "," & vcols1(1)
                    End If
                Next
            End If
            Dim j As Integer = 0
            Dim cnt1 = 1
            Dim strFor As String = ""
            ''************************************original/old***********************************************
            ''If formula <> "" Then
            ''    For j = 0 To arrform.length - 1
            ''        i = i + 1
            ''        If (Request("lsttab2cols") = "") Then
            ''            i = i - 1
            ''        End If
            ''        strFor = arrform(j)
            ''        arrform(j) = Replace(strFor, "~~", ",")
            ''        If (Request("lsttab2cols") <> "") Then
            ''            query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
            ''        Else
            ''            query.Append(arrform(j) & " as " & Request("txtcol" & i) & " ")
            ''        End If
            ''        'query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
            ''        cnt1 = cnt1 + 1
            ''    Next
            ''End If
            ''*****************************************changes made by vini****************************************
            If formula <> "" Then

                If (Request("lsttab2cols") <> "") Then

                    For j = 0 To arrform.Length - 1
                        i = i + 1
                        strFor = arrform(j)
                        If (strFor <> "") Then
                            arrform(j) = Replace(strFor, "~~", ",")
                            query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
                            cnt1 = cnt1 + 1
                        End If


                    Next
                ElseIf (Request("lsttab2cols") = "") Then
                    i = 1
                    Dim s As String = Request("lsttab2cols").ToString()
                    For j = 0 To arrform.Length - 1

                        strFor = arrform(j)
                        arrform(j) = Replace(strFor, "~~", ",")
                        If j = 0 Then
                            query.Append(arrform(j) & " as " & Request("txtcol" & i) & " ")
                        Else
                            query.Append("," & arrform(j) & " as " & Request("txtcol" & i) & " ")
                        End If
                        i = i + 1
                        cnt1 = cnt1 + 1
                    Next

                End If
            End If
            ''**************************End Changes***************************************************************************
            query1 = query1 & query.ToString & "from "
            'Dim arrtab = Split(Request("lsttab2"), "$")
            'j = 0
            Dim tab As String = ""
            'Dim arrtab1
            'For j = 0 To arrtab.length - 1
            '    If j > 0 Then
            '        arrtab1 = Split(arrtab(j), ",")
            '        arrtab(j) = arrtab1(0)
            '        If tab = "" Then
            '            tab = arrtab(j)
            '        Else
            '            tab = tab & "," & arrtab(j)
            '        End If
            '    End If
            'Next
            ' query1 = query1 & tab
            query1 = query1 & Request("lsttab2")
            'Replace("<%=str1 %>", " >= ") in below code added by atul to remove bug

            Dim qry1 As String = ""
            If cbocol11 <> "--Select--" And cbojoin1 <> "--Select--" And cbocol21 <> "--Select--" Then
                query1 = query1 & " where " & cbocol11 & cbojoin1.Replace("<%=str1 %>", " >= ") & cbocol21
                qry1 = cbocol11 & "$##$" & cbojoin1.Replace("<%=str1 %>", ">=") & "$##$" & cbocol21
            End If
            If cbocol12 <> "--Select--" And cbojoin2 <> "--Select--" And cbocol22 <> "--Select--" Then
                query1 = query1 & " and " & cbocol12 & cbojoin2.Replace("<%=str1 %>", " >= ") & cbocol22
                qry1 = qry1 & "," & cbocol12 & "$##$" & cbojoin2.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol22
            End If
            If cbocol13 <> "--Select--" And cbojoin3 <> "--Select--" And cbocol23 <> "--Select--" Then
                query1 = query1 & " and " & cbocol13 & cbojoin3.Replace("<%=str1 %>", " >= ") & cbocol23
                qry1 = qry1 & "," & cbocol13 & "$##$" & cbojoin3.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol23
            End If
            If cbocol14 <> "--Select--" And cbojoin4 <> "--Select--" And cbocol24 <> "--Select--" Then
                query1 = query1 & " and " & cbocol14 & cbojoin4.Replace("<%=str1 %>", " >= ") & cbocol24
                qry1 = qry1 & "," & cbocol14 & "$##$" & cbojoin4.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol24
            End If
            If cbocol15 <> "--Select--" And cbojoin5 <> "--Select--" And cbocol25 <> "--Select--" Then
                query1 = query1 & " and " & cbocol15 & cbojoin5.Replace("<%=str1 %>", " >= ") & cbocol25
                qry1 = qry1 & "," & cbocol15 & "$##$" & cbojoin5.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol25
            End If
            If cbocol16 <> "--Select--" And cbojoin6 <> "--Select--" And cbocol26 <> "--Select--" Then
                query1 = query1 & " and " & cbocol16 & cbojoin6.Replace("<%=str1 %>", " >= ") & cbocol26
                qry1 = qry1 & "," & cbocol16 & "$##$" & cbojoin6.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol26
            End If
            If cbocol17 <> "--Select--" And cbojoin7 <> "--Select--" And cbocol27 <> "--Select--" Then
                query1 = query1 & " and " & cbocol17 & cbojoin7.Replace("<%=str1 %>", " >= ") & cbocol27
                qry1 = qry1 & "," & cbocol17 & "$##$" & cbojoin7.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol27
            End If
            If cbocol18 <> "--Select--" And cbojoin8 <> "--Select--" And cbocol28 <> "--Select--" Then
                query1 = query1 & " and " & cbocol18 & cbojoin8.Replace("<%=str1 %>", " >= ") & cbocol28
                qry1 = qry1 & "," & cbocol18 & "$##$" & cbojoin8.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol28
            End If
            If cbocol19 <> "--Select--" And cbojoin9 <> "--Select--" And cbocol29 <> "--Select--" Then
                query1 = query1 & " and " & cbocol19 & cbojoin9.Replace("<%=str1 %>", " >= ") & cbocol29
                qry1 = qry1 & "," & cbocol19 & "$##$" & cbojoin9.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol29
            End If
            If cbocol20 <> "--Select--" And cbojoin10 <> "--Select--" <> 0 And cbocol30 <> "--Select--" Then
                query1 = query1 & " and " & cbocol20 & cbojoin10.Replace("<%=str1 %>", " >= ") & cbocol30
                qry1 = qry1 & "," & cbocol20 & "$##$" & cbojoin10.Replace("<%=str1 %>", " >= ") & "$##$" & cbocol30
            End If



            ''''''''''''''''''''''''''''''''''''''''''''''''''end---------

            '-------------**************start*************
            Dim qry As String = ""
            If cbocolA1 <> "--Select--" And cbofunc11 <> "--Select--" And txtval11 <> "" Then
                If cbocol11 <> "--Select--" And cbojoin1 <> "--Select--" And cbocol21 <> "--Select--" Then
                    query1 = query1 & " and " & cbocolA1 & " " & cbofunc11.Replace("<%=str1 %>", ">=") & " '" & txtval11 & "'"
                Else
                    query1 = query1 & " where " & cbocolA1 & " " & cbofunc11.Replace("<%=str1 %>", ">=") & " '" & txtval11 & "'"
                End If
                qry = cbocolA1 & "$##$" & cbofunc11 & "$##$'" & txtval11 & "'"
            End If
            If cbocolA2 <> "--Select--" And cbofunc12 <> "--Select--" And txtval12 <> "" Then
                query1 = query1 & " and " & cbocolA2 & " " & cbofunc12.Replace("<%=str1 %>", ">=") & " '" & txtval12 & "'"
                qry = qry & "," & cbocolA2 & "$##$" & cbofunc12.Replace("<%=str1 %>", ">=") & "$##$'" & txtval12 & "'"
            End If
            If cbocolA3 <> "--Select--" And cbofunc13 <> "--Select--" And txtval13 <> "" Then
                query1 = query1 & " and " & cbocolA3 & " " & cbofunc13.Replace("<%=str1 %>", ">=") & " '" & txtval13 & "'"
                qry = qry & "," & cbocolA3 & "$##$" & cbofunc13.Replace("<%=str1 %>", ">=") & "$##$'" & txtval13 & "'"
            End If
            If cbocolA4 <> "--Select--" And cbofunc14 <> "--Select--" And txtval14 <> "" Then
                query1 = query1 & " and " & cbocolA4 & " " & cbofunc14.Replace("<%=str1 %>", ">=") & " '" & txtval14 & "'"
                qry = qry & "," & cbocolA4 & "$##$" & cbofunc14.Replace("<%=str1 %>", ">=") & "$##$'" & txtval14 & "'"
            End If
            If cbocolA5 <> "--Select--" And cbofunc15 <> "--Select--" And txtval15 <> "" Then
                query1 = query1 & " and " & cbocolA5 & " " & cbofunc15.Replace("<%=str1 %>", ">=") & " '" & txtval15 & "'"
                qry = qry & "," & cbocolA5 & "$##$" & cbofunc15.Replace("<%=str1 %>", ">=") & "$##$'" & txtval15 & "'"
            End If
            If cbocolA6 <> "--Select--" And cbofunc16 <> "--Select--" And txtval16 <> "" Then
                query1 = query1 & " and " & cbocolA6 & " " & cbofunc16.Replace("<%=str1 %>", ">=") & " '" & txtval16 & "'"
                qry = qry & "," & cbocolA6 & "$##$" & cbofunc16.Replace("<%=str1 %>", ">=") & "$##$'" & txtval16 & "'"
            End If
            If cbocolA7 <> "--Select--" And cbofunc17 <> "--Select--" And txtval17 <> "" Then
                query1 = query1 & " and " & cbocolA7 & " " & cbofunc17.Replace("<%=str1 %>", ">=") & " '" & txtval17 & "'"
                qry = qry & "," & cbocolA7 & "$##$" & cbofunc17.Replace("<%=str1 %>", ">=") & "$##$'" & txtval17 & "'"
            End If
            If cbocolA8 <> "--Select--" And cbofunc18 <> "--Select--" And txtval18 <> "" Then
                query1 = query1 & " and " & cbocolA8 & " " & cbofunc18.Replace("<%=str1 %>", ">=") & " '" & txtval18 & "'"
                qry = qry & "," & cbocolA8 & "$##$" & cbofunc18.Replace("<%=str1 %>", ">=") & "$##$'" & txtval18 & "'"
            End If
            If cbocolA9 <> "--Select--" And cbofunc19 <> "--Select--" And txtval19 <> "" Then
                query1 = query1 & " and " & cbocolA9 & " " & cbofunc19.Replace("<%=str1 %>", ">=") & " '" & txtval19 & "'"
                qry = qry & "," & cbocolA9 & "$##$" & cbofunc19.Replace("<%=str1 %>", ">=") & "$##$'" & txtval19 & "'"
            End If
            If cbocolA10 <> "--Select--" And cbofunc20 <> "--Select--" And txtval20 <> "" Then
                query1 = query1 & " and " & cbocolA10 & " " & cbofunc20.Replace("<%=str1 %>", ">=") & " '" & txtval20 & "'"
                qry = qry & "," & cbocolA10 & "$##$" & cbofunc20.Replace("<%=str1 %>", ">=") & "$##$'" & txtval20 & "'"
            End If

            If Request("groupby") <> " group by" Then
                query1 = query1 & Request("groupby")
            End If

            '--************************end*********

            Try
                tab = Request("lsttab2").ToString()
                Dim cmddata As New SqlCommand(query1, connection)
                connection.Open()
                cmddata.ExecuteNonQuery()
                connection.Close()
                cmddata.Dispose()
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Showmsg(strmsg)
                'divimg.InnerHtml = ""
                WinClose()
                Exit Sub
            End Try
            Dim lob
            Dim client
            If Request("cbolob1") = "" Or Request("cbolob1") = "--Select--" Then
                lob = 0
            Else
                lob = Request("cbolob1")
            End If
            If Request("cboclient1") = "" Or Request("cboclient1") = "--Select--" Then
                client = 0
            Else
                client = Request("cboclient1")
            End If


            Try
                Dim cmdupd As New SqlCommand("update warslobtablemaster set LOBId='" & lob & "',LastModified='" & System.DateTime.Now & "',LastModifiedBy='" & Session("userid") & "',VisibleColumn='" & heading & "',DepartmentId='" & Request("cbodept1") & "',ClientId='" & client & "' where TableName='" & Request("lblname1") & "'", connection)
                connection.Open()
                cmdupd.ExecuteNonQuery()
                connection.Close()
                cmdupd.Dispose()
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Showmsg(strmsg)
            End Try
            qry = Trim(Replace(qry, "'", "$#$"))
            qry1 = Trim(qry1)
            Try
                If formula <> "" Then
                    formula = formula.Replace("'", "$#$")
                End If
                Dim cmdupdview As New SqlCommand("update idmsviewmaster set colname=@cols1,tablename=@tab,wheredatajoin=@qry1,wheredatacon=@qry,DeptId='" & Request("cbodept1") & "',ClientId='" & client & "',LOBId='" & lob & "',ModifiedBy='" & Session("userid") & "',ModifyDate='" & System.DateTime.Now & "',headings=@heading,groupby=@gp,formula=@actualformula where viewname='" & Request("viewname") & "'", connection)
                cmdupdview.Parameters.AddWithValue("@cols1", cols1)
                cmdupdview.Parameters.AddWithValue("@tab", tab)
                cmdupdview.Parameters.AddWithValue("@qry1", qry1)
                cmdupdview.Parameters.AddWithValue("@qry", qry)
                cmdupdview.Parameters.AddWithValue("@heading", heading)
                cmdupdview.Parameters.AddWithValue("@gp", Request("groupby"))
                cmdupdview.Parameters.AddWithValue("@actualformula", actualformula)
                connection.Open()
                cmdupdview.ExecuteNonQuery()
                connection.Close()
                cmdupdview.Dispose()
                '''''Track changes:By Suvidha
                Dim cmd As SqlCommand
                cmd = New SqlCommand("DataTransferOnIDMSViewMaster", connection)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@ActionBy", SqlDbType.VarChar, 100).Value = Session("userid")
                cmd.Parameters.Add("@Action", SqlDbType.VarChar, 50).Value = "Edit"
                cmd.Parameters.Add("@Date", SqlDbType.VarChar, 50).Value = System.DateTime.Now()
                cmd.Parameters.Add("@Entity", SqlDbType.VarChar, 50).Value = "View"
                cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 50).Value = Request("viewname")
                cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50).Value = Request("cbodept1")
                cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50).Value = client
                cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50).Value = lob
                connection.Open()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                connection.Close()
                '''''Track changes:By Suvidha
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & Request("viewname") & "' and Action='Edit'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()

                ''changes done by smitha to add details 
                Dim cmm1 As New SqlCommand("insert into logDataTransferSlave select MAX(Autoid),'Action ', ' View got Updated'  from logDataTransferMaster where EntityName='" & Request("viewname") & "' and Action='Edit'", connection)
                connection.Open()
                cmm1.ExecuteNonQuery()
                connection.Close()
                ''changes done by smitha to add details 
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Showmsg(strmsg)
            End Try

            Showmsg("View has been updated successfully.")
            WinClose()
        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub
    Public Sub WinClose()
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")

        str.Append("window.parent.focus();")
        str.Append(" window.self.close();")
        str.Append("</Script>")
        RegisterStartupScript("WinClose", str.ToString)
    End Sub
    Public Sub Redirect(ByVal recid)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("redirect('" + recid + "')")
        str.Append("</Script>")
        RegisterStartupScript("Redirect", str.ToString)
    End Sub
    Public Sub AssignValues()

    End Sub

End Class

