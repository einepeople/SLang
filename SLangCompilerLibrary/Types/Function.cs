using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.DataTypes;

namespace SLangCompilerLibrary.Types
{
    // Type for SLang functions and methods. 
    // F: I => O
    // I and O - list of input and output types correspondingly
    public class Function : IComparable<Function>
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

        public Function(string name, IType[] inputs, IType[] outputs)
        {
            I = new Seq<IType>(inputs);
            O = new Seq<IType>(outputs);
            this.name = name;
        }

        public int CompareTo(Function other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(name, other.name, StringComparison.Ordinal);
        }
    }
}
