using System;
using static System.Math;  //Math.Sqrt() => Sqrt()  Idk why it needs "static" though
using System.Collections;
using System.Collections.Generic;
using System.Linq;
class Truss
{
    public static Tuple<double, double> tuple0 = new Tuple<double, double>(0, 0);
    public static Tuple<double, double> tuple45   = Tuple.Create(45.0, 45.0);
    public static Tuple<double, double> tuple45_0 = Tuple.Create(45.0,  0.0);
    public static Tuple<double, double> tuple0_45 = Tuple.Create( 0.0, 45.0);
    public static Tuple<double, double> tuple50   = Tuple.Create(50.0, 50.0);
    public static Tuple<double, double> tuple50_0 = Tuple.Create(50.0,  0.0);
    public static Tuple<double, double> tuple0_50 = Tuple.Create( 0.0, 50.0);
    public static Tuple<double, double> tuple50sqrt2   = Tuple.Create(50*Sqrt(2), 50*Sqrt(2));
    public static Tuple<double, double> tuple50sqrt2_0 = Tuple.Create(50*Sqrt(2),        0.0);
    public static Tuple<double, double> tuple0_50sqrt2 = Tuple.Create(       0.0, 50*Sqrt(2));
    public static Tuple<double, double> tuple90    = Tuple.Create(90.0, 90.0);
    public static Tuple<double, double> tuple90_0  = Tuple.Create(90.0,  0.0);
    public static Tuple<double, double> tuple0_90  = Tuple.Create( 0.0, 90.0);
    public static Tuple<double, double> tuple120     = Tuple.Create(120.0, 120.0);
    public static Tuple<double, double> tuple120_0   = Tuple.Create(120.0,   0.0);
    public static Tuple<double, double> tuple0_120   = Tuple.Create(  0.0, 120.0);
    public static Tuple<double, double> tuple135   = Tuple.Create(135.0, 135.0);
    public static Tuple<double, double> tuple135_0 = Tuple.Create(135.0,   0.0);
    public static Tuple<double, double> tuple0_135 = Tuple.Create(  0.0, 135.0);
    public static Tuple<double, double> tuple180     = Tuple.Create(180.0, 180.0);
    public static Tuple<double, double> tuple180_0   = Tuple.Create(180.0,   0.0);
    public static Tuple<double, double> tuple0_180   = Tuple.Create(  0.0, 180.0);
    public static Tuple<double, double> tuple225   = Tuple.Create(225.0, 225.0);
    public static Tuple<double, double> tuple225_0 = Tuple.Create(225.0,   0.0);
    public static Tuple<double, double> tuple0_225 = Tuple.Create(  0.0, 225.0);
    public static Tuple<double, double> tuple270     = Tuple.Create(270.0, 270.0);
    public static Tuple<double, double> tuple270_0   = Tuple.Create(270.0,   0.0);
    public static Tuple<double, double> tuple0_270   = Tuple.Create(  0.0, 270.0);
    public static Tuple<double, double> tuple315   = Tuple.Create(315.0, 315.0);
    public static Tuple<double, double> tuple315_0 = Tuple.Create(315.0,   0.0);
    public static Tuple<double, double> tuple0_315 = Tuple.Create(  0.0, 315.0);
    public static Tuple<double, double> tuple150 = Tuple.Create(150.0, 150.0);
    public static Tuple<double, double> tuple1_2__3_4 = Tuple.Create(1.2, 3.4);
    public static Tuple<double, double> tuple5_6__7_8 = Tuple.Create(5.6, 7.8);
    public static List<Tuple<double, double>> emptyTupleList      = new List<Tuple<double, double>> { };
    public static List<Tuple<double, double>> tupleList1_2__3_4   = new List<Tuple<double, double>> { new Tuple<double, double>(1.2, 3.4) };
    public static List<Tuple<double, double>> tupleList50       = new List<Tuple<double, double>> { tuple50 };
    public static List<Tuple<double, double>> tupleList50x2     = new List<Tuple<double, double>> { tuple50,tuple50 };
    public static List<Tuple<double, double>> tupleList50x4     = new List<Tuple<double, double>> { tuple50,tuple50,tuple50,tuple50 };
    public static List<Tuple<double, double>> tupleList50Sqrt2    = new List<Tuple<double, double>> { tuple50sqrt2 };
    public static List<Tuple<double, double>> tupleList50Sqrt2_0  = new List<Tuple<double, double>> { tuple50sqrt2_0 };
    public static List<Tuple<double, double>> tupleList0_50Sqrt2  = new List<Tuple<double, double>> { tuple0_50sqrt2 };
    public static List<Tuple<double, double>> tupleList45        = new List<Tuple<double, double>> { tuple45 };
    public static List<Tuple<double, double>> tupleList45x2      = new List<Tuple<double, double>> { tuple45,tuple45 };
    public static List<Tuple<double, double>> tupleList45x4      = new List<Tuple<double, double>> { tuple45,tuple45,tuple45,tuple45 };
    public static List<Tuple<double, double>> tupleList45_0        = new List<Tuple<double, double>> { tuple45_0 };
    public static List<Tuple<double, double>> tupleList45_0x2      = new List<Tuple<double, double>> { tuple45_0,tuple45_0 };
    public static List<Tuple<double, double>> tupleList45_0x4      = new List<Tuple<double, double>> { tuple45_0,tuple45_0,tuple45_0,tuple45_0 };
    public static List<Tuple<double, double>> tupleList0_45      = new List<Tuple<double, double>> { tuple0_45 };
    public static List<Tuple<double, double>> tupleList90        = new List<Tuple<double, double>> { tuple90 };
    public static List<Tuple<double, double>> tupleList90_0        = new List<Tuple<double, double>> { tuple90_0 };
    public static List<Tuple<double, double>> tupleList90_0x2      = new List<Tuple<double, double>> { tuple90_0,tuple90_0 };
    public static List<Tuple<double, double>> tupleList90_0x4      = new List<Tuple<double, double>> { tuple90_0,tuple90_0,tuple90_0,tuple90_0 };
    public static List<Tuple<double, double>> tupleList0_90      = new List<Tuple<double, double>> { tuple0_90 };
    public static List<Tuple<double, double>> tupleList135         = new List<Tuple<double, double>> { tuple135 };
    public static List<Tuple<double, double>> tupleList135_0         = new List<Tuple<double, double>> { tuple135_0 };
    public static List<Tuple<double, double>> tupleList135_0x2       = new List<Tuple<double, double>> { tuple135_0,tuple135_0 };
    public static List<Tuple<double, double>> tupleList135_0x4       = new List<Tuple<double, double>> { tuple135_0,tuple135_0,tuple135_0,tuple135_0 };
    public static List<Tuple<double, double>> tupleList0_135       = new List<Tuple<double, double>> { tuple0_135 };
    public static List<Tuple<double, double>> tupleList180       = new List<Tuple<double, double>> { tuple180 };
    public static List<Tuple<double, double>> tupleList180x2     = new List<Tuple<double, double>> { tuple180,tuple180 };
    public static List<Tuple<double, double>> tupleList180x4     = new List<Tuple<double, double>> { tuple180,tuple180,tuple180,tuple180 };
    public static List<Tuple<double, double>> tupleList180_0       = new List<Tuple<double, double>> { tuple180_0 };
    public static List<Tuple<double, double>> tupleList180_0x2     = new List<Tuple<double, double>> { tuple180_0,tuple180_0 };
    public static List<Tuple<double, double>> tupleList180_0x4     = new List<Tuple<double, double>> { tuple180_0,tuple180_0,tuple180_0,tuple180_0 };
    public static List<Tuple<double, double>> tupleList0_180     = new List<Tuple<double, double>> { tuple0_180 };
    public static List<Tuple<double, double>> tupleList225         = new List<Tuple<double, double>> { tuple225 };
    public static List<Tuple<double, double>> tupleList225_0         = new List<Tuple<double, double>> { tuple225_0 };
    public static List<Tuple<double, double>> tupleList225_0x2       = new List<Tuple<double, double>> { tuple225_0,tuple225_0 };
    public static List<Tuple<double, double>> tupleList225_0x4       = new List<Tuple<double, double>> { tuple225_0,tuple225_0,tuple225_0,tuple225_0 };
    public static List<Tuple<double, double>> tupleList0_225       = new List<Tuple<double, double>> { tuple0_225 };
    public static List<Tuple<double, double>> tupleList270       = new List<Tuple<double, double>> { tuple270 };
    public static List<Tuple<double, double>> tupleList270x2     = new List<Tuple<double, double>> { tuple270,tuple270 };
    public static List<Tuple<double, double>> tupleList270x4     = new List<Tuple<double, double>> { tuple270,tuple270,tuple270,tuple270 };
    public static List<Tuple<double, double>> tupleList270_0       = new List<Tuple<double, double>> { tuple270_0 };
    public static List<Tuple<double, double>> tupleList270_0x2     = new List<Tuple<double, double>> { tuple270_0,tuple270_0 };
    public static List<Tuple<double, double>> tupleList270_0x4     = new List<Tuple<double, double>> { tuple270_0,tuple270_0,tuple270_0,tuple270_0 };
    public static List<Tuple<double, double>> tupleList0_270     = new List<Tuple<double, double>> { tuple0_270 };
    public static List<Tuple<double, double>> tupleList315         = new List<Tuple<double, double>> { tuple315 };
    public static List<Tuple<double, double>> tupleList315x2       = new List<Tuple<double, double>> { tuple315,tuple315 };
    public static List<Tuple<double, double>> tupleList315x4       = new List<Tuple<double, double>> { tuple315,tuple315,tuple315,tuple315 };
    public static List<Tuple<double, double>> tupleList315_0         = new List<Tuple<double, double>> { tuple315_0 };
    public static List<Tuple<double, double>> tupleList315_0x2       = new List<Tuple<double, double>> { tuple315_0,tuple315_0 };
    public static List<Tuple<double, double>> tupleList315_0x4       = new List<Tuple<double, double>> { tuple315_0,tuple315_0,tuple315_0,tuple315_0 };
    public static List<Tuple<double, double>> tupleList0_315       = new List<Tuple<double, double>> { tuple0_315 };

