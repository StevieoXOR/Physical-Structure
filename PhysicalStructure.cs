// 7/16/2022-7/19/2022  Converted from C++, added angle features
// 7/28/2022  Added NodeDistancesList feature
// 8/23/2023  Worked out and added the math (including explanatory comments) for converting distance and angle Tuples into a vector.
//            Did relative speed testing on the if()+trig vs the trig+trig methods.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class PhysicalStructure
{
    public bool debug = false;
	public List<PhysicalNode> NodeList{get; set;}
	public int NumNodes{get; set;}
	public double TwoDim_AngleBetween3Nodes{get; set;}//Include error checking on if(sumConnectionAngles>360){complainOrReduceAngles}


	public short LongestName_Len{get; set;}
	public string PrintOffset = "          ";	//10 spaces

	/*public PhysicalStructure();
	public PhysicalStructure(PhysicalStructure structure);
	public ~PhysicalStructure();

	public PhysicalNode GetNode(string nodeName);
	public void         AddNodeToStructure(PhysicalNode preMadeNode);
	public PhysicalNode AddNodeToStructure(string nodeName, List<PhysicalNode> nodeConnexionsToNewNode);
	public PhysicalNode AddNodeToStructure(string nodeName, List<PhysicalNode> nodeConnexionsToNewNode
	                                        , int Tstrength, int Cstrength, int Sstrength);
	public long               GetNumNodes();
	public long               GetTotalConnections();
	public List<PhysicalNode> GetAllNames_Nodes_List();
	public string             GetAllNames_Nodes_String();
	//public double GetTwoDim_AngleBetween3Nodes();

	//  List of strengths
	public List<int> GetListOfNodeStrengths_Tensile();
	public List<int> GetListOfNodeStrengths_Compressive();
	public List<int> GetListOfNodeStrengths_Shearing();

	//  Value of most/least strong
	public int GetWeakestStrengthVal_Tensile();
	public int GetWeakestStrengthVal_Compressive();
	public int GetWeakestStrengthVal_Shearing();
	public int GetStrongestStrengthVal_Tensile();
	public int GetStrongestStrengthVal_Compressive();
	public int GetStrongestStrengthVal_Shearing();

	//  List of strongest/weakest nodes, which could be 1 Node or more
	public List<PhysicalNode> GetWeakestNode_Tensile();
	/*public List<PhysicalNode> GetWeakestNode_Compressive();
	public List<PhysicalNode> GetWeakestNode_Shearing();
	public List<PhysicalNode> GetStrongestNode_Tensile();
	public List<PhysicalNode> GetStrongestNode_Compressive();
	public List<PhysicalNode> GetStrongestNode_Shearing();

	//  Considers all strengths together. Outputs overall strongest value
	public int GetOverallWeakest_StrengthVal();
	public int GetOverallStrongest_StrengthVal();*/

	/*public void Print();*/



public PhysicalStructure()//Constructor
{
    /*List<PhysicalNode>*/ NodeList = new List<PhysicalNode>();//Creates list of type PhysicalNode.
    NumNodes = 0;
    TwoDim_AngleBetween3Nodes = -1; //2*M_PI; //2Pi radians. 180 degrees.
}
public PhysicalStructure(PhysicalStructure someOtherStructure)//Copy Constructor
{
    NodeList = someOtherStructure.NodeList;
	NumNodes = someOtherStructure.NumNodes;
    TwoDim_AngleBetween3Nodes = someOtherStructure.TwoDim_AngleBetween3Nodes;
}
/*public*/ ~PhysicalStructure()//Destructor      Can't be public
{
    /*List<PhysicalNode>*/ NodeList = null;
    NumNodes = 0;
    TwoDim_AngleBetween3Nodes = 0;
}


//GetNode should look through PhysicalStructure's NodeList List,
// look at each node's name and see if any nodeNames match the inputName.
//If nodeName matches the inputName, return that Node
public PhysicalNode  GetNode(string nodeName) 
{
	if(debug)
		{Console.WriteLine("nodeX = material.GetNode(\"{0}\");", nodeName);}
	if(nodeName.Contains("?"))	//NodeList SHOULD never have a name with a '?' in it. If it somehow does, this doesn't let it be found
	{
		Console.WriteLine("'?' is an illegal character for names. Default node returned.");
		return new PhysicalNode();
	}
    if(nodeName.Length > LongestName_Len)   //if(nodeToLookFor isLongerThan existingNodeWithLongestName){soughtNodeDoesNotExist}
    {
        Console.WriteLine($"Node \"{nodeName}\" not found. Default node returned.");
        return new PhysicalNode();
    }
    else
    {
        for(int nodeID=0; nodeID<NodeList.Count; nodeID++)
        {
            PhysicalNode currNode = NodeList[nodeID];
            if(String.Equals(nodeName, currNode.name))//If( thereIsANodeWithSameNameAsUserInput ){return nodeWithSameName;}
            {
                if(debug){	Console.WriteLine("FOUND A NODE WITH THE SAME USER-SET NAME!");}
                return currNode;
            }
        }
        Console.WriteLine($"Node \"{nodeName}\" not found. Default node returned.");
        return new PhysicalNode();
    }
}

//Adds node to NodeList
public void AddNodeToStructure(PhysicalNode preMadeNode)
{
    short addedNode_NameLen = (short)preMadeNode.name.Length;
	if(addedNode_NameLen > LongestName_Len)
		{LongestName_Len = addedNode_NameLen;}
	NodeList.Add(preMadeNode);
	NumNodes++;
}
public PhysicalNode  AddNodeToStructure(string nodeName, List<PhysicalNode> nodeConnexionsToNewNode, List< Tuple<double,double> > nodeDistances, List< Tuple<double,double> > nodeAngles)
{
	if((short)nodeName.Length > LongestName_Len)
		{LongestName_Len = (short)nodeName.Length;}
    PhysicalNode newNode = new PhysicalNode(nodeName, nodeConnexionsToNewNode, nodeDistances, nodeAngles);
	if(debug)
	{
		string name = newNode.name;
		Console.WriteLine("MaterialName.AddNodeToStructure(\"{0}\",Connections,Distances,Angles);", name);
		Console.WriteLine("  LastNodeInListOfNodes_BeforeAppending\"{0}\".Name:   \"{1}\"", name, NodeList.Last().name);
		Console.WriteLine("  newNodeConnections: {0}", newNode.PhysNode_toString(newNode)[3]);
		Console.WriteLine("  NodeList.Count: {0}", NodeList.Count);
        Console.WriteLine("  #Nodes before/after structure.AddNode(): {0}/{1}\n", NumNodes++, NumNodes);
	}
    else{NumNodes++;}
	NodeList.Add(newNode);//Guaranteed to happen unless no available memory left

    return newNode;
}
public PhysicalNode  AddNodeToStructure( string nodeName, List<PhysicalNode> nodeConnexionsToNewNode,  List< Tuple<double,double> > nodeDistances, 
                                        List< Tuple<double,double> > nodeAngles, int Tstrength, int Cstrength, int Sstrength)
{
    if((short)nodeName.Length > LongestName_Len)
		{LongestName_Len = (short)nodeName.Length;}
	PhysicalNode newNode = new PhysicalNode(nodeName, nodeConnexionsToNewNode, nodeDistances, nodeAngles, Tstrength, Cstrength, Sstrength);
	if(debug)
	{
		string name = newNode.name;
        Console.WriteLine("MaterialName.AddNodeToStructure(\"{0}\",Connections,Distances,Angles,Tensile,Compressive,Shearing);",name);
		Console.WriteLine("  LastNodeInListOfNodes_BeforeAppending\"{0}\".Name:   \"{1}\"", name, NodeList.Last().name   );
		Console.WriteLine("  {0}.Connections: {1}", name, newNode.PhysNode_toString(newNode)[3]);
		Console.Write(	  "  {0}.TensileStrength(): {1}",                     name, newNode.TensileStrength      );
		Console.Write(	  "  {0}{1}.CompressiveStrength(): {2}", PrintOffset, name, newNode.CompressiveStrength  );
		Console.WriteLine("  {0}{1}.ShearingStrength(): {2}",    PrintOffset, name, newNode.ShearingStrength     );
		Console.WriteLine("  NodeList.Count: {0}", NodeList.Count);
		Console.WriteLine("  #Nodes before/after structure.AddNode(): {0}/{1}\n", NumNodes++, NumNodes);
	}
	else{NumNodes++;}
	NodeList.Add(newNode);//Guaranteed to happen unless no available memory left. Adds a Node object to end of NodeList

    return newNode;
}


//USES ONLY THE NAMES LIST, NOT THE ANGLES
public long  GetTotalConnections() //Returns total # of connections across all nodes
{
    long totalConnections = 0;
	if(debug){	Console.WriteLine("\nGetTotalConnections():");}
    for(int nodeID=0; nodeID<NodeList.Count; nodeID++)
    {
        PhysicalNode currNode = NodeList[nodeID];//Get the 0th Node, 1st Node, 2nd, etc as the loop progresses
		//#AllConxions   =   #AllConnxions + currNodeConnxions.Count
		totalConnections += currNode.ConnectionsFromCurrentNode.Count;//Add connections of currentNode to totalConnections
        if(debug)
		{
			Console.WriteLine("***{0,-25}", currNode.name   );
			Console.WriteLine("***{0,-60}", currNode.PhysNode_toString(currNode)[2]   );
			Console.WriteLine("***#TotalConnections {0,-25}\n", totalConnections);
		}
    }
    return totalConnections;
}
public List<string>  GetAllNames_Nodes_List()
{
    List<string> allNames_Nodes = new List<string>();
    for(int nodeID=0; nodeID<NodeList.Count; nodeID++)
    {
        PhysicalNode currentNode = NodeList[nodeID];//Get the 0th Node, 1st Node, 2nd, etc as the loop progresses
        allNames_Nodes.Add( currentNode.name );//Add nameOfCurrentNode to end of List with all node names
        Console.WriteLine($"allNames_Nodes.at({nodeID}): {allNames_Nodes[nodeID]},  ");
    }
    return allNames_Nodes;
}
public string  GetAllNames_Nodes_String()
{
    string allNames_Nodes = "{";
    for(int nodeID=0; nodeID < NodeList.Count-1; nodeID++)
    {
        PhysicalNode currentNode = NodeList[nodeID];//Get the 0th Node, 1st Node, 2nd, etc as the loop progresses
        //Add nameOfCurrentNode to end of the string with all node names
		allNames_Nodes += currentNode.name+",  ";	// {Jeffrey, Mike}
    }
	allNames_Nodes += (NodeList.Last().name + "}");
    return allNames_Nodes;
}
//public double  GetTwoDim_AngleBetween3Nodes()
    //  {return }



//strengthList -> Isolate max/min strengthValues -> Match strengthValues to Nodes -> Find overall strength
//Yes, these functions could be simpler AND more efficient, but I'm going for readability
public List<int>  GetListOfNodeStrengths_Tensile()
{
    //List<int> listOfTensileStrengths;     THIS ALLOCATES NO MEMORY AND HENCE IS USELESS
    List<int> listOfTensileStrengths = new List<int>();
    for(int i=0; i<NodeList.Count; i++)
    {
        PhysicalNode currNode = NodeList[i];
        listOfTensileStrengths.Add( currNode.TensileStrength );
        //^^^Appends each individual node's tensile strength to list of tensile strengths
        //^^^Create list of tensile strengths containing each node's tensile strength
    }
    return listOfTensileStrengths;
}
public List<int>  GetListOfNodeStrengths_Compressive()
{
    List<int> listOfCompressiveStrengths = new List<int>();
    for(int i=0; i<NodeList.Count; i++)
    {
        PhysicalNode currNode = NodeList[i];
        listOfCompressiveStrengths.Add( currNode.CompressiveStrength );
        //^^^Appends each individual node's compressive strength to list of compressive strengths
        //^^^Create list of compressive strengths containing each node's compressive strength
    }
    return listOfCompressiveStrengths;
}
public List<int>  GetListOfNodeStrengths_Shearing()
{
    List<int> listOfShearingStrengths = new List<int>();
    for(int i=0; i<NodeList.Count; i++)
    {
        PhysicalNode currNode = NodeList[i];
        listOfShearingStrengths.Add( currNode.ShearingStrength );
        //^^^Appends each individual node's shearing strength to list of shearing strengths
        //^^^Create list of shearing strengths containing each node's shearing strength
    }
    return listOfShearingStrengths;
}


public int  GetWeakestStrengthVal_Tensile()
{
    int weakestStrength_Tensile = int.MaxValue;  //weakest = Max int value
    List<int> list_TensileStrengths = GetListOfNodeStrengths_Tensile();

    for(int i=0; i<list_TensileStrengths.Count; i++)
    {
        int currNodeStrength_Tensile = list_TensileStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Tensile < weakestStrength_Tensile)
			{weakestStrength_Tensile = currNodeStrength_Tensile;}
        //^^^Sort through list of nodes and their strengths to find the lowest tensileStrength
    }
    return weakestStrength_Tensile;
}
public int  GetWeakestStrengthVal_Compressive()
{
    int weakestStrength_Compressive = int.MaxValue;  //weakest = Max int value
    List<int> list_CompressiveStrengths = GetListOfNodeStrengths_Compressive();

    for(int i=0; i<list_CompressiveStrengths.Count; i++)
    {
        int currNodeStrength_Compressive = list_CompressiveStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Compressive < weakestStrength_Compressive)
            {weakestStrength_Compressive = currNodeStrength_Compressive;}
        //^^^Sort through list of nodes and their strengths to find the lowest compressiveStrength
    }
    return weakestStrength_Compressive;
}
public int  GetWeakestStrengthVal_Shearing()
{
    int weakestStrength_Shearing = int.MaxValue;  //weakest = Max int value
    List<int> list_ShearingStrengths = GetListOfNodeStrengths_Shearing();

    for(int i=0; i<list_ShearingStrengths.Count; i++)
    {
        int currNodeStrength_Shearing = list_ShearingStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Shearing < weakestStrength_Shearing)
			{weakestStrength_Shearing = currNodeStrength_Shearing;}
        //^^^Sort through list of nodes and their strengths to find the lowest shearingStrength
    }
    return weakestStrength_Shearing;
}

