﻿// SQL Notebook
// Copyright (C) 2016 Brian Luft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Drawing;
using System.Windows.Forms;

namespace SqlNotebook {
    public sealed class MenuRenderer : ToolStripSystemRenderer {
        private Font _font = new Font("Segoe UI", 9);
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            e.TextColor = e.Item.Enabled ? Color.Black : Color.FromArgb(109, 109, 109);
            base.OnRenderItemText(e);
        }

        private static Pen _menuBorderPen = new Pen(Color.FromArgb(204, 204, 204));
        private static Brush _separatorBrush = new SolidBrush(Color.FromArgb(215, 215, 215));
        private static Brush _disabledSelectedBrush = new SolidBrush(Color.FromArgb(230, 230, 230));
        private static Brush _selectedBrush = new SolidBrush(Color.FromArgb(145, 201, 247));
        private static Brush _topMenuBrush = new SolidBrush(Color.FromArgb(250, 250, 250));
        private static Pen _topMenuPen = new Pen(Color.FromArgb(250, 250, 250));
        private static Brush _menuHoverBgBrush = new SolidBrush(Color.FromArgb(229, 243, 255));
        private static Pen _menuHoverBorderPen = new Pen(Color.FromArgb(204, 232, 255));
        private static Brush _menuOpenBgBrush = new SolidBrush(Color.FromArgb(204, 232, 255));
        private static Pen _menuOpenBorderPen = new Pen(Color.FromArgb(153, 209, 255));
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            var rect = e.Item.ContentRectangle;

            var isTopLevelMenu = e.Item.GetCurrentParent() is MenuStrip;
            if (isTopLevelMenu) {
                rect.X += 2;
                rect.Width -= 5;
                rect.Y--;
                rect.Height++;
                var menuItem = (ToolStripMenuItem)e.Item;
                Pen borderPen;
                Brush fillBrush;
                if (!menuItem.Enabled) {
                    borderPen = _topMenuPen;
                    fillBrush = _topMenuBrush;
                } else if (menuItem.DropDown?.Visible ?? false) {
                    borderPen = _menuOpenBorderPen;
                    fillBrush = _menuOpenBgBrush;
                } else if (e.Item.Selected) {
                    borderPen = _menuHoverBorderPen;
                    fillBrush = _menuHoverBgBrush;
                } else {
                    borderPen = _topMenuPen;
                    fillBrush = _topMenuBrush;
                }
                e.Graphics.FillRectangle(fillBrush, rect);
                e.Graphics.DrawRectangle(borderPen, rect);
                return;
            } else {
                rect.X++;
                rect.Width--;
                Brush brush;
                if (e.Item.Selected) {
                    brush = e.Item.Enabled ? _selectedBrush : _disabledSelectedBrush;
                } else {
                    brush = SystemBrushes.Control;
                }
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            var rect = e.Item.ContentRectangle;
            rect.Height = 1;
            rect.Y++;
            rect.X += 30;
            rect.Width -= 30;
            e.Graphics.FillRectangle(_separatorBrush, rect);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            var dd = e.ToolStrip as ToolStripDropDown;
            if (dd != null) {
                var rect = new Rectangle(0, 0, dd.Size.Width - 1, dd.Size.Height - 1);
                e.Graphics.DrawRectangle(_menuBorderPen, rect);
            } else {
                //base.OnRenderToolStripBorder(e);
            }
        }
    }
}
