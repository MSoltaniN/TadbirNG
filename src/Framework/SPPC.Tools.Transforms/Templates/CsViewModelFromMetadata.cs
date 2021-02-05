﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SPPC.Tools.Transforms.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class CsViewModelFromMetadata : CsViewModelFromMetadataBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("// ------------------------------------------------------------------------------" +
                    "\r\n// <auto-generated>\r\n//     This code was generated by a tool.\r\n//     Runtime" +
                    " Version: ");
            
            #line 9 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_version));
            
            #line default
            #line hidden
            this.Write("\r\n//     Template Version: 1.0\r\n//     Generation Date: ");
            
            #line 11 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
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
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

");
            
            #line 22 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
 var codeNamespace = NamespaceUtil.GetNamespace(_entity.Repository, "ViewModel", _entity.Area); 
            
            #line default
            #line hidden
            this.Write("namespace ");
            
            #line 23 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(codeNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 25 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
  if(!String.IsNullOrWhiteSpace(_entity.Description))
    { 
            
            #line default
            #line hidden
            this.Write("    /// <summary>\r\n    /// ");
            
            #line 28 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Description));
            
            #line default
            #line hidden
            this.Write("\r\n    /// </summary>\r\n");
            
            #line 30 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
  } 
            
            #line default
            #line hidden
            this.Write("    public partial class ");
            
            #line 31 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("ViewModel : ViewModelBase\r\n    {\r\n        /// <summary>\r\n        /// نمونه جدیدی " +
                    "از این کلاس می سازد\r\n        /// </summary>\r\n        public ");
            
            #line 36 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("ViewModel()\r\n        {\r\n");
            
            #line 38 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"

var textProps = _entity.Properties.Where(prop => prop.Type.ToString() == "String");
foreach(var textProp in textProps)
{ 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 42 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(textProp.Name));
            
            #line default
            #line hidden
            this.Write(" = String.Empty;\r\n");
            
            #line 43 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 45 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"

foreach(var property in _entity.Properties.Where(prop => prop.Name != "RowGuid" && prop.Name != "ModifiedDate"))
{
    if(property.Name == "Id")
    { 
            
            #line default
            #line hidden
            this.Write("\r\n        /// <summary>\r\n        /// شناسه دیتابیسی این موجودیت که به صورت خودکار" +
                    " توسط دیتابیس تولید می شود\r\n        /// </summary>\r\n        public int Id { get;" +
                    " set; }\r\n");
            
            #line 55 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
  }
    else
    {
        if(!String.IsNullOrWhiteSpace(property.Description))
        { 
            
            #line default
            #line hidden
            this.Write("\r\n        /// <summary>\r\n        /// ");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Description));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n");
            
            #line 64 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
      }
        if(property.IsValidated)
        {
            var rule = property.ValidationRule;
            if (rule.Required)
	        { 
            
            #line default
            #line hidden
            this.Write("        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]\r\n");
            
            #line 71 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
          }
            if (HasMinLengthRestrictionOnly(property))
	        { 
            
            #line default
            #line hidden
            this.Write("        [MinLength(");
            
            #line 74 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Minimum));
            
            #line default
            #line hidden
            this.Write(", ErrorMessage = ValidationMessages.TextFieldIsTooShort)]\r\n");
            
            #line 75 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
          }
            if (HasMaxLengthRestrictionOnly(property))
	        { 
            
            #line default
            #line hidden
            this.Write("        [StringLength(");
            
            #line 78 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Maximum));
            
            #line default
            #line hidden
            this.Write(", ErrorMessage = ValidationMessages.TextFieldIsTooLong)]\r\n");
            
            #line 79 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
          }
            if (HasLengthRangeRestriction(property))
	        { 
            
            #line default
            #line hidden
            this.Write("        [StringLength(");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Maximum));
            
            #line default
            #line hidden
            this.Write(", MinimumLength = ");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Minimum));
            
            #line default
            #line hidden
            this.Write(", ErrorMessage = ValidationMessages.TextFieldHasLengthRange)]\r\n");
            
            #line 83 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
          }
            if (HasRangeRestriction(property))
	        { 
            
            #line default
            #line hidden
            this.Write("        [Range(");
            
            #line 86 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Minimum));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 86 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(rule.Maximum));
            
            #line default
            #line hidden
            this.Write(", ErrorMessage = ValidationMessages.NumberHasValueRange)]\r\n");
            
            #line 87 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
          }
        } 
            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 89 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetTypeAlias(property)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 89 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 90 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Tools.Transforms\Templates\CsViewModelFromMetadata.tt"
  }
} 
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class CsViewModelFromMetadataBase
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