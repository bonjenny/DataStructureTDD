using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public static class MyListExtension
    {
        #region [확장 메서드 : Contains]
        public static bool Contains<T>(this MyList<T> list, Predicate<T> match)
        {
			if (list.FindIndex(match) != -1) return true;
            return false;
        }
        #endregion

        #region [확장 메서드 : Find, FindIndex]
        public static T Find<T>(this MyList<T> list, Predicate<T> match)
        {
            int index = list.FindIndex(0, list.Count, match);
            // 구해진 인덱스로 배열에 저장되어 있는 값을 리턴한다
			if (index != -1) return list[index];

            return default(T);
        }

        public static int FindIndex<T>(this MyList<T> list, int startIndex, int count, Predicate<T> match)
        {
            int endIndex = startIndex + count - 1;

            if (startIndex < 0 || startIndex >= list.Count) throw new ArgumentOutOfRangeException();
            if (count < 0 || endIndex >= list.Count) throw new ArgumentOutOfRangeException();

            for (int index = startIndex; index <= endIndex; index++) {
                if (match(list[index])) return index;
            }

            return -1;
        }

        public static int FindIndex<T>(this MyList<T> list, int startIndex, Predicate<T> match)
        {
            return list.FindIndex(startIndex, list.Count - startIndex, match);
        }

        public static int FindIndex<T>(this MyList<T> list, Predicate<T> match)
        {
            return list.FindIndex(0, list.Count, match);
        }
        #endregion

        #region [확장 메서드 : FindLast, FindLastIndex]
        public static T FindLast<T>(this MyList<T> list, Predicate<T> match)
        {
            int index = list.FindLastIndex(list.Count - 1, list.Count, match);
            // 구해진 인덱스로 배열에 저장되어 있는 값을 리턴한다
            if (index != -1) return list[index];

            return default(T);
        }

        public static int FindLastIndex<T>(this MyList<T> list, int startIndex, int count, Predicate<T> match)
        {
            int endIndex = startIndex - count + 1;

            if (startIndex < 0 || startIndex >= list.Count) throw new ArgumentOutOfRangeException();
            if (count < 0 || endIndex < 0) throw new ArgumentOutOfRangeException();

            for (int index = startIndex; index >= endIndex; index--) {
                if (match(list[index])) return index;
            }

            return -1;
        }

        public static int FindLastIndex<T>(this MyList<T> list, int startIndex, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, startIndex + 1, match);
        }

        public static int FindLastIndex<T>(this MyList<T> list, Predicate<T> match)
        {
            return list.FindLastIndex(list.Count - 1, list.Count, match);
        }
        #endregion

        #region [확장 메서드 : Remove, RemoveAll]
        public static bool Remove<T>(this MyList<T> list, Predicate<T> match)
        {
            int index = FindIndex(list, match);
			if (index != -1) {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }

        public static int RemoveAll<T>(this MyList<T> list, Predicate<T> match)
        {
            int removedCount = 0;
            int endIndex = list.Count - 1;

            for (int index = endIndex; index >= 0; index--) {
                if (match(list[index])) {
                    list.RemoveAt(index);
                    removedCount++;
                }
            }
            return removedCount;
        }
        #endregion
    }
}
