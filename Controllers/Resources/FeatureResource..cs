using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hereglish.Controllers.Resources
{
    public class FeatureResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public ICollection<KeyValuePairResource> Subfeatures { get; set; }

        public FeatureResource()
        {
          Subfeatures =  new Collection<KeyValuePairResource>();

        }
    }
}