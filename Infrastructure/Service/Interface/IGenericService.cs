﻿using Infrastructure.Response;

namespace Infrastructure.Service.Interface;

public interface IGenericService<T>
{
    ApiResponse<List<T>> GetAll();
    ApiResponse<T> GetById(int id);
    ApiResponse<bool> Add(T data);
    ApiResponse<bool> Update(T data);
    ApiResponse<bool> Delete(int id);
}