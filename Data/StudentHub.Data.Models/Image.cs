namespace StudentHub.Data.Models
{
    using System;

    using StudentHub.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Student Student { get; set; }
    }
}
