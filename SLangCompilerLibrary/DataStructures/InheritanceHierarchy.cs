using LanguageExt;
using static LanguageExt.Prelude;
using SLangCompilerLibrary.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Sockets;
using System.Text;

namespace SLangCompilerLibrary.DataStructures
{
    public class InheritanceHierarchy<TID>
    {
        private class Entry
        {
            public Option<Arr<TID>> parents;
            public Option<Arr<TID>> childrens;

            public Entry(Option<Arr<TID>> parents, Option<Arr<TID>> childrens)
            {
                this.parents = parents;
                this.childrens = childrens;
            }

            public Entry addChild(TID child)
            {
                Arr<TID> newChilds = this.childrens.Some(
                        c => c.Add(child))
                    .None(() => Arr.create(child));
                return new Entry(this.parents, Some(newChilds));
            }
        }

        Map<TID, Entry> hier;

        public InheritanceHierarchy(Arr<IType<TID>> types)
        {
            //hier = types.Map(typ => ());
        } 
        //private get
    }
}