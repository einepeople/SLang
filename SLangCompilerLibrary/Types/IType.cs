using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace SLangCompilerLibrary.Types
{

    public interface IType
    {
        string name();
        Option<Arr<IType>> parents();
    }

    public class DummyType: IType
    {
        private readonly string typename;
        private readonly Option<Arr<IType>> parents_;
        public DummyType(string typeName, Option<Arr<IType>> parents)
        {
            this.typename = typeName;
            this.parents_ = parents;
        }
        public string name() => this.typename;
        public Option<Arr<IType>> parents() => this.parents_;
    }
}
