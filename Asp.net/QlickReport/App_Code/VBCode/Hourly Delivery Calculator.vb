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
Imports System.IO 
Imports System.Web.Configuration
Imports System.Configuration.ConfigurationSettings
Imports System.Timers 

Public Class Hourly_Delivery_Calculator_class
    Dim conStr As String = AppSettings("connectionstring")
    Dim con As New SqlConnection(conStr)

    Public Function AgentTotalHoursYearly(ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double) As Double

        Dim AgentTotalHoursYearlyVal As Double
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        Return AgentTotalHoursYearlyVal
    End Function
    Public Function AgentGrossHours(ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, _
    ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim Sumofleaves As Double
        Dim mulby9 As Double
        Dim AgentGrossHoursVal As Double
        AgentTotalHoursYearlyVal = Convert.ToDouble(AgentTotalHoursDaily * DAYSINAYEARGROSS)
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby9 = Sumofleaves * 9
        AgentGrossHoursVal = AgentTotalHoursYearlyVal - mulby9
        Return AgentGrossHoursVal



    End Function
    Public Function AgentNetHoursAvailable(ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, _
    ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        Return AgentNetHoursAvailableVal



    End Function
    Public Function YearlyAdditionalHoursAvaliable(ByVal ClientMandatedHours As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim YearlyAdditionalHoursAvaliableVal As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        YearlyAdditionalHoursAvaliableVal = AgentNetHoursAvailableVal - ClientMandatedHours
        Return YearlyAdditionalHoursAvaliableVal

    End Function
    Public Function MonthlyAdditionalHoursAvailble(ByVal ClientMandatedHours As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim YearlyAdditionalHoursAvaliableVal As Double
        Dim MonthVal As Double
        Dim MonthlyAdditionalHoursAvailbleVal As String
        Dim hours As String
        Dim roundoff As Double
        Dim monthstr As String
        Dim decfind As Integer
        Dim totlength As Integer
        Dim ram As Integer
        Dim rem1 As String
        Dim minutes As String
        Dim min As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        YearlyAdditionalHoursAvaliableVal = AgentNetHoursAvailableVal - ClientMandatedHours
        MonthVal = YearlyAdditionalHoursAvaliableVal / 12
        monthstr = Convert.ToString(MonthVal)
        decfind = monthstr.IndexOf(".")
        If decfind = -1 Then
            min = 0
        Else
            totlength = monthstr.Length
            ram = totlength - decfind
            rem1 = monthstr.Substring(decfind, ram - 1)
            min = System.Math.Round((Convert.ToDouble(rem1) * 60), 0)
        End If
        minutes = Convert.ToString(min)
        roundoff = System.Math.Round(MonthVal, 0)
        hours = Convert.ToString(roundoff)
        MonthlyAdditionalHoursAvailbleVal = (hours + ":" + minutes)
        Return MonthlyAdditionalHoursAvailbleVal


    End Function
    Public Function DailyAdditionalHoursAvailble(ByVal ClientMandatedHours As Double, ByVal NETWORKINGDAYSMONTHLY As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim YearlyAdditionalHoursAvaliableVal As Double
        Dim MonthVal As Double
        Dim dailstr As String
        Dim decfind As Integer
        Dim totlength As Integer
        Dim ram As Integer
        Dim rem1 As String
        Dim DailyVal As Double
        Dim DailyAdditionalHoursAvailbleVal As String
        Dim roundoffdal As Double
        Dim hoursdal As String
        Dim mindal As String
        Dim min As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        YearlyAdditionalHoursAvaliableVal = AgentNetHoursAvailableVal - ClientMandatedHours
        MonthVal = YearlyAdditionalHoursAvaliableVal / 12
        DailyVal = MonthVal / NETWORKINGDAYSMONTHLY
        roundoffdal = System.Math.Round((MonthVal / NETWORKINGDAYSMONTHLY), 0)
        hoursdal = Convert.ToString(roundoffdal)
        dailstr = Convert.ToString(DailyVal)
        decfind = dailstr.IndexOf(".")
        totlength = dailstr.Length
        ram = totlength - decfind
        rem1 = dailstr.Substring(decfind, ram - 1)
        min = System.Math.Round((Convert.ToDouble(rem1) * 60), 0)
        mindal = Convert.ToString(min)
        DailyAdditionalHoursAvailbleVal = (hoursdal + ":" + mindal)
        Return DailyAdditionalHoursAvailbleVal
    End Function

    Public Function AgentNetHoursDailyRequired(ByVal ClientMandatedHours As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim YearlyAdditionalHoursAvaliableVal As Double
        Dim AgentNetHoursDailyRequiredVal As String
        Dim AgentNetHoursSubYearlyAdditionalHours As Double
        Dim DAYSINAYEARGROSSSubSumofleaves As Double
        Dim hoursval As Double
        Dim hoursage As String
        Dim decfind As Integer
        Dim totlength As Integer
        Dim ram As Integer
        Dim rem1 As String
        Dim agentnerstr As String
        Dim roundoffage As Double
        Dim minage As String
        Dim min As Double


        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        YearlyAdditionalHoursAvaliableVal = AgentNetHoursAvailableVal - ClientMandatedHours
        AgentNetHoursSubYearlyAdditionalHours = AgentNetHoursAvailableVal - YearlyAdditionalHoursAvaliableVal
        DAYSINAYEARGROSSSubSumofleaves = DAYSINAYEARGROSS - Sumofleaves
        hoursval = (AgentNetHoursSubYearlyAdditionalHours / DAYSINAYEARGROSSSubSumofleaves)
        roundoffage = System.Math.Round((AgentNetHoursSubYearlyAdditionalHours / DAYSINAYEARGROSSSubSumofleaves) - 1, 0)
        hoursage = Convert.ToString(roundoffage)
        agentnerstr = Convert.ToString(hoursval)
        decfind = agentnerstr.IndexOf(".")
        totlength = agentnerstr.Length
        ram = totlength - decfind
        rem1 = agentnerstr.Substring(decfind, ram - 1)
        min = System.Math.Round((Convert.ToDouble(rem1) * 60), 0)
        minage = Convert.ToString(min)
        AgentNetHoursDailyRequiredVal = (hoursage + ":" + minage)
        Return AgentNetHoursDailyRequiredVal
    End Function

    Public Function AgentNetHoursAvailableWithOT(ByVal WithOTModel As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim withOTModeldiv100 As Double
        Dim mulWithOTModeldiv100 As Double
        Dim AgentNetHoursAvailableWithOTVal As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        withOTModeldiv100 = WithOTModel / 100
        mulWithOTModeldiv100 = AgentNetHoursAvailableVal * withOTModeldiv100
        AgentNetHoursAvailableWithOTVal = AgentNetHoursAvailableVal + mulWithOTModeldiv100
        Return AgentNetHoursAvailableWithOTVal

    End Function
    Public Function DailyNetDeliverableHours(ByVal WithOTModel As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, _
    ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim withOTModeldiv100 As Double
        Dim mulWithOTModeldiv100 As Double
        Dim AgentNetHoursAvailableWithOTVal As Double
        Dim DailyNetDeliverableHoursVal As String
        Dim DAYSINAYEARGROSSsubSumofleaves As Double
        Dim hoursnet As Double
        Dim roundoffnet As Double
        Dim hoursdalnetdeliv As String
        Dim decfind As Integer
        Dim totlength As Integer
        Dim ram As Integer
        Dim rem1 As String
        Dim hoursstr As String
        Dim mindailnetdeliv As String
        Dim min As Double

        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        withOTModeldiv100 = WithOTModel / 100
        mulWithOTModeldiv100 = AgentNetHoursAvailableVal * withOTModeldiv100
        AgentNetHoursAvailableWithOTVal = AgentNetHoursAvailableVal + mulWithOTModeldiv100
        DAYSINAYEARGROSSsubSumofleaves = DAYSINAYEARGROSS - Sumofleaves
        hoursnet = AgentNetHoursAvailableWithOTVal / DAYSINAYEARGROSSsubSumofleaves
        roundoffnet = System.Math.Round((hoursnet), 0)
        hoursdalnetdeliv = Convert.ToString(roundoffnet)
        hoursstr = Convert.ToString(hoursnet)
        decfind = hoursstr.IndexOf(".")
        totlength = hoursstr.Length
        ram = totlength - decfind
        'rem1 = hoursstr.Substring(decfind + 1, rem - 1); 
        rem1 = hoursstr.Substring(decfind + 1, ram - 1)
        min = System.Math.Round((Convert.ToDouble(rem1) * 6), 0)
        mindailnetdeliv = Convert.ToString(min)
        'mindailnetdeliv = Convert.ToString(Convert.ToDouble(rem1) * 6); 
        DailyNetDeliverableHoursVal = (hoursdalnetdeliv + ":" + mindailnetdeliv)
        Return DailyNetDeliverableHoursVal


    End Function
    Public Function YearlyAdditionalHoursAvailbleWithOT(ByVal ClientMandatedHours As Double, ByVal WithOTModel As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As Double
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim withOTModeldiv100 As Double
        Dim mulWithOTModeldiv100 As Double
        Dim AgentNetHoursAvailableWithOTVal As Double
        Dim YearlyAdditionalHoursAvailbleWithOTVal As Double
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        withOTModeldiv100 = WithOTModel / 100
        mulWithOTModeldiv100 = AgentNetHoursAvailableVal * withOTModeldiv100
        AgentNetHoursAvailableWithOTVal = AgentNetHoursAvailableVal + mulWithOTModeldiv100
        YearlyAdditionalHoursAvailbleWithOTVal = System.Math.Round((AgentNetHoursAvailableWithOTVal - ClientMandatedHours), 1)
        Return YearlyAdditionalHoursAvailbleWithOTVal
    End Function
    Public Function MonthlyAdditionalHoursAvailbleWithOT(ByVal ClientMandatedHours As Double, ByVal WithOTModel As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, ByVal IBMEarnedLeaves As Double, _
    ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim withOTModeldiv100 As Double
        Dim mulWithOTModeldiv100 As Double
        Dim AgentNetHoursAvailableWithOTVal As Double
        Dim YearlyAdditionalHoursAvailbleWithOTVal As Double
        Dim hours As Double
        Dim hoursmin1 As Double
        Dim monthstr As String
        Dim roundoffhours As Double
        Dim hoursmonth As String
        Dim decfind As Integer
        Dim totlength As Integer
        Dim ram As Integer
        Dim rem1 As String
        Dim minmonth As String
        Dim min As Double
        Dim MonthlyAdditionalHoursAvailbleWithOTVal As String
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        withOTModeldiv100 = WithOTModel / 100
        mulWithOTModeldiv100 = AgentNetHoursAvailableVal * withOTModeldiv100
        AgentNetHoursAvailableWithOTVal = AgentNetHoursAvailableVal + mulWithOTModeldiv100
        YearlyAdditionalHoursAvailbleWithOTVal = System.Math.Round((AgentNetHoursAvailableWithOTVal - ClientMandatedHours), 1)
        hours = (YearlyAdditionalHoursAvailbleWithOTVal / 12)
        hoursmin1 = hours - 1
        roundoffhours = System.Math.Round((hoursmin1), 0)
        hoursmonth = Convert.ToString(roundoffhours)
        monthstr = Convert.ToString(hours)
        decfind = monthstr.IndexOf(".")
        totlength = monthstr.Length
        ram = totlength - decfind
        'rem1 = monthstr.Substring(decfind + 1, rem - 1); 
        rem1 = monthstr.Substring(decfind + 1, ram - 1)
        min = System.Math.Round((Convert.ToDouble(rem1) * 6), 0)
        minmonth = Convert.ToString(min)
        'minmonth = Convert.ToString(Convert.ToDouble(rem1) * 6); 
        MonthlyAdditionalHoursAvailbleWithOTVal = (hoursmonth + ":" + minmonth)
        Return MonthlyAdditionalHoursAvailbleWithOTVal

    End Function
    Public Function DailyAdditionalHoursAvailbleWithOT(ByVal NETWORKINGDAYSMONTHLY As Double, ByVal ClientMandatedHours As Double, ByVal WithOTModel As Double, ByVal AgentTotalHoursDaily As Double, ByVal DAYSINAYEARGROSS As Double, ByVal WeeklyOffs As Double, _
    ByVal IBMEarnedLeaves As Double, ByVal IBMCasualLeaves As Double, ByVal IBMSickLeaves As Double, ByVal IBMFestiveLeaves As Double, ByVal IBMMandatedTrainingDays As Double) As String
        Dim Sumofleaves As Double
        Dim mulby1 As Double
        Dim mulsubfrmDAYSINAYEARGROSS As Double
        Dim AgentGrossHoursVal As Double
        Dim AgentNetHoursAvailableVal As Double
        Dim mulby9 As Double
        Dim AgentTotalHoursYearlyVal As Double
        Dim withOTModeldiv100 As Double
        Dim mulWithOTModeldiv100 As Double
        Dim AgentNetHoursAvailableWithOTVal As Double
        Dim YearlyAdditionalHoursAvailbleWithOTVal As Double
        Dim hours As Double
        Dim hoursdivbynetworkingdays As Double
        Dim hoursmin1 As Double
        Dim hoursroundoff As Double
        Dim hoursdailyWithOT As String
        Dim minroundoff As Double
        Dim mindailywithOT As String
        Dim DailyAdditionalHoursAvailbleWithOTVal As String
        Sumofleaves = Convert.ToDouble(WeeklyOffs + IBMEarnedLeaves + IBMCasualLeaves + IBMSickLeaves + IBMFestiveLeaves + IBMMandatedTrainingDays)
        mulby1 = Sumofleaves * 1
        mulsubfrmDAYSINAYEARGROSS = DAYSINAYEARGROSS - mulby1
        mulby9 = Sumofleaves * 9
        AgentTotalHoursYearlyVal = AgentTotalHoursDaily * DAYSINAYEARGROSS
        AgentGrossHoursVal = Convert.ToDouble(AgentTotalHoursYearlyVal - mulby9)
        AgentNetHoursAvailableVal = AgentGrossHoursVal - mulsubfrmDAYSINAYEARGROSS
        withOTModeldiv100 = WithOTModel / 100
        mulWithOTModeldiv100 = AgentNetHoursAvailableVal * withOTModeldiv100
        AgentNetHoursAvailableWithOTVal = AgentNetHoursAvailableVal + mulWithOTModeldiv100
        YearlyAdditionalHoursAvailbleWithOTVal = System.Math.Round((AgentNetHoursAvailableWithOTVal - ClientMandatedHours), 1)
        hours = (YearlyAdditionalHoursAvailbleWithOTVal / 12)
        hoursdivbynetworkingdays = hours / NETWORKINGDAYSMONTHLY
        hoursmin1 = hoursdivbynetworkingdays - 1
        hoursroundoff = System.Math.Round((hoursmin1), 0)
        hoursdailyWithOT = Convert.ToString(hoursroundoff)
        minroundoff = System.Math.Round((hoursdivbynetworkingdays * 60), 0)
        mindailywithOT = Convert.ToString(minroundoff)
        DailyAdditionalHoursAvailbleWithOTVal = (hoursdailyWithOT + ":" + mindailywithOT)
        Return DailyAdditionalHoursAvailbleWithOTVal

    End Function

    Public Function ReportOfHourlyDeliverlyCalculator(ByVal savedby As String)
        Dim ClientMandatedHours As String
        Dim AgentTotalHoursDaily As String
        Dim AgentNetHoursAvailableWoOT As String
        Dim AgentNetHoursDailyRequired As String
        Dim DailyAdditionalHoursAvailbleIfAnyVsClientMandate As String
        Dim AgentNetHoursAvailableWithOT As String
        Dim DailyNetDeliverableHours As String
        Dim DailyAdditionalHoursAvailbleVsClientMandate As String
        Dim stringtable As String
        stringtable = "<table border=2px><caption style=background-color:#59afbb>Hourly Delivery Calculator</caption>"
        stringtable = stringtable + "<tr></tr><tr></tr>"
        stringtable = stringtable + "<td>Client Mandated Hours</td>"
        stringtable = stringtable + "<td>Agent Total Hours(Daily)</td>"
        stringtable = stringtable + "<td>Agent Net Hours Available without OT</td>"
        stringtable = stringtable + "<td>Agent Net Hours Daily Required</td>"
        stringtable = stringtable + "<td>Daily Additional Hours Availble(ifany) vs. Client Mandate</td>"
        stringtable = stringtable + "<td>Agent Net Hours Available With OT</td>"
        stringtable = stringtable + "<td>Daily Net Deliverable Hours</td>"
        stringtable = stringtable + "<td>Daily Additional Hours Availble(ifany) vs. Client Mandate</td>"
        'con = New SqlConnection(WebConfigurationManager.ConnectionStrings("connectionstring").ConnectionString)
        Dim cmd3 As New SqlCommand()
        Dim dr As SqlDataReader
        Dim i As Integer
        cmd3 = New SqlCommand("select * from HourlyDeliveryCalculator where  savedby= '" + savedby + "' ", con)
        cmd3.Connection = con
        con.Open()
        dr = cmd3.ExecuteReader()
        While dr.Read()
            stringtable = stringtable + "<tr>"
            ClientMandatedHours = dr("ClientMandatedHours").ToString()
            AgentTotalHoursDaily = dr("AgentTotalHoursDaily").ToString()
            AgentNetHoursAvailableWoOT = dr("AgentNetHoursAvailableWoOT").ToString()
            AgentNetHoursDailyRequired = dr("AgentNetHoursDailyRequired").ToString()
            DailyAdditionalHoursAvailbleIfAnyVsClientMandate = dr("DailyAdditionalHoursAvailbleIfAnyVsClientMandate").ToString()
            AgentNetHoursAvailableWithOT = dr("AgentNetHoursAvailableWithOT").ToString()
            DailyNetDeliverableHours = dr("DailyNetDeliverableHours").ToString()
            DailyAdditionalHoursAvailbleVsClientMandate = dr("DailyAdditionalHoursAvailbleVsClientMandate").ToString()
            stringtable = stringtable + "<td>" + ClientMandatedHours + "</td>"
            stringtable = stringtable + "<td>" + AgentTotalHoursDaily + "</td>"
            stringtable = stringtable + "<td>" + AgentNetHoursAvailableWoOT + "</td>"
            stringtable = stringtable + "<td>" + AgentNetHoursDailyRequired + "</td>"
            stringtable = stringtable + "<td>" + DailyAdditionalHoursAvailbleIfAnyVsClientMandate + "</td>"
            stringtable = stringtable + "<td>" + AgentNetHoursAvailableWithOT + "</td>"
            stringtable = stringtable + "<td>" + DailyNetDeliverableHours + "</td>"
            stringtable = stringtable + "<td>" + DailyAdditionalHoursAvailbleVsClientMandate + "</td>"

            stringtable = stringtable + "</tr>"
        End While
        con.Close()
        dr.Close()
        stringtable = stringtable + "</table>"
        Return stringtable
    End Function


End Class








