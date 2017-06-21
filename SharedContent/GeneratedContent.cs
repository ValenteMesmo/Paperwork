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
        var names = new string[] { "background", "recycle_mantis", "touch_inputs", "trash_bag", "trash_explosion" };

        foreach (var name in names)
        {
            Textures.Add(name, content.Load<Texture2D>(name));
        }
    }
    
    public static SimpleAnimation Create_background_wall_bot(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("background", X, Y, Width, Height, new Rectangle(0, 0, 69, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_background_wall_center(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("background", X, Y, Width, Height, new Rectangle(69, 0, 69, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_background_wall_left(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("background", X, Y, Width, Height, new Rectangle(138, 0, 69, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_background_wall_right(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("background", X, Y, Width, Height, new Rectangle(0, 68, 69, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_background_wall_top(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("background", X, Y, Width, Height, new Rectangle(69, 68, 69, 68)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Death(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(252, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(378, 0, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(126, 101, 126, 101)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HandsDown(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(252, 101, 39, 32)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HandsGoingDown(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(291, 101, 81, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(372, 101, 81, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 202, 81, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(81, 202, 81, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(162, 202, 81, 73)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HandsGoingUp(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(243, 202, 78, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(321, 202, 78, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(399, 202, 78, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 275, 78, 73)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(78, 275, 78, 73)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HandsUp(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(156, 275, 39, 33)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Head(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(195, 275, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(249, 275, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(303, 275, 54, 39)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_HeadUp(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(357, 275, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(411, 275, 54, 39)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(0, 348, 54, 39)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Stand(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(196, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(54, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(125, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_recycle_mantis_Walk(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(196, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(267, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("recycle_mantis", X, Y, Width, Height, new Rectangle(338, 348, 71, 72)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_touch_inputs_button(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 0, 68, 69)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(68, 0, 68, 69)){ ZIndex = Z, Flipped = Flipped }),
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(136, 0, 68, 69)){ ZIndex = Z, Flipped = Flipped })
        );

        return animation;
    }

    public static SimpleAnimation Create_touch_inputs_button_pressed(int X, int Y, float Z, int Width, int Height, bool Flipped = false)
    {
        var animation = new SimpleAnimation(
            
            new AnimationFrame(10, new GameCore.Texture("touch_inputs", X, Y, Width, Height, new Rectangle(0, 69, 64, 62)){ ZIndex = Z, Flipped = Flipped })
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
