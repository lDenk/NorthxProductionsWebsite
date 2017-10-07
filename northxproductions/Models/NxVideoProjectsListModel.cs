using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace northxproductions.Models
{
    public class NxVideoProjectsListModel
    {
        public List<NxVideoProjectModel> Projects { get; set; }

        public NxVideoProjectsListModel()
        {

        }

        public void Create(List<NxVideo> videos)
        {
            /*Creates new NxVideoProject List*/
            Projects = new List<NxVideoProjectModel>();
            NxVideoProjectModel newProject;
            Boolean added = false;
            //project.Project[0] = videos[0];
            //project.ProjectName = videos[0].Project;

            /*Goes through all videos*/
            foreach(NxVideo vid in videos)
            {
                if(Projects.Count == 0)
                {
                    newProject = new NxVideoProjectModel(vid);
                    Projects.Add(newProject);
                }
                else
                {
                    /*Goes through all projects created to check if project is in projects list*/
                    foreach(NxVideoProjectModel project in Projects)
                    {
                        if (project.CheckProject(vid))
                        {
                            project.AddVideo(vid);
                            added = true;
                        }
                        }
                    /*Project was not in Projects List*/
                    if (!added)
                    {
                        newProject = new NxVideoProjectModel(vid);
                        Projects.Add(newProject);
                    }
                }
            }
        }
    }
}