using GameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class GeneratedContent
{
    public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

    public void Load(ContentManager content)
    {
        var names = new string[] { "block", "Head", "papers", "touch_inputs", "Walk0001", "Walk0002", "Walk0003" };

        foreach (var name in names)
        {
            Textures.Add(name, content.Load<Texture2D>(name));
        }
    }
    
    public SimpleAnimation Create_touch_inputs_button(int X, int Y, float Z, int Width, int Height)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 0, 68, 68)){ ZIndex = Z })
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_button_pressed(int X, int Y, float Z, int Width, int Height)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 68, 64, 62)){ ZIndex = Z })
        );

        return animation;
    }

}
