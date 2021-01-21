import { Pagingmodeldto } from "src/app/@core/models/pagingmodeldto";
import { Sortingmodeldto } from "src/app/@core/models/sortingmodeldto";

export interface ListGetAllDto {
    ProductName :string;
    SupplierName :string;
    Url :string;
    PagingModel: Pagingmodeldto;
    SortingModel: Sortingmodeldto;

}
