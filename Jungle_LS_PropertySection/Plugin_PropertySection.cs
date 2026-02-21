using FEModel;
using FEModel.Element;
using FEModel.Interfaces;
using FESection;
using LiraAPI;
using System.Windows.Forms;

namespace Jungle_LS_PropertySection
{
    public class Plugin_PropertySection : ILiraPrimeAPI
    {

        public ReturnCodes ExecuteProgram_Prime(IModelAPI pModelAPI, Results_Key pCurentCase)
        {
            IModel imodel = pModelAPI as IModel;
            if (imodel == null) // модель не определена
                return ReturnCodes.RC_IGNOR;


            int numberSection = -1;
            int numbEl = imodel.getAllElementNumber();
            for (int i = 0; i < numbEl; i++)
            {
                CBar cBar = imodel.getElement(i) as CBar;
                if (cBar != null)
                {
                    if (cBar.getFlag(IElement.e_ElFlag.EEF_Selected))
                    {
                        numberSection = cBar.getSection();
                        break;
                    }
                }
            }
            if (numberSection == -1)
            {
                MessageBox.Show("Выберите стрежень с назначенным сечением");
                return ReturnCodes.RC_IGNOR;
            }

            ISectionContainer sectionContainer = imodel.getSectionContainer();
            ISection section = sectionContainer.getSection(numberSection);
            if (section is StBaseSection stSection)
            {
                string sectionName = stSection.get_ShapeName();
                string sectType = stSection.getSectionType().ToString();
                string tableTyle = stSection.getTableType().ToString();
                string tableFileName = stSection.get_TableFileName();

                MessageBox.Show($"Номер сечения: {numberSection}\n{sectionName}\n{sectType}\n{tableTyle}\n{tableFileName}");
                Clipboard.SetText($"{sectionName}\n{sectType}\n{tableTyle}\n{tableFileName}");
                return ReturnCodes.RC_OK;

            }
            else
            {
                MessageBox.Show("У выбранного стержня не стальное сечение");
                return ReturnCodes.RC_IGNOR;
            }            
        }
    }
}
