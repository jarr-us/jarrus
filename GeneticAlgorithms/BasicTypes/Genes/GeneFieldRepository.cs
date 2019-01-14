using Jarrus.GA.Models;
using Jarrus.GA.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jarrus.GA.BasicTypes.Genes
{
    public class GeneFieldRepository
    {
        public static GeneFieldRepository Instance = new GeneFieldRepository();

        private Dictionary<Type, List<GeneField>> _dict = new Dictionary<Type, List<GeneField>>();
        private GeneFieldRepository() {}

        public List<GeneField> GetFieldsFor(Gene obj)
        {
            GenerateFields(obj);
            return _dict[obj.GetType()];
        }

        private void GenerateFields(Gene obj)
        {
            var type = obj.GetType();
            if (_dict.ContainsKey(type)) { return; }

            var geneFields = new List<GeneField>();
            var attributes = new List<object>();

            foreach (var field in obj.GetMyFields())
            {
                var attributeObjects = field.GetCustomAttributes(typeof(GeneOptionAttribute), false);

                if (attributeObjects.Length > 0)
                {
                    var geneAttributes = attributeObjects.Cast<GeneOptionAttribute>().ToArray();
                    var attribute = geneAttributes[0];

                    var geneField = new GeneField();
                    geneField.Attribute = attribute;
                    geneField.Field = field;
                    geneFields.Add(geneField);
                }
            }

            _dict[type] = geneFields;
        }
    }
}
