using FindBorder.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBorder
{
    class Program
    {
        static void Main(string[] args)
        {         
            List<Point> list = new List<Point>();

            /*min max  в разных точках*/
            list.Add(new Point(2, 10));
            list.Add(new Point(4, 1));
            list.Add(new Point(5, 6));
            list.Add(new Point(6, 15));           
            list.Add(new Point(10, 3));
            list.Add(new Point(9, 5));
            list.Add(new Point(12, 11));

            /* min и max в обной точке*/
            //list.Add(new Point(1, 15));
            //list.Add(new Point(2, 2));
            //list.Add(new Point(10, 1));
            //list.Add(new Point(7, 9));
            //list.Add(new Point(6, 14));
            //list.Add(new Point(4, 10));

            foreach (var item in list)
            {
                Console.WriteLine("{0} - {1}", item.x, item.y);
            }
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Граница: ");
            List<Point> newListBorder = new List<Point>();
            newListBorder = Border(list);          
            foreach (var item in newListBorder)
            {
                Console.WriteLine("{0} - {1}", item.x, item.y);
            }                     
        }

        static int GetMaxX(List<Point> list, int count)
        {
            int maxX = list[0].x;
            for (int j = 1; j < count; j++)
            {
                if (list[j].x > maxX)
                    maxX = list[j].x;
            }
            return maxX;
        }
        static int GetMaxY(List<Point> list, int count)
        {
            int maxY = list[0].y;
            for (int j = 1; j < count; j++)
            {
                if (list[j].y > maxY)
                    maxY = list[j].y;
            }
            return maxY;
        }
        static int GetMinX(List<Point> list, int count)
        {
            int minX = list[0].x;
            for (int j = 1; j < count; j++)
            {
                if (list[j].x < minX)
                    minX = list[j].x;
            }
            return minX;
        }
        static int GetMinY(List<Point> list, int count)
        {
            int minY = list[0].y;
            for (int j = 1; j < count; j++)
            {
                if (list[j].y < minY)
                    minY = list[j].y;
            }
            return minY;
        }

        static List<Point> Border(List<Point> list)
        {
            List<Point> border = new List<Point>();
            List<Point> newListPoint = new List<Point>();
            int maxX, minX, maxY, minY;
            if (list.Count() <= 4)
            {
                return list;
            }
            else
            {
                maxX = GetMaxX(list, list.Count());
                maxY = GetMaxY(list, list.Count());
                minX = GetMinX(list, list.Count());
                minY = GetMinY(list, list.Count());

                foreach (Point item in list)
                {
                    if ((item.x == maxX && item.y != minY && item.y != maxY) ||
                        (item.x == maxX && item.y == minY) ||
                        (item.x == maxX && item.y == maxY) ||
                        (item.x == minX && item.y == minY) ||
                        (item.x == minX && item.y == maxY) ||
                        (item.x == minX && item.y != minY && item.y != maxY) ||
                        (item.y == minY && item.x != minX && item.x != maxX) ||
                        (item.y == maxY && item.x != minX && item.x != maxX))
                    {
                        border.Add(item);   //4 вершины границы                    
                    }
                    else
                    {
                        newListPoint.Add(item);// оставшиеся точки
                    }
                }

                //Лист точек пересечения     
                List<Point> ListC = new List<Point>();
                foreach (Point point in newListPoint)
                {
                    ListC.Clear();
                    for (int i = 0; i < border.Count - 1; i++)
                    {
                        for (int j = i + 1; j < border.Count; j++)
                        {
                            Point C = new Point();
                            double k;//коэффициент сотношения
                            C.x = point.x;
                            k = (double)(border[i].x - C.x) / (C.x - border[j].x);
                            if (k >= 0)//если есть пересечение
                            {
                                double y = (border[i].y + k * border[j].y) / (1 + k);
                                C.y = (int)y;
                                ListC.Add(C);
                            }
                        }
                    }
// нахождение min и max значения Y  у точек пересения и определение относительно них point
                    int indexMin, indexMax;
                    int min = ListC[0].y;
                    int max = ListC[0].y;
                    if (ListC.Count() > 0)
                    {
                        for (int k = 0; k < ListC.Count(); k++)
                        {
                            if (ListC[k].y < min)
                            {
                                min = ListC[k].y;
                                indexMin = k;
                            }
                        }
                        for (int k = 0; k < ListC.Count(); k++)
                        {
                            if (ListC[k].y > max)
                            {
                                max = ListC[k].y;
                                indexMax = k;
                            }
                        }
                        if (point.y < min || point.y > max)
                        {
                            border.Add(point);
                        }
                    }
                }
                return border;
            }
        }
        ////4 точки границы
        //static Point FindCoordinatesPointOnBorder(Point A, Point B, Point D)
        //{
        //    Point C = new Point();
        //    double k;//коэффициент сотношения
        //    C.x = D.x;
        //    k = (double)(A.x - C.x) / (C.x - B.x);

        //    Console.WriteLine("{0}", k);
        //    double y = (A.y + k * B.y) / (1 + k);

        //    Console.WriteLine(y);
        //    return C;
        //}
    }
}
