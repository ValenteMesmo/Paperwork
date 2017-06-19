using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Collections.Generic;

namespace ConvertJsonToCSharp
{
    public class Program
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            var SharedContentDirectoryPath = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\SharedContent"));

            var imageFiles = new DirectoryInfo(SharedContentDirectoryPath)
                .GetFiles("*.*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Extension == ".png"
                    || f.Extension == ".bmp"
                );

            var jsonFiles = new DirectoryInfo(SharedContentDirectoryPath)
                .GetFiles("*.*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Extension == ".json"
                );

            CreateCSharpFile(SharedContentDirectoryPath, imageFiles, jsonFiles);
        }

        private static void CreateCSharpFile(
            string path,
            IEnumerable<FileInfo> imageFiles,
            IEnumerable<FileInfo> jsonFiles)
        {
            var fileNamesArray = "new string[] {";
            foreach (var image in imageFiles)
            {
                fileNamesArray += $@" ""{Path.GetFileNameWithoutExtension(image.Name)}"",";
            }
            fileNamesArray = fileNamesArray.Remove(fileNamesArray.Length - 1);
            fileNamesArray += " }";


            var methods = "";
            foreach (var file in jsonFiles)
            {
                methods += GetMethodsAsString(file);
            }

            var fileContent = $@"using GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class GeneratedContent
{{
    public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

    public void Load(ContentManager content)
    {{
        var names = {fileNamesArray};

        foreach (var name in names)
        {{
            Textures.Add(name, content.Load<Texture2D>(name));
        }}
    }}
    {methods}
}}
";

            File.WriteAllText(Path.Combine(path, "GeneratedContent.cs"), fileContent);
        }

        private static string GetMethodsAsString(FileInfo file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.Name);
            var groupedContent = JsonConvert.DeserializeObject<AnimationFramesFile>(File.ReadAllText(file.FullName))
                .frames
                .GroupBy(f => f.filename.Remove(f.filename.Length - 4));

            var methods = "";
            foreach (var group in groupedContent)
            {
                var rectangles = "";
                foreach (var item in group)
                {
                    rectangles += $@"
            new AnimationFrame(10, new GameCore.Texture(""{fileName}"", X, Y, Width, Height, new Rectangle({item.frame.x}, {item.frame.y}, {item.frame.w}, {item.frame.h})){{ ZIndex = Z, Flipped = Flipped }}),";
                }
                rectangles = rectangles.Remove(rectangles.Length - 1);
                methods +=
$@"
    public static SimpleAnimation Create_{fileName}_{group.Key.Replace(' ', '_')}(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {{
        var animation = new SimpleAnimation(
            {rectangles}
        );

        return animation;
    }}
";
            }

            return methods;
        }

    }
}
