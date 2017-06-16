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

    public SimpleAnimation Create_touch_inputs_Down()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 43, 28, new Rectangle(0, 0, 43, 28)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_DownPressed()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 42, 25, new Rectangle(0, 28, 42, 25)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Left()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 26, 50, new Rectangle(0, 53, 26, 50)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_LeftPressed()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 26, 46, new Rectangle(26, 53, 26, 46)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Right()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 29, 50, new Rectangle(0, 103, 29, 50)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_RightPressed()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 28, 47, new Rectangle(29, 103, 28, 47)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_Up()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 47, 32, new Rectangle(0, 153, 47, 32)))
        );

        return animation;
    }

    public SimpleAnimation Create_touch_inputs_UpPressed()
    {
        var animation = new SimpleAnimation(

            new AnimationFrame(10, new GameCore.Texture("touch_inputs", 0, 0, 45, 29, new Rectangle(0, 185, 45, 29)))
        );

        return animation;
    }

}
