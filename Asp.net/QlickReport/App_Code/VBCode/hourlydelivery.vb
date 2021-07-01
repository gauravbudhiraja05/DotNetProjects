Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration.ConfigurationSettings

Public Class hourlydelivery

    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim con As New SqlConnection(constr)

    Public Function TotalHourstobeDeliveredinaWeek(ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        Return TotalHourstobeDeliveredinaWeekVAL

    End Function



    Public Function TotalHourstobeDeliveredinaYear(ByVal TotalHourstobeDeliveredinaWeek As Double, ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        Dim TotalHourstobeDeliveredinaYearVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        TotalHourstobeDeliveredinaYearVAL = TotalHourstobeDeliveredinaWeekVAL * 52
        Return TotalHourstobeDeliveredinaYearVAL
    End Function

    Public Function AVAILABLEDAYSGROSS(ByVal dayinyear As Double, ByVal weelyoffs As Double) As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        Return AVAILABLEDAYSGROSSVAL
    End Function

    Public Function NETWORKINGDAYS(ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, _
    ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        Return NETWORKINGDAYSVAL

    End Function


    Public Function TOTALHOURSDELIVERYAgentYear(ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double) As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        Return TOTALHOURSDELIVERYAgentYearVAL

    End Function

    Public Function NETHOURSDELIVERYAGENTYEAR(ByVal TOTALHOURSDELIVERYAgentYear As Double, ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, ByVal AdditionalShrinkageDowntime As Double) As Double
        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL
        Return NETHOURSDELIVERYAGENTYEARVAL

    End Function

    Public Function AGENTSREQUIREDtoDeliverTotalHours1(ByVal TotalHourstobeDeliveredinaYear As Double, ByVal TotalHourstobeDeliveredinaWeek As Double, ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double, ByVal NETHOURSDELIVERYAGENTYEAR As Double, _
    ByVal TOTALHOURSDELIVERYAgentYear As Double, ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, ByVal AdditionalShrinkageDowntime As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        Dim TotalHourstobeDeliveredinaYearVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        TotalHourstobeDeliveredinaYearVAL = TotalHourstobeDeliveredinaWeekVAL * 52


        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL

        Dim AGENTSREQUIREDtoDeliverTotalHoursval1 As Double

        AGENTSREQUIREDtoDeliverTotalHoursval1 = System.Math.Round((TotalHourstobeDeliveredinaYearVAL / NETHOURSDELIVERYAGENTYEARVAL), 2)
        Return AGENTSREQUIREDtoDeliverTotalHoursval1

    End Function



    Public Function AddPLANNEDOVERTIME(ByVal NETHOURSDELIVERYAGENTYEAR As Double, ByVal TOTALHOURSDELIVERYAgentYear As Double, ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, _
    ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, _
    ByVal AdditionalShrinkageDowntime As Double, ByVal OVERTIME As Double) As Double
        Dim AddPLANNEDOVERTIMEVAL As Double
        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        Dim d22 As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL
        d22 = NETHOURSDELIVERYAGENTYEARVAL * OVERTIME * 0.01
        AddPLANNEDOVERTIMEVAL = System.Math.Round((NETHOURSDELIVERYAGENTYEARVAL + d22), 2)
        Return AddPLANNEDOVERTIMEVAL

    End Function



    Public Function AGENTSREQUIREDtoDeliverTotalHourswithOT(ByVal TotalHourstobeDeliveredinaWeek As Double, ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double, ByVal NETHOURSDELIVERYAGENTYEAR As Double, ByVal TOTALHOURSDELIVERYAgentYear As Double, _
    ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, ByVal AdditionalShrinkageDowntime As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        Dim TotalHourstobeDeliveredinaYearVAL As Double
        Dim AGENTSREQUIREDtoDeliverTotalHourswoOVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        TotalHourstobeDeliveredinaYearVAL = TotalHourstobeDeliveredinaWeekVAL * 52
        Dim AddPLANNEDOVERTIMEVAL As Double
        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL
        AddPLANNEDOVERTIMEVAL = Convert.ToDouble(NETHOURSDELIVERYAGENTYEARVAL * 1.05)
        AGENTSREQUIREDtoDeliverTotalHourswoOVAL = System.Math.Round((TotalHourstobeDeliveredinaYearVAL / AddPLANNEDOVERTIMEVAL), 2)
        Return AGENTSREQUIREDtoDeliverTotalHourswoOVAL
    End Function


    Public Function AGENTSREQUIREDtoDeliverTotalHour(ByVal AGENTSREQUIREDtoDeliverTotalHours1 As Double, ByVal TotalHourstobeDeliveredinaYear As Double, ByVal TotalHourstobeDeliveredinaWeek As Double, ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double, _
    ByVal NETHOURSDELIVERYAGENTYEAR As Double, ByVal TOTALHOURSDELIVERYAgentYear As Double, ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, _
    ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, _
    ByVal AdditionalShrinkageDowntime As Double, ByVal ATTRITION As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        Dim TotalHourstobeDeliveredinaYearVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        TotalHourstobeDeliveredinaYearVAL = TotalHourstobeDeliveredinaWeekVAL * 52


        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL

        Dim AGENTSREQUIREDtoDeliverTotalHoursval1 As Double

        AGENTSREQUIREDtoDeliverTotalHoursval1 = TotalHourstobeDeliveredinaYearVAL / NETHOURSDELIVERYAGENTYEARVAL

        Dim ATTRITIONval As Double
        Dim d1 As Double
        Dim AGENTSREQUIREDtoDeliverTotalHourval As Double

        d1 = AGENTSREQUIREDtoDeliverTotalHoursval1 * 0.01 * ATTRITION

        AGENTSREQUIREDtoDeliverTotalHourval = System.Math.Round((AGENTSREQUIREDtoDeliverTotalHoursval1 + d1), 2)
        Return AGENTSREQUIREDtoDeliverTotalHourval


    End Function
    Public Function AGENTSREQUIREDtoDeliverTotalHourswith(ByVal AGENTSREQUIREDtoDeliverTotalHourswithOT As Double, ByVal TotalHourstobeDeliveredinaWeek As Double, ByVal ContractualFTESchedules As Double, ByVal ClientMandatedHoursFTEDay As Double, ByVal ClientMandatedWorkingdaysinaWeek As Double, ByVal NETHOURSDELIVERYAGENTYEAR As Double, _
    ByVal TOTALHOURSDELIVERYAgentYear As Double, ByVal NETWORKINGDAYS As Double, ByVal AVAILABLEDAYSGROSS As Double, ByVal dayinyear As Double, ByVal weelyoffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double, ByVal NETAGENTHOURSDAILY As Double, ByVal AdditionalShrinkageDowntime As Double, _
    ByVal ATTRITION As Double) As Double
        Dim TotalHourstobeDeliveredinaWeekVAL As Double
        Dim TotalHourstobeDeliveredinaYearVAL As Double
        Dim AGENTSREQUIREDtoDeliverTotalHourswoOVAL As Double
        TotalHourstobeDeliveredinaWeekVAL = Convert.ToDouble(ContractualFTESchedules * ClientMandatedHoursFTEDay * ClientMandatedWorkingdaysinaWeek)
        TotalHourstobeDeliveredinaYearVAL = TotalHourstobeDeliveredinaWeekVAL * 52
        Dim AddPLANNEDOVERTIMEVAL As Double
        Dim NETHOURSDELIVERYAGENTYEARVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL As Double
        Dim TOTALHOURSDELIVERYAgentYearVAL1 As Double
        Dim NETWORKINGDAYSVAL As Double
        Dim AVAILABLEDAYSGROSSVAL1 As Double
        Dim AVAILABLEDAYSGROSSVAL As Double
        Dim AdditionalShrinkageDowntimeVAL As Double
        AVAILABLEDAYSGROSSVAL = dayinyear - weelyoffs
        AVAILABLEDAYSGROSSVAL1 = Convert.ToDouble(IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        NETWORKINGDAYSVAL = AVAILABLEDAYSGROSSVAL - AVAILABLEDAYSGROSSVAL1
        TOTALHOURSDELIVERYAgentYearVAL = NETWORKINGDAYSVAL * NETAGENTHOURSDAILY
        TOTALHOURSDELIVERYAgentYearVAL1 = TOTALHOURSDELIVERYAgentYearVAL
        AdditionalShrinkageDowntimeVAL = Convert.ToDouble(TOTALHOURSDELIVERYAgentYearVAL1 * (AdditionalShrinkageDowntime * 0.01))
        NETHOURSDELIVERYAGENTYEARVAL = TOTALHOURSDELIVERYAgentYearVAL1 - AdditionalShrinkageDowntimeVAL
        AddPLANNEDOVERTIMEVAL = Convert.ToDouble(NETHOURSDELIVERYAGENTYEARVAL * 1.05)
        AGENTSREQUIREDtoDeliverTotalHourswoOVAL = TotalHourstobeDeliveredinaYearVAL / AddPLANNEDOVERTIMEVAL

        Dim d1 As Double
        Dim AGENTSREQUIREDtoDeliverTotalHourswithval As Double
        d1 = AGENTSREQUIREDtoDeliverTotalHourswoOVAL * 0.01 * ATTRITION
        AGENTSREQUIREDtoDeliverTotalHourswithval = System.Math.Truncate((AGENTSREQUIREDtoDeliverTotalHourswoOVAL + d1))
        Return AGENTSREQUIREDtoDeliverTotalHourswithval
    End Function

    Public Function ReportForftebasedcalculator(ByVal savedby As String)
        Dim ContractualFTESchedules As String
        Dim ClientMandatedHoursFTEDay As String
        Dim ClientMandatedWorkingdaysinaWeek As String
        Dim TotalHourstobeDeliveredinaWeek As String
        Dim TotalHourstobeDeliveredinaYear As String
        Dim GROSSAGENTHOURSDAILY As String
        Dim NETWORKINGDAYS As String
        Dim NETHOURSDELIVERYAGENTYEAR As String
        Dim OVERTIME As String
        Dim AddPLANNEDOVERTIME As String
        Dim ATTRITION As String
        Dim AGENTSREQUIREDtoDeliverTotalHourswoOT As String
        Dim AGENTSREQUIREDtoDeliverTotalHourswithOT As String
        Dim stringtable As String
        Dim comdepart As SqlCommand
        stringtable = "<table border=2px><caption style=background-color:#59afbb>LOB Detail For FTEBASEDCALCULATOR</caption>"
        stringtable = stringtable + "<tr>"
        stringtable = stringtable + "<td>Contractual FTE / Schedules</td>"
        stringtable = stringtable + "<td>Client Mandated Hours/FTE/Day</td>"
        stringtable = stringtable + "<td>Client Mandated Working days in a Week</td>"
        stringtable = stringtable + "<td>Total Hours to be Delivered in a Week</td>"
        stringtable = stringtable + "<td>Total Hours to be Delivered in a Year</td>"
        stringtable = stringtable + "<td>GROSS AGENT HOURS DAILY</td>"
        stringtable = stringtable + "<td>NET WORKING DAYS</td>"
        stringtable = stringtable + "<td>NET HOURS DELIVERY / AGENT / YEAR</td>"
        stringtable = stringtable + "<td>OVERTIME</td>"
        stringtable = stringtable + "<td>Add: PLANNED OVERTIME</td>"
        stringtable = stringtable + "<td>ATTRITION</td>"
        stringtable = stringtable + "<td>AGENTS REQUIRED to Deliver Total Hours (w/o OT)</td>"
        stringtable = stringtable + "<td>AGENTS REQUIRED to Deliver Total Hours (with OT)</td>"
        stringtable = stringtable + "</tr>"
        comdepart = New SqlCommand("select * from FTEBASEDCALCULATOR1 where savedby='" + savedby + "' ", con)
        Dim dr As SqlDataReader
        con.Open()
        dr = comdepart.ExecuteReader()
        While dr.Read()
            stringtable = stringtable + "<tr style=padding:20px>"
            ContractualFTESchedules = dr("ContractualFTESchedules").ToString()
            ClientMandatedHoursFTEDay = dr("ClientMandatedHoursFTEDay").ToString()
            ClientMandatedWorkingdaysinaWeek = dr("ClientMandatedWorkingdaysinaWeek").ToString()
            TotalHourstobeDeliveredinaWeek = dr("TotalHourstobeDeliveredinaWeek").ToString()
            TotalHourstobeDeliveredinaYear = dr("TotalHourstobeDeliveredinaYear").ToString()
            GROSSAGENTHOURSDAILY = dr("GROSSAGENTHOURSDAILY").ToString()
            NETWORKINGDAYS = dr("NETWORKINGDAYS").ToString()
            NETHOURSDELIVERYAGENTYEAR = dr("NETHOURSDELIVERYAGENTYEAR1").ToString()
            OVERTIME = dr("OVERTIME").ToString()
            AddPLANNEDOVERTIME = dr("AddPLANNEDOVERTIME").ToString()
            ATTRITION = dr("ATTRITION").ToString()
            AGENTSREQUIREDtoDeliverTotalHourswoOT = dr("AGENTSREQUIREDtoDeliverTotalHourswoOT").ToString()
            AGENTSREQUIREDtoDeliverTotalHourswithOT = dr("AGENTSREQUIREDtoDeliverTotalHourswithOT").ToString()


            stringtable = stringtable + "<td>" + ContractualFTESchedules + "</td>"
            stringtable = stringtable + "<td>" + ClientMandatedHoursFTEDay + "</td>"
            stringtable = stringtable + "<td>" + ClientMandatedWorkingdaysinaWeek + "</td>"
            stringtable = stringtable + "<td>" + TotalHourstobeDeliveredinaWeek + "</td>"
            stringtable = stringtable + "<td>" + TotalHourstobeDeliveredinaYear + "</td>"
            stringtable = stringtable + "<td>" + GROSSAGENTHOURSDAILY + "</td>"
            stringtable = stringtable + "<td>" + NETWORKINGDAYS + "</td>"
            stringtable = stringtable + "<td>" + NETHOURSDELIVERYAGENTYEAR + "</td>"
            stringtable = stringtable + "<td>" + OVERTIME + "</td>"
            stringtable = stringtable + "<td>" + AddPLANNEDOVERTIME + "</td>"
            stringtable = stringtable + "<td>" + ATTRITION + "</td>"
            stringtable = stringtable + "<td>" + AGENTSREQUIREDtoDeliverTotalHourswoOT + "</td>"
            stringtable = stringtable + "<td>" + AGENTSREQUIREDtoDeliverTotalHourswithOT + "</td>"


            stringtable = stringtable + "</tr>"
        End While
        con.Close()
        dr.Close()
        stringtable = stringtable + "</table>"
        Return stringtable
    End Function
End Class










