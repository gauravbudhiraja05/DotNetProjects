Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings

Public Class AjaxBlt

    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim conn As New SqlConnection(constr)
    Dim conbtch As New SqlConnection(constr)
    Dim conbatch As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connectionbal As New SqlConnection(constr)
    Dim balcount

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

    <Ajax.AjaxMethod()> Public Function bindlob(ByVal processid As String) As String
        Dim da As New SqlDataAdapter("select * from bltlob where processid=" & processid & " ", connection)
        Dim ds As New DataSet
        connection.Open()
        Dim counter = da.Fill(ds)
        connection.Close()
        Dim i As Integer
        Dim mainstr As String = ""
        Dim strprocessid
        Dim strlob
        For i = 0 To ds.Tables(0).Rows.Count - 1
            strlob = ds.Tables(0).Rows.Item(i).Item("lob").ToString()
            If mainstr = "" Then
                mainstr = strlob
            Else
                mainstr = mainstr & "$" & strlob
            End If
        Next
        If counter = 0 Then
            mainstr = "No Records"
        End If
        Return Trim(mainstr)

    End Function

    <Ajax.AjaxMethod()> Public Function saveworkingdays(ByVal process As String, ByVal lob As String, ByVal Month As Integer, ByVal Year As Integer, ByVal week_days As Integer, ByVal saturday As Integer, ByVal daksh_holiday As Integer, ByVal Training_Hrs As Integer, ByVal leaves As Integer, ByVal userid As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            'Dim Readinsert As SqlDataReader
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTworkingDays", connection)
            cmdinsert.CommandType = CommandType.StoredProcedure
            With cmdinsert.Parameters()
                .Add("@Process", Trim(process))
                .Add("@Lob", Trim(lob))
                .Add("@Month", Trim(Month))
                .Add("@Year", CType(Year, Integer))
                .Add("@week_Days", CType(week_days, Integer))
                .Add("@saturday", CType(saturday, Integer))
                .Add("@Dakash_Holiday", CType(daksh_holiday, Integer))
                .Add("@Training_Hrs", CType(Training_Hrs, Decimal))
                .Add("@Leaves", CType(leaves, Decimal))
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            SetBLTTrack(userid, "Save", "Add Working Days", CType(week_days, Integer))
            'Readinsert.Close()
            msg = "Data inserted Successfully!"
            Return (msg)
        Catch ex As Exception
            msg = "Sorry The Data is not saved!"
            Return (msg)
        End Try
    End Function


    <Ajax.AjaxMethod()> Public Function saveActual(ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As Integer, ByVal actu As Integer, ByVal ForYear As Integer, ByVal userid As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            'Dim Readinsert As SqlDataReader
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTActual", connection)
            cmdinsert.CommandType = CommandType.StoredProcedure
            With cmdinsert.Parameters()
                .Add("@Process", Trim(process))
                .Add("@Lob", Trim(lob))
                .Add("@BatchName", Trim(batch))
                .Add("@Month", Trim(Month))
                .Add("@ForMonth", Trim(ForMonth))
                .Add("@Year", CType(Year, Integer))
                .Add("@Actual", CType(actu, Integer))
                .Add("@ForYear", Trim(ForYear))
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            SetBLTTrack(userid, "Save", "Actual", CType(actu, Integer))
            'Readinsert.Close()
            msg = "Data inserted Successfully!"
            Return (msg)
        Catch ex As Exception
            msg = "Sorry The Data is not saved!"
            Return (msg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function saveAttrition(ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As Integer, ByVal att As Integer, ByVal ForYear As Integer, ByVal traRes As String, ByVal userid As Integer) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            '****************NEW***************

            '****************check for  balance  greater & equal to atteration in bltfinaltable***********
            'Dim cmdchkbal As New SqlCommand("select min(balance)as balance from bltfinaltable where processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "' and batchname='" & Trim(batch) & "'", connectionbal)
            'connectionbal.Open()
            'Dim balcount As String
            'balcount = cmdchkbal.ExecuteScalar()
            'connectionbal.Close()
            'cmdchkbal.Dispose()
            'Dim strtransfervalue As String = Trim(att)
            'If balcount <= CType(strtransfervalue, Integer) Then
            '    msg = "transfer value are more than  no.of joiness! so you can not proceed"
            '    Return (msg)
            'End If
            '*************NEW END***********
            '******************CHECK For HiredBatch***************************************************
            Dim batchchkcmd1 As New SqlCommand(" select BatchName from bltbatchentry where batchname= '" & Trim(batch) & "'and processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "'", conbatch)
            Dim batchdr1 As SqlDataReader
            conbatch.Open()
            batchdr1 = batchchkcmd1.ExecuteReader
            If batchdr1.Read Then
                '************old*************
                Dim cmdchkbal As New SqlCommand("select min(balance)as balance from bltfinaltable where processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "' and batchname='" & Trim(batch) & "'", connectionbal)
                connectionbal.Open()
                Dim balcount As String
                balcount = cmdchkbal.ExecuteScalar()
                connectionbal.Close()
                cmdchkbal.Dispose()
                Dim strtransfervalue As String = Trim(att)
                If balcount <= CType(strtransfervalue, Integer) Then
                    msg = "transfer value are more than  no.of joiness! so you can not proceed"
                    Return (msg)
                End If
                'old

                'Dim Readinsert As SqlDataReader
                ' previous()
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTAttrition", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Process", Trim(process))
                    .Add("@Lob", Trim(lob))
                    .Add("@BatchName", Trim(batch))
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@Attrition", CType(att, Integer))
                    .Add("@ForMonth", Trim(ForMonth))
                    .Add("@ForYear", CType(Trim(ForYear), Integer))
                    .Add("@ResignTransfer", Trim(traRes))
                    '''    .Add("@TransferTo", "")
                    '''    ''.Add("@Process", Trim(process))
                    '''    ''.Add("@Lob", Trim(lob))
                    '''    ''.Add("@BatchName", Trim(batch))
                    '''    ''.Add("@Month", Trim(Month))
                    '''    ''.Add("@Year", CType(Year, Integer))
                    '''    ''.Add("@Attrition", CType(att, Integer))
                    '''    ''.Add("@ForMonth", Trim(ForMonth))
                    '''    ''.Add("@ForYear", Trim(ForYear))
                    '''    ''.Add("@ResignTransfer", Trim(traRes))
                End With
                'end previous

                ''Dim strquery_chk As String
                ''Dim stryear, strattrition, strforyear As Integer
                ''stryear = CType(Year, Integer)
                ''strattrition = CType(att, Integer)
                ''strforyear = CType(Trim(ForYear), Integer)
                ''strquery_chk = "select * from bltattrition where processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "' and batchname='" & Trim(batch) & "' and  Month='" & Trim(Month) & "' and year='" & stryear & "' and  foryear='" & strforyear & "' and  formonth='" & Trim(ForMonth) & "' and ResignTransfer ='" & Trim(traRes) & "' "
                ''Dim cmdinsert As SqlCommand = New SqlCommand(strquery_chk, connection)
                connection.Open()
                ''Dim strquery_insert As String
                ' ******************check for process in bltattrition and insert ito table****************
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                SetBLTTrack(userid, "Save", "Add Attrition", CType(att, Integer))
                ''strquery_insert = "insert into bltattrition (processname,lobname,batchname,Month,year,attrition,foryear,formonth,ResignTransfer,TransferTo)values('" & Trim(process) & "','" & Trim(lob) & "','" & Trim(batch) & "','" & Trim(Month) & "','" & stryear & "','" & strattrition & "','" & strforyear & "','" & Trim(ForMonth) & "','" & Trim(traRes) & "','')"
                ''Dim cmdinsert_v As SqlCommand = New SqlCommand(strquery_insert, connection2)
                ''connection2.Open()
                ''cmdinsert_v.ExecuteNonQuery()
                ''connection2.Close()
                ''cmdinsert_v.Dispose()
                msg = "Data inserted Successfully!"
                Return (msg)
                ''connection.Close()
                ''cmdinsert.Dispose()
                'msg = "Data inserted Successfully!"
                'Return (msg)
                'End If

            Else
                batchchkcmd1.Dispose()
                conbatch.Close()
                batchdr1.Close()
                msg = "Hired BatchName does not Exist!"
                Return (msg)
            End If
        Catch ex As Exception
            'Dim msg As String
            'msg = "Data Is Not Saved!"
            'Return (msg)
            Return (ex.Message)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function saveAttritionTransfer(ByVal processTr As String, ByVal lobTr As String, ByVal batchTr As String, ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As Integer, ByVal att As Integer, ByVal ForYear As String, ByVal traRes As String, ByVal monthtr As String, ByVal yeartr As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Dim strTr As String
        Dim cmdinsert As SqlCommand
        Try


            '*******************check For HiredBatch 19/10/07***********************************************
            Dim batchchkcmd1 As New SqlCommand("select BatchName from bltbatchentry where batchname= '" & Trim(batch) & "'and processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "'", conbatch)
            Dim batchdr1 As SqlDataReader
            conbatch.Open()
            batchdr1 = batchchkcmd1.ExecuteReader
            If batchdr1.Read Then
                'OLD
                Dim cmdchkbal As New SqlCommand("select min(balance)as balance from bltfinaltable where processname='" & Trim(process) & "' and lobname='" & Trim(lob) & "' and batchname='" & Trim(batch) & "'", connectionbal)
                connectionbal.Open()
                Dim balcount As String
                balcount = cmdchkbal.ExecuteScalar()
                connectionbal.Close()
                cmdchkbal.Dispose()
                Dim strtransfervalue As String = Trim(att)
                If balcount <= CType(strtransfervalue, Integer) Then
                    msg = "transfer value are more than  no.of joiness! so you can not proceed"
                    Return (msg)
                End If
                'old

                '******************************************************************
                Dim Readinsert As SqlDataReader
                strTr = "" & att & "-Transfer To Process:-" & processTr & ", Lob:-" & lobTr & ", Batch:-" & batchTr
                cmdinsert = New SqlCommand("insert_BLTAttritionWithTransfer ", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Process", Trim(process))
                    .Add("@Lob", Trim(lob))
                    .Add("@BatchName", Trim(batch))
                    .Add("@Month", Trim(Month))
                    .Add("@Year", CType(Year, Integer))
                    .Add("@Attrition", CType(att, Integer))
                    .Add("@ForMonth", Trim(ForMonth))
                    .Add("@ForYear", Trim(ForYear))
                    .Add("@ResignTransfer", Trim(traRes))
                    .Add("@TransferTo", Trim(strTr))

                End With
                connection.Open()
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()

                'changes made on 27nov07

                Dim strTr1 As String
                strTr1 = "" & att & "-Transfer From Process:-" & process & ", Lob:-" & lob & ", Batch:-" & batch
                'Dim monthvalue As Integer = getmonthvalue(monthtr)
                Dim cmdinsert1 = New SqlCommand("insert_blttransferfrom", connection)
                cmdinsert1.CommandType = CommandType.StoredProcedure
                With cmdinsert1.Parameters()
                    .Add("@Process", Trim(processTr))
                    .Add("@Lob", Trim(lobTr))
                    .Add("@BatchName", Trim(batchTr))
                    .Add("@Month", Trim(monthtr))
                    .Add("@Year", CType(yeartr, Integer))
                    ''''.Add("@Attrition", CType(att, Integer))
                    ''''.Add("@ForMonth", Trim(monthtr))
                    ''''.Add("@ForYear", Trim(yeartr))
                    .Add("@ResignTransfer", Trim(traRes))
                    .Add("@TransferTo", Trim(strTr1))
                End With
                connection.Open()
                cmdinsert1.ExecuteNonQuery()
                connection.Close()
                cmdinsert1.Dispose()

                'end changes made on 27nov07




                '**********************************************check For TransferBatchName19/10/07*********************************************


                Dim cmdchk As New SqlCommand("select isnull(NoOfJoinees,0) as NoOfJoinees from  bltbatchentry where batchname='" & Trim(batchTr) & "' and processname='" & Trim(processTr) & "' and lobname='" & Trim(lobTr) & "'", conn)
                Dim drchk As SqlDataReader
                conn.Open()
                drchk = cmdchk.ExecuteReader

                If drchk.Read Then
                    Dim nojoinees = drchk("NoOfJoinees")
                    Dim newnojoinees = CType(att, Integer) + nojoinees
                    Dim cmdupd As New SqlCommand("update bltbatchentry set noofjoinees = '" & newnojoinees & "' where batchname='" & Trim(batchTr) & "' and processname='" & Trim(processTr) & "' and lobname='" & Trim(lobTr) & "'", connection1)
                    connection1.Open()
                    cmdupd.ExecuteNonQuery()
                    connection1.Close()
                    cmdupd.Dispose()


                Else
                    Dim cmdins As SqlCommand = New SqlCommand("insert_BLTBatchEntry", connection1)
                    cmdins.CommandType = CommandType.StoredProcedure
                    With cmdins.Parameters()
                        .Add("@Process", Trim(processTr))
                        .Add("@Lob", Trim(lobTr))
                        .Add("@BatchName", Trim(batchTr))
                        .Add("@Month", Trim(ForMonth))
                        .Add("@Year", Trim(ForYear))
                        .Add("@Joinee", CType(att, Integer))
                    End With
                    connection1.Open()
                    cmdins.ExecuteNonQuery()
                    connection1.Close()
                    cmdins.Dispose()
                End If
                conn.Close()
                drchk.Close()

                '**************call updatebatch() fun**************
                ' updateBatch(Trim(processTr), Trim(lobTr), Trim(batchTr), Trim(ForMonth), Trim(ForYear), att)

                '********************************************end*****************************************
                '''''''''''''''''''''''''''''''update joinees into batch entery''''''''''''''
                ''Dim cmdchk As New SqlCommand("select isnull(NoOfJoinees,0) as NoOfJoinees from  bltbatchentry where batchname='" & Trim(batchTr) & "' and processname='" & Trim(processTr) & "' and lobname='" & Trim(lobTr) & "'", connection)
                ''Dim drchk As SqlDataReader
                ''connection.Open()
                ''drchk = cmdchk.ExecuteReader
                ''If drchk.Read Then
                ''    Dim nojoinees = drchk("NoOfJoinees")
                ''    Dim newnojoinees = CType(att, Integer) + nojoinees
                ''    Dim cmdupd As New SqlCommand("update bltbatchentry set noofjoinees = '" & newnojoinees & "' where batchname='" & Trim(batchTr) & "' and processname='" & Trim(processTr) & "' and lobname='" & Trim(lobTr) & "'", connection1)
                ''    connection1.Open()
                ''    cmdupd.ExecuteNonQuery()
                ''    connection1.Close()
                ''    cmdupd.Dispose()
                ''Else
                ''Dim cmdins As SqlCommand = New SqlCommand("insert_BLTBatchEntry", connection)
                ''cmdins.CommandType = CommandType.StoredProcedure
                ''With cmdinsert.Parameters()
                ''    .Add("@Process", Trim(processTr))
                ''    .Add("@Lob", Trim(lobTr))
                ''    .Add("@BatchName", Trim(batchTr))
                ''    .Add("@Month", Month)
                ''    .Add("@Year", Year)
                ''    .Add("@Joinee", CType(att, Integer))
                ''End With
                ''connection.Open()
                ''cmdins.ExecuteNonQuery()
                ''connection.Close()
                ''cmdins.Dispose()
                ''End If
                ''drchk.Close()
                ''connection.Close()
                ''cmdchk.Dispose()
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                msg = "Data inserted Successfully!"
                Return (msg)

            Else

                batchchkcmd1.Dispose()
                conbatch.Close()
                batchdr1.Close()

                msg = "Hired BatchName does not Exist!"
                Return (msg)
            End If

        Catch ex As Exception

            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Return (strmsg)

        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function savebatch(ByVal process As String, ByVal Lob As String, ByVal BatchName As String, ByVal Month As Integer, ByVal Year As Integer, ByVal Joinee As Integer, ByVal userid As Integer) As String
        Dim msg As String
        Try

            Dim cmdchk As New SqlCommand("select isnull(NoOfJoinees,0) as NoOfJoinees from  bltbatchentry where batchname='" & Trim(BatchName) & "' and processname='" & Trim(process) & "' and lobname='" & Trim(Lob) & "'", connection)
            Dim drchk As SqlDataReader
            connection.Open()
            drchk = cmdchk.ExecuteReader
            If drchk.Read Then
                Dim nojoinees = drchk("NoOfJoinees")
                Dim newnojoinees = CType(Joinee, Integer) + nojoinees
                'Dim cmdupd As New SqlCommand("update bltbatchentry set noofjoinees = '" & newnojoinees & "' where batchname='" & Trim(BatchName) & "' and processname='" & Trim(process) & "' and lobname='" & Trim(Lob) & "'", connection1)
                Dim cmdupd As New SqlCommand("update bltbatchentry set noofjoinees = '" & newnojoinees & "' where batchname='" & Trim(BatchName) & "' and processname='" & Trim(process) & "' and lobname='" & Trim(Lob) & "'", connection1)
                connection1.Open()
                cmdupd.ExecuteNonQuery()
                connection1.Close()
                cmdupd.Dispose()
            Else
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTBatchEntry", connection1)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Process", Trim(process))
                    .Add("@Lob", Trim(Lob))
                    .Add("@BatchName", Trim(BatchName))
                    .Add("@Month", Month)
                    .Add("@Year", Year)
                    .Add("@Joinee", Joinee)
                End With
                connection1.Open()
                cmdinsert.ExecuteNonQuery()
                connection1.Close()
                cmdinsert.Dispose()
                SetBLTTrack(userid, "Save", "Batch", BatchName)
                ''msg = "Data inserted Successfully"
                ''Return (msg)
            End If

            msg = "Data inserted Successfully"
            Return (msg)

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            Return (strmsg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function updateBatch(ByVal process, ByVal Lob, ByVal BatchName, ByVal Month, ByVal Year, ByVal Joinee, ByVal userid) As String
        Dim msg As String
        Try


            Dim cmdchek As SqlCommand = New SqlCommand("select * from BLTBatchEntry where ProcessName='" & Trim(process) & "' and LOBName='" & Trim(Lob) & "' and BatchName='" & Trim(BatchName) & "' and Month='" & Trim(Month) & "' and year='" & Year & "'", connection2)
            '''''''''''''''''''''''''''''''''''
            Dim rdrCheck As SqlDataReader
            connection2.Open()
            rdrCheck = cmdchek.ExecuteReader
            If rdrCheck.Read Then
                Dim cmdinsert As SqlCommand = New SqlCommand("update BLTBatchEntry set NoOfJoinees=((select NoOfJoinees from BLTBatchEntry where ProcessName='" & Trim(process) & "' and LOBName='" & Trim(Lob) & "' and BatchName='" & Trim(BatchName) & "' and Month='" & Trim(Month) & "' and year='" & Year & "')+" & Joinee & ") where ProcessName='" & Trim(process) & "' and LOBName='" & Trim(Lob) & "' and BatchName='" & Trim(BatchName) & "' and Month='" & Trim(Month) & "' and year='" & Year & "'", connection)
                connection.Open()
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                msg = "Data Updated Successfully"
                Return (msg)
            Else
                Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTBatchEntry", connection)
                cmdinsert.CommandType = CommandType.StoredProcedure
                With cmdinsert.Parameters()
                    .Add("@Process", Trim(process))
                    .Add("@Lob", Trim(Lob))
                    .Add("@BatchName", Trim(BatchName))
                    .Add("@Month", Month)
                    .Add("@Year", Year)
                    .Add("@Joinee", Joinee)
                End With
                connection.Open()
                cmdinsert.ExecuteNonQuery()
                connection.Close()
                cmdinsert.Dispose()
                SetBLTTrack(userid, "Update", "Batch", BatchName)
                msg = "Data inserted Successfully"
                Return (msg)
            End If
            cmdchek.Dispose()
            rdrCheck.Close()
            connection2.Close()
            '''''''''''''''''''''''''''''''''''
        Catch ex As Exception
            msg = "Data Not Updated Successfully!"
            Return (msg)
        End Try
    End Function

    <Ajax.AjaxMethod()> Public Function getmonthvalue(ByVal intmonth) As Integer
        Dim month1 As Integer
        Select Case intmonth
            Case "January"
                month1 = 1
            Case "February"
                month1 = 2
            Case "March"
                month1 = 3
            Case "April"
                month1 = 4
            Case "May"
                month1 = 5
            Case "June"
                month1 = 6
            Case "July"
                month1 = 7
            Case "August"
                month1 = 8
            Case "September"
                month1 = 9
            Case "October"
                month1 = 10
            Case "November"
                month1 = 11
            Case "December"
                month1 = 12
        End Select
        Return month1
    End Function

    Public Function SetBLTTrack(ByVal ActionBy As String, ByVal Action As String, ByVal Entity As String, ByVal EntityName As String)
        connection.Close()
        Dim cmdinsert As SqlCommand = New SqlCommand("Sp_TrackBLTForMaster", connection)
        cmdinsert.CommandType = CommandType.StoredProcedure
        With cmdinsert.Parameters()
            .Add("@ActionBy", Trim(ActionBy))
            .Add("@Action", Trim(Action))
            .Add("@Entity", Trim(Entity))
            .Add("@EntityName", EntityName)
            .Add("@Date", System.DateTime.Now)
        End With
        connection.Open()
        cmdinsert.ExecuteNonQuery()
        connection.Close()
        cmdinsert.Dispose()
    End Function


End Class
