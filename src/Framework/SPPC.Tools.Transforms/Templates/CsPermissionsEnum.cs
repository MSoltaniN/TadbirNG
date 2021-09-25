﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SPPC.Tools.Transforms.Templates
{
    using SPPC.Tools.Utility;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CsPermissionsEnum : CsPermissionsEnumBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 4 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"

string enName = _entity.Entity.Name;
string faSingular = _entity.SingularName;
string faPlural = _entity.PluralName;

            
            #line default
            #line hidden
            this.Write("// ------------------------------------------------------------------------------" +
                    "\r\n// <auto-generated>\r\n//     This code was generated by a tool.\r\n//     Runtime" +
                    " Version: ");
            
            #line 12 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(VersionInfo.GetApiVersion()));
            
            #line default
            #line hidden
            this.Write("\r\n//     Template Version: 1.0\r\n//     Generation Date: ");
            
            #line 14 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write(@"
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// فلگ های تعریف شده برای دسترسی های امنیتی به یک ");
            
            #line 26 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faSingular));
            
            #line default
            #line hidden
            this.Write(" را تعریف می کند\r\n    /// </summary>\r\n    [Flags]\r\n    public enum ");
            
            #line 29 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(enName));
            
            #line default
            #line hidden
            this.Write("Permissions\r\n    {\r\n        /// <summary>\r\n        /// عدم دسترسی به اطلاعات ");
            
            #line 32 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n        None = 0x0,\r\n\r\n        /// <summary>\r\n        /" +
                    "// دسترسی مشاهده لیست ");
            
            #line 37 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write(" یا جزییات یک ");
            
            #line 37 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faSingular));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n        View = 0x1,\r\n\r\n        /// <summary>\r\n        /" +
                    "// دسترسی فیلتر ");
            
            #line 42 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write(" موجود\r\n        /// </summary>\r\n        Filter = 0x2,\r\n\r\n        /// <summary>\r\n " +
                    "       /// دسترسی چاپ ");
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write(" موجود\r\n        /// </summary>\r\n        Print = 0x4,\r\n\r\n        /// <summary>\r\n  " +
                    "      /// دسترسی ارسال اطلاعات ");
            
            #line 52 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write(" موجود\r\n        /// </summary>\r\n        Export = 0x8,\r\n\r\n        /// <summary>\r\n " +
                    "       /// دسترسی ایجاد یک ");
            
            #line 57 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faSingular));
            
            #line default
            #line hidden
            this.Write(" جدید\r\n        /// </summary>\r\n        Create = 0x10,\r\n\r\n        /// <summary>\r\n " +
                    "       /// دسترسی ویرایش یک ");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faSingular));
            
            #line default
            #line hidden
            this.Write(" موجود\r\n        /// </summary>\r\n        Edit = 0x20,\r\n\r\n        /// <summary>\r\n  " +
                    "      /// دسترسی حذف یک ");
            
            #line 67 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faSingular));
            
            #line default
            #line hidden
            this.Write(" موجود\r\n        /// </summary>\r\n        Delete = 0x40,\r\n\r\n        /// <summary>\r\n" +
                    "        /// دسترسی کامل به عملیات تعریف شده برای مدیریت ");
            
            #line 72 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsPermissionsEnum.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(faPlural));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n        All = 0x7f\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CsPermissionsEnumBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
