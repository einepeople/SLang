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

        public FunctionResolver(InheritanceHierarchy hier)
        {
            this.hier = hier;
        }

        public Option<Set<Function>> resolve(Seq<Function> funcScope, Function f)
        {
            var fScope = scope.Filter(fn => fn.I.Count == f.I.Count && fn.O.Count == f.O.Count);
            var resFIlter = f.I.Fold((fScope, 0),
                (st, it) => (st.fScope.Filter(fn => hier.traverseUp(it.name()).Contains(fn.I[st.Item2].name())),
                    st.Item2 + 1));
            fScope = resFIlter.fScope;
            resFIlter = f.O.Fold((fScope, 0),
                (st, it) => (st.fScope.Filter(fn => hier.traverseDown(it.name()).Contains(fn.I[st.Item2].name())),
                    st.Item2 + 1));
            fScope = resFIlter.fScope;
            if (fScope.IsEmpty) return None;
            return Some(new Set<Function>(fScope));
        }
    }
}