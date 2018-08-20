﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Urho3DMaterialEditor.Model.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class GLSLPassTemplate : GLSLPassTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 6 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"


foreach (var define in Graph.Defines)
{
    WriteLine("#if !defined("+define+")");
    WriteLine("#define "+define);
    WriteLine("#endif");
}

foreach (var undefine in Graph.Undefines)
{
    WriteLine("#if defined("+undefine+")");
    WriteLine("#undef "+undefine);
    WriteLine("#endif");
}


            
            #line default
            #line hidden
            this.Write(@"
// ------------------- Vertex Shader ---------------

#if defined(DIRLIGHT) && (!defined(GL_ES) || defined(WEBGL))
    #define NUMCASCADES 4
#else
    #define NUMCASCADES 1
#endif

#ifdef COMPILEVS

// Silence GLSL 150 deprecation warnings
#ifdef GL3
#define attribute in
#define varying out
#endif

");
            
            #line 40 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"


foreach (var uniform in Graph.VertexShaderAttributes)
{
	if (uniform.Type == NodeTypes.VertexData)
		throw new InvalidOperationException();
	var type = GLSLCodeGen.GetType(uniform.OutputPins[0].Type);
	if (uniform.Name == "BlendIndices")
		type = "vec4";
	uniform.Extra.Define.WriteLineIfDef(this,"attribute "+type+" i"+uniform.Name+GLSLCodeGen.GetArraySize(uniform.OutputPins[0].Type)+";");
}
foreach (var uniform in VertexShaderUniformsAndFunctions.Uniforms)
{
	uniform.Extra.Define.WriteLineIfDef(this,"uniform "+GLSLCodeGen.GetType(uniform.Type)+" "+uniform.Name+GLSLCodeGen.GetArraySize(uniform.Type)+";");
}
foreach (var uniform in Graph.VertexShaderVaryings)
{
	uniform.Extra.Define.WriteLineIfDef(this,"varying "+GLSLCodeGen.GetType(GLSLCodeGen.GetVaryingType(uniform.InputPins[0].Type))+" v"+uniform.Value+GLSLCodeGen.GetArraySize(uniform.InputPins[0].Type)+";");
}
this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write("\r\nattribute float iObjectIndex;\r\n\r\n\r\n");
            
            #line 65 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"

	foreach (var function in VertexShaderUniformsAndFunctions.Functions)
	{
		WriteLine("// --- "+function+" ---");
		WriteLine(VertexShaderGenerator.GetFunction(function));
	}
	this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write("\r\nvoid VS()\r\n{\r\n");
            
            #line 76 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"

	foreach (var varying in Graph.VertexShaderVaryings)
		VertexShaderGenerator.GenerateCode(varying);
	this.WriteLine(null,"");
	var ret = VertexShaderGenerator.GenerateCode(Graph.OutputPosition);
	WriteLine("vec4 ret =  " + ret + ";");
	this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write(@"
    // While getting the clip coordinate, also automatically set gl_ClipVertex for user clip planes
    #if !defined(GL_ES) && !defined(GL3)
       gl_ClipVertex = ret;
    #elif defined(GL3)
       gl_ClipDistance[0] = dot(cClipPlane, ret);
    #endif
    gl_Position = ret;
}

#else
// ------------------- Pixel Shader ---------------

");
            
            #line 97 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"

foreach (var uniform in PixelShaderUniformsAndFunctions.Uniforms)
{
//	WriteIfDef(uniform);
	if (uniform.Type == PinTypes.LightMatrices)
	{
		uniform.Extra.Define.WriteLineIfDef(this, @"
#if !defined(GL_ES) || defined(WEBGL)
    uniform mat4 cLightMatrices[4];
#else
    uniform highp mat4 cLightMatrices[2];
#endif
");
	}
	else
	{
		uniform.Extra.Define.WriteLineIfDef(this, "uniform "+GLSLCodeGen.GetType(uniform.Type)+" "+uniform.Name+";");
	}
//	WriteEndIf(uniform);
}
foreach (var uniform in Graph.Samplers)
{
	if (uniform.Name == SamplerNodeFactory.ShadowMap)
	{
	uniform.Extra.Define.WriteLineIfDef(this,@"
#ifndef GL_ES
    #ifdef VSM_SHADOW
        uniform sampler2D sShadowMap;
    #else
        uniform sampler2DShadow sShadowMap;
    #endif
#else
    uniform highp sampler2D sShadowMap;
#endif
	");
	}
	else
	{
	uniform.Extra.Define.WriteLineIfDef(this,"uniform "+GLSLCodeGen.GetType(uniform.OutputPins[0])+" "+GLSLCodeGen.GetSamplerName(uniform)+";");
	}
}
foreach (var uniform in Graph.PixelShaderVaryings)
{
	uniform.Extra.Define.WriteLineIfDef(this,"varying "+GLSLCodeGen.GetType(GLSLCodeGen.GetVaryingType(uniform.OutputPins[0].Type))+" v"+uniform.Value+GLSLCodeGen.GetArraySize(uniform.OutputPins[0].Type)+";");
}
this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 145 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"

	foreach (var function in PixelShaderUniformsAndFunctions.Functions)
	{
		WriteLine("// --- "+function+" ---");
		WriteLine(PixelShaderGenerator.GetFunction(function));
	}
	this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write("\r\nvoid PS()\r\n{\r\n");
            
            #line 156 "E:\MyWork\Urho3DMaterialGraphEditor\src\Urho3DMaterialEditor\Model\Templates\GLSLPassTemplate.tt"

	foreach (var discard in Graph.Discards)
		PixelShaderGenerator.GenerateCode(discard);
	foreach (var rt in Graph.RenderTargets)
		PixelShaderGenerator.GenerateCode(rt);
	this.WriteLine(null,"");

            
            #line default
            #line hidden
            this.Write("}\r\n#endif");
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
    public class GLSLPassTemplateBase
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
