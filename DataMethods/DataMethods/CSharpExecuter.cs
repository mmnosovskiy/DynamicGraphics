using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace DataMethods
{
    /// <summary>
    /// Класс для проверки валидности кода.
    /// </summary>
    public class CSharpExecuter
    {
        /// <summary>
        /// Код, готовый к выполнению.
        /// </summary>
        string formatedProgramText;
        /// <summary>
        /// Флаг, показывающий успешно ли был исполнен код.
        /// </summary>
        public bool IsSuccessful { get; set; }
        /// <summary>
        /// Сборки, которые  будут подключаться.
        /// </summary>
        public List<string> Refferences { get; set; }
        /// <summary>
        /// Using определения, которые будут добавлены начало кода
        /// </summary>
        public List<string> Usings { get; set; }
        //Предопределенные части программы.
        /// <summary>
        /// Часть кода до исполняемого кода.
        /// </summary>
        readonly string header = @"
            namespace CScript
            {
                public class Script
                {
                    public static void ScriptMethod()
                    {  
            ";
        /// <summary>
        /// Часть кода после исполняемого кода.
        /// </summary>
        readonly string footer = @"
                    }
                }
            }";
        /// <summary>
        /// Конструктор класса CSharpExecuter без параметров.
        /// </summary>
        public CSharpExecuter()
        {
            //Инициализация сборок, которые будут добавлены по умолчанию
            Refferences.AddRange(new string[]
                 {
                    "System.dll"
                 });
            //Инициализация using которые будут добавлены по умолчанию
            Usings.AddRange(new string[]
             {
                    "System"
             });
        }
        /// <summary>
        /// Метод выполнения кода.
        /// </summary>
        public void Execute()
        {
            Execute(formatedProgramText);
        }
        /// <summary>
        /// Метод, в который передается форматированный текст программы.
        /// </summary>
        /// <param name="program"></param>
        public void Execute(string program)
        {
            //Создание класса CSHarpProvider с указанием того, что сборка генерируется в памяти
            var CSHarpProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters compilerParams = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
            };
            //Добавление сборок для компиляции
            compilerParams.ReferencedAssemblies.AddRange(Refferences.ToArray());
            //Компиляция
            var compilerResult = CSHarpProvider.CompileAssemblyFromSource(compilerParams, program);
            if (compilerResult.Errors.Count == 0)
            {
                try
                {
                    //Вызов метода ScriptMethod в сборке которая скомпилировалась
                    compilerResult.CompiledAssembly.GetType("CScript.Script").GetMethod("ScriptMethod").Invoke(null, null);
                    IsSuccessful = true;
                }
                catch
                {
                    IsSuccessful = false;
                }
            }
            else
            {
                IsSuccessful = false;
            }
        }
        /// <summary>
        /// Форматирование кода. Добавление предопределенных частей.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string FormatSources(string text)
        {
            string usings = FormatUsings();
            formatedProgramText = string.Concat(usings, header, text, footer);
            return formatedProgramText;
        }
        /// <summary>
        /// Форматирование определений using.
        /// </summary>
        /// <returns></returns>
        private string FormatUsings()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string using_str in Usings)
                sb.AppendFormat("using {0};{1}", using_str, Environment.NewLine);
            return sb.ToString();
        }
    }
}

