Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings

Public Class TempAjaxClass2

    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim conn1 As New SqlConnection(constr)
    Dim conn2 As New SqlConnection(constr)
    Dim conn3 As New SqlConnection(constr)

    <Ajax.AjaxMethod()> Public Function Show(ByVal Process As String, ByVal LOB As String, ByVal CBOADD As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim rdrtarge
        Dim commId1 As New SqlCommand("select distinct Target from tempIDMSAddition_Prodn_Vol where ProcessName='" + Process + "' and LOBName='" + LOB + "' and additional_vol_by='" + CBOADD + "' and (Target is not null)", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader
        If rdrId1.Read Then
            rdrtarge = rdrId1("Target")
            Return (rdrtarge)
        Else
            rdrtarge = 0.0
            Return (rdrtarge)
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()

    End Function

    'Function for saving the process
    <Ajax.AjaxMethod()> Public Function SavePro(ByVal Process, ByVal Description) As String
        Dim msg As String
        Dim bx1 As String
        Try
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTProcess", connection)
            cmdinsert.CommandType = CommandType.StoredProcedure
            With cmdinsert.Parameters()
                .Add("@Process", Trim(Process))
                .Add("@Description", Trim(Description))
                .Add("@CreatedOn", System.DateTime.Now)
                .Add("@CreatedBy", "ash")
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            msg = "Data inserted Successfully"
            Return (msg)
        Catch ex As Exception
            msg = "Data Not Inserted Successfully!"
            Return (msg)
        End Try
    End Function
    'Function for saving the BLT
    <Ajax.AjaxMethod()> Public Function SaveLob(ByVal ProcessId, ByVal Lob, ByVal Description) As String
        Dim msg As String
        Try

            Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTLobs", connection)
            cmdinsert.CommandType = CommandType.StoredProcedure
            With cmdinsert.Parameters()
                .Add("@Lob", Trim(Lob))
                .Add("@Description", Trim(Description))
                .Add("@ProcessId", Trim(ProcessId))
                .Add("@CreatedOn", System.DateTime.Now)
                .Add("@CreatedBy", "om")
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            msg = "Data inserted Successfully"
            Return (msg)
        Catch ex As Exception
            msg = "Data Not Inserted Successfully!"
            Return (msg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function AddItemList(ByVal struserid As String) As String
        Dim i As Integer = 0
        Dim strPWD As String = ""
        ' Dim uid = Session("userid")
        Dim querySettType As String = "select tablename from warslobtablemaster where lobid='" & struserid & "' and Editable='Yes' "
        Dim objCmdSettType As New SqlCommand
        Dim adpSettType As New SqlDataAdapter
        Dim dsSettType As New DataSet
        objCmdSettType.Connection = connection
        objCmdSettType.CommandText = querySettType
        adpSettType.SelectCommand = objCmdSettType
        connection.Open()
        adpSettType.Fill(dsSettType, "warslobtablemaster")
        connection.Close()
        'strPWD = "Daksh@123"
        For i = 0 To dsSettType.Tables(0).Rows.Count - 1
            ' strSettType = dsSettType.Tables(0).Rows.Item(i).Item("sett_type").ToString()
            If Trim(strPWD) <> "" Then
                strPWD = strPWD & "$" & dsSettType.Tables(0).Rows.Item(i).Item("pwd").ToString()
            Else
                strPWD = dsSettType.Tables(0).Rows.Item(i).Item("pwd").ToString()
            End If
        Next

        Return Trim(strPWD)
    End Function
    <Ajax.AjaxMethod()> Public Function BindCombo(ByVal struserid As String) As String
        Dim i As Integer = 0
        Dim strname As String = ""
        Dim cmd As New SqlCommand("select tablename from warslobtablemaster where lobid='" & struserid & "' and Editable='Yes' ", connection)
        Dim adp As New SqlDataAdapter
        Dim ds As New DataSet
        adp.SelectCommand = cmd
        connection.Open()
        adp.Fill(ds, "warslobtablemaster")
        cmd.Dispose()
        connection.Close()
        Dim c As Integer = 0
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If Trim(strname) <> "" Then
                strname = strname & "$" & ds.Tables(0).Rows.Item(i).Item("tablename").ToString()
                c = c + 1
            Else
                strname = ds.Tables(0).Rows.Item(i).Item("tablename").ToString()
                c = c + 1
            End If
        Next
        If c = 0 Then
            strname = ""
        End If
        Return Trim(strname)
    End Function
    <Ajax.AjaxMethod()> Public Function bindtable(ByVal LOBId) As String

        Dim i As Integer = 0
        Dim strname As String = ""
        Dim adp As New SqlDataAdapter("select tablename from warslobtablemaster where LOBId=" & LOBId & " Editable=yes", connection)
        Dim ds As New DataSet
        connection.Open()
        adp.Fill(ds, "warslobtablemaster")

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If Trim(strname) <> "" Then
                strname = strname & "$" & ds.Tables(0).Rows.Item(i).Item("tablename").ToString()
            Else
                strname = ds.Tables(0).Rows.Item(i).Item("tablename").ToString()
            End If
        Next
        Return Trim(strname)
        connection.Close()

    End Function
    <Ajax.AjaxMethod()> Public Function bindlobtable(ByVal id) As String
        'Show the table name 
        Dim lobtable As String
        Dim lobfield As String
        Dim tablename As String = ""
        Dim datafieldstring As String
        Dim strQryPrgms As String
        Dim i As Integer
        Dim str1 As String
        lobtable = "select tablename from warslobtablemaster where lobid=' " & id & " ' order by lobid"
        Dim selectcmd As New SqlCommand(lobtable, conn1)
        Dim lobreader As SqlDataReader
        Dim lobreader1 As SqlDataReader
        Dim str2 As String
        conn1.Open()
        lobreader = selectcmd.ExecuteReader
        tablename = ""
        While lobreader.Read()
            If Trim(tablename) = "" Then
                tablename = lobreader("tablename")
            Else
                tablename = tablename & "$" & lobreader("tablename")
            End If
        End While
        lobreader.Close()

        conn1.Close()
        Return (tablename)
    End Function
    'show the field name on the selection of table buttons
    <Ajax.AjaxMethod()> Public Function bindlobField(ByVal tablename) As String
        Dim lobfield As String
        Dim fieldname As String
        Dim datafieldstring As String
        Dim i As Integer
        Dim str1 As String = ""
        Dim strQryPrgms As String
        lobfield = "select visiblecolumn from warslobtablemaster where tablename = '" & Trim(tablename) & "'"
        Dim selectcmd As New SqlCommand(lobfield, conn1)
        Dim lobreader As SqlDataReader
        conn1.Open()
        lobreader = selectcmd.ExecuteReader
        If lobreader.Read Then
            str1 = lobreader("visiblecolumn")
        End If
        lobreader.Close()
        conn1.Close()
        If str1 <> "" Then
            Return (str1)
        Else
            str1 = "0"
            Return (str1)
        End If
    End Function

    <Ajax.AjaxMethod()> Public Function chk(ByVal BatchName, ByVal Month, ByVal Year) As String
        Dim bx As String
        Dim cmd As New SqlCommand
        Dim rdr As New SqlDataAdapter
        Dim var As String
        Dim commId As New SqlCommand("select * from IDMSBatchEntry where BatchName ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " ", connection1)
        Dim rdrId As SqlDataReader
        connection1.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            bx = "T"
        Else
            bx = "F"
        End If
        connection1.Close()
        commId.Dispose()
        rdrId.Close()
        Return (bx)
    End Function
    <Ajax.AjaxMethod()> Public Function drop(ByVal BatchName, ByVal Month, ByVal Year, ByVal update) As String
        Dim var As String
        Dim ds3 As New DataSet
        Dim cmd As New SqlCommand("select * from IDMSBatchEntry where BatchName ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " ", conn3)
        Dim rdr1 As SqlDataReader
        conn3.Open()
        rdr1 = cmd.ExecuteReader
        If rdr1.Read Then
            var = "True"
        Else
            var = "false"
        End If
        conn3.Close()
        cmd.Dispose()
        rdr1.Close()
        If var = "True" Then
            Try
                Dim rdr As New SqlDataAdapter
                Dim cmd3 As New SqlCommand("UPDATE IDMSBatchEntry set Actual=" & update & " where BatchName ='" & BatchName & "' AND Month ='" & Month & "' AND Year =" & Year & "", conn3)
                conn3.Open()
                cmd3.ExecuteNonQuery()
                conn3.Close()
                cmd3.Dispose()
                Dim str2 As String
                str2 = "The Cph value has been updated successfully!"
                Return (str2)
            Catch
                Dim str2 As String
                str2 = "The Cph value hasn't been updated!"
                Return (str2)
            End Try
        Else
            Dim str2 As String
            str2 = ("Please select the right combination")
            Return (str2)
        End If
    End Function




    'function for take value

    <Ajax.AjaxMethod()> Public Function takevalue1(ByVal Month, ByVal Year) As Integer
        Dim bx1 As Integer
        Dim ds3 As New DataSet
        Dim cmd As New SqlCommand("select ProductionHours  from IDMSBatchEntry where Month = '" + Month + "' AND YEAR = " + Year + " order by month,year", conn3)
        Dim rdr1 As SqlDataReader
        conn3.Open()
        rdr1 = cmd.ExecuteReader
        'rdr1.Read()
        If (rdr1.Read) Then
            bx1 = rdr1("ProductionHours")
        End If
        rdr1.Close()
        conn3.Close()
        cmd.Dispose()
        Return (bx1)

    End Function

    'function for save the values 
    <Ajax.AjaxMethod()> Public Function saveworkingdays(ByVal Month As String, ByVal Year As Integer, ByVal week_days As Integer, ByVal saturday As Integer, ByVal daksh_holiday As Integer, ByVal Training_Hrs As Integer, ByVal leaves As Integer, ByVal HrsOfProduction As Integer, ByVal productionhours As Integer, ByVal Userid As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Dim MonthDays As DateTime
        Dim noOfDays As Integer
        Dim totdays As Integer
        'noOfDays = testDays.DaysInMonth(2007, months)

        Dim commId1 As New SqlCommand("select distinct Month,YEAR from IDMS_workingdays where Month = '" + Month.ToString() + "' AND YEAR = " + Year.ToString() + " ", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader

        If rdrId1.Read Then
            bx1 = "T"
        Else
            bx1 = "F"
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()

        If bx1 = "T" Then
            msg = ("Data Already Exists for this Month & Year!")
            Return (msg)
        Else
            Try
                noOfDays = MonthDays.DaysInMonth(Year, Month)
                totdays = CType(week_days, Integer) + CType(saturday, Integer) + CType(daksh_holiday, Integer)
                If totdays > noOfDays Then
                    msg = ("Sum Of WeekDays, Saturday & Daksh Holidays" & vbCr & "Can't Exceed the Total No.of days of Month!")
                    Return (msg)
                    Exit Function
                End If
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_IDMSworkingDays", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@Week_Days", CType(week_days, Integer))
                    .Add("@Saturday", CType(saturday, Integer))
                    .Add("@Daksh_Holiday", CType(daksh_holiday, Integer))
                    .Add("@Training_Hrs", CType(Training_Hrs, Decimal))
                    .Add("@Leaves", CType(leaves, Decimal))
                    .Add("@HrsOfProduction", CType(HrsOfProduction, Decimal))
                    .Add("@Addedon", System.DateTime.Today)
                    .Add("@Addedby", Userid)
                End With
                connection.Open()
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                'Readinsert.Close()

                msg = "Data inserted Successfully!"
                Return (msg)
            Catch ex As Exception
                msg = "Sorry The Data is not saved!"
                Return (msg)
            End Try
        End If
    End Function

    <Ajax.AjaxMethod()> Public Function bindmonth(ByVal batchname) As String

        Dim strname As String = ""
        Dim objcmd1 As New SqlCommand("select distinct month from idmsbatchentry where batchname='" & batchname & "' order by month", connection)
        Dim reader1 As SqlDataReader
        connection.Open()
        reader1 = objcmd1.ExecuteReader
        While reader1.Read
            If Trim(strname) <> "" Then
                strname = strname & "$" & getmonth(reader1("Month"))
            Else
                strname = getmonth(reader1("Month"))
            End If

        End While
        reader1.Close()
        connection.Close()
        Return (strname)
    End Function
    <Ajax.AjaxMethod()> Public Function bindyear(ByVal batchname) As String
        Dim i As Integer = 0
        Dim strname As String = ""
        Dim monthadapter As New SqlDataAdapter("select distinct year from idmsprodn_hr where batch_id='" & batchname & "' order by year", connection)
        Dim ds As New DataSet
        connection.Open()
        monthadapter.Fill(ds, "idmsprodn_hr")
        connection.Close()
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If Trim(strname) <> "" Then
                strname = strname & "$" & ds.Tables(0).Rows.Item(i).Item("year").ToString()
            Else
                strname = ds.Tables(0).Rows.Item(i).Item("year").ToString()
            End If
        Next
        Return Trim(strname)

    End Function
    <Ajax.AjaxMethod()> Public Function savecph(ByVal BatchName, ByVal Month, ByVal Year, ByVal Actual, ByVal Userid) As String
        Dim msg As String
        Dim bx As String
        Dim bx1 As String
        Dim previousforecasted1 As Decimal
        Dim strname As Decimal
        Dim strname1 As Decimal
        'Dim commId As New SqlCommand("select BatchName,Month,YEAR from IDMSBatchEntry where BatchName ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " order by month,year", connection)
        'Dim rdrId As SqlDataReader
        'connection.Open()
        'rdrId = commId.ExecuteReader

        'If rdrId.Read Then
        '    bx = "T"
        'Else
        '    bx = "F"
        'End If
        'connection.Close()
        'commId.Dispose()
        'rdrId.Close()
        Dim commId1 As New SqlCommand("select distinct Batch_id,Month,YEAR from IDMSprodn_hr where Batch_id ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " order by month,year", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader

        If rdrId1.Read Then
            bx1 = "T"
        Else
            bx1 = "F"
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()

        Dim i As Integer = 0
        'Dim strname As String = ""
        'If bx = "T" Then
        If bx1 = "T" Then
            Try
                Dim rdr As New SqlDataAdapter
                Dim cmd3 As New SqlCommand("UPDATE IDMSprodn_hr set Actual=" & Actual & " where Batch_id ='" & BatchName & "' AND Month ='" & Month & "' AND Year =" & Year & "", conn3)
                conn3.Open()
                cmd3.ExecuteNonQuery()
                conn3.Close()
                cmd3.Dispose()
                msg = "Data Updated Successfully"
                Return (msg)
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Return (strmsg)
            End Try
        Else
            Try
                previousforecasted1 = findpreviousmonth(BatchName)
                Dim stradapter As New SqlDataAdapter("select distinct isnull(Production_hr,0.0) as Production_hr,isnull(forecasted,0.0) as forecasted from IDMSProdn_Hr where Batch_id ='" + BatchName + "'", connection)
                Dim ds As New DataSet
                connection.Open()
                stradapter.Fill(ds, "IDMSProdn_Hr")
                connection.Close()
                strname1 = ds.Tables(0).Rows.Item(i).Item("Production_hr")

                Dim cmdu As New SqlCommand("insert_IDMSProdn_Hr2", connection)
                cmdu.CommandType = CommandType.StoredProcedure
                With cmdu.Parameters()
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@Forecasted", previousforecasted1)
                    .Add("@Actual", Trim(CType(Actual, Decimal)))
                    .Add("@Production_Hr", CType(strname1, Decimal))
                    .Add("@Addedon", System.DateTime.Today)
                    .Add("@Addedby", Userid)
                    .Add("@Batch_Id", Trim(BatchName))
                End With
                connection.Open()

                cmdu.ExecuteNonQuery()
                connection.Close()
                cmdu.Dispose()

                msg = "Data inserted Successfully"
                Return (msg)
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                Return (strmsg)
            End Try
        End If
        'Else

        'msg = ("Please select the right combination
        'Return (msg)
        'End If

    End Function


    'function for saving the additional production volume
    <Ajax.AjaxMethod()> Public Function save2(ByVal Process As String, ByVal LOB As String, ByVal Month As String, ByVal Year As Integer, ByVal volname As String, ByVal volprod As Integer, ByVal HeadCount As Integer, ByVal strTarget As Integer, ByVal Userid As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim commId1 As New SqlCommand("select distinct Month,YEAR,Additional_Vol_by from TempIDMSAddition_Prodn_Vol where LOBName='" + LOB + "' and ProcessName='" + Process + "' and Month = '" + Month + "' AND YEAR = " + Year.ToString() + " AND Additional_Vol_by = '" + volname + "' order by month,year", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader

        If rdrId1.Read Then
            bx1 = "T"
        Else
            bx1 = "F"
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()
        If bx1 = "T" Then

            msg = "Data Already Exists for this Month,Year & Volume Name!"
            Return (msg)

        Else
            Try
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_tempIDMSAddition_Prodn_Vol2", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@Additional_Vol_by", Trim(volname))
                    .Add("@Volume_production", CType(volprod, Integer))
                    .Add("@Addedon", System.DateTime.Today)
                    .Add("@Addedby", Userid)
                    .Add("@Process", Trim(Process))
                    .Add("@LOB", Trim(LOB))
                    .Add("@HeadCount", Trim(HeadCount))
                    .Add("@Target", CType(strTarget, Decimal))
                End With
                connection.Open()

                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                msg = "Data inserted Successfully"
                Return (msg)
            Catch ex As Exception
                msg = "Data Not Inserted Successfully!"
                ' msg = ex.Message()
                Return (msg)
            End Try
        End If
    End Function
    <Ajax.AjaxMethod()> Public Function Target(ByVal Process, ByVal LOB) As String
        Dim msg As String
        Dim bx1 As String
        Dim rdrtarge
        Dim commId1 As New SqlCommand("select distinct Target from IDMSAddition_Prodn_Vol where ProcessName='" + Process + "' and LOBName='" + LOB + "' and (Target is not null)", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader
        If rdrId1.Read Then
            rdrtarge = rdrId1("Target")
            Return (rdrtarge)
        Else
            rdrtarge = 0.0
            Return (rdrtarge)
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()
    End Function
    ' Function for saving the attrition value
    <Ajax.AjaxMethod()> Public Function saveActualProduction(ByVal Process As String, ByVal LOB As String, ByVal Month As String, ByVal Year As Integer, ByVal volprod As Integer, ByVal Userid As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim commId1 As New SqlCommand("select distinct Month,YEAR from IDMSActual_Prodn_Vol where LOBName='" + LOB + "' and ProcessName='" + Process + "' and Month = '" + Month.ToString() + "' AND YEAR = '" + Year.ToString() + "'  order by month,year", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader
        If rdrId1.Read Then
            bx1 = "T"
        Else
            bx1 = "F"
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()
        If bx1 = "T" Then
            Try
                Dim cmdUpdate As SqlCommand = New SqlCommand("Update_TempIDMSActualProductionVolume", connection)
                cmdUpdate.CommandType = CommandType.StoredProcedure
                With cmdUpdate.Parameters()
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@ActualVolumeProduction", CType(volprod, Integer))
                    .Add("@Addedon", System.DateTime.Today)
                    .Add("@Addedby", Userid)
                    .Add("@Process", Trim(Process))
                    .Add("@LOB", Trim(LOB))
                End With
                connection.Open()
                cmdUpdate.ExecuteNonQuery()
                connection.Close()
                cmdUpdate.Dispose()
                msg = "Data Updated Successfully"
                Return (msg)
            Catch ex As Exception
                msg = "Data Not Updated Successfully!"
                Return (msg)
            End Try
        Else
            Try
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempIDMSActualProductionVolume", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@ActualVolumeProduction", CType(volprod, Integer))
                    .Add("@Addedon", System.DateTime.Today)
                    .Add("@Addedby", Userid)
                    .Add("@Process", Trim(Process))
                    .Add("@LOB", Trim(LOB))
                End With
                connection.Open()
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                msg = "Data inserted Successfully"
                Return (msg)
            Catch ex As Exception
                msg = "Data Not Inserted Successfully!"
                Return (msg)
            End Try
        End If
    End Function
    <Ajax.AjaxMethod()> Public Function save1(ByVal BatchName, ByVal Month, ByVal Year, ByVal atteration, ByVal Userid) As String
        Dim msg As String
        Dim bx As String
        Dim bx1 As String
        Dim joinees As Integer
        Dim strname As Decimal
        Dim strname1 As Decimal
        Dim actual1 As Decimal
        Dim balance1 As Integer
        Dim commulative As Integer
        Dim balance2 As Integer
        Dim commulative1 As Integer
        Dim commId2 As New SqlCommand("select noofjoinees,isnull(noofattritions,0) as noofattritions,isnull(balance,0) as balance from IDMSbatchentry where Batchname ='" + BatchName + "'", connection)
        Dim rdrId2 As SqlDataReader
        connection.Open()
        rdrId2 = commId2.ExecuteReader

        If rdrId2.Read Then
            joinees = rdrId2("noofjoinees")
            commulative = rdrId2("noofattritions")
            balance1 = rdrId2("balance")
        End If
        connection.Close()
        commId2.Dispose()
        rdrId2.Close()
        If atteration > joinees Then
            msg = "Attrition value can not be Greater than the No Of Joinees!"
            Return (msg)
            Exit Function
        End If


        Dim commId As New SqlCommand("select Batch_id,Month,YEAR from IDMSprodn_hr where Batch_id ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " order by month,year", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader

        If rdrId.Read Then
            bx = "T"
        Else
            bx = "F"
        End If
        connection.Close()
        commId.Dispose()
        rdrId.Close()
        Dim commId1 As New SqlCommand("select distinct Batch_id,Month,YEAR from IDMSattrition where Batch_id ='" + BatchName + "'AND Month = '" + Month + "' AND YEAR = " + Year + " order by month,year", connection)
        Dim rdrId1 As SqlDataReader
        connection.Open()
        rdrId1 = commId1.ExecuteReader

        If rdrId1.Read Then
            bx1 = "T"
        Else
            bx1 = "F"
        End If
        connection.Close()
        commId1.Dispose()
        rdrId1.Close()

        If (balance1 = 0) Then
            balance1 = joinees - atteration
        Else
            balance1 = balance1 - atteration
        End If
        'balance = Abs(joinees - actual1)
        'commulative = commulative + atteration
        If commulative >= joinees Then
            msg = "Attrition value should not be Greater than the No Of Joinees!"
            Return (msg)
            Exit Function
        Else
            commulative = commulative + atteration
        End If
        ' Calculation  for checking the balance and Noof Attrition Values
        If balance1 < 0 And commulative > joinees Then
            msg = "Attrition value should not be Greater than the No Of Joinees!"
            Return (msg)
            Exit Function
        End If
        If bx = "T" Then
            If bx1 = "T" Then
                Try
                    Dim rdr As New SqlDataAdapter
                    Dim cmd3 As New SqlCommand("UPDATE IDMSattrition set Actual=" & atteration & " where Batch_id ='" & BatchName & "' AND Month ='" & Month & "' AND Year =" & Year & "", conn3)
                    conn3.Open()
                    cmd3.ExecuteNonQuery()
                    conn3.Close()
                    cmd3.Dispose()
                    'UPDATE idmsbatchentry set Actual=" & atteration & " where Batch_id ='" & BatchName & "' AND Month ='" & Month & "' AND Year =" & Year & "
                    Dim cmd4 As New SqlCommand("UPDATE idmsbatchentry set noofattritions=" & commulative & ",Balance=" & balance1 & " where Batchname ='" & BatchName & "'", conn3)
                    conn3.Open()
                    cmd4.ExecuteNonQuery()
                    conn3.Close()
                    cmd4.Dispose()

                    msg = "Data Updated Successfully"
                    Return (msg)
                Catch ex As Exception
                    Dim strmsg As String
                    strmsg = Replace(ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    Return (strmsg)
                End Try
            Else
                Try
                    Dim i As Integer = 0
                    Dim cmdu As New SqlCommand("insert_IDMSAttrition", connection)
                    cmdu.CommandType = CommandType.StoredProcedure
                    With cmdu.Parameters()
                        .Add("@Batch_Id", Trim(BatchName))
                        .Add("@Month", Trim(Month))
                        .Add("@Year", CType(Year, Integer))
                        '.Add("@Forecasted", CType(strname1, Decimal))
                        .Add("@Actual", Trim(CType(atteration, Integer)))
                        '.Add("@Attrition", CType(strname, Decimal))
                        .Add("@Addedon", System.DateTime.Today)
                        .Add("@Addedby", Userid)
                    End With
                    connection.Open()

                    cmdu.ExecuteNonQuery()
                    connection.Close()
                    cmdu.Dispose()
                    'Dim rdr As New SqlDataAdapter
                    Dim cmd4 As New SqlCommand("UPDATE idmsbatchentry set noofattritions=" & commulative & ",Balance=" & balance1 & " where Batchname ='" & BatchName & "'", conn3)
                    conn3.Open()
                    cmd4.ExecuteNonQuery()
                    conn3.Close()
                    cmd4.Dispose()

                    msg = "Data inserted Successfully"
                    Return (msg)
                Catch ex As Exception
                    msg = Replace(ex.Message, "'", " ") '"Data not saved successfully!"
                    Return (msg)
                End Try
            End If
        Else

            msg = "Please Select the Right Combination !"
            Return (msg)
        End If
    End Function
    <Ajax.AjaxMethod()> Public Function getmonth(ByVal intmonth) As String
        Dim month1 As String
        Select Case intmonth
            Case 1
                month1 = "Jan"
            Case 2
                month1 = "Feb"
            Case 3
                month1 = "Mar"
            Case 4
                month1 = "Apr"
            Case 5
                month1 = "May"
            Case 6
                month1 = "Jun"
            Case 7
                month1 = "Jul"
            Case 8
                month1 = "Aug"
            Case 9
                month1 = "Sep"
            Case 10
                month1 = "Oct"
            Case 11
                month1 = "Nov"
            Case 12
                month1 = "Dec"
        End Select
        Return month1
    End Function
    <Ajax.AjaxMethod()> Public Function findpreviousmonth(ByVal Batch) As Decimal
        Dim previousforecasted As Decimal
        Dim commId As New SqlCommand("select isNull(Forecasted,0.0) as Forecasted from IDMSProdn_Hr where batch_id='" & Batch & "'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        While rdrId.Read
            previousforecasted = rdrId("Forecasted")
        End While
        connection.Close()
        commId.Dispose()
        rdrId.Close()
        Return previousforecasted
    End Function

End Class
