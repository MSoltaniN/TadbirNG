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
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using SPPC.Tools.Model;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CsPocoFromXmlMetadata : CsPocoFromXmlMetadataBase
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
            
            #line 10 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_version));
            
            #line default
            #line hidden
            this.Write("\r\n//     Template Version: 1.0\r\n//     Generation Date: ");
            
            #line 12 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
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
using System.Collections.Generic;
");
            
            #line 21 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"

foreach(var ns in GetRequiredNamespaces())
{ 
            
            #line default
            #line hidden
            this.Write("using ");
            
            #line 24 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ns));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 25 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
 }

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 28 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
 var codeNamespace = NamespaceUtil.GetNamespace(_repository, "Model", _entity.Area); 
            
            #line default
            #line hidden
            this.Write("namespace ");
            
            #line 29 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(codeNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n");
            
            #line 31 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  if(!String.IsNullOrWhiteSpace(_entity.Description))
    { 
            
            #line default
            #line hidden
            this.Write("    /// <summary>\r\n    /// ");
            
            #line 34 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Description));
            
            #line default
            #line hidden
            this.Write("\r\n    /// </summary>\r\n");
            
            #line 36 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  } 
            
            #line default
            #line hidden
            this.Write("    public partial class ");
            
            #line 37 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 37 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Type));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        /// <summary>\r\n        /// نمونه جدیدی از این کلاس می سازد\r\n    " +
                    "    /// </summary>\r\n        public ");
            
            #line 42 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 44 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"

var textProps = _entity.Properties.Where(prop => prop.Type.ToString() == "String");
foreach(var textProp in textProps)
{ 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 48 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(textProp.Name));
            
            #line default
            #line hidden
            this.Write(" = String.Empty;\r\n");
            
            #line 49 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
 }
var dateProps = _entity.Properties.Where(prop => prop.Type.ToString() == "DateTime");
foreach(var dateProp in dateProps)
{ 
            
            #line default
            #line hidden
            this.Write("            ");
            
            #line 53 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dateProp.Name));
            
            #line default
            #line hidden
            this.Write(" = DateTime.Now;\r\n");
            
            #line 54 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        }\r\n");
            
            #line 56 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"

foreach(var property in GetNonInheritedProperties())
{
    if(!String.IsNullOrWhiteSpace(property.Description))
    { 
            
            #line default
            #line hidden
            this.Write("\r\n        /// <summary>\r\n        /// ");
            
            #line 63 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Description));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n");
            
            #line 65 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  } 
            
            #line default
            #line hidden
            this.Write("        public virtual ");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetTypeAlias(property)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 67 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"

}
foreach (var relation in GetNonInheritedRelations())
{ 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 72 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  if (!String.IsNullOrEmpty(relation.Description))
    { 
            
            #line default
            #line hidden
            this.Write("        /// <summary>\r\n        /// ");
            
            #line 75 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Description));
            
            #line default
            #line hidden
            this.Write("\r\n        /// </summary>\r\n");
            
            #line 77 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  }
    if (relation.Multiplicity == RelationMultiplicity.OneToOne || relation.Multiplicity == RelationMultiplicity.ManyToOne)
    { 
            
            #line default
            #line hidden
            this.Write("        public virtual ");
            
            #line 80 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 80 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 81 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  }
    else if (relation.Multiplicity == RelationMultiplicity.OneToMany)
    { 
            
            #line default
            #line hidden
            this.Write("        public virtual IList<");
            
            #line 84 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 84 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" { get; protected set; }\r\n");
            
            #line 85 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
  }
    else if (relation.Multiplicity == RelationMultiplicity.ManyToMany)
    { 
            
            #line default
            #line hidden
            this.Write("        public virtual IList<");
            
            #line 88 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.JoinTable));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 88 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.JoinTable));
            
            #line default
            #line hidden
            this.Write("s { get; protected set; }\r\n");
            
            #line 89 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\TadbirNG\SPPC.Tools.Transforms\Templates\CsPocoFromXmlMetadata.tt"
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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CsPocoFromXmlMetadataBase
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
