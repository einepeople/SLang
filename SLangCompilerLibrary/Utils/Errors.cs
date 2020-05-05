using System;
using System.Collections.Generic;
using System.Text;

namespace SLangCompilerLibrary.Utils
{
    public class MalformedInheritanceHierarchyError : Exception
    {
        public MalformedInheritanceHierarchyError()
        {
        }

        public MalformedInheritanceHierarchyError(string message)
            : base(message)
        {
        }
    }
}
