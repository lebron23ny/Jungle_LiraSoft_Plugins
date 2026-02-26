using FEMaterial;
using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using FEModel.Node;
using FESection;
using Jungle_LS_ColumnarFoundation.Windows;
using LiraAPI;
using System;
using System.Collections.Generic;

namespace Jungle_LS_ColumnarFoundation
{
    public class Jungle_LS_ColumnarFoundation : ILiraPrimeAPI
    {
        private double _width1;
        private double _width2;
        private double _thickPlate;
        private double _height;
        private double _b1;
        private double _b2;
        private string _classConcrete;

        private bool _response = false;

        private double c1;
        private double c2;
        private double cx;
        private double cy;

        IModel imodel;
        private int numberMat;
        private int numberSect;

        Action<double, double, double, double, double, double, string, bool> _calculate;
        Action<double, double, double, double> _elasticBasicParam;
        public ReturnCodes ExecuteProgram_Prime(IModelAPI pModelAPI, Results_Key pCurentCase)
        {
            imodel = pModelAPI as IModel;
            if (imodel == null) // модель не определена
                return ReturnCodes.RC_IGNOR;

            List<int> listNode = new List<int>();
            int numbNode = imodel.getAllNodeNumber();

            for (int i = 0; i < numbNode; i++)
            {
                var node = imodel.getNode(i) as CNode;
                if (node != null)
                {
                    if (node.getFlag(INode.e_NdFlag.ENF_Selected))
                    {
                        listNode.Add(i);
                    }
                }
            }

            if (listNode.Count == 0)
            {
                System.Windows.MessageBox.Show("Не выбраны узлы");
                return ReturnCodes.RC_IGNOR;
            }

            _calculate = setProperty;
            _elasticBasicParam = setElasticBasicParam;
            FoundationWindow foundationWindow = new FoundationWindow(_calculate, _elasticBasicParam);
            foundationWindow.ShowDialog();

            if (!_response)
            {
                return ReturnCodes.RC_IGNOR;
            }
            numberMat = AddMaterial();
            numberSect = InsertSection();   

            foreach (int nodeNumberInsert in listNode)
            {
                InsertFoundation(nodeNumberInsert);
            }
            return ReturnCodes.RC_OK;
        }

        private int InsertSection()
        {
            ISectionContainer sectionContainer = imodel.getSectionContainer();
            int numberSection = sectionContainer.getSectionNumber();

            SectionPlate sectionPlate = new SectionPlate(sectionContainer);
            sectionPlate.setH(_thickPlate);            
            sectionContainer.addSection(sectionPlate);

            SectionBarRect sectionBarRect = new SectionBarRect(sectionContainer);
            sectionBarRect.setB(_b1);
            sectionBarRect.setH(_b2);
            sectionContainer.addSection(sectionBarRect);

            return numberSection;
        }

        private int AddMaterial()
        {
            IMaterialContainer matContainer = imodel.getMaterialContainer();
            int numberMat = matContainer.getMaterialsNumber();

            MaterialDBConcreteSP materialDBConcreteSP = new MaterialDBConcreteSP(matContainer);
            materialDBConcreteSP.setTableType(MaterialDBAdapter.e_st_table_type.CONCRETE_DB_SP);
            materialDBConcreteSP.setTableName("Тяжелый");
            materialDBConcreteSP.setMaterialClass(_classConcrete);
            materialDBConcreteSP.Refresh();

            matContainer.addMaterial(materialDBConcreteSP);
            return numberMat;
        }

        private void InsertFoundation(int nodeNumber)
        {
            INode nodeC = imodel.getNode(nodeNumber);

            double X3 = nodeC.getX();
            double Y3 = nodeC.getY();
            double Z3 = nodeC.getZ();

            CNode node1 = new CNode(imodel, X3, Y3, Z3 - _height);
            imodel.addNode(node1);

            CBar bar1 = new CBar(imodel, (uint)nodeC.getIndex(), (uint)node1.getIndex());
            bar1.setMaterial(numberMat);
            bar1.setSection(numberSect + 1);

            imodel.addElement(bar1);

            ArchitecturePlate architecturePlate = new ArchitecturePlate(imodel);

            ArchitecturePolygon architecturePolygonPlate = new ArchitecturePolygon();

            CNode nPl1 = new CNode(imodel, X3 - _width1 / 2, Y3 - _width2 / 2, Z3 - _height);
            CNode nPl2 = new CNode(imodel, X3 - _width1 / 2, Y3 + _width2 / 2, Z3 - _height);
            CNode nPl3 = new CNode(imodel, X3 + _width1 / 2, Y3 + _width2 / 2, Z3 - _height);
            CNode nPl4 = new CNode(imodel, X3 + _width1 / 2, Y3 - _width2 / 2, Z3 - _height);

            PolygonEdgeLine polygonEdgeLinePl1 = new PolygonEdgeLine(nPl1.m_X, nPl1.m_Y, nPl1.m_Z, architecturePolygonPlate);
            PolygonEdgeLine polygonEdgeLinePl2 = new PolygonEdgeLine(nPl2.m_X, nPl2.m_Y, nPl2.m_Z, architecturePolygonPlate);
            PolygonEdgeLine polygonEdgeLinePl3 = new PolygonEdgeLine(nPl3.m_X, nPl3.m_Y, nPl3.m_Z, architecturePolygonPlate);
            PolygonEdgeLine polygonEdgeLinePl4 = new PolygonEdgeLine(nPl4.m_X, nPl4.m_Y, nPl4.m_Z, architecturePolygonPlate);

            architecturePolygonPlate.addEdge(polygonEdgeLinePl1);
            architecturePolygonPlate.addEdge(polygonEdgeLinePl2);
            architecturePolygonPlate.addEdge(polygonEdgeLinePl3);
            architecturePolygonPlate.addEdge(polygonEdgeLinePl4);

            architecturePlate.addContour(architecturePolygonPlate);

            #region Назначение настроек триангуляции
            ArchitectureElementMeshSetting aMeshSetting = new ArchitectureElementMeshSetting();
            aMeshSetting.setStep(0.5);
            aMeshSetting.setIsARBcreate(true);
            //try
            //{
            //    aMeshSetting.setType(e_ArchitectureElementMeshType.AEMT_ReGridQuad);

            //}
            //catch(Exception ex)
            //{
            //    //System.Windows.MessageBox.Show(ex.Message);
            //}


            architecturePlate.MeshSetting = aMeshSetting;
            #endregion

            #region Назначение коэффициентов постели
            CElasticBasis cElasticBasis = new CElasticBasis();
            cElasticBasis.PaternElement = architecturePlate;

            cElasticBasis.set_TypeElement(e_ElasticBasis_Type.EBT_PLATE);
            cElasticBasis.set_C1(c1*1000);
            cElasticBasis.set_C2(c2*1000);

            cElasticBasis.set_ShearCx(cx * 1000);
            cElasticBasis.set_ShearCy(cy * 1000);

            architecturePlate.setElasticBasis(cElasticBasis);
            #endregion



            architecturePlate.setMaterial(numberMat);
            architecturePlate.setSection(numberSect);

            imodel.addArchitectureElement(architecturePlate);
        }

        private void setProperty(double w1, double w2, double th, double h, double b1, double b2, string classConcrete, bool response)
        {
            _width1 = w1;
            _width2 = w2;
            _thickPlate = th;
            _height = h;
            _b1 = b1;
            _b2 = b2;
            _classConcrete = classConcrete;
            _response = response;
        }

        private void setElasticBasicParam(double c1, double c2, double cx, double cy)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.cx = cx;
            this.cy = cy;
        }
    }
}
