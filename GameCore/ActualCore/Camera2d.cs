﻿using GameCore.ActualCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameCore
{
    public class Camera2d : SomethingThatHandleUpdates
    {
        protected float _zoom;
        public Matrix _transform;
        public Vector2 _pos;
        public Vector2 OriginalPosition;
        protected float _rotation;

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom <= 0f)
                    throw new Exception(
                        "Cameras Zoom must be greater than zero! Negative zoom will flip image");
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { OriginalPosition = _pos = value; }
        }

        public Camera2d()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        const float VirtualWidth = 1366;
        const float VirtualHeight = 768;

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            var widthDiff = graphicsDevice.Viewport.Width / VirtualWidth;
            var HeightDiff = graphicsDevice.Viewport.Height / VirtualHeight;

            _transform =
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(_rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom * widthDiff, Zoom * HeightDiff, 1)) *
                                         Matrix.CreateTranslation(new Vector3(
                                             graphicsDevice.Viewport.Width * 0.5f,
                                             graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }

        public Vector2 ToWorldLocation(Vector2 position)
        {
            return Vector2.Transform(position, Matrix.Invert(_transform));
        }

        public Vector2 ToLocalLocation(Vector2 position)
        {
            return Vector2.Transform(position, _transform);
        }

        public void Update()
        {
            if (shakeDuration > 0)
            {
                _pos.Y = OriginalPosition.Y - shakeDuration * 40;

                shakeDuration--;
            }
            else
            {
                shakeDuration = 0;
                _pos.Y = OriginalPosition.Y;
            }
        }

        private int shakeDuration;
        public void Shake()
        {
            shakeDuration = 5;
            AndroidStuff.Vibrate(50);
        }
    }
}