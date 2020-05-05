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
        private Seq<IType> I { get; }
        private Seq<IType> O { get; }
        private long fptr { get; } 
        public Function(Seq<IType> inputTypes, Seq<IType> outputTypes, long fptr)
        {
            I = inputTypes;
            O = outputTypes;
            this.fptr = fptr;
        }
    }
}
