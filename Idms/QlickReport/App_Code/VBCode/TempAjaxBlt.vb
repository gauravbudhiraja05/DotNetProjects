Imports Microsoft.VisualBasic
Imports Ajax
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings

Public Class TempAjaxBlt

    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)

    <Ajax.AjaxMethod()> Public Function bindlob(ByVal processid As Integer) As String
        Dim da As New SqlDataAdapter("select * from bltlob where processid=" & processid & " ", connection)
        Dim ds As New DataSet
        connection.Open()
        Dim counter = da.Fill(ds)
        connection.Close()
        Dim i As Integer
        Dim mainstr As String = ""
        ' Dim strprocessid
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

    <Ajax.AjaxMethod()> Public Function saveworkingdays(ByVal process As String, ByVal lob As String, ByVal Month As String, ByVal Year As Integer, ByVal week_days As Integer, ByVal saturday As Integer, ByVal daksh_holiday As Integer, ByVal Training_Hrs As Integer, ByVal leaves As Integer, ByVal userid As Integer) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            'Dim Readinsert As SqlDataReader
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempBLTworkingDays", connection)
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


    <Ajax.AjaxMethod()> Public Function saveActual(ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As Integer, ByVal actu As Integer, ByVal ForYear As Integer, ByVal userid As Integer) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            'Dim Readinsert As SqlDataReader
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempBLTActual", connection)
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
            ' msg = "Sorry The Data is not saved!"
            msg = ex.Message()
            Return (msg)
        End Try
    End Function


    <Ajax.AjaxMethod()> Public Function saveAttrition(ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As String, ByVal att As String, ByVal ForYear As String, ByVal traRes As String, ByVal userid As Integer) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Try
            'Dim Readinsert As SqlDataReader
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempBLTAttrition", connection)
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

                'traRes
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            SetBLTTrack(userid, "Save", "Add Attrition", CType(att, Integer))
            msg = "Data inserted Successfully!"
            Return (msg)
        Catch ex As Exception
            msg = "Sorry The Data is not saved!"
            Return (msg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function saveAttritionTransfer(ByVal processTr As String, ByVal lobTr As String, ByVal batchTr As String, ByVal process As String, ByVal lob As String, ByVal batch As String, ByVal Month As String, ByVal ForMonth As String, ByVal Year As Integer, ByVal att As Integer, ByVal ForYear As String, ByVal traRes As String) As String
        Dim msg As String
        Dim bx1 As String
        Dim str1 As String
        Dim prodnhrs As Decimal
        Dim var As String
        Dim strTr As String
        Try
            'Dim Readinsert As SqlDataReader
            strTr = "Transfer To Process:-" & processTr & ", Lob:-" & lobTr & ", Batch:-" & batchTr
            Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempBLTAttritionWithTransfer ", connection)
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
                'strTr
            End With
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            msg = "Data inserted Successfully!"
            Return (msg)
        Catch ex As Exception
            msg = "Sorry The Data is not saved!"
            Return (msg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function savebatch(ByVal process As String, ByVal Lob As String, ByVal BatchName As String, ByVal Month As String, ByVal Year As Integer, ByVal Joinee As Integer, ByVal userid As Integer) As String
        Dim msg As String
        Try

            Dim cmdinsert As SqlCommand = New SqlCommand("insert_TempBLTBatchEntry", connection)
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
            SetBLTTrack(userid, "Save", "Batch", BatchName)
            msg = "Data inserted Successfully"
            Return (msg)
        Catch ex As Exception
            msg = "Data Not Inserted Successfully!"
            Return (msg)
        End Try
    End Function
    <Ajax.AjaxMethod()> Public Function updateBatch(ByVal process As String, ByVal Lob As String, ByVal BatchName As String, ByVal Month As String, ByVal Year As Integer, ByVal Joinee As Integer, ByVal userid As Integer) As String
        Dim msg As String
        Try
            Dim cmdinsert As SqlCommand = New SqlCommand("update TempBLTBatchEntry set NoOfJoinees=((select NoOfJoinees from TempBLTBatchEntry where ProcessName='" & Trim(process) & "' and LOBName='" & Trim(Lob) & "' and BatchName='" & Trim(BatchName) & "' and Month='" & Trim(Month) & "' and year='" & Year.ToString() & "')+" & Joinee & ") where ProcessName='" & Trim(process) & "' and LOBName='" & Trim(Lob) & "' and BatchName='" & Trim(BatchName) & "' and Month='" & Trim(Month) & "' and year='" & Year.ToString() & "'", connection)
            'Dim cmdinsert As SqlCommand = New SqlCommand("insert_BLTBatchEntry", connection)
            connection.Open()
            cmdinsert.ExecuteNonQuery()
            connection.Close()
            cmdinsert.Dispose()
            SetBLTTrack(userid, "Update", "Batch", BatchName)
            msg = "Data Updated Successfully"
            Return (msg)
        Catch ex As Exception
            msg = "Data Not Updated Successfully!"
            Return (msg)
        End Try
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