public int  GetStrongestStrengthVal_Tensile()
{
    int highestStrength_Tensile = int.MinValue;  //Strongest = Min int value
    List<int> list_TensileStrengths = GetListOfNodeStrengths_Tensile();

    for(int i=0; i<list_TensileStrengths.Count; i++)
    {
        int currNodeStrength_Tensile = list_TensileStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Tensile > highestStrength_Tensile)
			{highestStrength_Tensile = currNodeStrength_Tensile;}
        //^^^Sort through list of nodes and their strengths to find the highest tensileStrength
    }
    return highestStrength_Tensile;
}
public int  GetStrongestStrengthVal_Compressive()
{
    int highestStrength_Compressive = int.MinValue;  //Strongest = Min int value
    List<int> list_CompressiveStrengths = GetListOfNodeStrengths_Compressive();

    for(int i=0; i<list_CompressiveStrengths.Count; i++)
    {
        int currNodeStrength_Compressive = list_CompressiveStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Compressive > highestStrength_Compressive)
			{highestStrength_Compressive = currNodeStrength_Compressive;}
        //^^^Sort through list of nodes and their strengths to find the highest CompressiveStrength
    }
    return highestStrength_Compressive;
}
public int  GetStrongestStrengthVal_Shearing()
{
    int highestStrength_Shearing = int.MinValue;  //Strongest = Min int value
    List<int> list_ShearingStrengths = GetListOfNodeStrengths_Shearing();

    for(int i=0; i<list_ShearingStrengths.Count; i++)
    {
        int currNodeStrength_Shearing = list_ShearingStrengths[i];

        //The if() doesn't use "<=" because this part just finds #s, not the nodes themselves.
        //Since only finding #s, assigning the same value would be a waste of a needless computation
        if(currNodeStrength_Shearing > highestStrength_Shearing)
			{highestStrength_Shearing = currNodeStrength_Shearing;}
        //^^^Sort through list of nodes and their strengths to find the highest CompressiveStrength
    }
    return highestStrength_Shearing;
}


