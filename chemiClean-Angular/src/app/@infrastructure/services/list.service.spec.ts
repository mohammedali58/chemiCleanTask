import { TestBed } from '@angular/core/testing';
import { ListsService } from './lists.service';
import { of } from 'rxjs'; 
import { ListGetAllResponseDto } from '../models/lists/outPut/Lists-Get-All-Response-Dto';
import { SortDirection } from 'src/app/@core/enums/shared.enum';

describe('ListsService', () => {
  let service: ListsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

describe('getListsAsync', () => {
  it('should return a collection of entities', () => {
    const listResponse :ListGetAllResponseDto[]= [
      {
        id:1,
        Url:'www.google.com',
        ProductName:'Absole',
        SupplierName:'supplier',
        FileContent:'',
        LastModified: new Date,
        PagingModel:{ PageNumber:1, PageSize:1},
        SortingModel:{SortingDirection:SortDirection.Ascending, SortingExpression:" "},
      }
    ];
    let response;
    spyOn(ListsService, 'getListsAsync').and.returnValue(of(listResponse));

    ListsService.getListsAsync().subscribe(res => {
      response = res;
    });

    expect(response).toEqual(listResponse);
  });
});
