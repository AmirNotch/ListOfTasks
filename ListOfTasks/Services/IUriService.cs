﻿using ListOfTasks.Filter;

namespace ListOfTasks.Services;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}