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
        var names = new string[] { "block", "Head", "papers", "recycle_mantis", "touch_inputs", "Walk0001", "Walk0002", "Walk0003" };

        foreach (var name in names)
        {
            Textures.Add(name, content.Load<Texture2D>(name));
        }
    }
    
    public static SimpleAnimation Create_recycle_mantis_Death(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 202, 126, 101)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Head(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 303, 54, 38)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Walk(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(54, 303, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(125, 303, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 375, 71, 72)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_touch_inputs_button(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 0, 68, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_touch_inputs_button_pressed(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 68, 64, 62)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

}
