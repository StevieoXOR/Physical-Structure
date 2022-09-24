/*
UNLIKE C++, C# DOESN'T USE REFERENCE OPERATORS (ObjType &Obj) WITH NORMAL USAGE BECAUSE OBJECTS CAN
  BE SHIFTED AROUND A LOT WHEN NEEDED
UNLIKE C++, C# THROWS A NULL-REFERENCE EXCEPTION IF A NULL IS PASSED INTO A FUNCTION WITHOUT THE FUNCTION
  SPECIFIED AS OK WITH TAKING IN NULLABLE ARGUMENTS

class SomeClassName
{
//Can instantiate (set the variable to some value), but generally, save that for the constructor
* private string NameVisibleToOnlyThisClass;
* public  string NameVisibleToEveryoneWhoHasAccessToThisClass;
* 
* //IMPLEMENTATION OF *ALL* CLASS'S FUNCTIONS/METHODS (missing function implementations will throw compiler errors)
//Methods belong strictly to a class. Functions are in a class but don't belong to the class; functions are labeled with static.
* private        MethodVisibleToOnlyThisClass(){						doStuff1;}	//Implementation of private method
* public         MethodVisibleToEveryoneWhoHasAccessToThisClass(){		doStuff2;}	//Implementation of public  method
* private static FunctionVisibleToOnlyThisClass(){						doStuff3;}	//Implementation of private function
* public  static FunctionVisibleToEveryoneWhoHasAccessToThisClass(){	doStuff4;}	//Implementation of public  function

//Default Constructor implementation	(creates Object data)
//Purpose: Set the private variables to their intended default values, which will apply to every Object created by this class
//  when no arguments are passed into the constructor.
//Constructors, Nondefault Constructors, Copy Constructors, and Destructors should always be public so other classes
//  can instantiate(create) Objects of this class's type.
//Acts just like a method but with NO return type.
* public SomeClassName(){	NameVisibleToOnlyThisClass = "some default name";}

//Nondefault Constructor implementation.
//Purpose: Create Object data. Same as Default Constructor, but values can be passed in instead of being OneSizeFitsAllObjects
* public SomeClassName(string InfoThatEveryObjectCreatedByThisClassWillHave, int OtherCriticalDataForEveryObjectInThisClass)
*   {this.info1 = InfoThatEveryObjectCreatedByThisClassWillHave;
*    this.info2 = OtherCriticalDataForEveryObjectOfThisClass;   }
* public SomeClassName(string someNonDefaultName)
*   {NameVisibleToOnlyThisClass =  someNonDefaultName;}

//Copy Constructor implementation.
//Purpose: Copy Object data from the passed-in Object anObjectOfSomeClassName to Object this
//property1 is a member variable that every Object of type SomeClassName has access to, which can be either public or private
* public SomeClassName(SomeClassName anObjectOfSomeClassName)
* 	{this.privateProperty1 =  anObjectOfSomeClassName.privateProperty1;
* 	 this.publicProperty2  =  anObjectOfSomeClassName.publicProperty2; }

//Destructor implementation.   Purpose: Clean up/Delete an object(from this specific class)'s data
//Called when an object created by this class goes out of scope (e.g. an if() statement exits, and any object created
//  in that if() statement is destructed, meaning the object's contents (and then the Object/container itself) are deleted.
* ~SomeClassName(){	SensitiveObjectData = "";}
*/

//If not specified, accessType is internal by default
//accessType static returnType functionName(arguments)								Functions (Methods that don't belong to a class)
//  {implementationOfFunction;}
//
//accessType returnType functionName(arguments)										Methods (functions that belong to a class)
//  {implementationOfFunction;}
//
//accessType className(arguments)			accessType ~className(arguments)		Constructors and Destructors DON'T have a return type.
//  {implementationOfConstructor;}			{implementationOfDestructor;}

using System;		//Lets me write Console.WriteLine() instead of System.Console.WriteLine()
using System.Collections;		//Enables ArrayList to be used
using System.Collections.Generic;   //Enables List<> to be used
using System.Linq;                  //Enables List.Last() to be used

