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
    // Represents a graph-ish structure with traversal capabilities
    // Main purpose - to be able to collect all the direct and indirect parents or childrens of a type given the type
    // Used for dynamic dispatch function resolution
    public class InheritanceHierarchy
    {
        private class Entry
        {
            public Option<Arr<string>> parents;
            public Option<Arr<string>> childrens;

            public Entry(Option<Arr<string>> parents, Option<Arr<string>> childrens)
            {
                this.parents = parents;
                this.childrens = childrens;
            }

            public Entry addChild(string child)
            {
                Arr<string> newChilds = this.childrens.Some(
                        c => c.Add(child))
                    .None(() => Arr.create(child));
                return new Entry(this.parents, Some(newChilds));
            }
        }

        Map<string, Entry> hier;

        // Initilized with types and their DIRECT parents. Easily inferenceable from `class A: B, C`
        // Firstly fill `hier` mapping with given information, leaving childrens field as `None`
        // After that traversing through input, filling the childrens fields.
        public InheritanceHierarchy(Arr<IType> types)
        {
            hier = new Map<string, Entry>(types.Map(type => (type.name(), new Entry(getParentsTIDs(type), None))));
            foreach (IType type in types)
            {
                (from x in type.parents() select x).IfSome(pars =>
                {
                    foreach (IType parent in pars)
                    {
                        string pname = parent.name();
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
        // Given type identifier (==name), find all its parents
        public Set<string> traverseUp(string start)
        {
            if (hier.ContainsKey(start))
            {
                Set<string> ret = Set(start);
                return hier[start].parents.Some(v => v.Fold(ret,(arr,tid) => arr.AddOrUpdateRange(traverseUp(tid)))).None(() => ret);
            }
            else
            {
                throw new MalformedInheritanceHierarchyError($" TID {start} was not found in hierarchy while traversing up");
            }
        }
        // Given type identifier (==name), find all its childrens
        public Set<string> traverseDown(string start)
        {
            if (hier.ContainsKey(start))
            {
                Set<string> ret = Set(start);
                return hier[start].childrens.Some(v => v.Fold(ret, (arr, tid) => arr.AddOrUpdateRange(traverseDown(tid)))).None(() => ret);
            }
            else
            {
                throw new MalformedInheritanceHierarchyError($" TID {start} was not found in hierarchy while traversing down");
            }
        }
        private Option<Arr<string>> getParentsTIDs(IType type)
        {
            return from x in type.parents() select x.Map(t => t.name());
        }
    }
}