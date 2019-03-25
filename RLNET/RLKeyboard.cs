#region license
/* 
 * Released under the MIT License (MIT)
 * Copyright (c) 2014 Travis M. Clark
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 */
#endregion

using OpenTK;
using OpenTK.Input;

namespace RLNET
{
    public class RLKeyboard
    {
        private RLKeyPress keyPress;
        private bool numLock;
        private bool capsLock;
        private bool scrollLock;
        private bool alt;
        private bool shift;
        private bool ctrl;
        private bool repeating;

        internal RLKeyboard(GameWindow gameWindow)
        {
            gameWindow.KeyDown += gameWindow_KeyDown;
            repeating = gameWindow.Keyboard.KeyRepeat;
            
        }

        private void gameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {

            KeyboardState keyState = Keyboard.GetState();



            if (e.Key == Key.NumLock) numLock = !numLock;
            else if (e.Key == Key.CapsLock) capsLock = !capsLock;
            else if (e.Key == Key.ScrollLock) scrollLock = !scrollLock;

            //Check modifier keys (OpenTK changes)
            shift = (keyState.IsKeyDown(Key.LShift) || keyState.IsKeyDown(Key.RShift));
            ctrl = (keyState.IsKeyDown(Key.LControl) || keyState.IsKeyDown(Key.RControl));
            alt = (keyState.IsKeyDown(Key.LAlt) || keyState.IsKeyDown(Key.RAlt));

            RLKeyPress newKeyPress = new RLKeyPress((RLKey)e.Key, alt, shift, ctrl, repeating, numLock, capsLock, scrollLock);
            if (keyPress != newKeyPress) keyPress = newKeyPress;
        }


        /// <summary>
        /// Checks to see if a key was pressed.
        /// </summary>
        /// <returns>Key Press, null if nothing was pressed.</returns>
        public RLKeyPress GetKeyPress()
        {
            RLKeyPress kp = keyPress;
            keyPress = null;
            return kp;
        }

    }
}
