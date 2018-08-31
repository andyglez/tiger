using System.Collections.Generic;

namespace Tiger
{
    public static class CircularDefinition
    {
        public static void Analize(List<TypeDeclarationNode> declared_types, Scope scope, List<Error> errors)
        {
            var graphNodes = new Dictionary<string, TypeDeclarationNode>();
            foreach (var type in declared_types)
                if (type is AliasDeclarationNode || type is ArrayTypeDeclarationNode)
                    if (!graphNodes.ContainsKey(type.Identifier))
                        graphNodes.Add(type.Identifier, type);
            AnalizeCircularDependency(graphNodes, scope, errors);
        }
        private static bool AnalizeCircularDependency(Dictionary<string, TypeDeclarationNode> graphNodes, Scope scope, List<Error> errors)
        {
            bool is_cyclic = false;
            Dictionary<string, List<string>> graph;
            Dictionary<string, List<string>> reverseGraph;

            CreateGraph(graphNodes, out graph, out reverseGraph);

            List<List<string>> scc = SCC(graph, reverseGraph);

            foreach (var component in scc)
                if (component.Count > 1 || (graph[component[0]].Count == 1 && graph[component[0]][0].CompareTo(component[0]) == 0))
                    foreach (string s in component)
                    {
                        TypeDeclarationNode node = graphNodes[s];
                        graphNodes.Remove(s);

                        is_cyclic = true;
                        errors.Add(new CircularDefinitionError(node, s));

                        //the following are invalid nodes
                        if (node is AliasDeclarationNode)
                            node.TypeDeclared = NilType.GetInstance;
                        if (node is ArrayTypeDeclarationNode)
                            node.TypeDeclared = new ArrayType(node.TypeDeclared, node.Identifier);
                        scope.TypesDictionary.Add(s, node.TypeDeclared);
                    }


            CreateGraph(graphNodes, out graph, out reverseGraph, false);
            var topological_order = ReversedTopologicalSort(graph);

            foreach (var type_name in topological_order)
            {
                var node = graphNodes[type_name];
                node.CheckSemantics(scope, errors);
                if (!scope.TypesDictionary.ContainsKey(type_name))
                    scope.TypesDictionary.Add(type_name, node.TypeDeclared);
            }
            return is_cyclic;
        }

        private static void CreateGraph(Dictionary<string, TypeDeclarationNode> graphNodes,
            out Dictionary<string, List<string>> graph,
            out Dictionary<string, List<string>> reverseGraph,
            bool createReverse = true)
        {
            graph = new Dictionary<string, List<string>>();
            reverseGraph = new Dictionary<string, List<string>>();
            foreach (var node in graphNodes)
            {
                graph.Add(node.Key, new List<string>());
                if (createReverse)
                    reverseGraph.Add(node.Key, new List<string>());
            }
            foreach (var node in graphNodes)
            {
                string dest;
                if (node.Value is AliasDeclarationNode)
                    dest = ((AliasDeclarationNode)node.Value).BaseIdentifier;
                else
                    dest = ((ArrayTypeDeclarationNode)node.Value).ElementsTypeIdentifier;

                if (graphNodes.ContainsKey(dest))
                {
                    graph[node.Key].Add(dest);
                    if (createReverse)
                        reverseGraph[dest].Add(node.Key);
                }
            }
        }

        private static List<string> ReversedTopologicalSort(Dictionary<string, List<string>> graph)
        {
            var used = new HashSet<string>();
            var res = new List<string>();
            foreach (string node in graph.Keys)
                if (!used.Contains(node))
                    DFS(graph, used, res, node);
            return res;
        }

        private static List<List<string>> SCC(Dictionary<string, List<string>> graph, Dictionary<string, List<string>> reverseGraph)
        {
            var used = new HashSet<string>();
            var order = new List<string>();
            foreach (string node in graph.Keys)
                if (!used.Contains(node))
                    DFS(graph, used, order, node);

            order.Reverse();
            var res = new List<List<string>>();
            used.Clear();

            foreach (string node in order)
                if (!used.Contains(node))
                {
                    List<string> component = new List<string>();
                    DFS(reverseGraph, used, component, node);
                    res.Add(component);
                }
            return res;
        }

        private static void DFS(Dictionary<string, List<string>> graph, HashSet<string> used, List<string> order, string node)
        {
            used.Add(node);
            foreach (string nextNode in graph[node])
                if (!used.Contains(nextNode))
                    DFS(graph, used, order, nextNode);
            order.Add(node);
        }
    }
}
