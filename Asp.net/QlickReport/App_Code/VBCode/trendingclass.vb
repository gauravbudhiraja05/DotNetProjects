Imports Microsoft.VisualBasic

Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.Data.OleDb

Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.Configuration
Imports System.Math


Public Class trendingclass
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim drdata As SqlDataReader
    Dim dr As SqlDataReader
    Dim comdepart As SqlCommand
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim con As New SqlConnection(constr)
    'Dim connection1 As New SqlConnection(constr)
    'Dim comdepart As New SqlCommand
    Function AvailableDaysGross(ByVal DaysInAYear As Double, ByVal WeelyOffs As Double)
        Dim AvailableDaysGrossval As Double

        AvailableDaysGrossval = DaysInAYear - WeelyOffs
        Return AvailableDaysGrossval

    End Function
    Function round(ByVal value As Double) As Double
        Dim rounded As Double = Math.Round(value, 2)
        Return rounded
    End Function
    Function WeeklyOffpercentage(ByVal DaysInAYear As Double, ByVal WeelyOffs As Double)
        Dim WeeklyOffpercentageval As Double

        WeeklyOffpercentageval = (WeelyOffs * 100) / DaysInAYear
        Return WeeklyOffpercentageval

    End Function
    ' (, , , , , , , , , )
    Function minusfromninevaluesun(ByVal avldays As Double, ByVal trdays As Double, ByVal festivelives As Double, ByVal ibmskleave As Double, ByVal IBMCasualLeaves As Double, ByVal IBMEarnedLeaves As Double, ByVal all As Double)
        Dim WeeklyOffpercentageval As Double

        WeeklyOffpercentageval = avldays - (trdays + festivelives + ibmskleave + IBMCasualLeaves + IBMEarnedLeaves + all)

        Return WeeklyOffpercentageval

    End Function
    Function netdlvryperyear(ByVal dlveryagentperyear As Double, ByVal srinkdwntime As Double)
        Dim AvailableDaysGrossval As Double

        AvailableDaysGrossval = (dlveryagentperyear * srinkdwntime) / 100
        Dim actaualvalue As Double = dlveryagentperyear - AvailableDaysGrossval
        Return actaualvalue

    End Function
    Function dividetwovalues(ByVal netdlvryperyear As Double, ByVal wkdaysyearly As Double)
        Dim AvailableDaysGrossval As Double

        AvailableDaysGrossval = netdlvryperyear / wkdaysyearly

        Return AvailableDaysGrossval

    End Function
    Function twofieldpercentage(ByVal netdlvryperyear As Double, ByVal AddPlanOvertime As Double)
        Dim AvailableDaysGrossval As Double

        AvailableDaysGrossval = (netdlvryperyear * (AddPlanOvertime * 0.01)) + netdlvryperyear
        Return AvailableDaysGrossval

    End Function
    Function multiplytwonumbers(ByVal netdlvryperyear As Double, ByVal AddPlanOvertime As Double)
        Dim AvailableDaysGrossval As Double

        AvailableDaysGrossval = (netdlvryperyear * AddPlanOvertime)
        Return AvailableDaysGrossval

    End Function
    Function savecalculater(ByVal calculatername As String, ByVal ddldaysinyear As Double, ByVal ddlweeklyoffs As Double, ByVal txtAvailableDays As Double, ByVal txtWeeklyOff As Double, ByVal ddlAgentHoursDaily As Double, ByVal ddlNetAgentHoursDaily As Double, ByVal txtBreak As Double, ByVal ddlIBMEarnedLeaves As Double, ByVal txtEarnedLeave As Double, ByVal ddlIBMCasualLeaves As Double, ByVal txtCasualLeave As Double, ByVal ddlIBMSickLeaves As Double, ByVal txtSickLeave As Double, ByVal ddlIBMFestiveLeaves As Double, ByVal txtFestiveleave As Double, ByVal ddltrainingDay As Double, ByVal txtTrainingDays As Double, ByVal txtwkdaysyearly As Double, ByVal txtwkdaysmonthly As Double, ByVal txtdlveryagentperyear As Double, ByVal ddlsrinkdwntime As Double, ByVal txtnetdlvryperyear As Double, ByVal txtdlytrgetprohour As Double, ByVal ddlAddPlanOvertime As Double, ByVal txtHoursDeliveryOverTime As Double, ByVal txtDailyProdHoursot As Double, ByVal txtmthlyProdHours As Double, ByVal txtmthlyProdHoursot As Double, ByVal savedby As String)
        comdepart = New SqlCommand("select caculatername from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim calculater As String = ""
        While dr.Read
            If calculater = "" Then

                calculater = dr("caculatername").ToString
            Else
                calculater = calculater & "," & dr("caculatername").ToString
            End If

        End While
        connection.Close()
        dr.Close()

        If calculater <> "" Then

            Return "3"
        End If





        comdepart = New SqlCommand("select caculatername from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If dr("caculatername") = calculatername Then
                Dim check As String = "1"
                Return check
            End If

        End While
        connection.Close()
        dr.Close()
        comdepart = New SqlCommand("insert into LOBCLIENTSPECIFICATIONScalculater values(@calculatername,@ddldaysinyear,@ddlweeklyoffs,@txtAvailableDays,@txtWeeklyOff,@ddlAgentHoursDaily,@ddlNetAgentHoursDaily,@txtBreak,@ddlIBMEarnedLeaves,@txtEarnedLeave,@ddlIBMCasualLeaves,@txtCasualLeave,@ddlIBMSickLeaves,@txtSickLeave,@ddlIBMFestiveLeaves,@txtFestiveleave,@ddltrainingDay,@txtTrainingDays,@txtwkdaysyearly,@txtwkdaysmonthly,@txtdlveryagentperyear,@ddlsrinkdwntime,@txtnetdlvryperyear,@txtdlytrgetprohour,@ddlAddPlanOvertime,@txtHoursDeliveryOverTime,@txtDailyProdHoursot,@txtmthlyProdHours,@txtmthlyProdHoursot,@savedby)", connection)
        connection.Open()
        comdepart.Parameters.Add("@calculatername", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@calculatername").Value = calculatername
        comdepart.Parameters.Add("@ddldaysinyear", SqlDbType.Decimal)
        comdepart.Parameters("@ddldaysinyear").Value = ddldaysinyear
        comdepart.Parameters.Add("@ddlweeklyoffs", SqlDbType.Decimal)
        comdepart.Parameters("@ddlweeklyoffs").Value = ddlweeklyoffs
        comdepart.Parameters.Add("@txtAvailableDays", SqlDbType.Decimal)
        comdepart.Parameters("@txtAvailableDays").Value = txtAvailableDays
        comdepart.Parameters.Add("@txtWeeklyOff", SqlDbType.Decimal)
        comdepart.Parameters("@txtWeeklyOff").Value = txtWeeklyOff
        comdepart.Parameters.Add("@ddlAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlAgentHoursDaily").Value = ddlAgentHoursDaily
        comdepart.Parameters.Add("@ddlNetAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlNetAgentHoursDaily").Value = ddlNetAgentHoursDaily

        comdepart.Parameters.Add("@txtBreak", SqlDbType.Decimal)
        comdepart.Parameters("@txtBreak").Value = txtBreak

        comdepart.Parameters.Add("@ddlIBMEarnedLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMEarnedLeaves").Value = ddlIBMEarnedLeaves

        comdepart.Parameters.Add("@txtEarnedLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtEarnedLeave").Value = txtEarnedLeave

        comdepart.Parameters.Add("@ddlIBMCasualLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMCasualLeaves").Value = ddlIBMCasualLeaves

        comdepart.Parameters.Add("@txtCasualLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtCasualLeave").Value = txtCasualLeave

        comdepart.Parameters.Add("@ddlIBMSickLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMSickLeaves").Value = ddlIBMSickLeaves

        comdepart.Parameters.Add("@txtSickLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtSickLeave").Value = txtSickLeave

        comdepart.Parameters.Add("@ddlIBMFestiveLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMFestiveLeaves").Value = ddlIBMFestiveLeaves

        comdepart.Parameters.Add("@txtFestiveleave", SqlDbType.Decimal)
        comdepart.Parameters("@txtFestiveleave").Value = txtFestiveleave

        comdepart.Parameters.Add("@ddltrainingDay", SqlDbType.Decimal)
        comdepart.Parameters("@ddltrainingDay").Value = ddltrainingDay

        comdepart.Parameters.Add("@txtTrainingDays", SqlDbType.Decimal)
        comdepart.Parameters("@txtTrainingDays").Value = txtTrainingDays

        comdepart.Parameters.Add("@txtwkdaysyearly", SqlDbType.Decimal)
        comdepart.Parameters("@txtwkdaysyearly").Value = txtwkdaysyearly

        comdepart.Parameters.Add("@txtwkdaysmonthly", SqlDbType.Decimal)
        comdepart.Parameters("@txtwkdaysmonthly").Value = txtwkdaysmonthly

        comdepart.Parameters.Add("@txtdlveryagentperyear", SqlDbType.Decimal)
        comdepart.Parameters("@txtdlveryagentperyear").Value = txtdlveryagentperyear

        comdepart.Parameters.Add("@ddlsrinkdwntime", SqlDbType.Decimal)
        comdepart.Parameters("@ddlsrinkdwntime").Value = ddlsrinkdwntime

        comdepart.Parameters.Add("@txtnetdlvryperyear", SqlDbType.Decimal)
        comdepart.Parameters("@txtnetdlvryperyear").Value = txtnetdlvryperyear

        comdepart.Parameters.Add("@txtdlytrgetprohour", SqlDbType.Decimal)
        comdepart.Parameters("@txtdlytrgetprohour").Value = txtdlytrgetprohour

        comdepart.Parameters.Add("@ddlAddPlanOvertime", SqlDbType.Decimal)
        comdepart.Parameters("@ddlAddPlanOvertime").Value = ddlAddPlanOvertime

        comdepart.Parameters.Add("@txtHoursDeliveryOverTime", SqlDbType.Decimal)
        comdepart.Parameters("@txtHoursDeliveryOverTime").Value = txtHoursDeliveryOverTime

        comdepart.Parameters.Add("@txtDailyProdHoursot", SqlDbType.Decimal)
        comdepart.Parameters("@txtDailyProdHoursot").Value = txtDailyProdHoursot

        comdepart.Parameters.Add("@txtmthlyProdHours", SqlDbType.Decimal)
        comdepart.Parameters("@txtmthlyProdHours").Value = txtmthlyProdHours

        comdepart.Parameters.Add("@txtmthlyProdHoursot", SqlDbType.Decimal)
        comdepart.Parameters("@txtmthlyProdHoursot").Value = txtmthlyProdHoursot
        comdepart.Parameters.Add("@savedby", SqlDbType.VarChar, 50)
        comdepart.Parameters("@savedby").Value = savedby

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 2
    End Function
    'Function selectforcphvoice()
    '    comdepart = New SqlCommand("select * from LOBCLIENTSPECIFICATIONScalculater", connection)
    '    connection.Open()
    '    dr = comdepart.ExecuteReader
    '    While dr.Read
    '        If dr("caculatername") = "" Then
    '            Dim check As String = "1"
    '            Return check
    '        End If

    '    End While
    '    connection.Close()
    '    dr.Close()
    '    Return 1
    'End Function
    Function calculatername(ByVal savedby As String)
        comdepart = New SqlCommand("select caculatername from LOBCLIENTSPECIFICATIONScalculater  where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim calculater As String = ""
        While dr.Read
            If calculater = "" Then

                calculater = dr("caculatername").ToString
            Else
                calculater = calculater & "," & dr("caculatername").ToString
            End If

        End While
        connection.Close()
        dr.Close()
        Return calculater
    End Function
    Function savecalculaterFTElobnonvoice(ByVal calculatername As String, ByVal ddldaysinyear As String, ByVal ddlweeklyoffs As String, ByVal txtAvailableDays As Double, ByVal txtWeeklyOff As String, ByVal ddlAgentHoursDaily As Double, ByVal ddlNetAgentHoursDaily As Double, ByVal txtBreak As Double, ByVal ddlIBMEarnedLeaves As Double, ByVal txtEarnedLeave As Double, ByVal ddlIBMCasualLeaves As Double, ByVal txtCasualLeave As Double, ByVal ddlIBMSickLeaves As Double, ByVal txtSickLeave As Double, ByVal ddlIBMFestiveLeaves As Double, ByVal txtFestiveleave As String, ByVal savedby As String)
        comdepart = New SqlCommand("select CalculatorName from FTElobnonvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If dr("CalculatorName") = txtFestiveleave Then
                Dim check As String = "1"
                Return check
            End If

        End While
        connection.Close()
        dr.Close()
        comdepart = New SqlCommand("insert into FTElobnonvoice values(@calculatername,@ddldaysinyear,@ddlweeklyoffs,@txtAvailableDays,@txtWeeklyOff,@ddlAgentHoursDaily,@ddlNetAgentHoursDaily,@txtBreak,@ddlIBMEarnedLeaves,@txtEarnedLeave,@ddlIBMCasualLeaves,@txtCasualLeave,@ddlIBMSickLeaves,@txtSickLeave,@ddlIBMFestiveLeaves,@txtFestiveleave,@savedby)", connection)
        connection.Open()
        comdepart.Parameters.Add("@calculatername", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@calculatername").Value = calculatername
        comdepart.Parameters.Add("@ddldaysinyear", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ddldaysinyear").Value = ddldaysinyear
        comdepart.Parameters.Add("@ddlweeklyoffs", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ddlweeklyoffs").Value = ddlweeklyoffs
        comdepart.Parameters.Add("@txtAvailableDays", SqlDbType.Decimal)
        comdepart.Parameters("@txtAvailableDays").Value = txtAvailableDays
        comdepart.Parameters.Add("@txtWeeklyOff", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@txtWeeklyOff").Value = txtWeeklyOff
        comdepart.Parameters.Add("@ddlAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlAgentHoursDaily").Value = ddlAgentHoursDaily
        comdepart.Parameters.Add("@ddlNetAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlNetAgentHoursDaily").Value = ddlNetAgentHoursDaily

        comdepart.Parameters.Add("@txtBreak", SqlDbType.Decimal)
        comdepart.Parameters("@txtBreak").Value = txtBreak

        comdepart.Parameters.Add("@ddlIBMEarnedLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMEarnedLeaves").Value = ddlIBMEarnedLeaves

        comdepart.Parameters.Add("@txtEarnedLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtEarnedLeave").Value = txtEarnedLeave

        comdepart.Parameters.Add("@ddlIBMCasualLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMCasualLeaves").Value = ddlIBMCasualLeaves

        comdepart.Parameters.Add("@txtCasualLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtCasualLeave").Value = txtCasualLeave

        comdepart.Parameters.Add("@ddlIBMSickLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMSickLeaves").Value = ddlIBMSickLeaves

        comdepart.Parameters.Add("@txtSickLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtSickLeave").Value = txtSickLeave

        comdepart.Parameters.Add("@ddlIBMFestiveLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMFestiveLeaves").Value = ddlIBMFestiveLeaves

        comdepart.Parameters.Add("@txtFestiveleave", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@txtFestiveleave").Value = txtFestiveleave
        comdepart.Parameters.Add("@savedby", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@savedby").Value = savedby


        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 2
    End Function
    Function savecalculaterFTElobvoice(ByVal calculatername As String, ByVal ddldaysinyear As String, ByVal ddlweeklyoffs As String, ByVal txtAvailableDays As Double, ByVal txtWeeklyOff As String, ByVal ddlAgentHoursDaily As Double, ByVal ddlNetAgentHoursDaily As Double, ByVal txtBreak As Double, ByVal ddlIBMEarnedLeaves As Double, ByVal txtEarnedLeave As Double, ByVal ddlIBMCasualLeaves As Double, ByVal txtCasualLeave As Double, ByVal ddlIBMSickLeaves As Double, ByVal txtSickLeave As Double, ByVal ddlIBMFestiveLeaves As Double, ByVal txtFestiveleave As String, ByVal savedby As String)
        comdepart = New SqlCommand("select CalculatorName from FTElobvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If dr("CalculatorName") = txtFestiveleave Then
                Dim check As String = "1"
                Return check
            End If

        End While
        connection.Close()
        dr.Close()
        comdepart = New SqlCommand("insert into FTElobvoice values(@calculatername,@ddldaysinyear,@ddlweeklyoffs,@txtAvailableDays,@txtWeeklyOff,@ddlAgentHoursDaily,@ddlNetAgentHoursDaily,@txtBreak,@ddlIBMEarnedLeaves,@txtEarnedLeave,@ddlIBMCasualLeaves,@txtCasualLeave,@ddlIBMSickLeaves,@txtSickLeave,@ddlIBMFestiveLeaves,@txtFestiveleave,@savedby)", connection)
        connection.Open()
        comdepart.Parameters.Add("@calculatername", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@calculatername").Value = calculatername
        comdepart.Parameters.Add("@ddldaysinyear", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ddldaysinyear").Value = ddldaysinyear
        comdepart.Parameters.Add("@ddlweeklyoffs", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ddlweeklyoffs").Value = ddlweeklyoffs
        comdepart.Parameters.Add("@txtAvailableDays", SqlDbType.Decimal)
        comdepart.Parameters("@txtAvailableDays").Value = txtAvailableDays
        comdepart.Parameters.Add("@txtWeeklyOff", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@txtWeeklyOff").Value = txtWeeklyOff
        comdepart.Parameters.Add("@ddlAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlAgentHoursDaily").Value = ddlAgentHoursDaily
        comdepart.Parameters.Add("@ddlNetAgentHoursDaily", SqlDbType.Decimal)
        comdepart.Parameters("@ddlNetAgentHoursDaily").Value = ddlNetAgentHoursDaily

        comdepart.Parameters.Add("@txtBreak", SqlDbType.Decimal)
        comdepart.Parameters("@txtBreak").Value = txtBreak

        comdepart.Parameters.Add("@ddlIBMEarnedLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMEarnedLeaves").Value = ddlIBMEarnedLeaves

        comdepart.Parameters.Add("@txtEarnedLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtEarnedLeave").Value = txtEarnedLeave

        comdepart.Parameters.Add("@ddlIBMCasualLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMCasualLeaves").Value = ddlIBMCasualLeaves

        comdepart.Parameters.Add("@txtCasualLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtCasualLeave").Value = txtCasualLeave

        comdepart.Parameters.Add("@ddlIBMSickLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMSickLeaves").Value = ddlIBMSickLeaves

        comdepart.Parameters.Add("@txtSickLeave", SqlDbType.Decimal)
        comdepart.Parameters("@txtSickLeave").Value = txtSickLeave

        comdepart.Parameters.Add("@ddlIBMFestiveLeaves", SqlDbType.Decimal)
        comdepart.Parameters("@ddlIBMFestiveLeaves").Value = ddlIBMFestiveLeaves

        comdepart.Parameters.Add("@txtFestiveleave", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@txtFestiveleave").Value = txtFestiveleave
        comdepart.Parameters.Add("@savedby", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@savedby").Value = savedby


        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 2
    End Function

    Function selectfromvoicecph(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim CalculatorName As Double
        Dim calculater As Double
        'Dim FTERequiredot As String
        Dim FTERequired As Double
        Dim HoursRequiredToManage As Double
        Dim Cph_Target As Double
        Dim VolumeForTheMonth As Double
        Dim NetFTEAvailableDailly As Double
        Dim NetProductionHous As Double
        Dim Absenteeism_DT As Double
        'Dim department As String
        ' Dim Client As String
        Dim LOB As String
        Dim Head_Count As Double
        Dim Months As String
        Dim WorkingDaysInAMonth As Double
        Dim Attrition As Double
        Dim stringtable As String
        stringtable = "<table border=2px><caption caption style=background-color:#59afbb;color:White>LOB Detail For Non Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr><tr>"
        stringtable = stringtable & "<td>LOB</td>"
        stringtable = stringtable & "<td >Volume For TheMonth</td>"
        stringtable = stringtable & "<td >CPH Target</td>"

        stringtable = stringtable & "<td > Hours Required To Manage Volume Inventry</td>"
        stringtable = stringtable & "<td >FTE Required Without OT</td>"
        stringtable = stringtable & "<td >Head Count</td>"
        stringtable = stringtable & "<td>Attrition</td>"
        stringtable = stringtable & "<td >Absenteeism + DT</td>"
        stringtable = stringtable & "<td >Net Production Hours For The Month</td>"



        stringtable = stringtable & "<td  style=background-color:Yellow>Hours Required To Manage Volume Inventory-Net Production Hours For The Month</td>"
        stringtable = stringtable & "<td >Hiring Required Without OT</td>"
        stringtable = stringtable & "<td >ADD:OT with Hours Available</td>"

        stringtable = stringtable & "<td >Excess / (Deficit) against Hours Required</td>"
        stringtable = stringtable & "<td  style=background-color:Yellow>FTE Delivered Via OT</td>"
        stringtable = stringtable & "<td >Hiring Required with OT</td>"
        stringtable = stringtable & "<td >Month</td>"

        stringtable = stringtable & "</tr>"
        ''comdepart = New SqlCommand("select Count(*) as counts from FTElobnonvoice", connection)
        ''connection.Open()

        ''Dim count As Integer = 0
        ''dr = comdepart.ExecuteReader
        ''If dr.Read Then
        ''    count = dr("counts")
        ''End If
        ''connection.Close()
        ''dr.Close()
        ''comdepart = New SqlCommand("select isnull(max(autoid),0) as counts from FTElobnonvoice", connection)
        ''connection.Open()

        ''Dim newcount As Integer = 0
        ''dr = comdepart.ExecuteReader
        ''If dr.Read Then
        ''    newcount = dr("counts")
        ''End If
        ''connection.Close()
        ''dr.Close()
        ''Dim i As Integer
        ''count = newcount - count

        ' ''For i = count To newcount
        ''Dim autoid As String = ""
        ''autoid = CType(i, String)


        Dim da As New SqlDataAdapter("select CalculatorName,FTERequiredot,FTERequired,HoursRequiredToManage,Cph_Target,VolumeForTheMonth,NetFTEAvailableDailly,NetProductionHous,Absenteeism_DT,department,Client,LOB,Head_Count,Months,WorkingDaysInAMonth,Attrition  from FTElobnonvoice where savedby='" + savedby + "'", connection)

        ' FTERequiredot, FTERequired, HoursRequiredToManage, Cph_Target, VolumeForTheMonth, NetFTEAvailableDailly, NetProductionHous, Absenteeism_DT, department, Client, LOB, Head_Count, Months, WorkingDaysInAMonth, Attrition
        connection.Open()

        Dim ds As New DataSet
        da.Fill(ds)
        Dim row As DataRow
        Dim column As DataColumn
        Dim asd As Integer = ds.Tables(0).Rows.Count

        For Each row In ds.Tables(0).Rows
            For Each column In ds.Tables(0).Columns

                If column.ColumnName = "LOB" Then
                    LOB = row.Item(column.ColumnName).ToString()

                End If

                If column.ColumnName = "VolumeForTheMonth" Then
                    VolumeForTheMonth = CType(row.Item(column.ColumnName), Double)

                End If

                If column.ColumnName = "Cph_Target" Then
                    Cph_Target = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "HoursRequiredToManage" Then
                    HoursRequiredToManage = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "FTERequired" Then
                    FTERequired = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Head_Count" Then
                    Head_Count = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Attrition" Then
                    Attrition = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Absenteeism_DT" Then
                    Absenteeism_DT = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "NetProductionHous" Then
                    NetProductionHous = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "NetFTEAvailableDailly" Then
                    NetFTEAvailableDailly = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Months" Then
                    Months = row.Item(column.ColumnName).ToString

                End If
            Next


            stringtable = stringtable & "<tr>"
            stringtable = stringtable & "<td>" & LOB & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(VolumeForTheMonth) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Cph_Target) & "</td>"

            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(FTERequired) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Head_Count) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Attrition) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Absenteeism_DT) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(NetProductionHous) & "</td>"

            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - NetProductionHous) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(FTERequired - NetFTEAvailableDailly) & "</td>"

            stringtable = stringtable & "<td>" & Math.Round(NetProductionHous * valval) & "</td>"
            Dim cumvalue As Double = NetProductionHous * valval
            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - (NetProductionHous + cumvalue)) & "</td>"
            Dim cumvalue1 As Double = HoursRequiredToManage - (NetProductionHous + cumvalue)
            stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
            stringtable = stringtable & "<td>" & Months & "</td>"


            stringtable = stringtable & "</tr>"





        Next
        stringtable = stringtable & "</table>"
        connection.Close()
        '    While dr.Read

        '        LOB = dr("LOB").ToString
        '        VolumeForTheMonth = dr("VolumeForTheMonth")
        '        Cph_Target = dr("Cph_Target").ToString
        '        HoursRequiredToManage = dr("HoursRequiredToManage")
        '        FTERequired = dr("FTERequired")
        '        Head_Count = dr("Head_Count")


        '        Attrition = dr("Attrition")
        '        Absenteeism_DT = dr("Absenteeism_DT")
        '        NetProductionHous = dr("NetProductionHous")

        '        NetFTEAvailableDailly = dr("NetFTEAvailableDailly")

        '        Months = dr("Months")

        '    End While
        '    If Months <> "" Then


        '        stringtable = stringtable & "<td>" & LOB & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(VolumeForTheMonth) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Cph_Target) & "</td>"

        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(FTERequired) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Head_Count) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Attrition) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Absenteeism_DT) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(NetProductionHous) & "</td>"

        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - NetProductionHous) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(FTERequired - NetFTEAvailableDailly) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(NetProductionHous * valval) & "</td>"
        '        Dim cumvalue As Double = NetProductionHous * valval
        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - (NetProductionHous + cumvalue)) & "</td>"
        '        Dim cumvalue1 As Double = HoursRequiredToManage - (NetProductionHous + cumvalue)
        '        stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
        '        stringtable = stringtable & "<td>" & Months & "</td>"
        '    End If
        '    connection.Close()
        '    dr.Close()
        '    stringtable = stringtable & "</tr>"
        'Next
        'stringtable = stringtable & "</table>"
        Return stringtable


    End Function
    Function selectfromvoiceaht(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim CalculatorName As Double
        Dim calculater As Double
        'Dim FTERequiredot As String
        Dim FTERequired As Double
        Dim HoursRequiredToManage As Double
        Dim Cph_Target As Double
        Dim VolumeForTheMonth As Double
        Dim NetFTEAvailableDailly As Double
        Dim NetProductionHous As Double
        Dim Absenteeism_DT As Double
        'Dim department As String
        ' Dim Client As String
        Dim LOB As String
        Dim Head_Count As Double
        Dim Months As String
        Dim WorkingDaysInAMonth As Double
        Dim Attrition As Double
        Dim stringtable As String
        stringtable = "<table border=2px><caption style=background-color:#59afbb;color:White>LOB Detail For Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr><tr>"
        stringtable = stringtable & "<td>LOB</td>"
        stringtable = stringtable & "<td >Volume For TheMonth</td>"
        stringtable = stringtable & "<td >AHT Target</td>"

        stringtable = stringtable & "<td > Hours Required To Manage Volume Inventry</td>"
        stringtable = stringtable & "<td >FTE Required Without OT</td>"
        stringtable = stringtable & "<td >Head Count</td>"
        stringtable = stringtable & "<td>Attrition</td>"
        stringtable = stringtable & "<td >Absenteeism + DT</td>"
        stringtable = stringtable & "<td >Net Production Hours For The Month</td>"
        stringtable = stringtable & "<td  style=background-color:Yellow>Hours Required To Manage Volume Inventry-Net Production Hours For The Month</td>"

        stringtable = stringtable & "<td >Hiring Required Without OT</td>"
        stringtable = stringtable & "<td >ADD:OT with Hours Available</td>"

        stringtable = stringtable & "<td >Excess / (Deficit) against Hours Required</td>"
        stringtable = stringtable & "<td  style=background-color:Yellow>FTE Delivered Via OT</td>"
        stringtable = stringtable & "<td >Hiring Required with OT</td>"
        stringtable = stringtable & "<td >Month</td>"

        stringtable = stringtable & "</tr>"
        'comdepart = New SqlCommand("select Count(*) as counts from FTElobvoice", connection)
        'connection.Open()

        'Dim count As Integer = 0
        'dr = comdepart.ExecuteReader
        'If dr.Read Then
        '    count = dr("counts")
        'End If
        'connection.Close()
        'dr.Close()
        'comdepart = New SqlCommand("select isnull(max(autoid),0) as counts from FTElobvoice", connection)
        'connection.Open()

        'Dim newcount As Integer = 0
        'dr = comdepart.ExecuteReader
        'If dr.Read Then
        '    newcount = dr("counts")
        'End If
        'connection.Close()
        'dr.Close()
        'Dim i As Integer
        'count = newcount - count
        'stringtable = stringtable & "<tr>"
        'For i = count To newcount
        '    Dim autoid As String = ""
        'autoid = CType(i, String)


        'comdepart = New SqlCommand("select CalculatorName,FTERequiredot,FTERequired,HoursRequiredToManage,Cph_Target,VolumeForTheMonth,NetFTEAvailableDailly,NetProductionHous,Absenteeism_DT,department,Client,LOB,Head_Count,Months,WorkingDaysInAMonth,Attrition  from FTElobvoice where autoid='" + autoid + "'", connection)

        '' FTERequiredot, FTERequired, HoursRequiredToManage, Cph_Target, VolumeForTheMonth, NetFTEAvailableDailly, NetProductionHous, Absenteeism_DT, department, Client, LOB, Head_Count, Months, WorkingDaysInAMonth, Attrition
        'connection.Open()
        'dr = comdepart.ExecuteReader
        Dim da As New SqlDataAdapter("select CalculatorName,FTERequiredot,FTERequired,HoursRequiredToManage,Cph_Target,VolumeForTheMonth,NetFTEAvailableDailly,NetProductionHous,Absenteeism_DT,department,Client,LOB,Head_Count,Months,WorkingDaysInAMonth,Attrition  from FTElobvoice where savedby='" + savedby + "'", connection)

        ' FTERequiredot, FTERequired, HoursRequiredToManage, Cph_Target, VolumeForTheMonth, NetFTEAvailableDailly, NetProductionHous, Absenteeism_DT, department, Client, LOB, Head_Count, Months, WorkingDaysInAMonth, Attrition
        connection.Open()

        Dim ds As New DataSet
        da.Fill(ds)
        Dim row As DataRow
        Dim column As DataColumn
        Dim asd As Integer = ds.Tables(0).Rows.Count

        For Each row In ds.Tables(0).Rows
            For Each column In ds.Tables(0).Columns

                If column.ColumnName = "LOB" Then
                    LOB = row.Item(column.ColumnName).ToString()

                End If

                If column.ColumnName = "VolumeForTheMonth" Then
                    VolumeForTheMonth = CType(row.Item(column.ColumnName), Double)

                End If

                If column.ColumnName = "Cph_Target" Then
                    Cph_Target = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "HoursRequiredToManage" Then
                    HoursRequiredToManage = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "FTERequired" Then
                    FTERequired = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Head_Count" Then
                    Head_Count = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Attrition" Then
                    Attrition = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Absenteeism_DT" Then
                    Absenteeism_DT = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "NetProductionHous" Then
                    NetProductionHous = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "NetFTEAvailableDailly" Then
                    NetFTEAvailableDailly = CType(row.Item(column.ColumnName), Double)

                End If
                If column.ColumnName = "Months" Then
                    Months = row.Item(column.ColumnName).ToString

                End If
            Next


            stringtable = stringtable & "<tr>"
            stringtable = stringtable & "<td>" & LOB & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(VolumeForTheMonth) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Cph_Target) & "</td>"

            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(FTERequired) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Head_Count) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Attrition) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(Absenteeism_DT) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(NetProductionHous) & "</td>"

            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - NetProductionHous) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(FTERequired - NetFTEAvailableDailly) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(NetProductionHous * valval) & "</td>"
            Dim cumvalue As Double = NetProductionHous * valval
            stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - (NetProductionHous + cumvalue)) & "</td>"
            Dim cumvalue1 As Double = HoursRequiredToManage - (NetProductionHous + cumvalue)
            stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
            stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
            stringtable = stringtable & "<td>" & Months & "</td>"


            stringtable = stringtable & "</tr>"





        Next
        stringtable = stringtable & "</table>"
        connection.Close()

        '    While dr.Read

        '        LOB = dr("LOB").ToString
        '        VolumeForTheMonth = dr("VolumeForTheMonth")
        '        Cph_Target = dr("Cph_Target").ToString
        '        HoursRequiredToManage = dr("HoursRequiredToManage")
        '        FTERequired = dr("FTERequired")
        '        Head_Count = dr("Head_Count")


        '        Attrition = dr("Attrition")
        '        Absenteeism_DT = dr("Absenteeism_DT")
        '        NetProductionHous = dr("NetProductionHous")

        '        NetFTEAvailableDailly = dr("NetFTEAvailableDailly")
        '        Months = dr("Months")


        '    End While
        '    If Months <> "" Then


        '        stringtable = stringtable & "<td>" & LOB & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(VolumeForTheMonth) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Cph_Target) & "</td>"

        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(FTERequired) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Head_Count) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Attrition) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(Absenteeism_DT) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(NetProductionHous) & "</td>"

        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - NetProductionHous) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(FTERequired - NetFTEAvailableDailly) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(NetProductionHous * valval) & "</td>"
        '        Dim cumvalue As Double = NetProductionHous * valval
        '        stringtable = stringtable & "<td>" & Math.Round(HoursRequiredToManage - (NetProductionHous + cumvalue)) & "</td>"
        '        Dim cumvalue1 As Double = HoursRequiredToManage - (NetProductionHous + cumvalue)
        '        stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
        '        stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
        '        stringtable = stringtable & "<td>" & Months & "</td>"
        '    End If
        '    connection.Close()
        '    dr.Close()
        '    stringtable = stringtable & "</tr>"
        'Next
        'stringtable = stringtable & "</table>"
        Return stringtable


    End Function
    Function ReportForlobvoice(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim stringtable As String
        stringtable = "<table border=2px><caption style=background-color:#59afbb;color:White>LOB Detail For Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr>"
        Dim lobname As String = ""

        comdepart = New SqlCommand("select distinct(LOB) from FTElobvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If lobname = "" Then
                lobname = dr("LOB").ToString()
            Else
                lobname = lobname & "," & dr("LOB").ToString()
            End If

        End While
        connection.Close()
        dr.Close()
        Dim i, j As Integer
        Dim arras = lobname.Split(",")
        i = UBound(arras)
        For j = 0 To i
            stringtable = stringtable & "<tr><td style=background-color:Yellow>WITH OT</td>"

            comdepart = New SqlCommand("select Months from FTElobvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            Dim newcolumn As Integer = 0
            While dr.Read
                newcolumn = newcolumn + 1

                stringtable = stringtable & "<td>" & dr("Months") & "</td>"
                If newcolumn = 3 Then
                    stringtable = stringtable & "<td style=background-color:Yellow>Quarter</td>"
                    newcolumn = 0
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"
            stringtable = stringtable & "<tr>"

            stringtable = stringtable & "<td >" + arras(j) + "</td>"
            Dim nowchk As Integer = 0
            Dim value As String = ""
            Dim ftearray
            Dim n, bmw As Integer
            Dim intvalue As Double
            Dim valnow As Double = 0

            'Dim cumvalue As Double = NetProductionHous * valval

            'Dim cumvalue1 As Double = HoursRequiredToManage - (NetProductionHous + cumvalue)
            'stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"
            'stringtable = stringtable & "<td>" & Math.Round(cumvalue1 / MonthlyProductionHourswith_OT) & "</td>"

            comdepart = New SqlCommand("select FTERequiredot,FTERequired,NetFTEAvailableDailly,LOB,Months,NetProductionHous,HoursRequiredToManage from FTElobvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            While dr.Read
                nowchk = nowchk + 1
                Dim conversion As Double = CType(dr("FTERequiredot"), Double)
                Dim vallaaa As Double = CType(dr("NetProductionHous"), Double) ' * valval)
                Dim NetProductionHous As Double = vallaaa * valval
                Dim vallaaa1 As Double = CType(dr("HoursRequiredToManage"), Double)
                Dim hrumanage As Double = vallaaa1 - (vallaaa + NetProductionHous)

                'Dim newval As Double = CType(dr("MonthlyProductionHourswith_OT"), Double)

                stringtable = stringtable & "<td>" & Math.Round(hrumanage / MonthlyProductionHourswith_OT) & "</td>"
                If value = "" Then
                    value = Math.Round(hrumanage / MonthlyProductionHourswith_OT) 'dr("FTERequiredot")
                Else
                    value = value & "," & Math.Round(hrumanage / MonthlyProductionHourswith_OT) 'dr("FTERequiredot")
                End If
                If nowchk = 3 Then
                    ftearray = value.Split(",")
                    n = UBound(ftearray)
                    For bmw = 0 To n
                        intvalue = CType(ftearray(bmw), Double)

                        valnow = intvalue + valnow

                    Next
                    stringtable = stringtable & "<td>" & Math.Round(valnow) & "</td>"
                    nowchk = 0
                    intvalue = 0
                    valnow = 0
                    value = ""
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"

        Next
        stringtable = stringtable & "</table>"
        Return stringtable
    End Function
    Function ReportForlobvoicewithoutot(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim stringtable As String
        stringtable = "<table border=2px><caption style=background-color:#59afbb;color:White>LOB Detail For Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr>"
        Dim lobname As String = ""

        comdepart = New SqlCommand("select distinct(LOB) from FTElobvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If lobname = "" Then
                lobname = dr("LOB").ToString()
            Else
                lobname = lobname & "," & dr("LOB").ToString()
            End If

        End While
        connection.Close()
        dr.Close()
        Dim i, j As Integer
        Dim arras = lobname.Split(",")
        i = UBound(arras)
        For j = 0 To i
            stringtable = stringtable & "<tr><td style=background-color:Yellow>WITHOUT OT</td>"

            comdepart = New SqlCommand("select Months from FTElobvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            Dim newcolumn As Integer = 0
            While dr.Read
                newcolumn = newcolumn + 1

                stringtable = stringtable & "<td>" & dr("Months") & "</td>"
                If newcolumn = 3 Then
                    stringtable = stringtable & "<td style=background-color:Yellow>Quarter</td>"
                    newcolumn = 0
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"
            stringtable = stringtable & "<tr>"

            stringtable = stringtable & "<td >" + arras(j) + "</td>"
            Dim nowchk As Integer = 0
            Dim value As String = ""
            Dim ftearray
            Dim n, bmw As Integer
            Dim intvalue As Double
            Dim valnow As Double = 0
            comdepart = New SqlCommand("select FTERequiredot,FTERequired,NetFTEAvailableDailly,LOB,Months from FTElobvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            While dr.Read
                nowchk = nowchk + 1
                Dim conversion As Double = CType(dr("FTERequired"), Double)
                Dim newval As Double = CType(dr("NetFTEAvailableDailly"), Double)
                stringtable = stringtable & "<td>" & Math.Round(conversion - newval) & "</td>"

                If value = "" Then
                    value = Math.Round(conversion - newval) ' dr("FTERequired")
                Else
                    value = value & "," & Math.Round(conversion - newval) ' dr("FTERequired")
                End If
                If nowchk = 3 Then
                    ftearray = value.Split(",")
                    n = UBound(ftearray)
                    For bmw = 0 To n
                        intvalue = CType(ftearray(bmw), Double)

                        valnow = intvalue + valnow

                    Next
                    stringtable = stringtable & "<td>" & Math.Round(valnow) & "</td>"
                    nowchk = 0
                    intvalue = 0
                    valnow = 0
                    value = ""
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"

        Next
        stringtable = stringtable & "</table>"
        Return stringtable
    End Function
    Function ReportForlobnonvoice(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim stringtable As String
        stringtable = "<table border=2px><caption  style=background-color:#59afbb;color:White>LOB Detail For Non Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr>"
        Dim lobname As String = ""

        comdepart = New SqlCommand("select distinct(LOB) from FTElobnonvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If lobname = "" Then
                lobname = dr("LOB").ToString()
            Else
                lobname = lobname & "," & dr("LOB").ToString()
            End If

        End While
        connection.Close()
        dr.Close()
        Dim i, j As Integer
        Dim arras = lobname.Split(",")
        i = UBound(arras)
        For j = 0 To i
            stringtable = stringtable & "<tr><td style=background-color:Yellow>WITHOUT OT</td>"

            comdepart = New SqlCommand("select Months from FTElobnonvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            Dim newcolumn As Integer = 0
            While dr.Read
                newcolumn = newcolumn + 1
                stringtable = stringtable & "<td>" & dr("Months") & "</td>"


                If newcolumn = 3 Then
                    stringtable = stringtable & "<td style=background-color:Yellow>Quarter</td>"
                    newcolumn = 0
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"
            stringtable = stringtable & "<tr>"

            stringtable = stringtable & "<td >" + arras(j) + "</td>"
            Dim nowchk As Integer = 0
            Dim value As String = ""
            Dim ftearray
            Dim n, bmw As Integer
            Dim intvalue As Double
            Dim valnow As Double = 0
            comdepart = New SqlCommand("select FTERequiredot,FTERequired,NetFTEAvailableDailly,LOB,Months from FTElobnonvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            While dr.Read
                nowchk = nowchk + 1
                Dim conversion As Double = CType(dr("FTERequired"), Double)
                Dim newval As Double = CType(dr("NetFTEAvailableDailly"), Double)
                stringtable = stringtable & "<td>" & Math.Round(conversion - newval) & "</td>"

                If value = "" Then
                    value = Math.Round(conversion - newval) 'dr("FTERequired")
                Else
                    value = value & "," & Math.Round(conversion - newval) ' dr("FTERequired")
                End If
                If nowchk = 3 Then
                    ftearray = value.Split(",")
                    n = UBound(ftearray)
                    For bmw = 0 To n
                        intvalue = CType(ftearray(bmw), Double)

                        valnow = intvalue + valnow

                    Next
                    stringtable = stringtable & "<td>" & Math.Round(valnow) & "</td>"
                    nowchk = 0
                    intvalue = 0
                    valnow = 0
                    value = ""
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"

        Next
        stringtable = stringtable & "</table>"
        Return stringtable
    End Function
    Function ReportForlobnonvoicewithotvalue(ByVal savedby As String)
        comdepart = New SqlCommand("select AddPLANNEDOVERTIME,MonthlyProductionHourswith_OT from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        Dim AddPLANNEDOVERTIME As Double
        Dim MonthlyProductionHourswith_OT As Double
        While dr.Read
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")

        End While
        Dim valval As Double = AddPLANNEDOVERTIME / 100
        dr.Close()
        connection.Close()
        Dim stringtable As String
        stringtable = "<table border=2px><caption  style=background-color:#59afbb;color:White>LOB Detail For Non Voice</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr>"
        Dim lobname As String = ""

        comdepart = New SqlCommand("select distinct(LOB) from FTElobnonvoice where savedby='" + savedby + "'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            If lobname = "" Then
                lobname = dr("LOB").ToString()
            Else
                lobname = lobname & "," & dr("LOB").ToString()
            End If

        End While
        connection.Close()
        dr.Close()
        Dim i, j As Integer
        Dim arras = lobname.Split(",")
        i = UBound(arras)
        For j = 0 To i
            stringtable = stringtable & "<tr><td style=background-color:Yellow>WITH OT</td>"

            comdepart = New SqlCommand("select Months from FTElobnonvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            Dim newcolumn As Integer = 0
            While dr.Read
                newcolumn = newcolumn + 1
                stringtable = stringtable & "<td>" & dr("Months") & "</td>"


                If newcolumn = 3 Then
                    stringtable = stringtable & "<td style=background-color:Yellow>Quarter</td>"
                    newcolumn = 0
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"
            stringtable = stringtable & "<tr>"

            stringtable = stringtable & "<td >" + arras(j) + "</td>"
            Dim nowchk As Integer = 0
            Dim value As String = ""
            Dim ftearray
            Dim n, bmw As Integer
            Dim intvalue As Double
            Dim valnow As Double = 0
            comdepart = New SqlCommand("select FTERequiredot,NetFTEAvailableDailly,FTERequired,LOB,Months,NetProductionHous,HoursRequiredToManage from FTElobnonvoice where lob='" + arras(j) + "' and savedby='" + savedby + "'", connection)
            connection.Open()
            dr = comdepart.ExecuteReader
            While dr.Read
                nowchk = nowchk + 1
                Dim conversion As Double = CType(dr("FTERequiredot"), Double)
                Dim vallaaa As Double = CType(dr("NetProductionHous"), Double) ' * valval)
                Dim NetProductionHous As Double = vallaaa * valval
                Dim vallaaa1 As Double = CType(dr("HoursRequiredToManage"), Double)
                Dim hrumanage As Double = vallaaa1 - (vallaaa + NetProductionHous)

                'Dim newval As Double = CType(dr("MonthlyProductionHourswith_OT"), Double)

                stringtable = stringtable & "<td>" & Math.Round(hrumanage / MonthlyProductionHourswith_OT) & "</td>"
                If value = "" Then
                    value = Math.Round(hrumanage / MonthlyProductionHourswith_OT) 'dr("FTERequiredot")
                Else
                    value = value & "," & Math.Round(hrumanage / MonthlyProductionHourswith_OT) 'dr("FTERequiredot")
                End If
                If nowchk = 3 Then
                    ftearray = value.Split(",")
                    n = UBound(ftearray)
                    For bmw = 0 To n
                        intvalue = CType(ftearray(bmw), Double)

                        valnow = intvalue + valnow

                    Next
                    stringtable = stringtable & "<td>" & Math.Round(valnow) & "</td>"
                    nowchk = 0
                    intvalue = 0
                    valnow = 0
                    value = ""
                End If
            End While
            dr.Close()
            connection.Close()
            stringtable = stringtable & "</tr>"

        Next
        stringtable = stringtable & "</table>"
        Return stringtable
    End Function
    Public Function bind_htmlreportforcapacityplanning(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String, ByVal userid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sel_htmlreportcapacityplanning", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    Public Function bind_htmlreportforcapacityplanning1(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String, ByVal userid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sel_htmlreportcapacityplanning1", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    Function calculaterdata(ByVal savedby As String)
        Dim stringtable As String
        stringtable = "<table border=2px><caption  style=background-color:#59afbb;color:White>Main Calculator</caption>"
        stringtable = stringtable & "<tr></tr><tr></tr>"

        Dim Caculatername, DAYSINAYEARGROSS, WEEKLYOFFS, AVAILABLEDAYSGROSS, WEEKLYOFFpercentage, GROSSAGENTHOURSDAILY, NETAGENTHOURSDAILY, BREAKpercentage, IBMEarnedLeaves, EarnedLeavepercentage, IBMCasualLeaves, CasualLeavepercentage, IBMSickLeaves, SickLeavepercentage, IBMFestiveLeaves, Festiveleavepercentage, IBMMandatedTrainingDays, TrainingDayspercentage, NETWORKINGDAYSYEARLY, NETWORKINGDAYSMONTHLY, TOTALHOURSDELIVERYAgentYear, AdditionalShrinkageDowntime, NETHOURSDELIVERYAGENTYEAR, DailyTargetProductionHours, AddPLANNEDOVERTIME, HOURSDELIVERYWITHOVERTIME, DailyTargetProductionHourswith_OT, MonthlyProductionHourswo_OT, MonthlyProductionHourswith_OT As Double

        comdepart = New SqlCommand("select * from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "' ", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        If dr.Read Then
            DAYSINAYEARGROSS = dr("DAYSINAYEARGROSS")
            WEEKLYOFFS = dr("WEEKLYOFFS")

            AVAILABLEDAYSGROSS = dr("AVAILABLEDAYSGROSS")
            WEEKLYOFFpercentage = dr("WEEKLYOFFpercentage")

            GROSSAGENTHOURSDAILY = dr("GROSSAGENTHOURSDAILY")

            NETAGENTHOURSDAILY = dr("NETAGENTHOURSDAILY")

            BREAKpercentage = dr("BREAKpercentage")
            IBMEarnedLeaves = dr("IBMEarnedLeaves")

            EarnedLeavepercentage = dr("EarnedLeavepercentage")

            IBMCasualLeaves = dr("IBMCasualLeaves")



            CasualLeavepercentage = dr("CasualLeavepercentage")
            IBMSickLeaves = dr("IBMSickLeaves")
            SickLeavepercentage = dr("SickLeavepercentage")
            IBMFestiveLeaves = dr("IBMFestiveLeaves")

            Festiveleavepercentage = dr("Festiveleavepercentage")
            IBMMandatedTrainingDays = dr("IBMMandatedTrainingDays")
            TrainingDayspercentage = dr("TrainingDayspercentage")
            NETWORKINGDAYSYEARLY = dr("NETWORKINGDAYSYEARLY")
            NETWORKINGDAYSMONTHLY = dr("NETWORKINGDAYSMONTHLY")
            TOTALHOURSDELIVERYAgentYear = dr("TOTALHOURSDELIVERYAgentYear")
            AdditionalShrinkageDowntime = dr("AdditionalShrinkageDowntime")
            NETHOURSDELIVERYAGENTYEAR = dr("NETHOURSDELIVERYAGENTYEAR")
            DailyTargetProductionHours = dr("DailyTargetProductionHours")

            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME")
            HOURSDELIVERYWITHOVERTIME = dr("HOURSDELIVERYWITHOVERTIME")
            DailyTargetProductionHourswith_OT = dr("DailyTargetProductionHourswith_OT")
            MonthlyProductionHourswo_OT = dr("MonthlyProductionHourswo_OT")
            MonthlyProductionHourswith_OT = dr("MonthlyProductionHourswith_OT")
        End If
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td>DAYS IN A YEAR GROSS</td>"
        stringtable = stringtable & "<td>" & DAYSINAYEARGROSS & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >WEEKLY OFFS</td>"
        stringtable = stringtable & "<td >" & WEEKLYOFFS & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >AVAILABLE DAYS GROSS</td>"
        stringtable = stringtable & "<td >" & AVAILABLEDAYSGROSS & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >WEEKLY OFF %</td>"
        stringtable = stringtable & "<td >" & WEEKLYOFFpercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >GROSS AGENT HOURS DAILY</td>"
        stringtable = stringtable & "<td >" & GROSSAGENTHOURSDAILY & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >NET AGENT HOURS DAILY</td>"
        stringtable = stringtable & "<td >" & NETAGENTHOURSDAILY & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td>BREAK %</td>"
        stringtable = stringtable & "<td>" & BREAKpercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >IBM EARNED LEAVES</td>"

        stringtable = stringtable & "<td >" & IBMEarnedLeaves & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >EARNED LEAVES %</td>"

        stringtable = stringtable & "<td >" & EarnedLeavepercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"

        stringtable = stringtable & "<td >IBM CASUAL LEAVES</td>"
        stringtable = stringtable & "<td >" & IBMCasualLeaves & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >CASUAL LEAVES %</td>"

        stringtable = stringtable & "<td >" & CasualLeavepercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >IBM SICK LEAVES</td>"
        stringtable = stringtable & "<td >" & IBMSickLeaves & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"

        stringtable = stringtable & "<td >SICK LEAVES %</td>"
        stringtable = stringtable & "<td >" & SickLeavepercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >IBM FESTIVE LEAVES</td>"
        stringtable = stringtable & "<td >" & IBMFestiveLeaves & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >FESTIVE LEAVES %</td>"
        stringtable = stringtable & "<td >" & Festiveleavepercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >IBM MANDATED TRAININGDAYS</td>"
        stringtable = stringtable & "<td >" & IBMMandatedTrainingDays & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"


        stringtable = stringtable & "<td >TRAININGDAYS %</td>"
        stringtable = stringtable & "<td >" & TrainingDayspercentage & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        'stringtable = stringtable & "<td >FESTIVE LEAVES %</td>"
        stringtable = stringtable & "<td >NETWORKING DAYS YEARLY</td>"
        stringtable = stringtable & "<td >" & NETWORKINGDAYSYEARLY & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >NETWORKING DAYS MONTHLY</td>"
        stringtable = stringtable & "<td >" & NETWORKINGDAYSMONTHLY & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >TOTAL HOURS DELIVERY AGENT/YEAR</td>"
        stringtable = stringtable & "<td >" & TOTALHOURSDELIVERYAgentYear & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >ADDITITIONAL SHRINKAGE DOWNTIME</td>"
        stringtable = stringtable & "<td >" & AdditionalShrinkageDowntime & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >NET HOURS DELIVERY /AGENT/ YEAR</td>"
        stringtable = stringtable & "<td >" & NETHOURSDELIVERYAGENTYEAR & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >DAILY TARGET PRODUCTION HOURS</td>"
        stringtable = stringtable & "<td >" & DailyTargetProductionHours & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >ADD: PLANNED OVERTIME</td>"
        stringtable = stringtable & "<td >" & AddPLANNEDOVERTIME & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >HOURS DELIVERY WITH OVERTIME</td>"
        stringtable = stringtable & "<td >" & HOURSDELIVERYWITHOVERTIME & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"


        stringtable = stringtable & "<td >DAILY TARGET PRODUCTION HOURS(WITH OT)</td>"
        stringtable = stringtable & "<td >" & DailyTargetProductionHourswith_OT & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >MONTHLY PRODUCTION HOURS(WITHOUT OT)</td>"

        stringtable = stringtable & "<td >" & MonthlyProductionHourswo_OT & "</td>"
        stringtable = stringtable & "</tr>"
        stringtable = stringtable & "<tr>"
        stringtable = stringtable & "<td >MONTHLY PRODUCTION HOURS(WITH OT)</td>"
        stringtable = stringtable & "<td >" & MonthlyProductionHourswith_OT & "</td>"
        stringtable = stringtable & "</tr>"
        'stringtable = stringtable & "<td >" & DAYSINAYEARGROSS & "</td>"
        'stringtable = stringtable & "</tr>"
        stringtable = stringtable & "</table>"
        Return stringtable
    End Function
    Function deletecalculator(ByVal savedby As String)
        comdepart = New SqlCommand("delete from LOBCLIENTSPECIFICATIONScalculater where savedby='" + savedby + "'", connection)
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        con.Close()
        comdepart = New SqlCommand("delete from voicecph where savedby='" + savedby + "'", con)
        con.Open()
        comdepart.ExecuteNonQuery()
        con.Close()
        comdepart = New SqlCommand("delete from voiceaht where savedby='" + savedby + "'", con)
        con.Open()
        comdepart.ExecuteNonQuery()
        con.Close()
        comdepart = New SqlCommand("delete from FTElobnonvoice  where savedby='" + savedby + "'", con)
        con.Open()
        comdepart.ExecuteNonQuery()
        con.Close()
        comdepart = New SqlCommand("delete from FTElobvoice  where savedby='" + savedby + "'", con)
        con.Open()
        comdepart.ExecuteNonQuery()
        con.Close()
        
    End Function
    ' ............................................pragya.....................................................

    Public Function bind_htmlreportforhourlydelcalculator(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String, ByVal userid As String)
        ds1.Clear()
        comdepart = New SqlCommand("SP_HtmlHourlyCalculator", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    '..............................................chetan.................................................

    Public Function bind_FTEBASEDCALCULATOR(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String, ByVal userid As String)
        ds1.Clear()

        comdepart = New SqlCommand("sel_FTEBASEDCALCULATORHtml", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function


End Class
