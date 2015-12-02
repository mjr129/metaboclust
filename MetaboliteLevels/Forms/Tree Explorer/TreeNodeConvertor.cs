using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Settings;
using MetaboliteLevels.Utilities;

namespace MetaboliteLevels.Forms.Tree_Explorer
{
    static class TreeNodeConvertor
    {
        class Expander
        {
            public static Expander Instance = new Expander();
        }

        #region Root Creation

        public static TreeNode Create(Core core)
        {
            TreeNode root = new TreeNode(core.FileNames.Title);
            root.ImageKey = "LIST";
            root.SelectedImageKey = "LIST";

            // PATTERNS
            var tnClusters = root.AddSubNode(core.Clusters.Count.ToString() + " clusters", "LIST", "Cluster list/Lists all clusters in the current dataset.");

            foreach (Cluster p in core.Clusters)
            {
                AddExpandableNode(tnClusters, p);
            }

            // VARIABLES
            var tnVariables = root.AddSubNode(core.Peaks.Count.ToString() + " variables", "LIST", "Variable list/Lists all variables in the current dataset.");
            var tnVariables2 = tnVariables.AddSubNode("All " + core.Peaks.Count, "LIST", "Variable list/Lists all variables in the current dataset.\r\nThe numbers in the brackets show the number of compounds they potentially identify.");

            foreach (Peak v in core.Peaks)
            {
                AddExpandableNode(tnVariables2, v, v.Annotations.Count != 0 ? v.Annotations.Count.ToString() : null);
            }

            var tnVariablesWith = core.Peaks.Where(λ => λ.Annotations.Count != 0).ToArray();
            var tnVariablesWithout = core.Peaks.Where(λ => λ.Annotations.Count == 0).ToArray();

            tnVariables2 = tnVariables.AddSubNode(tnVariablesWith.Length.ToString() + " with potential compound IDs", "LIST", "Variable list/Lists all variables in the current dataset which potentially identify compounds.\r\nThe numbers in the brackets show the number of compounds they potentially identify.");

            foreach (Peak v in tnVariablesWith)
            {
                AddExpandableNode(tnVariables2, v, v.Annotations.Count.ToString());
            }

            tnVariables2 = tnVariables.AddSubNode(tnVariablesWithout.Length.ToString() + " without potential compound IDs", "LIST", "Variable list/Lists all variables in the current dataset which do not identify compounds.");

            foreach (Peak v in tnVariablesWithout)
            {
                AddExpandableNode(tnVariables2, v);
            }

            // PATHWAYS
            var tnPathways = root.AddSubNode(core.Pathways.Count.ToString() + " pathways", "LIST", "Pathway list/Lists all pathways in the current dataset, sorted by the number of compounds which have potentially been identified by variables in the current dataset.");
            var sortOrder = core.Pathways.OrderBy(λ => -λ.Compounds.Count(ƛ => ƛ.Annotations.Count != 0));

            foreach (Pathway p in sortOrder)
            {
                int count = p.Compounds.Count(λ => λ.Annotations.Count != 0);
                AddExpandableNode(tnPathways, p, Maths.AsFraction(count, p.Compounds.Count));
            }

            // COMPOUNDS
            var compoundsWith = core.Compounds.Where(λ => λ.Annotations.Count != 0).ToArray();
            var compoundsWithout = core.Compounds.Where(λ => λ.Annotations.Count == 0).ToArray();

            var tnCompounds = root.AddSubNode(core.Compounds.Count.ToString() + " compounds", "LIST", "Compound list/Lists all compounds in the current dataset.");
            var tnCompoundsAll = tnCompounds.AddSubNode("All " + core.Compounds.Count, "LIST", "Compound list/Lists all compounds in the current dataset. Numbers in brackets show the number of variables potentially identifying the compound.");
            var tnCompoundsWith = tnCompounds.AddSubNode(compoundsWith.Length + " with matched variables", "LIST", "Compound list/Lists all compounds in the current dataset which have variables potentially identifying them. Numbers in brackets show the number of variables.");
            var tnCompoundsWithout = tnCompounds.AddSubNode(compoundsWithout.Length + " without matched variables", "LIST", "Compound list/Lists all compounds in the current dataset which do not have variables potentially identifying them.");

            foreach (Compound c in core.Compounds.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(tnCompoundsAll, c, c.Annotations.Count != 0 ? c.Annotations.Count.ToString() : null);
            }

            foreach (Compound c in compoundsWith.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(tnCompoundsWith, c, c.Annotations.Count != 0 ? c.Annotations.Count.ToString() : null);
            }

            foreach (Compound c in compoundsWithout.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(tnCompoundsWithout, c);
            }


            // ADDUCTS
            var tnAdducts = root.AddSubNode(core.Adducts.Count.ToString() + " adducts", "LIST", "Adduct list/Lists all adducts in the current dataset.");

            foreach (Adduct a in core.Adducts.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(tnAdducts, a);
            }
            return root;
        }

