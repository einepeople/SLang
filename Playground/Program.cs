using System;
using SLangCompilerLibrarySpec;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new InheritanceHierarchySpec().init().traverseDown("A");
        }
    }
}
