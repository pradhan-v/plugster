//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Output;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MadPiranha.Plugster.Base.QuickTest
{
    public class QuickTest
    {

        private IQuickTestInput quickTestInput;

        public QuickTest(IQuickTestInput qti)
        {
            this.quickTestInput = qti;
        }

        public void LoadAssembliesFromCurrentDomain()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                if (!assembly.ManifestModule.Name.StartsWith("Microsoft.VisualStudio"))
                    quickTestInput.AddAssembly(assembly.ManifestModule.Name);
            }
        }

        private CompilerResults CompileCode(CodeDomProvider provider, String exeFile)
        {
            //String[] referenceAssemblies = { "System.dll" };
            String[] referenceAssemblies = quickTestInput.GetReferencedAssemblies();
            CompilerParameters cp = new CompilerParameters(referenceAssemblies,
                                                           exeFile, false);

            string copt = quickTestInput.GetCompilerOptions();
            if (copt != null && !string.Empty.Equals(copt))
                cp.CompilerOptions = copt;

            //cp.

            if (quickTestInput.ShouldGenerateExecutable())
                cp.GenerateExecutable = true;
            else
                cp.GenerateExecutable = false;

            if (quickTestInput.ShouldGenerateInMemory())
                cp.GenerateInMemory = true;

            CompilerResults cr = provider.CompileAssemblyFromSource(cp, quickTestInput.GetSource());
      
            return cr;

        }

        public string GenerateCode(CodeDomProvider provider, CodeCompileUnit compileunit)
        {
            StringWriter codeWriter = new StringWriter();
            IndentedTextWriter tw = new IndentedTextWriter(codeWriter, "    ");

            provider.GenerateCodeFromCompileUnit(compileunit, tw, new CodeGeneratorOptions());

            tw.Close();
            return codeWriter.ToString();
        }

        public string GenerateCode(CodeCompileUnit compileUnit)
        {
            CodeDomProvider provider = GetCurrentProvider();
            return GenerateCode(provider, compileUnit);
        }

        private string currentAssembly;
        public string GetCurrentQuickTestAssembly()
        {
            return currentAssembly;
        }

        public bool Compile()
        {
            IOutput compileroutput = quickTestInput.GetMessageOut();

            CodeDomProvider provider = GetCurrentProvider();
            CompilerResults compileResults = CompileCode(provider, quickTestInput.GetOutputName());

            if (compileResults.Errors.Count > 0)
            {
                compileroutput.WriteLine("Errors encountered while building " +
                                "<whatever you have typed>" + " into " + compileResults.PathToAssembly + ":");
                foreach (CompilerError ce in compileResults.Errors)
                    compileroutput.WriteLine(ce.ToString());

                return false;
            }
            else
            {
                foreach (string ce in compileResults.Output)
                    compileroutput.WriteLine(ce.ToString());

                compileroutput.WriteLine("Source " + "<whatever you have typed>" + " built into " +
                                compileResults.PathToAssembly + " with no errors.");

                currentAssembly = compileResults.PathToAssembly;

                return true;
            }
        }

        public void Execute(String exe, bool capture)
        {          
            ProcessStartInfo psi = new ProcessStartInfo(exe);
            Process proc = new Process();
            proc.StartInfo = psi;

            FileInfo fi = new FileInfo(exe);
            psi.WorkingDirectory = fi.DirectoryName;

            if (capture)
            {
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true; 
                //TODO:
                //psi.RedirectStandardInput = true;

                proc.StartInfo.CreateNoWindow = true;
                psi.UseShellExecute = false;
            }

            bool started = proc.Start();

            if (started && capture)
            {
                StreamReader outputReader = proc.StandardOutput;
                StreamReader errorReader = proc.StandardError;
                //proc.WaitForExit();
                StartReading(proc, outputReader);
                StartReading(proc, errorReader);
            }
        }

        private void StartReading(Process proc, StreamReader reader)
        {
            new Thread(
                new ThreadStart(
                    delegate()
                    {
                        while (!reader.EndOfStream)
                        {
                            try
                            {
                                char[] buffer = new char[255];
                                int count = reader.Read(buffer, 0, 255);
                                quickTestInput.GetMessageOut().Write(proc.MainModule.ModuleName + "[" + proc.Id + "]> " + new String(buffer, 0, count));
                            }
                            catch (Exception e)
                            {
                                quickTestInput.GetMessageOut().WriteLine(e.Message + "\n" + e.StackTrace);
                            }
                            finally
                            {
                                
                            }
                        }
                        reader.Close();
                    }
                )).Start();
        }

        private CodeDomProvider GetCurrentProvider()
        {
            CodeDomProvider provider;
            switch (quickTestInput.GetCodeProvider())
            {
                case "CSharp":
                    provider = CodeDomProvider.CreateProvider("CSharp");
                    break;
                case "Visual Basic":
                    provider = CodeDomProvider.CreateProvider("VisualBasic");
                    break;
                case "JScript":
                    provider = CodeDomProvider.CreateProvider("JScript");
                    break;
                default:
                    provider = CodeDomProvider.CreateProvider("CSharp");
                    break;
            }
            return provider;
        }
    }
}
