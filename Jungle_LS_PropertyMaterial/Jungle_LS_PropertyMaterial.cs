using FEMaterial;
using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using LiraAPI;
using System.Windows.Forms;

namespace Jungle_LS_PropertyMaterial
{
    public class Jungle_LS_PropertyMaterial : ILiraPrimeAPI
    {
        public ReturnCodes ExecuteProgram_Prime(IModelAPI pModelAPI, Results_Key pCurentCase)
        {
            IModel imodel = pModelAPI as IModel;
            if (imodel == null) // модель не определена
                return ReturnCodes.RC_IGNOR;

            int numberMaterial = -1;
            int numbEl = imodel.getAllElementNumber();
            for (int i = 0; i < numbEl; i++)
            {
                CBar cBar = imodel.getElement(i) as CBar;
                if (cBar != null)
                {
                    if (cBar.getFlag(IElement.e_ElFlag.EEF_Selected))
                    {
                        numberMaterial = cBar.getMaterial();
                        break;
                    }
                }
            }
            if (numberMaterial == -1)
            {
                MessageBox.Show("Выберите стрежень с назначенным материалом");


                return ReturnCodes.RC_IGNOR;
            }

            IMaterialContainer matContainer = imodel.getMaterialContainer();
            IMaterial material = matContainer.getMaterial(numberMaterial);
            if (material is MaterialDBSteelRoll stMaterial)
            {
                string tableType = stMaterial.getTableType().ToString();
                string tableName = stMaterial.getTableName().ToString();
                string materialType = stMaterial.getMaterialClass();
                MessageBox.Show($"Номер материала: {numberMaterial}\n{tableType}\n{tableName}\n{materialType}");
                Clipboard.SetText($"Номер материала: {numberMaterial}\n{tableType}\n{tableName}\n{materialType}");
                return ReturnCodes.RC_OK;
            }
            else
            {
                MessageBox.Show("У выбранного стержня не стальной материал");
                return ReturnCodes.RC_IGNOR;
            }
        }
    }
}
