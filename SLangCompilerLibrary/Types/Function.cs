using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.DataTypes;

namespace SLangCompilerLibrary.Types
{
    public class Function<ID>
    {
        private Seq<IType<ID>> I { get; }
        private Seq<IType<ID>> O { get; }
        private long fptr { get; } 
        public Function(Seq<IType<ID>> inputTypes, Seq<IType<ID>> outputTypes, long fptr)
        {
            I = inputTypes;
            O = outputTypes;
            this.fptr = fptr;
        }
    }
}