class PhysicalNode
{
    //Private variables only accessible by this class
	//Instead of:	private string Name;
	//				public void   SetName(string nodeName){/*this.*/Name = nodeName;}
	//				public string GetName(){return /*this.*/Name;}
	//Use this: public string Name{get; set;}		NOTE THE public MODIFIER. Use by PhysicalNode.Name = "hi";
	private string Name;	//field
    public string name		//property
	{
		get{return Name;}
		set
		{
			if( !value.Contains("?") ){Name = value;}	//value is a keyword representing valueToAssign
			else{Console.WriteLine("ERROR: Name cannot contain the character '?'. Choose different Name.");}
		}
	}
	private bool debug = false;
	public List<PhysicalNode> ConnectionsFromCurrentNode{get; set;}	 //Each element in the List should be PhysicalNode

	//The 1st of the 2 distance #s describes any point in 2D space when used with the 1st of the 2 2D angles.
	//E.g. dist=5,angle=270deg => yDistFromCurrPoint=5*sin(270)=-5 && xDistFromCurrPoint=5*cos(270)=5*0=0
    //Each element in the List should be {double XYPlane_2DDist,  double 3D_ZOffsetDist}		3D_ZOffsetDist is perpendicular to XYPlane_2DDist
	public List< Tuple<double,double> > ConexionDistancesFromCurrentNode{get; set;}
    //Each element in the List should be {double XYPlane_2DAngle,  double 3D_ZOffsetAngle}		3D_ZOffsetAngle is perpendicular to XYPlane_2DAngle
	public List< Tuple<double,double> > ConexionAnglesFromCurrentNode{get; set;}
    public int TensileStrength{get; set;}
    public int CompressiveStrength{get; set;}
    public int ShearingStrength{get; set;}

    //The following commented-out lines are because C# doesn't declare functions without implementing them.
	//C# declares AND implements in one step.
    /*public  PhysicalNode();																		//Default Constructor
      public  PhysicalNode(string nodeName, List< Tuple<double,double> > nodeConnexionsToNewNode,	//Nondefault Constructor
							int Tstrength, int Cstrength, int Sstrength);
      public  PhysicalNode(PhysicalNode node);					//Copy constructor (yeah, that's all bc Objects are passed by reference)
      public ~PhysicalNode();									//Destructor
      public string PhysNode_toString(List<PhysicalNode> listContaining_PhysNodes);
      void PrintNodeInfo();*/

	public PhysicalNode()
	{
		Name = "defaultNodeName";
		ConnectionsFromCurrentNode       = new List<PhysicalNode>();
		ConexionDistancesFromCurrentNode = new List< Tuple<double,double> >();
		ConexionAnglesFromCurrentNode    = new List< Tuple<double,double> >();
		TensileStrength     = -1;
		CompressiveStrength = -1;
		ShearingStrength    = -1;
	}
	public PhysicalNode(string nodeName, List<PhysicalNode> nodeConnexionsToNewNode)
	{
		/*this.*/Name      = nodeName;
		ConnectionsFromCurrentNode       = nodeConnexionsToNewNode;
		ConexionDistancesFromCurrentNode = new List< Tuple<double,double> >();
		ConexionAnglesFromCurrentNode    = new List< Tuple<double,double> >();
		TensileStrength     = -1;
		CompressiveStrength = -1;
		ShearingStrength    = -1;
	}
	public PhysicalNode(string nodeName, List<PhysicalNode> nodeConxionsToNewNode,  List< Tuple<double,double> >  nodeDistances,  List< Tuple<double,double> >  nodeAngles)
	{
		/*this.*/Name      = nodeName;
		ConnectionsFromCurrentNode       = nodeConxionsToNewNode;
		ConexionDistancesFromCurrentNode = nodeDistances;
		ConexionAnglesFromCurrentNode    = nodeAngles;
		TensileStrength     = -1;
		CompressiveStrength = -1;
		ShearingStrength    = -1;
	}
	public PhysicalNode(string nodeName, List<PhysicalNode> nodeConxionsToNewNode,  List< Tuple<double,double> >  nodeDistances,  List< Tuple<double,double> >  nodeAngles,
						int Tstrength,  int Cstrength,  int Sstrength)
	{
		/*this.*/Name      = nodeName;
		ConnectionsFromCurrentNode       = nodeConxionsToNewNode;
		ConexionDistancesFromCurrentNode = nodeDistances;
		ConexionAnglesFromCurrentNode    = nodeAngles;
		TensileStrength     = Tstrength;
		CompressiveStrength = Cstrength;
		ShearingStrength    = Sstrength;
	}
	public PhysicalNode(PhysicalNode node)
	{
		TensileStrength     = node.TensileStrength;
		CompressiveStrength = node.CompressiveStrength;
		ShearingStrength    = node.ShearingStrength;
								    Name = node.Name;
		ConnectionsFromCurrentNode       = node.ConnectionsFromCurrentNode;
		ConexionDistancesFromCurrentNode = node.ConexionDistancesFromCurrentNode;
		ConexionAnglesFromCurrentNode    = node.ConexionAnglesFromCurrentNode;
	}
	~PhysicalNode()
	{
		Name = "";
		ConnectionsFromCurrentNode       = null;
		ConexionDistancesFromCurrentNode = null;
		ConexionAnglesFromCurrentNode    = null;
		TensileStrength     = 0;
		CompressiveStrength = 0;
		ShearingStrength    = 0;
	}

