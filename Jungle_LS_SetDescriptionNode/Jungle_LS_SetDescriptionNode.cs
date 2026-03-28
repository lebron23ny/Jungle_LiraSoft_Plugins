
using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using FEModel.Node;
using Jungle_LS_SetDescriptionNode.Windows;
using LiraAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace Jungle_LS_SetDescriptionNode
{
    public class Jungle_LS_SetDescriptionNode : ILiraPrimeAPI
    {
        Action<string> _getDescription;
        string description;
        IModel imodel;
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
                System.Windows.Forms.MessageBox.Show("Не выбраны узлы");
                return ReturnCodes.RC_IGNOR;
            }

            _getDescription = getProperty;
            MainWindow mainWindow = new MainWindow(_getDescription);
            mainWindow.ShowDialog();


            foreach (var item in listNode)
            {
                var node = imodel.getNode(item) as CNode;
                if (node != null)
                {
                    var value = node.GetType().GetProperty("description").GetValue(node);

                    PropertyInfo prop = node.GetType().GetProperty("description");
                    prop.SetValue(node, description);
                }
            }


            return ReturnCodes.RC_OK;
        }


        void getProperty(string res)
        {
            description = res;
        }
    }
}
