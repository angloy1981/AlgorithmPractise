using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrutAndAlgorithmPractise
{
    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="T">数据泛型</typeparam>
    public interface IList<T>
    {
        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <returns>返回列表长度</returns>
        int Count();
        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="iIndex">对象的索引</param>
        /// <returns>返回查到的对象，如果找不到报错</returns>
        T FindIndex(int iIndex);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="oData">数据实体</param>
        void Add(T oData);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="oData">数据实体</param>
        /// <param name="iIndex">索引</param>
        void Insert(T oData, int iIndex);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="iIndex">索引</param>
        void Delete(int iIndex);
        /// <summary>
        /// 列表反转
        /// </summary>
        void Reverse();
        /// <summary>
        /// 列表合并（本有序列表和另一个有序列表进行合并，合并到本有序列表）
        /// </summary>
        /// <param name="lData">另一个有序列表</param>
        /// <param name="iCompareable">比较委托（T1比T2小为真，否则返回假）</param>
        void MergeList(IList<T> lData, Func<T, T, bool> iCompareable);
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="iCompareable">比较委托（T1比T2小为真，否则返回假）</param>
        void Sort(Func<T, T, bool> iCompareable);
    }
}
