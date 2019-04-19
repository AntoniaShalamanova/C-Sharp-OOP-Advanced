using System;

namespace P02.Graphic_Editor
{
    class StartUp
    {
        static void Main()
        {
            IShape circle = new Circle();
            IShape rectangle = new Rectangle();
            IShape square = new Square();

            GraphicEditor ge = new GraphicEditor();

            Console.WriteLine(ge.DrawShape(circle));
            Console.WriteLine(ge.DrawShape(rectangle));
            Console.WriteLine(ge.DrawShape(square));
        }
    }
}
