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
        var names = new string[] { "block", "Head", "papers", "recycle_mantis", "touch_inputs", "trash_bag", "trash_explosion", "Walk0001", "Walk0002", "Walk0003" };

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
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 303, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(54, 303, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(108, 303, 54, 39)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HeadUp(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(162, 303, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 342, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(54, 342, 54, 39)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Walk(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(108, 342, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(179, 342, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 414, 71, 72)){ ZIndex = Z, Flipped = Flipped })
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

    public static SimpleAnimation Create_trash_bag_Trash(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("trash_bag", X, Y, Width, Height, new Rectangle(0, 0, 72, 65)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_bag", X, Y, Width, Height, new Rectangle(0, 65, 72, 65)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_bag", X, Y, Width, Height, new Rectangle(0, 130, 72, 65)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_trash_explosion_Explosion(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 0, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(159, 0, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(318, 0, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 147, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(159, 147, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(318, 147, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 294, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(159, 294, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(318, 294, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 441, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(159, 441, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(318, 441, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("trash_explosion", X, Y, Width, Height, new Rectangle(0, 588, 159, 147)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

}
