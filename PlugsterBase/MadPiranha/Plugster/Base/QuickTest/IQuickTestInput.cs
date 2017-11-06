//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Output;

namespace MadPiranha.Plugster.Base.QuickTest
{
    public interface IQuickTestInput
    {
        void AddAssembly(string assemblyName);

        string[] GetReferencedAssemblies();

        bool ShouldGenerateExecutable();

        bool ShouldGenerateInMemory();

        string GetCompilerOptions();

        string GetSource();

        string GetOutputName();

        string GetCodeProvider();

        IOutput GetMessageOut();

        //IOutput GetErrorOut();

        //IOutput GetStandardOut();

        //IOutput GetStandardIn();
    }
}
