using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting;
using System.Reflection.Emit;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace ConsoleApplication1
{
    public class Program
    {
        static string logFile = @"D:\log.txt";
        static StreamWriter fs = new StreamWriter(logFile,true);
       public static void Main(string[] args)
        {
            if (!File.Exists(logFile)) {
                File.Create(logFile);
                }
                    if (args.Length == 0) {
                        string fileName = @"D:\Bowling\bowling-ball\Bowling\bin\Debug\Bowling.dll";
                        //var assembly = CompileAndCreateDll(fileName);
                        var assembly = Assembly.LoadFrom(fileName);
                        IEnumerable<TypeInfo> typeInfoList = assembly.DefinedTypes;
                        foreach(TypeInfo typeinfo in typeInfoList) {
                            Console.WriteLine(typeinfo.Name);
                            IEnumerable<MethodInfo> declaredMethods = typeinfo.DeclaredMethods;
                        IEnumerable<PropertyInfo> declaredProperties = typeinfo.DeclaredProperties;
                        CheckVariableNamingConvention(declaredProperties);
                        CheckMethodNamingConvention(declaredMethods);
                        }
                    }
                    fs.Close();
        }


       private static void CheckMethodNamingConvention(IEnumerable<MethodInfo> declaredMethods)
       { 
            foreach(MethodInfo methodInformation in declaredMethods) {
                string methodName = methodInformation.Name;
                if (char.IsLower(methodName[0])) {
                    string namingConventionWarning = methodName + " Method name should start with Capital letter";
                    fs.WriteLine(methodName);
                }

                //methodInformation.
        //        CheckVariablesInsideMethod(methodInformation.GetMethodBody());
            }
       }

       private static void CheckVariableNamingConvention(IEnumerable<PropertyInfo> declaredProperties)
       {
           foreach(PropertyInfo propertyInformation in declaredProperties)
           {
   
           }   
       }

       private static Assembly CompileAndCreateDll(string fileName)
       {
            CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.OutputAssembly = fileName.Substring(0, fileName.IndexOf("."));
            Console.WriteLine(compilerParameters.OutputAssembly);
            compilerParameters.GenerateInMemory = true;
            CompilerResults results = cSharpCodeProvider.CompileAssemblyFromFile(compilerParameters,fileName);

            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n",
                           error.Line, error.Column, error.ErrorText);
                }
                throw new Exception(errors.ToString());
            }
            else
            {
                return results.CompiledAssembly;
            }
        }
    }
}
