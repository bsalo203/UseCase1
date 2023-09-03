using System.Xml.Linq;

namespace UseCase1.Models
{
    public class Country
    {
        public Name? Name { get; set; }
        public List<string>? Capital { get; set; }
        public int? Population { get; set; }

        internal OutputModel ConvertToOutput()
        {
            return new OutputModel
            {
                Capital = Capital?[0] ?? null,
                Name = Name?.Official,
                Population = Population                
            };
        }
    }
}
