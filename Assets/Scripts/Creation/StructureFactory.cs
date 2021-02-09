using System;
using System.Collections.Generic;
using CoreProject.Resource;
using CoreProject.Singleton;
using Panteon.Data;

namespace Panteon.Creation
{
    public class StructureFactory : SingletonClass<StructureFactory>
    {
        public List<StructureTemplate> GetStructerTemplates()
        {
            List<StructureTemplate> structureTemplates = new List<StructureTemplate>();
            foreach (var item in Enum.GetNames(typeof(StructureType)))
            {
                StructureTemplate structureTemplate = ResourceManager.Instance.GetResource<StructureTemplate>($"{item}Template");
                structureTemplates.Add(structureTemplate);
            }
            return structureTemplates;
        }
    }
}