﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SPPC.Tadbir.Tools.SystemDesigner.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class SqlCreateTableFromMetadata : SqlCreateTableFromMetadataBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 6 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"

string dbName = "ApplicationDb";
if (_entities.Length > 0)
{
    dbName = _entities[0].Repository.Database;
}

            
            #line default
            #line hidden
            this.Write("USE [");
            
            #line 13 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dbName));
            
            #line default
            #line hidden
            this.Write("]\r\nGO\r\n\r\nSET ANSI_NULLS ON\r\nGO\r\n\r\nSET QUOTED_IDENTIFIER ON\r\nGO\r\n\r\n");
            
            #line 22 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"

foreach (var entity in _entities)
{
    SetupTokenPadding(entity);
    var tableName = String.Format("[{0}].[{1}]", entity.Area, entity.Name);
    var idProperty = entity.Properties.Where(prop => prop.Name == entity.Identifier).FirstOrDefault();

            
            #line default
            #line hidden
            this.Write("/****** Object: Table ");
            
            #line 29 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tableName));
            
            #line default
            #line hidden
            this.Write(" Script Date: ");
            
            #line 29 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(DateTime.Now.ToString()));
            
            #line default
            #line hidden
            this.Write(" ******/\r\nCREATE TABLE ");
            
            #line 30 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tableName));
            
            #line default
            #line hidden
            this.Write(" (\r\n    ");
            
            #line 31 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(idProperty.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 31 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName("INT")));
            
            #line default
            #line hidden
            this.Write("IDENTITY (1, 1) NOT NULL,\r\n");
            
            #line 32 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
 foreach (var relation in entity.Relations.Where(rel => rel.HasKey))
   {
      string fieldName = String.Format("{0}ID", relation.EndpointName); 
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 35 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(fieldName)));
            
            #line default
            #line hidden
            
            #line 35 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName("INT")));
            
            #line default
            #line hidden
            this.Write("NOT NULL,\r\n");
            
            #line 36 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
 }

   var nonIdProps = entity.Properties.Where(prop => prop.Name != entity.Identifier).ToList();
   for (int i = 0; i < nonIdProps.Count; i++)
   {
       var property = nonIdProps.ElementAt(i);
       var nullable = property.Storage.Nullable ? "NULL" : "NOT NULL";
       if (i < nonIdProps.Count - 1)
       {
           if (property.Storage.Name == "rowguid")
           { 
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            this.Write("CONSTRAINT [DF_");
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("_rowguid] DEFAULT (newid()) ROWGUIDCOL ");
            
            #line 47 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 48 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
           else if (property.Name == "ModifiedDate")
           {
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 51 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 51 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            this.Write("CONSTRAINT [DF_");
            
            #line 51 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 51 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("_ModifiedDate] DEFAULT (getdate()) ");
            
            #line 51 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 52 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
           else
           {
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 55 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 55 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            
            #line 55 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 56 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
       }
       else
	   {
           if (property.Storage.Name == "rowguid")
           { 
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            this.Write("CONSTRAINT [DF_");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("_rowguid] DEFAULT (newid()) ROWGUIDCOL ");
            
            #line 62 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 63 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
           else if (property.Name == "ModifiedDate")
           {
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            this.Write("CONSTRAINT [DF_");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("_ModifiedDate] DEFAULT (getdate()) ");
            
            #line 66 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 67 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
           else
           {
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 70 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadFieldName(property.Storage.Name)));
            
            #line default
            #line hidden
            
            #line 70 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(PadTypeName(property.Storage.Type.ToUpper())));
            
            #line default
            #line hidden
            
            #line 70 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nullable));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 71 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
         }
       }
   }

   if (idProperty != null)
   {

            
            #line default
            #line hidden
            this.Write("    , CONSTRAINT [PK_");
            
            #line 78 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 78 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("] PRIMARY KEY CLUSTERED ([");
            
            #line 78 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(idProperty.Storage.Name));
            
            #line default
            #line hidden
            this.Write("] ASC)\r\n");
            
            #line 79 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
 }
   foreach (var relation in entity.Relations.Where(rel => rel.HasKey))
   {
            
            #line default
            #line hidden
            this.Write("    , CONSTRAINT [FK_");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelationArea(entity, relation)));
            
            #line default
            #line hidden
            this.Write("_");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write("] FOREIGN KEY ([");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EndpointName));
            
            #line default
            #line hidden
            this.Write("ID]) REFERENCES [");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Area));
            
            #line default
            #line hidden
            this.Write("].[");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write("]([");
            
            #line 82 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.EntityName));
            
            #line default
            #line hidden
            this.Write("ID])\r\n");
            
            #line 83 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"
 }

            
            #line default
            #line hidden
            this.Write(")\r\nGO\r\n\r\n");
            
            #line 88 "D:\GitHub\babaksoft\Projects\SPPC\framework\src\Framework\SPPC.Framework.Tools.ProjectCLI\Templates\SqlCreateTableFromMetadata.tt"

}

            
            #line default
            #line hidden
            this.Write("SET QUOTED_IDENTIFIER OFF\r\nGO\r\n\r\nSET ANSI_NULLS OFF\r\nGO\r\n");
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
    public class SqlCreateTableFromMetadataBase
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
