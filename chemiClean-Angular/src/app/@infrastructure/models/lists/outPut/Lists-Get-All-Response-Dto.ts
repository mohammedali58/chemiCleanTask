import { Pagingmodeldto } from "src/app/@core/models/pagingmodeldto";
import { Sortingmodeldto } from "src/app/@core/models/sortingmodeldto";


export interface ListGetAllResponseDto {
    id: number;
    ProductName :string;
    SupplierName :string;
    Url :string;
    LastModified: Date;
    FileContent: string;
    PagingModel: Pagingmodeldto;
    SortingModel: Sortingmodeldto;

}

