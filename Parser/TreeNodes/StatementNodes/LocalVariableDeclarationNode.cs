using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Statements
{
    public class LocalVariableDeclarationNode : StatementNode
    {
        [XmlArray("LocalVariables"),
        XmlArrayItem("Variable")]
        public List<FieldNode> localVariables;

        private LocalVariableDeclarationNode()
        {
            localVariables = null;
        }

        public LocalVariableDeclarationNode(List<FieldNode> localVariables)
        {
            this.localVariables = localVariables;
        }
    }
}