        #endregion

        #region Private

        private static TreeNode AddExpander(TreeNode node)
        {
            node.AddSubNode("EXPANDS", "INFO", Expander.Instance);
            return node;
        }

        private static string CreateText(string p, string extraText)
        {
            if (extraText == null)
            {
                return p;
            }
            else
            {
                return p + " \t(" + extraText + ")";
            }
        }

        public static void ScorePathways(Cluster cluster, out List<PathwayScore> pathwaysInCluster, out HashSet<Compound> compoundsInCluster)
        {
            pathwaysInCluster = new List<PathwayScore>();

            // Make a list of compounds + pathways in this cluster
            compoundsInCluster = new HashSet<Compound>();
            HashSet<Pathway> pathways = new HashSet<Pathway>();

            foreach (var p in cluster.Assignments.Peaks)
            {
                foreach (Annotation comp in p.Annotations)
                {
                    if (!compoundsInCluster.Contains(comp.Compound))
                    {
                        compoundsInCluster.Add(comp.Compound);
                    }

                    foreach (Pathway path in comp.Compound.Pathways)
                    {
                        if (!pathways.Contains(path))
                        {
                            pathways.Add(path);
                        }
                    }
                }
            }

            // Iterate over pathways
            foreach (Pathway path in pathways)
            {
                int compoundsInThisPathway = 0; // compounds IN pathway WITH one or more compounds IN implicatedCompounds

                // Iterate over compounds in pathway
                foreach (Compound comp in path.Compounds)
                {
                    if (compoundsInCluster.Contains(comp))
                    {
                        compoundsInThisPathway++;
                    }
                }

                int peaksWithMatches = 0; // peaks IN cluster WITH one or more compounds IN pathway

                // Iterate over peaks in cluster
                foreach (Peak v in cluster.Assignments.Peaks)
                {
                    // Iterate over compounds in peak
                    foreach (Annotation ac in v.Annotations)
                    {
                        bool found = false;

                        // Iterate over compounds in pathway
                        foreach (Compound c in path.Compounds)
                        {
                            if (ac.Compound == c)
                            {
                                found = true;
                                break;
                            }
                        }

                        // Count peaks with 
                        if (found)
                        {
                            peaksWithMatches++;
                            break;
                        }
                    }
                }

                pathwaysInCluster.Add(new PathwayScore(path, compoundsInThisPathway, peaksWithMatches, compoundsInThisPathway));
            }

            pathwaysInCluster.Sort();
        }

        #endregion

        #region AddExpandableNode

        public static TreeNode AddExpandableNode(TreeNode parent, Adduct a, string extraText = null)
        {
            return AddExpandableNode(parent.Nodes, a, extraText);
        }

        public static TreeNode AddExpandableNode(TreeNode parent, Compound c, string extraText = null)
        {
            return AddExpandableNode(parent.Nodes, c, extraText);
        }

        public static TreeNode AddExpandableNode(TreeNode parent, Pathway p, string extraText = null)
        {
            return AddExpandableNode(parent.Nodes, p, extraText);
        }

        public static TreeNode AddExpandableNode(TreeNode parent, Peak v, string extraText = null)
        {
            return AddExpandableNode(parent.Nodes, v, extraText);
        }

        public static TreeNode AddExpandableNode(TreeNode parent, Cluster p, string extraText = null)
        {
            return AddExpandableNode(parent.Nodes, p, extraText);
        }

