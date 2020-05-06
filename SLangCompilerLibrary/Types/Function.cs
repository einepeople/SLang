using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.DataTypes;

namespace SLangCompilerLibrary.Types
{
    public class Function
    {
        public Seq<IType> I { get; }
        public Seq<IType> O { get; }
        public string name { get; } 
        public Function(Seq<IType> inputTypes, Seq<IType> outputTypes, string fname)
        {
            I = inputTypes;
            O = outputTypes;
            name = fname;
        }
    }
}