    //horiz == horizontal == flat (2-dimensional)
    //  .---.
    //  |\ /|
    //  | * |     Overall shape (doesn't extend into 3rd spatial dimension)
    //  |/ \|
    //  *---*
    // Tuples are in the following format: (angleIn2D_XYplane,angleIn3D_butOnlyZaxis)
    public static List<Tuple<double, double>> horizAnglesFromTopLeftNode      =  new List<Tuple<double, double>> { tuple270_0/*down*/, tuple0/*right*/ };
    public static List<Tuple<double, double>> horizAnglesFromTopRightNode     =  new List<Tuple<double, double>> { tuple180_0/*left*/, tuple270_0/*down*/ };
    public static List<Tuple<double, double>> horizAnglesFromBottomLeftNode   =  new List<Tuple<double, double>> { tuple90_0/*up*/, tuple0/*right*/ };
    public static List<Tuple<double, double>> horizAnglesFromBottomRightNode  =  new List<Tuple<double, double>> { tuple90_0/*up*/, tuple180_0/*left*/ };
    public static List<Tuple<double, double>> horizDiagAnglesFromCentralNode  =  new List<Tuple<double, double>> { tuple45_0/*upRight*/,tuple135_0/*upLeft*/,tuple225_0/*downLeft*/, tuple315_0/*downRight*/ };
    public static List<Tuple<double, double>> horizSqrAnglesFromCentralNode   =  new List<Tuple<double, double>> { tuple90_0/*up*/,tuple180_0/*left*/,tuple270_0/*down*/, tuple0/*right*/ };
    
