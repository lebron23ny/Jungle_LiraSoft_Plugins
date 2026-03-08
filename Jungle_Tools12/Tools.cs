using FEModel;
using FEModel.Interfaces;
using System;

namespace Jungle_Tools12
{
    public static class Tools
    {
        public static void SetMeshType(ArchitectureElementMeshSetting aMeshSetting, string MeshType)
        {
            //int meshTypeNumber = (int)Enum.Parse(typeof(e_ArchitectureElementMeshType), MeshType);
            //e_ArchitectureElementMeshType meshType = (e_ArchitectureElementMeshType)Enum.GetValues(
            //            typeof(e_ArchitectureElementMeshType)).GetValue(meshTypeNumber);

            e_ArchitectureElementMeshType meshType;
            if (MeshType == "None")
                meshType = e_ArchitectureElementMeshType.AEMT_NONE;
            else if (MeshType == "Delaunay")
                meshType = e_ArchitectureElementMeshType.AEMT_DELANAU;
            else if (MeshType == "ReGrid")
                meshType = e_ArchitectureElementMeshType.AEMT_ReGrid1;
            else if (MeshType == "ReGrid2")
                meshType = e_ArchitectureElementMeshType.AEMT_ReGrid2;
            else if (MeshType == "ReGridQuad")
                meshType = e_ArchitectureElementMeshType.AEMT_ReGridQuad;           
            else
                meshType = e_ArchitectureElementMeshType.AEMT_NONE;

                aMeshSetting.setType(meshType);
        }
    }
}
