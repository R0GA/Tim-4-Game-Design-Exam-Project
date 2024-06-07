public static class Utility
{
        public static int Remap(int value, int lowA, int highA, int lowB, int highB)
        {
                //low2 + (value - low1) *
                //(high2 - low2) /
                //(high1 - low1)
                var a = lowB + (value - lowA);
                var bRange = highB - lowB;
                var aRange = highA - lowA;
                return a * bRange / aRange;
        }
}