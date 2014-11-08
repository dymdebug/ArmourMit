﻿using System;
using System.Linq;

namespace RMTFRK.Core.Interface
{
    /// <summary>
    /// 基仓储实现的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:class,new()
    {
        //添加
        T AddEntities(T entity);

        //修改
        bool UpdateEntities(T entity);


        //修改
        bool DeleteEntities(T entity);


        //查询
        IQueryable<T> LoadEntities(Func<T, bool> wherelambda);


        //分页
        IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,
            out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);

    }
}