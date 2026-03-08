using FEModel;
using FEModel.Interfaces;

namespace Jungle_Tools2024
{
    public static class Tools
    {
        public static void SetMeshType(ArchitectureElementMeshSetting aMeshSetting, string MeshType)
        {
            //FEModel.Interfaces
            e_LiraMeshType meshType;
            if (MeshType == "None")
                meshType = e_LiraMeshType.LMT_NONE;
            else if (MeshType == "Delaunay")
                meshType = e_LiraMeshType.LMT_Delanau;
            else if (MeshType == "ReGrid")
                meshType = e_LiraMeshType.LMT_ReGridTriangle1;
            else if (MeshType == "ReGrid2")
                meshType = e_LiraMeshType.LMT_ReGridTriangle2;
            else if (MeshType == "ReGridQuad")
                meshType = e_LiraMeshType.LMT_QuadDominant;
            else
                meshType = e_LiraMeshType.LMT_NONE;
            aMeshSetting.setType(meshType);
        }
    }
}
