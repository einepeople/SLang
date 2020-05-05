using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SLangCompilerLibrary.Types;

namespace SLangCompilerLibrarySpec
{
    [TestClass]
    public class InheritanceHierarchySpec
    {


        [TestMethod]
        public void TestMethod1()
        {
            DummyType A = new DummyType("A", None);
            DummyType B = new DummyType("B", Some(Arr.create<IType<string>>(A)));
            DummyType C = new DummyType("C", Some(Arr.create<IType<string>>(A)));
            DummyType D = new DummyType("D", Some(Arr.create<IType<string>>(B,C)));
        }
    }
}
