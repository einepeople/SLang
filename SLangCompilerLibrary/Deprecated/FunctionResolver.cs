using LanguageExt;
using static LanguageExt.Prelude;
using SLangCompilerLibrary.Types;
using System;
using System.Runtime.CompilerServices;
using SLangCompilerLibrary.DataStructures;

namespace SLangCompilerLibrary
{
    
    public class FunctionResolver
    {
        private Seq<Function> scope;
        private InheritanceHierarchy hier;

        public FunctionResolver(InheritanceHierarchy hier, Seq<Function> funcScope)
        {
            this.hier = hier;
            scope = funcScope;
        }
        // Given function scope and inheritance hierarchy, for every function f we can find all the functions from scope, that conform to f's signature
        // Since F: -I => +O
        // We are searching for all the functions, which Input argument types are either equal or parents to f's ones
        // ...and output types are equal or childrens to f's ones.

        public Option<Set<Function>> resolve( Function f)
        {
            var fScope = scope.Filter(fn => fn.I.Count == f.I.Count && fn.O.Count == f.O.Count);
            var resFilter = f.I.Fold((fScope, 0),
                (st, it) => (st.fScope.Filter(fn => hier.traverseUp(it.name()).Contains(fn.I[st.Item2].name())),
                    st.Item2 + 1));
            fScope = resFilter.fScope;
            resFilter = f.O.Fold((fScope, 0),
                (st, it) => (st.fScope.Filter(fn => hier.traverseDown(it.name()).Contains(fn.O[st.Item2].name())),
                    st.Item2 + 1));
            fScope = resFilter.fScope;
            return fScope.IsEmpty ? None : Some(new Set<Function>(fScope));
        }
    }
}