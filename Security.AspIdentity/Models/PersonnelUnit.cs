namespace Security.AspIdentity.Models
{
    public class PersonnelUnit
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PersonnelUnit ReportsTo { get; set; } 

        public AppIdentityUser UserData { get; set; }
    }
}
