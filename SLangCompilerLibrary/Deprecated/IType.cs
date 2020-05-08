using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace SLangCompilerLibrary.Types
{
    // Interface for representing SLang types
    // We're using name as a type identifier, but this could be easily rewritten to anything like long ptr to some table etc
    // IType was parametrized with TID in previous commits, but the resulting code was too verbose
    public interface IType
    {
        string name();
        Option<Arr<IType>> parents();
    }

    // Naive implementation of IType for testing purposes
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
