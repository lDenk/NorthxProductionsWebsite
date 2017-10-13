using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace northxproductions.Models
{
    public class BlogPostModel
    {
        public int Id { get; set; }
        public Boolean Important { get; set; }

        public DateTime PostDate { get; set; }
        public String ImageUrl { get; set; }
        public String Content { get; set; }
    }

    public class BlogPostManager
    {
        private static string BlogPostFile = HttpContext.Current.Server.MapPath("~/App_Data/NxVideos.json");
        private List<BlogPostModel> BlogPosts { get; set; }

        public List<BlogPostModel> Read()
        {
            if (!File.Exists(BlogPostFile))
            {
                /*Create file if it doesn't exist*/
                File.Create(BlogPostFile).Close();
                File.WriteAllText(BlogPostFile, "[]");
            }
            BlogPosts = JsonConvert.DeserializeObject<List<BlogPostModel>>(File.ReadAllText(BlogPostFile));
            return BlogPosts;
        }

        public void AddPost(BlogPostModel blogPost)
        {
            //BlogPosts = new List<BlogPostModel>();
            Read();
            //BlogPostModel blogPost = JsonConvert.DeserializeObject<BlogPostModel>(postJson);

            if(BlogPosts.Count > 0)
            {
                BlogPosts = (from post in BlogPosts
                             orderby post.PostDate
                             select post).ToList();
                blogPost.Id = BlogPosts.Last().Id + 1;
            }
            else
            {
                blogPost.Id = 0;
            }

            BlogPosts.Add(blogPost);
            Save();
        }

        public void DeletePost(int id)
        {
            Read();
            BlogPosts.Remove(BlogPosts.Find(x => x.Id == id));
            Save();
        }

        private void Save()
        {
            File.WriteAllText(BlogPostFile, JsonConvert.SerializeObject(BlogPosts));
        }
    }
}