public List<PhysicalNode> GetWeakestNode_Tensile()
{
    int weakestTensileStrength = GetWeakestStrengthVal_Tensile();
    List<PhysicalNode>  listOfWeakestNodes_Tensile = new List<PhysicalNode>();

    for(int i=0; i<NodeList.Count; i++)
    {
        PhysicalNode currNode = NodeList[i];
        int currNodeStrength_Tensile = currNode.TensileStrength;

        //If(currentNodeHasWeakestTensileStrength){addNodeTo_listOfWeakestTensileStrengthNodes;}
        if(currNodeStrength_Tensile == weakestTensileStrength)
			{listOfWeakestNodes_Tensile.Add(currNode);}
        //^^^Sort through list of nodes to find the node(s) with the previously found weakest tensileStrength
    }
    return listOfWeakestNodes_Tensile;
}/*
public List<PhysicalNode>  GetWeakestNode_Compressive()
{
    return null;
}
public List<PhysicalNode> GetWeakestNode_Shearing()
{
    return null;
}

public List<PhysicalNode> GetStrongestNode_Tensile()
{
    return null;
}
public List<PhysicalNode> GetStrongestNode_Compressive()
{
    return null;
}
public List<PhysicalNode> GetStrongestNode_Shearing()
{
    return null;
}



public List<PhysicalNode> GetOverallWeakest_StrengthVal()
{
    Console.WriteLine("FIXME: GetOverallWeakestNode");
    return null;
}
public List<PhysicalNode> GetOverallStrongest_StrengthVal()
{
    Console.WriteLine("FIXME: GetOverallStrongestNode");
    return null;
}*/


