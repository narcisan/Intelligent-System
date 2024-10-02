using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AliacSearchAlgo;

namespace AISearchSample
{
    class Search
    {
        Fringes fringe;
        ArrayList n;
        bool startInitialized = false;
        bool start=false;

        public Search(ArrayList nodes,int type) 
        {
           if(type==1)//DFS 
            fringe = new Fringe();
           if(type==2)//BFS
            fringe = new Fringe2();
            n = nodes;
            

        }

        public void setStart(Node n)
        {
            n.Start = true;
        }

        public void setGoal(Node n) 
        {
            n.Goal = true;
        }

        public Node search()
        {
            Node explored = null;

            // Find the Start node and add it to the fringe
            for (int i = 0; i < n.Count; i++)
            {
                if (((Node)n[i]).Start == true)
                {
                    fringe.add(((Node)n[i]), null);
                }
            }

            // Perform the search using BFS or DFS based on the fringe implementation
            Node currentNode = null;
            ArrayList neighbors;
            Object[] neighborArray;

            while ((currentNode = fringe.remove()) != null)
            {
                if (currentNode.Goal == true) // Goal node found
                {
                    currentNode.Expanded = true;
                    MessageBox.Show("Goal found: " + currentNode.Name);
                    explored = currentNode;
                    break;
                }

                // Get neighboring nodes and add them to the fringe
                neighbors = currentNode.getNeighbor();
                neighborArray = neighbors.ToArray();

                for (int i = 0; i < neighborArray.Length; i++)
                {
                    Node neighborNode = (Node)neighborArray[i];
                    if (!neighborNode.Expanded) // Only add unexplored nodes
                    {
                        fringe.add(neighborNode, currentNode);
                    }
                }

                // Mark current node as expanded
                currentNode.Expanded = true;
                explored = currentNode;
            }

            return explored; // Return the explored node (null if not found)
        }



        public Node searchone()
        {
            Node explored = null;

            // Initialize the search by adding the start node to the fringe
            if (!startInitialized)
            {
                for (int i = 0; i < n.Count; i++)
                {
                    if (((Node)n[i]).Start == true)
                    {
                        fringe.add(((Node)n[i]), null);
                    }
                }
                startInitialized = true;
            }

            // Perform one step of the search
            Node currentNode = fringe.remove();
            if (currentNode != null)
            {
                if (currentNode.Goal == true) // Goal node found
                {
                    currentNode.Expanded = true;
                    explored = currentNode;
                }
                else
                {
                    // Get neighboring nodes
                    ArrayList neighbors = currentNode.getNeighbor();
                    Object[] neighborArray = neighbors.ToArray();

                    for (int i = 0; i < neighborArray.Length; i++)
                    {
                        Node neighborNode = (Node)neighborArray[i];
                        if (!neighborNode.Expanded) // Add unexplored neighbors
                        {
                            fringe.add(neighborNode, currentNode);
                        }
                    }

                    currentNode.Expanded = true; // Mark current node as expanded
                    explored = currentNode;
                }
            }

            return explored; // Return explored node or null if no further nodes
        }
    }
}