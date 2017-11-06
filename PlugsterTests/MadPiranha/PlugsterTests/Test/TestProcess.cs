//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Util;
using MadPiranha.Plugster.Base.Param;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;

namespace MadPiranha.PlugsterTests.Test
{
    public class TestProcess : TestWindows
    {
        //protected NumberParam pidParam;
        protected TextParam filterModuleParam;
        protected BoolParam showDesc;
        protected BoolParam showComments;
        protected BoolParam showVersion;
        protected BoolParam showCompany;
        protected BoolParam showFilePath;
        protected BoolParam showMemory;

        public TestProcess()
        {
            //pidParam = new NumberParam("PID", 0);
            filterModuleParam = new TextParam("Filter", "");         
            showFilePath = new BoolParam("File Path", false);
            showDesc = new BoolParam("Description", false);
            showComments = new BoolParam("Comments", false);
            showVersion = new BoolParam("Version", false);
            showCompany = new BoolParam("Company", false);
            showMemory = new BoolParam("Memory", false);

            parameters = new IParam[] { xyParam, /*pidParam,*/ filterModuleParam, showFilePath, showVersion, showDesc, showCompany, showMemory, showComments };
        }

        public override void ExecuteThis()
        {
            base.BeSilent = true;
            base.ExecuteThis();
            base.BeSilent = false;

            WindowInfo winfo = ListedWindows[0];
            
            WriteLine("Window:" + winfo);
            
            GetModules(winfo.Pid);
        }

        public List<ProcessModule> GetModules(int pid)
        {
            List<ProcessModule> a = new List<ProcessModule>();

            Process p = Process.GetProcessById(pid);
            ProcessModuleCollection pmc = p.Modules;
            IEnumerator ie = pmc.GetEnumerator();
            while (ie.MoveNext())
            {
                ProcessModule pm = (ProcessModule)ie.Current;
                a.Add(pm);

                PrintModule(pm);
            }
            return a;
        }

        private void PrintModule(ProcessModule pm)
        {
            string text = pm.ModuleName + "\t" +
                    (showFilePath.BoolValue ? pm.FileName + "\t" : "") +
                    (showVersion.BoolValue ? pm.FileVersionInfo.FileVersion + "\t" : "") +
                    (showDesc.BoolValue ? pm.FileVersionInfo.FileDescription + "\t" : "") +
                    (showCompany.BoolValue ? pm.FileVersionInfo.CompanyName + "\t" : "") +
                    (showMemory.BoolValue ? pm.ModuleMemorySize + "\t" : "") +
                    (showComments.BoolValue ? pm.FileVersionInfo.Comments + "\t" : "")
                    ;

            if (string.Empty.Equals(filterModuleParam.TextValue)
                || text.ToUpper().Contains(filterModuleParam.TextValue.ToUpper())
                )
            {
                //showFilePath, showVersion, showDesc, showCompany, showMemory, showComments
                WriteLine(text);
            }
        }


        public void asdf()
        {
            string s = System.Console.ReadLine();
            int x = Convert.ToInt32(s);

            List<ProcessModule> lpm = GetModules(x);
            foreach (ProcessModule pm in lpm)
            {
                Console.WriteLine(pm.FileName + " : " + pm.ModuleName);
            }
        }

        public override string GetDescription()
        {
            return "Print Process Modules.";
        }


  #region Interop

  [DllImport("psapi.dll")]
  private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);

  [DllImport("psapi.dll")]
  private static extern uint GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, uint nSize);

  [DllImport("kernel32.dll")]
  private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

  [DllImport("kernel32.dll")]
  private static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

  [DllImport("kernel32.dll")]
  private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint uuchMax);

  [DllImport("kernel32.dll")]
  private static extern IntPtr GetModuleHandle(string lpModuleName);

  [DllImport("user32.dll")]
  private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

  [DllImport("kernel32.dll")]
  private static extern bool CloseHandle(IntPtr hObject);

  [Flags]
  private enum ProcessAccessFlags : uint
  {
   Read = 0x10, // PROCESS_VM_READ
   QueryInformation = 0x400 // PROCESS_QUERY_INFORMATION
  }

  #endregion

  private const uint PathBufferSize = 512; 
  private readonly static StringBuilder _PathBuffer = new StringBuilder((int)PathBufferSize);

  private static string GetExecutablePath(IntPtr hwnd)
  {
   if (hwnd == IntPtr.Zero) { return string.Empty; } // not a valid window handle

   // Get the process id
   uint processid;
   GetWindowThreadProcessId(hwnd, out processid);

   // Try the GetModuleFileName method first since it's the fastest. 
   // May return ACCESS_DENIED (due to VM_READ flag) if the process is not owned by the current user.
   // Will fail if we are compiled as x86 and we're trying to open a 64 bit process...not allowed.
   IntPtr hprocess = OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.Read, false, processid);
   if (hprocess != IntPtr.Zero)
   {
    try
    {
     if (GetModuleFileNameEx(hprocess, IntPtr.Zero, _PathBuffer, PathBufferSize) > 0)
     {
      return _PathBuffer.ToString();
     }
    }
    finally
    {
     CloseHandle(hprocess);
    }
   }

   hprocess = OpenProcess(ProcessAccessFlags.QueryInformation, false, processid);
   if (hprocess != IntPtr.Zero)
   {
    try
    {
     // Try this method for Vista or higher operating systems
     uint size = PathBufferSize;
     if ((Environment.OSVersion.Version.Major >= 6) &&
      (QueryFullProcessImageName(hprocess, 0, _PathBuffer, ref size) && (size > 0)))
     {
      return _PathBuffer.ToString();
     }

     // Try the GetProcessImageFileName method
     if (GetProcessImageFileName(hprocess, _PathBuffer, PathBufferSize) > 0)
     {
      string dospath = _PathBuffer.ToString();
      foreach (string drive in Environment.GetLogicalDrives())
      {
       if (QueryDosDevice(drive.TrimEnd('\\'), _PathBuffer, PathBufferSize) > 0)
       {
        if (dospath.StartsWith(_PathBuffer.ToString()))
        {
         return drive + dospath.Remove(0, _PathBuffer.Length);
        }
       }
      }
     }
    }
    finally
    {
     CloseHandle(hprocess);
    }
   }

   return string.Empty;
  }

  public static string[] GetProcesses()
  {
   List<string> results = new List<string>();
   Process[] procArr = Process.GetProcesses();
   string tmp;
   foreach (Process CurProc in procArr)
   {
    tmp = GetExecutablePath(CurProc.MainWindowHandle);
    if (tmp != string.Empty)
    {
     results.Add(GetExecutablePath(CurProc.MainWindowHandle));
    }
   }
   return results.ToArray();
  }
 }


}

