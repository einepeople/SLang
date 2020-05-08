using System;
using System.Collections.Generic;
using System.Net.Sockets;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SLangCompilerLibrary;
using SLangCompilerLibrary.DataStructures;
using SLangCompilerLibrary.Types;
using SLangCompilerLibrary.Utils;

namespace SLangCompilerLibrarySpec
{
    [TestClass]
    public class FunctionResolverSpec
    {
        public (IType[], Function[]) init()
        {
            DummyType A = new DummyType("A", None);
            DummyType B = new DummyType("B", Some(Arr.create<IType>(A)));
            DummyType C = new DummyType("C", Some(Arr.create<IType>(A)));
            DummyType D = new DummyType("D", Some(Arr.create<IType>(B, C)));

            DummyType X = new DummyType("X", None);
            DummyType Y = new DummyType("Y", Some(Arr.create<IType>(X)));
            DummyType Z = new DummyType("Z", Some(Arr.create<IType>(Y)));

            var functions = new Function[]
            {
                new Function("AtoZ", new IType[] {A}, new IType[] {Z}),
                new Function("BtoZ", new IType[] {B}, new IType[] {Z}),
                new Function("CtoY", new IType[] {C}, new IType[] {Y}),
                new Function("DtoX", new IType[] {D}, new IType[] {X}),
                new Function("BZtoD", new IType[] {B, Z}, new IType[] {D}),
                new Function("BCtoD", new IType[] {B, C}, new IType[] {Y}),
                new Function("DDtoA", new IType[] {D, D}, new IType[] {A}),
                new Function("AAtoY", new IType[] {A, A}, new IType[] {Y}),
                new Function("ADtoZ", new IType[] {A, D}, new IType[] {Z}),

            };
            var types = new IType[]
            {
                A,B,C,D,X,Y,Z
            };
            return (types, functions);
        }

        [TestMethod]
        public void NaiveResolve()
        {
            (IType[] types, Function[] scope) = init();
            InheritanceHierarchy ih = new InheritanceHierarchy(Arr.create<IType>(types));
            FunctionResolver fr = new FunctionResolver(ih, new Seq<Function>(scope));

            var res = fr.resolve(new Function("A_toZ_", new[] {types[0]}, new[] {types[6]}));
            var correct = new Set<string>(new[] {"AtoZ"});
            Assert.AreEqual(true, res.IsSome );
            match(from someres in res select someres.name,
                Some: v => Assert.AreEqual(correct, v),
                None: () => throw new FunctionResolutionError("Function resolution return empty set, when should not"));

        }

        [TestMethod]
        public void MultipleResovle()
        {
            (IType[] types, Function[] scope) = init();
            InheritanceHierarchy ih = new InheritanceHierarchy(Arr.create<IType>(types));
            FunctionResolver fr = new FunctionResolver(ih, new Seq<Function>(scope));

            var res = fr.resolve(new Function("B_toZ_", new[] { types[1] }, new[] { types[6] }));
            var correct = new Set<string>(new[] { "AtoZ", "BtoZ" });
            match(from someres in res select someres.name,
                Some: v => Assert.AreEqual(v, correct),
                None: () => throw new FunctionResolutionError("Function resolution return empty set, when should not"));
        }
    }
}
