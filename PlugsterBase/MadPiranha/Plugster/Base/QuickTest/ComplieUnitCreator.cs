//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace MadPiranha.Plugster.Base.QuickTest
{
    public class CompileUnitCreator
    {
        public const String CSharpCode = 
@"namespace QuickTest {
    using System;
    using MadPiranha.Plugster.Base.Test;
    using MadPiranha.Plugster.Base.Param;
    using System.Threading;
    
    public class Class1 : MadPiranha.Plugster.Base.Test.AbstractTest {
    
        private TextParam textParam;
        private NumberParam numberParam;
    
        public Class1()
        {
            textParam = new TextParam(""Text"", """");
            numberParam = new NumberParam(""Number"", 0);
            AddParameters(new IParam[]{textParam, numberParam});
        }

        public override void ExecuteThis() {
            this.WriteLine(""TODO: do stuffs here..."");
            this.WriteLine(""Number : "" + numberParam.Number);
            this.WriteLine(""Text : "" + textParam.TextValue);
        }
        
        public override string GetDescription() {
            return ""This is a Quick Test."";
        }
        
        public static void Main() {
            System.Console.WriteLine(""Hello World!"");
            System.Console.ReadLine();
        }
    }
}
";


        #region Code templates

        public static CodeCompileUnit BuildHelloWorldGraph()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            CodeNamespace samples = new CodeNamespace("Samples");
            compileUnit.Namespaces.Add(samples);

            samples.Imports.Add(new CodeNamespaceImport("System"));

            CodeTypeDeclaration class1 = new CodeTypeDeclaration("Class1");
            samples.Types.Add(class1);

            CodeEntryPointMethod start = new CodeEntryPointMethod();

            CodeTypeReferenceExpression csSystemConsoleType = new CodeTypeReferenceExpression("System.Console");

            CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(
                csSystemConsoleType, "WriteLine",
                new CodePrimitiveExpression("Hello World!"));

            start.Statements.Add(cs1);

            CodeMethodInvokeExpression cs2 = new CodeMethodInvokeExpression(
                csSystemConsoleType, "WriteLine",
                new CodePrimitiveExpression("Press the Enter key to continue."));

            start.Statements.Add(cs2);

            CodeMethodInvokeExpression csReadLine = new CodeMethodInvokeExpression(
                csSystemConsoleType, "ReadLine");

            start.Statements.Add(csReadLine);

            class1.Members.Add(start);

            return compileUnit;
        }

        public static CodeCompileUnit BuildPlugsterTestExecuteGraph()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            CodeNamespace samples = new CodeNamespace("QuickTest");
            compileUnit.Namespaces.Add(samples);

            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("MadPiranha.Plugster.Base.Test"));
            samples.Imports.Add(new CodeNamespaceImport("MadPiranha.Plugster.Base.Param"));
            samples.Imports.Add(new CodeNamespaceImport("System.Threading"));

            CodeTypeDeclaration class1 = new CodeTypeDeclaration("Class1");
            class1.BaseTypes.Add("MadPiranha.Plugster.Base.Test.AbstractTest");
            samples.Types.Add(class1);

            CodeMemberMethod execThisMeth = new CodeMemberMethod();
            execThisMeth.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            execThisMeth.Name = "ExecuteThis";
            CodeMethodInvokeExpression outputstmt = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "WriteLine", new CodePrimitiveExpression("TODO: do stuffs here..."));
            execThisMeth.Statements.Add(outputstmt);
            execThisMeth.Comments.Add(new CodeCommentStatement("TODO: do stuffs here... "));

            CodeMemberMethod getDescMeth = new CodeMemberMethod();
            getDescMeth.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            getDescMeth.Name = "GetDescription";
            getDescMeth.ReturnType = new CodeTypeReference(typeof(string));
            getDescMeth.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression("This is a Quick Test.")));

            class1.Members.Add(execThisMeth);
            class1.Members.Add(getDescMeth);
            class1.Members.Add(GetEntryPointCode());

            return compileUnit;
        }

        public static CodeEntryPointMethod GetEntryPointCode()
        {
            CodeEntryPointMethod start = new CodeEntryPointMethod();

            CodeTypeReferenceExpression csSystemConsoleType = new CodeTypeReferenceExpression("System.Console");

            start.Statements.Add(new CodeMethodInvokeExpression(
                csSystemConsoleType, "WriteLine",
                new CodePrimitiveExpression("Hello World!")));
            start.Statements.Add(new CodeMethodInvokeExpression(
                csSystemConsoleType, "ReadLine"));

            return start;
        }

        #endregion

        public static string CompileUnitFromSourceTemp(String source)
        {
            StringBuilder sourceBuilder = new StringBuilder(source);
            StringParser sparser = new StringParser(source);

            sourceBuilder.Append( sparser.ReadTill("qwe"));

            return sourceBuilder.ToString();
        }

        public static CodeCompileUnit CompileUnitFromSource(String source)
        {
            CodeCompileUnit ccu = new CodeCompileUnit();
            StringBuilder sourceBuilder = new StringBuilder(source);
            

            return ccu;
        }


    }

    class StringParser
    {
        private StringBuilder source;
        private int cursor;
        public StringParser(string s)
        {
            source = new StringBuilder(s);
            cursor = 0;
        }
        public string ReadTill(char token)
        {
            StringBuilder readstr = new StringBuilder();
            while (cursor < source.Length)
            {
                char c = source[cursor++];
                readstr.Append(c);
                if (c == token) break;
            }
            return readstr.ToString();
        }
        public string ReadTill(string token)
        {
            StringBuilder readstr = new StringBuilder();
            int tokenindex = 0;

            while (cursor < source.Length && tokenindex < token.Length)
            {
                //read string till the first char match
                string firstmatch = ReadTill(token[tokenindex]);    
                readstr.Append(firstmatch);
                tokenindex++;
                //read till either source expires or token expires.
                while (cursor < source.Length && tokenindex < token.Length)
                {
                    char src = source[cursor++];
                    char tokc = token[tokenindex++];
                    readstr.Append(src);
                    //add if the token is matched
                    if (src == tokc)
                    {
                        
                    }
                    else
                    {
                        tokenindex = 0;
                        break;
                    }

                }
                
            }
            return readstr.ToString();
        }
    }
}
