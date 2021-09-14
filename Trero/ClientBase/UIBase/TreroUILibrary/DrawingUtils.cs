namespace Trero.ClientBase.UIBase.TreroUILibrary
{
    internal class DrawingUtils
    {
        public static Vector2 screenCenter
        {
            get
            {
                var temp = Base.Vec2();

                temp.x = Overlay.handle.Size.Width / 2;
                temp.y = Overlay.handle.Size.Height / 2;

                return temp;
            }
        }

        public static Vector2 logoVCenter
        {
            get
            {
                var temp = Base.Vec2();

                temp.x = Overlay.handle.Size.Width / 2;
                temp.y = Overlay.handle.Size.Height / 4;

                return temp;
            }
        }

        public static Vector2 logoCenter
        {
            get
            {
                var temp = Base.Vec2();

                temp.x = Overlay.handle.Size.Width / 2;
                temp.y = Overlay.handle.Size.Height / 5;

                return temp;
            }
        }

        public static int screenSize
        {
            get
            {
                if (Overlay.handle == null)
                    return 0;

                var size = 1;
                var sizeIncr = 200; // ill modify 300 later

                for (var i = 1; i < 0xFF; ++i)
                    if (Overlay.handle.Size.Width > i * sizeIncr && Overlay.handle.Size.Height > i * sizeIncr)
                        size++;
                    else break;

                return size;
            }
        }
    }
}