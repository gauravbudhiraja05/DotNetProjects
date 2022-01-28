using System;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Infrastructure.Utils
{
    public static class StringUtils
    {
        public static string ToCsvString(string textValueComaList, bool surroundValuesWithQuotes = false)
        {
            string result = "";
            string[] valueList = textValueComaList.Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int textIdx = 0; textIdx < valueList.Length; textIdx++)
            {
                if (surroundValuesWithQuotes) result += $"'{valueList[textIdx]}'";
                else result += valueList[textIdx];

                if (textIdx < valueList.Length - 1) result += ", ";
            }
            return result;
        }
        public static List<int> StringComaValuesToListInt(string comaValues)
        {
            List<int> intList = new List<int>();

            if (!String.IsNullOrWhiteSpace(comaValues))
            {
                foreach (string value in comaValues.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        intList.Add(Convert.ToInt32(value));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return intList;
        }
        public static List<string> StringComaValuesToListString(string comaValues)
        {
            List<string> stringList;

            if (!String.IsNullOrWhiteSpace(comaValues))
                stringList = new List<string>(comaValues.Split(",", StringSplitOptions.RemoveEmptyEntries));
            else
                stringList = new List<string>();

            return stringList;
        }

        //Convert  "attr_1" => 1 || "attr_2" => 2
        public static int FreeAttributeToNumber(string freeAttribute)
        {
            int number;

            string[] caracters = freeAttribute.Split("attr_", StringSplitOptions.RemoveEmptyEntries);
            number = Convert.ToInt32(caracters[0]);

            return number;
        }
    }
}
