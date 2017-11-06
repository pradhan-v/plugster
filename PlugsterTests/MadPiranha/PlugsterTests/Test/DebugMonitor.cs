//Copyright © MadPiranha 2012-2013

//http://www.codeproject.com/Articles/23776/Mechanism-of-OutputDebugString

using System;
using System.Threading;
using System.Runtime.InteropServices;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.PlugsterTests.Test
{

    public sealed class DebugMonitor : AbstractTest
    {
		private IntPtr ackEvent = IntPtr.Zero;
		private IntPtr readyEvent = IntPtr.Zero;
		private IntPtr sharedFile = IntPtr.Zero;
		private IntPtr sharedMem = IntPtr.Zero;
		private object syncRoot = new object();
		private Mutex mutex = null;

        public DebugMonitor()
        {
            filterPid = new NumberParam("Filter PID", 0);
            filterText = new TextParam("Filter Text", "");
            listen = new BoolParam("Listen", true);
            parameters = new IParam[] { listen, filterPid, filterText };
        }

        #region ITest

        private NumberParam filterPid;
        private TextParam filterText;
        private BoolParam listen;

        public override void ExecuteThis()
        {
            if (listen.BoolValue)
                Start();
            else
                WriteLine("Listen Unchecked");
        }

        public override string GetDescription()
        {
            return "Debug Monitor which prints the text printed using \"OutputDebugString\".";
        }

        public override void Closing()
        {
            listen.BoolValue = false;
        }

        #endregion

		public void Start() {
            WriteLine("Starting...");

            lock (syncRoot) {

				bool createdNew = false;
				mutex = new Mutex(false, typeof(DebugMonitor).Namespace, out createdNew);				
				if (!createdNew)
					throw new ApplicationException("There is already an instance of 'DebugMonitor' running.");

                WriteLine("Init Security Descriptor");
				SECURITY_DESCRIPTOR sd = new SECURITY_DESCRIPTOR();
				
				if (!InitializeSecurityDescriptor(ref sd, SECURITY_DESCRIPTOR_REVISION)) {
                    throw CreateApplicationException("Failed to initialize SECURITY_DESCRIPTOR_REVISION.");
				}

				if (!SetSecurityDescriptorDacl(ref sd, true, IntPtr.Zero, false)) {
					throw CreateApplicationException("Failed to initialize the security descriptor");
				}

				SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();

                WriteLine("Create the event for slot 'DBWIN_BUFFER_READY'");
				ackEvent = CreateEvent(ref sa, false, false, "DBWIN_BUFFER_READY");
				if (ackEvent == IntPtr.Zero) {
					throw CreateApplicationException("Failed to create event 'DBWIN_BUFFER_READY'");
				}

                WriteLine("Create the event for slot 'DBWIN_DATA_READY'");
				readyEvent = CreateEvent(ref sa, false, false, "DBWIN_DATA_READY");
				if (readyEvent == IntPtr.Zero) {
					throw CreateApplicationException("Failed to create event 'DBWIN_DATA_READY'");
				}

                WriteLine("Create a file mapping to slot 'DBWIN_BUFFER'");
				sharedFile = CreateFileMapping(new IntPtr(-1), ref sa, PageProtection.ReadWrite, 0, 4096, "DBWIN_BUFFER");
				if (sharedFile == IntPtr.Zero) {
					throw CreateApplicationException("Failed to create a file mapping to slot 'DBWIN_BUFFER'");
				}

                WriteLine("Create Mapping view for shared file.");
				sharedMem = MapViewOfFile(sharedFile, SECTION_MAP_READ, 0, 0, 512);
				if (sharedMem == IntPtr.Zero) {
					throw CreateApplicationException("Failed to create a mapping view for slot 'DBWIN_BUFFER'");
				}

                WriteLine("Start Capture.");
                Monitor();
			}
		}

		private void Monitor() {
            WriteLine("------------------------------------------------------------------");
			try {
				IntPtr pString = new IntPtr(
					sharedMem.ToInt32() + Marshal.SizeOf(typeof(int))
				);

				while (true) {		
					SetEvent(ackEvent);	

                    int ret = WaitForSingleObject(readyEvent, 1000);

                    if (!listen.BoolValue)
                    {
                        WriteLine("Stopping...");
                        break;
                    }

					if (ret == WAIT_OBJECT_0)  {			
						int pid = Marshal.ReadInt32(sharedMem);
                        string text = Marshal.PtrToStringAnsi(pString);
                        if ((filterPid.Number == 0 || filterPid.Number == pid)
                            && (string.Empty.Equals(filterText.TextValue) || text.ToLower().Contains(filterText.TextValue.ToLower())))
                        {
                            WriteLine(pid + " : " + text);
                        }
					}
				}	

			} catch {
				throw;
			} finally {		
				Dispose();
			}
		}
		
		private void Dispose() {
            WriteLine("Disposing...");

            WriteLine("Closing event DBWIN_BUFFER_READY");
			
			if (ackEvent != IntPtr.Zero) {
				if (!CloseHandle(ackEvent)) {
					throw CreateApplicationException("Failed to close handle for 'AckEvent'");
				}			
				ackEvent = IntPtr.Zero;	
			}

            WriteLine("Closing event DBWIN_DATA_READY");
			if (readyEvent != IntPtr.Zero) {
				if (!CloseHandle(readyEvent)) {
					throw CreateApplicationException("Failed to close handle for 'ReadyEvent'");
				}
				readyEvent = IntPtr.Zero;		
			}

            WriteLine("Closing file handle of the file created for DBWIN_BUFFER");
			if (sharedFile != IntPtr.Zero) {
				if (!CloseHandle(sharedFile)) {
					throw CreateApplicationException("Failed to close handle for 'SharedFile'");
				}
				sharedFile = IntPtr.Zero;
			}

            WriteLine("Unmapping view of file for slot 'DBWIN_BUFFER'");
			if (sharedMem != IntPtr.Zero) {
				if (!UnmapViewOfFile(sharedMem)) {
					throw CreateApplicationException("Failed to unmap view for slot 'DBWIN_BUFFER'");
				}
				sharedMem = IntPtr.Zero;		
			}		

            WriteLine("Closing Mutex.");
			if (mutex != null) {
				mutex.Close();
				mutex = null;
			}

            WriteLine("!DebugMonitor.");
		}

		public void Stop() {		
			lock (syncRoot) {
				PulseEvent(readyEvent);
				while (ackEvent != IntPtr.Zero);			
			}
		}

		private ApplicationException CreateApplicationException(string text) {
			if (text == null || text.Length < 1)
				throw new ArgumentNullException("text", "'text' may not be empty or null.");

			return new ApplicationException(string.Format("{0}. Last Win32 Error was {1}", text, Marshal.GetLastWin32Error()));
		}

        #region Win32 API Imports

        [StructLayout(LayoutKind.Sequential)]
        private struct SECURITY_DESCRIPTOR
        {
            public byte revision;
            public byte size;
            public short control;
            public IntPtr owner;
            public IntPtr group;
            public IntPtr sacl;
            public IntPtr dacl;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [Flags]
        private enum PageProtection : uint
        {
            NoAccess = 0x01,
            Readonly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400,
        }


        private const int WAIT_OBJECT_0 = 0;
        private const uint INFINITE = 0xFFFFFFFF;
        private const int ERROR_ALREADY_EXISTS = 183;

        private const uint SECURITY_DESCRIPTOR_REVISION = 1;

        private const uint SECTION_MAP_READ = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint
            dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool InitializeSecurityDescriptor(ref SECURITY_DESCRIPTOR sd, uint dwRevision);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateEvent(ref SECURITY_ATTRIBUTES sa, bool bManualReset, bool bInitialState, string lpName);

        [DllImport("kernel32.dll")]
        private static extern bool PulseEvent(IntPtr hEvent);

        [DllImport("kernel32.dll")]
        private static extern bool SetEvent(IntPtr hEvent);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile,
            ref SECURITY_ATTRIBUTES lpFileMappingAttributes, PageProtection flProtect, uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        private static extern Int32 WaitForSingleObject(IntPtr handle, uint milliseconds);
        #endregion

    }
}
