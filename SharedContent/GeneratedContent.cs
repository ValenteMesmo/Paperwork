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
    
    public SimpleAnimation Create_touch_inputs_Down(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(43 * WidthScale), (int)(28 * HeightScale), new Rectangle(0, 0, 43, 28)) { ZIndex=0})
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_DownPressed(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(42 * WidthScale), (int)(25 * HeightScale), new Rectangle(0, 28, 42, 25)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Left(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(26 * WidthScale), (int)(50 * HeightScale), new Rectangle(0, 53, 26, 50)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_LeftPressed(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(26 * WidthScale), (int)(46 * HeightScale), new Rectangle(26, 53, 26, 46)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Right(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(29 * WidthScale), (int)(50 * HeightScale), new Rectangle(0, 103, 29, 50)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_RightPressed(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(28 * WidthScale), (int)(47 * HeightScale), new Rectangle(29, 103, 28, 47)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Up(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(47 * WidthScale), (int)(32 * HeightScale), new Rectangle(0, 153, 47, 32)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_UpPressed(int X, int Y, float WidthScale, float HeightScale)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, (int)(45 * WidthScale), (int)(29 * HeightScale), new Rectangle(0, 185, 45, 29)))
        );

        return animation;
    }

}
