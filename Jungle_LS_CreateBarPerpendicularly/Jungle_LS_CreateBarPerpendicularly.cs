using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using FEModel.Node;
using LiraAPI;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace Jungle_LS_CreateBarPerpendicularly
{
    public class Jungle_LS_CreateBarPerpendicularly : ILiraPrimeAPI
    {
        public ReturnCodes ExecuteProgram_Prime(IModelAPI pModelAPI, Results_Key pCurentCase)
        {
            IModel imodel = pModelAPI as IModel;
            if (imodel == null) // модель не определена
                return ReturnCodes.RC_IGNOR;

            int numberSelectedBar = -1;
            int numbEl = imodel.getAllElementNumber();
            for (int i = 0; i < numbEl; i++)
            {
                CBar cBar = imodel.getElement(i) as CBar;
                if (cBar != null)
                {
                    if (cBar.getFlag(IElement.e_ElFlag.EEF_Selected))
                    {
                        numberSelectedBar = i;
                        break;
                    }
                }
            }




            List<int> listNode = new List<int>();
            int numbNode = imodel.getAllNodeNumber();
            for (int i = 0; i < numbNode; i++)
            {
                var node = imodel.getNode(i) as CNode;
                if(node != null)
                {
                    if(node.getFlag(INode.e_NdFlag.ENF_Selected))
                    {
                        listNode.Add(i);                        
                    }
                }
            }
            if(numberSelectedBar == -1)
            {
                MessageBox.Show("Не выбран стержень");
                return ReturnCodes.RC_IGNOR;
            }
            if(listNode.Count == 0)
            {
                MessageBox.Show("Не выбраны узлы"); return ReturnCodes.RC_IGNOR;
            }
            

            CBar selectedBar = (CBar)imodel.getAllElement(numberSelectedBar);
            var firstN = selectedBar.getNode(0);
            var secondN = selectedBar.getNode(1);

            var nodeA = imodel.getNode((int)firstN);
            var nodeB = imodel.getNode((int)secondN);

            double X1 = nodeA.getX();
            double Y1 = nodeA.getY();
            double Z1 = nodeA.getZ();

            double X2 = nodeB.getX();
            double Y2 = nodeB.getY();
            double Z2 = nodeB.getZ();
            foreach(int node in listNode)
            {
                INode nodeC = imodel.getNode(node);
                double X3 = nodeC.getX();
                double Y3 = nodeC.getY();
                double Z3 = nodeC.getZ();

                Point3D A = new Point3D(X1, Y1, Z1);
                Point3D B = new Point3D(X2, Y2, Z2);
                Point3D V = new Point3D(X3, Y3, Z3);
                Point3D P = Geometry3D.ProjectPointOnLine(A, B, V);

                CNode cNode = new CNode(imodel, P.X, P.Y, P.Z);
                imodel.addNode(cNode);

                CBar cBar1 = new CBar(imodel, (uint)nodeC.getIndex(), (uint)cNode.getIndex());
                imodel.addElement(cBar1);
            }

            return ReturnCodes.RC_OK;
        }
    }

    public struct Point3D
    {
        public double X;
        public double Y;
        public double Z;

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public static class Geometry3D
    {
        public static Point3D ProjectPointOnLine(Point3D A, Point3D B, Point3D V)
        {
            double abX = B.X - A.X;
            double abY = B.Y - A.Y;
            double abZ = B.Z - A.Z;

            double avX = V.X - A.X;
            double avY = V.Y - A.Y;
            double avZ = V.Z - A.Z;

            double dotAVAB = avX * abX + avY * abY + avZ * abZ;
            double dotABAB = abX * abX + abY * abY + abZ * abZ;

            double t = dotAVAB / dotABAB;

            return new Point3D(
                A.X + abX * t,
                A.Y + abY * t,
                A.Z + abZ * t
            );
        }
    }
}
