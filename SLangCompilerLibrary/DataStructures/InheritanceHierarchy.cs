using LanguageExt;
using static LanguageExt.Prelude;
using SLangCompilerLibrary.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Sockets;
using System.Text;
using SLangCompilerLibrary.Utils;

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
            hier = new Map<TID, Entry>(types.Map(type => (type.name(), new Entry(getParentsTIDs(type), None))));
            foreach (IType<TID> type in types)
            {
                (from x in type.parents() select x).IfSome(pars =>
                {
                    foreach (IType<TID> parent in pars)
                    {
                        TID pname = parent.name();
                        if (hier.ContainsKey(pname))
                        {
                            hier = hier.AddOrUpdate(pname, hier[pname].addChild(type.name()));
                        }
                        else
                        {
                            throw new MalformedInheritanceHierarchyError(
                                $" Type {pname}, parent of {type.name()} was not found in types array {types}"
                            );
                        }
                    }
                });
            }
        }

        public Arr<TID> traverseUp(TID start)
        {
            if (hier.ContainsKey(start))
            {
                Arr<TID> ret = Arr.create(start);
                return hier[start].parents.Some(v => v.Fold(ret,(arr,tid) => arr.AddRange(traverseUp(tid)))).None(() => ret);
            }
            else
            {
                throw new MalformedInheritanceHierarchyError($" TID {start} was not found in hierarchy while traversing up");
            }
        }
        public Arr<TID> traverseDown(TID start)
        {
            if (hier.ContainsKey(start))
            {
                Arr<TID> ret = Arr.create(start);
                return hier[start].childrens.Some(v => v.Fold(ret, (arr, tid) => arr.AddRange(traverseDown(tid)))).None(() => ret);
            }
            else
            {
                throw new MalformedInheritanceHierarchyError($" TID {start} was not found in hierarchy while traversing down");
            }
        }
        private Option<Arr<TID>> getParentsTIDs(IType<TID> type)
        {
            return from x in type.parents() select x.Map(t => t.name());
        }
    }
}