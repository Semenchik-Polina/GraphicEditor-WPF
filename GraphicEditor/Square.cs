﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor
{
    public class Square:Figure
    {
        public Square():base()
        { }

        public Square(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Square";
            typeNameRu = "Квадрат";
        }

        protected Size size;

        protected double Height
        {
            get {
                if (startPoint.Y > endPoint.Y)
                {
                    double temp;
                    temp = endPoint.Y;
                    endPoint.Y = startPoint.Y;
                    startPoint.Y = temp;
                }
                return endPoint.Y - startPoint.Y;
            }
        }

        public override void Draw()
        {
            size = new Size(Height, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(startPoint,size);

            path.Data = rectangleGeometry;
        }
    }
}