        public static TreeNode AddExpandableNode(TreeNodeCollection parent, Adduct a, string extraText = null)
        {
            var node = parent.AddSubNode(CreateText(a.Name, extraText), "ADDUCT", a);
            AddExpander(node);
            return node;
        }

        public static TreeNode AddExpandableNode(TreeNodeCollection parent, Compound c, string extraText = null)
        {
            var node = parent.AddSubNode(CreateText(c.Name, extraText), "COMPOUND", c);
            AddExpander(node);
            return node;
        }

        public static TreeNode AddExpandableNode(TreeNodeCollection parent, Pathway p, string extraText = null)
        {
            var node = parent.AddSubNode(CreateText(p.Name, extraText), "PATHWAY", p);
            AddExpander(node);
            return node;
        }

        public static TreeNode AddExpandableNode(TreeNodeCollection parent, Peak v, string extraText = null)
        {
            var node = parent.AddSubNode(CreateText(v.DisplayName, extraText), "VARIABLE", v);
            AddExpander(node);
            return node;
        }

        public static TreeNode AddExpandableNode(TreeNodeCollection parent, Cluster p, string extraText = null)
        {
            var node = parent.AddSubNode(CreateText(p.DisplayName, extraText), "PATTERN", p);
            AddExpander(node);
            return node;
        }

        #endregion

        #region ExpandNodeIfNeeded

        public static void ExpandNodeIfNeeded(TreeNode node)
        {
            if (node.Nodes.Count == 1 && node.Nodes[0].Tag is Expander)
            {
                ExpandNode(node);
            }
        }

        private static void ExpandNode(TreeNode node)
        {
            node.Nodes.Clear();
            object tag = node.Tag;

            if (tag is Cluster)
            {
                Expand(node, (Cluster)tag);
            }
            else if (tag is Peak)
            {
                Expand(node, (Peak)tag);
            }
            else if (tag is Compound)
            {
                Expand(node, (Compound)tag);
            }
            else if (tag is Pathway)
            {
                Expand(node, (Pathway)tag);
            }
            else if (tag is Adduct)
            {
                Expand(node, (Adduct)tag);
            }
        }

