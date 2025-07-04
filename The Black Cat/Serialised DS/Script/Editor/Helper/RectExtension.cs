using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public static class RectExtensions
    {
        public static Rect WithX(this Rect rect, float x) => new Rect(x, rect.y, rect.width, rect.height);
        public static Rect WithY(this Rect rect, float y) => new Rect(rect.x, y, rect.width, rect.height);
        public static Rect WithWidth(this Rect rect, float width) => new Rect(rect.x, rect.y, width, rect.height);
        public static Rect WithHeight(this Rect rect, float height) => new Rect(rect.x, rect.y, rect.width, height);

        public static Rect AppendTop(this Rect rect, float gap = 0) => new Rect(rect.x, rect.y - rect.height - gap, rect.width, rect.height);
        public static Rect AppendBottom(this Rect rect, float gap = 0) => new Rect(rect.x, rect.y + rect.height + gap, rect.width, rect.height);
        public static Rect AppendLeft(this Rect rect, float gap = 0) => new Rect(rect.x - rect.width - gap, rect.y, rect.width, rect.height);
        public static Rect AppendRight(this Rect rect, float gap = 0) => new Rect(rect.x + rect.width + gap, rect.y, rect.width, rect.height);

        public static Rect ExpandTop(this Rect rect, float expand) => new Rect(rect.x, rect.y - expand, rect.width, rect.height + expand);
        public static Rect ExpandBottom(this Rect rect, float expand) => new Rect(rect.x, rect.y, rect.width, rect.height + expand);
        public static Rect ExpandLeft(this Rect rect, float expand) => new Rect(rect.x - expand, rect.y, rect.width + expand, rect.height);
        public static Rect ExpandRight(this Rect rect, float expand) => new Rect(rect.x, rect.y, rect.width + expand, rect.height);        

        public static Rect CutTop(this Rect rect, float cut) => new Rect(rect.x, rect.y + cut, rect.width, rect.height - cut);
        public static Rect CutBottom(this Rect rect, float cut) => new Rect(rect.x, rect.y, rect.width, rect.height - cut);
        public static Rect CutLeft(this Rect rect, float cut) => new Rect(rect.x + cut, rect.y, rect.width - cut, rect.height);
        public static Rect CutRight(this Rect rect, float cut) => new Rect(rect.x - cut, rect.y, rect.width - cut, rect.height);

        public static Rect MoveX(this Rect rect, float move) => new Rect(rect.x + move, rect.y, rect.width, rect.height);
        public static Rect MoveY(this Rect rect, float move) => new Rect(rect.x, rect.y + move, rect.width, rect.height);

        public static Rect AddWidth(this Rect rect, float width) => new Rect(rect.x, rect.y, rect.width + width, rect.height);
        public static Rect AddHeight(this Rect rect, float height) => new Rect(rect.x, rect.y, rect.width, rect.height + height);
    }
}