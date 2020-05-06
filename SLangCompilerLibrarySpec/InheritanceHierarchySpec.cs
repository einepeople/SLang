using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SLangCompilerLibrary.DataStructures;
using SLangCompilerLibrary.Types;

namespace SLangCompilerLibrarySpec
{
    [TestClass]
    public class InheritanceHierarchySpec
    {
        [TestMethod]
        public void TraverseDownLeaf()
        {
            InheritanceHierarchy<string> ih = init();

            var res = ih.traverseDown("G");

            Assert.AreEqual(res, Set("G"));
        }

        public InheritanceHierarchy<string> init()
        {
            DummyType A = new DummyType("A", None);
            DummyType B = new DummyType("B", Some(Arr.create<IType<string>>(A)));
            DummyType C = new DummyType("C", Some(Arr.create<IType<string>>(A)));
            DummyType D = new DummyType("D", Some(Arr.create<IType<string>>(B, C)));
            DummyType E = new DummyType("E", Some(Arr.create<IType<string>>(D)));
            DummyType F = new DummyType("F", None);
            DummyType G = new DummyType("G", Some(Arr.create<IType<string>>(F)));
            return new InheritanceHierarchy<string>(Arr.create<IType<string>>(A, B, C, D, E, F, G));
        }

        [TestMethod]
        public void TraverseUpRoot()
        {
            InheritanceHierarchy<string> ih = init();

            var res = ih.traverseUp("A");

            Assert.AreEqual(res,Set("A"));
        }
        


        [TestMethod]
        public void TraverseUpDiamond()
        {
            InheritanceHierarchy<string> ih = init();

            var res = ih.traverseUp("D");

            Assert.AreEqual(res, Set("D","B","C","A"));
        }
        [TestMethod]
        public void TraverseDownDiamondSide()
        {
            InheritanceHierarchy<string> ih = init();

            var res = ih.traverseDown("B");

            Assert.AreEqual(res, Set("B", "D", "E"));
        }

        [TestMethod]
        public void TraverseDownSeparateBranch()
        {
            InheritanceHierarchy<string> ih = init();

            var res = ih.traverseDown("F");

            Assert.AreEqual(res, Set("F", "G"));
        }
    }
}
