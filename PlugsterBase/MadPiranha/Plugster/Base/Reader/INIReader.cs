//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MadPiranha.Plugster.Base.Reader
{
    public class INIReader
    {
        string iniFile;
        
        public INIReader(string iniFile)
        {
            this.iniFile = iniFile;
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, iniFile);
        }

        public string ReadValue(string Section, string Key)
        {
            //StringBuilder returnString = new StringBuilder(255);
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(Section, Key, "", returnString,
                                            65536, iniFile);
            List<string> result = new List<string>(returnString.Split('\0'));
            if (result.Count > 0)
                return result[0];
            else return null;

        }

        public List<string> GetSections()
        {
            //StringBuilder returnString = new StringBuilder(255);
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(null, null, null, returnString, 65536, iniFile);
            List<string> result = new List<string>(returnString.Split('\0'));
            if (result.Count > 1)
                result.RemoveRange(result.Count - 2, 2);
            return result;
        }

        public List<string> GetKeys(string category)
        {
            //StringBuilder returnString = new StringBuilder(255);
            string returnString = new string(' ', 32768);
            GetPrivateProfileString(category, null, null, returnString, 32768, iniFile);
            List<string> result = new List<string>(returnString.Split('\0'));
            if(result.Count>1)
                result.RemoveRange(result.Count-2,2);
            return result;
        }

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
              SetLastError = true,
              CharSet = CharSet.Unicode, ExactSpelling = true,
              CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPrivateProfileString(
          string lpAppName,
          string lpKeyName,
          string lpDefault,
          string lpReturnString,
          int nSize,
          string lpFilename);
        [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW",
          SetLastError = true,
          CharSet = CharSet.Unicode, ExactSpelling = true,
          CallingConvention = CallingConvention.StdCall)]
        private static extern int WritePrivateProfileString(
          string lpAppName,
          string lpKeyName,
          string lpString,
          string lpFilename);

        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    }
}