public void Print() 
{
	Console.Write("\nMATERIAL STRUCTURE PRINT:");
    Console.Write("\n  Number of Nodes in structure: {0}", /*structure.*/NumNodes              );
	Console.Write("\n  NodeList.Count:               {0}", /*structure.*/NodeList.Count        );
    Console.Write("\n  Total connections:            {0}", /*structure.*/GetTotalConnections() );
    Console.Write("\n  Names of structural nodes:    ");
	Console.WriteLine(  GetAllNames_Nodes_String() );
    //Console.WriteLine( "Weakest Node:      ", /*structure.*/GetOverallWeakest_StrengthVal()   );
    //Console.WriteLine( "Strongest Node:    ", /*structure.*/GetOverallStrongest_StrengthVal() );
	Console.WriteLine( "END MATERIAL STRUCTURE PRINT" );
}





public void Test()
{
	PhysicalNode rootNode = new PhysicalNode();
    PhysicalNode node1    = new PhysicalNode();
    PhysicalNode node2    = new PhysicalNode();
    PhysicalNode node6    = new PhysicalNode();
    PhysicalNode node7    = new PhysicalNode();
    PhysicalStructure material = new PhysicalStructure();

    List<PhysicalNode> emptyPNlist              = new List<PhysicalNode>{};
    List< Tuple<double,double> > emptyTupleList = new List< Tuple<double,double> >{};
    Tuple<double,double> pair1 = new Tuple<double,double>(1.2,3.4);
    Tuple<double,double> pair2 = Tuple.Create(5.6,7.8);
    Tuple<double,double> pair3 = new Tuple<double,double>(9.1,11.12);
    List< Tuple<double,double> > tupleList1 = new List< Tuple<double,double> >{pair1,pair2};
    List< Tuple<double,double> > tupleList2 = new List< Tuple<double,double> >{pair2,pair3};

    //CreateNewNode(string Name, List<PhysNode> Connections, List<double> ConxionAngles,  int TensStren,int ComprStren,int ShearStren)
	PhysicalNode node3 = new PhysicalNode("Thing1", emptyPNlist,  emptyTupleList, emptyTupleList,   15,  35,  55);
	PhysicalNode node4 = new PhysicalNode("Thing2", new List<PhysicalNode>{node3,node1},  emptyTupleList, emptyTupleList,   20,  40,  60);
	PhysicalNode node5 = new PhysicalNode("Thing3", new List<PhysicalNode>{node4,node1},  tupleList1,     tupleList2,      100, 101, 102);
    
    node1.name = "Abogado";
    node2.name = "Alabamana";
    node2.name = "?Hi";
    node1.ConnectionsFromCurrentNode = new List<PhysicalNode>{ node1,node4,node2 };//Square shape, plus selfloop at node1 (which should have angle of <0,0>
    node2.ConnectionsFromCurrentNode = new List<PhysicalNode>{ node1,node3 };
    List< Tuple<double,double> >  pairs1 = new List< Tuple<double,double> >{Tuple.Create(4.4,4.5), Tuple.Create(4.6,4.7)};
    //List< Tuple<double,double> > angles = new List< Tuple<double, double> >(){pair1, pair2, pair3};	//COMPILES
    //List< Tuple<double,double> > angles = new List< Tuple<double, double> >{pair1, pair2, pair3}();	//ERROR
    //List< Tuple<double,double> > angles = new List< Tuple<double, double> >(pair1, pair2, pair3);		//ERROR bc no constructor takes ()
    List< Tuple<double,double> >   angles = new List< Tuple<double, double> >{pair1, pair2, pair3};
    node1.ConexionAnglesFromCurrentNode = angles;
    node2.ConexionAnglesFromCurrentNode = pairs1;
    node3.ConexionAnglesFromCurrentNode = new List< Tuple<double,double> >{Tuple.Create(171.302,0.2), Tuple.Create(20.02,30.03)};
    node1.TensileStrength = 5;
    node2.TensileStrength = 10;
    node1.CompressiveStrength = 25;
    node2.CompressiveStrength = 30;
    node1.ShearingStrength = 45;
    node2.ShearingStrength = 50;
    

    Console.WriteLine("node1.Name: {0}", node1.name);
    Console.WriteLine("node1.Connections: {0}", node1.PhysNode_toString( node1 )[3]);
    Console.WriteLine("node1.TensileStrength: {0,6}\tnode1.CompressiveStrength: {1,6}\t\tnode1.ShearingStrength: {2,6}\n",
					node1.TensileStrength, node1.CompressiveStrength, node1.ShearingStrength);
    Console.WriteLine("node2.Name: {0}", node2.name);
    Console.WriteLine("node2.Connections: {0}", node2.PhysNode_toString( node2 )[3]);
    Console.WriteLine("node2.TensileStrength: {0,6}\tnode2.CompressiveStrength: {1,6}\t\tnode2.ShearingStrength: {2,6}\n",
					node2.TensileStrength, node2.CompressiveStrength, node2.ShearingStrength);

    node3.PrintNodeInfo();
    node4.PrintNodeInfo();

    material.AddNodeToStructure(node2);
    material.AddNodeToStructure(node4);

    PhysicalNode node8 = material.GetNode("Alabamana");
    Console.WriteLine("node8.Name: {0}\n", node8.name);//Should get node2, which is "Alabamana"
    node8 = material.GetNode("Thing2");
    Console.WriteLine("node8.Name: {0}\n", node8.name);//Should get node4, which is "Thing2"
    node8 = material.GetNode("Thing");
    Console.WriteLine("node8.Name: {0}\n", node8.name);//Should get nothing since no Node has Name "Thing"
    

	//PhysicalNode constructor
    PhysicalNode abc = new PhysicalNode("ABC", emptyPNlist, emptyTupleList, emptyTupleList, 150, 175, 200);
    PhysicalNode def = new PhysicalNode("DEF", new List<PhysicalNode>{abc}, new List<Tuple<double, double>>{new Tuple<double, double>(1.2,3.4)}, new List<Tuple<double, double>>{Tuple.Create(1.2,3.4)}, 201, 202, 203);

	PhysicalNode bcd = material.AddNodeToStructure("BCD", new List<PhysicalNode>{abc}, new List<Tuple<double, double>>{new Tuple<double, double>(1.2,3.4)}, new List<Tuple<double, double>>{Tuple.Create(1.2,3.4)}, 90, 95, 100);//Node constructor in Material

	PhysicalNode cde = material.AddNodeToStructure("CDE", new List<PhysicalNode>{bcd,abc}, tupleList1, tupleList2);			//Node constructor in Material

    abc.PrintNodeInfo();
	bcd.PrintNodeInfo();
	cde.PrintNodeInfo();
	def.PrintNodeInfo();

	material.AddNodeToStructure(abc);
	material.AddNodeToStructure(def);
    
    material.Print();

    List<Tuple<double, double>> list_tuple90_180   = new List<Tuple<double, double>>{new Tuple<double, double>(90.0, 180.0)};
    List<Tuple<double, double>> list_tuple1_2__3_4 = new List<Tuple<double, double>>{new Tuple<double, double>(1.25, 3.125)};
    PhysicalNode pn1234_135x2 = new PhysicalNode("UniqueName", new List<PhysicalNode>{abc}, list_tuple1_2__3_4, list_tuple90_180);
    Tuple<double,double,double> vec3D = PhysicalNode.convertDistsAndAnglesTo3DVector(pn1234_135x2.ConexionDistancesFromCurrentNode[0], pn1234_135x2.ConexionAnglesFromCurrentNode[0]);
    Console.WriteLine("PhysicalNode.convertDistsAndAnglesTo3DVector(distancesList, anglesList):  <{0}, {1}, {2}>",vec3D.Item1, vec3D.Item2, vec3D.Item3);
    
}
}