using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace northxproductions.Models
{
    public class NxVideoProjectModel
    {
        public List<NxVideo> Project { get; set; }
        public string ProjectName { get; set; }

        public NxVideoProjectModel(NxVideo video)
        {
            Project = new List<NxVideo>();
            Project.Add(video);
            ProjectName = video.ProjectName; 
        }

        public Boolean CheckProject(NxVideo video)
        {
            if(Project != null)
            {
                if (ProjectName.Equals(video.ProjectName))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddVideo(NxVideo video)
        {
            Project.Add(video);
        }
    }
}