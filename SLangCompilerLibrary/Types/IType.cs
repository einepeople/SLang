using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace SLangCompilerLibrary.Types
{

    public interface IType<ID>
    {
        ID name();
        Option<Arr<IType<ID>>> parents();
    }

    public class DummyType: IType<string>
    {
        private readonly string typename;
        private readonly Option<Arr<IType<string>>> parents_;
        public DummyType(string typeName, Option<Arr<IType<string>>> parents)
        {
            this.typename = typeName;
            this.parents_ = parents;
        }
        public string name() => this.typename;
        public Option<Arr<IType<string>>> parents() => this.parents_;
    }
}
