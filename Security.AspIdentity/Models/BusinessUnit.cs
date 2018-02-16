using System.Collections.Generic;

namespace Security.AspIdentity.Models
{
    public class BusinessUnit
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public BusinessUnit ParentBusinessUnit { get; set; }

        public bool IsRoot { get; set; }

    }
}