    public int FeNodeSize = 15;
    public static int FeTensileStrength     = 150;
    public static int FeCompressiveStrength = 125;
    public static int FeShearingStrength    =  75;
    public List<PhysicalNode> nodeConxions = new List<PhysicalNode>();
    
    public Truss() //Constructor
	{
        PhysicalNode a = new PhysicalNode("UpperLeft",   new List<PhysicalNode> {}, tupleList50x2, horizAnglesFromTopLeftNode,     FeTensileStrength, FeCompressiveStrength, FeShearingStrength);
        PhysicalNode b = new PhysicalNode("UpperRight",  new List<PhysicalNode> {}, tupleList50x2, horizAnglesFromTopRightNode,    FeTensileStrength, FeCompressiveStrength, FeShearingStrength);
        PhysicalNode c = new PhysicalNode("LowerLeft",   new List<PhysicalNode> {}, tupleList50x2, horizAnglesFromBottomLeftNode,  FeTensileStrength, FeCompressiveStrength, FeShearingStrength);
        PhysicalNode d = new PhysicalNode("LowerRight",  new List<PhysicalNode> {}, tupleList50x2, horizAnglesFromBottomRightNode, FeTensileStrength, FeCompressiveStrength, FeShearingStrength);
        PhysicalNode center = new PhysicalNode("Center", new List<PhysicalNode> {}, tupleList50x4, horizDiagAnglesFromCentralNode, FeTensileStrength, FeCompressiveStrength, FeShearingStrength);

        a.ConnectionsFromCurrentNode      = new List<PhysicalNode>{b, center, c};
        b.ConnectionsFromCurrentNode      = new List<PhysicalNode>{a, center, d};
        c.ConnectionsFromCurrentNode      = new List<PhysicalNode>{a, center, d};
        d.ConnectionsFromCurrentNode      = new List<PhysicalNode>{b, center, c};
        center.ConnectionsFromCurrentNode = new List<PhysicalNode>{a,b,c,d};
        PhysicalStructure structure = new PhysicalStructure();
		structure.AddNodeToStructure(a);
		structure.AddNodeToStructure(b);
		structure.AddNodeToStructure(c);
		structure.AddNodeToStructure(d);
		structure.AddNodeToStructure(center);
        nodeConxions = structure.NodeList;
		structure.Print();
	}
    public List<PhysicalNode> GetListOfNodesInStructure()
    { return nodeConxions; }
    public List<Tuple<double, double>> GetListOf_TupleDistances(int index)
    { return nodeConxions[index]/*PhysNode*/.ConexionDistancesFromCurrentNode/*ListOfTuples*/; }
}