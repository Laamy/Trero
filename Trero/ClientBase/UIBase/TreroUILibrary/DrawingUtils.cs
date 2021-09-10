using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.UIBase.TreroUILibrary
{
    class DrawingUtils
    {
        public static Vector2 screenCenter
        {
            get
            {
                Vector2 temp = Base.Vec2();

                temp.x = Overlay.handle.Size.Width / 2;
                temp.y = Overlay.handle.Size.Height / 2;

                return temp;
            }
        }
        public static Vector2 LogoVCenter
        {
            get
            {
                Vector2 temp = Base.Vec2();

                temp.x = Overlay.handle.Size.Width / 2;
                temp.y = Overlay.handle.Size.Height / 4;

                return temp;
            }
        }
        public static Vector2 LogoCenter
        {
            get
            {
                Vector2 temp = Base.Vec2();

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

                int size = 1;
                int sizeIncr = 200; // ill modify 300 later

                for (int i = 1; i < 0xFF; ++i)
                {
                    if (Overlay.handle.Size.Width > (i * sizeIncr) && Overlay.handle.Size.Height > (i * sizeIncr))
                        size++;
                    else break;
                }

                return size;
            }
        }
    }
}
