using System;

namespace GameCore
{
    public class InputRepository
    {
        public Button Left = new Button();
        public Button Jump  =new Button();
        public Button Right =new Button();
        public Button Crouch=new Button();
        public Button Grab  =new Button();
    }

    public enum ButtomStatus
    {
        None,
        Click,
        Hold,
        Release
    }

    public class Button
    {
        private ButtomStatus status;
        private DateTime pressDelay;
        private DateTime releaseDelay;
        private int delay = 10;

        public void Update(bool value)
        {
            status = getNextStatus(value);
        }

        private ButtomStatus getNextStatus(bool isDownNow)
        {
            if (isDownNow)
            {
                if (status == ButtomStatus.None || status == ButtomStatus.Release)
                {
                    pressDelay = DateTime.Now.AddMilliseconds(delay);
                    return ButtomStatus.Click;
                }

                if (status == ButtomStatus.Click)
                {
                    if (pressDelay < DateTime.Now)
                        return ButtomStatus.Hold;

                    return ButtomStatus.Click;
                }

                if (status == ButtomStatus.Hold)
                    return ButtomStatus.Hold;
            }
            else
            {
                if(status == ButtomStatus.Release)
                {
                    releaseDelay = DateTime.Now.AddMilliseconds(delay);
                    return ButtomStatus.Hold;
                }

                if (status == ButtomStatus.Click || status == ButtomStatus.Hold)
                {
                    if (releaseDelay < DateTime.Now)
                        return ButtomStatus.None;
                    
                    return status;
                }
            }

            return ButtomStatus.None;
        }

        public ButtomStatus GetStatus()
        {
            return status;
        }
    }
}
