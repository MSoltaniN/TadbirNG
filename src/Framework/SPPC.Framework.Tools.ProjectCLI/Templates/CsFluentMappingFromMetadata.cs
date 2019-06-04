﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SPPC.Framework.Tools.ProjectCLI.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using BabakSoft.Platform.Metadata;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class CsFluentMappingFromMetadata : CsFluentMappingFromMetadataBase
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
            
            #line 10 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_version));
            
            #line default
            #line hidden
            this.Write("\r\n//     Template Version: 1.0\r\n//     Generation Date: ");
            
            #line 12 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
");
            
            #line 22 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

var usingNamespace = NamespaceUtil.GetNamespace(_entity.Repository, "Model", _entity.Area);
var codeNamespace = NamespaceUtil.GetNamespace(_entity.Repository, "Persistence", "Mapping");
var idProperty = _entity.Properties.Where(prop => prop.Name == "Id").First();
var propNames = new string[] { "Id", "RowGuid", "ModifiedDate" };
var customProperties = _entity.Properties.Where(prop => !propNames.Contains(prop.Name)).ToArray();
bool needsRelationMapping = _entity.Relations
    .Where(rel => rel.Multiplicity == RelationMultiplicity.ManyToOne
	    || rel.Multiplicity == RelationMultiplicity.OneToOne)
    .Count() > 0;

            
            #line default
            #line hidden
            this.Write("using ");
            
            #line 33 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(usingNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 35 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(codeNamespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    internal static class ");
            
            #line 37 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("Map\r\n    {\r\n        internal static void BuildMapping(EntityTypeBuilder<");
            
            #line 39 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("> builder)\r\n        {\r\n            builder.ToTable(\"");
            
            #line 41 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("\", \"");
            
            #line 41 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Area));
            
            #line default
            #line hidden
            this.Write("\");\r\n            builder.HasKey(e => e.Id);\r\n            builder.Property(e => e." +
                    "Id)\r\n                .HasColumnName(\"");
            
            #line 44 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(idProperty.Storage.Name));
            
            #line default
            #line hidden
            this.Write("\");\r\n");
            
            #line 45 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

    foreach (var property in customProperties)
    { 
            
            #line default
            #line hidden
            this.Write("            builder.Property(e => e.");
            
            #line 48 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")");
            
            #line 48 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

        if (property.ValidationRule.Required)
        { 
            
            #line default
            #line hidden
            this.Write("\r\n                .IsRequired()");
            
            #line 52 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

        }
        if (property.ValidationRule.Type == ValidationRuleType.Length && !String.IsNullOrEmpty(property.ValidationRule.Maximum))
        { 
            
            #line default
            #line hidden
            this.Write("\r\n                .HasMaxLength(");
            
            #line 57 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.ValidationRule.Maximum));
            
            #line default
            #line hidden
            this.Write(")");
            
            #line 57 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

        } 
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 59 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
  }

            
            #line default
            #line hidden
            this.Write(@"            builder.Property(e => e.RowGuid)
                .HasColumnName(""rowguid"")
                .HasDefaultValueSql(""(newid())"");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType(""datetime"")
                .HasDefaultValueSql(""(getdate())"");
");
            
            #line 67 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

    if (needsRelationMapping)
    { 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 71 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
  }

    foreach (var relation in _entity.Relations)
    {
        if (relation.Multiplicity == RelationMultiplicity.ManyToOne)
        { 
            
            #line default
            #line hidden
            this.Write("            builder.HasOne(e => e.");
            
            #line 77 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write(")\r\n                .WithMany()\r\n");
            
            #line 79 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

            string idPropName = String.Format("{0}Id", relation.EndpointName);
			string idColName = String.Format("{0}ID", relation.EndpointName);
            idProperty = _entity.Properties.Where(prop => prop.Name == idPropName).FirstOrDefault();
            if (idProperty != null)
            { 
            
            #line default
            #line hidden
            this.Write("                .HasForeignKey(e => e.");
            
            #line 85 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(idPropName));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 86 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
          }
            else
            { 
            
            #line default
            #line hidden
            this.Write("                .HasForeignKey(\"");
            
            #line 89 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(idColName));
            
            #line default
            #line hidden
            this.Write("\")\r\n");
            
            #line 90 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
          } 
            
            #line default
            #line hidden
            this.Write("                .OnDelete(DeleteBehavior.ClientSetNull)\r\n                .HasCons" +
                    "traintName(\"FK_");
            
            #line 92 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 92 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 92 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelationArea(relation)));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 92 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write("\");\r\n");
            
            #line 93 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
      }
        else if (relation.Multiplicity == RelationMultiplicity.OneToOne)
        { 
            
            #line default
            #line hidden
            this.Write("            builder.HasOne(e => e.");
            
            #line 96 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write(")\r\n                .WithOne(p => p.");
            
            #line 97 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 98 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"

            if (relation.HasKey)
            { 
            
            #line default
            #line hidden
            this.Write("                .HasForeignKey<");
            
            #line 101 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write(">(\"");
            
            #line 101 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write("ID\")\r\n");
            
            #line 102 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
          }
            else
            { 
            
            #line default
            #line hidden
            this.Write("                .HasForeignKey<");
            
            #line 105 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write(">(\"");
            
            #line 105 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("ID\")\r\n");
            
            #line 106 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
          }

            
            #line default
            #line hidden
            this.Write("                .OnDelete(DeleteBehavior.ClientSetNull)\r\n                .HasCons" +
                    "traintName(\"FK_");
            
            #line 109 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 109 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_entity.Name));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 109 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelationArea(relation)));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 109 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write("\");\r\n");
            
            #line 110 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
      }

            
            #line default
            #line hidden
            
            #line 112 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\CsFluentMappingFromMetadata.tt"
  }

            
            #line default
            #line hidden
            this.Write("        }\r\n    }\r\n}\r\n");
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
    public class CsFluentMappingFromMetadataBase
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
