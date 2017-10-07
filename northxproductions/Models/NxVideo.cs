using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace northxproductions.Models
{
    public class NxVideo
    {
        public String Url { get; set; }
        public String Name { get; set; }
        public String Embed { get; set; }
        public String ProjectName { get; set; }
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

    }

    /*Manages Json database for videos*/
    public class NxVideoPostManager
    {
        private static string VideosFile = HttpContext.Current.Server.MapPath("~/App_Data/NxVideos.json");
        private static List<NxVideo> videos = new List<NxVideo>();

        public static void Create(string postJson)
        {
            var obj = JsonConvert.DeserializeObject<NxVideo>(postJson);

            if(videos.Count > 0)
            {
                videos = (from video in videos
                          orderby video.CreateTime
                          select video).ToList();
                obj.Id = videos.Last().Id + 1;
            }
            else
            {
                obj.Id = 0;
            }
            videos.Add(obj);
            save();
        }

        public static List<NxVideo> Read()
        {
            if (!File.Exists(VideosFile))
            {
                /*Create file if it doesn't exist*/
                File.Create(VideosFile).Close();
                File.WriteAllText(VideosFile, "[]");
            }
            videos = JsonConvert.DeserializeObject<List<NxVideo>>(File.ReadAllText(VideosFile));
            return videos;
        }

        public static void Delete(int id)
        {
            videos.Remove(videos.Find(x => x.Id == id));
            save();
        }

        private static void save()
        {
            File.WriteAllText(VideosFile, JsonConvert.SerializeObject(videos));
        }
    }
}