        /// <summary>
        /// PATTERN
        /// </summary>
        private static void Expand(TreeNode node, Cluster x)
        {
            // Add information
            var l = node.AddSubNode("Information", "LIST", "Information/Information about this cluster");
            l.AddSubNode("Name = " + x.DisplayName);
            l.AddSubNode("Comment = " + x.Comment);
            l.AddSubNode("Variables = " + x.Assignments);
            l.AddSubNode("Exemplars = " + x.Exemplars);
            l.AddSubNode("Options = " + x.States);

            // Add statistics
            l = node.AddSubNode("Statistics", "LIST", "Statistics/Statistics calculated for this cluster");

            foreach (var kvp in x.Statistics)
            {
                double total = 0;

                l.AddSubNode("Mean " + kvp.Key + " = " + total);
            }

            // Add peaks
            l = node.AddSubNode(x.Assignments.Count + " variables", "LIST", "Variable list/Shows the variables assigned to this cluster");
            l.AddSubNode("- Sorted by distance");

            foreach (var ass in x.Assignments.List.OrderBy(λ => λ.Score))
            {
                AddExpandableNode(l, ass.Peak);
            }

            // Add exemplars
            /*l = node.AddSubNode(x.Exemplars.Count + " exemplars", "LIST", "Exemplar list/Shows the exemplars of this cluster");

            foreach (Peak v in x.Exemplars.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(l, v);
            }*/

            // Add compounds
            List<PathwayScore> pathwaysInCluster;
            HashSet<Compound> compoundsInCluster;
            ScorePathways(x, out pathwaysInCluster, out compoundsInCluster);

            l = node.AddSubNode(compoundsInCluster.Count + " implicated compounds", "LIST", "Compound list/Shows the compounds potentially identified by variables in this cluster. Numbers in brackets show the number of variables potentially identifying this compound that are in this cluster, out of the total number.");

            foreach (Compound c in compoundsInCluster.OrderBy(λ => λ.Name))
            {
                AddExpandableNode(l, c, c.Annotations.Count(λ => λ.Peak.Assignments.Clusters.Contains(x)) + " / " + c.Annotations.Count);
            }

            // Add pathways
            l = node.AddSubNode(pathwaysInCluster.Count + " implicated pathways", "LIST", @"Pathway list/Shows pathways which contain compounds potentially identified by variables in this cluster.

This is sorted by the number of variables in this cluster with potential compound IDs in the pathways.

The numbers in brackets after the pathway name show:
    V = <Number of variables in this cluster identifying compounds in this pathway>
    P = <Number of pathway compounds identified by variables in this cluster>
    I = <Number of pathway compounds identified by variables>
    C = <Number of pathway compounds>");

            foreach (var score in pathwaysInCluster)
            {
                int count = score.pathway.Compounds.Count(λ => λ.Annotations.Count != 0);
                AddExpandableNode(l, score.pathway, "V = " + score.variableScore + ", P = " + score.compoundScore + ", I = " + count + ", C = " + score.pathway.Compounds.Count);
            }
        }

        /// <summary>
        /// VARIABLE
        /// </summary>
        private static void Expand(TreeNode node, Peak x)
        {
            if (x.Assignments.Count != 0)
            {
                foreach (Cluster cluster in x.Assignments.Clusters)
                {
                    AddExpandableNode(node, cluster);
                }
            }
            else
            {
                node.AddSubNode("(Not in any cluster)", "PATTERN");
            }

            var l = node.AddSubNode("Information", "LIST", "Information/Information about this peak");
            l.AddSubNode("Name = " + x.DisplayName);
            l.AddSubNode("Comment = " + x.Comment);
            l.AddSubNode("Cluster = " + (x.Assignments.Count != 0 ? Maths.ArrayToString(x.Assignments.Clusters) : "(none)"));
            //l.AddSubNode("Is exemplar for = " + x.ExemplarCluster);
            l.AddSubNode("Data index = " + x.Index);
            l.AddSubNode("LC-MS mode = " + x.LcmsMode);
            l.AddSubNode("M/Z = " + x.Mz);
            l.AddSubNode("Num. Comment flags = " + x.CommentFlags.Count);
            l.AddSubNode("Num. Potential compounds = " + x.Annotations.Count);

            if (x.CommentFlags.Count != 0)
            {
                TreeNode tnCommentFlags = l.AddSubNode("Comment Flags", "LIST", "Comment flags set on this variable");

                foreach (PeakFlag flag in x.CommentFlags)
                {
                    tnCommentFlags.AddSubNode(flag.Id);
                }
            }

            //var l2 = l.AddSubNode("Meta-info", "LIST", "Meta-information/Any extra information present in the variable information file is shown here.");
            //foreach (var kvp in x.MetaInfo)
            //{
            //    l2.AddSubNode(kvp.Key + " = " + kvp.Value);
            //}

            l = node.AddSubNode("Statistics", "LIST", "Statistics/Statistics calculated for this variable");
            l.AddSubNode("score = " + (x.Assignments.Count != 0 ? Maths.ArrayToString(x.Assignments.Scores) : "(none)"));

            foreach (var kvp in x.Statistics)
            {
                l.AddSubNode(kvp.Key + " = " + kvp.Value);
            }

            l = node.AddSubNode(x.Annotations.Count + " compounds", "LIST", "Compound list/Shows compounds potentially identified by this variable.");

            foreach (Annotation ac in x.Annotations)
            {
                //l2 = l.AddSubNode(ac.Compound.Name + " + " + ac.Adduct.Name, "LIST");
                //var l3 = l2.AddSubNode("Mass = " + Maths.SignificantDigits((double)(ac.Compound.Mass + ac.Adduct.Mass)));
                //l3.AddSubNode("Mass = " + ac.Compound.Mass + " + " + ac.Adduct.Mass + " = " + (ac.Compound.Mass + ac.Adduct.Mass));

                //AddExpandableNode(l2, ac.Compound);
                //AddExpandableNode(l2, ac.Adduct);

                AddExpandableNode(l, ac.Compound, ac.Adduct.Name);
            }
        }

        private static void Expand(TreeNode node, Compound x)
        {
            var l = node.AddSubNode("Information", "LIST", "Information/Information about this compound");
            l.AddSubNode("Name = " + x.Name);
            l.AddSubNode("ID = " + x.Id);
            l.AddSubNode("Mass = " + x.Mass);
            l.AddSubNode("Pathways = " + x.Pathways.Count);
            l.AddSubNode("Potential variables = " + x.Annotations.Count);

            l = node.AddSubNode(x.Annotations.Count.ToString() + " potential variables", "LIST", "Variable list/Variables potentially identifying this compound");

            foreach (Annotation c in x.Annotations)
            {
                StringBuilder sb = new StringBuilder();

                if (c.Compound == x)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append(c.Adduct.Name);
                }

                AddExpandableNode(l, c.Peak, sb.Length != 0 ? sb.ToString() : null);
            }

            l = node.AddSubNode(x.Pathways.Count.ToString() + " pathways", "LIST", "Pathway list/Pathways containing variables potentially identified by this compound");

            foreach (Pathway p in x.Pathways)
            {
                AddExpandableNode(l, p);
            }
        }

