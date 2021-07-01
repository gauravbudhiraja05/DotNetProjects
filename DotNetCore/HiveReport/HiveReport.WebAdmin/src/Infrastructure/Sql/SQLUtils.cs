using HiveReport.WebAdmin.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HiveReport.WebAdmin.Infrastructure.Sql
{
    public class SQLUtils
    {
        public static string SQLDateTimeToString(DateTime dat)
        {
            string sDateTime;
            // Format as yyyy-MM-dd HH:mm:ss but on our own
            sDateTime = dat.ToString("yyyy-MM-dd HH:mm:ss");
            return sDateTime;
        }

        public static string SQLInjectPatchValue(string sValue, bool bAllowWildCards = false)
        {
            // Patches value to avoid problems when injecting SQL
            // Warning!!! Optional parameters not supported
            if (!string.IsNullOrEmpty(sValue))
            {
                //sValue = sValue.Replace("\'", "\'\'");
                if (!bAllowWildCards)
                {
                    sValue = sValue.Replace("%", "");
                }
            }
            return sValue;
        }

        public static string SQLInject(string sSQL, string sCode, string sValue, bool bAllowWildCards = false)
        {
            // Patches value to avoid problems when injecting SQL
            // Warning!!! Optional parameters not supported
            sValue = SQLInjectPatchValue(sValue, bAllowWildCards);
            // Replaces Code with Value
            sSQL = sSQL.Replace(sCode, sValue);
            return sSQL;
        }

        // Date: Use DATE YMD Format, previously configured in the Queue
        // If you're not using the Queue: Must Use SET DATEFORMAT YMD in the SQL First!!!!
        public static string SQLInject(string sSQL, string sCode, DateTime datDate)
        {
            string dateText = "CONVERT(DATETIME, '%DATE%',102)";
            dateText = dateText.Replace("%DATE%", SQLDateTimeToString(datDate));
            // Inject DateTime as yyyy-MM-dd HH:mm:ss
            return sSQL.Replace(sCode, dateText);
        }

        // Single: Use English Format
        public static string SQLInject(string sSQL, string sCode, float vValue, bool bAllowWildCards = false)
        {
            // Convert Single to String
            // Warning!!! Optional parameters not supported
            string sValue = vValue.ToString(CultureInfo.InvariantCulture);
            // Patches value to avoid problems when injecting SQL
            return SQLInject(sSQL, sCode, sValue, bAllowWildCards);
        }

        public static string SQLInject(string sSQL, string sCode, double vValue, bool bAllowWildCards = false)
        {
            // Convert Single to String
            // Warning!!! Optional parameters not supported
            string sValue = vValue.ToString(CultureInfo.InvariantCulture);
            // Patches value to avoid problems when injecting SQL
            return SQLInject(sSQL, sCode, sValue, bAllowWildCards);
        }

        // Short: Just convert to string
        public static string SQLInject(string sSQL, string sCode, short iShort, bool bAllowWildCards = false)
        {
            // Patches value to avoid problems when injecting SQL
            // Warning!!! Optional parameters not supported
            return SQLInject(sSQL, sCode, iShort.ToString(), bAllowWildCards);
        }

        // Integer: Just convert to string
        public static string SQLInject(string sSQL, string sCode, int iLong, bool bAllowWildCards = false)
        {
            // Patches value to avoid problems when injecting SQL
            // Warning!!! Optional parameters not supported
            return SQLInject(sSQL, sCode, iLong.ToString(), bAllowWildCards);
        }

        public static string SQLInject(string sSQL, string sCode, bool value, bool bAllowWildCards = false)
        {

            return SQLInject(sSQL, sCode, Convert.ToInt32(value), bAllowWildCards);
        }

        // Builds a InlineIf/Case according SQL requirements
        public static string SQLBuildExceptionField(string sFieldCheck, string sFieldCondition = "''", string sFieldOK = "",
            string sFieldKO = "''", bool bAutoAlias = true)
        {
            string sSQL;
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Set Some Defaults
            if ((sFieldOK == ""))
            {
                sFieldOK = sFieldCheck;
            }

            // Access Model
            // sSQL = "iif(%FIELDCHECK%=%FIELDCONDITION%, %FIELDKO%, %FIELDOK%)"
            // SQL Server Model
            sSQL = "CASE WHEN %FIELDCHECK% IS NULL THEN %FIELDKO%";
            sSQL += " WHEN %FIELDCHECK% = %FIELDCONDITION% THEN %FIELDKO% ELSE %FIELDOK% END";
            sSQL = SQLInject(sSQL, "%FIELDCHECK%", sFieldCheck);
            sSQL = SQLInject(sSQL, "%FIELDCONDITION%", sFieldCondition);
            sSQL = SQLInject(sSQL, "%FIELDOK%", sFieldOK);
            sSQL = SQLInject(sSQL, "%FIELDKO%", sFieldKO);
            if (bAutoAlias)
            {
                sSQL += (" " + SQLInject("AS [%ALIAS%]", "%ALIAS%", sFieldCheck));
            }

            // Return Built-SQL for Field
            return sSQL;
        }

        // Builds a InlineIf/Case according SQL requirements
        public static string SQLBuildCheckNumField(string sFieldCheck, string sFieldKOCondition = "", string sFieldOK = "",
            string sFieldKO = "0")
        {
            string sSQL;
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Default OK Field is same Check Field
            if ((sFieldOK == ""))
            {
                sFieldOK = sFieldCheck;
            }

            // SQL Server Model
            sSQL = "CASE WHEN %FIELDCHECK% IS NULL THEN %FIELDKO% ";
            if ((sFieldKOCondition != ""))
            {
                sSQL += " WHEN %FIELDCHECK% %FIELDCONDITION% THEN %FIELDKO% ";
            }

            sSQL += "ELSE %FIELDOK% END";
            sSQL = SQLInject(sSQL, "%FIELDCHECK%", sFieldCheck);
            sSQL = SQLInject(sSQL, "%FIELDCONDITION%", sFieldKOCondition);
            sSQL = SQLInject(sSQL, "%FIELDOK%", sFieldOK);
            sSQL = SQLInject(sSQL, "%FIELDKO%", sFieldKO);
            // Return Built-SQL for Field
            return sSQL;
        }

        public static DateTime SQLDateCheckValidity(DateTime dtDate)
        {
            //  checks that a date is within the validity range of SQL
            DateTime dtOut;
            dtOut = dtDate;
            if ((dtOut.Year < 1900))
            {
                dtOut = dtOut.AddYears((1900 - dtOut.Year));
            }

            return dtOut;
        }

        public static string SQLSubstractLastComa(string sSQL)
        {
            // If exist, removes tail empty spaces. If exist removes last coma
            return sSQL.Trim().TrimEnd(',');
        }
        public static string SQLBuildInList(List<int> valueList)
        {
            StringBuilder inList;

            try
            {
                if (valueList.Count > 0)
                {
                    inList = new StringBuilder(" IN (");

                    foreach (int value in valueList)
                        inList.Append(value).Append(", ");

                    inList = SubstractLastComa(inList);

                    inList.Append(")");
                }
                else
                {
                    inList = new StringBuilder("");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return inList.ToString();
        }
        public static string SQLBuildInList(List<string> valueList)
        {
            string inList = " IN (";

            try
            {
                foreach (string value in valueList)
                    inList += $"'{value}', ";

                inList = SQLSubstractLastComa(inList);
                inList += ")";
            }
            catch (Exception)
            {
                throw;
            }

            return inList;
        }

        public static string SqlFilterDatesFromTo(string whereClausure, string table, string colNameFrom, DateFilterParam dateFrom, string colNameTo, DateFilterParam dateTo)
        {
            string whereSQL = "";

            if (dateFrom.FilterEnabled && dateTo.FilterEnabled)
                whereSQL = SqlFilterBetweenDates(whereClausure, table, colNameFrom, dateFrom, colNameTo, dateTo);
            else if (dateFrom.FilterEnabled)
                whereSQL = whereClausure + SqlDateGreaterWhereClause(table, colNameFrom, dateFrom.DateValue);
            else if (dateTo.FilterEnabled)
                whereSQL = whereClausure + SqlDateLesserWhereClause(table, colNameTo, dateTo.DateValue);

            return whereSQL;
        }
        public static string SqlFilterBetweenDates(string whereClausure, string table, string colNameFrom, DateFilterParam dateFrom, string colNameTo, DateFilterParam dateTo)
        {
            string whereSQL = "";

            // StartDate - DateFilterParam;
            if (dateFrom != null && dateFrom.FilterEnabled)
            {
                string bracket = (dateTo.FilterEnabled) ? " ( " : "";
                whereSQL += whereClausure + bracket + SQLUtils.SqlDateBetweenWhereClause(table, colNameFrom, dateFrom.DateValue, dateTo.DateValue);
            }
            // EndDate - DateFilterParam;
            if (dateTo != null && dateTo.FilterEnabled)
            {
                string bracket = (dateFrom.FilterEnabled) ? " ) " : "";
                whereClausure = (dateFrom.FilterEnabled) ? "\n    AND " : (whereSQL == "") ? whereClausure : " AND ";
                whereSQL += whereClausure + SQLUtils.SqlDateBetweenWhereClause(table, colNameTo, dateFrom.DateValue, dateTo.DateValue) + bracket;
            }

            return whereSQL;
        }
        public static string SqlDateBetweenWhereClause(string table, string dateField, DateTime dateFrom, DateTime dateTo)
        {
            string whereSQL;
            table = (string.IsNullOrWhiteSpace(table)) ? "" : table + ".";
            whereSQL = "( (%TABLE%%DATE_FIELD% >= CONVERT(DATETIME, '%DATE_FROM%',102) ) AND " + " (%TABLE%%DATE_FIELD% <= CONVERT(DATETIME, '%DATE_TO% 23:59:59',102) ) )";
            whereSQL = SQLUtils.SQLInject(whereSQL, "%TABLE%", table);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_FIELD%", dateField);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_FROM%", SqlDateToString(dateFrom));
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_TO%", SqlDateToString(dateTo));
            return whereSQL;
        }

        public static string SqlDateGreaterWhereClause(string table, string dateField, DateTime dateFrom)
        {
            string whereSQL;
            table = (string.IsNullOrWhiteSpace(table)) ? "" : table + ".";
            whereSQL = " (%TABLE%%DATE_FIELD% >= CONVERT(DATETIME, '%DATE_FROM%',102) ) ";
            whereSQL = SQLUtils.SQLInject(whereSQL, "%TABLE%", table);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_FIELD%", dateField);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_FROM%", SqlDateToString(dateFrom));
            return whereSQL;
        }

        public static string SqlDateLesserWhereClause(string table, string dateField, DateTime dateTo)
        {
            string whereSQL;
            table = (string.IsNullOrWhiteSpace(table)) ? "" : table + ".";
            whereSQL = " (%TABLE%%DATE_FIELD% <= CONVERT(DATETIME, '%DATE_TO% 23:59:59',102) ) ";
            whereSQL = SQLUtils.SQLInject(whereSQL, "%TABLE%", table);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_FIELD%", dateField);
            whereSQL = SQLUtils.SQLInject(whereSQL, "%DATE_TO%", SqlDateToString(dateTo));
            return whereSQL;
        }

        public static string SqlDateTSqlFormat(DateTime date)
        {
            string sql;
            sql = "CONVERT(DATETIME, '%DATE% 23:59:59',102)";
            sql = SQLUtils.SQLInject(sql, "%DATE%", SqlDateToString(date));
            return sql;
        }

        public static string SqlDateToString(DateTime dat)
        {
            string dateFormatted;
            dateFormatted = $"{ String.Format("{0000}", dat.Year)} - { String.Format("{00}", dat.Month)} - { String.Format("{00}", dat.Day)}";
            return dateFormatted;
        }
        public static StringBuilder SubstractLastComa(StringBuilder messageSQL)
        {
            StringBuilder msg;
            msg = messageSQL.Replace(",", "", messageSQL.Length - 3, 3);
            return msg;
        }
    }
}
