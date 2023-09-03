using System.Xml.Linq;

namespace UseCase1.Models
{
    public class Country
    {
        public Name Name { get; set; }
        public List<string> Capital { get; set; }

        internal OutputModel ConvertToOutput()
        {
            return new OutputModel
            {
                Capital = Capital?[0],
                Name = Name?.Official
            };
        }
    }
}