        private static void Expand(TreeNode node, Pathway x)
        {
            var l = node.AddSubNode("Information", "LIST", "Information/Information about this pathway");
            l.AddSubNode("Name = " + x.Name);
            l.AddSubNode("Database ID = " + x.Id);
            l.AddSubNode("Compounds = " + x.Compounds.Count);

            var withVars = x.Compounds.FindAll(λ => λ.Annotations.Count != 0);
            var withoutVars = x.Compounds.FindAll(λ => λ.Annotations.Count == 0);

            l = node.AddSubNode(x.Compounds.Count.ToString() + " compounds", "LIST", "Compound list/Compounds present in this pathway");

            var l2 = l.AddSubNode(withVars.Count.ToString() + " with potential variables", "LIST", "Variable list/Compounds present in this pathway which have variables potentially identifying them.\r\nThe numbers in brackets show the number of variables potentially identifying each compound.");

            foreach (Compound c in withVars)
            {
                AddExpandableNode(l2, c, c.Annotations.Count.ToString());
            }

            l2 = l.AddSubNode(withoutVars.Count.ToString() + " without potential variables", "LIST", "Variable list/Compounds present in this pathway which do not have variables potentially identifying them in the current dataset.");

            foreach (Compound c in withoutVars)
            {
                AddExpandableNode(l2, c);
            }

            Dictionary<Peak, int> vars = new Dictionary<Peak, int>();

            foreach (var c in withVars)
            {
                foreach (var v in c.Annotations)
                {
                    if (vars.ContainsKey(v.Peak))
                    {
                        vars[v.Peak] = vars[v.Peak] + 1;
                    }
                    else
                    {
                        vars[v.Peak] = 1;
                    }
                }
            }

            l = node.AddSubNode(x.Compounds.Count.ToString() + " variables", "LIST", "Variable list/Shows the variables potentially identifying compounds in this pathway.\r\nThe numbers in brackets show the number of compounds in this pathway potentially identified by the variable, out of the total number of compounds potentially identified by this variable.");

            foreach (var kvp in vars)
            {
                AddExpandableNode(l, kvp.Key, Maths.AsFraction(kvp.Value, kvp.Key.Annotations.Count));
            }
        }

        private static void Expand(TreeNode node, Adduct x)
        {
            var l = node.AddSubNode("Information", "LIST", "Information/Information about this adduct");
            l.AddSubNode("Name = " + x.Name);
            l.AddSubNode("Charge = " + x.Charge);
            l.AddSubNode("Mass = " + x.Mz);
        }

        #endregion

        #region SelectNode

        public static bool SelectNode(TreeView tv, object selected)
        {
            return SelectNode(tv, tv.Nodes, selected);
        }

        private static bool SelectNode(TreeView tv, TreeNodeCollection treeNodeCollection, object selected)
        {
            if (selected == null)
            {
                return true;
            }

            foreach (TreeNode n in treeNodeCollection)
            {
                if (n.Tag == selected)
                {
                    tv.SelectedNode = n;
                    n.Expand();
                    return true;
                }

                if (SelectNode(tv, n.Nodes, selected))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
