using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrutAndAlgorithmPractise
{
    public class ArrayList<T> : IList<T>
    {
        private T[] _lData;
        private int _iCount;
        private int _iMaxCount;
        private readonly object _lockObject = new object();
        public ArrayList(int iMaxCount=0)
        {
            if (iMaxCount == 0)
                iMaxCount = 100;
            _lData = new T[iMaxCount];
            _iMaxCount = iMaxCount;
            _iCount = 0;
        }
        public int Count()
        {
            return _iCount;
        }
        public T FindIndex(int iIndex)
        {
            if (iIndex < 0 || iIndex >= _iCount)
                throw new Exception("索引超出集合范围！");

            return _lData[iIndex];
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="oData">数据实体</param>
        public void Add(T oData)
        {
            if(_iCount >= _iMaxCount)
            {
                lock(_lockObject)
                {
                    if(_iCount>=_iMaxCount)
                    {
                        MigrateNewArray();
                    }
                }
            }
            _lData[_iCount++] = oData;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="iIndex">索引</param>
        public void Delete(int iIndex)
        {
            if (iIndex < 0 || iIndex >= _iCount)
                return;
            int i = iIndex;
            for (; i < _iCount-1; i++)
            {
                _lData[i] = _lData[i + 1];
            }
            _lData[i] = default(T);
            _iCount--;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="oData">数据实体</param>
        /// <param name="iIndex">索引</param>
        public void Insert(T oData, int iIndex)
        {
            if (iIndex < 0 || iIndex >= _iCount)
                return;

            if (_iCount >= _iMaxCount)
            {
                lock (_lockObject)
                {
                    if (_iCount >= _iMaxCount)
                    {
                        MigrateNewArray();
                    }
                }
            }
            int i = _iCount;
            for (; i > iIndex; i--)
            {
                _lData[i] = _lData[i - 1];
            }
            _lData[i] = oData;
        }

        /// <summary>
        /// 将两个已排序的列表合并为1个有序的列表
        /// </summary>
        /// <param name="lData1">有序列表1</param>
        /// <param name="lData2">有序列表2</param>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        public static ArrayList<T> MergeList(ArrayList<T> lData1, ArrayList<T> lData2, Func<T, T, bool> iCompareable)
        {
            ArrayList<T> lData = new ArrayList<T>();
            int iLeftIndex = 0;
            int iRightIndex = 0;
            while (true)
            {
                if (iLeftIndex == lData1.Count() && iRightIndex == lData2.Count())
                    break;
                if (iLeftIndex == lData1.Count())
                {
                    lData.Add(lData2.FindIndex(iRightIndex++));
                    continue;
                }
                if (iRightIndex == lData2.Count())
                {
                    lData.Add(lData1.FindIndex(iLeftIndex++));
                    break;
                }
                if (iCompareable(lData1.FindIndex(iLeftIndex), lData2.FindIndex(iRightIndex)))
                    lData.Add(lData1.FindIndex(iLeftIndex++));
                else
                    lData.Add(lData2.FindIndex(iRightIndex++));
            }
            return lData;
        }
        public void Print(Action<T> fPrintAction)
        {
            for (int i = 0; i < _iCount; i++)
            {
                fPrintAction(_lData[i]);
            }
        }
        private void MigrateNewArray()
        {
            int iMaxCount = _iMaxCount + _iMaxCount / 2;
            T[] lData = new T[iMaxCount];
            for (int i = 0; i < _iMaxCount; i++)
            {
                lData[i] = _lData[i];
            }

            _lData = lData;
            _iMaxCount = iMaxCount;
        }
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        private void BubbleSort(Func<T,T,bool> iCompareable )
        {
            bool swap = true;
            for (int i = 0; i < _iCount; i++)
            {
                swap = false;
                for (int j = 1; j < _iCount-i; j++)
                {
                    if(!iCompareable(_lData[j-1],_lData[j]))
                    {
                        T oData = _lData[j];
                        _lData[j] = _lData[j - 1];
                        _lData[j - 1] = oData;
                        swap = true;
                    }
                }
                if (swap == false)
                    return;
            }
        }
        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        private void SelectSort(Func<T, T, bool> iCompareable)
        {
            if (_iCount <= 1)
                return;
            
            int iMinIndex = 0;
            for (int i = 0; i < _iCount; i++)
            {
                T oMinData = _lData[i];
                for (int j = i+1; j < _iCount; j++)
                {
                    if (iCompareable(_lData[j], oMinData))
                    {
                        oMinData = _lData[j];
                        iMinIndex = j;
                    }
                }
                _lData[iMinIndex] = _lData[i];
                _lData[i] = oMinData;
            }
        }
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        private void InsertSort(Func<T, T, bool> iCompareable)
        {
            if (_iCount <= 1)
                return;
            for (int i = 1; i < _iCount; i++)
            {
                T oData = _lData[i];
                int j = i;
                for (; j > 0 ; j--)
                {
                    if (iCompareable(_lData[j - 1], oData))
                        break;

                    _lData[j] = _lData[j - 1];
                }
                _lData[j] = oData;
            }
        }
        /// <summary>
        /// 归并排序
        /// </summary>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        private void MergeSort(Func<T, T, bool> iCompareable)
        {
            if (_iCount <= 1)
                return;
            MergeSort(0, _iCount - 1, iCompareable);
        }
        private void MergeSort(int iBeginIndex, int iEndIndex, Func<T, T, bool> iCompareable)
        {
            if (iBeginIndex == iEndIndex)
                return;

            int iMiddleIndex = iBeginIndex + (iEndIndex - iBeginIndex) / 2;
            MergeSort(iBeginIndex, iMiddleIndex, iCompareable);
            MergeSort(iMiddleIndex + 1, iEndIndex, iCompareable);
            MergeList(iBeginIndex, iMiddleIndex, iEndIndex, iCompareable);
        }
        private void MergeList(int iBeginIndex , int iMiddleIndex, int iEndIndex, Func<T, T, bool> iCompareable)
        {
            T[] lTmp = new T[iEndIndex - iBeginIndex + 1];
            int iLeftIndex = iBeginIndex;
            int iRightIndex = iMiddleIndex + 1;
            int iTmpIndex = 0;
            while(true)
            {
                if (iLeftIndex == iMiddleIndex + 1 &&
                    iRightIndex == iEndIndex + 1)
                    break ;

                if (iLeftIndex == iMiddleIndex + 1)
                {
                    lTmp[iTmpIndex++] = _lData[iRightIndex++];
                    continue;
                }
                if (iRightIndex == iEndIndex + 1)
                {
                    lTmp[iTmpIndex++] = _lData[iLeftIndex++];
                    continue;
                }

                if (iCompareable(_lData[iLeftIndex], _lData[iRightIndex]))
                    lTmp[iTmpIndex++] = _lData[iLeftIndex++];
                else
                    lTmp[iTmpIndex++] = _lData[iRightIndex++];
            }
            for (int i = 0; i < iTmpIndex; i++)
            {
                _lData[iBeginIndex + i] = lTmp[i];
            }
        }
        /// <summary>
        /// 快速排序
        /// </summary>
        private void QuickSort(Func<T, T, bool> iCompareable)
        {
            if (_iCount <= 1)
                return;
            QuickSort(0, _iCount - 1, iCompareable);
        }
        private void QuickSort(int iBeginIndex, int iEndIndex, Func<T, T, bool> iCompareable)
        {
            if (iBeginIndex >= iEndIndex)
                return;

            int iLeftIndex = iBeginIndex;
            int iRightIndex = iBeginIndex;
            T oData = default(T);
            while(iRightIndex < iEndIndex)
            {
                if(iCompareable(_lData[iRightIndex],_lData[iEndIndex]))
                {
                    oData = _lData[iRightIndex];
                    _lData[iRightIndex] = _lData[iLeftIndex];
                    _lData[iLeftIndex++] = oData;
                }
                iRightIndex++;
            }
            oData = _lData[iEndIndex];
            _lData[iEndIndex] = _lData[iLeftIndex];
            _lData[iLeftIndex] = oData;
            QuickSort(iBeginIndex, iLeftIndex - 1, iCompareable);
            QuickSort(iLeftIndex + 1, iEndIndex, iCompareable);
        }


        public void Reverse()
        {
            for (int i = 0; i < _iCount / 2; i++)
            {
                T oData = _lData[i];
                _lData[i] = _lData[_iCount - 1 - i];
                _lData[_iCount - 1 - i] = oData;
            }
        }

        public void MergeList(IList<T> lData, Func<T, T, bool> iCompareable)
        {
            ArrayList<T> lTmpData = new ArrayList<T>();
            int iLeftIndex = 0;
            int iRightIndex = 0;
            while (true)
            {
                if (iLeftIndex == _lData.Count() && iRightIndex == lData.Count())
                    break;
                if (iLeftIndex == _lData.Count())
                {
                    lTmpData.Add(lData.FindIndex(iRightIndex++));
                    continue;
                }
                if (iRightIndex == lData.Count())
                {
                    lTmpData.Add(this.FindIndex(iLeftIndex++));
                    continue;
                }
                if (iCompareable(this.FindIndex(iLeftIndex), lData.FindIndex(iRightIndex)))
                    lTmpData.Add(this.FindIndex(iLeftIndex++));
                else
                    lTmpData.Add(lData.FindIndex(iRightIndex++));
            }
            for (int i = 0; i < lTmpData.Count(); i++)
            {
                _lData[i] = lTmpData.FindIndex(i);
            }
        }

        /// <summary>
        /// 排序(1、冒泡排序。2、选择排序。3、插入排序。4、归并排序。5、快速排序)
        /// </summary>
        /// <param name="iCompareable">比较方法（T1比T2小为真，否则返回假）</param>
        public void Sort(Func<T, T, bool> iCompareable)
        {
            SortPattern eSortPattern = SortPattern.Quick;
            if (_iCount <= 10)
                eSortPattern = SortPattern.Insert;

            switch (eSortPattern)
            {
                case SortPattern.Bubble:
                    BubbleSort(iCompareable);
                    break;
                case SortPattern.Insert:
                    InsertSort(iCompareable);
                    break;
                case SortPattern.Merge:
                    MergeSort(iCompareable);
                    break;
                case SortPattern.Quick:
                    QuickSort(iCompareable);
                    break;
                case SortPattern.Select:
                    SelectSort(iCompareable);
                    break;
            }
        }
    }
}