	//e.g. ( {nodeA,nodeB,nodeF} ){return "A: <1.2mm,2.3mm><13.2°,9.7°>,  B: <3.4mm,4.5mm><1.2°,3.4°>,  F: <66mm,69mm><88°,77°>"
	public string[]  PhysNode_toString(PhysicalNode nodeOfFocus)
	{
		//NOT nodeOfFocus.Capacity
		int conxionNames_ListSize = nodeOfFocus.ConnectionsFromCurrentNode.Count;	//Gets # of node connections in the list of PhysicalNode connections
		int distPairs_ListSize    = nodeOfFocus.ConexionDistancesFromCurrentNode.Count;	//Gets # of tuples in the list of tuples
		int anglePairs_ListSize   = nodeOfFocus.ConexionAnglesFromCurrentNode.Count;	//Gets # of tuples in the list of tuples
		if(debug){Console.WriteLine("PhysNode_toString:   #Conxions: "+conxionNames_ListSize+", #SetsOfDistances: "+distPairs_ListSize+", #SetsOfAngles: "+anglePairs_ListSize);}

		string AllConxionNamesAsString  = "{";
		string AllConxionDistsAsString  = "{";
		string AllConxionAnglesAsString = "{";
		string AllConxionNamesANDDistancesANDAnglesAsString = "{";
		//if((NamesList AND AnglesList) OR NamesList are/is empty){return emptyListsAsStringArray;}
		//if(List is empty){return "{}" for that specific List;}
		//if(no List is empty){return {NamesAsString,DistancesAsString,AnglesAsString,NamesANDDistancesANDAnglesAsString};}
		//Note that the 3 lists can each be different sizes!!!
		int maxListSize = conxionNames_ListSize;
			 if(anglePairs_ListSize>conxionNames_ListSize && anglePairs_ListSize>distPairs_ListSize){  maxListSize = anglePairs_ListSize;}
		else if(conxionNames_ListSize>anglePairs_ListSize && conxionNames_ListSize>distPairs_ListSize){maxListSize = conxionNames_ListSize;}
		else{maxListSize = distPairs_ListSize;}

		//COVERS ALL LIST ITEMS EXCEPT LAST ITEM FROM EACH LIST
		for(int i=0; i<maxListSize-1; i++)
		{
			//if(allListsAreAtValidIndices)
			if(i < conxionNames_ListSize-1  &&  i < distPairs_ListSize-1  &&  i < anglePairs_ListSize-1)//nameList, distsList, anglesList
			{
				PhysicalNode currNodeConnection = nodeOfFocus.ConnectionsFromCurrentNode[i];
				string currNodeName = currNodeConnection.Name;
				Tuple<double,double> currDists = nodeOfFocus.ConexionDistancesFromCurrentNode[i];
				double currHorizoDist = currDists.Item1;
				double currVerticDist = currDists.Item2;
				Tuple<double,double> currAngles = nodeOfFocus.ConexionAnglesFromCurrentNode[i];
				double currHorizoAngle = currAngles.Item1;
				double currVerticAngle = currAngles.Item2;
				AllConxionNamesAsString		+= (currNodeName+", ");
				AllConxionDistsAsString		+= (				"<"+currHorizoDist+"mm, "+currVerticDist+"mm>, ");
				AllConxionAnglesAsString	+= (				                                              "<"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += (currNodeName+": <"+currHorizoDist+"mm, "+currVerticDist+"mm><"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
			}
			//if(namesListIsAtValidIndex AND distListIsAtValidIndex  AND  i isNotLast nameIndex AND i isNotLast distsIndex)
			else if(i < conxionNames_ListSize-1  &&  i < distPairs_ListSize-1)//nameList, distsList, XanglesListX
			{
				string currNodeConnectionName = nodeOfFocus.ConnectionsFromCurrentNode[i].Name;
				Tuple<double,double> currDists = nodeOfFocus.ConexionDistancesFromCurrentNode[i];
				double currHorizoDist = currDists.Item1;
				double currVerticDist = currDists.Item2;
				AllConxionNamesAsString		+= (currNodeConnectionName+", ");
				AllConxionDistsAsString		+= (						 "<"+currHorizoDist+"mm, "+currVerticDist+"mm>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += (currNodeConnectionName+": <"+currHorizoDist+"mm, "+currVerticDist+"mm><?°, ?°>, ");
			}
			//if(namesListIsAtValidIndex AND anglesListIsAtValidIndex  AND  i isNotLast nameIndex AND i isNotLast anglesIndex)
			else if(i < conxionNames_ListSize-1  &&  i < anglePairs_ListSize-1)//nameList, XdistsListX, anglesList
			{
				PhysicalNode currNodeConnection = nodeOfFocus.ConnectionsFromCurrentNode[i];
				string currNodeName = currNodeConnection.Name;
				Tuple<double,double> currAngles = nodeOfFocus.ConexionAnglesFromCurrentNode[i];
				double currHorizoAngle = currAngles.Item1;
				double currVerticAngle = currAngles.Item2;
				AllConxionNamesAsString			 += (currNodeConnection.Name+", ");
				AllConxionAnglesAsString		 += (						   "<"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += (currNodeName+": <?mm, ?mm><"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
			}
			//if(distListIsAtValidIndex AND anglesListIsAtValidIndex  AND  i isNotLast distsIndex AND i isNotLast anglesIndex)
			else if(i < distPairs_ListSize-1  &&  i < anglePairs_ListSize-1)//XnameListX, distsList, anglesList
			{
				Tuple<double,double> currDists = nodeOfFocus.ConexionDistancesFromCurrentNode[i];
				double currHorizoDist = currDists.Item1;
				double currVerticDist = currDists.Item2;
				Tuple<double,double> currAngles = nodeOfFocus.ConexionAnglesFromCurrentNode[i];
				double currHorizoAngle = currAngles.Item1;
				double currVerticAngle = currAngles.Item2;
				AllConxionDistsAsString		+= ("<"+currHorizoDist+"mm, "+currVerticDist+"mm>, ");
				AllConxionAnglesAsString	+= (											  "<"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += ("????: <"+currHorizoDist+"mm, "+currVerticDist+"mm><"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
			}
			//if(namesListIsAtValidIndex  AND  i isNotLast nameIndex)
			else if(i < conxionNames_ListSize-1)//nameList, XdistsListX, XanglesListX
			{
				string currNodeConnectionName = nodeOfFocus.ConnectionsFromCurrentNode[i].Name;
				AllConxionNamesAsString		+= (currNodeConnectionName+", ");
				AllConxionNamesANDDistancesANDAnglesAsString += (currNodeConnectionName+": <?mm, ?mm><?°, ?°>, ");
			}
			//if(distListIsAtValidIndex  AND  i isNotLast distsIndex)
			else if(i < distPairs_ListSize-1)//XnameListX, distsList, XanglesListX
			{
				Tuple<double,double> currDists = nodeOfFocus.ConexionDistancesFromCurrentNode[i];
				double currHorizoDist = currDists.Item1;
				double currVerticDist = currDists.Item2;
				AllConxionDistsAsString		+= ("<"+currHorizoDist+"mm, "+currVerticDist+"mm>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += ("????: <"+currHorizoDist+"mm, "+currVerticDist+"mm><?°, ?°>, ");
			}
			//if(anglesListIsAtValidIndex  AND  i isNotLast anglesIndex)
			else if(i < anglePairs_ListSize-1)//XnameListX, XdistsListX, anglesList
			{
				Tuple<double,double> currAngles = nodeOfFocus.ConexionAnglesFromCurrentNode[i];
				double currHorizoAngle = currAngles.Item1;
				double currVerticAngle = currAngles.Item2;
				AllConxionAnglesAsString	+= (	"<"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
				AllConxionNamesANDDistancesANDAnglesAsString += ("????: <?mm, ?mm><"+currHorizoAngle+"°, "+currVerticAngle+"°>, ");
			}
			//This should never be reached because this entire for() loop would be skipped first
			//if(NO listIsAtValidIndex)
			else//XnameListX, XdistsListX, XanglesListX
			{
				AllConxionNamesAsString		+= "????, ";
				AllConxionDistsAsString		+= 	     "<?mm,?mm>, ";
				AllConxionAnglesAsString	+=					"<?°,?°>, ";
				AllConxionNamesANDDistancesANDAnglesAsString += ("????: <?mm, ?mm><?°, ?°>, ");
			}
		}

		//COVERS LAST LIST ITEM FROM EACH LIST
		//if(allListsAreNotDefault){Add last item of each list to strings}
		if(conxionNames_ListSize > 0  &&  distPairs_ListSize > 0  &&  anglePairs_ListSize > 0)
		{
			string lastConxionName = nodeOfFocus.ConnectionsFromCurrentNode.Last().Name;
			double lastHorizoDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item1;
			double lastVerticDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item2;
			double lastHorizoAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item1;
			double lastVerticAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item2;
			AllConxionNamesAsString			 +=  lastConxionName;
			AllConxionDistsAsString			 += ("<"+lastHorizoDist+"mm, "+lastVerticDist+"mm>");
			AllConxionAnglesAsString		 += ("<"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
			AllConxionNamesANDDistancesANDAnglesAsString += (lastConxionName+": <"+lastHorizoDist+"mm, "+lastVerticDist+"mm><"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
		}
		//if(nameANDdistsListsAreNotDefault){Add last item of each list to strings}
		if(conxionNames_ListSize > 0   &&   distPairs_ListSize > 0)
		{
			string lastConxionName = nodeOfFocus.ConnectionsFromCurrentNode.Last().Name;
			double lastHorizoDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item1;
			double lastVerticDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item2;
			AllConxionNamesAsString			 +=  lastConxionName;
			AllConxionDistsAsString			 += ("<"+lastHorizoDist+"mm, "+lastVerticDist+"mm>");
			AllConxionNamesANDDistancesANDAnglesAsString += (lastConxionName+": <"+lastHorizoDist+"mm, "+lastVerticDist+"mm><?°, ?°>");
		}
		//if(nameANDanglesListsAreNotDefault){Add last item of each list to strings}
		if(conxionNames_ListSize > 0   &&   anglePairs_ListSize > 0)
		{
			string lastConxionName = nodeOfFocus.ConnectionsFromCurrentNode.Last().Name;
			double lastHorizoAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item1;
			double lastVerticAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item2;
			AllConxionNamesAsString			 +=  lastConxionName;
			AllConxionAnglesAsString		 += ("<"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
			AllConxionNamesANDDistancesANDAnglesAsString += (lastConxionName+": <?mm, ?mm><"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
		}
		//if(distsANDanglesListsAreNotDefault){Add last item of each list to strings}
		if(distPairs_ListSize > 0  &&  anglePairs_ListSize > 0)
		{
			double lastHorizoDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item1;
			double lastVerticDist  = nodeOfFocus.ConexionDistancesFromCurrentNode.Last().Item2;
			double lastHorizoAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item1;
			double lastVerticAngle = nodeOfFocus.ConexionAnglesFromCurrentNode.Last().Item2;
			AllConxionDistsAsString			 += ("<"+lastHorizoDist+"mm, "+lastVerticDist+"mm>");
			AllConxionAnglesAsString		 += ("<"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
			AllConxionNamesANDDistancesANDAnglesAsString += ("????: <"+lastHorizoDist+"mm, "+lastVerticDist+"mm><"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
		}
		else if(conxionNames_ListSize > 0)	//if(ONLY namesListIsNotDefault)
		{
			PhysicalNode lastNodeConnection = nodeOfFocus.ConnectionsFromCurrentNode.Last();
			AllConxionNamesAsString			 += lastNodeConnection.Name;
			AllConxionNamesANDDistancesANDAnglesAsString += (lastNodeConnection.Name+": <?mm, ?mm><?°, ?°>");
		}
		else if(distPairs_ListSize > 0)		//if(ONLY distsListIsNotDefault)
		{
			Tuple<double,double> lastDistances = nodeOfFocus.ConexionAnglesFromCurrentNode.Last();
			double lastHorizoDist = lastDistances.Item1;
			double lastVerticDist = lastDistances.Item2;
			AllConxionDistsAsString		 += ("<"+lastHorizoDist+"mm, "+lastVerticDist+"mm>");
			AllConxionNamesANDDistancesANDAnglesAsString += ("????: <"+lastHorizoDist+"mm, "+lastVerticDist+"mm><?°, ?°>");
		}
		else if(anglePairs_ListSize > 0)	//if(ONLY anglesListIsNotDefault)
		{
			Tuple<double,double> lastAngles = nodeOfFocus.ConexionAnglesFromCurrentNode.Last();
			double lastHorizoAngle = lastAngles.Item1;
			double lastVerticAngle = lastAngles.Item2;
			AllConxionAnglesAsString		 += ("<"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
			AllConxionNamesANDDistancesANDAnglesAsString += ("????: <?mm, ?mm><"+lastHorizoAngle+"°, "+lastVerticAngle+"°>");
		}
		AllConxionNamesAsString						 += "}";
		AllConxionDistsAsString						 += "}";
		AllConxionAnglesAsString					 += "}";
		AllConxionNamesANDDistancesANDAnglesAsString += "}";
		return new string[]{AllConxionNamesAsString, AllConxionDistsAsString, AllConxionAnglesAsString, AllConxionNamesANDDistancesANDAnglesAsString};
	}

	public void PrintNodeInfo()
	{
		//VVV toString(ListOfPhysNodeConnections)
		if(!debug)
			{Console.WriteLine("{0}.Connections: {1}", Name, PhysNode_toString(this)[3] );}
		else
		{
			Console.WriteLine( "{0}.Connections:\n  Names-{1}\n  DistancePairs-{2}\n  AnglePairs-{3}\n  Names&Distances&Angles-{4}",Name,
				/*specificNode.*/PhysNode_toString( /*specificNode*/this)[0], PhysNode_toString(this)[1], PhysNode_toString(this)[2], PhysNode_toString(this)[3] );
		}

		Console.Write(         "{0}.TensileStrength (kPa): {1,6}",     /*specificNode.*/Name, /*specificNode.*/TensileStrength     );
		Console.Write(     "\t\t{0}.CompressiveStrength (kPa): {1,6}", /*specificNode.*/Name, /*specificNode.*/CompressiveStrength );
		Console.WriteLine( "\t\t{0}.ShearingStrength (kPa): {1,6}\n",  /*specificNode.*/Name, /*specificNode.*/ShearingStrength    );
	}
}