using System;

namespace _8CreateCustomClassAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomClassAttribute : Attribute
    {
        public string Author { get; set; }
        public int Revision { get; set; }
        public string Description { get; set; }
        public string[] Reviewers { get; set; }

        public CustomClassAttribute(string author, 
            int revision, 
            string description, 
            params string[] reviewers)
        {
            this.Author = author;
            this.Revision = revision;
            this.Description = description;
            this.Reviewers = reviewers;
        }
    }
}
