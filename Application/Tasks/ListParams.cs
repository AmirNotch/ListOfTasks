using Application.Core;

namespace Application.Tasks;

public class ListParams : PagingParams
{
    public string Id { get; set; }
}